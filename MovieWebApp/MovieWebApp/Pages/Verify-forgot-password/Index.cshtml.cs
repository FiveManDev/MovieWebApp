using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieWebApp.Pages.Verify_forgot_password
{
  public class IndexModel : PageModel
  {
    public void OnGet()
    {
    }
    public IActionResult OnPost(string verifycode)
    {
      var code = TempData["code"];
      if (string.Equals(code, verifycode))
      {
        return RedirectToPage("/New-password/Index");
      }
      TempData.Keep("code");
      TempData.Keep("email");
      return Page();
    }
  }
}
