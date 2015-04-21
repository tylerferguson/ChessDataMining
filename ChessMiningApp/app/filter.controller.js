angular.module('ChessMining').controller('FilterCtrl', ['$scope', '$http', '$location', 'appService', function ($scope, $http, $location, appService) {

    var self = this;
    var factStage = $location.path().replace("/", "") + 's';

    $scope.facts = appService['get' + factStage]() || [];
    $scope.actionButtonClicked;
    $scope.optionClicked = [];
    $scope.valueOptions = ['']
    $scope.factOptions = {
        facts: []
        //key value paris of individual facts
    };

    $(document).ready(function () {
        $http.get('/api/AssociationRules', { 'Content-Type': 'application / json' })
            .then(function (response) {
                //var index = response.data.indexOf("SimpleFact");
                //response.data.splice(index, 0, "WhiteFact", "BlackFact", "ResultFact");
                response.data.forEach(function (elem) {
                    $scope.factOptions.facts.push(elem.FactType);
                    $scope.factOptions[elem.FactType] = {};
                    if (elem.FactType === "SimpleFact") {
                        $scope.factOptions.SimpleFact.validNameParams = elem.ValidParams;
                    } else {
                        $scope.factOptions[elem.FactType].validValueParams = elem.ValidParams;
                    }
                })
            },
            function (error) {
                console.log("get error!");
            });
    });

    $scope.selectFromSuggestedList = function (selection, optionType) {
        $scope[optionType] = selection;
        $scope[optionType + 'OptionsShown'] = false;
    }

    $scope.showOptions = function (optionType) {
        $scope[optionType + 'OptionsShown'] = true;
    }

    $scope.showActionMenu = function () {
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

    $scope.submitFact = function () {
        var fact = {
            type: $scope.fact,
            name: $scope.name,
            value: $scope.value
        };

        $scope.facts.push(fact);
        appService['update' + factStage]($scope.facts);
        $scope.fact = '';
        $scope.name = '';
        $scope.value = '';
    }

    $scope.removeFact = function (index) {
        $scope.facts.splice(index, 1);
    }
}]);