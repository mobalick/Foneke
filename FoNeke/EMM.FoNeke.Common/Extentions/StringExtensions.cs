using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;

namespace EMM.FoNeke.Common.Extentions
{
    public static class StringExtentions
    {
        private static ResourceManager _resourceManager;
         
        private static ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager("Foneke.Resources.UI", Assembly.Load("FoNeke.Resources"));
                }
                return _resourceManager;
            }
        }

        public static List<T> ToList<T>(this string str) where T:class
        {
            var listStr = str.Trim(',').Split(',');
            return listStr.Select(s => s as T).ToList();
        }

        public static List<string> ToList(this string str)
        {
            return ToList<string>(str);
        }


        public static string ToLocalized(this string str)
        {
            return ResourceManager.GetString(string.IsNullOrEmpty(str) ? "EmptyValue" : str) ?? str;
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJsonString<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}