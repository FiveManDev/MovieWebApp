namespace MovieWebApp.Models.DTO
{
    public class CatalogFilterDTO
    {
        public string genreID { get; set; }
        public string quality { get; set; }
        public string ratingMin { get; set; }
        public string ratingMax { get; set; }
        public string releaseTimeMin { get; set; }
        public string releaseTimeMax { get; set; }
    }
}