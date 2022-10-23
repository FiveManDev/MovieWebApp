using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;
using MovieWebApp.Models.DTO;

namespace MovieWebApp.Service
{
    public class ReviewServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public ReviewServices(IHttpClientFactory httpClientFactory)
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
        public async Task<bool> CreateReview(HttpContext context, CreateReviewDTO createReviewDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.CreateReview, createReviewDTO);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        public async Task<List<ReviewDTO>> GetAllReviewsOfMovie(HttpContext context, string id)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetAllReviewsOfMovie + $"?MovieID={id}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<ReviewDTO>>(response.Data);
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