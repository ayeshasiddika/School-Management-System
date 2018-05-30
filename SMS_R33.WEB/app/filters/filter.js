angular.module("myApp")
.filter("unique", function () {
    return function (input, prop) {
        if (angular.isArray(input) && angular.isString(prop)) {
            var keys = [];
            var data = [];
            angular.forEach(input, function (item) {
                var key = item[prop];
                if (keys.indexOf(key) == -1) {
                    keys.push(key);
                    data.push(item);
                }
            })
            return data;
        }
        else
            return input;
    }
})
.filter("gradeFN", function () {
    return function (mark) {
        var result = "";
        if (mark >= 80) {
            result = "A+";
        }
        else if (mark >= 70) {
            result = "A";
        }
        else if (mark >= 60) {
            result = "-A";
        }
        else if (mark >= 50) {
            result = "B";
        }
        else if (mark >= 40) {
            result = "C";
        }
        else if (mark >= 33) {
            result = "D";
        }
        else {
            result = "F"
        }
        return result;
    }
})
.filter("sumFN", function () {
    return function (results) {
        if (angular.isArray(results)) {

            var sum = 0;
            for (var i = 0; i < results.length; i++) {
                var mark = results[i].Mark;
                sum += mark;
            }
            return sum;
        }

    }
})
.filter("averageGradeFN", function () {
    return function (results) {
        if (angular.isArray(results)) {
            var grde = 0;
            var avarage = 0;
            var sum = 0;
            for (var i = 0; i < results.length; i++) {
                var mark = results[i].Mark;
                if (mark < 33) {
                    avarage = 0;
                    result = "F";
                    return result;
                }
                else {
                    sum += mark;
                }
            }
            avarage = sum / results.length;

            if (avarage >= 80) {
                result = "A+";
            }
            else if (avarage >= 70) {
                result = "A";
            }
            else if (avarage >= 60) {
                result = "-A";
            }
            else if (avarage >= 50) {
                result = "B";
            }
            else if (avarage >= 40) {
                result = "C";
            }
            else if (avarage >= 33) {
                result = "D";
            }
            else {
                result = "F"
            }
            return result

        }

    }
});