namespace IMDB_DAL.DTO
{
    public class IMDbListDataDetail
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string Title { set; get; }
        public string FullTitle { set; get; }
        public string Year { set; get; }
        public string Image { get; set; }
        public string IMDbRating { get; set; }
        public string IMDbRatingCount { get; set; }
        public string Description { get; set; }
    }
}
