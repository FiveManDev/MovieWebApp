namespace MovieAPI.Models.DTO
{
    public class GetUsersDTO
    {
        public List<UserAdminDTO> users { get; set; }
        public Pager pager { get; set; }
    }
}