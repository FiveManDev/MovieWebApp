using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models;
using MovieAPI.Services.MomoPayment;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using MovieWebApp.Utility.Extension;
using System.Security.Claims;
namespace MovieWebApp.Pages.Pricing
{
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ClassificationServices _classificationServices;
        const double Dollar = 24000;
        [BindProperty]
        public string UserClass { get; set; }
        public List<ClassificationDTO> ClassificationDTOs { get; set; }
        public IndexModel(UserServices userServices, ClassificationServices classificationServices)
        {
            _userServices = userServices;
            _classificationServices = classificationServices;
        }

        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                UserClass = await _userServices.GetClassOfUser(HttpContext, userId);
            }
            else
            {
                UserClass = "";
            }
            ClassificationDTOs = await _classificationServices.GetAllClassification(HttpContext);
            ClassificationDTOs = ClassificationDTOs.OrderBy(cl => cl.ClassLevel).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(string amount)
        {
            try
            {
                var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
                var paymentModel = new PaymentModel
                {
                    UserID = Guid.Parse(userId),
                    Amount = Double.Parse(amount) * Dollar,
                    Message = "Upgrade to premium"
                };
                var paymentUrl = await MomoConnection.MomoResponse(paymentModel);
                if (paymentUrl == null)
                {
                    throw new Exception("Get payment url failed");
                }
                return Redirect(paymentUrl);
            }
            catch
            {
                return Page();
            }
        }
    }
}
