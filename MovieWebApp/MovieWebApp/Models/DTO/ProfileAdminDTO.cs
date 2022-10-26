namespace MovieAPI.Models.DTO
{
    public class ProfileAdminDTO
    {
        public string profileID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
        //Relationship
        public string userID { get; set; }
        public Classification classification { get; set; }

    }
}
