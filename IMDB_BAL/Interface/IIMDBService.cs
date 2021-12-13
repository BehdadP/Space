using IMDB_DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_BAL.Interface
{
    public interface IIMDBService
    {
        public Task<List<SearchResult>> SearchMovieAsync(string movieName);
        public Task<TitleData> GetMovieInfo(string id);
        public Task<PosterData> GetPosters(string id);

        public Task<WikipediaData> GetWikipediaInfo(string id);


    }
}
