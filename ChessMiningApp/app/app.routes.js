angular.module('ChessMining').config(function ($routeProvider) {

    $routeProvider
        .when('/', {
            templateUrl: 'app/pages/home.html',
            controller: 'HomeCtrl'
        })
        .when('/Data', {
            templateUrl: 'app/pages/data.html',
            controller: 'DataCtrl'
        })
        .when('/Filter', {
            templateUrl: 'app/pages/filter.html',
            controller: 'FilterCtrl'  
        })
        .when('/Target', {
            templateUrl: 'app/pages/filter.html',
            controller: 'FilterCtrl'
        })
        .when('/Mine', {
            templateUrl: 'app/pages/mine.html',
            controller: 'MineCtrl'
        })
        .otherwise('/');
});