using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Services;
using FoNeke.Services.Interfaces;
using FoNeke.Web.Repositories.Implementations;
using MassTransit;
using Ninject.Modules;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Models.Dto;
using log4net;

namespace Foneke.Windows.SmsServer
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Log().Info("Foneke Windows SmsServer => NinjectBindings");

            Bind<ISMSService>().To<SMSService>().InSingletonScope();
            //Bind<IWebSocketService>().To<WebSocketService>().InSingletonScope();

            Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>)).InTransientScope();

            var types =  typeof(DeviceRepository).Assembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Repository"));

            foreach (var type in types)
            {
                Bind(typeof(DeviceRepository).Assembly.GetTypes().FirstOrDefault(t =>t.IsInterface && t.Name.EndsWith(type.Name))).To(type);
            }
            ConfigureBus();
        }

        private void ConfigureBus()
        {

            var types = GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IConsumer))).ToArray();

            Kernel.Bind(types).ToSelf();

            var _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri($"rabbitmq://{ConfigurationManager.AppSettings["RabbitMqUrl"]}"), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMqUser"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMqPassword"]);
                });
               
                sbc.ReceiveEndpoint(host, $"{ConfigurationManager.AppSettings["RabbitMqQueueName"]}_SmsServer", ep =>
                {
                    ep.LoadFrom(Kernel);

                    //ep.Consumer<RecievedSmsConsumer>();

                    //ep.Handler<YourMessage>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    //});


                });
            });
            try
            {
                _bus.Start();
            }
            catch (Exception e)
            {
                this.Log().Error("error starting the bus", e);
            }

            //Bind<IBusControl>().ToProvider(new CallbackProvider<IBusControl>(x => _bus)).InSingletonScope();
            //_bus.Publish(new ReceivedSmsMessage { Smss = new List<Sms>{ new Sms {From= "+33789028156", Text = "http://maps.google.com/maps?q=+14.44799,-017.02071 Date:2018-8-16 Time:15:1 ID:6028046657 Fix:A Speed:38KM/H Bat:5" } } });

            Bind<IBusControl>().ToConstant(_bus).InSingletonScope();
            Bind<IBus>().ToConstant(_bus).InSingletonScope();

        }
    }
}
