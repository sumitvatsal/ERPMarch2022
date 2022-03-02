app.controller("salryhead", function ($scope, SeviceHead, $timeout) {

    $scope.chkstatus = true;



    // function getAllBatches() {
    //    $("#loader").fadeIn();
    //    var getAllBatches = batchs.getAllBatches();
    //    debugger;
    //    getAllBatches.then(function (emp) {
    //        var t = $('#example1').DataTable();
    //        t.destroy();
    //        $scope.employees = emp.data;
    //        $timeout(function () {
    //            $('#example1').DataTable({
                    
    //                scrollX: true,
    //                'columnDefs': [{
    //                    "targets": -1,
    //                    "orderable": false
    //                }],
    //            });
    //        }, 1000);
    //        $("#loader").fadeOut();
    //    }, function () {
    //        alert('Error in getting records');
    //    });

    //}




    getAllSalryHead();
    $scope.saveSalaryHead = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var payrolhead = {

                Id: $scope.txtheadId,
                Type: $("#ddlSalaryType").val(),
                Name: $scope.txtSalaryHead,
                Code: $scope.txtSalaryCode,

                Status: $scope.chkstatus,
                SchoolID: $("#ddlSchoolName").val()

            }

            var getData = SeviceHead.saveSalaryHead(payrolhead);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllSalryHead();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }
    function getAllSalryHead() {
        $("#loader").fadeIn();
        var getAllSalryHead = SeviceHead.getAllSalryHead();
        debugger;
        getAllSalryHead.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.editsalryheadById = function (employee) {
        if (employee.Status == "Activate") {
            employee.Statuss = true;
        }
        else {
            employee.Statuss = false;
        }
   
        $scope.txtheadId = employee.Id;
        // $scope.ddlSalaryType = employee.Type;
        $("#ddlSalaryType").val(employee.Type);
        $scope.txtSalaryHead = employee.Name;
        $scope.txtSalaryCode = employee.Code;
        $scope.chkstatus = employee.Statuss;
        $('#ddlSchoolName').val(employee["SchoolID"])
    }

    $scope.deletesalaryHeadById = function (employee) {
        if (confirm('are you sure,you want to Delete?')) {
            $("#loader").fadeIn();
            var getData = SeviceHead.deletesalaryHeadById(employee.Id);
            getData.then(function (msg) {
                getAllSalryHead();
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
        $scope.txtheadId =null;
        $scope.ddlSalaryType = null;
        $scope.txtSalaryHead = null;
        $scope.txtSalaryCode = null;
        $scope.chkstatus = true;
    }

})