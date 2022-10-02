using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieWebApp.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            HttpContext.Response.Cookies.Delete("refreshToken");
            return Redirect("/Login/Index");
        }

    }
}
