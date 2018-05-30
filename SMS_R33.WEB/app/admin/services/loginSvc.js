angular.module("schoolAdmin")
.factory("loginSvc", function ($http) {
    return {
        signin: function (u, p) {
            return $http({
                url: "http://localhost:7643/Token",
                method: "POST",
                data: $.param({ grant_type: 'password', username: u, password: p }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
        }
    }
});