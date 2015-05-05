angular.module('ChessMining').directive('addMining', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addMining/add-mining.html',
        controller: ['$scope', '$http', 'appService', function ($scope, $http, $appService) {
            
        }]
    }
})