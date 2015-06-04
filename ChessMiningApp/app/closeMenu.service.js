angular.module('ChessMining').factory('closeMenuService', function () {

    //var sideNavShown;
    var subscribers = {
        //topic: [] //subscribers
    };

    return {
        subscribe: subscribe,
        publish: publish
        //closeSideNav: closeSideNav,
        //showSideNav: showSideNav,
        //getSideNavShownStatus: getSideNavShownStatus
    }

    function subscribe(topic, subscriber) {
        subscribers[topic] = subscribers[topic] || []; //if topic not present make empty subscribers array
        subscribers[topic].push(subscriber);
    }
     
    function publish(topic, showStatus) {
        subscribers[topic].forEach(function(subscriber) {
            subscriber(topic, showStatus);
        })
    }

//    function closeSideNav() {
//        sideNavShown = false;
//    }

//    function showSideNav() {
//        sideNavShown = true;
//    }

//    function getSideNavShownStatus() {
//        return sideNavShown;
//    }
})