using EMM.FoNeke.Common.Extentions;
using FoNeke.Services;
using FoNeke.Web.Models.Actions;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Repositories.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common;

namespace FoNeke.Windows.ServiceHost.Consumers
{

    public class RecievedSmsConsumer : IConsumer<ReceivedSmsMessage>
    {
        public IDevicePositionRepository DevicePositionRepository { get; set; }
        public IVehicleRepository VehicleRepository { get; }
        public IGeoService GeoService { get; }
        public IBusControl _bus { get; set; }

        public RecievedSmsConsumer(IDevicePositionRepository devicePositionRepository,IVehicleRepository vehicleRepository, IGeoService geoService, IBusControl bus)
        {
            DevicePositionRepository = devicePositionRepository;
            VehicleRepository = vehicleRepository;
            GeoService = geoService;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<ReceivedSmsMessage> context)
        {
            this.Log().Info($"RecievedSmsConsumer Consuming : {context.Message.ToJsonString()}");

            //http://maps.google.com/maps?q=+14.44799,-017.02071 Date:2018-8-16 Time:15:1 ID:6028046657 Fix:A Speed:38KM/H Bat:5

            //http://maps.google.com/maps?q=+14.44799,-017.02071 
            //Date:2018-8-16 
            //Time:15:1 
            //ID:6028046657 
            //Fix:A 
            //Speed:38KM/H 
            //Bat:5

            foreach (var sms in context.Message.Smss)
            {
                if (sms.Text.Contains("maps"))
                {
                    var tab = sms.Text.Split(' ');

                    var position = new DevicePosition
                    {
                        Number      = sms.From,
                        Latitude    = double.Parse(tab[0].Split('=')[1].Split(',')[0], CultureInfo.InvariantCulture),
                        Longitude   = double.Parse(tab[0].Split('=')[1].Split(',')[1], CultureInfo.InvariantCulture),
                        Speed       = tab[5].Split(':')[1],
                        Time        = DateTime.Parse(tab[1].Split(':')[1]).
                                        AddHours(double.Parse(tab[2].Split(':')[1], CultureInfo.InvariantCulture)).
                                        AddMinutes(double.Parse(tab[2].Split(':')[2], CultureInfo.InvariantCulture)),//DateTime.ParseExact(tab[1].Split(':')[1] + " " + tab[2].Split(':')[1], "MM/dd/yy HH:mm", CultureInfo.InvariantCulture),
                        //DeviceId    = VehicleRepository.GetAll().Where(v => v.Device.PhoneNumber == sms.From)
                        //    .Select(v => v.Device.Id).FirstOrDefault()
                    };


                    if (!DevicePositionRepository.GetAll().Any(r => r.Number == position.Number && r.Time == position.Time))
                    {
                        //DevicePositionRepository.SaveOrUpdate(position);
                        GeoService.GeoCodeAdresses(position);
                    }
                }




            }

            this.Log().Info("RecievedSmsConsumer End Consuming");


            //await _bus.Publish(new SendSmsMessage { Sms = new Web.Models.Dto.Sms { Text = "Hi hellooo" } });

            await Console.Out.WriteLineAsync($"----RecievedSmsConsumer----");
        }
    }
}
