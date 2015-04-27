angular.module('ChessMining').controller('MineCtrl', ['$scope', 'appService', '$http', function ($scope, appService, $http) {

    $scope.getAssociationRules = function () {

        var dataTransferObject = {
            games: JSON.parse(appService.getData()),
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
            },
            function (error) {
                console.log('error!');
            });
    }
}]);