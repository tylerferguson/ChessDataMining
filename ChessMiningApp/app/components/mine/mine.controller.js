angular.module('ChessMining').controller('MineCtrl', ['$scope', 'appService', '$http', function ($scope, appService, $http) {

    $scope.rules = appService.getResults() || [];
}]);