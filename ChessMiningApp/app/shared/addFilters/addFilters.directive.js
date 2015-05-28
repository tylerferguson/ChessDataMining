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
            $scope.fact = '';
            $scope.name = '';
            $scope.value = '';
            $scope.factOptions = {
                facts: []
                //key value pairs of individual facts
            };

            $scope.submitFact = function () {
                var fact = {
                    type: $scope.fact,
                    name: $scope.name,
                    value: $scope.value
                };

                facts.push(fact);
                $scope.fact = '';
                $scope.name = '';
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
            }

            getFactOptions();
          

        }]
    }
})