using MovieWebApp.Utility.Extension;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace MovieWebApp.API.ApiConfig
{
    public class BaseRequestApi
    {
        private static HttpClient ConfigRequest(string token = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AppSettings.Host);
            client.DefaultRequestHeaders.Accept.Clear();
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(double.Parse(AppSettings.TimeOut));
            return client;
        }
        public static async Task<ApiResponse> Get(string ApiUrl, object ReqParams = null, string Token =null )
        {
            try
            {
                using HttpClient client = ConfigRequest(Token);
                var param = ReqParams.GetQueryString();
                var result = client.GetAsync(ApiUrl + param).Result;
                var streamRead = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(streamRead);
                var response = jsonResult.ToObject<ApiResponse>();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public static async Task<ApiResponse> Post(string ApiUrl, object ReqParams = null, string Token = null)
        {
            try
            {
                var param = ReqParams.GetQueryString();
                using HttpClient client = ConfigRequest(Token);
                HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
                var result = client.PostAsync(ApiUrl + param, content).Result;
                var streamRead = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(streamRead);
                var response = jsonResult.ToObject<ApiResponse>();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public static async Task<ApiResponse> Put(string ApiUrl, object ReqParams = null, string Token = null)
        {
            try
            {
                var param = ReqParams.GetQueryString();
                using HttpClient client = ConfigRequest(Token);
                HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
                var result = client.PutAsync(ApiUrl + param, content).Result;
                var streamRead = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(streamRead);
                var response = jsonResult.ToObject<ApiResponse>();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public static async Task<ApiResponse> Delete(string ApiUrl, object ReqParams = null, string Token = null)
        {
            try
            {
                var param = ReqParams.GetQueryString();
                using HttpClient client = ConfigRequest(Token);
                var result = client.DeleteAsync(ApiUrl + param).Result;
                var streamRead = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(streamRead);
                var response = jsonResult.ToObject<ApiResponse>();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
    }
}
