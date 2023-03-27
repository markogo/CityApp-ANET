## Run app locally

1. Download and install [.NET SDK 7.0](https://dotnet.microsoft.com/download) (If not already installed)
2. Execute the command `dotnet run --launch-profile "https"` from inside the solution folder in the command prompt to
   run the application
3. Navigate to `https://localhost:7299/swagger/index.html` in your favourite web browser to test out the API endpoints

## Generate DB migrations (Run from solution folder)

~~~
dotnet ef migrations add InitialDbCreation
~~~

## Update DB with migrations

~~~
dotnet ef database update
~~~

## Delete DB

~~~
dotnet ef database drop
~~~