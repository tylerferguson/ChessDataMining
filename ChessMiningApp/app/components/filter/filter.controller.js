angular.module('ChessMining').controller('FilterCtrl', ['$scope', '$http', '$location', 'appService', function ($scope, $http, $location, appService) {

    var self = this;
    var factStage = $location.path().replace("/", "") + 's';

    $scope.facts = appService['get' + factStage]() || [];

    $scope.updateFilters = function () {
        appService['update' + factStage]($scope.facts);
    }
}]);