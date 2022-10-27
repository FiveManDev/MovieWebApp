namespace MovieAPI.Models.DTO
{
    public class UpdateMovieStatusDTO
    {
        public string movieID { get; set; }
        public bool IsVisible { get; set; }
    }
}