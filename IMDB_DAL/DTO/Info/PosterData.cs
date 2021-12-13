using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class PosterData
    {
        public string IMDbId { get; set; }
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public string Type { set; get; }
        public string Year { set; get; }

        public List<PosterDataItem> Posters { get; set; }
        public List<PosterDataItem> Backdors { get; set; }

        public string ErrorMessage { get; set; }
    }
}
