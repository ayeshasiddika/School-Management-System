angular.module("myApp")
.controller("admissionCtrl", function ($scope, apiVerbSvc, $window, admissionsUrl) {
    $scope.pan2 = false;
    $scope.pan1 = true;
    $scope.apply = function () {
        $scope.pan2 = true;
        $scope.pan1 = false;
    }
    $scope.save = function () {
        console.log($scope.current);
        apiVerbSvc.insert(admissionsUrl, null, $scope.current)
      .then(function (result) {
          $scope.model.admissions = result.data.value;
          $scope.pan1 = true;
          $scope.current = null;
          $scope.temp = null;
          $window.location.href = '/Home/Index#/';
          console.log("then function is successed")
      }, function (response) {
      })
    }
    $scope.cancelF = function () {
        $scope.current = null;
        $scope.temp = null;
        $window.location.href = '/Home/Index#/';
    }
});