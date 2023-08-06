(function (app) {
    app.controller('homeController', homeController)
    homeController.$inject = ['$scope', 'apiService']
    function homeController($scope, apiService) {     
        $scope.getTestMethod = getTestMethod;
        function getTestMethod() {
            apiService.get('/api/home/TestMethod', null, function (result) {
                $scope.TestMethod = result.data;
            }, function () {
                console.log('TestMethod Error');
            });
        }
        $scope.getTestMethod();
    }
})(angular.module('donghoshop'));