using MovieAPI.Models.DTO;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;
using MovieWebApp.API.ApiConfig;
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
                string url = MovieApiUrl.GetInformation + $"?UserID={id}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                return ExtensionMethods.ToModel<UserDTO>(response.Data);
            }
            catch
            {
                return null;
            }

        }
        public async Task<ApiResponse> ChangeFirstLastName(HttpContext context, ChangeFirstLastNameDTO ChangeFirstLastNameDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PutAsJsonAsync(MovieApiUrl.ChangeFirstLastName, ChangeFirstLastNameDTO);

                // check status code: not yet
                var rawData = await response.Content.ReadAsStringAsync();
                var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                return responseApi;
            }
            catch
            {
                return null;
            }
        }


    }
}
