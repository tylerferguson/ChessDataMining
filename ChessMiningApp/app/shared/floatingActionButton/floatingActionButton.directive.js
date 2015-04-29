angular.module('ChessMining').directive('floatingActionButton', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/floatingActionButton/floating-action-button.html',
        scope: true,
        transclude: true,
        controller: ['$scope', function ($scope) {

            $scope.showActionMenu = function () {
                $scope.actionButtonClicked = true;
            }

            $scope.closeActionMenu = function () {
                setTimeout(function () {
                    var focus = $(document.activeElement);
                    if (focus.is("#fab") || $('#fab').has(focus).length) {
                        console.log("still focused");
                    } else {
                        $scope.$apply(function () {
                            $scope.actionButtonClicked = false;
                        })
                    }
                }, 0);
            }
        }]
    }
})