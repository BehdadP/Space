using IMDB_BAL.Interface;
using IMDB_BAL.Service;
using IMDB_DAL.DTO;
using IMDB_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IMDBWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImdbController : ControllerBase
    {
        private readonly ILogger<ImdbController> _logger;

        //private readonly IConfiguration _config;
        private readonly IIMDBService _imdbService;


        public ImdbController(

                ILogger<ImdbController> logger, IIMDBService imdbService)

        {

            _logger = logger;
            _imdbService = imdbService;
        }
        [HttpGet]
        [Route("/Search/{expression}")]
        [ProducesResponseType(typeof(List<SearchResult>), (int)HttpStatusCode.OK)]
        public async Task<Object> Search([FromRoute] string expression)
        {
            return await _imdbService.SearchMovieAsync(expression);
        }
        [HttpGet]
        [Route("/Title/{Id}")]
        [ProducesResponseType(typeof(TitleData), (int)HttpStatusCode.OK)]
        public async Task<Object> GetMoviInfo([FromRoute] string Id)
        {
            return await _imdbService.GetMovieInfo(Id);
        }
    }
}
