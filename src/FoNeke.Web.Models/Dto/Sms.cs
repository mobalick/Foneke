using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoNeke.Web.Models.Dto
{
    public class Sms
    {
        public DateTime? Date { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
    }
}
