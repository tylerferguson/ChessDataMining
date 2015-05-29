angular.module('ChessMining').controller('MineCtrl', ['$scope', '$http', 'appService', 'closeMenuService', function ($scope, $http, $appService, $closeMenuService) {

    $scope.$appService = $appService;
    $scope.$closeMenuService = $closeMenuService;

    $scope.predicate = 'LiftCorrelation';
    $scope.reverse = true;

    $scope.setPredicateAndReverse = function (predicate) {
        $scope.predicate = predicate;
        $scope.reverse = !$scope.reverse;
    }
}]);