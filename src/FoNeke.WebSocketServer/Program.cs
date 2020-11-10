using FoNeke.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common.Extentions;
using FoNeke.Services;
using FoNeke.Web.Models.Actions;
using FoNeke.Web.Repositories.Implementations;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace FoNeke.WebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketSharp.Server.WebSocketServer(int.Parse(ConfigurationManager.AppSettings["WebSocketPort"]));
            wssv.AddWebSocketService<SimComunication>($"/{ConfigurationManager.AppSettings["WebSocketService"]}");
            wssv.AuthenticationSchemes = AuthenticationSchemes.Basic;
            wssv.UserCredentialsFinder = id => {
                var name = id.Name;
                // Return user name, password, and roles.
                return name == ConfigurationManager.AppSettings["WebSocketUser"]  
                    ? new NetworkCredential(name, ConfigurationManager.AppSettings["WebSocketPassword"], "admin")
                    : null; // If the user credentials are not found.
            };
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();
        }
    }

    public class SimComunication : WebSocketBehavior
    {
        private DevicePositionRepository DevicePositionRepository { get;}
        private VehicleRepository VehicleRepository { get;}
        private GeoService GeoService { get; }

        public SimComunication()
        {
            DevicePositionRepository = new DevicePositionRepository();
            VehicleRepository = new VehicleRepository();
            GeoService = new GeoService(DevicePositionRepository);
        }


        protected override void OnMessage(MessageEventArgs e)
        {
            
            if (HandleMessage(e.Data))
            {
                Sessions.Broadcast(e.Data);
            }

        }

        private bool HandleMessage(string data)
        {
            var message = data.FromJsonString<WebSocketMessage<List<Sms>>>();
            if (message == null) return true;
            

            switch (message.ApplicationName)
            {
                case "FoNeke":
                    return HandleFoNeke(message);
            }

            return true;
        }

        private bool HandleFoNeke(WebSocketMessage<List<Sms>> message)
        {

            //http://maps.google.com/maps?q=+14.44799,-017.02071 Date:2018-8-16 Time:15:1 ID:6028046657 Fix:A Speed:38KM/H Bat:5

            foreach (var sms in message.MessageObject)
            {
                sms.Text = "http://maps.google.com/maps?q=+14.44799,-017.02071 Date:2018-8-16 Time:15:1 ID:6028046657 Fix:A Speed:38KM/H Bat:5";
                if (sms.Text.Contains("maps"))
                {
                    var tab = sms.Text.Substring(sms.Text.IndexOf("q=", StringComparison.Ordinal) + 2).Split(' ');

                    var position = new DevicePosition
                    {
                        Number = sms.From,
                        Latitude = double.Parse(tab[0].Split(',')[0], CultureInfo.InvariantCulture),
                        Longitude = double.Parse(tab[0].Split(',')[1], CultureInfo.InvariantCulture),
                        Speed = tab[5].Split(':')[1],
                        Time = sms.Date,//DateTime.ParseExact(tab[1].Split(':')[1] + " " + tab[2], "MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                        DeviceId = VehicleRepository.GetAll().Where(v => v.Device.PhoneNumber == sms.From)
                            .Select(v => v.Device.Id).FirstOrDefault()
                    };


                    if (!DevicePositionRepository.GetAll().Any(r => r.Number == position.Number && r.Time == position.Time))
                    {
                        DevicePositionRepository.SaveOrUpdate(position);
                        GeoService.GeoCodeAdresses();
                    }
                }
            }

            return false;
        }
    }



}
