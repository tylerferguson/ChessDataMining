(function () {
    angular.module('ChessMining')
        .factory('appService', appService);

    function appService() {
        return {
            updateDataFile: updateDataFile,
            updateFilters: updateFilters,
            updateTargets: updateTargets,
            updateResults: updateResults,
            getDataFile: getDataFile,
            getFilters: getFilters,
            getTargets: getTargets,
            getResults: getResults
        }

        var dataFile;
        var filters;
        var targets;
        var results;

        function updateDataFile(newDataFile) {
            dataFile = newDataFile;
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
        
        function getDataFile() {
            return dataFile;
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