angular.module("schoolAdmin")
.controller("teacherpanelCtrl", function ($scope, apiVerbSvc, teachersUrl) {
    var fileSelect, fileSelectEdit;
    apiVerbSvc.get(teachersUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            $scope.model.teachers = result.data.value;
            console.log(result.data.value);
            console.log("teachers");

        }, function (response) { });

    $scope.addTeacher = function () {
        
        $scope.current = null;
        $scope.temp = null;
        $("#TeacherAddModal").modal("show");
    };
    $scope.insertTeacher = function () {
        console.log($scope.current);

        apiVerbSvc.insert(teachersUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.current)
        .then(function (result) {

            $scope.model.teachers.push(result.data);
            $("#TeacherAddModal").modal("hide");
            $scope.current = null;
            $scope.temp = null;
            $scope.teacherForm.$setPristine();
            //$scope.teacherForm.$setValidity();
            //$scope.teacherForm.$setUntouched();

        }, function (response) {

        })
    }
    $scope.tEdit = function (teacher) {
        $scope.temp = teacher;
        $scope.current = { TeacherId: teacher.TeacherId, TeacherName: teacher.TeacherName, Designation: teacher.Designation, JoiningDate: teacher.JoiningDate, EcademicQualification: teacher.EcademicQualification, CellNo: teacher.CellNo, Photo: teacher.Photo }
        $("#TeacherEditModal").modal("show");
    }
    $scope.editTeacher = function () {
        apiVerbSvc.update(teachersUrl + "(" + $scope.current.TeacherId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.current)
       .then(function (result) {

           var i = $scope.model.teachers.indexOf($scope.temp);
           $scope.model.teachers[i] = $scope.current;

           $("#TeacherEditModal").modal("hide");
           $scope.current = null;
           $scope.temp = null;

       }, function (response) {

       })
    }

    $scope.tDelete = function (teacher) {
        $scope.current = teacher;
        //$scope.admissionToDel = admission;
        $("#teacherDeleteModal").modal("show");
    }
    $scope.ConteacherDelete = function () {
        apiVerbSvc.remove(teachersUrl + "(" + $scope.current.TeacherId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.teachers.indexOf($scope.current);
             $scope.model.teachers.splice(i, 1);
             $scope.current = null;
             //$scope.admissionToDel = admission;
             $("#teacherDeleteModal").modal("hide");
         }, function (response) {

         })
    }
    //Photo
    $scope.photoClick = function () {

        fileSelect = document.createElement('input'); //input it's not displayed in html, I want to trigger it form other elements
        fileSelect.type = 'file';

        if (fileSelect.disabled) { //check if browser support input type='file' and stop execution of controller
            return;
        }
        //console.log("new pic");
        if (fileSelect) { //activate function to begin input file on click
            fileSelect.click();
        }

        fileSelect.onchange = function () { //set callback to action after choosing file
            var f = fileSelect.files[0],
              r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.current.Photo = e.target.result;
                $scope.$apply();
               
                $("#newPhoto").attr("src", $scope.current.Photo)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    //Edit Photo
    $scope.editPhotoClick = function () {
        fileSelectEdit = document.createElement("input");// same as insert
        fileSelectEdit.type = 'file';
        if (fileSelectEdit.disabled) {
            return;
        }
        if (fileSelectEdit) {
            fileSelectEdit.click();
        }
        fileSelectEdit.onchange = function () { //set callback to action after choosing file
            var f = fileSelectEdit.files[0],
            r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.current.Photo = e.target.result;
                $scope.$apply();
                //console.log($scope.model.currentProduct.Picture.replace(/^data:image\/(png|jpg);base64,/, "")); //replace regex if you want to rip off the base 64 "header"
                $("#newPhoto").attr("src", $scope.current.Photo)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    //cancel....
    $scope.cancel = function () {
        $scope.current = null;
        $scope.temp = null;
        $("#TeacherAddModal").modal("hide");
        $("#TeacherEditModal").modal("hide");
        $("#teacherDeleteModal").modal("hide");
        $scope.teacherForm.$setPristine();
        //$scope.teacherForm.$setValidity();
        //$scope.teacherForm.$setUntouched();
        console.log($scope.current);
    }
});