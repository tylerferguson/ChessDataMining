angular.module('ChessMining').directive('addData', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addData/add-data.html',
        controller: ['$scope', '$timeout', 'appService', function ($scope, $timeout, $appService) {

            $scope.submitDataFile = function (event) {
                var file = $("#file-input")[0].files[0];
                var reader = new FileReader();

                reader.onload = function (event) {
                    $scope.$apply(function () {
                        $scope.dataFile.data = JSON.parse(event.target.result);
                        $appService.updateData($scope.dataFile.data);
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

            $scope.triggerFileInput = function () {
                $timeout(function () {
                    $('#file-input').trigger('click');
                }, 0, false)
            }
        }]
    }
})