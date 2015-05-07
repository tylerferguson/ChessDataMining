angular.module('ChessMining').directive("addFilters", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addFilters/add-filters.html',
        scope: {
            factStage: '@',
            buttonClicked: '='
        },
        controller: ['$scope', '$http', 'appService', function ($scope, $http, $appService) {

            var facts = [];
            $scope.fact = '';
            $scope.name = '';
            $scope.value = '';
            $scope.factOptions = {
                facts: []
                //key value pairs of individual facts
            };

            $scope.closeInputForm = function () {
                setTimeout(function () {
                    var focus = $(document.activeElement);
                    if (focus.is(".add-filter-form") || $('.add-filter-form').has(focus).length) {
                        console.log("still focused");
                    } else {
                        $scope.$apply(function () {
                            $scope.buttonClicked = false;
                        })
                    }
                }, 0);
            }

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

                function updateFilters() {
                    $appService['update' + $scope.factStage](facts);
                }
            }

            function getFactOptions() {
                
                $http.get('/api/AssociationRules', { 'Content-Type': 'application / json' })
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