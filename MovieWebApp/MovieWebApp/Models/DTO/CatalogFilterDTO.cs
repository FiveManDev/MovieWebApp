namespace MovieWebApp.Models.DTO
{
    public class CatalogFilterDTO
    {
        public Guid genreID { get; set; }
        public string quality { get; set; }
        public int ratingMin { get; set; }
        public int ratingMax { get; set; }
        public int releaseTimeMin { get; set; }
        public int releaseTimeMax { get; set; }
    }
}