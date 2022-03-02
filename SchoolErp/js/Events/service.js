app.service("eventService", function ($http) {
    this.saveEventType = function (event) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/saveEventType",
            data: JSON.stringify(event),
            dataType: "json"
        });
        return response;
    }



    this.getAllEventsTypes = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/getAllEventsTypes",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }



    this.deleteEventTypeById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/deleteEventTypeById",
            params: {
                id: Id
            }
        });
        return response;
    }


    //this.getAllEventsTypes = function () {
    //    var response = $http({
    //        method: "post",
    //        url: "/api/EventsAPI/getAllEventsTypes",
    //        data: "",
    //        dataType: "json",
    //        contentType:"application/json; charset=utf-8"
    //    });
    //    return response;
    //}
});
app.service("AddeventService", function ($http) {

    
    this.getAllEvents = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/getAllEvents",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.getAllEventsForDepartment = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/getAllEventsForDepartment",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }


    

    this.deleteEventsById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/deleteEventsById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
})

app.service("taskService", function ($http) {

    this.saveTaskDetails = function (events) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/saveTaskDetails",
            data: JSON.stringify(events),
            dataType: "json"
        });
        return response;
    }
})

app.service("viewTaskss", function ($http) {

    this.getAllTaskDetailsForStudent = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/getAllTaskDetailsForStudent",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.getAllEventsTypesEmployess = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/getAllEventsTypesEmployess",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deleteStudentTaskById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/deleteStudentTaskById",
            params: {
                id: Id
            }
        });
        return response;
    }

    this.deleteEmployeeTaskbyId = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/EventsAPI/deleteEmployeeTaskbyId",
            params: {
                id: Id
            }
        });
        return response;
    }

    
    
});

 
