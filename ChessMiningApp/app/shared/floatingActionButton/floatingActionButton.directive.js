angular.module('ChessMining').directive('floatingActionButton', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/floatingActionButton/floating-action-button.html',
        scope: true,
        transclude: true,
        controller: ['$scope', function ($scope) {

           
        }]
    }
})