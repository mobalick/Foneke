using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FoNeke.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FoNeke.Web.Controllers.Identity;
using FoNeke.Web.Repositories.Interfaces;
using FoNeke.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Globalization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using FoNeke.Web.Models.Actions;
using System.Text.RegularExpressions;
using EMM.FoNeke.Common.Extentions;
using FoNeke.Web.Models.Dto;
using WebSocketSharp;

namespace FoNeke.Web.Controllers.Home
{
    [Authorize]
    public class HomeController : Controller
    {

        public IDevicePositionRepository DevicePositionRepository { get; set; }
        public IVehicleRepository VehicleRepository { get; set; }
       // public ISMSService SMSService { get; set; }
        public IGeoService GeoService { get; set; }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public HomeController()
        {
            
        }

        public ApplicationUser CurrentUser => UserManager.FindById(User.Identity.GetUserId());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendSMS()
        {
            return View(new SmsModel());
        }

        [HttpPost]
        public ActionResult SendSMS(SmsModel sms)
        {

            var url = $"ws://{ConfigurationManager.AppSettings["HostServerName"]}:{ConfigurationManager.AppSettings["WebSocketPort"]}/{ConfigurationManager.AppSettings["WebSocketService"]}";
            using (var ws = new WebSocket(url))
            {
                ws.SetCredentials(ConfigurationManager.AppSettings["WebSocketUser"], ConfigurationManager.AppSettings["WebSocketPassword"], true);

                ws.Connect();

                var message = new WebSocketMessage<SmsModel>
                {
                    MessageType = WebSocketMessageType.SendSms,
                    MessageObject = sms
                };

                ws.Send(message.ToJsonString());

                ws.Close();
            }
            
            //var sud = SMSService.SendSms(sms.To,sms.Text);
            
            return View(sms);
        }


        public ActionResult GeoCodeAdresses()
        {
            GeoService.GeoCodeAdresses();

            return Json(new { success = "ok" });
        }

       public ActionResult ReadSMS()
        {

            //var sud = SMSService.ReadSms();

            //foreach (var item in sud)
            //{
            //    //https://stackoverflow.com/questions/19193251/regex-to-get-the-words-after-matching-string

            //    //GPS! lat:48.75251 long:2.40817 speed:006.4 T:03/28/18 21:28 http://maps.google.com/maps?f=q&q=48.75251,2.40817&z=16

            //    if (item.Text.Contains("GPS!"))
            //    {
            //        var tab = item.Text.Split(' ');

            //        var position = new DevicePosition
            //        {
            //            Number = item.From,
            //            Latitude = double.Parse(tab[1].Split(':')[1], CultureInfo.InvariantCulture),
            //            Longitude = double.Parse(tab[2].Split(':')[1], CultureInfo.InvariantCulture),
            //            Speed = double.Parse(tab[3].Split(':')[1], CultureInfo.InvariantCulture),
            //            Time = DateTime.ParseExact(tab[4].Split(':')[1] + " " + tab[5], "MM/dd/yy HH:mm",CultureInfo.InvariantCulture),
            //            DeviceId = VehicleRepository.GetAll().Where(v => v.Device.PhoneNumber == item.From)
            //                .Select(v => v.Device.Id).FirstOrDefault()
            //        };


            //        if (!DevicePositionRepository.GetAll().Any(r=>r.Number == position.Number &&r.Time == position.Time))
            //        {
            //            DevicePositionRepository.SaveOrUpdate(position);
            //            GeoService.GeoCodeAdresses();
            //        }
            //    }
            //}

            //return View(sud);

            return View(new List<Sms>());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //if (!Request.IsAjaxRequest() && User.Identity.IsAuthenticated)
            //    ViewBag.NewNotificationCount = NotificationRepository.GetReceivedByUser(CurrentUser.User.Id).Count(n => !n.IsRead);

        }
    }

}