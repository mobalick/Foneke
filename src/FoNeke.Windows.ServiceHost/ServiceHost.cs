using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EMM.FoNeke.Common;
using FoNeke.Services;
using FoNeke.Web.Models.Dto.Messages;
using FoNeke.Web.Repositories.Interfaces;
using FoNeke.Windows.ServiceHost.Consumers;
using MassTransit;
using Ninject;
using Ninject.Activation.Providers;

namespace FoNeke.Windows.ServiceHost
{
    

    public class ServiceHost
    {
        public IBusControl _bus { get; set; }

        public ServiceHost(IBusControl bus)
        {
            _bus = bus;
        }
        

        public void Start()
        {
            var ele = ConfigurationManager.AppSettings["HostServerName"];
            var ele2 = ConfigurationManager.AppSettings["Toto"];

            //var kernel = new StandardKernel();
            //kernel.Bind<RecievedSmsConsumer>().ToSelf();


            //_bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            //{
            //    var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
            //    {
            //        h.Username("guest");
            //        h.Password("guest");
            //    });

            //    sbc.ReceiveEndpoint(host, "test_queue", ep =>
            //    {
            //        ep.LoadFrom(kernel);

            //        //ep.Consumer<RecievedSmsConsumer>();

            //        //ep.Handler<YourMessage>(context =>
            //        //{
            //        //    return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
            //        //});


            //    });
            //});

            //kernel.Bind<IBus>().ToProvider(new CallbackProvider<IBus>(x => x.Kernel.Get<IBusControl>()));

            //_bus.Start(); // This is important!

            //_bus.Publish(new RecievedSmsMessage { Smss = new List<Web.Models.Dto.Sms>{ new Web.Models.Dto.Sms {Text ="Hi" } } });

            //_bus.Publish(new ReceivedSmsMessage { Smss = new List<Web.Models.Dto.Sms> { new Web.Models.Dto.Sms { Text = "http://maps.google.com/maps?q=+14.44799,-017.02071 Date:2018-8-16 Time:15:1 ID:6028046657 Fix:A Speed:38KM/H Bat:5" } } });


            this.Log().Info("Foneke.Windows.Service Started.");
        }



        public void Stop()
        {
            this.Log().Info("Foneke.Windows.Service Stoped.");

            _bus?.Stop();

        }
    }
    



}
