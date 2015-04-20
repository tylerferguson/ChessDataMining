﻿angular.module('ChessMining').controller('FilterCtrl', ['$scope', '$http', 'appService', function ($scope, $http, appService) {

    var self = this;
    var facts = [];
    $scope.actionButtonClicked;
    $scope.optionClicked = [];

    $(document).ready(function () {
        $http.get('/api/AssociationRules', { 'Content-Type': 'application / json' })
            .then(function (response) {
                $scope.factOptions = response.data;
            },
            function (error) {
                console.log("get error!");
            });
    });

    $scope.selectFromSuggestedList = function (type) {
        $scope.type = type;
        $scope.factOptionsShown = false;
    }

    $scope.showFactOptions = function () {
        $scope.factOptionsShown = true;
    }

    $scope.showActionMenu = function () {
        console.log($(document.activeElement));
        $scope.actionButtonClicked = true;
    }

    $scope.closeActionMenu = function () {
        setTimeout(function () {
            var focus = $(document.activeElement);
            if (focus.is("#fab") || $('#fab').has(focus).length) {
                console.log("still focused");
            } else {
                $scope.$apply(function () {
                    $scope.actionButtonClicked = false;
                })
            }
        }, 0);
    }

    $scope.stopPropagation = function (event) {
        event.stopPropagation();
    }

    $scope.preventDefault = function (event) {
        event.preventDefault();
    }

    $scope.toggleShowForm = function (index) {
        for (i = 0; i < $scope.optionClicked.length; i++) {
            if (i == index) {
                continue;
            }
            if ($scope.optionClicked[i]) {
                $scope.optionClicked[i] = false;
            }
        };
        $scope.optionClicked[index] = $scope.optionClicked[index] ? false : true;

    }

    $scope.submitFact = function (factStage) {
        var fact = {
            type: $scope.type,
            name: $scope.name,
            value: $scope.value
        };

        facts.push(fact);
        appService['update' + factStage](facts);
        $scope.name = '';
        $scope.value = '';

        //var indexOfFact = indexOf(fact, facts);

        //if (indexOfFact > -1) {
        //    facts.splice(indexOfFact, 1);
        //} else {
        //    facts.push(fact);
        //}

        //function indexOf(fact, arr) {
        //    for (var index = 0; index < arr.length; index++) {
        //        var candidateFact = arr[index];

        //        if (candidateFact.type === fact.type && candidateFact.name === fact.name && candidateFact.value === fact.value) {
        //            return index;
        //        }
        //        return -1;
        //    }
        //}
    }
}]);