namespace MovieAPI.Models.DTO
{
    public class UserChat
    {
        public Guid userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
        public bool isRead { get; set; }
        public Guid ticketID { get; set; }

    }
}