using IMDB_DAL.Data;
using IMDB_DAL.Interface;
using IMDB_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_DAL.Repository
{
    public class RepositoryWatchList : IRepository<WatchList>
    {
        ApplicationDbContext _dbContext;
        public RepositoryWatchList(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<WatchList> Create(WatchList _object)
        {
            var obj = await _dbContext.WatchLists.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(WatchList _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<WatchList> GetAll()
        {
            try
            {
                return _dbContext.WatchLists.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public WatchList GetById(int Id)
        {
            return _dbContext.WatchLists.Where(x => x.Watched == false && x.IMDBId == Id.ToString()).FirstOrDefault();
        }

        public void Update(WatchList _object)
        {
            _dbContext.WatchLists.Update(_object);
            _dbContext.SaveChanges();
        }

        WatchList IRepository<WatchList>.GetById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
