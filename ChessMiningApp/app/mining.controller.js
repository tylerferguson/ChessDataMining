(function () {

    angular.module('ChessMining').controller('MiningCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;


        $scope.submitDataFile = function (event) {
            event.preventDefault();
            var file = $("#fileInput")[0].files[0];
            var reader = new FileReader();

            reader.onload = function (event) {
                $scope.$apply(function () {
                    $scope.dataFile = event.target.result;
                });   
            }

            reader.onerror = function () {
                console.log("here we are!");
                //$scope.dataFile = undefined;       
            }

            if (file) {
                reader.readAsText(file);
            } else {
                $scope.dataFile = undefined;
            } 
        }

        $scope.getAssociationRules = function () {
            console.log($.parseJSON($scope.dataFile));
            $http.post('/api/AssociationRules/Mine', JSON.parse($scope.dataFile), { 'Content-Type': 'application / json' })
                .then(function (response) {
                    console.log($scope.dataFile);
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

})();