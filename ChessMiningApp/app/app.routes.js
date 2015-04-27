angular.module('ChessMining').config(function ($routeProvider) {

    $routeProvider
        .when('/', {
            templateUrl: 'app/components/home/home.html',
            controller: 'HomeCtrl'
        })
        .when('/Data', {
            templateUrl: 'app/components/data/data.html',
            controller: 'DataCtrl'
        })
        .when('/Filter', {
            templateUrl: 'app/components/filter/filter.html',
            controller: 'FilterCtrl'  
        })
        .when('/Target', {
            templateUrl: 'app/components/filter/filter.html',
            controller: 'FilterCtrl'
        })
        .when('/Mine', {
            templateUrl: 'app/components/mine/mine.html',
            controller: 'MineCtrl'
        })
        .otherwise('/');
});