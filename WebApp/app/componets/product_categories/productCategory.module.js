/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('donghoshop.product_categories', ['donghoshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: "/product_categories",
            templateUrl: "/app/componets/product_categories/productCategoryListView.html",  
            parent: 'base',
            controller: "productCategoryListController"
        })
            .state('add_product_categories', {
                url: "/add_product_categories",
                parent: 'base',
                templateUrl: "/app/componets/product_categories/productCategoryAddView.html",
                controller: "productCategoryAddController"
            })
            .state('edit_product_categories', {
                url: "/edit_product_categories/:id",
                templateUrl: "/app/componets/product_categories/productCategoryEditView.html",
                controller: "productCategoryEditController",
                parent: 'base',

            });
    }
})();