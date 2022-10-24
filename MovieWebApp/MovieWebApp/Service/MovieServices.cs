using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class MovieServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public MovieServices(IHttpClientFactory httpClientFactory)
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
        public async Task<List<MovieDTO>> GetTopLatestPublicationMovies(HttpContext context, int count)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetTopLatestPublicationMovies + $"?top={count}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<MovieDTO>>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<MovieDTO>> GetTopLatestReleaseMovies(HttpContext context, int count)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetTopLatestReleaseMovies + $"?top={count}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<MovieDTO>>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<MovieDTO>> GetAllMovieIsPremium(HttpContext context)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetAllMovieIsPremium;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<MovieDTO>>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<MovieDTO> GetMovie(HttpContext context, string id)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMovie + $"?id={id}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<MovieDTO>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<MovieDTO>> GetMoviesBasedOnGenre(HttpContext context, string id, int top)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMoviesBasedOnGenre + $"?genreID={id}" + $"&top={top}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<MovieDTO>>(response.Data);
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