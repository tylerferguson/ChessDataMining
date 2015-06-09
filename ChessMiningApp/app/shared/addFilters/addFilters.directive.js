angular.module('ChessMining').directive("addFilters", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addFilters/add-filters.html',
        scope: {
            factStage: '@',
            buttonClicked: '='
        },
        controller: ['$scope', '$http', '$location', 'appService', 'closeMenuService', function ($scope, $http, $location, $appService, $closeMenuService) {

            var facts = [];
            var simpleFacts = [];
            $scope.$appService = $appService;
            $scope.$closeMenuService = $closeMenuService;

            $scope.fact = '';
            $scope.value = '';
            $scope.factOptions = {
                facts: []
                //key value pairs of individual facts
            };

            $closeMenuService.subscribe('displayArea', update);
            $closeMenuService.subscribe('navBar', update);

            getFactOptions();

            $scope.$watch('$appService.getDataFile()', function (newValue) {
                updateValidValueParams(newValue);
            })

            $scope.submitFact = function () {
                var fact = $scope.fact;
                var name;
                if (simpleFacts.indexOf(fact) > -1) {
                    name = fact;
                    fact = 'SimpleFact';
                } else {
                    fact = $scope.fact.concat('Fact');
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

            $scope.formClicked = function () {
                event.stopPropagation();
                $closeMenuService.publish('addFilterForm');
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
                                    $scope.factOptions[validNameParam].validValueParams = [];
                                })
                            }
                            else {
                                $scope.factOptions.facts.push(elem.FactType.replace('Fact', ''));
                                $scope.factOptions[elem.FactType.replace('Fact', '')] = {};
                                $scope.factOptions[elem.FactType.replace('Fact', '')].validValueParams = elem.ValidParams;
                            }
                        })
                    },
                    function (error) {
                        console.log("get error!");
                    });
            }
          

            function updateValidValueParams(dataFile) {
                var facts = simpleFacts.concat(['Opening']);
                facts.forEach(function (fact) {
                    dataFile && dataFile.data.forEach(function (game) {
                        if ($scope.factOptions[fact].validValueParams.indexOf(game[fact.replace('Fact', '')]) < 0 ) {
                            $scope.factOptions[fact].validValueParams.push(game[fact.replace('Fact', '')]);
                        } 
                    })
                })
            }

            function update() {
                $scope.buttonClicked = false;
            }

        }]
    }
})