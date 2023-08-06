(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService', 'apiService'];

    function rootController($state, authData, loginService, $scope, authenticationService, apiService) {

        $scope.logOut = function () {
            loginService.logOut();
            //   authenticationService.setHeader();
            $state.go('login');

        }
      //  authenticationService.validateRequest();
        $scope.authentication = authData.authenticationData;
        //    $scope.sideBar = "/app/shared/views/sideBar.html";
       
    }

})(angular.module('donghoshop'));