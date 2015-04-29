angular.module('ChessMining').directive('addData', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addData/add-data.html',
        controller: ['$scope', 'appService', function ($scope, $appService) {

            $scope.submitDataFile = function (event) {
                var file = $("#fileInput")[0].files[0];
                var reader = new FileReader();

                reader.onload = function (event) {
                    $scope.$apply(function () {
                        $scope.dataFile.data = JSON.parse(event.target.result);
                        $appService.updateData($scope.dataFile);
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
        }]
    }
})