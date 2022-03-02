app.service("myService", function ($http) {
    this.AddCountry = function (country) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/showtest",
            data: JSON.stringify(country),
            dataType: "json"
        });
        return response;
    }
});
