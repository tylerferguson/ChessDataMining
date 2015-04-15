angular.module('ChessMining').controller('FilterCtrl', ['$scope', '$http', 'appService', function ($scope, $http, appService) {

    var self = this;
    var projectionFacts = [];
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

    $scope.stopPropagation = function (event) {
        event.stopPropagation();
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

    $scope.submitProjectionFact = function (factOption) {
        var fact = {
            type: factOption,
            name: $scope.name,
            value: $scope.value
        };

        projectionFacts.push(fact);
        appService.updateFilters(projectionFacts);
        $scope.name = '';
        $scope.value = '';

        //var indexOfFact = indexOf(fact, projectionFacts);

        //if (indexOfFact > -1) {
        //    projectionFacts.splice(indexOfFact, 1);
        //} else {
        //    projectionFacts.push(fact);
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