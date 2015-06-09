angular.module('ChessMining').directive('removeHighlightHover', function() {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {

            $elem.mousemove(function (event) {
                $('.suggestion-hover').removeClass('suggestion-hover');
                $('.suggestion-focus').removeClass('suggestion-focus');
                $(event.target).addClass('suggestion-hover');
            })
        }
    }
})


