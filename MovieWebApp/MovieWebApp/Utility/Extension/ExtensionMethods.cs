using MovieWebApp.API;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;

namespace MovieWebApp.Utility.Extension
{
    public static class ExtensionMethods
    {
        public static string GetQueryString(this object obj)
        {
            if (obj != null)
            {
                var properties = from p in obj.GetType().GetProperties()
                                 where p.GetValue(obj, null) != null
                                 select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)!.ToString());
                return "?"+string.Join("&", properties.ToArray());
            }
            return "";
        }
        public static T ToModel<T>(this object obj)
        {
            T t = JsonConvert.DeserializeObject<T>(obj.ToString());
            return t;
        }
    }
}
