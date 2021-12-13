using IMDB_BAL.Service;
using IMDB_DAL.Interface;
using IMDB_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace IMDBWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WathcListController : ControllerBase
    {
        private readonly WatchListService _watchListService;

        private readonly IRepository<WatchList> _WatchList;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WathcListController(IRepository<WatchList> WatchList, WatchListService ProductService, IHttpContextAccessor httpContextAccessor)
        {
            _watchListService = ProductService;
            _WatchList = WatchList;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost("AddWatchList")]
        public async Task<Object> AddWatchList([FromBody] WatchList watchList)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _watchListService.AddWatchList(watchList);
                return true;
            }
            catch (Exception ex)
            {

                return ex;
            }
        }
        [HttpPut("UpdateWatchList")]
        public bool UpdateWatchList(WatchList watachList)
        {
            try
            {
                _watchListService.UpdateWatchList(watachList);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet("GetWatchListByUser")]
        public Object GetWatchListByUser(string UserEmail)
        {
            var data = _watchListService.GetWatchListByUserEmail(UserEmail);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }


    }
}