namespace MovieWebApp.Models.DTO
{
    public class CreateReviewDTO
    {
        public string title { get; set; }
        public string reviewContent { get; set; }
        public int rating { get; set; }
        public DateTime reviewTime { get; set; }
        public string userID { get; set; }
        public string movieID { get; set; }
    }
}