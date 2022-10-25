using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using MovieWebApp.Service;
using MovieAPI.Models.DTO;

namespace MovieWebApp.Pages.Search
{
    public class IndexModel : PageModel
    {

        private readonly MovieServices _movieServices;
        private readonly UserServices _userServices;
        public List<MovieDTO> MovieSearched { get; set; }
        public string UserClass { get; set; }
        public string searchText { get; set; }

        public IndexModel(UserServices userServices, MovieServices movieServices)
        {
            _userServices = userServices;
            _movieServices = movieServices;
        }

        public async Task<IActionResult> OnGet()
        {

            searchText = Request.Query["searchText"].ToString();
            MovieSearched = await _movieServices.GetMoviesBasedOnSearchTextInCatalog(HttpContext, searchText);

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