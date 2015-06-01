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

            if ($scope.fact.type === "SimpleFact") {
                $scope.displayType = $scope.fact.name;
            } else {
                $scope.displayType = $scope.fact.type.replace('Fact', '');
            }
            $scope.displayValue = $scope.fact.value;

            var remainingList;
            $scope.removeFact = function () {
                $scope.facts.splice($scope.index, 1);
                if ($scope.facts.length) {
                    remainingList = $scope.facts;
                }
                $appService['update' + $scope.factStage](remainingList);
            }
        }]
    }
})