angular.module('ChessMining').directive("addFilters", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addFilters/add-filters.html',
        scope: {
            factStage: '@',
            buttonClicked: '=',
            closeInputForm: '&'
        },
        controller: ['$scope', '$http', '$location', 'appService', function ($scope, $http, $location, $appService) {

            var facts = [];
            var simpleFacts;

            $scope.fact = '';
            $scope.value = '';
            $scope.factOptions = {
                facts: []
                //key value pairs of individual facts
            };

            $scope.submitFact = function () {
                var fact = $scope.fact;
                var name;
                if (simpleFacts.indexOf(fact) > -1) {
                    name = fact;
                    fact = 'SimpleFact';
                }
                var fact = {
                    type: fact,
                    name: name,
                    value: $scope.value
                };

                facts.push(fact);
                $scope.fact = '';
                $scope.value = '';
                updateFilters();
                $scope.buttonClicked = false;

                function updateFilters() {
                    $appService['update' + $scope.factStage](facts);
                }
            }

            function getFactOptions() {
                $http.get($location.$$absUrl.replace('#/', 'api/AssociationRules'), { 'Content-Type': 'application / json' })
                    .then(function (response) {
                        response.data.forEach(function (elem) {
                            if (elem.FactType === "SimpleFact") {
                                simpleFacts = elem.ValidParams;
                                simpleFacts.forEach(function (validNameParam) {
                                    $scope.factOptions.facts.push(validNameParam);
                                    $scope.factOptions[validNameParam] = {};
                                    $scope.factOptions[validNameParam].validValueParams = '';
                                })
                            }
                            else {
                                $scope.factOptions.facts.push(elem.FactType);
                                $scope.factOptions[elem.FactType] = {};
                                $scope.factOptions[elem.FactType].validValueParams = elem.ValidParams;
                            }
                        })
                    },
                    function (error) {
                        console.log("get error!");
                    });
            }

            getFactOptions();
          

        }]
    }
})