angular.module('ChessMining').controller('MineCtrl', ['$scope', 'appService', '$http', function ($scope, appService, $http) {

    $scope.rules = appService.getResults() || [];

    $scope.getAssociationRules = function () {

        var dataTransferObject = {
            games: appService.getData(),
            minsup: $scope.minsup,
            minconf: $scope.minconf,
            projectionFacts: appService.getFilters(),
            targetFacts: appService.getTargets()
        };

        $http.post('/api/AssociationRules/Mine', dataTransferObject, { 'Content-Type': 'application / json' })
            .then(function (response) {
                $scope.rules = [];
                response.data.forEach(function (rule) {
                    $scope.rules.push(rule);
                })
                appService.updateResults($scope.rules);
            },
            function (error) {
                console.log('error!');
            });
    }
}]);