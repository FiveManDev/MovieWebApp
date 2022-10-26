namespace MovieAPI.Models.DTO
{
    public class GetMovies
    {
        public List<MovieDTO> movies { get; set; }
        public Pager pager { get; set; }
    }
}