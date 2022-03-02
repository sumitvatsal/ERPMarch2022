app.service("myservice",function($http){

    this.saveCategory = function (country) {
        var response = $http({
            method: "post",
            url: "/api/AccountsAPI/saveCategory",
            data: JSON.stringify(country),
            dataType: "json"
        });
        return response;
    }

    this.getAllCategory = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/AccountsAPI/getAllCategory",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }


    this.deleteCategoryById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/AccountsAPI/deleteCategoyById",
            params: {
                id: Id
            }
        });
        return response;
    }

    
})

 