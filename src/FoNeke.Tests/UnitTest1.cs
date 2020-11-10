using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FoNeke.Services;
using FoNeke.Web.Models.Directory;
using FoNeke.Web.Repositories.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoNeke.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public async Task SendAsync(string subject, string body, string to)
        {
            using (var message = new MailMessage("foneke@outlook.fr", to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                using (var client = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.office365.com",
                    EnableSsl = true,
                    
                    //Credentials = new NetworkCredential("malickmbaye@outlook.com", "worldpeace123", "outlook.com"),
                    Credentials = new NetworkCredential("foneke@outlook.fr", "Prd2V6NQ42RTM6S", "outlook.com"),
                })
                {
                    await client.SendMailAsync(message);
                }
            }
        }



        [TestMethod]
        public async Task InitDefaultUserAsync()
        {
            var entrepriseRepo = new EntrepriseRepository();
            if (!entrepriseRepo.GetAll().Any(p => p.EntrepriseName == "AdminEntrePrise"))
            {
                var entreprise = new Entreprise
                {
                    Name = "AdminEntrePrise",
                    EntrepriseName = "AdminEntrePrise"
                };

                entreprise = await new EntrepriseRepository().SaveOrUpdate(entreprise);

                var user = new User
                {
                    LastName = "Admin",
                    FirstName = "Admin",
                    Email = "malickmbaye@outlook.com",
                    Roles = new List<RoleEnum> {RoleEnum.AdminSoc, RoleEnum.SuperAdmin, RoleEnum.User},
                    IdEntreprise = entreprise.Id
                };

                user = await new UserRepository().SaveOrUpdate(user);
            }

            await SendAsync("helloo", "totototo", "elhadji.malick.mbaye@gmail.com");
        }

        [TestMethod]
        public void ImportCarMakeModel()
        {

            var importServices = new ImportService();

             importServices.SendSMSFromTPLink();
        }

        [TestMethod]
        public void GeoCodeAdresse()
        {

            var geoService = new GeoService(new DevicePositionRepository());

            geoService.GeoCodeAdresses();
        }


        [TestMethod]
        public void CanParseUrl()
        {

            var text = "GPS! lat:48.97881 long:2.38560 speed:000.8 T:04/28/18 14:41 http://maps.google.com/maps?f=q&q=48.97881,2.38560&z=16";

            var urls = Regex.Matches(text, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
            foreach (Match url in urls)
            {
                var uri = new Uri(url.Value);
                var parts = uri.Query.Split('q').ToList();

                var lonlat = parts.IndexOf("q");


            }
        }


        [TestMethod]
        public void CanSendSMS()
        {
            //var service = new SMSService(new BusService());
        }

    }
}
