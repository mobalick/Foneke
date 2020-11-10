using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace FoNeke.Services
{
    public class BusService : IBusService
    {
        public IBusControl Bus { get; set; }

        public void Initialize()
        {
            Bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            Bus.Start(); // This is important!
        }
    }
}
