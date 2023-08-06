(function (app) {

    app.controller('orderListController', orderListController)

    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getOrders = getOrders;
        $scope.keyword = '';
    
       
        $scope.search = search;

        $scope.deleteOrder = deleteOrder;
        $scope.selectAll = selectAll;
        $scope.deleteMutiple = deleteMutiple;

        function search() {
            getOrders();
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.orders, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.orders, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        $scope.$watch('orders', function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.seleted = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }

        }, true)
        function deleteOrder(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
              //  console.log(id);
                apiService.del('/api/order/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công !');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công !');
                });
            });

        }
        function deleteMutiple() {
            var listId = [];
            $.each($scope.seleted, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    checkedOrders: JSON.stringify(listId)
                }
            }
            apiService.del('/api/order/deletemulti', config, function (rerult) {
                notificationService.displaySuccess('Xóa thành công ' + rerult.data + ' bản ghi !');
                search();
            }, function (eror) {
                notificationService.displayError('Xóa không thành công !');
            });
        }
        function getOrders(page) {
            page = page || 0;
            var config = {

                params: {
                    orderid: $scope.orderid || 0,
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            apiService.get('/api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy !');
                }
                $scope.orders = result.data.Items;
                console.log(result.data.Items);
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Get orders failed');
            })
        }
        $scope.getOrders();
    }
})(angular.module('donghoshop.orders'));