using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Pricing
{
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        [BindProperty]
        public string UserClass { get; set; }

        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
            }
            else
            {
                UserClass = "";
            }
            return Page();
        }
    }
}
