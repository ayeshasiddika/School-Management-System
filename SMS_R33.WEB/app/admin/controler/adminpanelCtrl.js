

angular.module("schoolAdmin")
   
.controller("adminCtrl", function ($scope, authStore, loginSvc, $location) {
    $scope.loginError = "";
    $scope.loginModel = {};
    $scope.model = {};
    $scope.selectedPage = 1;
    $scope.pageSize = 5;

    $scope.selectPage = function (newPage) {
        $scope.selectedPage = newPage;
        console.log(newPage);
        console.log($scope.selectedPage);
    }
    $scope.getPageClass = function (page) {

        return $scope.selectedPage == page ? "active" : "";
    }
    $scope.auth = authStore.get();
    // console.log($scope.auth);
    if ($scope.auth.authenticated)
        $location.path("/home");
    $scope.showLoginForm = function () {
        $("#loginModal").modal("show");
    }
    $scope.signin = function () {
        loginSvc.signin($scope.loginModel.username, $scope.loginModel.password)
        .then(function (result) {

            authStore.save(result.data.userName, result.data.access_token);
            $scope.auth = authStore.get();
            $scope.loginModel = null;
            $scope.loginError = "";
            $("#loginModal").modal("hide");
            $location.path("/home");
            //$window.location.href = "/app/admin/views/products.html";
        }, function (respose) {
            console.log(respose);
            $scope.loginError = respose.data.error_description;
        })
    }
    $scope.signout = function () {
        authStore.remove();
        $scope.auth = {};
        $location.path("/login");
    }

    $scope.homepage = function () {
        //$scope.current = null;
        //$scope.temp = null;
        $window.location.href = '/Home/Index#/';
});


