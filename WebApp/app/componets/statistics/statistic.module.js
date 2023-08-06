/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('donghoshop.statistics', ['donghoshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('statistic_revenuse', {
                url: "/statistic_revenuse",
                parent: 'base',
                templateUrl: "/app/componets/statistics/revenueStatisticView.html",
                controller: "revenueStatisticController"
            });
    }
})();