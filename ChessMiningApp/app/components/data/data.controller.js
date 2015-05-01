angular.module('ChessMining').controller('DataCtrl', ['$scope', 'appService', function ($scope, appService) {

    $scope.dataFile = appService.getDataFile() || {};

    $scope.$watch('dataFile.data', function (newValue, oldValue) {
        $scope.dataFile.displayData = JSON.stringify($scope.dataFile.data, null, 2);
    });
}]);