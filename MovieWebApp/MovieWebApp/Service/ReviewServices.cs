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
        public async Task<List<ReviewDTO>> GetTopLastestReview(HttpContext context, int count)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetTopLastestReview + $"?top={count}";
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
        public async Task<GetReviews> GetReviews(HttpContext context, string searchText, string sortBy, string sortType)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetReviews + $"?q={searchText}" + $"&sortBy={sortBy}" + "&pageSize=50" + $"&sortType={sortType}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<GetReviews>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<ReviewDTO>> GetAllReviewsOfUser(HttpContext context, string id)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetAllReviewsOfUser + $"?UserID={id}";
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

        public async Task<bool> DeleteReview(HttpContext context, string Id)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.DeleteReview + $"?ReviewID={Id}";
                var response = await _httpClient.DeleteAsync(url);

                // check status code: not yet
                var rawData = await response.Content.ReadAsStringAsync();
                var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                return responseApi.IsSuccess;
            }
            catch
            {
                return false;
            }
        }

    }
}