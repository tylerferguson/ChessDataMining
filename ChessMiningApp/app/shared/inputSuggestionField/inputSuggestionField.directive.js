angular.module('ChessMining').directive("inputSuggestionField", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/inputSuggestionfield/input-suggestion-field-partial.html',
        scope: {
            label: '@inputLabel',
            optionType: '=inputType',
            options: '=options',
            closeInputForm: '&'
        },
        controller: ['$scope', 'closeMenuService', function ($scope, $closeMenuService) {

            $closeMenuService.subscribe('suggestionBox', update);

            $scope.limit = 10;

            $scope.selectFromSuggestedList = function (selection) {
                $scope.optionType = selection;
                $scope.optionsShown = false;
            }

            $scope.showOptions = function () {
                event.stopPropagation();
                $scope.optionsShown = true;
            }

            function update(topic, status) {
                $scope.optionsShown = status;
            }
        }] 
    }
})