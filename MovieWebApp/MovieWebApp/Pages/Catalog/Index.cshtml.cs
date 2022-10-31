using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using MovieWebApp.Service;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;

namespace MovieWebApp.Pages.Catalog
{
    public class IndexModel : PageModel
    {

        private readonly MovieServices _movieServices;
        private readonly UserServices _userServices;
        private readonly GenreServices _genreServices;
        public List<MovieDTO> MoviePremiumDTOs { get; set; }
        public List<GenreDTO> GenreDTOs { get; set; }
        public string UserClass { get; set; }
        [BindProperty]
        public CatalogFilterDTO catalogFilterDTO { get; set; }
        [BindProperty]
        public string genreName { get; set; }
        public List<MovieDTO> MovieFilters { get; set; }


        public IndexModel(UserServices userServices, MovieServices movieServices, GenreServices genreServices)
        {
            _userServices = userServices;
            _movieServices = movieServices;
            _genreServices = genreServices;
        }

        public async Task<IActionResult> OnGet()
        {
            MoviePremiumDTOs = await _movieServices.GetAllMovieIsPremium(HttpContext);
            GenreDTOs = await _genreServices.GetAllGenre(HttpContext);
            if (User.Identity.IsAuthenticated)
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
            }
            else
            {
                UserClass = "";
            }

            MovieFilters = await _movieServices.GetMovieBaseOnFilter(HttpContext, catalogFilterDTO);

            return Page();
        }
        public async Task<IActionResult> OnPostFilter()
        {
            MoviePremiumDTOs = await _movieServices.GetAllMovieIsPremium(HttpContext);
            GenreDTOs = await _genreServices.GetAllGenre(HttpContext);

            if (User.Identity.IsAuthenticated)
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
            }
            else
            {
                UserClass = "";
            }

            foreach (var genre in GenreDTOs)
            {
                if (genre.GenreName == genreName)
                {
                    catalogFilterDTO.genreID = genre.GenreID.ToString();
                }
            }

            MovieFilters = await _movieServices.GetMovieBaseOnFilter(HttpContext, catalogFilterDTO);

            return Page();
        }
    }
}
