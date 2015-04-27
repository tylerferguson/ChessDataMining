angular.module('ChessMining').directive('filterPane', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/filterPane/filter-pane.html',
        scope: {
            fact: '=fact',
            facts: '=facts'
        },
        controller: ['$scope', function ($scope) {

            $scope.removeFact = function (index) {
                $scope.facts.splice(index, 1);
            }
        }]
    }
})