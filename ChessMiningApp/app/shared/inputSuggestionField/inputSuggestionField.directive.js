angular.module('ChessMining').directive("inputSuggestionField", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/inputSuggestionfield/input-suggestion-field-partial.html',
        scope: {
            label: '@inputLabel',
            optionType: '=inputType',
            options: '=options'
        },
        controller: ['$scope', function($scope)  {

            $scope.selectFromSuggestedList = function (selection) {
                $scope.optionType = selection;
                $scope.optionsShown = false;
            }

            $scope.showOptions = function () {
                $scope.optionsShown = true;
            }
        }] 
    }
})