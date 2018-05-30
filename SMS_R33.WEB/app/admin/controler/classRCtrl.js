angular.module("schoolAdmin")
.controller("classRCtrl", function ($scope, apiVerbSvc, classesUrl, examYearsUrl, examTermsUrl, subjectsUrl, studentsUrl, examsUrl, resultsUrl) {
    $scope.model.current = null;
    $scope.model.temp = {};
    $scope.model.target = {};

    apiVerbSvc.get(classesUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
   .then(function (result) {
       console.log("Class list");
       $scope.model.classes = result.data.value;
   }, function (response) {

   });
    apiVerbSvc.get(examYearsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
  .then(function (result) {
      $scope.model.examyears = result.data.value;
      console.log(result.data.value);
      console.log("exam Year");

  }, function (response) { });

    apiVerbSvc.get(examTermsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
      .then(function (result) {
          console.log("exam Term");
          $scope.model.examterms = result.data.value;
      }, function (response) {

      });

    apiVerbSvc.get(subjectsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
.then(function (result) {
    console.log("Class list");
    $scope.model.subjects = result.data.value;
}, function (response) {

});
    apiVerbSvc.get(studentsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
  .then(function (result) {
      console.log("exam Term");
      $scope.model.students = result.data.value;
  }, function (response) {

  });

    //GET STUDENT OF TARGET Exam Year
    $scope.selectedClassId = function () {
        $scope.model.current.ExamId = null;
        $scope.model.temp = {};
        $scope.model.target = {};
        $scope.model.target.examyears = [];
        //$scope.model.targetresults = [];
        //$("#resulterr").css("visibility", "hidden");
        //$("#addresult").css("visibility", "hidden");

        apiVerbSvc.get(examsUrl+"?$filter=ClassId eq " + $scope.model.current.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
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
        $scope.model.current.ExamId = null;
        $scope.model.temp.ExamTermId = {};
        $scope.model.temp.SubjectId = {};
        $scope.model.current.ExamTermId = null;
        $scope.model.target.examterms = [];
        $scope.model.target.subjects = [];
        //$scope.model.target.results = [];
        //$scope.model.targetresults = [];
        //$("#resulterr").css("visibility", "hidden");
        //$("#addresult").css("visibility", "hidden");

        for (var ti = 0; ti < $scope.model.examterms.length; ti++) {
            for (var ei = 0; ei < $scope.model.tempExams.length; ei++) {
                if ($scope.model.examterms[ti].ExamTermId == $scope.model.tempExams[ei].ExamTermId) $scope.model.target.examterms.push($scope.model.examterms[ti]);
            }
        }

    }

    //GET TARGET SUBJECT
    $scope.selectedETerm = function () {
        $scope.model.current.ExamId = null;
        $scope.model.temp.SubjectId = {};
        $scope.model.target.subjects = [];
        $scope.model.temp.subjects = [];
        //$scope.model.targetresults = [];
        //$("#resulterr").css("visibility", "hidden");
        //$("#addresult").css("visibility", "hidden");

        apiVerbSvc.get(examsUrl + "?$filter=ExamYearId eq " + $scope.model.temp.ExamYearId + " and ExamTermId eq " + $scope.model.temp.ExamTermId + " and ClassId eq " + $scope.model.current.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
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
       
    }
    //GET TRGET EXAM AND RESULT
    $scope.getExamId = function () {
        //RARGET EXAM
        apiVerbSvc.get(examsUrl + "?$filter=ExamYearId eq " + $scope.model.temp.ExamYearId + " and ExamTermId eq " + $scope.model.temp.ExamTermId + " and SubjectId eq " + $scope.model.temp.SubjectId + " and ClassId eq " + $scope.model.current.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
            .then(function (result) {
                $scope.model.current.ExamId = result.data.value[0].ExamId;
                if (result.data.value.length > 0) {
                    $("#err").css("visibility", "hidden");
                    $("#table").css("visibility", "initial");
                    //TARGET RESULT
                    apiVerbSvc.insert(studentsUrl + "/GetStudentMark", { "Authorization": "Bearer " + $scope.auth.accesstoken }, { ExamId: $scope.model.current.ExamId, ClassId: $scope.model.current.ClassId })
                     .then(function (result) {
                         $scope.model.target.results = result.data.value;

                     }, function (response) { });
                }
                else
                { 
                    $("#err").css("visibility", "initial");
                    $("#table").css("visibility", "hidden");
                }

            }, function (response) { });

        
    }
    $scope.saveResults = function ()
    {
        var results = [];
        for (var i = 0; i < $scope.model.target.results.length; i++) {
            console.log($scope.model.target.results[i].ResultId);
            var x= {
                ResultId: $scope.model.target.results[i].ResultId,
                ExamId: $scope.model.target.results[i].ExamId,
                StudentId: $scope.model.target.results[i].StudentId,
                Mark: $scope.model.target.results[i].Mark,
            }
            results.push(x);
            console.log("results lengteh");
            console.log($scope.model.target.results.length);

        }
        console.log("Binded result");
        console.log(results);
        apiVerbSvc.insert(resultsUrl + "/ResultSave", { "Authorization": "Bearer " + $scope.auth.accesstoken }, { results: results })
                .then(function (result) {
                    $scope.succMsg = "Result Save succesfully";

                }, function (response) { });

    }


});