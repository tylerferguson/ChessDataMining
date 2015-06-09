angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {

            $scope.$watch('filtered', function () {
                $scope.current = null;
            }, true)

            $('.' + $scope.factStage + '-filter-display' + ' .' + $scope.label + '-suggestion-field-input').on('keydown', function (event) {

                $elem.removeClass('suggestion-focus');
                if (event.which === 40) {
                    if ($scope.current === null) {
                        $scope.current = -1;
                    }
                    $scope.current++;
                    if (isCurrentlySelected()) {
                        $elem.addClass('suggestion-focus');
                    }
                } else if (event.which === 38) {
                    if ($scope.current === null) {
                        $scope.current = $scope.filtered.length;
                    }
                    $scope.current--;
                    if ($scope.current < 0) {
                        $scope.current = $scope.filtered.length + $scope.current;
                    }

                    if (isCurrentlySelected()) {
                        $elem.addClass('suggestion-focus');
                    }
                } else if (event.which === 13 || event.which === 9) {
                    if ($scope.current !== null) {
                        event.preventDefault();
                        if (isCurrentlySelected()) {
                            if ($scope.filtered.length) {
                                $scope.$apply($scope.selectFromSuggestedList($scope.filtered[$scope.current % $scope.filtered.length]));
                            }
                        }
                    }
                } else {
                    $scope.current = null;
                }
            })

            function isCurrentlySelected() {
                return $elem[0].innerText === $scope.filtered[$scope.current % $scope.filtered.length].replace('Fact', '');
            }
        }
    }
})