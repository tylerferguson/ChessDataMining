angular.module('ChessMining').directive('addMining', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/addMining/add-mining.html',
        controller: ['$scope', '$http', 'appService', function ($scope, $http, $appService) {

            $scope.getAssociationRules = function () {

                var dataTransferObject = {
                    games: $appService.getDataFile().data,
                    minsup: $scope.minsup,
                    minconf: $scope.minconf,
                    projectionFacts: $appService.getFilters(),
                    targetFacts: $appService.getTargets()
                };

                $http.post('/api/AssociationRules/Mine', dataTransferObject, { 'Content-Type': 'application / json' })
                    .then(function (response) {
                        $scope.rules.length = 0;
                        response.data.forEach(function (rule) {
                            $scope.rules.push(rule);
                        })
                        $appService.updateResults($scope.rules);
                    },
                    function (error) {
                        console.log('error!');
                    });
            }
        }]
    }
})