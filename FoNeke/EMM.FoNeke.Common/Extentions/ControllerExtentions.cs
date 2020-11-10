using System.Web.Mvc;

namespace EMM.FoNeke.Common.Extentions
{
    public static class ControllerExtentions
    {
        public static string GetQueryString(this Controller controller, string queryStringName)
        {
            return controller.ValueProvider.GetValue(queryStringName) != null
                ? controller.ValueProvider.GetValue(queryStringName).AttemptedValue
                : string.Empty;
        }
    }
}