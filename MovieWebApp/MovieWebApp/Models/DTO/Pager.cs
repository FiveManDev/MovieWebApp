namespace MovieAPI.Models.DTO
{
    public class Pager
    {
        public int pageIndex { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public bool hasPrevious { get; set; }
        public bool hasNext { get; set; }
    }
}