using System.Web.Mvc;

namespace EMM.FoNeke.Common.WebUI
{
    public abstract class CitUIWebViewPage : WebViewPage<dynamic>
    {
    }

    public abstract class CitUIWebViewPage<TModel> : WebViewPage<TModel>
    {
        private CitUIHtmlHelper<TModel> _citUI;

        public CitUIHtmlHelper<TModel> CitUI
        {
            get { return _citUI ?? new CitUIHtmlHelper<TModel>(ViewContext, Html.ViewDataContainer); }
            set { _citUI = value; }
        }
    }
}