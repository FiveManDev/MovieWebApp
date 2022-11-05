using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Service;
using MovieWebApp.Utility.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace MovieWebApp.Pages.Verify_signup
{
  public class IndexModel : PageModel
  {
    private readonly UserServices _userServices;
    public IndexModel(UserServices userServices)
    {
      _userServices = userServices;
    }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost(string verifyCode)
    {
        var userData = TempData["user"] as string;
        var code = TempData["code"] as string;
        var user = JsonConvert.DeserializeObject<CreateUserRequestDTO>(userData);
        if (string.Equals(verifyCode, code))
        {
            var response = await _userServices.CreateUser(HttpContext, user);
            if (response)
            {
                return RedirectToPage("/Login/Index");
            }
            TempData["error"] = "Account is exist!";
        }
        else
        {
            TempData["error"] = "Code is wrong! God Damn!";
        }
        TempData.Keep("code");
        TempData.Keep("user");
        return Page();
    }
  }
}
