using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class FullCastData
    {
        public string IMDbId { get; set; }
        public string Title { set; get; }
        public string FullTitle { set; get; }
        public string Type { set; get; }
        public string Year { get; set; }
        public CastShort Directors { get; set; }
        public CastShort Writers { get; set; }
        public List<ActorShort> Actors { get; set; }
        public List<CastShort> Others { get; set; }
        public string ErrorMessage { get; set; }
    }
}
