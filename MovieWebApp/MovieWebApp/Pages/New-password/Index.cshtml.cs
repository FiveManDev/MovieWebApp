using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieWebApp.Service;

namespace MovieWebApp.Pages.New_password
{
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }
        public IActionResult OnGet()
        {
            var email = TempData["email"];
            if (email == null)
            {
                return Redirect("/");
            }
            TempData.Keep("email");
            return Page();
        }
        public async Task<IActionResult> OnPost(string newPassword,string confirmPasword)
        {
            if (string.Equals(newPassword, confirmPasword))
            {
                var email = TempData["email"];
                var resetPassword = new
                {
                    Email = email,
                    NewPassword = newPassword,
                    ConfirmPassword = confirmPasword
                };
                var response = await _userServices.ResetPassword(HttpContext, resetPassword);
                if (response)
                {
                    return Redirect("/login");
                }
            }

            TempData.Keep("email");
            return Page();
        }
    }
}
