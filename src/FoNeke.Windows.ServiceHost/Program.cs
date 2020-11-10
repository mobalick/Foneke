using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoNeke.Windows.ServiceHost.Jobs;
using log4net;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;

namespace FoNeke.Windows.ServiceHost
{
    class Program
    {


        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                ILog log = log4net.LogManager.GetLogger(typeof(Program));

                c.UseNinject(new NinjectBindings()); //Initiates Ninject and consumes Modules

                c.Service<ServiceHost>(s =>
                {
                    //Specifies that Topshelf should delegate to Ninject for construction
                    s.ConstructUsingNinject();
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());

                    // Topshelf.Quartz.Ninject (Optional) - Construct IJob instance with Ninject
                    s.UseQuartzNinject();

                    // Schedule a job to run in the background every 5 seconds.
                    // The full Quartz Builder framework is available here.
                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                                JobBuilder.Create<SendSmsJob>().Build())
                            .AddTrigger(() =>
                                TriggerBuilder.Create()
                                    .StartAt(DateTimeOffset.Now.AddSeconds(30))
                                    .WithSimpleSchedule(builder => builder
                                        .WithIntervalInMinutes(120)
                                        .RepeatForever())
                                    .Build())
                    );

                });

                c.RunAsNetworkService();
                c.StartAutomatically();

                c.SetServiceName("ServiceHost");
                c.SetDisplayName("FoNeke.Windows.ServiceHost");
                c.SetDescription("FoNeke.Windows.ServiceHost");
            });

        }
    }
}
