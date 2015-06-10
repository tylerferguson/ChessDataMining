angular.module('ChessMining').directive("inputSuggestionField", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/inputSuggestionfield/input-suggestion-field-partial.html',
        scope: {
            label: '@inputLabel',
            optionType: '=inputType',
            options: '=options',
            factStage: '='
        },
        controller: ['$scope', 'closeMenuService', function ($scope, $closeMenuService) {

            $scope.current = null;

            $scope.$watch('current', function () {
                console.log('it updated yo: ' + $scope.current);
            })

            $closeMenuService.subscribe('addFilterForm', update);
            $closeMenuService.subscribe('factInput', update);
            $closeMenuService.subscribe('valueInput', update);


            $scope.limit = 10;

            $scope.selectFromSuggestedList = function (selection) {
                $scope.optionType = selection;
                $scope.optionsShown = false;
            }

            $scope.showOptions = function () {
                event.stopPropagation();
                $scope.optionsShown = true;
                $closeMenuService.publish($scope.label.toLowerCase() + 'Input');
            }

            function update(topic) {
                if (topic.replace('Input', '') !== $scope.label.toLowerCase()) {
                    $scope.optionsShown = false;

                }
            }
        }] 
    }
})