angular.module("schoolAdmin")
.factory("authStore", function () {
    return {
        save: function (u, a) {
            sessionStorage.setItem("auth", JSON.stringify({username:u, accesstoken:a, authenticated:true}));
        },
        get: function () {
            if (sessionStorage.length > 0) {
                return sessionStorage.getItem("auth") == null ? { authenticated: false } : JSON.parse(sessionStorage.getItem("auth"));
            }
            else {
                return { authenticated: false };
            }
        },
        remove: function () {
            sessionStorage.removeItem("auth");
        }

    }
});