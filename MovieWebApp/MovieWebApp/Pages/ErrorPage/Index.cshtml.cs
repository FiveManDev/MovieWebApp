using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MovieWebApp.Pages.ErrorPage
{
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //[IgnoreAntiforgeryToken]
    [BindProperties]
    public class IndexModel : PageModel
    {
        public int _StatusCode { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int statusCode)
        {
            var validStatusCodes = Enum.GetValues(typeof(HttpStatusCode))
                                .Cast<HttpStatusCode>()
                                .Select(HttpStatusCode => (int)HttpStatusCode)
                                .ToList().Contains(statusCode);
            if (!validStatusCodes) statusCode = 404;

            _StatusCode = statusCode;
            Message = ReasonPhrases.GetReasonPhrase(_StatusCode);
            return Page();
        }
    }
}
