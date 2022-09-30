using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;

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
            var response = await _userServices.CreateUser(CreateUserRequestDTO);
            if (response)
            {
                return RedirectToPage("/Login/Index");
            }
            return Page();
        }
    }
}
