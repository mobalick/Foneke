using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Extentions;
using FoNeke.Web.Models.Dto;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Repositories.Interfaces;
using MassTransit;
using Quartz;

namespace FoNeke.Windows.ServiceHost.Jobs
{
    public class SendSmsJob : IJob
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IBusControl _bus;

        public SendSmsJob(IBusControl bus, IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
            _bus = bus;
        }
        public void Execute(IJobExecutionContext context)
        {
            this.Log().Info("StartedSendSmsJob...");
            
            var sms = _deviceRepository.GetAll().Select(d => new Sms() {To = d.PhoneNumber, Text = "6691251"}).ToList();
 
            _bus.Publish(new SendSmsMessage(){ Sms = sms});
           

            this.Log().Info("EndedSendSmsJob...");
        }
    }

}
