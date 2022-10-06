using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieWebApp.Models;
namespace MovieWebApp.Pages.Login
{
    public class LoginServiceModel : PageModel
    {
        public IActionResult OnGetGoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.PageLink("/Login/LoginService", "GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        public IActionResult OnGetFacebookLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.PageLink("/Login/LoginService", "FacebookResponse") };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> OnGetGoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal!.Identities.FirstOrDefault()!.Claims.Select(claim => new
            {
                claim.Value
            });
            var response = claims.ToList();
            ServiceLoginModel serviceLoginModel = new ServiceLoginModel
            {
                Id = response[0].Value,
                FirstName = response[2].Value,
                LastName = response[3].Value,
                Mail = response[4].Value
            };

            return Content("Google");
        }
        public async Task<IActionResult> OnGetFacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal!.Identities.FirstOrDefault()!.Claims.Select(claim => new
            {
                claim.Value
            });
            var response = claims.ToList();
            ServiceLoginModel serviceLoginModel = new ServiceLoginModel
            {
                Id = response[0].Value,
                Mail = response[1].Value,
                FirstName = response[3].Value,
                LastName = response[4].Value
            };
            return Content("Facebook");
        }
    }
}
