(function () {

    angular.module('ChessMining').controller('MiningCtrl', ['$scope', '$http', function ($scope, $http) {

        var self = this;
        var projectionFacts = [];
        $scope.optionClicked = [];

        $(document).ready(function () {
            $http.get('/api/AssociationRules', { 'Content-Type': 'application / json' })
                .then(function (response) {
                    $scope.factOptions = response.data;
                },
                function (error) {
                    console.log("get error!");
                });
        });

        $scope.stopPropagation = function(event) {
            event.stopPropagation();
        }

        $scope.toggleShowForm = function (index) {
            $scope.optionClicked[index] = $scope.optionClicked[index] ? false : true;
            console.log($scope.optionClicked);
            
        }

        $scope.submitProjectionFact = function (factOption) {
            var fact = {
                type: factOption,
                name: $scope.name,
                value: $scope.value
            };

            projectionFacts.push(fact);
            $scope.name = '';
            $scope.value = '';

            //var indexOfFact = indexOf(fact, projectionFacts);

            //if (indexOfFact > -1) {
            //    projectionFacts.splice(indexOfFact, 1);
            //} else {
            //    projectionFacts.push(fact);
            //}

            //function indexOf(fact, arr) {
            //    for (var index = 0; index < arr.length; index++) {
            //        var candidateFact = arr[index];

            //        if (candidateFact.type === fact.type && candidateFact.name === fact.name && candidateFact.value === fact.value) {
            //            return index;
            //        }
            //        return -1;
            //    }
            //}
        }

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
                $scope.$apply(function () {
                    $scope.dataFile = undefined;
                });
            }

            if (file) {
                reader.readAsText(file);
            } else {
                $scope.dataFile = undefined;
            } 
        }

        $scope.getAssociationRules = function () {

            //var projectionFactsDto = [
            //    {
            //        type: 'SimpleFact',
            //        name: 'White',
            //        value: 'tailuge'
            //    }
            //];

            var targetFactsDto = [
                {
                    type: 'SimpleFact',
                    name: 'Result',
                    value: '1-0'
                }
            ];

            var dataTransferObject = {
                games: JSON.parse($scope.dataFile),
                minsup: $scope.minsup,
                minconf: $scope.minconf,
                projectionFacts: projectionFacts,
                targetFacts: targetFactsDto
            };

            $http.post('/api/AssociationRules/Mine', dataTransferObject, { 'Content-Type': 'application / json' })
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