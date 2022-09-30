using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models;
using MovieWebApp.Service;

namespace MovieWebApp.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            HttpContext.Response.Cookies.Delete("accessToken");
            return Redirect("Login/Index");
        }

    }
}
