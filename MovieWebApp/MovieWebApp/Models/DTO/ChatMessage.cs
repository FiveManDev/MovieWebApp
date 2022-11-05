namespace MovieWebApp.Models.DTO
{
    public class ChatMessage
    {
        public bool isFromAdmin { get; set; }
        public string messageContent { get; set; }
        public DateTime messageTime { get; set; }
        public Guid senderId { get; set; }
        public Guid receiverId { get; set; }
        
    }
}
