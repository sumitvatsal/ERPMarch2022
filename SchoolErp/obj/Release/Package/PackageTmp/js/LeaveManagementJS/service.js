app.service("LeaveService", function ($http) {
    
    this.saveLeaveTypeDetails = function (leavetype) {
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/saveLeaveTypeDetails",
            data: JSON.stringify(leavetype),
            dataType: "json"
        });
        return response;
    }


    this.getAllLeaveType = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/getAllLeaveType",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    

    this.deleteLeaveTypeById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/deleteLeaveTypeById",
            params: {
                id: Id
            }
        });
        return response;
    }

    
})
app.service("LeaveDetails", function ($http) {
    this.getAllLeaveType = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/getAllActiveLeaveType",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }


    this.getAllDesignation = function () {
        var response = $http({
            method: "post",
            url: "/api/EmployeeAPI/getAllDesignation",
            data: "",
            dataType: "json"
        });
        return response;
    }


    this.saveLeaveDetails = function (leavedeta) {
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/saveLeaveDetails",
            data: JSON.stringify(leavedeta),
            dataType: "json"
        });
        return response;
    }


    /////Vehicle////


    this.saveVehicleDetails = function (vd) {
        var response = $http({
            method: "post",
            url: "/api/TransportApi/saveVehicleDetails",
            data: JSON.stringify(vd),
            dataType: "json"
        });
        return response;
    }


    this.deleteVehicleById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/TransportApi/deleteVehicleById",
            params: {
                id: Id
            }
        });
        return response;
    }
/////////////////////////adddestination///
    this.saveDestinationDetails = function (vd) {
        var response = $http({
            method: "post",
            url: "/api/TransportApi/saveDestinationDetails",
            data: JSON.stringify(vd),
            dataType: "json"
        });
        return response;
    }

    //////////////////
    this.getAllLeaveDetails = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/getAllLeaveDetails",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deleteLeaveDetailsById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/LeaveAPI/deleteLeaveDetailsById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
})