using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;

namespace MovieWebApp.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        [BindProperty]
        public UserDTO UserDTO { get; set; }

        [BindProperty]
        public ChangePasswordDTO ChangePasswordDTO { get; set; }

        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _userServices.GetUserInformation(HttpContext, userId);
            return Page();
        }
        public async Task<IActionResult> OnPostChangePassword()
        {
            var result = await _userServices.ChangePassword(HttpContext, ChangePasswordDTO);
            if (result.IsSuccess)
            {
                TempData["success"] = result.Message;
            } else 
            {
                TempData["error"] = result.Message;
            }
            return Page();
        }
    }
}
