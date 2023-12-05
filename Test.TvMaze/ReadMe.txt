# Project Title Test.TvMaze

This is a project that utilizes .NET Core and MongoDB.

## Prerequisites

.NET 4.7.2
MongoDB

## Configuration

The application settings are located in the Web.config file. Here you can configure the MongoDB database connection string and other settings.


## Running the Application

To run the application, open a terminal in the project folder and type:

dotnet run


## Using the Application

consume webapi with:

GetToken
curl --location 'http://localhost:52023/api/TvMaze/getToken' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRENvbGFib3JhZG9yIjoiMSIsIm5iZiI6MTcwMTU5NjgxNCwiZXhwIjoxNzAyMjAxNjE0LCJpYXQiOjE3MDE1OTY4MTR9.OhRBOR0RNmYCKWR0Tb94g_Det_U2L4KyRQ_9mPzkNtE' \
--data ''
Shows
curl --location --request POST 'http://localhost:52023/api/TvMaze/shows/1' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRENvbGFib3JhZG9yIjoiMCIsIm5iZiI6MTcwMTYwODc2NywiZXhwIjoxNzAyMjEzNTY3LCJpYXQiOjE3MDE2MDg3Njd9.WCwnCXwQzbiN0zAZtj0_IVgytFtvz7j7Luqg5yEk1Vk'
StoreShows
curl --location --request POST 'http://localhost:52023/api/tvmaze/storeShows/1' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJZb3VySW5mbyI6Ik15SW5mbyIsIm5iZiI6MTcwMTU5OTA5NSwiZXhwIjoxNzAyMjAzODk1LCJpYXQiOjE3MDE1OTkwOTV9.2ZCe1BBBP3Lp82F64EnHx05SWoVk8wpvuK30lPqGwBY'

## Error Handling

Errors are logged in the log.txt file in the main project folder.

## Contributing

If you wish to contribute to this project, please make a pull request.

## License



