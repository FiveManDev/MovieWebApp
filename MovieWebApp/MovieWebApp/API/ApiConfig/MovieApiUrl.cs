namespace MovieWebApp.API.ApiConfig
{
    public static class MovieApiUrl
    {
        #region User
        public const string Login = "api/v1/User/Login";
        public const string GetAllUser = "";
        public const string CreateUser = "/api/v1/User/CreateUser";
        public const string ConfirmEmail = "api/v1/User/ConfirmEmail";
        public const string ConfirmEmailForgotPassword = "api/v1/User/ConfirmEmailForgotPassword";
        public const string LoginWithService = "api/v1/User/LoginWithService";
        public const string ResetPasword = "api/v1/User/ResetPassword";
        public const string ChangePassword = "/api/v1/User/ChangePassword";
        public const string ChangeFirstLastName = "/api/v1/Profile/UpdateProfileForUser";
        public const string GetClassOfUser = "api/v1/User/GetClassOfUser";
        public const string GetInformation = "/api/v1/Profile/GetInformation";
        public const string PremiumUpgrade = "/api/v1/Profile/PremiumUpgrade";
        public const string GetLatestCreatedAccount = "/api/v1/User/GetLatestCreatedAccount";
        public const string GetUsers = "/api/v1/User/GetUsers";
        public const string ChangeStatusUser = "/api/v1/User/ChangeUserStatus";
        public const string DeleteUser = "/api/v1/User/DeleteUser";
        public const string UpdateProfileForAdmin = "/api/v1/Profile/UpdateProfileForAdmin";
        #endregion

        #region Movie
        public const string GetTopLatestPublicationMovies = "/api/v1/Movie/GetTopLatestPublicationMovies";
        public const string GetTopLatestReleaseMovies = "/api/v1/Movie/GetTopLatestReleaseMovies";
        public const string GetMovie = "/api/v1/Movie/GetMovieInformationById";
        public const string GetAllMovieIsPremium = "/api/v1/Movie/GetAllMovieIsPremium";
        public const string GetMoviesBasedOnGenre = "/api/v1/Movie/GetMoviesBasedOnGenre";
        public const string GetMovieBaseOnFilter = "/api/v1/Movie/GetMovieBaseOnFilter";
        public const string GetMoviesBasedOnSearchTextInCatalog = "/api/v1/Movie/GetMoviesBasedOnSearchTextInCatalog";
        public const string GetMoviesBasedOnSearchText = "api/v1/Movie/GetMoviesBasedOnSearchText";
        public const string GetMovieBaseOnTopRating = "/api/v1/Movie/GetMovieBaseOnTopRating";
        public const string GetMovies = "/api/v1/Movie/GetMovies";
        public const string GetTotalNumberOfMovies = "/api/v1/Movie/GetTotalNumberOfMovies";
        public const string UpdateMovieStatus = "/api/v1/Movie/UpdateMovieStatus";
        public const string DeleteMovie = "/api/v1/Movie/DeleteMovie";
        #endregion

        #region Genre
        public const string GetAllGenre = "/api/v1/Genre/GetAll";
        #endregion 

        #region Statistics
        public const string GetStatisticsForMonth = "/api/v1/Statistics/GetStatisticsForMonth";
        public const string GetTotalChat = "/api/v1/Statistics/getTotalChat";
        public const string GetTotalReview = "/api/v1/Statistics/getTotalReview";
        public const string GetTotalUser = "/api/v1/Statistics/getTotalUser";
        #endregion 

        #region Review
        public const string CreateReview = "/api/v1/Review/CreateReview";
        public const string GetAllReviewsOfMovie = "/api/v1/Review/GetAllReviewsOfMovie";
        public const string GetTopLastestReview = "/api/v1/Review/GetTopLastestReview";
        public const string GetReviews = "/api/v1/Review/GetReviews";
        public const string DeleteReview = "/api/v1/Review/DeleteReview";
        public const string GetAllReviewsOfUser = "/api/v1/Review/GetAllReviewsOfUser";
        #endregion
        #region Classification
        public const string GetAllClassification = "/api/v1/Classification/GetAll";
        #endregion

        #region Chat
        public const string GetAllUserChat = "/api/v1/Chat/GetAllUserChat";
        public const string GetChatForUser = "api/v1/Chat/GetChatMessage";
        #endregion

        #region test
        public const string test = "api/v1/Files/test";
        public const string testpost = "api/v1/Files/testpost";
        public const string testdecode = "api/v1/User/DecodeToken";
        #endregion
    }
}
