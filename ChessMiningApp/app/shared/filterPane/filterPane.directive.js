angular.module('ChessMining').directive('filterPane', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/filterPane/filter-pane.html',
        scope: {
            fact: '=fact',
            facts: '=',
            factStage: '@'
        },
        controller: ['$scope', 'appService', function ($scope, $appService) {


            $scope.removeFact = function (index) {
                $scope.facts.splice(index, 1);
                $appService['update' + $scope.factStage]($scope.facts);
            }
        }]
    }
})