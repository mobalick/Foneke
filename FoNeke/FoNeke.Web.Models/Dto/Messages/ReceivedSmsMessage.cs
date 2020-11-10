using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Web.Models.Dto.Messages
{
    public class ReceivedSmsMessage : BaseMessage
    {
        public List<Sms> Smss { get; set; }
    }
}
