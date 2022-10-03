using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MovieWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            //if (identity != null)
            //{
            //    IEnumerable<Claim> claims = identity.Claims;
            //    // or
            //    //identity.FindFirst("ClaimName").Value;
            //   
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //var value = identity.FindFirst(ClaimTypes.Role).Value;
            //var identity2 = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Role).Value;
        }
    }
}