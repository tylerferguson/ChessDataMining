angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {
            
            var current = -1;
            $elem.on('keydown', function (event) {
                if (event.which === 40) {
                    current++;
                } else if (event.which === 38) {
                    current--;
                } else if (event.which === 13) {
                    if ($scope.label !== 'Value') {
                        $scope.$apply($scope.selectFromSuggestedList($('.suggestion-focus')[0].innerText));
                        event.preventDefault();
                    } 
                }
                $('.suggestion-focus').removeClass('suggestion-focus');
                $('.suggestion').eq(current).addClass('suggestion-focus');
            })
        }
    }
})