using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Models.DTO;
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
        public async Task<List<MovieDTO>> GetMovieBaseOnFilter(HttpContext context, CatalogFilterDTO CatalogFilterDTO)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMovieBaseOnFilter;
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
        public async Task<List<MovieDTO>> GetMoviesBasedOnSearchTextInCatalog(HttpContext context, string searchText)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMoviesBasedOnSearchTextInCatalog + $"?searchText={searchText}";
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
        public async Task<List<MovieDTO>> GetMovieBaseOnTopRating(HttpContext context, int count)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMovieBaseOnTopRating + $"?top={count}";
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
        public async Task<GetMovies> GetMovies(HttpContext context, string searchText, string sortBy, string sortType)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetMovies + $"?q={searchText}" + $"&sortBy={sortBy}" + $"&sortType={sortType}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<GetMovies>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<int> GetTotalNumberOfMovies(HttpContext context)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetTotalNumberOfMovies;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<int>(response.Data);
                }
                return 0;
            }
            catch
            {
                return 0;
            }

        }

    }
}