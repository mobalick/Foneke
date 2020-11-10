using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.FoNeke.Common.BaseObjects
{
   public static class Format
    {
       public static string ShortDateString
       {
           get
           {
               return string.Format("{{0:{0}}}", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
           }

       }

       public static string ShortDateTimeString
       {
           get
           {
               return string.Format("{{0:{0} {1}}}", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern);
           }

       }
    }
}
