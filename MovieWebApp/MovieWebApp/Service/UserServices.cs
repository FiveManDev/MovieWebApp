using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
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
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.CreateUser, createUserRequestDTO);

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

        }
        public async Task<string> ConfirmEmail(HttpContext context, string email)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.ConfirmEmail, email);

                // check status code: not yet
                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                    return responseApi.Data.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }



        }
        public async Task<string> ConfirmEmailForgotPassword(HttpContext context, string email)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.ConfirmEmailForgotPassword, email);

                // check status code: not yet
                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                    return responseApi.Data.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }



        }
        public async Task<TokenModel> Login(HttpContext context, LoginDTO loginDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.Login, loginDTO);

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
        public async Task<TokenModel> LoginWithService(HttpContext context, ServiceLoginModel serviceLoginModel)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.LoginWithService, serviceLoginModel);

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
        public async Task<ApiResponse> ChangePassword(HttpContext context, ChangePasswordDTO changePasswordDTO)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.ChangePassword, changePasswordDTO);

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
        public async Task<Boolean> ResetPassword(HttpContext context, Object resetPassword)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PostAsJsonAsync(MovieApiUrl.ResetPasword, resetPassword);

                // check status code: not yet
                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var responseApi = ExtensionMethods.ToModel<ApiResponse>(rawData);
                    if (responseApi.IsSuccess)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<string> GetClassOfUser(HttpContext context, string id)
        {
            getClient(context);
            if (id == null || id == "") return "";
            try
            {
                string url = MovieApiUrl.GetClassOfUser + $"?UserID={id}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                return response.Data.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
