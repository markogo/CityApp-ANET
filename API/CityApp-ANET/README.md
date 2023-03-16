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