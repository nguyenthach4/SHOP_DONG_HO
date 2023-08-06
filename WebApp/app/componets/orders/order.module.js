/// <reference path="../../../assets/admin/libs/angular/angular.js" />


(function () {

    angular.module('donghoshop.orders', ['donghoshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('orders', {
                url: "/orders",
                parent: 'base',
                templateUrl: "/app/componets/orders/orderListView1.html",
                controller: "orderListController"
            })
            .state('add_order', {
                url: "/add_order",
                parent: 'base',
                templateUrl: "/app/componets/orders/orderAddView.html",
                controller: "orderAddController"
            });
          
    }

})();