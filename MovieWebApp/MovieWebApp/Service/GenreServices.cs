using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class GenreServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public GenreServices(IHttpClientFactory httpClientFactory)
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
        public async Task<List<GenreDTO>> GetAllGenre(HttpContext context)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetAllGenre;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<GenreDTO>>(response.Data);
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