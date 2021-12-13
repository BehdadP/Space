namespace IMDB_DAL.DTO
{
    public class ReviewDetail
    {
        public string Username { get; set; }
        public string UserUrl { get; set; }
        public string ReviewLink { get; set; }
        public bool WarningSpoilers { get; set; }
        public string Date { get; set; }
        public string Rate { get; set; }
        public string Helpful { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
