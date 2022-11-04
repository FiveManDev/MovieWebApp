using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
using MovieWebApp.Models.DTO;
namespace MovieWebApp.Pages.Detail
{
    public class IndexModel : PageModel
    {
        private readonly MovieServices _movieServices;
        private readonly UserServices _userServices;
        private readonly ReviewServices _reviewServices;
        [BindProperty]
        public MovieDTO MovieDTO { get; set; }
        [BindProperty]
        public List<ReviewDTO> ReviewDTOs { get; set; }
        [BindProperty]
        public CreateReviewDTO CreateReviewDTO { get; set; }
        [BindProperty]
        public List<MovieDTO> MovieDTOs { get; set; }
        public string UserClass { get; set; }
        [BindProperty]
        public string rating { get; set; }
        public IndexModel(UserServices userServices, MovieServices movieServices, ReviewServices reviewServices)
        {
            _userServices = userServices;
            _movieServices = movieServices;
            _reviewServices = reviewServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Request.Cookies["accessToken"];
            var id = RouteData.Values["id"].ToString();
            MovieDTOs = await _movieServices.GetTopLatestReleaseMovies(HttpContext, 6);
            MovieDTO = await _movieServices.GetMovie(HttpContext, id);
            ReviewDTOs = await _reviewServices.GetAllReviewsOfMovie(HttpContext, id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
                TempData["UserID"] = userId;
            }
            else
            {
                UserClass = "";
            }
            TempData["Token"] = token;
            return Page();
        }
        
    }
}
