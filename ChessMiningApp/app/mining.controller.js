(function () {

    angular.module('ChessMining').controller('MiningCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;

        $scope.submitDataFile = function () {
            var file = $("#fileInput")[0].files[0];
            console.log(file);
            var reader = new FileReader();
            reader.onloadend = function (event) {
                console.log(event.target);
               self.dataFile = event.target.result;
            }

            reader.onabort = function (event) {
                console.log("i am aborted!");
            }

            reader.readAsText(file);
        }

        $scope.getAssociationRules = function () {
            console.log($.parseJSON(self.dataFile));
            $http.post('/api/AssociationRules/Mine', JSON.parse(self.dataFile), { 'Content-Type': 'application / json' })
                .then(function (response) {
                    console.log(self.dataFile);
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