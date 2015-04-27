angular.module('ChessMining').controller('ToolbarCtrl', ['$scope', function ($scope) {

    $scope.stages = ['Data', 'Filter', 'Target', 'Mine'];

    $scope.selectStage = function (index) {
        $scope.stageSelected = index;
    }
}])