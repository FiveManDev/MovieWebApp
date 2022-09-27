using Microsoft.AspNetCore.Mvc;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Utility.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MovieWebApp.API
{
    public class HomeController : Controller
    {
        [Route("home")]
        public object index()
        {
            //Các ghi access token và refresh token
            //HttpContext.Response.Cookies.Append("access_token", "ghi access token ở đây", new CookieOptions { HttpOnly = true });
            //HttpContext.Response.Cookies.Append("refresh_token", "ghi refresh token ở đây", new CookieOptions { HttpOnly = true });
            //cách đọc token
            //string accessToken =  HttpContext.Request.Cookies["access_token"];
            //string refreshToken =  HttpContext.Request.Cookies["refresh_token"];

            var param = new
            {
                UserName ="S" ,
                Password = "",
            };

            var a = BaseRequestApi.Get(MovieApiUrl.test);
            var value = a.Result.Data;
            List<xs> myDeserializedClass = value.ToModel<List<xs>>();
            return myDeserializedClass;
        }

    }
    public class xs
    {
        public string name { get; set; }
        public string id { get; set; }
    }
}
