angular.module("myApp")
.factory("apiVerbSvc", function ($http) {
    return {
        get: function (url, headers, data) {
            return $http({
                url: url,
                method:"GET",
                headers: headers,
                params:data
            });

        },

        insert: function (url, headers, data) {
            return $http({
                url: url,
                method: "POST",
                headers: headers,
                data: data 
            });

        }, 
        remove: function (url, headers, data) {
            return $http({
                url: url,
                method: "DELETE",
                headers: headers,
               
            })
        },
        update: function (url, headers, data) {
            return $http({
                url: url,
                method: "PUT",
                headers: headers,
                data: data
            });
        }
        
    }
});