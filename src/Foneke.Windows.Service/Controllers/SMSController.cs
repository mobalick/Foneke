using System;
using System.Collections.Generic;
using System.Web.Http;
using FoNeke.Services;
using FoNeke.Services.Interfaces;
using FoNeke.Web.Models.Dto;


namespace Foneke.Windows.SmsServer.Controllers
{
    public class SMSController : ApiController
    {
        private readonly ISMSService _smsService;

        public SMSController(ISMSService smsService)
        {
            _smsService = smsService;
        }

        public string Get(int id)
        {
            return "hellooo";
        }

        [HttpPost]
        public string Post([FromBody] SmsModel model)
        {
            try
            {
                _smsService.SendSms(model.Number, model.Text);

                return "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return "Error";
            }
        }

        [HttpGet]
        public List<Sms> Get()
        {
            return _smsService.ReadSms();
        }
    }

}
