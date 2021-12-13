using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class CastShort
    {
        public string Job { get; set; }
        public List<CastShortItem> Items { get; set; }
    }
}
