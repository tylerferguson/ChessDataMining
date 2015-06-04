angular.module('ChessMining').directive('filterDisplay', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/filterDisplay/filter-display.html',
        scope: {
            factStage: '@'
        },
        controller: ['$scope', 'appService', function ($scope, $service) {

            $scope.$service = $service;

            $scope.showInputForm = function () {
                event.stopPropagation();
                $scope.buttonClicked = true;
            }

        }]
    }
})