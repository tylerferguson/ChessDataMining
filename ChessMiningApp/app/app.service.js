(function () {
    angular.module('ChessMining')
        .factory('appService', appService);

    function appService() {
        return {
            updateData: updateData,
            updateFilters: updateFilters,
            upateTargets: updateTargets,
            getData: getData,
            getFilters: getFilters,
            getTargets: getTargets
        }

        var data;
        var filters;
        var targets;

        function updateData(newData) {
            data = newData;
        }

        function updateFilters(newFilters) {
            filters = newFilters;
        }

        function updateTargets(newTargets) {
            targets = newTargets;
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


    }

})()