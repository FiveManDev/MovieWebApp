using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieWebApp.Service;
using Newtonsoft.Json.Linq;
using System.Web;

namespace MovieWebApp.Pages.Pricing
{
    public class PaymentStatusModel : PageModel
    {
        private readonly ProfileServices profileServices;
        public PaymentStatusModel(ProfileServices profileServices)
        {
            this.profileServices = profileServices;
        }
        public  async Task<IActionResult> OnGet()
        {
            var response = HttpContext.Request.Query;
            var errorCode = response["errorCode"];
            if (errorCode == 0)
            {
                return Redirect("/ErrorPage?statusCode=404");
              
            }
            var UserID = response["orderInfo"];
            var upgradeStatus = await profileServices.PremiumUpgrade(HttpContext, Guid.Parse(UserID));
            if (!upgradeStatus)
            {
                return Redirect("/ErrorPage?statusCode=404");

            }
            return Redirect("/pricing");
        }
    }
}
