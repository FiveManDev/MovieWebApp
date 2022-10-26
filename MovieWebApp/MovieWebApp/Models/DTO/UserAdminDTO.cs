namespace MovieAPI.Models.DTO
{
    public class UserAdminDTO
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public string createAt { get; set; }
        public bool status { get; set; }
        //Relationship
        public ProfileAdminDTO profile { get; set; }
        public AuthorizationDTO authorization { get; set; }
        public int numberOfReviews { get; set; }
    }
}
