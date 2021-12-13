using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class ReviewData
    {
        public string IMDbId { get; set; }
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }

        public List<ReviewDetail> Items { get; set; }

        public string ErrorMessage { get; set; }
    }
}
