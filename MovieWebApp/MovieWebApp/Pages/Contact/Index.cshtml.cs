using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MovieWebApp.Pages.Contact
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            try
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
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
