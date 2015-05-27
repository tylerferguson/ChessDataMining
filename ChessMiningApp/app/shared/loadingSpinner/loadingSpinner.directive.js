angular.module('ChessMining').directive('loadingSpinner',  function () {
    return {
        restrict: 'A',
        controller: ['$scope', '$http', 'usSpinnerService', function ($scope, $http, $spinnerService) {
            $scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            $scope.$watch($scope.isLoading, function (v) {
                if (v) {
                    $spinnerService.spin('spinner-1');
                    $(".content-container").addClass('waiting');
                } else {
                    $spinnerService.stop('spinner-1');
                    $(".content-container").removeClass('waiting');

                }
            });
        }]
    }
})