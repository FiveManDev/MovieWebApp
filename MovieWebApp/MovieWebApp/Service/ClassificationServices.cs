using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Models.DTO;
using MovieWebApp.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieWebApp.Service
{
    public class ClassificationServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public ClassificationServices(IHttpClientFactory httpClientFactory)
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
        public async Task<List<ClassificationDTO>> GetAllClassification(HttpContext context)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(MovieApiUrl.GetAllClassification);
                return ExtensionMethods.ToModel<List<ClassificationDTO>>(response.Data);
            }
            catch
            {
                return null;
            }

        }
    }
}