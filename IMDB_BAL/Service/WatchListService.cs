using IMDB_DAL.Interface;
using IMDB_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_BAL.Service
{
    public class WatchListService
    {
        private readonly IRepository<WatchList> _watchList;

        public WatchListService(IRepository<WatchList> watchList)
        {
            _watchList = watchList;
        }
        //Get Person Details By Person Id
        public IEnumerable<WatchList> GetWatchListByUserEmail(string UserEmail)
        {
            return _watchList.GetAll().Where(x => x.UserEmail == UserEmail).ToList();
        }
        //GET All Perso Details 
        public IEnumerable<WatchList> GetWatchLists()
        {
            try
            {
                return _watchList.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<WatchList> GetUserUnWatcedList(string userEmail)
        {
            return _watchList.GetAll().Where(x => x.UserEmail == userEmail && !x.Watched).ToList();
        }
        //Add Person
        public async Task<WatchList> AddWatchList(WatchList watchList)
        {
            return await _watchList.Create(watchList);
        }
        //Delete Person 
        public bool DeleteWatchList(string userEmail)
        {

            try
            {
                var DataList = _watchList.GetAll().Where(x => x.UserEmail == userEmail).ToList();
                foreach (var item in DataList)
                {
                    _watchList.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        public bool UpdateWatchList(WatchList watchList)
        {
            try
            {
                var movie = GetWatchListByUserEmail(watchList.UserEmail).Where(x => x.IMDBId == watchList.IMDBId).FirstOrDefault();
                if (movie == null) return false;
                movie.Watched = watchList.Watched;
                _watchList.Update(movie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}