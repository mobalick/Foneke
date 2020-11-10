using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.FoNeke.Common.Extentions
{
   public static class DateTimeExtentions
    {
       public static string GetElapsedTimeUntil(this DateTime start, DateTime end)
       {
           var span = end.Subtract(start);
           
           if (span.TotalSeconds < 60)
           {
               return String.Format("{0} seconde(s)", span.Seconds);
           }
           if (span.TotalMinutes < 60)
           {
               return String.Format("{0} minute(s)", span.Minutes);
           }
           if (span.TotalHours < 24)
           {
               return String.Format("{0} heure(s) {1} minute(s)", span.Hours,span.Minutes);
           }
           if (span.TotalDays < 31)
           {
               return String.Format("{0} jour(s) {1} heure(s)", span.Days, span.Hours);
           }
           if (span.TotalDays < 365)
           {
               return String.Format("{0} mois", span.Days/31);
           }
           if (span.TotalDays > 365)
           {
               return String.Format("{0} année(s)", span.Days / 365);
           }
           return "";
       }
    }
}
