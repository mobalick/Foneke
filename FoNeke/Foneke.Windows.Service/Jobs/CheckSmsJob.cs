using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Extentions;
using FoNeke.Services;
using FoNeke.Web.Models.Dto;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Repositories.Interfaces;
using log4net;
using MassTransit;
using Quartz;

namespace Foneke.Windows.SmsServer.Jobs
{
    class CheckSmsJob : IJob
    {
        ILog log = log4net.LogManager.GetLogger(typeof(CheckSmsJob));
        private readonly ISMSService _smsService;
        private readonly IBusControl _bus;

        public CheckSmsJob(ISMSService smsService, IBusControl bus)
        {
            _smsService = smsService;
            _bus = bus;
        }
        public void Execute(IJobExecutionContext context)
        {
            log.Info("...StartedCheckSmsJobJob...");

            if (_smsService.IsBusy)
            {
                log.Info("_smsService.IsBusy ...");
            }
            else
            {
                try
                {
                    var messages = _smsService.ReadSms();
                    SenToBus(messages);
                    _smsService.DeleteSms();
                }
                catch (Exception e)
                {
                    log.Error("Erreur de lectures des sms ...", e);
                }

            }

            log.Info("...EndedCheckSmsJobJob...");
        }


        private void SenToBus(List<Sms> list)
        {
            var message = new ReceivedSmsMessage { Smss = list };
            log.Info($"Sending to bus : {message.ToJsonString()}");

            _bus.Publish(message);
        }
    }
}
