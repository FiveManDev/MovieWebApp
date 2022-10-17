using MovieAPI.Models.DTO;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class ProfileServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public ProfileServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void getClient(HttpContext context)
        {
            _httpClient = _httpClientFactory.CreateClient("api");
            var result = context.Request.Cookies.TryGetValue("accessToken", out string token);
            if (result)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<UserDTO> GetInformation(HttpContext context, string id)
        {
            getClient(context);
            try
            {
                string url = $"/api/v1/Profile/GetInformation?UserID={id}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                return ExtensionMethods.ToModel<UserDTO>(response.Data);
            }
            catch (Exception ex)
            {
                return null;
            }

            //var request = new HttpRequestMessage(HttpMethod.Get, url);

            ////var client = _httpclientfactory.createclient();
            //var response = await _httpClient.SendAsync(request);
            //if (response.IsSuccessStatusCode)
            //{
            //    var rawData = await response.Content.ReadAsStringAsync();
            //    var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
            //    if (responseApi.IsSuccess)
            //    {
            //        return ExtensionMethods.ToModel<UserDTO>(responseApi.Data);
            //    }
            //}
            //return null;
        }


    }
}
