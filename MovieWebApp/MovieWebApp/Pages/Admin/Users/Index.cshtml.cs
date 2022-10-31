using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly StatisticsServicess _statisticsServicess;
        public GetUsersDTO GetUsersDTOs { get; set; }
        public UserDTO UserDTO { get; set; }
        public UserDTO UserChangeDTO { get; set; }
        public string changeStatusUser { get; set; }
        public string deleteUser { get; set; }
        public int GetTotalUser { get; set; }
        [BindProperty]
        public string UserSort { get; set; }
        [BindProperty]
        public string UserSearch { get; set; }

        public IndexModel(UserServices userServices, ProfileServices profileServices, StatisticsServicess statisticsServicess)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _statisticsServicess = statisticsServicess;
        }
        public async Task<IActionResult> OnGet()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);

            UserSort = "";
            UserSearch = "";

            GetUsersDTOs = await _userServices.GetUsers(HttpContext, "", "", "");

            GetTotalUser = await _statisticsServicess.GetTotalUser(HttpContext);

            changeStatusUser = Request.Query["changeStatusUser"].ToString();
            deleteUser = Request.Query["deleteUser"].ToString();

            if (changeStatusUser != "")
            {
                UserChangeDTO = await _profileServices.GetInformation(HttpContext, changeStatusUser);
                ChangeStatusUserDTO update = new ChangeStatusUserDTO();
                update.UserID = changeStatusUser;
                update.IsBanned = !UserChangeDTO.status;
                var result = await _userServices.ChangeStatusUser(HttpContext, update);
                if (result)
                {
                    TempData["success"] = "Change status user successfully!";
                }
                else
                {
                    TempData["error"] = "Change status user fail!";
                }
                return RedirectToPage("/Admin/Users/Index");
            }
            if (deleteUser != "")
            {
                var result = await _userServices.DeleteUser(HttpContext, deleteUser);
                if (result)
                {
                    TempData["success"] = "Delete user successfully!";
                }
                else
                {
                    TempData["error"] = "Delete user fail!";
                }
                return RedirectToPage("/Admin/Users/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostSearchSort()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);

            GetUsersDTOs = await _userServices.GetUsers(HttpContext, UserSearch, UserSort, "");

            GetTotalUser = await _statisticsServicess.GetTotalUser(HttpContext);

            changeStatusUser = Request.Query["changeStatusUser"].ToString();
            deleteUser = Request.Query["deleteUser"].ToString();

            if (changeStatusUser != "")
            {
                UserChangeDTO = await _profileServices.GetInformation(HttpContext, changeStatusUser);
                ChangeStatusUserDTO update = new ChangeStatusUserDTO();
                update.UserID = changeStatusUser;
                update.IsBanned = !UserChangeDTO.status;
                var result = await _userServices.ChangeStatusUser(HttpContext, update);
                if (result)
                {
                    TempData["success"] = "Change status user successfully!";
                }
                else
                {
                    TempData["error"] = "Change status user fail!";
                }
                return RedirectToPage("/Admin/Users/Index");
            }
            if (deleteUser != "")
            {
                var result = await _userServices.DeleteUser(HttpContext, deleteUser);
                if (result)
                {
                    TempData["success"] = "Delete user successfully!";
                }
                else
                {
                    TempData["error"] = "Delete user fail!";
                }
                return RedirectToPage("/Admin/Users/Index");
            }

            return Page();
        }
    }
}
