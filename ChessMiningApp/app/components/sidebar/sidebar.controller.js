angular.module('ChessMining').controller('SidebarCtrl', ['$scope', 'appService', '$http', function($scope, $appService, $http) {

    $scope.service = $appService;

    var rules = [];

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
                rules.length = 0;
                response.data.forEach(function (rule) {
                    rules.push(rule);
                })
                $appService.updateResults(rules);
            },
            function (error) {
                console.log('error!');
            });
    }

    $scope.showInputForm = function () {
        $scope.addFilterButtonClicked = true;
    }
}])