app.controller("LeaveController", function ($scope, LeaveService,$timeout) {
  
    getAllLeaveType();
    $scope.chkstatus = true;

    function getAllLeaveType()
    {
        $("#loader").fadeIn();
        var getAllLeaveType = LeaveService.getAllLeaveType();
        debugger;
        getAllLeaveType.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $("#loader").fadeOut();
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


    $scope.saveLeaveTypeDetails = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var leavetype = {

                Id: $scope.txtLeaveId,
                Name: $scope.txtLeaveType,
                Status: $scope.chkstatus,
                SchoolID: $('#ddlSchoolName').val()
            }

            var getData = LeaveService.saveLeaveTypeDetails(leavetype);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllLeaveType();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    $scope.editLeaveTypeById = function (employee)
    {
        if (employee.Status == "Activate") {
            $scope.chkstatus = true;
        }
        else {
            $scope.chkstatus = false;
        }
        $scope.txtLeaveId=  employee.Id ;
        $scope.txtLeaveType=employee.Name;
        //$scope.chkstatus = employee.Status;
        $('#ddlSchoolName').val(employee["SchoolID"])
    }

    $scope.deleteLeaveTypeById = function (employee)
    {
        if (confirm('are you sure,you want to delete this record?')) {
            $("#loader").fadeIn();
            var getData = LeaveService.deleteLeaveTypeById(employee.Id);
            getData.then(function (msg) {
                getAllLeaveType();
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
        $scope.txtLeaveId = null;
        $scope.txtLeaveType = null;
        $scope.chkstatus = false;
        //$('#ddlSchoolName').val(0);
    }
   
})


app.controller("LeaveDetailsController", function ($scope, LeaveDetails, $timeout) {
    
  //  getAllLeaveType();
   // getAllDesignation();
    getAllLeaveDetails();
    $scope.chkstatus = true;
     function getAllLeaveType() {
         $("#loader").fadeIn();
        var getAllLeaveType = LeaveDetails.getAllLeaveType();
        debugger;
        getAllLeaveType.then(function (emp) {
            $scope.leaves = emp.data;
            $("#loader").fadeOut();
            $timeout(function () {
                $('#example1').DataTable();
            }, 1000);
        }, function () {
            alert('Error in getting records');
        });
    }

    function getAllDesignation()
    {
        var getAllDesignation = LeaveDetails.getAllDesignation();
        debugger;
        getAllDesignation.then(function (emp) {
            $scope.designation = emp.data;
           
        }, function () {
            alert('Error in getting records');
        });

    }




    $scope.saveLeaveDetails = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var leavedeta = {

                Id: $scope.txtLeaveId,
                leaveCategory: $("#ddlLeaveCategory").val(),
                Designation: $("#ddlDesignatin").val(),
                StartDate: $scope.txtStartDate,
                EndDate: $scope.txtEndDate,
                AcademicYear: $("#ddl_academic_year").val(),
                leaveAssign: $scope.txtLeaveAssign,
                Status: $scope.chkstatus,
                //Name: $scope.ddlLeaveCategory.Name,
                SchoolID: $("#ddlSchoolName").val()
            }

            var getData = LeaveDetails.saveLeaveDetails(leavedeta);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllLeaveDetails();
                $scope.clearAllTexbox();
            }, function () {
                alert('Error in adding record');
            });
        }
    }



    function getAllLeaveDetails() {
        $("#loader").fadeIn();
        var getAllLeaveDetails = LeaveDetails.getAllLeaveDetails();
        debugger;
        getAllLeaveDetails.then(function (emp) {

            var t = $('#example1').DataTable();
            t.destroy();

            $scope.employees = emp.data;
            $("#loader").fadeOut();
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

    $scope.editLeaveDetailsById = function (employee) {
         
        $scope.txtLeaveId = employee.Id;
        employee.Designation = employee.DesigId;
 
        //fetchmultpledesg(employee.DesigId);
        $scope.txtLeaveAssign = employee.leaveAssign;
        //$scope.ddlLeaveCategory = employee.leaveCategoryId;
        //$scope.ddlDesignatin = employee.DesigId;
        $('#ddlDesignatin').val(employee["DesigId"]),
        $('#ddlLeaveCategory').val(employee["leaveCategoryId"]),
        //$scope.txtStartDate = employee.StartDate;
        //$scope.txtEndDate = employee.EndDate;
        $scope.chkstatus = employee.Status2;
        $('#ddlSchoolName').val(employee["SchoolID"])
    }

    function fetchmultpledesg(val)
    {
        var selectedValues = new Array();
        selectedValues.push(val);
        $("#ddlDesignatin").val(selectedValues).trigger("change");
        
    }

    $scope.deleteLeaveDetailsById = function (employee) {
        if (confirm('are you sure,you want to delete this record?')) {
            $("#loader").fadeIn();
            var getData = LeaveDetails.deleteLeaveDetailsById(employee.Id);
            getData.then(function (msg) {
                //getAllLeaveType();
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllLeaveDetails();
            }, function () {
                alert('Error in getting records');
            });
        }
    }
    
    $scope.clearAllTexbox = function () {
        $scope.txtLeaveId = null;
        var s = {};
     
        //$("#ddlDesignatin").val(s).trigger("change");
       // employee.Designation = null;

         //$scope.ddlLeaveCategory = null;
        $scope.txtLeaveAssign = null; 
        $scope.txtStartDate = null;
        $scope.txtEndDate = null;
        $scope.chkstatus = false;
        //$("#ddlSchoolName").val(0);
        $("#ddlDesignatin").val(-1); 
        $("#ddlLeaveCategory").val(-1);
    }
    
});

