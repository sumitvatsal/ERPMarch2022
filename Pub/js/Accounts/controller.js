app.controller("myCtrl", function ($scope, myservice, $timeout) {
    getAllCategory();
    $scope.chkstatus = true;
    $scope.saveCategory = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var country = {
                Name: $scope.txtCategoryName,
                Status: $scope.chkstatus,
                Id: $scope.txtId,
                SchoolID: $('#ddlSchoolName').val()
            };

            var getData = myservice.saveCategory(country);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                claeartextbox();
                getAllCategory();
                $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                    $scope.IsVisible = false;
                }, 2000);

            }, function () {
                alert('Error in adding record');
            });

        }

    }

    function claeartextbox ()
    {
        setControlValue("txtCategoryName", "");
    }







    function getAllCategory() {


        var getCountryData = myservice.getAllCategory();
        debugger;
        getCountryData.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    destroy: true,
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                    'columnDefs': [{
                        "targets": [-1,-2] ,
                        "orderable": false
                    }],
                   
                });
            }, 2000);
        }, function () {
            alert('Error in getting records');
        });

    }


    $scope.editCategoyById = function (employee)
    {
        //$scope.txtCategoryName = employee.Name;
        $("#txtCategoryName").val(employee.Name);
        if (employee.Status == "Activate") {
            //employee.Status = true;
            $scope.chkstatus = true;
        }
        else {
            $scope.chkstatus = false;
            //employee.Status = false;
        }
        //$scope.chkstatus=employee.Status;
        $scope.txtId = employee.Id;
        $('#ddlSchoolName').val(employee["SchoolID"])
    }

    $scope.deleteCategoyById = function (employee)
    {
        if (confirm('are you sure,you want to Delete?')) {
            $("#loader").fadeIn();
            var getData = myservice.deleteCategoryById(employee.Id);
            getData.then(function (msg) {
                getAllCategory();
                $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $scope.IsVisible = false;
                }, 2000);
            }, function () {
                alert('Error in getting records');
            });
        }
    }
});