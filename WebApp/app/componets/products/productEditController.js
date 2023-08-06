(function (app) {

    app.controller('productEditController', productEditController)

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams']

    function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.product = {};

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px',
        }
        $scope.UpdateProduct = UpdateProduct;
   //     $scope.moreImages = [];
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                console.log(result.data);
                $scope.product = result.data;
                if ($scope.product.MoreImages == "null") {
                    $scope.moreImages = [];
                }
                else
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            });

        }
        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã dược cập nhật !');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công !');
            })
        }

        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.PrductCategories = result.data;
            }, function (error) {
                console.log('Cannot get lists parent !');
            })
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl
                })

            }
            finder.popup();
        }
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
        loadProductDetail();
    }
})(angular.module('donghoshop.products'))