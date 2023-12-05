using System.Web.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using Test.TvMaze.Helpers;
using System.Collections.Generic;
using System.Net;
using System;

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
        #region Token
        [HttpGet()]
        [Route("getToken")]
        [AllowAnonymous]
        [FiltroToken]
        public IHttpActionResult GenerarToken()
        {
            string keySecret = System.Configuration.ConfigurationManager.AppSettings["KeySecret"];
            var tokenString = _jwtService.GenerarToken(keySecret, "0");
            return Ok(new { Token = tokenString });
        }
        #endregion
    }
}

public class Show
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Language { get; set; }
    public List<string> Genres { get; set; }
    public string Status { get; set; }
    public int Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public string Premiered { get; set; }
    public string Ended { get; set; }
    public string OfficialSite { get; set; }
    public Schedule Schedule { get; set; }
    public Rating Rating { get; set; }
    public int Weight { get; set; }
    public Network Network { get; set; }
    public object WebChannel { get; set; }
    public object DvdCountry { get; set; }
    public Externals Externals { get; set; }
    public Image Image { get; set; }
    public string Summary { get; set; }
    public int Updated { get; set; }
    public Links _Links { get; set; }
}

public class Schedule
{
    public string Time { get; set; }
    public List<string> Days { get; set; }
}

public class Network
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public string OfficialSite { get; set; }
}

public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Timezone { get; set; }
}

public class Externals
{
    public int Tvrage { get; set; }
    public int Thetvdb { get; set; }
    public string Imdb { get; set; }
}

public class Links
{
    public Self Self { get; set; }
    public Previousepisode Previousepisode { get; set; }
}

public class Previousepisode
{
    public string Href { get; set; }
}


public class Episode
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public int Season { get; set; }
    public int Number { get; set; }
    public string Type { get; set; }
    public string Airdate { get; set; }
    public string Airtime { get; set; }
    public string Airstamp { get; set; }
    public int Runtime { get; set; }
    public Rating Rating { get; set; }
    public Image Image { get; set; }
    public string Summary { get; set; }
    public Links _Links { get; set; }
}

public class Rating
{
    public double Average { get; set; }
}

public class Image
{
    public string Medium { get; set; }
    public string Original { get; set; }
}



public class Self
{
    public string Href { get; set; }
}
