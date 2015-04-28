(function () {
    angular.module('ChessMining')
        .factory('appService', appService);

    function appService() {
        return {
            updateData: updateData,
            updateFilters: updateFilters,
            updateTargets: updateTargets,
            updateResults: updateResults,
            getData: getData,
            getFilters: getFilters,
            getTargets: getTargets,
            getResults: getResults
        }

        var data;
        var filters;
        var targets;
        var results;

        function updateData(newData) {
            data = newData;
        }

        function updateFilters(newFilters) {
            filters = newFilters;
        }

        function updateTargets(newTargets) {
            targets = newTargets;
        }

        function updateResults(newResults) {
            results = newResults;
        }
        
        function getData() {
            return data;
        }
        
        function getFilters() {
            return filters;
        }

        function getTargets() {
            return targets;
        }

        function getResults() {
            return results;
        }


    }

})()