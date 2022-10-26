using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Models.DTO;
using MovieWebApp.Utility.Extension;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class StatisticsServicess
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public StatisticsServicess(IHttpClientFactory httpClientFactory)
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
        public async Task<StatisticsDTO> GetStatisticsForMonth(HttpContext context)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetStatisticsForMonth;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<StatisticsDTO>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

    }
}