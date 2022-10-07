using MovieAPI.Models.DTO;
using MovieWebApp.Models;
using MovieWebApp.Utility.Extension;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class UserServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public UserServices(IHttpClientFactory httpClientFactory)
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

        public async Task<bool> CreateUser(HttpContext context, CreateUserRequestDTO createUserRequestDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/User/CreateUser", createUserRequestDTO);

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

            //var request = new httprequestmessage(httpmethod.get, "url");

            //var client = _httpclientfactory.createclient();
            //var response = await client.sendasync(request);
            //if (response.issuccessstatuscode)
            //{
            //    model = await response.content.readfromjsonasync<model>();
            //}
            //else
            //{
            //    return response.reasonphrase;
            //}

        }

        public async Task<TokenModel> Login(HttpContext context, LoginDTO loginDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/User/Login", loginDTO);

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

        public async Task<UserDTO> GetUserInformation(HttpContext context, string id)
        {
            getClient(context);
            try
            {
                string url = $"/api/v1/User/GetUserInformation?id={id}";
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

        public async Task<ApiResponse> ChangePassword(HttpContext context, ChangePasswordDTO changePasswordDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1/User/ChangePassword", changePasswordDTO);

                // check status code: not yet
                var rawData = await response.Content.ReadAsStringAsync();
                var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                return responseApi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
