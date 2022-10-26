using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Reviews
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly ReviewServices _reviewServices;

        public UserDTO UserDTO { get; set; }
        public GetReviews GetReviews { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices, ReviewServices reviewServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _reviewServices = reviewServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            GetReviews = await _reviewServices.GetReviews(HttpContext, "", "", "");
            return Page();
        }
    }
}
