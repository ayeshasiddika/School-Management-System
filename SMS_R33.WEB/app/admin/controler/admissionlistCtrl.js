angular.module("schoolAdmin")
.controller("admissionlistCtrl", function ($scope, apiVerbSvc, admissionsUrl) {
    $scope.admissionToDel = null;
    apiVerbSvc.get(admissionsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.model.admissions = result.data.value;
    }, function (response) {

    })
    $scope.admissiondelete = function (admission) {
        $scope.current = admission;
        //$scope.admissionToDel = admission;
        $("#admissionDeleteModal").modal("show");
    }
    $scope.ConAdmission = function () {
        apiVerbSvc.remove(admissionsUrl+"(" + $scope.current.AdmissionId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.admissions.indexOf($scope.current);
             $scope.model.admissions.splice(i, 1);
             $scope.current = null;
             //$scope.admissionToDel = admission;
             $("#admissionDeleteModal").modal("hide");
          }, function (response) {

             })
       
    }
    
    $scope.sortColumn = 'Name';
    $scope.reverseSort = false;
    $scope.sortData = function (column) {
        $scope.sortColumn = column;
        $scope.reverseSort = ($scope.sortColumn == column) ?
        !$scope.reverseSort : false;
    }
    $scope.getClass = function (column) {
        if ($scope.sortColumn == column) {
            return $scope.reverseSort ? "arrow-down" : "arrow-up";
        }
        return '';
    }
    
});