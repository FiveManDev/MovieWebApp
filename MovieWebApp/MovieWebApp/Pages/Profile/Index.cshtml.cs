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
        private readonly ProfileServices _profileServices;
        [BindProperty]
        public UserDTO UserDTO { get; set; }

        [BindProperty]
        public ChangePasswordDTO ChangePasswordDTO { get; set; }

        [BindProperty]
        public ChangeFirstLastNameDTO ChangeFirstLastNameDTO { get; set; }

        [BindProperty]
        public string UserClass { get; set; }

        public IndexModel(UserServices userServices, ProfileServices profileServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
            return Page();
        }
        public async Task<IActionResult> OnPostChangePassword()
        {
            var result = await _userServices.ChangePassword(HttpContext, ChangePasswordDTO);
            if (result.IsSuccess)
            {
                TempData["success"] = "Change password success!";
            }
            else
            {
                TempData["error"] = "Change password fail!";
            }
            return RedirectToPage("/Profile/Index");
        }
        public async Task<IActionResult> OnPostChangeFirstLastName()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            ChangeFirstLastNameDTO.UserID = Guid.Parse(userId);
            var result = await _profileServices.ChangeFirstLastName(HttpContext, ChangeFirstLastNameDTO);
            System.Console.WriteLine("Test out: " + ChangeFirstLastNameDTO.UserID);
            System.Console.WriteLine("Test out: " + ChangeFirstLastNameDTO.FirstName);
            System.Console.WriteLine("Test out: " + ChangeFirstLastNameDTO.LastName);
            if (result.IsSuccess)
            {
                TempData["success"] = "Change name success!";
            }
            else
            {
                TempData["error"] = "Change name fail!";
            }
            return RedirectToPage("/Profile/Index");
        }

    }
}
