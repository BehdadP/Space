using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class SearchData
    {
        public string SearchType { get; set; }
        public string Expression { get; set; }

        public List<SearchResult> Results { get; set; }

        public string ErrorMessage { get; set; }
    }
}
