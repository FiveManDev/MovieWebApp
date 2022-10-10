using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;
using Newtonsoft.Json;
using System.Reflection;

namespace MovieWebApp.Pages.Register
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        public CreateUserRequestDTO CreateUserRequestDTO { get; set; }


        public IndexModel(UserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> OnPost()
        {
            var response = await _userServices.ConfirmEmail(HttpContext, CreateUserRequestDTO.Email);
            if (!string.Equals(response, ""))
            {
                TempData["user"] = JsonConvert.SerializeObject(CreateUserRequestDTO);
                TempData["code"] = response;
                return RedirectToPage("/Verify-signup/Index");
            }
            return Page();
        }
    }
}
