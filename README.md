# CityApp-ANET

A simple web application that displays a list of editable cities

## Prerequisites

* [Docker](https://www.docker.com/)
* [Docker Compose](https://docs.docker.com/compose/)

1. Database will run on localhost:5432. 
2. Backend  will run on http://localhost:5083/api.
3. Frontend will run on http://localhost:8080.

## Tech stack

* Database: [PostgreSQL](https://www.postgresql.org/)
* Backend:  [.NET](https://dotnet.microsoft.com)
* Frontend: [Angular](https://github.com/angular)

## How to run
1. Clone this repository
2. Run all containers with `docker compose up` from the root folder (where `compose.yaml` is located at)
3. Navigate to `http://localhost:8080` in your browser
4. Enjoy

## Seeded users

When the application starts, a list of users are created that are available for usage.

* To view the list of cities the user must first be authenticated - `Default_user` can be used for this purpose or a new account can be registered instead.
* To edit the names and photos of cities, the user must be authenticated and have a role of `ROLE_ALLOW_EDIT` or `ROLE_ADMIN`.

| ID | USERNAME     | PASSWORD | ROLE            |
|----|--------------|----------|-----------------|
| 1  | Default_user | Kala.123 | ROLE_USER       |
| 2  | Editor_user  | Karu.123 | ROLE_ALLOW_EDIT |
| 3  | Admin_user   | Koer.123 | ROLE_ADMIN      |

## Teamwork notes

* Team setup: Marko (BE, FE), Kristjan (FE), Merily (DevOps, Design)
* Worked hours: 83h
  
* Benefits of working in a team:
1. More efficient planning for all parts.
2. Dividing tasks means better focus, faster development and richer implementation.
3. When a team-member falls ill or cannot continue his/her work for some reason, other team members can take over the unfinished work.
4. Multiple people reviewing the code ensures that the code is of high quality. This also reduces the chance of errors or bugs in the application.
5. Teams consist of individuals with diverse skill sets, expertise, and experience which recude the possibility of unknowingness.

**Note: Initially we divided work of each area to separate person. 
When one of the developers fell ill, others took over to 
finish the work while following the design agreed beforehand.**

### Takeover process
All made changes were pushed into a separate branch

The developer who fell ill also included comments and TODO's in code and README to ease the takeover for other developers on the team.
Information related to the branch was shared with teammates.


