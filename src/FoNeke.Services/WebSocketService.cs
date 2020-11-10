using System;
using FoNeke.Services.Interfaces;
using WebSocketSharp;


namespace FoNeke.Services
{
    public  class WebSocketService : IWebSocketService
    {
        public readonly WebSocket _webSocket;

        protected WebSocketService()
        {
            _webSocket = new WebSocket("ws://localhost:7416/SimComunication");
        }

        public void SendMessage(string message)
        {
            _webSocket.Send(message);
        }

        public void Connect(Func<object, object, object> onMessage)
        {
            _webSocket.OnMessage += (sender, e) => { onMessage(sender, e); }; 
            _webSocket.Connect();
        }

        public void Dispose()
        {
            ((IDisposable) _webSocket)?.Dispose();
        }
    }
}