using System.Web.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using Test.TvMaze.Helpers;
using System.Collections.Generic;
using System.Net;
using System;
using TvMaze.Project.Models;
using TvMaze.Project.Services;

namespace Test.TvMaze.Controllers
{
    [RoutePrefix("api/TvMaze")]
    public class TvMazeController : ApiController
    {
        private readonly IJwtService _jwtService;
        private readonly IMongoCollection<Show> _collection;

        public TvMazeController(IJwtService jwtService, IMongoDatabase database)
        {
            _jwtService = jwtService;
            _collection = database.GetCollection<Show>("shows");
        }

        [HttpPost()]
        [Route("shows/{id}")]
        [Authorize]
        [FiltroToken]
        public IHttpActionResult GetShowById(int id)
        {
            var result = _collection.Find(s => s.Id == id).FirstOrDefault();
            return Ok(result);
        }


        [HttpPost()]
        [Route("storeShows/{id}")]
        [Authorize]
        [FiltroToken]
        public IHttpActionResult StoreShowById(int id)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string apiUrl = $"http://api.tvmaze.com/shows/{id}";
                    string jsonResult = client.DownloadString(apiUrl);
                    var show = JsonConvert.DeserializeObject<Show>(jsonResult);
                    _collection.InsertOneAsync(show);
                    return Ok(show);
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Spettacolo con ID {id} non trovato.");
                    }
                    else
                    {
                        Console.WriteLine($"Errore durante la richiesta all'API di TVMaze: {ex.Message}");
                    }
                }
            }
            return BadRequest("Show not found");
        }
        [HttpGet()]
        [Route("getToken/{key}")]
        [AllowAnonymous]
        [FiltroToken]
        public IHttpActionResult GenerarToken(string key)
        {
            string keySecret = key;// System.Configuration.ConfigurationManager.AppSettings["KeySecret"];
            var token = _jwtService.GenerarToken(keySecret);
            return Ok(token);
        }
    }
}


