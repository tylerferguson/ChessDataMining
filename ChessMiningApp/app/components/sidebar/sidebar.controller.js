angular.module('ChessMining').controller('SidebarCtrl', ['$scope', 'appService', function($scope, appService) {

    $scope.service = appService;
}])