angular.module("myApp")
.controller("examsheduleCtrl", function ($scope, apiVerbSvc, examYearsUrl, examsUrl, subjectsUrl, examTermsUrl, classesUrl) {
    $scope.model.current = null;
    $scope.model.examTable = null;
    $scope.model.examYearTable = null;
    $scope.model.temp = {};
    $scope.model.target = {};
    $scope.model.tempSubjectsId = [];
    apiVerbSvc.get(examYearsUrl, null, null)
    .then(function (result) {
        $scope.model.examyears = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(examsUrl+"?$expand=Subject, Class, ExamYear, ExamTerm", null, null)
    .then(function (result) {
        $scope.model.exams = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(subjectsUrl, null, null)
  .then(function (result) {
      console.log("Class list");
      $scope.model.subjects = result.data.value;
  }, function (response) {

  });

    //For Exam table -start

  //For Get target Exam term Id and  Refress when new year is selected
    $scope.selectedEYId = function () {
        apiVerbSvc.get(examTermsUrl, null, null)
      .then(function (result) {
          console.log("exam Term");
          $scope.model.target.examterms = result.data.value;
        }, function (response) { });

        $scope.model.examTable.ExamTermId = null;
        $scope.model.examTable.ClassId = null;
        $scope.model.temp = {};
    }
  //For Get target Class and  Refress when new year is selected
         $scope.selectedETId = function () {
             apiVerbSvc.get(classesUrl, null, null)
       .then(function (result) {
           $scope.model.target.classes = result.data.value;
       }, function (response) {});
        
        $scope.model.examTable.ClassId = null; 
        $scope.model.temp = {};


    }

  //For GETTING TARGET EXAM SHEDULE
         $scope.selectedCId = function () {
             apiVerbSvc.get(examsUrl + "?$filter=ExamTermId eq " + $scope.model.examTable.ExamTermId + " and ExamYearId eq " + $scope.model.examTable.ExamYearId + " and  ClassId eq " + $scope.model.examTable.ClassId, null, null)
        .then(function (result) {

            $scope.model.temp.examps = result.data.value;
            if ($scope.model.temp.examps.length > 0) {
                $("#selectedES").css("visibility", "visible");
                $("#eserr").css("visibility", "hidden");
            }
            else {
                $("#eserr").css("visibility", "visible");
                $("#selectedES").css("visibility", "hidden");

            }

   }, function (response) { });
         }
    

});