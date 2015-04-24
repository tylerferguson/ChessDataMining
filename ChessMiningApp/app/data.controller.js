angular.module('ChessMining').controller('DataCtrl', ['$scope', 'appService', function ($scope, appService) {

    $scope.submitDataFile = function (event) {
        var file = $("#fileInput")[0].files[0];
        var reader = new FileReader();

        reader.onload = function (event) {
            $scope.$apply(function () {
                $scope.dataFile = event.target.result;
                appService.updateData($scope.dataFile);
            });
        }

        reader.onerror = function () {
            $scope.$apply(function () {
                $scope.dataFile = undefined;
            });
        }

        if (file) {
            reader.readAsText(file);
        } else {
            $scope.dataFile = undefined;
        }
    }
}]);