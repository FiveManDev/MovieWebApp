using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using MovieWebApp.Service;
using MovieAPI.Models.DTO;
namespace MovieWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MovieServices _movieServices;
        private readonly UserServices _userServices;
        [BindProperty]
        public List<MovieDTO> MovieDTOs { get; set; }
        public List<MovieDTO> MoviePremiumDTOs { get; set; }
        public List<MovieDTO> MovieActionDTOs { get; set; }
        public List<MovieDTO> MovieRomanceDTOs { get; set; }
        public List<MovieDTO> MovieComedyDTOs { get; set; }
        public List<MovieDTO> MovieCartoonDTOs { get; set; }
        public string UserClass { get; set; }

        public IndexModel(UserServices userServices, MovieServices movieServices)
        {
            _userServices = userServices;
            _movieServices = movieServices;
        }

        public async Task<IActionResult> OnGet()
        {
            MovieDTOs = await _movieServices.GetTopLatestReleaseMovies(HttpContext, 6);
            MoviePremiumDTOs = await _movieServices.GetAllMovieIsPremium(HttpContext);

            MovieActionDTOs = await _movieServices.GetMoviesBasedOnGenre(HttpContext, "9ad897ac-89d2-4ef0-be64-a7d58dfd5f8d", 10);
            MovieRomanceDTOs = await _movieServices.GetMoviesBasedOnGenre(HttpContext, "22849f83-a5b4-49eb-93ed-e2d942254521", 10);
            MovieComedyDTOs = await _movieServices.GetMoviesBasedOnGenre(HttpContext, "cd3e7605-81ac-4a97-9ca3-58ed958fb4b6", 10);
            MovieCartoonDTOs = await _movieServices.GetMoviesBasedOnGenre(HttpContext, "350af705-bd31-40ab-8b0f-3a064d4e3df9", 10);


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