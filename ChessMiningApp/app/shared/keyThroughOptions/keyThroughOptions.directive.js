angular.module('ChessMining').directive('keyThroughOptions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs) {

            //console.log($('.' + $scope.factStage + '-filter-display' + ' .' + $scope.label + '-suggestion-field-input'));

            $('.' + $scope.factStage + '-filter-display' + ' .' + $scope.label + '-suggestion-field-input').on('keydown', function (event) {
                //console.log($elem[0].innerText);
                //console.log($scope.filtered);
                if (event.which === 40) {
                    console.log($scope.current);
                    $elem.removeClass('suggestion-focus');
                    if ($elem[0].innerText === $scope.filtered[$scope.current++ % $scope.filtered.length].replace('Fact', '')) {
                        $elem.addClass('suggestion-focus');
                    }
                } 
                //else if (event.which === 38 && current > 0) {
                //    $('.suggestion-focus').removeClass('suggestion-focus');
                //    currentSuggestion = previousSuggestion();
                //    currentSuggestion.addClass('suggestion-focus');
                //} else if (event.which === 13 || event.which === 9) {
                //    if ($scope.label !== 'Value') {
                //        event.preventDefault();
                //    }
                //    if ($scope.filtered.length) {
                //        $scope.$apply($scope.selectFromSuggestedList($scope.filtered[current]));
                //    }
                //    current = 0;
                //    $('.suggestion-focus').removeClass('suggestion-focus');

                //    function nextSuggestion() {
                //        return $($elem.next()[0].children[++current]);
                //    }

                //    function previousSuggestion() {
                //        return $($elem.next()[0].children[--current]);
                //    }
                //} else {
                //    $('.suggestion-focus').removeClass('suggestion-focus');
                //    $($elem.next()[0].children[current]).addClass('suggestion-focus');
                //}
            })
            
            //var current = 0;
            //var currentSuggestion;
            //$elem.on('keydown', function (event) {
            //    if (event.which === 40 && current < $scope.filtered.length -1) {
            //        $('.suggestion-focus').removeClass('suggestion-focus');
            //        currentSuggestion = nextSuggestion();
            //        currentSuggestion.addClass('suggestion-focus');
            //    } else if (event.which === 38 && current > 0) {
            //        $('.suggestion-focus').removeClass('suggestion-focus');
            //        currentSuggestion = previousSuggestion();
            //        currentSuggestion.addClass('suggestion-focus');
            //    } else if (event.which === 13 || event.which === 9) {
            //        if ($scope.label !== 'Value') {
            //            event.preventDefault();
            //        }
            //        if ($scope.filtered.length) {
            //            $scope.$apply($scope.selectFromSuggestedList($scope.filtered[current]));
            //        }
            //        current = 0;
            //        $('.suggestion-focus').removeClass('suggestion-focus');

            //        function nextSuggestion() {
            //            return $($elem.next()[0].children[++current]);
            //        }

            //        function previousSuggestion() {
            //            return $($elem.next()[0].children[--current]);
            //        }
            //    } else {
            //        $('.suggestion-focus').removeClass('suggestion-focus');
            //        $($elem.next()[0].children[current]).addClass('suggestion-focus');
            //    }
            //})
        }
    }
})