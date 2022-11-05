using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;

namespace MovieWebApp.Pages.Contact
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ChatServices _chatServices;
        public IndexModel(ChatServices chatServices)
        {
            _chatServices = chatServices;
        }
        public List<ChatMessage> chatMessage { get; set; }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                chatMessage = await _chatServices.GetChatMessage(HttpContext, Guid.Parse(userId));
                var token = HttpContext.Request.Cookies["accessToken"];
                TempData["Token"] = token;
                TempData["UserID"] = userId;
                return Page();
            }
            catch
            {
                return Redirect("/ErrorPage?statusCode=404");
            }
        }
    }
}
