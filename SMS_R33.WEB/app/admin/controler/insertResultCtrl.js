angular.module("schoolAdmin")
.controller("insertResultCtrl", function ($scope, apiVerbSvc, studentsUrl, examYearsUrl, examsUrl, classesUrl, subjectsUrl, examTermsUrl, resultsUrl) {
    $scope.model.current = null;
    $scope.model.temp = {};
    $scope.model.target = {};
    $scope.model.targetresults = [];

    apiVerbSvc.get(studentsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
   .then(function (result) {
       $scope.model.students = result.data.value;
       console.log(result.data.value);
       console.log("exam Year");

   }, function (response) { });

    apiVerbSvc.get(examYearsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.model.examyears = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(examsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.model.exams = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(classesUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        console.log("Class list");
        $scope.model.classes = result.data.value;
    }, function (response) {

    });

    apiVerbSvc.get(subjectsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
  .then(function (result) {
      console.log("Class list");
      $scope.model.subjects = result.data.value;
  }, function (response) {

  });

    apiVerbSvc.get(examTermsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
       .then(function (result) {
           console.log("exam Term");
           $scope.model.examterms = result.data.value;
       }, function (response) {

       });
    apiVerbSvc.get(resultsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
   .then(function (result) {
       console.log("exam Term");
       $scope.model.results = result.data.value;
   }, function (response) {

   });

    //GET STUDENT OF TARGET CLASS
    $scope.insertselectedId = function () {
        $scope.model.current = null;
        $scope.model.target = {};
        $scope.model.targetresults = [];
        $("#resulterr").css("visibility", "hidden");
        $("#addresult").css("visibility", "hidden");
        //$scope.model.targetresults = [];
        apiVerbSvc.get(studentsUrl + "?$filter=ClassId eq " + $scope.model.temp.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
     .then(function (result) {
         
         $scope.model.target.students = result.data.value;
         console.log(result.data.value);
     }, function (response) {

     });
    }
    //GET TARGET EXAM YEAR
    $scope.selectedSId = function () {
        $scope.model.target.examyears = [];
        $scope.model.target.results = [];
        $scope.model.targetresults = [];
        $("#resulterr").css("visibility", "hidden");
        $("#addresult").css("visibility", "hidden");

        apiVerbSvc.get(examsUrl + "?$filter=ClassId eq " + $scope.model.temp.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
       
        $scope.model.tempExams = result.data.value;
        

        for (var yi = 0; yi < $scope.model.examyears.length; yi++) {
            for (var ei = 0; ei < $scope.model.tempExams.length; ei++) {
                if ($scope.model.examyears[yi].ExamYearId == $scope.model.tempExams[ei].ExamYearId) $scope.model.target.examyears.push($scope.model.examyears[yi]);
            }
        }

    }, function (response) { });

    }
    //GET TARGET EXAM TERM
    $scope.selectedEYear = function () {
        $scope.model.current.ExamTermId = null;
        $scope.model.target.examterms = [];
        $scope.model.target.subjects = [];
        $scope.model.target.results = [];
        $scope.model.targetresults = [];
        $("#resulterr").css("visibility", "hidden");
        $("#addresult").css("visibility", "hidden");

        for (var ti = 0; ti < $scope.model.examterms.length; ti++) {
            for (var ei = 0; ei < $scope.model.tempExams.length; ei++) {
                if ($scope.model.examterms[ti].ExamTermId == $scope.model.tempExams[ei].ExamTermId) $scope.model.target.examterms.push($scope.model.examterms[ti]);
            }
        }
       
    }
    //GET TARGET SUBJECT
    $scope.getEIforSub = function () {
        $scope.model.target.subjects = [];
        $scope.model.temp.subjects = [];
        $scope.model.targetresults = [];
        $("#resulterr").css("visibility", "hidden");
        $("#addresult").css("visibility", "hidden");

        apiVerbSvc.get(examsUrl + "?$filter=ExamYearId eq " + $scope.model.current.ExamYearId + " and ExamTermId eq " + $scope.model.current.ExamTermId + " and ClassId eq " + $scope.model.temp.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
          .then(function (result) {
            
              $scope.model.examsforSub = result.data.value;
              console.log("exam");
              console.log($scope.model.examsforSub);

              for (var si = 0; si < $scope.model.subjects.length; si++) {
                  for (var ei = 0; ei < $scope.model.examsforSub.length; ei++) {
                      if ($scope.model.subjects[si].SubjectId == $scope.model.examsforSub[ei].SubjectId) $scope.model.target.subjects.push($scope.model.subjects[si]);
                  }
                 
              }
      
          }, function (response) { });
        //TARGET RESULT
        apiVerbSvc.get(resultsUrl + "?$filter=ExamYearId eq " + $scope.model.current.ExamYearId + " and ExamTermId eq " + $scope.model.current.ExamTermId + " and StudentId eq " + $scope.model.current.StudentId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {

             $scope.model.target.results = result.data.value;
             console.log("target results");
             console.log($scope.model.target.results);

         }, function (response) { });

        

    }
    //GET EXAM ID TARGET OPTIONS
    $scope.getExamId = function () {
        apiVerbSvc.get(examsUrl + "?$filter=ExamYearId eq " + $scope.model.current.ExamYearId + " and ExamTermId eq " + $scope.model.current.ExamTermId + " and SubjectId eq " + $scope.model.current.SubjectId + " and ClassId eq " + $scope.model.temp.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
            .then(function (result) {  
         $scope.model.current.ExamId = result.data.value[0].ExamId;

                apiVerbSvc.get(resultsUrl + "?$filter=ExamYearId eq " + $scope.model.current.ExamYearId + " and ExamTermId eq " + $scope.model.current.ExamTermId + " and StudentId eq " + $scope.model.current.StudentId + " and SubjectId eq " + $scope.model.current.SubjectId + " and ExamId eq " + $scope.model.current.ExamId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
            .then(function (result) {
                $scope.model.tempResults = result.data.value;
                console.log("tem result")
                console.log($scope.model.tempResults)
                if ($scope.model.tempResults.length>=1) {
                    console.log("value null")
                    $("#resulterr").css("visibility", "initial");
                    $("#addresult").css("visibility", "hidden");
                }
                else {
                    $("#resulterr").css("visibility", "hidden");
                    $("#addresult").css("visibility", "initial");
                }
            }, function (response) { });

        //


    }, function (response) {

    });
        
    }
    //ADD RESULT
    $scope.addResult = function () {
       
        apiVerbSvc.insert(resultsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.current)
         .then(function (result) {
             $scope.model.targetresults.push(result.data);
             $scope.model.target.results.push(result.data);

             $scope.model.current.SubjectId = null;
             $scope.model.current.Mark = null;

             $scope.markForm.$setPristine();
             $scope.markForm.$setValidity();
             $scope.markForm.$setUntouched();
         }, function (response) { });
    }
    //BTN Add New Student
    $scope.addNewStudent = function () {
        $scope.model.current.StudentId = null;
        $scope.model.current.Mark = null;
        $scope.model.target.results = [];
        $scope.model.targetresults = [];
        
        $("#addresult").css("visibility", "hidden");
        $("#resulterr").css("visibility", "hidden");

    }
    $scope.reFresh = function () {   
        $scope.model.current = null;
        $scope.model.temp = {};
        $scope.model.target = {};
        $scope.model.targetresults = [];
        $("#resulterr").css("visibility", "hidden");
        $("#addresult").css("visibility", "hidden");
        console.log("refresh");   
    }


    //UpdateResult


    $scope.update = null;
    $scope.temp = null;

    $scope.resEdit = function (result) {
        
        $scope.temp = result;
        $scope.update = { ExamId: result.ExamId, SubjectId: result.SubjectId, ExamYearId: result.ExamYearId, ExamTermId: result.ExamTermId, ResultId: result.ResultId, Mark: result.Mark,StudentId:result.StudentId};
        $("#resEditModal").modal("show");
    }
    $scope.rescancel = function () {
        $("#resEditModal").modal("hide");
    }
    $scope.updateres = function () {
        //console.log("Update exam");
        //console.log($scope.update);
        //console.log("insert update");
        apiVerbSvc.update(resultsUrl + "(" + $scope.update.ResultId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.update)
       .then(function (result) {

           var i = $scope.model.target.results.indexOf($scope.temp);
           $scope.model.target.results[i] = $scope.update;

           //console.log("Update exam");
           //console.log($scope.update);
           $("#resEditModal").modal("hide");
           $scope.current = null;
           $scope.temp = null;

       }, function (response) {

       })

    }

    //DeleteResult

    $scope.resdelete = function (result) {
        $scope.current = result;
        //$scope.admissionToDel = admission;
        $("#resDeleteModal").modal("show");
    }
    $scope.ConResultDelete = function () {
        apiVerbSvc.remove(resultsUrl + "(" + $scope.current.ResultId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.target.results.indexOf($scope.current);
             $scope.model.target.results.splice(i, 1);
             $scope.current = null;
             //$scope.admissionToDel = admission;
             $("#resDeleteModal").modal("hide");
         }, function (response) {

         })
    }


    //cancel....

    $scope.cancel = function () {
        $scope.current = null;
        
        $("#resDeleteModal").modal("hide");
        
        $scope.studentForm.$setPristine();
        $scope.studentForm.$setValidity();
        $scope.studentForm.$setUntouched();
        console.log($scope.current);
    }

});

