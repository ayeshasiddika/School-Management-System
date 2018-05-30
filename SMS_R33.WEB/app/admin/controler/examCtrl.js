angular.module("schoolAdmin")
.controller("examCtrl", function ($scope, apiVerbSvc, examYearsUrl, examsUrl, subjectsUrl, studentsUrl, examTermsUrl, classesUrl) {
    $scope.model.current = null;
    $scope.model.examTable = null;
    $scope.model.examYearTable = null;
    $scope.model.temp = {};
    $scope.model.target = {};
    $scope.model.tempSubjectsId = [];
    apiVerbSvc.get(examYearsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.model.examyears = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(examsUrl + "?$expand=Subject, Class, ExamYear, ExamTerm", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
    .then(function (result) {
        $scope.model.exams = result.data.value;
        console.log(result.data.value);
        console.log("exam Year");

    }, function (response) { });

    apiVerbSvc.get(subjectsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
  .then(function (result) {
      console.log("Class list");
      $scope.model.subjects = result.data.value;
  }, function (response) {

  });

    $scope.insertselectedId = function () {
        apiVerbSvc.get(studentsUrl + "?$filter=ClassId eq " + $scope.model.current.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
     .then(function (result) {
         console.log("Student List eq id");
         $scope.model.students = result.data.value;
     }, function (response) {

     });
    }
    //for Exam Year Table -start
    $scope.addExamYear = function () {
        console.log($scope.model.current);
        apiVerbSvc.insert(examYearsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.examYearTable)
        .then(function (result) {
            $scope.model.examyears.push(result.data);
            $scope.model.examYearTable = null;
            console.log("Exam Year"+result.data);
        }, function (response) { });
    }
    $scope.selectForD = function (examY) {
        $scope.current = examY;
        console.log("select for Delete");
        console.log($scope.current);
    }

   
    $scope.deleteY = function () {
        apiVerbSvc.remove(examYearsUrl + "(" + $scope.current.ExamYearId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             var i = $scope.model.examyears.indexOf($scope.current);
             $scope.model.examyears.splice(i, 1);
             $scope.current = null; 
         }, function (response) {

         })
    }
    //Exam Year table end

    //For Exam table -start

  //For Get target Exam term Id and  Refress when new year is selected
    $scope.selectedEYId = function () {
        $scope.model.target.classes = null;
        $scope.model.target.examterms = null;

        apiVerbSvc.get(examTermsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
      .then(function (result) {
          console.log("exam Term");
          $scope.model.target.examterms = result.data.value;
        }, function (response) { });

        $scope.model.examTable.ExamTermId = null;
        $scope.model.examTable.ClassId = null;
        $scope.model.examTable.SubjectId = null;
        $scope.model.examTable.ExamDate = null;
        $scope.model.temp = {};
    }
  //For Get target Class and  Refress when new year is selected
    $scope.selectedETId = function () {
        $scope.model.target.classes = null;
        apiVerbSvc.get(classesUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
       .then(function (result) {
           $scope.model.target.classes = result.data.value;
       }, function (response) {});
        
        $scope.model.examTable.ClassId = null;
        $scope.model.examTable.SubjectId = null;
        $scope.model.examTable.ExamDate = null;
        $scope.model.temp = {};


    }

  //For GETTING TARGET EXAM SHEDULE
         $scope.selectedCId = function () {
             apiVerbSvc.get(examsUrl + "?$filter=ExamTermId eq " + $scope.model.examTable.ExamTermId + " and ExamYearId eq " + $scope.model.examTable.ExamYearId + " and  ClassId eq " + $scope.model.examTable.ClassId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
       $scope.model.temp.examps = result.data.value;
       console.log("exam temp");
       console.log($scope.model.temp.examps);


   }, function (response) { });
         }
    //GET TARGET EXAM
         $scope.selectedSId = function () {
             apiVerbSvc.get(examsUrl + "?$filter=ExamTermId eq " + $scope.model.examTable.ExamTermId + " and ExamYearId eq " + $scope.model.examTable.ExamYearId + " and  ClassId eq " + $scope.model.examTable.ClassId + " and  SubjectId eq " + $scope.model.examTable.SubjectId, { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
                     .then(function (result) {
                         $scope.model.targetExamps = result.data.value;
                         console.log("Target Examp Length");
                         console.log($scope.model.targetExamps.length);
                         if ($scope.model.targetExamps.length >= 1) {
                             console.log("value null")
                             $("#err").css("visibility", "initial");
                             $("#add").css("visibility", "hidden");
                         }
                         else {
                             $("#err").css("visibility", "hidden");
                             $("#add").css("visibility", "initial");
                         }

                     }, function (response) {

                     });

         }

    //ADD EXAM
    $scope.addExam = function () {
        
        apiVerbSvc.insert(examsUrl, { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.examTable)
        .then(function (result) {
            $scope.model.exams.push(result.data);
            $scope.model.tempSubjectsId.push(result.data);
            $scope.model.temp.examps.push(result.data);
            $scope.model.examTable.SubjectId = null;
            $scope.model.examTable.ExamDate = null;
        }, function (response) { });
    }

    //
    $scope.refresh = function () {
        $scope.model.examTable = null;
        $scope.model.tempSubjectsId = [];
        $scope.model.temp = {};
        $scope.model.target = {};
        $("#err").css("visibility", "hidden");
        $("#add").css("visibility", "hidden");
    }

    //Exam table end
    //UPDATE EXAM SHEDULE
    $scope.update = null;
    $scope.temp = null;

    $scope.examEdit = function (exam) {
        $("#examEditModal").modal("show");
        $scope.temp = exam;
        $scope.update = { ExamId: exam.ExamId, ExamDate: exam.ExamDate, ClassId: exam.ClassId, SubjectId: exam.SubjectId, ExamYearId: exam.ExamYearId, ExamTermId: exam.ExamTermId };

    }
    $scope.ecancel = function () {
        $("#examEditModal").modal("hide");
    }
    $scope.updatexam = function () {
        console.log("Update exam");
        console.log($scope.update);
        console.log("insert update");
        apiVerbSvc.update(examsUrl + "(" + $scope.update.ExamId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.update)
       .then(function (result) {

           var i = $scope.model.temp.examps.indexOf($scope.temp);
           $scope.model.temp.examps[i] = $scope.update;

           console.log("Update exam");
           console.log($scope.update);
           $("#examEditModal").modal("hide");
           $scope.current = null;
           $scope.temp = null;

       }, function (response) {

       })

    }

});