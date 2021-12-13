using HangfireApplication.Services.Interfaces;
using IMDB_BAL.Interface;
using IMDB_BAL.Service;
using IMDB_DAL.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApplication.Services
{
    public class JobService : IJobService
    {
        private readonly WatchListService _watchService;

        private readonly IMailService _mailService;
        private readonly IIMDBService _imdbService;



        public JobService(WatchListService watchListService, IMailService mailService, IIMDBService imdbService)
        {
            _watchService = watchListService;
            _mailService = mailService;
            _imdbService = imdbService;
        }

        public async Task WatchListJobAsync()
        {
            var userEmailList = new List<string>() { "b.partovi@gmail.com" };//should get from DB 
            await userEmailList.ForEachAsync(async email =>
            {
                var unWatchedList = _watchService.GetUserUnWatcedList(email);
                if (unWatchedList.Count >= 3)
                {
                    var filmInfoList = new List<TitleData>(unWatchedList.Count);
                    await unWatchedList.ForEachAsync(async item =>
                     {
                         var filmInfo = await _imdbService.GetMovieInfo(item.IMDBId);
                         if (filmInfo != null) filmInfoList.Add(filmInfo);

                     });
                    var topRatedFilm = filmInfoList.OrderByDescending(x => (x.IMDbRating)).FirstOrDefault();

                    var posters = await _imdbService.GetPosters(topRatedFilm.Id);
                    var posterLinks = posters.Posters.Select(x => x.Link);
                    var posterJson = posterLinks != null ?
                                   JsonConvert.SerializeObject(posterLinks, Formatting.Indented,
                                       new JsonSerializerSettings()
                                       {
                                           ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                       }) : null;
                    var wikiInfo = await _imdbService.GetWikipediaInfo(topRatedFilm.Id);
                    var wikiPlotShort = wikiInfo != null ? wikiInfo.PlotShort.Html : null;
                    await _mailService.SendEmailAsync(new Model.MailRequest
                    {
                        Attachments = null,
                        Body = "<H2>Title: </H2>" + topRatedFilm.Title + "<br/>" + "<H2>IMDb Rating:</H2>" +
                        topRatedFilm.IMDbRating + "<br/>" + "<H2>Posters: </H2>" +
                        (posterJson ?? "") + "<H2>Wiki Info: </H2>" + (wikiPlotShort ?? ""),
                        Subject = "Top rated movie to watch from your watchlist",
                        ToEmail = email

                    });
                }

            });

            Console.WriteLine("Job Successfully Done!");

        }


    }
}
