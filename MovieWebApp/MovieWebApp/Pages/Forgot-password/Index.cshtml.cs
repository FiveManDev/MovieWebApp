using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;
using Newtonsoft.Json;

namespace MovieWebApp.Pages.Forgot_password
{
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string email)
        {
            var response = await _userServices.ConfirmEmailForgotPassword(HttpContext, email);
            if (!string.Equals(response, ""))
            {
                TempData["email"] = email;
                TempData["code"] = response;
                return RedirectToPage("/Verify-forgot-password/Index");
            }
            return Page();
        }
    }
}
