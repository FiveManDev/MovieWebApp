namespace MovieWebApp.Utility.Extension
{
    public static class AppSettings
    {
        public static string Host { get; set; }
        public static string Version { get; set; }
        public static string TimeOut { get; set; }

        public static string SecretKey { get; set; }
        #region Momo
        public static string PartnerCode { get; set; }
        public static string MomoAccessKey { get; set; }
        public static string MomoSerectkey { get; set; }
        public static string Endpoint { get; set; }
        public static string ReturnUrl { get; set; }
        public static string NotifyUrl { get; set; }
        #endregion
    }
}
