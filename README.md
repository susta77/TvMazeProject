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

## Contributing

If you wish to contribute to this project, please make a pull request.



