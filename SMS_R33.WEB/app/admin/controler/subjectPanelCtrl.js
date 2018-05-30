angular.module("schoolAdmin")
.controller("subjectPanelCtrl", function ($scope, apiVerbSvc, subjectsUrl) {
    apiVerbSvc.get(subjectsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
     .then(function (result) {
         console.log("Subject list");
         $scope.model.subjects = result.data.value;
     }, function (response) {

     })
    $scope.addSub = function () {
        $scope.targetsub = false;
        $scope.temp = null;
       
        for(var i=0; i< $scope.model.subjects.length; i++)
        {
            console.log("enter for loop");
            if ($scope.model.subjects[i].SubjectName == $scope.current.SubjectName)
            {
                $scope.targetsub = true;
            }
        }
        console.log("target sub is " + $scope.targetsub);
        if ($scope.targetsub == true) {
            $("#suberr").css("visibility", "initial");
            $("#suberr").html("Subject is Exist!");
            console.log("Subject is Exist");
        }
        else {
            $("#suberr").css("visibility", "hidden");
            $scope.targetsub = false;
            apiVerbSvc.insert(subjectsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.current)
       .then(function (result) {

           $scope.model.subjects.push(result.data);
           $scope.current = null;
           $scope.temp = null;
       }, function (response) {

       });

        }
       
    }

    //Edit
    $scope.update = null;

    $scope.subEdit = function (subject) {
        $scope.temp = subject;
        $scope.update = { SubjectId: subject.SubjectId, SubjectName: subject.SubjectName }
        
        $("#subEditModal").modal("show");
    }
    $scope.updateSubject = function () {
        apiVerbSvc.update(subjectsUrl + "(" + $scope.update.SubjectId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.update)
       .then(function (result) {

           var i = $scope.model.subjects.indexOf($scope.temp);
           $scope.model.subjects[i] = $scope.update;

           $("#subEditModal").modal("hide");
           $scope.update = null;
           $scope.temp = null;

       }, function (response) {

       });

    }

    //DeleteResult

    $scope.subDelete = function (subject) {
        $scope.current = subject;
        //$scope.admissionToDel = admission;
        $("#subDeleteModal").modal("show");
    }
    $scope.ConSubjectDelete = function () {
        apiVerbSvc.remove(subjectsUrl + "(" + $scope.current.SubjectId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.subjects.indexOf($scope.current);

             $scope.model.subjects.splice(i, 1);
             $scope.current = null;
             //$scope.admissionToDel = admission;
             $("#subDeleteModal").modal("hide");
         }, function (response) {

         })
    }

    $scope.ucancel = function () {
        $scope.current = null;
        $scope.temp = null;
        $("#subEditModal").modal("hide");
        //$("#studentEditModal").modal("hide");
        $("#subDeleteModal").modal("hide");
        $scope.subtEditForm.$setPristine();
        $scope.subtEditForm.$setValidity();
        $scope.subtEditForm.$setUntouched();
        console.log($scope.current);
    }
    $scope.dcancel = function () {
        $scope.current = null;
        $scope.temp = null;
        $("#subEditModal").modal("hide");
        //$("#studentEditModal").modal("hide");
        $("#subDeleteModal").modal("hide");
        //$scope.subtEditForm.$setPristine();
        //$scope.subtEditForm.$setValidity();
        //$scope.subtEditForm.$setUntouched();
        console.log($scope.current);
    }
    
});