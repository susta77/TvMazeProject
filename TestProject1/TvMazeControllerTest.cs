using MongoDB.Driver;
using System.Web.Http;
using System.Web.Http.Results;
using Test.TvMaze.Controllers;
using TvMaze.Project.Models;
using TvMaze.Project.Services;

public class TvMazeControllerTest
{
    private readonly TvMazeController _controller;
    private readonly IJwtService _jwtService;
    private readonly IMongoCollection<Show> _collection;
    public TvMazeControllerTest()
    {
        _jwtService = new JwtService();
        var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
        var client = new MongoClient(settings);
        var databases = client.GetDatabase("TvMazeDb");
        _collection = databases.GetCollection<Show>("shows");
        _controller = new TvMazeController(_jwtService, _collection.Database);
    }

    [Fact]
    public void GetShowTest()
    {
        // Arrange
        int showId = 1;
        // Act
        IHttpActionResult result = _controller.GetShowById(showId);
        /// Assert
        Assert.IsType<OkNegotiatedContentResult<Show>>(result);
    }

    [Fact]
    public void StoreShowTest()
    {
        // Arrange
        int showId = 1;
        // Act
        IHttpActionResult result = _controller.StoreShowById(showId);
        /// Assert
        Assert.IsType<OkNegotiatedContentResult<Show>>(result);
    }


    [Fact]
    public void GetTokenTest()
    {
        // Arrange
        string key = "Q3J5cHRvZ3JhcGhpY0tleUV4YW1wbGUxMjM0NTY3ODkwYWJjZGVmZw == !";
        // Act
        var result = _controller.GenerarToken(key);
        // Assert
        var okResult = Assert.IsType<System.Web.Http.Results.OkNegotiatedContentResult<Token>>(result);
        var content = okResult as dynamic;
        Assert.NotNull(content);
        Assert.NotNull(((System.Web.Http.Results.OkNegotiatedContentResult<Token>)content).Content.Json);
    }

}
