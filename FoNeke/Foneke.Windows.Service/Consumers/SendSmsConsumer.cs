using FoNeke.Services;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Repositories.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Extentions;

namespace Foneke.Windows.SmsServer.Consumers
{
    public class SendSmsConsumer : IConsumer<SendSmsMessage>
    {
        public ISMSService _smsService{ get; set; }

        public SendSmsConsumer(ISMSService smsService)
        {
            _smsService = smsService;
        }

        public async Task Consume(ConsumeContext<SendSmsMessage> context)
        {
            this.Log().Info($"Consuming SendSmsMessage : {context.Message.Sms.ToJsonString()}");

            foreach (var sms in context.Message.Sms)
            {
                _smsService.SendSms(sms);
            }


            await Console.Out.WriteLineAsync($"----SendSmsMessage : {context.Message.Sms.ToJsonString()}");
        }
    }
}
