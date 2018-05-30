angular.module("myApp")
.controller("teacherCtrl", function ($scope, apiVerbSvc, teachersUrl) {
    apiVerbSvc.get(teachersUrl, null, null)
        .then(function (result) {
            $scope.model.teachers = result.data.value;
            console.log(result.data.value);
            console.log("teachers");

        }, function (response) { });
});