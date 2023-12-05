# TestTvMate

# Project Title Test.TvMaze

This is a project that utilizes .NET Core and MongoDB.

## Prerequisites

- .NET 4.7.2
- MongoDB

## Configuration

The application settings are located in the `Web.config` file. Here you can configure the MongoDB database connection string and other settings.


## Running the Application

To run the application, open a terminal in the project folder and type:

dotnet run


## Using the Application

consume webapi with:

-----------GetToken
curl --location 'http://localhost:52023/api/tvmaze/getToken/Q3J5cHRvZ3JhcGhpY0tleUV4YW1wbGUxMjM0NTY3ODkwYWJjZGVmZw == !' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRENvbGFib3JhZG9yIjoiMSIsIm5iZiI6MTcwMTU5NjgxNCwiZXhwIjoxNzAyMjAxNjE0LCJpYXQiOjE3MDE1OTY4MTR9.OhRBOR0RNmYCKWR0Tb94g_Det_U2L4KyRQ_9mPzkNtE' \
--data ''
-----------Shows
curl --location --request POST 'http://localhost:52023/api/tvmaze/shows/2' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJTaG93IjoiIiwibmJmIjoxNzAxNzIzOTU1LCJleHAiOjE3MDIzMjg3NTUsImlhdCI6MTcwMTcyMzk1NX0.bgcVsiH1vKsKZNiHaXRbPrTFd-u6MttY71Km2Fo3IKo'
-----------StoreShow
curl --location --request POST 'http://localhost:52023/api/tvmaze/storeShows/2' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJTaG93IjoiIiwibmJmIjoxNzAxNzIzOTU1LCJleHAiOjE3MDIzMjg3NTUsImlhdCI6MTcwMTcyMzk1NX0.bgcVsiH1vKsKZNiHaXRbPrTFd-u6MttY71Km2Fo3IKo'
## Error Handling

Errors are logged in the `log.txt` file in the main project folder.

# TvMaze Controller Test

This project contains unit tests for the `TvMazeController` class within the context of an application using ASP.NET Web API and MongoDB as the backend.

## Initial Setup

Before running the tests, make sure you have set up your development environment correctly. You can use a local MongoDB database, ensuring it is listening on `localhost:27017`. Also, make sure you have all the necessary dependencies installed.

## Test Configuration

The constructor of the `TvMazeControllerTest` class is used to initialize the dependencies needed to run the tests. In particular, an instance of `TvMazeController` is initialized using a JWT service and a MongoDB database.

```csharp
public TvMazeControllerTest()
{
    _jwtService = new JwtService();
    var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
    var client = new MongoClient(settings);
    var databases = client.GetDatabase("TvMazeDb");
    _collection = databases.GetCollection<Show>("shows");
    _controller = new TvMazeController(_jwtService, _collection.Database);
}
Running the Tests
The tests are executed using xUnit. Here's an example of three tests:

GetShowTest: Verifies that the GetShowById method returns a result of type OkNegotiatedContentResult<Show>.
csharp
Copy code
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
StoreShowTest: Verifies that the StoreShowById method returns a result of type OkNegotiatedContentResult<Show>.
csharp
Copy code
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
GetTokenTest: Verifies that the GenerarToken method returns a result of type OkNegotiatedContentResult<Token>.
csharp
Copy code
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
Running the Tests
To run the tests, use your preferred development environment or execute the commands from the command line, such as:

bash
Copy code
dotnet test

## Contributing

If you wish to contribute to this project, please make a pull request.



