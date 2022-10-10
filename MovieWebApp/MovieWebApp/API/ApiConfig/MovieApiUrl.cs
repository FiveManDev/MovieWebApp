namespace MovieWebApp.API.ApiConfig
{
    public static class MovieApiUrl
    {
        #region User
        public const string Login = "api/v1/User/Login";
        public const string GetAllUser = "";
        public const string ConfirmEmail = "api/v1/User/ConfirmEmail";
        public const string ConfirmEmailForgotPassword = "api/v1/User/ConfirmEmailForgotPassword";
        public const string LoginWithService = "api/v1/User/LoginWithService";
        public const string ResetPasword = "api/v1/User/ResetPassword";
        #endregion

        #region test
        public const string test = "api/v1/Files/test";
        public const string testpost = "api/v1/Files/testpost";
        public const string testdecode = "api/v1/User/DecodeToken";
        #endregion
    }
}
