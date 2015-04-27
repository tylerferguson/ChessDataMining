angular.module('ChessMining').directive("addFilterActionButton", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addFilterActionButton/add-filter-action-button-partial.html',
        scope: {
            facts: '=filters',
            updateFilters: '&'
        },
        controller: ['$scope', '$http', function ($scope, $http) {

            $scope.fact = '';
            $scope.name = '';
            $scope.value = '';
            $scope.actionButtonClicked;
            $scope.factOptions = {
                facts: []
                //key value pairs of individual facts
            };

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

            $scope.submitFact = function () {
                var fact = {
                    type: $scope.fact,
                    name: $scope.name,
                    value: $scope.value
                };

                $scope.facts.push(fact);
                $scope.fact = '';
                $scope.name = '';
                $scope.value = '';
                $scope.updateFilters();
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