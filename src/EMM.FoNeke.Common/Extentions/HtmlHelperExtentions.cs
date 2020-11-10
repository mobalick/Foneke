using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using EMM.FoNeke.Common.Entities;

namespace EMM.FoNeke.Common.Extentions
{
    public static class HtmlHelperExtentions
    {
        public static string ConvertToJQueryDateFormat(this HtmlHelper html)
        {
            return ConvertDateFormat(html,
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

        public static string ConvertDateFormat(this HtmlHelper html, string format)
        {
            /*
            *  Date used in this comment : 5th - Nov - 2009 (Thursday)
            *
            *  .NET    JQueryUI        Output      Comment
            *  --------------------------------------------------------------
            *  d       d               5           day of month(No leading zero)
            *  dd      dd              05          day of month(two digit)
            *  ddd     D               Thu         day short name
            *  dddd    DD              Thursday    day long name
            *  M       m               11          month of year(No leading zero)
            *  MM      mm              11          month of year(two digit)
            *  MMM     M               Nov         month name short
            *  MMMM    MM              November    month name long.
            *  yy      y               09          Year(two digit)
            *  yyyy    yy              2009        Year(four digit)             *
            */

            string currentFormat = format;

            // Convert the date
            currentFormat = currentFormat.Replace("dddd", "DD");
            currentFormat = currentFormat.Replace("ddd", "D");

            // Convert month
            if (currentFormat.Contains("MMMM"))
            {
                currentFormat = currentFormat.Replace("MMMM", "MM");
            }
            else if (currentFormat.Contains("MMM"))
            {
                currentFormat = currentFormat.Replace("MMM", "M");
            }
            else if (currentFormat.Contains("MM"))
            {
                currentFormat = currentFormat.Replace("MM", "mm");
            }
            else
            {
                currentFormat = currentFormat.Replace("M", "m");
            }

            // Convert year
            currentFormat = currentFormat.Contains("yyyy")
                ? currentFormat.Replace("yyyy", "yy")
                : currentFormat.Replace("yy", "y");

            return currentFormat;
        }


        public static String GetGridColumnModel(this HtmlHelper html)
        {
            var sb = new StringBuilder("[");
            foreach (
                ModelMetadata metadata in
                    html.ViewData.ModelMetadata.Properties.First(p => p.PropertyName == "Item")
                        .Properties.Where(p => !p.AdditionalValues.ContainsKey("FormIgnore")))
            {
                sb.AppendFormat("{{ name: '{0}', index: '{0}', editable: true,{1} }},", metadata.PropertyName,
                    GetEditType(metadata));
            }

            sb.AppendLine("]");
            return sb.ToString();
        }

        public static String GetGridColumnNames(this HtmlHelper html)
        {
            var sb = new StringBuilder("[");

            foreach (
                ModelMetadata metadata in
                    html.ViewData.ModelMetadata.Properties.First(p => p.PropertyName == "Item")
                        .Properties.Where(p => !p.AdditionalValues.ContainsKey("FormIgnore")))
            {
                sb.AppendFormat("'{0}',", metadata.PropertyName);
            }
            sb.AppendLine("]");
            return sb.ToString();
        }

        public static IHtmlString RenderIndexGridToolbar(this HtmlHelper helper, string controllerName)
        {
            return helper.Raw(string.Format(@"<div class='widget-header widget-header-flat'>
                                    <div class='smaller'>
                                        <div class='btn-group'>
                                            <button class='btn btn-xs btn-purple' id='Grid{0}AddBtn' data-controller-name='{0}'><i class='icon-plus align-top bigger-125'></i> Ajouter</button>
                                            <button class='btn btn-xs btn-primary disabled' id='Grid{0}EditBtn' data-controller-name='{0}'><i class='icon-pencil align-top bigger-125'></i>Editer</button>
                                            <button class='btn btn-xs btn-danger disabled' id='Grid{0}DeleteBtn' data-controller-name='{0}' ><i class='icon-trash align-top bigger-125'></i>Supprimer</button>
                                        </div>
                                    </div>

                                    <div class='widget-toolbar'>
                                        <div class='btn-group'>
                                            <button class='btn btn-xs btn-yellow'><i class='icon-search align-top bigger-125'></i></button>
                                            <button class='btn btn-xs btn-default'><i class='icon-eraser align-top bigger-125'></i></button>
                                            <button class='btn btn-xs btn-grey'><i class='icon-exchange align-top bigger-125'></i></button>
                                            <button class='btn btn-xs btn-success'><i class='icon-refresh align-top bigger-125'></i></button>
                                        </div>
                                    </div>
                                </div>", controllerName));
        }


        private static string GetEditType(ModelMetadata metadata)
        {
            if (metadata.ModelType.IsSubclassOf(typeof (BaseEntity)))
            {
                return "edittype:'select', editoptions: { value: 'FE:FedEx;IN:InTime;TN:TNT;AR:ARAMEX'}";
            }
            if (metadata.ModelType.IsAssignableFrom(typeof (DateTime)) ||
                metadata.ModelType.IsAssignableFrom(typeof (DateTime?)))
            {
                return "edittype:'date', unformat: pickDate";
            }

            if (metadata.ModelType.IsAssignableFrom(typeof (bool)) ||
                metadata.ModelType.IsAssignableFrom(typeof (bool?)))
            {
                return "edittype:'checkbox', editoptions: { value: 'Yes:No' }, unformat: aceSwitch";
            }

            if (metadata.ModelType.IsAssignableFrom(typeof (int)) || metadata.ModelType.IsAssignableFrom(typeof (int?)) ||
                metadata.ModelType.IsAssignableFrom(typeof (decimal)) ||
                metadata.ModelType.IsAssignableFrom(typeof (decimal?)) ||
                metadata.ModelType.IsAssignableFrom(typeof (float)) ||
                metadata.ModelType.IsAssignableFrom(typeof (float?)))
            {
                return "edittype:'int'";
            }

            if (metadata.ModelType.IsEnum)
            {
                return "edittype:'select', editoptions: { value: 'FE:FedEx;IN:InTime;TN:TNT;AR:ARAMEX' }";
            }
            return "";
        }


        //http://stackoverflow.com/questions/5433531/using-sections-in-editor-display-templates/5433722#5433722
        public static MvcHtmlString Script(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        htmlHelper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return MvcHtmlString.Empty;
        }
    }
}