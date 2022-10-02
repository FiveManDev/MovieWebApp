using MovieAPI.Models.DTO;
using MovieWebApp.Models;
using MovieWebApp.Utility.Extension;

namespace MovieWebApp.Service
{
    public class UserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateUser(CreateUserRequestDTO createUserRequestDTO)
        {
            var client = _httpClientFactory.CreateClient("api");
            try
            {
                var response = await client.PostAsJsonAsync("/api/v1/User/CreateUser", createUserRequestDTO);

                // check status code: not yet
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            //var request = new HttpRequestMessage(HttpMethod.Get, "url");

            //var client = _httpClientFactory.CreateClient();
            //var response = await client.SendAsync(request);
            //if (response.IsSuccessStatusCode)
            //{
            //    model = await response.Content.ReadFromJsonAsync<model>();
            //}
            //else
            //{
            //    return response.ReasonPhrase;
            //}

        }

        public async Task<TokenModel> Login(LoginDTO loginDTO)
        {
            var client = _httpClientFactory.CreateClient("api");
            try
            {
                var response = await client.PostAsJsonAsync("/api/v1/User/Login", loginDTO);

                // check status code: not yet
                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                    if (responseApi.IsSuccess)
                    {
                        return ExtensionMethods.ToModel<TokenModel>(responseApi.Data);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
