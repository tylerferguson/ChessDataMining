angular.module('ChessMining').factory('closeMenuService', function () {
    return {
        closeSideNav: closeSideNav,
        showSideNav: showSideNav,
        getSideNavShownStatus: getSideNavShownStatus
    }

    var sideNavShown;

    function closeSideNav() {
        sideNavShown = false;
    }

    function showSideNav() {
        sideNavShown = true;
    }

    function getSideNavShownStatus() {
        return sideNavShown;
    }
})