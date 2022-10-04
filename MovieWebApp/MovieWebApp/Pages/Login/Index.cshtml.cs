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
                return Redirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var tokenModel = await _userServices.Login(HttpContext, LoginDTO);
            if (tokenModel == null)
            {
                return Page();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = LoginDTO.RememberMe ? DateTime.Now.AddDays(30) : null
            };  

            HttpContext.Response.Cookies.Append("accessToken", tokenModel.AccessToken,
                cookieOptions
            );
            
            HttpContext.Response.Cookies.Append("refreshToken", tokenModel.AccessToken,
                cookieOptions
            );
            
            TempData["success"] = "Login success!";
            return Redirect("/");
        }

    }
}
