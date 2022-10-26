using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Catalog
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly MovieServices _movieService;

        public UserDTO UserDTO { get; set; }
        public GetMovies GetMovies { get; set; }
        public int TotalNumberOfMovies { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices, MovieServices movieService)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _movieService = movieService;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            GetMovies = await _movieService.GetMovies(HttpContext, "", "", "");
            TotalNumberOfMovies = await _movieService.GetTotalNumberOfMovies(HttpContext);
            return Page();
        }
    }
}
