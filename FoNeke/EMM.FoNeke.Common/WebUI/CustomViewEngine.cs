using System.Linq;
using System.Web.Mvc;

namespace EMM.FoNeke.Common.WebUI
{
    public class CustomViewEngine : RazorViewEngine
    {
        private static readonly string[] NewReferentialsViewFormats =
        {
            "~/Views/Referentials/{1}/{0}.cshtml",
            "~/Views/Shared/Referentials/{0}.cshtml",
            "~/Views/Directory/{1}/{0}.cshtml",
            "~/Views/Shared/Directory/{0}.cshtml",
            "~/Views/Addressing/{1}/{0}.cshtml",
            "~/Views/Shared/Addressing/{0}.cshtml"
        };

        public CustomViewEngine()
        {
            base.ViewLocationFormats = base.ViewLocationFormats.Union(NewReferentialsViewFormats).ToArray();
        }
    }
}