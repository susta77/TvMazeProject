using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
//using System.Web.Http;
using Test.TvMaze.Controllers;
using TvMaze.Project.Models;
using TvMaze.Project.Services;

namespace TestUnitTvMaze
{
    [TestClass]
    public class UnitTest1
    {
        private readonly TvMazeController _controller;
        private readonly IJwtService _jwtService;
        private readonly IMongoCollection<Show> _collection;

        public UnitTest1()
        {
            //_jwtService = new JwtService();
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            var client = new MongoClient(settings);
            var databases = client.GetDatabase("TvMazeDb");
            _collection = databases.GetCollection<Show>("shows");
            //_controller = new TvMazeController(_jwtService, _collection.Database);
        }

        [TestMethod]
        public void TestMethod1()
        {
             // Arrange
        int showId = 1;
        // Act
        //IHttpActionResult result = _controller.GetShowById(showId);
        ///// Assert
        //Assert.IsType<OkNegotiatedContentResult<Show>>(result);
        }
    }
}
