using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Support_chat
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly ChatServices _chatServices;
        public UserDTO UserDTO { get; set; }
        public List<UserChat> UserChats { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices, ChatServices chatServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _chatServices = chatServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserChats = await _chatServices.GetAllUserChat(HttpContext);
            return Page();
        }
    }
}
