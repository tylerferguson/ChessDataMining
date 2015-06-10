angular.module('ChessMining').directive('removeHighlightHover', function() {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {

            $elem.mousemove(function (event) {
                $scope.currentHovered = null;
                $scope.$apply();
                $scope.currentHovered = $scope.filtered.indexOf(event.target.innerText);
                $scope.$apply();
                
                $('.suggestion-hover').removeClass('suggestion-hover');
                $('.suggestion-focus').removeClass('suggestion-focus');
                $(event.target).addClass('suggestion-hover');
            })
        }
    }
})


