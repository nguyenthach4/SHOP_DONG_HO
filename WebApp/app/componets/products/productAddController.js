﻿(function (app) {

    app.controller('productAddController', productAddController)

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true

        }

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px',
        }
        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + 'đã dược thêm mới !');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công !');
            })
        }
        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.PrductCategories = result.data;
            }, function (error) {
                console.log('Cannot get list parent !');
            })
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
               
            }
            finder.popup();
        }
        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
               
            }
            finder.popup();
        }
        loadProductCategories();
    }
})(angular.module('donghoshop.products'))