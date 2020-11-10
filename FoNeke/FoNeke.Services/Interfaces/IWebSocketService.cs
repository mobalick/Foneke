using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace FoNeke.Services.Interfaces
{
    public interface IWebSocketService : IDisposable
    {
        void SendMessage(string message);

        void Connect(Func<object, object, object> onMessage);
    }
}
