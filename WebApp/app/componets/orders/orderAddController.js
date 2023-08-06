(function (app) {

    app.controller('orderAddController', orderAddController)

    orderAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function orderAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.order = {
            CreatedDate: new Date(),
            Status: true
        }

    }
    $scope.AddOrder = AddOrder;

    function AddOrder() {
        apiService.post('/api/order/create', $scope.order, function (result) {
            notificationService.displaySuccess(result.data.Name + 'đã dược thêm mới !');
            $state.go('orders');
        }, function (error) {
            notificationService.displayError('Thêm mới không thành công !');
        })
    }
    function loadProducts() {
        apiService.get('/api/product/getall', null, function (result) {
            $scope.Prducts = result.data;
        }, function (error) {
            console.log('Cannot get list parent !');
        })
    }

    loadProducts();

}) (angular.module('donghoshop.products'))