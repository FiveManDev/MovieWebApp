using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieAPI.Models.DTO;
using MovieWebApp.Models.DTO;
using MovieWebApp.Service;
using System.Security.Claims;
namespace MovieWebApp.Pages.Admin.Edit_user
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserServices _userServices;
        private readonly ProfileServices _profileServices;
        private readonly ReviewServices _reviewServices;
        public UserDTO UserDTO { get; set; }
        public UserDTO UserProfile { get; set; }
        [BindProperty]
        public string UserClass { get; set; }
        [BindProperty]
        public AdminEditUserProfileDTO AdminEditUserProfileDTO { get; set; }
        [BindProperty]
        public List<ReviewDTO> GetReviews { get; set; }
        public string changeStatusUserAdmin { get; set; }
        public string deleteUserAdmin { get; set; }
        [BindProperty]
        public ChangePasswordDTO ChangePasswordDTO { get; set; }
        [BindProperty]
        public UpdateProfileForAdminDTO UpdateProfileForAdminDTO { get; set; }

        public IndexModel(UserServices userServices, ReviewServices reviewServices, ProfileServices profileServices)
        {
            _userServices = userServices;
            _profileServices = profileServices;
            _reviewServices = reviewServices;
        }
        public async Task<IActionResult> OnGet()
        {
            var id = RouteData.Values["id"].ToString();
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserProfile = await _profileServices.GetInformation(HttpContext, id);
            UserClass = await _userServices.GetClassOfUser(HttpContext, id);

            AdminEditUserProfileDTO = new AdminEditUserProfileDTO();
            AdminEditUserProfileDTO.FirstName = UserProfile.Profile.FirstName;
            AdminEditUserProfileDTO.LastName = UserProfile.Profile.LastName;
            AdminEditUserProfileDTO.Subscription = UserClass;
            AdminEditUserProfileDTO.Right = UserProfile.Authorization.AuthorizationName;

            GetReviews = await _reviewServices.GetAllReviewsOfUser(HttpContext, id);

            changeStatusUserAdmin = Request.Query["changeStatusUserAdmin"].ToString();
            deleteUserAdmin = Request.Query["deleteUserAdmin"].ToString();

            if (changeStatusUserAdmin != "")
            {
                ChangeStatusUserDTO update = new ChangeStatusUserDTO();
                update.UserID = changeStatusUserAdmin;
                update.IsBanned = !UserProfile.status;
                var result = await _userServices.ChangeStatusUser(HttpContext, update);
                if (result)
                {
                    TempData["success"] = "Change status user successfully!";
                }
                else
                {
                    TempData["error"] = "Change status user fail!";
                }
                return RedirectToPage("/Admin/Edit-User/Index", new { id = id });
            }
            if (deleteUserAdmin != "")
            {
                var result = await _userServices.DeleteUser(HttpContext, deleteUserAdmin);
                if (result)
                {
                    TempData["success"] = "Delete user successfully!";
                    return RedirectToPage("/Admin/Users/Index");
                }
                else
                {
                    TempData["error"] = "Delete user fail!";
                    return RedirectToPage("/Admin/Edit-User/Index", new { id = id });
                }
            }

            return Page();
        }
        public async Task<IActionResult> OnPostChangePassword()
        {
            var id = RouteData.Values["id"].ToString();
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserProfile = await _profileServices.GetInformation(HttpContext, id);
            UserClass = await _userServices.GetClassOfUser(HttpContext, id);

            GetReviews = await _reviewServices.GetAllReviewsOfUser(HttpContext, id);
            var result = await _userServices.ChangePassword(HttpContext, ChangePasswordDTO);
            if (result.IsSuccess)
            {
                TempData["success"] = "Change password success!";
                if (userId == id)
                {
                    return RedirectToPage("/Logout/Index");
                }
            }
            else
            {
                TempData["error"] = "Change password fail!";
            }
            return RedirectToPage("/Admin/Edit-User/Index", new { id = id });
        }
        public async Task<IActionResult> OnPostUpdateProfileForAdmin()
        {
            var id = RouteData.Values["id"].ToString();
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserID").Value;
            UserDTO = await _profileServices.GetInformation(HttpContext, userId);
            UserProfile = await _profileServices.GetInformation(HttpContext, id);
            UserClass = await _userServices.GetClassOfUser(HttpContext, id);
            GetReviews = await _reviewServices.GetAllReviewsOfUser(HttpContext, id);

            UpdateProfileForAdminDTO = new UpdateProfileForAdminDTO();
            UpdateProfileForAdminDTO.UserID = id;
            UpdateProfileForAdminDTO.FirstName = AdminEditUserProfileDTO.FirstName;
            UpdateProfileForAdminDTO.LastName = AdminEditUserProfileDTO.LastName;
            if (AdminEditUserProfileDTO.Subscription == "Basic")
            {
                UpdateProfileForAdminDTO.ClassID = "84251a89-a458-46f8-ba28-83df593ed2a9";
            }
            else
            {
                UpdateProfileForAdminDTO.ClassID = "e360722f-7405-4278-a4b2-17497036cef0";
            }

            if (AdminEditUserProfileDTO.Right == "User")
            {
                UpdateProfileForAdminDTO.AuthorizationID = "3db2b025-a20c-460d-8810-36aa273229be";
            }
            else
            {
                UpdateProfileForAdminDTO.AuthorizationID = "147aad7e-eefe-4b49-b637-7aa5dfaa38ab";
            }


            var result = await _profileServices.UpdateProfileForAdmin(HttpContext, UpdateProfileForAdminDTO);
            if (result)
            {
                TempData["success"] = "Change name success!";
            }
            else
            {
                TempData["error"] = "Change name fail!";
            }
            return RedirectToPage("/Admin/Edit-User/Index", new { id = id });
        }

    }
}
