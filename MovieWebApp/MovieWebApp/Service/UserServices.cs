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
            catch
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
            catch
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
            catch
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
            catch
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
            catch
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
            catch
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
            catch
            {
                return "";
            }

        }
        public async Task<List<UserDTO>> GetLatestCreatedAccount(HttpContext context, int count)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetLatestCreatedAccount + $"?top={count}";
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<UserDTO>>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<GetUsersDTO> GetUsers(HttpContext context, string searchText, string sortBy, string sortType)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetUsers + "?pageSize=50&sortType=desc";
                if (searchText != "")
                {
                    url = url + $"&q={searchText}";
                }
                if (sortBy != "")
                {
                    url = url + $"&sortBy={sortBy}";
                }

                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);

                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<GetUsersDTO>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> ChangeStatusUser(HttpContext context, ChangeStatusUserDTO update)
        {
            getClient(context);
            try
            {
                var response = await _httpClient.PutAsJsonAsync(MovieApiUrl.ChangeStatusUser, update);

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
        public async Task<bool> DeleteUser(HttpContext context, string Id)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.DeleteUser + $"?UserId={Id}";
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
