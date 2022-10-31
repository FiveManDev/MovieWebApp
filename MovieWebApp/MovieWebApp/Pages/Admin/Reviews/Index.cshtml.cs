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
        private readonly StatisticsServicess _statisticsServicess;

        public UserDTO UserDTO { get; set; }
        public GetReviews GetReviews { get; set; }
        public string deleteReview { get; set; }
        public int GetTotalReview { get; set; }
        [BindProperty]
        public string ReviewSort { get; set; }
        [BindProperty]
        public string ReviewSearch { get; set; }
        public IndexModel(UserServices userServices, ProfileServices profileServices, ReviewServices reviewServices, StatisticsServicess statisticsServicess)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _reviewServices = reviewServices;
            _statisticsServicess = statisticsServicess;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            ReviewSearch = "";
            ReviewSort = "";
            GetReviews = await _reviewServices.GetReviews(HttpContext, "", "", "");
            GetTotalReview = await _statisticsServicess.GetTotalReview(HttpContext);

            deleteReview = Request.Query["deleteReview"].ToString();
            if (deleteReview != "")
            {
                var result = await _reviewServices.DeleteReview(HttpContext, deleteReview);
                if (result)
                {
                    TempData["success"] = "Delete review movie successfully!";
                }
                else
                {
                    TempData["error"] = "Delete review movie fail!";
                }
                return RedirectToPage("/Admin/Reviews/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostSearchSort()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);

            GetReviews = await _reviewServices.GetReviews(HttpContext, ReviewSearch, ReviewSort, "");
            GetTotalReview = await _statisticsServicess.GetTotalReview(HttpContext);

            deleteReview = Request.Query["deleteReview"].ToString();
            if (deleteReview != "")
            {
                var result = await _reviewServices.DeleteReview(HttpContext, deleteReview);
                if (result)
                {
                    TempData["success"] = "Delete review movie successfully!";
                }
                else
                {
                    TempData["error"] = "Delete review movie fail!";
                }
                return RedirectToPage("/Admin/Reviews/Index");
            }

            return Page();
        }
    }
}
