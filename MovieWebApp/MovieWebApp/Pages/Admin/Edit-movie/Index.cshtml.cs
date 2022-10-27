using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Edit_movie
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly MovieServices _movieServices;
        private readonly GenreServices _genreServices;
        public UserDTO UserDTO { get; set; }
        [BindProperty]
        public MovieDTO getMovie { get; set; }
        public List<GenreDTO> GenreDTOs { get; set; }
        [BindProperty]
        public string CoverImageURL { get; set; }
        [BindProperty]
        public string ThumbnailURL { get; set; }
        [BindProperty]
        public string MovieURL { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices, MovieServices movieServices, GenreServices genreServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _movieServices = movieServices;
            _genreServices = genreServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var id = RouteData.Values["id"].ToString();
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            getMovie = await _movieServices.GetMovie(HttpContext, id);
            GenreDTOs = await _genreServices.GetAllGenre(HttpContext);
            return Page();
        }
    }
}
