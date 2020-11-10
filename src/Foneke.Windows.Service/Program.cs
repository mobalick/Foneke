using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Foneke.Windows.SmsServer.Jobs;
using log4net;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using Topshelf.WebApi;
using Topshelf.WebApi.Ninject;

namespace Foneke.Windows.SmsServer
{
    class Program
    {

        static void Main(string[] args)
        {
           ILog log = log4net.LogManager.GetLogger(typeof(Program));

            HostFactory.Run(c =>
            {
                c.UseNinject(new NinjectBindings()); //Initiates Ninject and consumes Modules

                c.Service<RestService>(s =>
                {
                    //Specifies that Topshelf should delegate to Ninject for construction
                    s.ConstructUsingNinject();
                    s.WhenStarted(service=> service.Start());
                    s.WhenStopped(service => service.Stop());


                    ////Topshelf.WebApi - Begins configuration of an endpoint
                    //s.WebApiEndpoint(api =>
                    //    //Topshelf.WebApi - Uses localhost as the domain, defaults to port 8080.
                    //    //You may also use .OnHost() and specify an alternate port.
                    //        api.OnHost(null, null, int.Parse(ConfigurationManager.AppSettings["ServicePort"]))
                    //            //Topshelf.WebApi - Pass a delegate to configure your routes
                    //            .ConfigureRoutes(Configure)
                    //            //Topshelf.WebApi.Ninject (Optional) - You may delegate controller 
                    //            //instantiation to Ninject.
                    //            //Alternatively you can set the WebAPI Dependency Resolver with
                    //            //.UseDependencyResolver()
                    //            .UseNinjectDependencyResolver()
                    //            //Instantaties and starts the WebAPI Thread.
                    //            .Build());

                    // Topshelf.Quartz.Ninject (Optional) - Construct IJob instance with Ninject
                    s.UseQuartzNinject();

                    // Schedule a job to run in the background every 5 seconds.
                    // The full Quartz Builder framework is available here.
                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                                JobBuilder.Create<CheckSmsJob>().Build())
                            .AddTrigger(() =>
                                TriggerBuilder.Create()
                                    .StartAt(DateTimeOffset.Now.AddSeconds(60))
                                    .WithSimpleSchedule(builder => builder
                                        .WithIntervalInMinutes(5)
                                        .RepeatForever())
                                    .Build()));

                });

                c.RunAsNetworkService();
                c.StartAutomatically();

                c.SetServiceName("FoneKe.SmsServer");
                c.SetDisplayName("FoneKe.SmsServer");
                c.SetDescription("This is an example of self-hosted web api rest service.");
            });

           // Console.ReadLine();
        }


        private static void Configure(HttpRouteCollection routes)
        {
            //routes.MapHttpRoute(
            //    "DefaultApiWithId",
            //    "Api/{controller}/{id}",
            //    new { id = RouteParameter.Optional },
            //    new { id = @"\d+" });

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});
        }
    }
}
