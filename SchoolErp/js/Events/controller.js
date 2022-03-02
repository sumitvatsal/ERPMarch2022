 
app.controller("eventCtrl", function ($scope, eventService, $timeout) {
    getAllEventsTypes();
  
    $("#SaveButton").prop('value', 'Save');
    $scope.chkstatus = true;
    $scope.saveEventType = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var event = {

                Id: $scope.txtEventId,
                Name: $scope.txtEventType,
                Status: $scope.chkstatus,
              SchoolID: getControlValue("ddlSchoolName")
            }
            var getData = eventService.saveEventType(event);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllEventsTypes();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }

    }

   
    function getAllEventsTypes()
    {
        $("#loader").fadeIn();
        var getAllEventsTypes = eventService.getAllEventsTypes();
        debugger;
        getAllEventsTypes.then(function (emp) {
            $scope.employees = emp.data;
            $("#loader").fadeOut();
            var t = $('#example1').DataTable();
            t.destroy();
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],

                });
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.editEventTypeById = function (employee) {
        $("#SaveButton").prop('value', 'Edit');
       
        $scope.txtEventId=employee.Id;
        $scope.txtEventType = employee.Name;
        // $scope.chkstatus = employee.Status;
        if (employee.Status == "Activate") {
        //    employee.Status = true;
            $scope.chkstatus = true;
        }
        else {
        //    employee.Status = false;
            $scope.chkstatus = false;
        }
        //$scope.chkstatus = employee.Status;
        $('#ddlSchoolName').val(employee["SchoolID"])

    }

    $scope.deleteEventTypeById = function (employee)
    {
        if (confirm('are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var getData = eventService.deleteEventTypeById(employee.Id);
            getData.then(function (msg) {
                getAllEventsTypes();
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.clearAllTexbox = function () {
        $scope.txtEventId = null;
        $scope.txtEventType = null;
        $scope.chkstatus = true;
        $("#SaveButton").prop('value', 'Save');
       
        //setControlValue("ddlSchoolName", "0")
       }
})

app.controller("AddeventCtrl", function ($scope, AddeventService, $timeout) {
    getAllEvents();
    getAllEventsForDepartment();
    $("#chkSms").prop("checked", false);
    //callServiceMethodForDDl("", "/api/EventsAPI", "getAllActiveEvents", "ddlEventType", false, "", "", "", "");
    //callServiceMethodForDDl("", "/api/EventsAPI", "getAllClassAndSection", "ddlClassSection", false, "", "", "", "");
    //callServiceMethodForDDl("", "/api/EventsAPI", "getAllDepartment", "ddlDepartment", false, "", "", "", "");
    $('#ddlClassSection').select2();

    var droplist = $('#txtEventFor');
    droplist.change(function () {
        if (droplist.val() === '2') {
            $("#department").hide("slow");
            $("#course").show("slow");
            $("#batch").show("slow");
        }
        if (droplist.val() === '3') {
            $("#course").hide("slow");
            $("#batch").hide("slow");
            $("#department").show("slow");
        }
    })

    $scope.saveEventDetails=function()
    {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
           

            var classid = getControlValue("ddlClassSection");
          
            var a = classid.split(",");
          
            var replacefirst = a[0];
           
           
            var Classidvalue;
            if (replacefirst === "-1") {

               
                
                a.shift();
                Classidvalue = a.toString();
              
            }
            else
            {
                 Classidvalue = getControlValue("ddlClassSection");
            }

           

            var event = {
                Id: getControlValue("txtEventId"),
                EventName: getControlValue("txtEventName"),
                EventType: getControlValue("ddlEventType"),
                Description: getControlValue("txtEventDescription"),
                StartDate: getControlValue("txtStartDate") + ' ' + getControlValue("sTime"),
                EndDate: getControlValue("txtEndDate") + ' ' + getControlValue("eTime"),
                StartDatechange: getControlValue("txtStartDate") + ' ' + getControlValue("sTime"),
                EndDatechange : getControlValue("txtEndDate") + ' ' + getControlValue("eTime"),
                InchargName: getControlValue("txtInchargeName"),
                EventFor: getControlValue("txtEventFor"),
                ClassId: Classidvalue,
                //Section: getControlValue("ddlClassSection"),
                Department: getControlValue("ddlDepartment"),
                SchoolID: getControlValue("ddlSchoolName"),
                SendSms: getControlValue("chkSms")
            }

            var ddl = JSON.stringify(event)


            callServiceMethod(ddl, "/api/EventsAPI", "saveEventDetails", false, "", "", false, "", true, saveEventDetails_scs, "", "", "");
        }


    }

    function saveEventDetails_scs(p1, p2, p3, datas)
    {
        $("#divSucesEmail").fadeIn();
        $("#sentEmailMsg").html(datas);
        setTimeout(function () {
            $("#divSucesEmail").fadeOut();
            $("#sentEmailMsg").html("");
            if (datas == "Events Updated Successfully") {
                location.href = "/Events/AddEvents";
            }
        }, 2000)
        getAllEvents();
        getAllEventsForDepartment();
        claearEvents();
      
        $("#loader").fadeOut();
        $("#department").hide("hide");
        
        $("#course").hide("hide");
        $("#txtEventDescription").val("");

    }

    function claearEvents()
    {
        setControlValue("txtEventName", "");
        setControlValue("ddlEventType", 0);
        setControlValue("txtEventDescription", "");
        setControlValue("txtInchargeName", ""); 
        setControlValue("txtEndDate", ""); 
        setControlValue("txtEventFor", 0); 
        setControlValue("ddlClassSection", "");
       
        setControlValue("ddlDepartment", 0);

        //setControlValue("txtEventFor", 0); 
        //setControlValue("ddlClassSection", ""); 
        //setControlValue("ddlDepartment", 0);
        setControlValue("txtStartDate", ""); 
        //setControlValue("sTime", ""); 
        
    }



    function getAllEvents()
    {
       
      $("#loader").fadeIn();
        var getAllEvents = AddeventService.getAllEvents();
        getAllEvents.then(function (emp) {
          
            $scope.employees = emp.data;
            $("#loader").fadeOut();
            var t = $('#example1').DataTable();
            t.destroy();
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],
                });
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }


    function getAllEventsForDepartment() {
        $("#loader").fadeIn();
        var getAllEvents = AddeventService.getAllEventsForDepartment();
        debugger;
        getAllEvents.then(function (emp) {
           
            $scope.eventss = emp.data;
            $("#loader").fadeOut();
            var t = $('#example2').DataTable();
            t.destroy();
            $timeout(function () {
                $('#example2').DataTable(
                    {
                        'columnDefs': [{
                            "targets": -1,
                            "orderable": false
                        }],
                    }

                );
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }



    $scope.deleteEventsDetailsById = function (employee) {
        if (confirm('are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var getData = AddeventService.deleteEventsById(employee.eventId);
            getData.then(function (msg) {
                getAllEvents();
                getAllEventsForDepartment();
                $("#divSucesEmail").fadeIn();
                $("#sentEmailMsg").html(msg.data);
             //   $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divSucesEmail").fadeOut();
                }, 2000);
            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.editEventsClassDetailsById = function (employee)
    {
        
        setControlValue("txtEventId", employee.eventId);
        setControlValue("txtEventName", employee.EventName);
        setControlValue("ddlEventType", employee.eventTypeId);

        setControlValue("txtEventDescription", employee.Description);
        setControlValue("txtInchargeName", employee.InchargName);
        setControlValue("sTime", employee.starttime);
        setControlValue("eTime", employee.endtime);
        setControlValue("txtStartDate", employee.startdated);
        setControlValue("txtEndDate", employee.enddated);
        setControlValue("txtEventFor", employee.EventForId);
        $('#ddlSchoolName').val(employee["SchoolID"])

        if (employee.EventForId === '2') {
            $("#department").hide("slow");
            $("#course").show("slow");
            $("#batch").show("slow");
        }
        if (employee.EventForId === '3') {
            $("#course").hide("slow");
            $("#batch").hide("slow");
            $("#department").show("slow");
        }

        setTimeout(function () {
            var s = new Array();
            s.push(employee.classSection);
            $("#ddlClassSection").val(s).trigger('change');
        }, 1000);
        $('#ddlClassSection').attr("disabled", "disabled");
        $('#txtEventFor').attr("disabled", "disabled");

        
        //setControlValue("ddlEventType", employee.eventTypeId);

  
        
    }



    $scope.editEventsDepartmentDetailsById = function (employee) {

        setControlValue("txtEventId", employee.eventId);
        setControlValue("txtEventName", employee.EventName);
        setControlValue("ddlEventType", employee.eventTypeId);

        setControlValue("txtEventDescription", employee.Description);
        setControlValue("txtInchargeName", employee.InchargName);
        setControlValue("sTime", employee.starttime);
        setControlValue("eTime", employee.endtime);
        setControlValue("txtStartDate", employee.startdated);
        setControlValue("txtEndDate", employee.enddated);
        setControlValue("txtEventFor", employee.EventForId);
        $('#ddlSchoolName').val(employee["SchoolID"])

        if (employee.EventForId === '2') {
            $("#department").hide("slow");
            $("#course").show("slow");
            $("#batch").show("slow");
        }
        if (employee.EventForId === '3') {
            $("#course").hide("slow");
            $("#batch").hide("slow");
            $("#department").show("slow");
        }

        setTimeout(function () {
            var s = new Array();
            s.push(employee.depId);
            $("#ddlDepartment").val(s).trigger('change');
        }, 1000);
        $('#ddlDepartment').attr("disabled", "disabled");
        $('#txtEventFor').attr("disabled", "disabled");
        //setControlValue("ddlEventType", employee.eventTypeId);



    }


});




app.controller("taskDetails", function ($scope, taskService, $timeout) {
    
    $scope.saveTaskDetails = function ()
    {
        if ($("#form1").valid()) {
            if ($("#ddlUserType").val() == 1) {
                if ($("#ddlCourse").val() == 0) {
                    alert("please Select Class")
                }
                else if ($("#ddlSection").val() == 0) {
                    alert("please Select Section")
                }
                else if ($("#ddlStudents").val() == "") {
                    alert("please Select Students")
                }
                else {
                    $("#loader").fadeIn();
                    var events = {
                        Id: $("#txtTaskId").val(),
                        TaskName: $("#txtTaskName").val(),
                        Description: $("#txtTaskDescription").val(),
                        Priority: $("#ddlTaskPriority").val(),
                        TaskDate: $("#taskDate").val(),
                        UserType: $("#ddlUserType").val(),
                        Class: $("#ddlCourse").val(),
                        Section: $("#ddlSection").val(),
                        Student: $("#ddlStudents").val(),
                        Status: $("#ddlTaskStatus").val(),
                        Department: $("#txtDepartment").val(),
                        Employee: $("#ddlEmployees").val(),
                        SchoolID: getControlValue("ddlSchoolName")

                    }
                    var getData = taskService.saveTaskDetails(events);
                    getData.then(function (msg) {

                        $("#divSucesEmail").fadeIn();
                        //  $scope.successMsg = msg.data;
                        $("#sentEmailMsg").html(msg.data);
                        $("#loader").fadeOut();
                        $timeout(function () {
                            $("#divSucesEmail").fadeOut();
                        }, 2000);

                        if (msg.data == "Task Assigned updated Successfully") { }
                        else {
                            $scope.clearAllTextBoxx();
                        }

                    }, function () {
                        alert('Error in adding record');
                    });

                }
            }
            else {
                if ($("#txtDepartment").val() == 0) {
                    alert("Please select Department");
                }
                else if ($("#ddlEmployees").val() == "") {
                    alert("Please Select Employee");

                }
                //else if ($("ddlemployee").val()==0)
                //{
                //    alert("please recheck ");

                //}
                else {
                    $("#loader").fadeIn();
                    var events = {
                        Id: $("#txtTaskId").val(),
                        TaskName: $("#txtTaskName").val(),
                        Description: $("#txtTaskDescription").val(),
                        Priority: $("#ddlTaskPriority").val(),
                        TaskDate: $("#taskDate").val(),
                        UserType: $("#ddlUserType").val(),
                        Class: $("#ddlCourse").val(),
                        Section: $("#ddlSection").val(),
                        Student: $("#ddlStudents").val(),
                        Status: $("#ddlTaskStatus").val(),
                        Department: $("#txtDepartment").val(),
                        Employee: $("#ddlEmployees").val(),
                        SchoolID: getControlValue("ddlSchoolName")

                    }
                    var getData = taskService.saveTaskDetails(events);
                    getData.then(function (msg) {

                        $("#divSucesEmail").fadeIn();
                        //  $scope.successMsg = msg.data;
                        $("#sentEmailMsg").html(msg.data);
                        $("#loader").fadeOut();
                        $timeout(function () {
                            $("#divSucesEmail").fadeOut();
                        }, 2000);

                        if (msg.data == "Task Assigned updated Successfully") { }
                        else {
                            $scope.clearAllTextBoxx();
                        }

                    }, function () {
                        alert('Error in adding record');
                    });
                }

       

            }
     
        }
    
    }

    $scope.clearAllTextBoxx=function()
    {
       

        $scope.txtTaskName=null;
        $scope.txtTaskDescription=null;
        $scope.ddlTaskPriority=null;
        $scope.taskDate=null;
       
        $("#ddlCourse").val("0");
        $("#ddlSection").val("");
        $scope.ddlTaskStatus = null;
        $("#txtDepartment").val("0");
        if ($scope.ddlUserType == "1")
        {
            $scope.ddlUserType = null;
            $("#ddlStudents").empty().trigger('change')
        }
        else
        {
            $scope.ddlUserType = null;
            $("#ddlEmployees").empty().trigger('change')

        }

       // $("#ddlStudenddddts").val("");
      
      //   $("#ddlStudents").val("");
       
       
        //$("#ddlEmployees").val("");
    }
});

app.controller("ViewtaskDetails", function ($scope, viewTaskss, $timeout) {
    ViewTaskDetailsForStudent();
    ViewTaskDetailsForEmployess();
    function ViewTaskDetailsForStudent()
    {
        $("#loader").fadeIn();
        var getAllEventsTypesForStudent = viewTaskss.getAllTaskDetailsForStudent();
        debugger;
        getAllEventsTypesForStudent.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $("#loader").fadeOut();
            $timeout(function () {
                $('#example1').DataTable({
                    scrollX: true,
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],
                });
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }


    function ViewTaskDetailsForEmployess() {
        $("#loader").fadeIn();
        var getAllEventsTypesEmployess = viewTaskss.getAllEventsTypesEmployess();
        debugger;
        getAllEventsTypesEmployess.then(function (emp) {
            var t = $('#example2').DataTable();
            t.destroy();
            $scope.eventss = emp.data;
            $("#loader").fadeOut();
            $timeout(function () {
                $('#example2').DataTable({
                    scrollX: true,
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],
                });
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.viewStudentTaskDetasils = function (employee)
    {
        document.getElementById("btnStudentShowTasks").click();
        setControlValue("spStudentTaskName", employee.TaskName);
        setControlValue("spStudentTaskDesc", employee.Description);
        setControlValue("spStudentTaskDate", employee.TaskDate);
        setControlValue("spStudentName", employee.StudentName);
        setControlValue("spStudentTaskPriority", "<a href='#' style='background-color:" + employee.priorityColor + ";color:white;padding: 4px;border-radius: 8px;'>" + employee.Priority + "</a>");
        setControlValue("spStudentStatus", "<a href='#' style='background-color:" + employee.color + ";color:white;padding: 4px;border-radius: 8px;'>" + employee.Status + "</a>");
        setControlValue("ddlSchoolName1", employee.School)
    }

    $scope.viewEmployeeTaskDetails = function (employee) {
        document.getElementById("btnEmployeeShowTasks").click();
        setControlValue("spEmployeeTaskName", employee.TaskName);
        setControlValue("spEmployeeTaskDesc", employee.Description);
        setControlValue("spEmployeeTaskDate", employee.TaskDate);
        setControlValue("spEmployeeName", employee.EmployeeName);
        setControlValue("spEmployeeTaskPriority", "<a href='#' style='background-color:" + employee.priorityColor + ";color:white;padding: 4px;border-radius: 8px;'>" + employee.Priority + "</a>");
        setControlValue("spEmployeeStatus", "<a href='#' style='background-color:" + employee.color + ";color:white;padding: 4px;border-radius: 8px;'>" + employee.Status + "</a>");
        setControlValue("ddlSchoolName", employee.School)
    }


    $scope.deleteStudentTaskById = function (employee) {
        if (confirm('are you sure,you want to delete this task?')) {
              $("#loader").fadeIn();
             var getData = viewTaskss.deleteStudentTaskById(employee.Id);
              getData.then(function (msg) {
                ViewTaskDetailsForStudent();
                $("#divSuccessMsg").fadeIn();
                $("#divSuccessMsg").html("<strong>Success!</strong> " + data.Msg);
                setTimeout(function () {
                    $("#divSuccessMsg").fadeOut();
                }, 2000)
                $("#loader").fadeOut();
            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.deleteEmployeeTaskbyId = function (employee)
    {
        if (confirm('are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var getData = viewTaskss.deleteEmployeeTaskbyId(employee.Id);
            getData.then(function (msg) {
                ViewTaskDetailsForEmployess();
                $("#divSuccessMsg").fadeIn();
                $("#divSuccessMsg").html("<strong>Success!</strong> " + data.msg);
                setTimeout(function () {
                    $("#divSuccessMsg").fadeOut();
                }, 2000)
                $("#loader").fadeOut();
            }, function () {
                alert('Error in getting records');
            });
        }
    }
    

    $scope.editStudentTaskDetasils = function (employee)
    {
        location.href = "/Events/AssignTask?Id=" + employee.Id;
    }
});

 
