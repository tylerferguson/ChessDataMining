﻿angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {
            
            var current = -1;
            var currentSuggestion;
            $elem.on('keydown', function (event) {
                if (event.which === 40 && current < $scope.filtered.length -1) {
                    $('.suggestion-focus').removeClass('suggestion-focus');
                    currentSuggestion = nextSuggestion();
                    currentSuggestion.addClass('suggestion-focus');
                } else if (event.which === 38 && current > 0) {
                    $('.suggestion-focus').removeClass('suggestion-focus');
                    currentSuggestion = previousSuggestion();
                    currentSuggestion.addClass('suggestion-focus');
                } else if (event.which === 13) {
                    if ($scope.label !== 'Value') {
                        event.preventDefault();
                    }
                    if (current !== -1) {
                        $scope.$apply($scope.selectFromSuggestedList($scope.filtered[current]));
                    }
                    current = -1;
                    $('.suggestion-focus').removeClass('suggestion-focus');

                    function nextSuggestion() {
                        return $($elem.next()[0].children[++current]);
                    }

                    function previousSuggestion() {
                        return $($elem.next()[0].children[--current]);
                    }
                }
            })
        }
    }
})