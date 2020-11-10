using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Web.Models.Dto
{
    public class WebSocketMessage<T>
    {
        public WebSocketMessageType MessageType { get; set; }
        public string ApplicationName { get; set; }
        public T MessageObject { get; set; }
    }

    public enum WebSocketMessageType
    {
        Undefined,
        SendSms,
        ReadSms,
        DeleteSms,
        SmsReceived
    }
}
