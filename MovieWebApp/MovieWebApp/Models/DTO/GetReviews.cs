namespace MovieAPI.Models.DTO
{
    public class GetReviews
    {
        public List<ReviewDTO> reviews { get; set; }
        public Pager pager { get; set; }
    }
}