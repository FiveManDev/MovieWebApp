using MovieAPI.Models.DTO;
using MovieWebApp.API.ApiConfig;
using MovieWebApp.Models.DTO;
using MovieWebApp.Utility.Extension;
using System.Net.Http.Headers;

namespace MovieWebApp.Service
{
    public class ChatServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public ChatServices(IHttpClientFactory httpClientFactory)
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
        public async Task<List<UserChat>> GetAllUserChat(HttpContext context)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetAllUserChat;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<UserChat>>(response.Data);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<ChatMessage>> GetChatMessage(HttpContext context,Guid groupID)
        {
            getClient(context);
            try
            {
                string url = MovieApiUrl.GetChatForUser+ "?GroupID="+groupID;
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                if (response.IsSuccess)
                {
                    return ExtensionMethods.ToModel<List<ChatMessage>>(response.Data);
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