angular.module('ChessMining').controller('MineCtrl', ['$scope', 'appService', '$http', function ($scope, appService, $http) {

    $scope.$appService = appService;

    $scope.predicate = 'LiftCorrelation';
    $scope.reverse = true;

    $scope.setPredicateAndReverse = function (predicate) {
        $scope.predicate = predicate;
        $scope.reverse = !$scope.reverse;
    }
}]);