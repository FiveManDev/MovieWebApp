using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models;
using MovieWebApp.Service;

namespace MovieWebApp.Pages.Login
{
    public class LoginServiceModel : PageModel
    {
        private readonly UserServices _userServices;

        public LoginServiceModel(UserServices userServices)
        {
            _userServices = userServices;
        }
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
            try
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
                var tokenModel = await _userServices.LoginWithService(HttpContext, serviceLoginModel);
                if (tokenModel == null)
                {
                    return Page();
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = null
                };

                HttpContext.Response.Cookies.Append("accessToken", tokenModel.AccessToken,
                    cookieOptions
                );

                HttpContext.Response.Cookies.Append("refreshToken", tokenModel.AccessToken,
                    cookieOptions
                );

                TempData["success"] = "Login success!";
                return Redirect("/");
            }
            catch
            {
                return Redirect("/Login");
            }
        }
        public async Task<IActionResult> OnGetFacebookResponse()
        {
            try
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
                var tokenModel = await _userServices.LoginWithService(HttpContext, serviceLoginModel);
                if (tokenModel == null)
                {
                    return Page();
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = null
                };

                HttpContext.Response.Cookies.Append("accessToken", tokenModel.AccessToken,
                    cookieOptions
                );

                HttpContext.Response.Cookies.Append("refreshToken", tokenModel.AccessToken,
                    cookieOptions
                );

                TempData["success"] = "Login success!";
                return Redirect("/");
            }
            catch
            {
                return Redirect("/Login");
            }
        }
    }
}
