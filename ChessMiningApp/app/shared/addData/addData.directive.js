angular.module('ChessMining').directive('addData', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addData/add-data.html',
        controller: ['$scope', '$timeout', '$http', 'appService', function ($scope, $timeout, $http, $appService) {
            var dataFile = {};
            $scope.submitDataFile = function (event) {
                event.preventDefault();
                var file = $("#file-input")[0].files[0];
                var reader = new FileReader();

                reader.onload = function (event) {
                    $scope.$apply(function () {
                        dataFile.name = file.name;
                        dataFile.data = JSON.parse(event.target.result);
                        $appService.updateDataFile(dataFile);
                    });
                }

                //reader.onerror = function () {
                //    $scope.$apply(function () {
                //        $scope.dataFile = undefined;
                //    });
                //}

                if (file) {
                    reader.readAsText(file);
                } else {
                    //$scope.dataFile = {};
                }
            }

            $scope.triggerFileInput = function () {
                $timeout(function () {
                    $('#file-input').trigger('click');
                }, 0, false)
            }

            $scope.triggerDropboxInput = function () {
                var options = {
                    success: function (files) {
                        var file = files[0];
                        $scope.$apply(function () {
                            dataFile.name = file.name;
                            $http.get(file.link).then(function (data) {
                                dataFile.data = data;
                                $appService.updateDataFile(dataFile);
                            })
                        });
                    },
                    linkType: 'direct',
                    extensions: ['.json']
                }

                Dropbox.choose(options);
            }
        }]
    }
})