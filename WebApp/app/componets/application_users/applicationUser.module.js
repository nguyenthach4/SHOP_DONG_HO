/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('donghoshop.application_users', ['donghoshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('application_users', {
            url: "/application_users",
            templateUrl: "/app/componets/application_users/applicationUserListView.html",
            parent:'base',
            controller: "applicationUserListController"
        })
            .state('add_application_user', {
                url: "/add_application_user",
                parent: 'base',
                templateUrl: "/app/componets/application_users/applicationUserAddView.html",
                controller: "applicationUserAddController"
            })
            .state('edit_application_user', {
                url: "/edit_application_user/:id",
                templateUrl: "/app/componets/application_users/applicationUserEditiew.html",
                controller: "applicationUserEditController",
                parent: 'base',

            });
    }
})();