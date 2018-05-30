
angular.module("myApp")
.controller("examResultCtrl", function ($scope, apiVerbSvc, classesUrl, examTermsUrl, examYearsUrl, studentsUrl) {
    var selectedExamYear = null;
    $scope.selectOption = true;
    apiVerbSvc.get(classesUrl, null, null)
        .then(function (result) {
            console.log("Class list");
            $scope.model.classes = result.data.value;
        }, function (response) {

        })
  
    apiVerbSvc.get(examTermsUrl, null, null)
       .then(function (result) {
           console.log("exam Term");
           $scope.model.examTerms = result.data.value;
       }, function (response) {

       })
    apiVerbSvc.get(examYearsUrl, null, null)
     .then(function (result) {
         console.log("exam Term");
         $scope.model.examYears = result.data.value;
     }, function (response) {

     })
    $scope.getResult = function (student)
    {
        $scope.studentResult = true;
        $scope.selectOption = false;
        if ($scope.result.StudentId & $scope.result.ExamYearId & $scope.result.ExamTermId) {
          
            apiVerbSvc.get(resultsUrl+"?$expand=Exam, Student, Subject, ExamYear, ExamTerm&$filter=StudentId eq " + $scope.result.StudentId + " and ExamYearId eq " + $scope.result.ExamYearId + " and ExamTermId eq " + $scope.result.ExamTermId, null, null)
       .then(function (result) {
           console.log("result list" + result.data.value);
           $scope.model.results = result.data.value;
           
           $("#resultwarning").css("visibility", "hidden");
       }, function (response) {

       });
        }
        else {
           
            $("#resultwarning").css("visibility", "visible");
        }

    }
    $scope.rselectedId = function () {
        console.log("Change is ocured" + $scope.result.ClassId);
        apiVerbSvc.get(studentsUrl + "?$filter=ClassId eq " + $scope.result.ClassId, null, null)
     .then(function (result) {
         console.log("Student List eq id");
         $scope.model.students = result.data.value;
     }, function (response) {

     });
    }
    $scope.backTo = function () {
        $scope.result = null;
        $scope.model.results = null;
        $scope.model.students = null;
        $scope.studentResult = false;
        $scope.selectOption = true;
    }

    $scope.selectexamYear = function () {
        selectedExamYear = $scope.result.ExamId;
        console.log("ExamId Selected");
    }
    $scope.examFilterM = function (result) {
        return selectedExamYear == result.ExamId;
    }
    //PRINT THE MSRK SHEET
    $scope.printToCart = function (msheet) {
        var innerContents = document.getElementById('msheet').innerHTML;
        var popupWinindow = window.open('', '_blank', 'width=600,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
        popupWinindow.document.open();
        popupWinindow.document.write('<html><head><link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"></head><body onload="window.print()">' + innerContents + '</html>');
        popupWinindow.document.close();
    }
});