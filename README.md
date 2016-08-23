People Search
=============

A very simple WebAPI application to search for and display person information.

# Architecture

## Server Side
The server side part of the solution makes use of the Onion architecture and currently has three layers: Domain, Infrastructure, and Server.  The Domain and Infrastructure projects are class library projects.  The PeopleSearch.Server server project is a mix of MVC and WebAPI and is combined with the WebUI project to make a full website.  Additionally, the PeopleSearch.Server project makes use of dependency injection with the help of Autofac, for the purpose of injecting an implementation of a few repositories that to help make testing the ApiController methods easier.  Lastly, there is a UnitTests project which contains tests for the Entities, Dtos, Services, and Controllers.

## Client Side

The WebUI project is provided to the MVC project via a virtual directory (`/public`) and makes use of AngularJS, TypeScript, Bootstrap, and Jasmine. Separating the MVC code from the pure client code makes for a nice separation of concerns.

## MVC and WebAPI

There are two MVC controllers:
* AppController - which is responsible for redirecting to the `public/index.html` file produced in the WebUI project
* ErrorController - provides some basic boiler plate for handling errors

There are two WebAPI controllers:
* PersonCommandsApiController - which contains all the command operations for People
* PersonQueriesApiController - which contains all the queries operations for People

Splitting up the command and query operations is the beginning of implementing a CQRS pattern.

## WebUI

The AngularJS code is all located in the ClientAppsTs folder where a controller (`searchAreaCtrl`), view model (`SearchAreaVm.ts`), and a queries service (`SearchQueriesService.ts`) combine to provide the basic pieces of a simple AngularJS application.

* `SearchArea.ts` - sets up the `searchAreaCtrl`, injects the `SearchQueriesServcie`, create a vm (`SearchAreaVm.ts`) on the $scope.
* `SearchAreaVm.ts` - contains all the code to help drive the UI and to help remove as much logic from the view iteself (`SearchArea.html`)
* `SearchQueriesService.ts` handles all the AJAX requests and responses to the WebAPI controllers making use of promises.

Additionally, there are some Jasmine tests in `SearchAreaVmSpecs.ts` that test some of the basic behavior of the VM.  I had planned to write some Jasmine tests for `SearchQueriesService.ts` but ran out of time.

If you have the Chutzpah Test Runner Context Menu Extension installed you can run the Jasmine tests from the WebUI project.  NOTE: When I run all unit tests in solution, the Jasmine tests are picked up but only the Javascript ones (the files compiled from the TypeScript files) in spite of the fact that I told Chutzpah to ignore those in the `chutzpah.json` file.

## Other Considerations

There are a number of things I would like to udpate/improve, but have already invested quite a bit of time into this project so I decided to defer that work.  Here are a few of the things I had planned or would like to have done:

* Write some tests for the `SearchQueriesService.ts`
* Create a directive for people to move that stuff out of `SearchArea.html`
* Add the ability to create and save new people -- a lot of the server code already exists and was used to seed the database
* Fine tune the CSS and even consider switching to LESS

Also, I wanted to make use of some `npm` scripts to automate some client side tasks (bundling, tests, templates, minifying etc...), but wasn't sure if I could assume that `node.js` or `npm` was installed.
