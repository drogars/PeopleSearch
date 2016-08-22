/// <reference path="../scripts/typings/angularjs/angular.d.ts" />
var app = angular.module('app', ['ngRoute'])
    .config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/search', { templateUrl: '/public/clientappsts/searcharea/searcharea.html', controller: 'searchAreaCtrl' })
        .otherwise({ redirectTo: '/search' });
    $locationProvider.html5Mode(true);
})
    .run(function ($rootScope) { angular.extend($rootScope); });
