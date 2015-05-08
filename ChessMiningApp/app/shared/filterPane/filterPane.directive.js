angular.module('ChessMining').directive('filterPane', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/filterPane/filter-pane.html',
        scope: {
            index: '@',
            fact: '=fact',
            facts: '=',
            factStage: '@'
        },
        controller: ['$scope', 'appService', function ($scope, $appService) {


            $scope.removeFact = function () {
                $scope.facts.splice($scope.index, 1);
                $appService['update' + $scope.factStage]($scope.facts);
            }
        }]
    }
})