# App

A simple FE application for showing different cities and their pictues.

## Prerequisites

- [Angular CLI 15.2.3](https://github.com/angular/angular-cli)
- [Node 18.15.0](https://nodejs.org/en)

## Running locally

1. Run `npm install`
2. Run `ng serve --open` to start the development server with hot reloading (the option `--open` opens your browsers with the correct URL already)

## Code scaffolding

- Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`
- As a side note - the project has also been bootstrapped with [Angular material](https://material.angular.io/)

## Running unit tests (via [Karma](https://karma-runner.github.io))

- Run `ng test`

## Running end-to-end tests

- Run `ng e2e` (note this can only be executed if a package is added which implements end-to-end testing capabilites - currently it doesn't work)

## Building for production

- Run `ng build`
- Copy the created `dist/` folder's content to the webserver of your choosing

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Todos

- Add cities search/listing functionality (under cities-list folder)
  - Create the HTML template (can use components from Angular Material and )
  - Update the component's .ts file with related functionalities
  - Update the city-service with related queries towards the API layer
- Update the backend to return only the token on a successful login
  - Additionally handle errors as well during login and display an error to the user
  - Improve the styling of the login page a little bit (currently it is centered at the top of the page but could probably be centered in the middle instead)
- Update environment files (under /environements) with corresponding information (apiUrl needs to be provided)
- Create a separate HTTP interceptor for requests so that the token which is saved to the LocalStorage is also sent with the request
- Nice to have - add an auth guard to certain routes (only authenticated users should be able to access the cities list for example)
- Nice to have - complete the not-found component by adding a simple 404 not found page for the application
