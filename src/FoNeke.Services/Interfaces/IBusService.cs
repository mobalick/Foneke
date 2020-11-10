using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace FoNeke.Services
{
    interface IBusService
    {
        IBusControl Bus { get; set; }
        void Initialize();
    }
}
