using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;

namespace MovieWebApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly ReviewServices _reviewServices;
        private readonly MovieServices _movieServices;
        private readonly StatisticsServicess _statisticsServicess;

        public UserDTO UserDTO { get; set; }
        public string UserClass { get; set; }


        public List<UserDTO> LastestUsers { get; set; }
        public List<MovieDTO> TopMovies { get; set; }
        public List<MovieDTO> LatestMovies { get; set; }
        public List<ReviewDTO> LatestReviews { get; set; }
        public StatisticsDTO StatisticsDTO { get; set; }

        public IndexModel(UserServices userServices, ProfileServices profileServices, ReviewServices reviewServices, MovieServices movieServices, StatisticsServicess statisticsServicess)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _reviewServices = reviewServices;
            _movieServices = movieServices;
            _statisticsServicess = statisticsServicess;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserClass = await _userServices.GetClassOfUser(HttpContext, userId);

            LastestUsers = await _userServices.GetLatestCreatedAccount(HttpContext, 5);
            TopMovies = await _movieServices.GetMovieBaseOnTopRating(HttpContext, 5);
            LatestMovies = await _movieServices.GetTopLatestReleaseMovies(HttpContext, 5);
            LatestReviews = await _reviewServices.GetTopLastestReview(HttpContext, 5);
            StatisticsDTO = await _statisticsServicess.GetStatisticsForMonth(HttpContext);
            return Page();
        }
    }
}
