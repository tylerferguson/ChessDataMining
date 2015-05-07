angular.module('ChessMining').directive('filterDisplay', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/filterDisplay/filter-display.html',
        scope: {
            factStage: '@'
        },
        controller: ['$scope', function ($scope) {

            $scope.closeInputForm = function () {
                setTimeout(function () {
                    var focus = $(document.activeElement);
                    if (focus.is(".add-filter-form") || $('.add-filter-form').has(focus).length) {
                        console.log("still focused");
                    } else {
                        $scope.$apply(function () {
                            $scope.buttonClicked = false;
                        })
                    }
                }, 0);
            }

            $scope.showInputForm = function () {
                $scope.buttonClicked = true;
            }

        }]
    }
})