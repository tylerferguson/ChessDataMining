angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {
            
            var current = -1;
            $elem.on('keydown', function (event) {
                if (event.which === 40 && current < $scope.filtered.length -1) {
                    current++;
                    $('.suggestion-focus').removeClass('suggestion-focus');
                    $($elem.next()[0].children[current]).addClass('suggestion-focus');
                } else if (event.which === 38 && current > 0) {
                    current--;
                    $('.suggestion-focus').removeClass('suggestion-focus');
                    $($elem.next()[0].children[current]).addClass('suggestion-focus');
                } else if (event.which === 13) {
                    if ($scope.label !== 'Value') {
                        $scope.$apply($scope.selectFromSuggestedList($scope.filtered[current]));
                        $scope.showOptions = false;
                        event.preventDefault();
                    }
                    current = -1;
                    $('.suggestion-focus').removeClass('suggestion-focus');

                }
            })
        }
    }
})