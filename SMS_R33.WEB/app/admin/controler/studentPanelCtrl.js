angular.module("schoolAdmin")
.controller("studentPanelCtrl", function ($scope, apiVerbSvc, classesUrl, studentsUrl) {
    apiVerbSvc.get(classesUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
     .then(function (result) {
         console.log("Students list");
         $scope.model.classes = result.data.value;
     }, function (response) {

     })
    apiVerbSvc.get(studentsUrl + "?$expand=Class", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        console.log("Students list");
        $scope.model.students = result.data.value;
        console.log(result.data);
    }, function (response) {

    })
    $scope.addStudent = function () {

        $scope.current = null;
        $scope.temp = null;
        $("#studentModal").modal("show");
    };
    $scope.getRoll = function () {
        console.log("Get ROll");
        apiVerbSvc.insert(studentsUrl + "/NextRoll", { "Authorization": "Bearer " + $scope.auth.accesstoken }, { ClassId: $scope.current.ClassId })
       .then(function (result) {

           //$scope.model.students.push(result.data);
           $scope.current.ClassRoll = result.data.value;
           console.log(result.data.value);

       }, function (response) {

       })
    }
    $scope.insertStudent = function () {
        console.log($scope.current);

        apiVerbSvc.insert(studentsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.current)
        .then(function (result) {

            $scope.model.students.push(result.data);
            $("#studentModal").modal("hide");
            $scope.current = null;
            $scope.temp = null;
            $scope.studentForm.$setPristine();

        }, function (response) {

        })

    }
    $scope.sEdit = function (student) {
        $scope.temp = student;
        $scope.current = { StudentId: student.StudentId, StudentName: student.StudentName, ClassId: student.ClassId, ClassRoll: student.ClassRoll }
        //$scope.admissionToDel = admission;
        $("#studentEditModal").modal("show");
    }

    //Update Student

    $scope.updateStudent = function () {
        apiVerbSvc.update(studentsUrl + "(" + $scope.current.StudentId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.current)
       .then(function (result) {

           var i = $scope.model.students.indexOf($scope.temp);
           $scope.model.students[i] = $scope.current;

           $("#studentEditModal").modal("hide");
           $scope.current = null;
           $scope.temp = null;

       }, function (response) {

       })

    }
    $scope.studentdelete = function (student) {
        $scope.current = student;
        //$scope.admissionToDel = admission;
        $("#studentDeleteModal").modal("show");
    }
    $scope.ConStudentDelete = function () {
        apiVerbSvc.remove(studentsUrl + "(" + $scope.current.StudentId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.students.indexOf($scope.current);
             $scope.model.students.splice(i, 1);
             $scope.current = null;
             //$scope.admissionToDel = admission;
             $("#studentDeleteModal").modal("hide");
         }, function (response) {

         })
    }

    //cancel....
    $scope.cancel = function () {
        $scope.current = null;
        $scope.temp = null;
        $("#studentModal").modal("hide");
        $("#studentEditModal").modal("hide");
        $("#studentDeleteModal").modal("hide");
        $scope.studentForm.$setPristine();
        //$scope.studentForm.$setValidity();
        //$scope.studentForm.$setUntouched();
        console.log($scope.current);
    }

    //sorting...

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