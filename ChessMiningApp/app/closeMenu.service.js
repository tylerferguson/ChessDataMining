angular.module('ChessMining').factory('closeMenuService', function () {

    var subscribers = {
        //<topic>: [<subscribers>] 
    };

    return {
        subscribe: subscribe,
        publish: publish
    }

    function subscribe(topic, subscriber) {
        subscribers[topic] = subscribers[topic] || []; //if topic not present make empty subscribers array
        subscribers[topic].push(subscriber);
    }
     
    function publish(topic) {
        subscribers[topic].forEach(function(subscriber) {
            subscriber(topic);
        })
    }
})