app.service("SeviceHead", function ($http) {
    this.saveSalaryHead=function(payrolhead){
    
        var response = $http({
            method: "post",
            url: "/api/PayrollAPI/saveSalaryHead",
            data: JSON.stringify(payrolhead),
            dataType: "json"
        });
        return response;
    }

    this.getAllSalryHead = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/PayrollAPI/getAllSalryHead",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deletesalaryHeadById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/PayrollAPI/deletesalaryHeadById",
            params: {
                id: Id
            }
        });
        return response;
    }


    
})