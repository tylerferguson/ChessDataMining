angular.module('ChessMining').controller('SidebarCtrl', ['$scope', '$location', '$http', 'appService', 'closeMenuService', function ($scope, $location, $http, $appService, $closeMenuService) {

    $scope.$service = $appService;
    $scope.$closeMenuService = $closeMenuService;
    //$scope.sideNavShown = true;
    

    var rules = [];

    $scope.getAssociationRules = function () {

        var dataTransferObject = {
            games: $appService.getDataFile().data,
            minsup: $scope.minsup,
            minconf: $scope.minconf,
            projectionFacts: $appService.getFilters(),
            targetFacts: $appService.getTargets()
        };

        $http.post($location.$$absUrl.replace('#/', 'api/AssociationRules/Mine'), dataTransferObject, { 'Content-Type': 'application / json' })
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
}])