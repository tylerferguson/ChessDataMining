angular.module('ChessMining').config(function ($routeProvider) {

    $routeProvider
        .when('/', {
            templateUrl: 'app/components/mine/mine.html',
            controller: 'MineCtrl'
        })
        .otherwise('/');
});