(function (app) {

    app.controller('productCategoryListController', productCategoryListController)

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.getProductCategories = getProductCategories;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteProductCategory = deleteProductCategory;


        $scope.selectAll = selectAll;
        $scope.deleteMutiple = deleteMutiple;

        function deleteMutiple() {
            var listId = [];
            $.each($scope.seleted, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemulti', config, function (serult) {
                notificationService.displaySuccess('Xóa thành công ' + serult.data + ' bản ghi !');
                search();
            }, function (eror) {
                    notificationService.displayError('Xóa không thành công !');
            });
        }
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }
        $scope.$watch('productCategories', function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.seleted = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }

        }, true)
        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công !');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công !');
                });
            });

        }
        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy !');
                }

                $scope.productCategories = result.data.Items;
                console.log(result.data.Items);
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                console.log('Load Product Categories falied');
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('donghoshop.product_categories'));