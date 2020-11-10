using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMM.FoNeke.Common.Repositories;
using FoNeke.Services;
using FoNeke.Services.Interfaces;
using FoNeke.Web.Repositories.Implementations;
using FoNeke.Web.Repositories.Interfaces;
using FoNeke.Windows.ServiceHost.Consumers;
using MassTransit;
using Ninject.Modules;
using Ninject.Activation.Providers;
using FoNeke.Web.Models.Dto.Messages;
using Ninject;
using System.Configuration;

namespace FoNeke.Windows.ServiceHost
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            //Bind<ISMSService>().To<SMSService>().InSingletonScope();
            //Bind<IWebSocketService>().To<WebSocketService>().InSingletonScope();

            Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>)).InTransientScope();
            InjectRepositories();
            //InjectServices();

            

            ConfigureBus();

        }

        private void InjectRepositories()
        {
            var types = typeof(DeviceRepository).Assembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Repository"));

            foreach (var type in types)
            {
                Bind(typeof(DeviceRepository).Assembly.GetTypes().FirstOrDefault(t => t.IsInterface && t.Name.EndsWith(type.Name))).To(type);
            }
        }
        private void InjectServices()
        {
            var types = typeof(SMSService).Assembly.GetTypes().Where(type => type.IsClass && type.Name.EndsWith("Service") && type != typeof(SMSService));

            foreach (var type in types)
            {
                Bind(typeof(SMSService).Assembly.GetTypes().FirstOrDefault(t => t.IsInterface && t.Name.EndsWith(type.Name))).To(type);
            }
        }

        private void ConfigureBus()
        {
            //Bind<IConsumer>().To<RecievedSmsConsumer>().InTransientScope();
            
            //Bind<RecievedSmsConsumer>().ToSelf();


            var types = GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IConsumer))).ToArray();

            Kernel.Bind(types).ToSelf();

            var _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri($"rabbitmq://{ConfigurationManager.AppSettings["RabbitMqUrl"]}"), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["RabbitMqUser"]);
                    h.Password(ConfigurationManager.AppSettings["RabbitMqPassword"]);
                });

                sbc.ReceiveEndpoint(host, $"{ConfigurationManager.AppSettings["RabbitMqQueueName"]}_ServiceHost", ep =>
                {
                    ep.LoadFrom(Kernel);

                    //ep.Consumer<RecievedSmsConsumer>();

                    //ep.Handler<YourMessage>(context =>
                    //{
                    //    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    //});


                });
            });

            _bus.Start();

            //Bind<IBusControl>().ToProvider(new CallbackProvider<IBusControl>(x => _bus)).InSingletonScope();

            Bind<IBusControl>().ToConstant(_bus).InSingletonScope();
            Bind<IBus>().ToConstant(_bus).InSingletonScope();

        }
    }
}
