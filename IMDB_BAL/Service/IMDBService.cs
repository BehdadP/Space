using IMDB_BAL.Interface;
using IMDB_BAL.Settings;
using IMDB_DAL.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_BAL.Service
{
    public class IMDBService : IIMDBService
    {
        private readonly IMDbSettings _imdbSettings;
        public IMDBService(IOptions<IMDbSettings> imdbSettings)
        {
            _imdbSettings = imdbSettings.Value;
        }
        public async Task<List<SearchResult>> SearchMovieAsync(string movieName)
        {
            using (var client = new HttpClient())
            {
                var httpResponseMessage = await client.GetAsync(string.Format("{0}/{1}/API/Search/{2}/{3}", _imdbSettings.IMDbBaseUrl, _imdbSettings.Lang, _imdbSettings.ApiKey, movieName));
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var movieResult = JsonConvert.DeserializeObject<SearchData>(response);
                return movieResult.Results;
            }
        }
        public async Task<TitleData> GetMovieInfo(string imdbId)
        {
            using (var client = new HttpClient())
            {
                var httpResponseMessage = await client.GetAsync(string.Format("{0}/{1}/API/Title/{2}/{3}", _imdbSettings.IMDbBaseUrl, _imdbSettings.Lang, _imdbSettings.ApiKey, imdbId));
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var infoResult = JsonConvert.DeserializeObject<TitleData>(response);
                return infoResult;
            }
        }
        public async Task<PosterData> GetPosters(string imdbId)
        {
            using (var client = new HttpClient())
            {
                var httpResponseMessage = await client.GetAsync(string.Format("{0}/{1}/API/Posters/{2}/{3}", _imdbSettings.IMDbBaseUrl, _imdbSettings.Lang, _imdbSettings.ApiKey, imdbId));
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var postersResult = JsonConvert.DeserializeObject<PosterData>(response);
                return postersResult;
            }
        }
        public async Task<WikipediaData> GetWikipediaInfo(string imdbId)
        {
            using (var client = new HttpClient())
            {
                var httpResponseMessage = await client.GetAsync(string.Format("{0}/{1}/API/Wikipedia/{2}/{3}", _imdbSettings.IMDbBaseUrl, _imdbSettings.Lang, _imdbSettings.ApiKey, imdbId));
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var wikiResult = JsonConvert.DeserializeObject<WikipediaData>(response);
                return wikiResult;
            }
        }

    }
}
