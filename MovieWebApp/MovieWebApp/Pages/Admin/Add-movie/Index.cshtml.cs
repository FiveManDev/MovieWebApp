using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Add_movie
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        public UserDTO UserDTO { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            var token = HttpContext.Request.Cookies["accessToken"];
            TempData["UserID"] = userId;
            TempData["Token"] = token;
            return Page();
        }
    }
}
