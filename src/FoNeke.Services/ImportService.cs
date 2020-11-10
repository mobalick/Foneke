using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FoNeke.Web.Models.Vehicles;
using FoNeke.Web.Repositories.Implementations;
using FoNeke.Web.Repositories.Interfaces;

namespace FoNeke.Services
{
    public class ImportService : IImportService
    {
        public IVehicleMakeRepository VehicleMakeRepository { get; set; }
        public IVehicleModelRepository VehicleModelRepository { get; set; }

        public void  ImportCarMakeAndModelAsync()
        {
            var str = "http://www.sra.asso.fr/zendsearch/automobiles/xhr-get-datas".PostJsonToUrl(new { type ="modele", marque= "AU"});


            var makes = str.TrimStart('?','(').TrimEnd(';',')').FromJson<Rootobject>();

            foreach (var makesMake in makes.Makes)
            {
                
                var make = new VehicleMake {Make = makesMake.make_display};
                if (VehicleMakeRepository.GetAll().Any(m=>m.Make==make.Make))
                {
                    continue;
                }

                VehicleMakeRepository.SaveOrUpdate(make);

                var u = "https://www.carqueryapi.com/api/0.3?cmd=getModels&make=" + makesMake.make_display;
                var modelStrs = u.GetJsonFromUrl();
                var modem = modelStrs.TrimStart('?', '(').TrimEnd(';', ')').FromJson<Rootobject>();


                foreach (var modemModel in modem.Models)
                {
                    var model=new VehicleModel{CarMake = make,ModelName = modemModel.model_name};
                   

                    if (VehicleModelRepository.GetAll().Any(m => m.CarMake.Make == make.Make && m.ModelName==model.ModelName))
                    {
                        continue;
                    }


                    VehicleModelRepository.SaveOrUpdate(model);
                }
            }
        }


        private const string Password = "mynigg@1251";
        private const string RouterUrl = "http://tplinkmodem.net";
        public void SendSMSFromTPLink()
        {
            //var request = (HttpWebRequest)WebRequest.Create("http://tplinkmodem.net");

            ////using (var resp = request.GetResponse())
            ////{
            //    string authString = Base64Encode(Password);
            //    Cookie c = new System.Net.Cookie("Authorization",
            //        "Basic " + authString, "/", "tplinkmodem.net");


            //    request.CookieContainer = new CookieContainer();
            //    request.CookieContainer.Add(c);
            //    request.Headers["Cookie"] = "Authorization=Basic bXluaWdnQDEyNTE=";



            //    using (var resp2 = request.GetResponse())
            //    {
            //      var eee=  resp2.ReadToEnd();
            //    }

            ////}


            //SendSms("192.168.1.1", "", Password,"777944565","helloo");



            var auth = $"Basic {Base64Encode(Password)}";


            //Cookie cookie = new Cookie("Authorization", auth) { Expires = DateTime.Now.AddDays(1) };

            //Uri uri = new Uri($"{RouterUrl}/cgi?1");
            //var cookieContainer = new CookieContainer();
            //cookieContainer.Add(uri, cookie);



            var url = "http://192.168.1.1/cgi?5".PostToUrl(@"[LTE_SMS_SENDMSGBOX#0,0,0,0,0,0#0,0,0,0,0,0]0,1
PageNumber=0
", requestFilter: req =>
            {
                req.Headers["Accept-Encoding"] = "gzip, deflate";
                   //req.CookieContainer = cookieContainer;
                   req.Headers["Cookie"] = "Authorization=Basic bXluaWdnQDEyNTE=";
                   //req.Host = "tplinkmodem.net";
                   //req.Headers["Pragma"] = "no-cache";

                   //req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
               });


        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }













        /// <summary>
        /// Gets MD5 hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>MD5 hash</returns>
        private static string GetMD5Hash(string password)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var bytes = md5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }

                return sb.ToString().ToLower();
            }
        }

        /// <summary>
        /// Send SMS with TP-LINK TL-MR6400 1.0.12 Build 160322 Rel.33912n
        /// </summary>
        /// <param name="ipAddress">IP Address of the TL-MR6400 device</param>
        /// <param name="userName">Administrator username</param>
        /// <param name="password">Password</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="message">SMS text</param>
        /// <returns>True if sms was sent</returns>
        public static bool SendSms(string ipAddress,string userName, string password, string phoneNumber, string message)
        {
            // Create auth cookie
            string authString = Base64Encode(Password);
            System.Net.Cookie c = new System.Net.Cookie("Authorization",
            "Basic " + authString, "/", ipAddress);

            string responseString = null;

            // Login
            var request = (HttpWebRequest)HttpWebRequest.Create
            (string.Format("http://{0}/userRpm/LoginRpm.htm?Save=Save", ipAddress));
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(c);

            request.Headers["Cookie"] = "Authorization=Basic bXluaWdnQDEyNTE=";
            using (var resp = request.GetResponse())
            {
                using (var str = resp.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(str))
                    {
                        responseString = sr.ReadToEnd();
                    }
                }
            }

            // Get session URI
            System.Text.RegularExpressions.Regex regex =
            new System.Text.RegularExpressions.Regex("http://.*(?=/Index.htm)");
            var hostUri = regex.Match(responseString).Value;

            // Send SMS
            // Create request
            request = (HttpWebRequest)HttpWebRequest.Create(hostUri + "/lteWebCfg");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(c);
            request.Method = "POST";
            request.Referer = hostUri + "/_lte_SmsNewMessageCfgRpm.htm";

            // Not needed
            //req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //req.Accept = "application/json, text/javascript, */*; q=0.01";

            // Create JSON
            string sms = string.Format("toto",phoneNumber, message, DateTime.Now.ToString("yyyy,m,dd,HH,MM,ss"));

            // Write to request
            var smsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sms);

            using (var reqstr = request.GetRequestStream())
            {
                reqstr.Write(smsBytes, 0, smsBytes.Length);
                reqstr.Flush();
            }

            using (var resp = request.GetResponse())
            {
                using (var str = resp.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(str))
                    {
                        responseString = sr.ReadToEnd();
                        return responseString.Contains("\"result\":0");
                    }
                }
            }
        }






















    }


    public class Rootobject
    {
        public Rootobject()
        {
            Makes=new List<Make>().ToArray();
            Models = new List<Model>().ToArray();
        }
        public Make[] Makes { get; set; }
        public Model[] Models { get; set; }

    }

    public class Make
    {
        public string make_id { get; set; }
        public string make_display { get; set; }
        public string make_is_common { get; set; }
        public string make_country { get; set; }
    }
    
    public class Model
    {
        public string model_name { get; set; }
        public string model_make_id { get; set; }
    }





}
