angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {

            $scope.$watch('filtered', function () {
                //if ($elem[0].innerText === $scope.filtered[0].replace('Fact', '')) {
                //    $elem.addClass('suggestion-focus');
                //}
                $scope.current = null;
            }, true)

            $('.' + $scope.factStage + '-filter-display' + ' .' + $scope.label + '-suggestion-field-input').on('keydown', function (event) {
                if (event.which === 40) {
                    if ($scope.current === null) {
                        $scope.current = -1;
                    }
                    $scope.current++;

                    $elem.removeClass('suggestion-focus');
                    if ($elem[0].innerText === $scope.filtered[$scope.current % $scope.filtered.length].replace('Fact', '')) {
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
                    $elem.removeClass('suggestion-focus');
                    if ($elem[0].innerText === $scope.filtered[$scope.current % $scope.filtered.length].replace('Fact', '')) {
                        $elem.addClass('suggestion-focus');
                    }
                } else if (event.which === 13 || event.which === 9) {
                    if ($scope.current !== null) {
                        event.preventDefault();
                        if ($elem[0].innerText === $scope.filtered[$scope.current % $scope.filtered.length].replace('Fact', '')) {
                            if ($scope.filtered.length) {
                                $scope.$apply($scope.selectFromSuggestedList($scope.filtered[$scope.current % $scope.filtered.length]));
                            }
                        }
                    }
                    
                    $elem.removeClass('suggestion-focus');
                } else {
                    $scope.current = null;
                    $elem.removeClass('suggestion-focus');
                }
            })
        }
    }
})