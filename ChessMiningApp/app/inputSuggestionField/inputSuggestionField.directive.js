angular.module('ChessMining').directive("inputSuggestionField", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/inputSuggestionfield/input-suggestion-field-partial.html'
        //link: function($scope, $element, $attrs)  {

        //    //$scope.optionType = $attrs.inputType;

        //    //$scope.selectFromSuggestedList = function (selection) {
        //    //    $scope.$parent[$scope.optionType] = selection;
        //    //    $scope.$parent[$scope.optionType + 'OptionsShown'] = false;
        //    //}

        //    //$scope.showOptions = function () {
        //    //    $scope.$parent[$scope.optionType + 'OptionsShown'] = true;
        //    //}
        //}
    }
})