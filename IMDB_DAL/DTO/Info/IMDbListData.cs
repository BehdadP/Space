using System.Collections.Generic;

namespace IMDB_DAL.DTO
{
    public class IMDbListData
    {
        public string Title { get; set; }
        public string By { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string Description { get; set; }

        public List<IMDbListDataDetail> Items { get; set; }

        public string ErrorMessage { get; set; }
    }
}
