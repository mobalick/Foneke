using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Web.Models.Dto.Messages
{
    public class SendSmsMessage : BaseMessage
    {
        public List<Sms> Sms { get; set; }
    }
}
