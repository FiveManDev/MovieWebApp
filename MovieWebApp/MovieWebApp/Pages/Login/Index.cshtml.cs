using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;

namespace MovieWebApp.Pages.Login
{
    [BindProperties]
    public class IndexModel : PageModel
    {

        private readonly UserServices _userServices;
        public LoginDTO LoginDTO { get; set; }

        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/About/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var tokenModel = await _userServices.Login(LoginDTO);
            if (tokenModel == null)
            {
                return NotFound();
            }
            HttpContext.Response.Cookies.Append("accessToken", tokenModel.AccessToken, new CookieOptions { HttpOnly = true });
            HttpContext.Response.Cookies.Append("refreshToken", tokenModel.AccessToken, new CookieOptions { HttpOnly = true });
            return Redirect("/About/Index");
        }

    }
}
