app.controller("mycntrl", function ($scope, myService, $timeout) {
    getAllCountries()


    //  $scope.IsVisible = false;
    $scope.chkstatus = true;

    $scope.saveCountry = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var country = {
                Name: $scope.txtCountryName,
                Status: $scope.chkstatus,
                Id: $scope.txtCountryId,
            };
            var getData = myService.AddCountry(country);
            getData.then(function (msg) {
                getAllCountries();
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                    //  $scope.IsVisible = false;
                }, 2000);

                $scope.clearAllTexbox();
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllCountries() {

        $("#loader").fadeIn();
        var getCountryData = myService.getAllCountries();
        debugger;
        getCountryData.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    scrollX: true,
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }]
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }

    $scope.editCountryById = function (employee) {
        debugger;
        var getData = myService.getCountryById(employee.Id);
        getData.then(function (emp) {
            $scope.txtCountryId = employee.Id;
            $scope.txtCountryName = employee.Name;
            $scope.chkstatus = employee.Status;

        },
        function () {
            alert('Error in getting records');
        });
    }

    $scope.deleteCountryById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var getData = myService.deleteCountryById(employee.Id);
            getData.then(function (msg) {
                getAllCountries();
                $("#divsuccesmsg").fadeIn();
                //  $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
            }, function () {
                alert('Error in getting records');
            });
        }
    }


    $scope.getstatebyCountryId = function () {
        var Id = $scope.ddlCountry;
        alert("h");
        var getstatebyCountryId = myService.getstatebyCountryId(Id)
        getstatebyCountryId.then(function (emp) {

            $scope.state = emp.data;

        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.clearAllTexbox = function () {
        $scope.txtCountryId = null;
        $scope.txtCountryName = null;
        $scope.chkstatus = true;
    }
});


////////////////--------State----------////////////////////////

app.controller("mystate", function ($scope, statss, $timeout) {
    getAllCountriesforSate();               
    getAllState();
    // $scope.IsVisible = false;
    $scope.chkstatus = true;
    function getAllCountriesforSate() {


        var getAllCountriesforState = statss.getAllCountriesforState();
        debugger;
        getAllCountriesforState.then(function (emp) {

            $scope.country = emp.data;

        }, function () {
            alert('Error in getting records');
        });

    }
    $scope.saveState = function () {

        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var statee = {
                Id: $scope.txtStateId,
                CountryName: $("#ddlCountry").val(),
                Name: $scope.txtStateName,
                Status: $scope.chkstatus,
            }
            var getData = statss.AddState(statee);
            getData.then(function (msg) {
                //   getAllCountries();
                //  $scope.IsVisible = $scope.IsVisible ? false : true;
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                //  loadInternalPage();
                //  location.reload(true);
                getAllState();
                $scope.clearAllTexbox();

            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllState() {

        $("#loader").fadeIn();
        var getAllStateData = statss.getAllState();
        debugger;
        getAllStateData.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    scrollX: true,
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }]
                });
            }, 2000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }


    $scope.editStateById = function (employee) {

        var editStateById = statss.editStateById(employee.Id)
        editStateById.then(function (emp) {
            $scope.state = emp.data;
            $scope.txtStateId = $scope.state.Id;
            //   $scope.ddlCountry = $scope.state.CountryName;
            $("#ddlCountry").val($scope.state.CountryName);

            $scope.txtStateName = $scope.state.Name;


            $scope.chkstatus = $scope.state.Status;

        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.deleteStateById = function (employee) {
        if (confirm('Are you sure,you want to delete')) {
            $("#loader").fadeIn();
            var deleteStateById = statss.deleteStateById(employee.Id)
            deleteStateById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //  $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllState();

            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.clearAllTexbox = function () {
        $scope.txtStateId = null,
            $("#ddlCountry").val("0"),
            $scope.txtStateName = null,
            $scope.chkstatus = true;
    }

});

////////------City--------////////////////

app.controller("cityController", function ($scope, cities, $timeout) {
    // $scope.IsVisible = false;
    $scope.chkstatus = true;
    getallCountriesforCity();
    getallCities();
    function getallCountriesforCity() {

        var getAllCountriesforcity = cities.getAllCountriesforcity();
        debugger;
        getAllCountriesforcity.then(function (emp) {
            $scope.country = emp.data;
        }, function () {
            alert('Error in getting records');
        });

    }


    $scope.getstatebyCountryId = function () {

        var Id = $scope.ddlCountry;
        var getstatebyCountryId = cities.getstatebyCountryId(Id)
        getstatebyCountryId.then(function (emp) {

            $scope.state = emp.data;

        }, function () {
            alert('Error in getting records');
        });
    }


    $scope.saveCity = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var citiess = {
                Id: $scope.txtCityId,
                CountryId: $("#ddlCountry").val(),
                StateId: $("#ddlState").val(),
                //CountryId: $scope.ddlCountry,
                //StateId: $scope.ddlState,
                Name: $scope.txtCityName,
                Status: $scope.chkstatus,
            }
            var getData = cities.AddCity(citiess);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                //$scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //  $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getallCities();
                $scope.clearAllTexbox();

            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getallCities() {
        $("#loader").fadeIn();
        var getCityData = cities.getallCities();
        debugger;
        getCityData.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    scrollX: true,
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }]
                });
            }, 2000);

            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }


    $scope.editCityById = function (employee) {

        var editCityById = cities.editCityById(employee.Id)
        editCityById.then(function (emp) {
            $scope.city = emp.data;
            $scope.txtCityId = $scope.city.Id;
            $("#ddlCountry").val($scope.city.CountryId);
            var countryID = $scope.city.CountryId; 
            //$scope.ddlCountry = $scope.city.CountryId;
            callServiceMethodForDDl(JSON.stringify([countryID]), "/api/StudentApi", "getAllstatebyCountryId", "ddlState", false, "", "", "", "");
            //$scope.getstatebyCountryId();
            $("#ddlState").val($scope.city.StateId);
            //$scope.ddlState = $scope.city.StateId;
            $scope.txtCityName = $scope.city.Name;

            $scope.chkstatus = employee.Status;

        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.deleteCityById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteCityById = cities.deleteCityById(employee.Id)
            deleteCityById.then(function (datas) {
                //   $scope.IsVisible = $scope.IsVisible ? false : true;
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                    // $scope.IsVisible = false;
                }, 2000);
                getallCities();

            }, function () {
                alert('Error in getting records');
            });
        }
    }
    $scope.clearAllTexbox = function () {
        $scope.city = null;
        $scope.txtCityId = null;
        $scope.ddlCountry = null;

        $scope.ddlState = null;
        $scope.txtCityName = null;

        $scope.chkstatus = true;
    }

});




//////////////////////////////--Course---/////////////////////////////

app.controller("addCourses", function ($scope, courses, $timeout) {
    // $scope.IsVisible = false;
    $scope.chkstatus = true;
    getallCourses();
    $scope.saveCourse = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var course = {
                Id: $scope.txtCourseId,
                Name: $scope.txtcourseName,
                Status: $scope.chkstatus,
                Desc: $scope.txtdesc,
                Code: $scope.txtCoursecoursecode,
                MiniumuAttendePer: $scope.txtcourseminimumattendance,
                AttendenceType: $scope.ddlAttendenceType,
                TotlWorkingDay: $scope.txtcoursetotalworkingdays,
                Syllabus: "",
                ID: $('#ddlSchoolName').val()
            }

            var getData = courses.saveCourse(course);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //  $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getallCourses();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }

    }

    function getallCourses() {
        $("#loader").fadeIn();
        var getCourseData = courses.getallCourses();
        debugger;
        getCourseData.then(function (emp) {
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
                        "targets": -1,
                        "orderable": false

                    }],
                });
            }, 2000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }
    $scope.editcourseById = function (employee) {

        var editcourseById = courses.editcourseById(employee.Id)
        editcourseById.then(function (emp) {
            //CHANGE FOR txtcourseminimumattendance
            $scope.course = emp.data;
            $scope.txtCourseId = $scope.course.Id;
            $scope.txtcourseName = $scope.course.Name;
            $scope.txtcourseminimumattendance = $scope.course.MiniumuAttendePer;
            $scope.txtdesc = employee.Desc;
            // $scope.txtcourseName = employee.Code;
            $scope.txtCoursecoursecode = employee.Code;
            //$scope.txtcourseminimumattendance = employee.MiniumuAttendePer;
            $scope.ddlAttendenceType = employee.AttendenceType;
            $scope.txtcoursetotalworkingdays = employee.TotlWorkingDay;

            $scope.ddlSyllabus = employee.Syllabus;

            $scope.chkstatus = employee.Status;
            $('#ddlSchoolName').val(employee["ID"])

        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.deleteCourseById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteCourseById = courses.deleteCourseById(employee.Id)
            deleteCourseById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getallCourses();

            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.clearAllTexbox = function () {

        $scope.txtCourseId = null;
        $scope.txtcourseName = null;

        $scope.txtdesc = null;
        // $scope.txtcourseName = employee.Code;
        $scope.txtCoursecoursecode = null;
        $scope.txtcourseminimumattendance = null;
        $scope.ddlAttendenceType = null;
        $scope.txtcoursetotalworkingdays = null;

        $scope.ddlSyllabus = null;

        $scope.chkstatus = true;
    }



})

/////////---------Batch---------///////////////
app.controller("addBatch", function ($scope, batchs, $timeout) {
    getallCourses();
    getAllBatches();
    $scope.chkstatus = true;
    function getallCourses() {
        var getCourseData = batchs.getallCourses();
        debugger;
        getCourseData.then(function (emp) {
            $scope.batch = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    function getAllBatches() {
        $("#loader").fadeIn();
        var getAllBatches = batchs.getAllBatches();
        debugger;
        getAllBatches.then(function (emp) {
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

    $scope.saveBatch = function () {
        var startdate = $scope.txtStartDate;
        var enddate = $scope.txtEndDate;
        if (new Date(startdate) > new Date(enddate)) {
            $("#diverrormsg").fadeIn();
            $scope.errorMsg = "Start Date should less than End Date!!";
            $("#loader").fadeOut();
            $timeout(function () {
                $("#diverrormsg").fadeOut();
            }, 4000);
            return false;
        }
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var batch = {
                Id: $scope.txtBatchId,
                Name: $scope.txtDesc,
                Status: $scope.chkstatus,
                CurrActive: $scope.chkstatus1,
                StartDate: $scope.txtStartDate,
                EndDate: $scope.txtEndDate,
                SchoolID: $('#ddlSchoolName').val()
            }

            var getData = batchs.saveBatch(batch);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 4000);
                getAllBatches();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    $scope.markactive = function (employee) {
        var batch = {
            Id: employee.Id,
            SchoolID: employee.SchoolID
        }
        var getData = batchs.markactive(batch);
        getData.then(function (msg) {

            $("#divsuccesmsg").fadeIn();
            $scope.successMsg = msg.data;
            $("#loader").fadeOut();
            $timeout(function () {
                $("#divsuccesmsg").fadeOut();
            }, 2000);
            getAllBatches();
        }, function () {
            alert('Error in adding record');
        });
    }
    $scope.editBatchById = function (employee) {
        var editBatchById = batchs.editBatchById(employee.Id)
        editBatchById.then(function (emp) {

            $scope.batdch = emp.data;
            $scope.txtBatchId = $scope.batdch.Id;
            $scope.ddlClass = $scope.batdch.Class;
            $scope.txtDesc = $scope.batdch.Name;
            $scope.txtStartDate = $scope.batdch.StartDate;
            $scope.txtEndDate = $scope.batdch.EndDate;
            $scope.txtMaxNumberOfStudent = $scope.batdch.maxNoOfStudent;
            $scope.chkstatus = $scope.batdch.Status;
            $scope.chkstatus1 = $scope.batdch.CurrActive;
            $('#ddlSchoolName').val(employee["SchoolID"])
        })
    }



    $scope.deleteBatchById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteBatchById = batchs.deleteBatchById(employee.Id)
            deleteBatchById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllBatches();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.txtBatchId = null;
        $scope.ddlClass = null;
        $scope.txtBatchName = null;
        $scope.txtStartDate = null;
        $scope.txtEndDate = null;
        $scope.txtMaxNumberOfStudent = null;
        $scope.chkstatus = true;
        $scope.chkstatus1 = true;
        $scope.txtDesc = true;
    }
})


///////////////////////////----Section----///////////////////////////////////////
app.controller("addSection", function ($scope, Sectionss, $timeout) {
    $scope.IsVisible = false;
    // getallClasses();
    getAllSections();
    $scope.chkstatus = true;
    function getallClasses() {
        var getallClasses = Sectionss.getallClasses();
        debugger;
        getallClasses.then(function (emp) {
            $scope.batch = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.saveSection = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var Section = {
                Id: $scope.txtSectionId,
                Name: $scope.txtSectionName,
                Status: $scope.chkstatus,
                Class: $('#ddlClass').val(),
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = Sectionss.saveSection(Section);
            getData.then(function (msg) {
              
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                $scope.clearAllTexbox();
                getAllSections();
            
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllSections() {
        $("#loader").fadeIn();
        var getAllSections = Sectionss.getAllSections();
        debugger;

        getAllSections.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": [-1, -5],
                        "orderable": false
                    }],
                    destroy: true,
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }


    $scope.editSectionById = function (employee) {


        var editSectionById = Sectionss.editSectionById(employee.Id)
        editSectionById.then(function (emp) {
            $('#ddlSchoolName').val(employee["SchoolID"]);
            var SchoolID = getControlValue("ddlSchoolName");
            callServiceMethodForDDl(JSON.stringify([SchoolID]), "/api/MasterAPI", "getAllClassesBYSchool", "ddlClass", false, "", "", "", "");

            $scope.sectionsss = emp.data;
            $scope.txtSectionId = $scope.sectionsss.Id;
            $('#ddlClass').val(employee["classid"]);

            $scope.txtSectionName = $scope.sectionsss.Name;

            $scope.chkstatus = $scope.sectionsss.Status;

        })
    }

    $scope.deleteSectionById = function (employee) {

        if (confirm('Are you sure,you want to delete')) {
            $("#loader").fadeIn();
            var deleteSectionById = Sectionss.deleteSectionById(employee.Id)
            deleteSectionById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllSections();

            }, function () {
                alert('Error in getting records');
            });
        }
    }


    $scope.clearAllTexbox = function () {


        $scope.txtSectionId = null;
        //$scope.ddlClass = null;
        $("#ddlClass").val(-1);
        $scope.txtSectionName = null;

        $scope.chkstatus = true;
        //$("#ddlSchoolName").val(0);
    }

})


///////////////////////////----Designation----///////////////////////////////////////
app.controller("addDesignation", function ($scope, desig, $timeout) {

    $scope.IsVisible = false;
    $scope.chkstatus = true;
    getAllDesignation();

    $scope.saveDesignation = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var designation = {
                Id: $scope.txtDesignationId,
                Name: $scope.txtDesignationName,
                Status: $scope.chkstatus,
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = desig.saveDesignation(designation);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllDesignation();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllDesignation() {
        $("#loader").fadeIn();

        var getAllDesignation = desig.getAllDesignation();
        debugger;
        getAllDesignation.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable(
                    {
                        'columnDefs': [{
                            "targets": -1,
                            "orderable": false
                        }],
                    }
                );
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }


    $scope.editDesignationById = function (employee) {

        var editDesignationById = desig.editDesignationById(employee.Id)
        editDesignationById.then(function (emp) {


            $scope.desg = emp.data;
            $scope.txtDesignationId = $scope.desg.Id;
            $scope.txtDesignationName = $scope.desg.Name;

            $scope.chkstatus = $scope.desg.Status;
            $('#ddlSchoolName').val(employee["SchoolID"])
        })
    }

    $scope.deleteDesignationById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteDesignationById = desig.deleteDesignationById(employee.Id)
            deleteDesignationById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllDesignation();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.txtDesignationId = null;
        $scope.txtDesignationName = null;

        $scope.chkstatus = true;

    }


});

///////////////////////////----Cast Category----///////////////////////////////////////
app.controller("addCastCategory", function ($scope, castcat, $timeout) {

    // $scope.IsVisible = false;
    getAllCasts();
    $scope.chkstatus = true;

    $scope.saveCasts = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var cast = {
                Id: $scope.txtCastId,
                Name: $scope.txtCastName,
                Status: $scope.chkstatus


            }

            var getData = castcat.saveCasts(cast);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                //  $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllCasts();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllCasts() {
        $("#loader").fadeIn();
        var getAllCasts = castcat.getAllCasts();
        debugger;
        getAllCasts.then(function (emp) {


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


    $scope.editCastById = function (employee) {

        var editCastById = castcat.editCastById(employee.Id)
        editCastById.then(function (emp) {


            $scope.cast = emp.data;
            $scope.txtCastId = $scope.cast.Id;
            $scope.txtCastName = $scope.cast.Name;

            $scope.chkstatus = $scope.cast.Status;

        })
    }


    $scope.deleteCastById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteCastById = castcat.deleteCastById(employee.Id)
            deleteCastById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllCasts();

            }, function () {
                alert('Error in getting records');
            });
        }

    }
    $scope.clearAllTexbox = function () {
        $scope.txtCastId = null;
        $scope.txtCastName = null;
        $scope.chkstatus = true;
    }
})

///////////////////////////----Status----///////////////////////////////////////
app.controller("addstatus", function ($scope, statees, $timeout) {
    //  $scope.IsVisible = false;
    getAllStatus();
    $scope.chkstatus = true;
    $scope.saveStatus = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var status = {
                Id: $scope.txtStatusId,
                Name: $scope.txtStatusName,
                Status: $scope.chkstatus


            }

            var getData = statees.saveStatus(status);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                //$scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //$scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllStatus();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllStatus() {
        $("#loader").fadeIn();
        var getAllStatus = statees.getAllStatus();
        debugger;
        getAllStatus.then(function (emp) {
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                    paging: true
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }

    $scope.editStatusId = function (employee) {
        $scope.txtStatusId = employee.Id;
        $scope.txtStatusName = employee.Name;

        $scope.chkstatus = employee.Status;

    }

    $scope.deleteStatusById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteStatusById = statees.deleteStatusById(employee.Id)
            deleteStatusById.then(function (datas) {
                $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $scope.IsVisible = false;
                }, 2000);
                getAllStatus();

            }, function () {
                alert('Error in getting records');
            });
        }
    }

    $scope.clearAllTexbox = function () {
        $scope.txtStatusId = null;
        $scope.txtStatusName = null;


        $scope.chkstatus = true;
    }

})
///////////////////////////----Role----///////////////////////////////////////
app.controller("addRoles", function ($scope, rolesss, $timeout) {
    //$scope.IsVisible = false;
    getAllRoles();
    $scope.chkstatus = true;

    $scope.saveRoles = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var role = {
                Id: $scope.txtRoleId,
                Name: $scope.txtRoleName,
                Status: $scope.chkstatus


            }

            var getData = rolesss.saveRoles(role);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);

                getAllRoles();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllRoles() {
        $("#loader").fadeIn();
        var getAllRoles = rolesss.getAllRoles();
        debugger;
        getAllRoles.then(function (emp) {
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                    paging: true
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.editRolesId = function (employee) {
        $scope.txtRoleId = employee.Id;
        $scope.txtRoleName = employee.Name;

        $scope.chkstatus = employee.Status;

    }

    $scope.deleteRolesById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteRolesById = rolesss.deleteRolesById(employee.Id)
            deleteRolesById.then(function (datas) {
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllRoles();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.txtRoleId = null;
        $scope.txtRoleName = null;


        $scope.chkstatus = true;
        $('#ddlSchoolName').val(0);
    }

})


///////////////////////////----Department----///////////////////////////////////////
app.controller("addDepartment", function ($scope, DepartService, $timeout) {

    getAllDepartment();
    $scope.chkstatus = true;
    $scope.saveDepartment = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var depart = {
                Id: $scope.txtDepartmentId,
                Name: $scope.txtDepartmentName,
                Status: $scope.chkstatus,
                School: $('#ddlSchoolName').val()


            }

            var getData = DepartService.saveDepartment(depart);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllDepartment();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllDepartment() {
        $("#loader").fadeIn();
        var getAllDepartment = DepartService.getAllDepartment();
        debugger;
        getAllDepartment.then(function (emp) {
            var OTable = $('#example1').DataTable();
            OTable.destroy();
            $scope.employees = emp.data;


            $timeout(function () {
                $('#example1').DataTable({
                    columnDefs: [{ orderable: false, targets: -1 }]
                    //, scrollX: true,
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }



    $scope.editDepartmentId = function (employee) {
        var data = employee;
        $scope.txtDepartmentId = employee.Id;
        $scope.txtDepartmentName = employee.Name;
        $scope.chkstatus = employee.Status;

        $("#ddlSchoolName").val(employee["ID"]);
    }

    $scope.deleteDepartmentById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteDepartmentById = DepartService.deleteDepartmentById(employee.Id)
            deleteDepartmentById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 2000);
                getAllDepartment();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.txtDepartmentId = null;
        $scope.txtDepartmentName = null;


        $scope.chkstatus = true;

    }


})

///////////////////////////----Qualfication----///////////////////////////////////////

///add sms//
//app.controller("addSchoolSms", function ($scope, DepartService, $timeout) {

//    getAllDepartment();
//    $scope.chkstatus = true;
//    $scope.saveSchool = function () {
//        if ($("#form1").valid()) {
//            $("#loader").fadeIn();
//            var depart = {
//                Id: $scope.txtDepartmentId,
//                Name: $scope.txtDepartmentName,
//                Status: $scope.chkstatus,
//                School: $('#ddlSchoolName').val()


//            }

//            var getData = DepartService.saveSchool(depart);
//            getData.then(function (msg) {

//                $("#divsuccesmsg").fadeIn();
//                $scope.successMsg = msg.data;
//                $("#loader").fadeOut();
//                $timeout(function () {
//                    $("#divsuccesmsg").fadeOut();
//                }, 2000);
//                getAllDepartment();
//                $scope.clearAllTexbox();
//                //clearAllTextBoxes("divAddCourses");
//            }, function () {
//                alert('Error in adding record');
//            });
//        }
//    }

//    function getAllDepartment() {
//        $("#loader").fadeIn();
//        var getAllDepartment = DepartService.getAllDepartment();
//        debugger;
//        getAllDepartment.then(function (emp) {
//            var OTable = $('#example1').DataTable();
//            OTable.destroy();
//            $scope.employees = emp.data;


//            $timeout(function () {
//                $('#example1').DataTable({
//                    columnDefs: [{ orderable: false, targets: -1 }]
//                    //, scrollX: true,
//                });
//            }, 1000);
//            $("#loader").fadeOut();
//        }, function () {
//            alert('Error in getting records');
//        });
//    }



//    $scope.editDepartmentId = function (employee) {
//        var data = employee;
//        $scope.txtDepartmentId = employee.Id;
//        $scope.txtDepartmentName = employee.Name;
//        $scope.chkstatus = employee.Status;

//        $("#ddlSchoolName").val(employee["ID"]);
//    }

//    $scope.deleteDepartmentById = function (employee) {
//        if (confirm('Are you sure,you want to delete?')) {
//            $("#loader").fadeIn();
//            var deleteDepartmentById = DepartService.deleteDepartmentById(employee.Id)
//            deleteDepartmentById.then(function (datas) {
//                $("#divsuccesmsg").fadeIn();
//                $scope.successMsg = datas.data;
//                $("#loader").fadeOut();
//                $timeout(function () {
//                    $("#divsuccesmsg").fadeOut();
//                }, 2000);
//                getAllDepartment();

//            }, function () {
//                alert('Error in getting records');
//            });
//        }

//    }

//    $scope.clearAllTexbox = function () {
//        $scope.txtDepartmentId = null;
//        $scope.txtDepartmentName = null;


//        $scope.chkstatus = true;

//    }


//})
//
app.controller("addQualfication", function ($scope, QualficationService, $timeout) {
    $scope.chkstatus = true;
    getAllQualfication();
    $scope.saveQualfication = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var qual = {
                Id: $scope.txtQualficationId,
                Name: $scope.txtQualficationName,
                Status: $scope.chkstatus,
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = QualficationService.saveQualfication(qual);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllQualfication();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllQualfication() {
        $("#loader").fadeIn();
        var getAllQualfication = QualficationService.getAllQualfication();
        debugger;
        getAllQualfication.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": -1,
                        "orderable": false
                    }],
                }

                );
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }


    $scope.editQualficationId = function (employee) {
        $scope.txtQualficationId = employee.Id;
        $scope.txtQualficationName = employee.Name;

        $scope.chkstatus = employee.Status;
        $('#ddlSchoolName').val(employee["SchoolID"])

    }

    $scope.deleteQualficationById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteQualficationById = QualficationService.deleteQualficationById(employee.Id)
            deleteQualficationById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllQualfication();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.txtQualficationId = null;
        $scope.txtQualficationName = null;
        $scope.chkstatus = true;
    }
});



///////////////////////////----Cast Category----///////////////////////////////////////
app.controller("addCasteCategory", function ($scope, CategoryService, $timeout) {
    // $scope.IsVisible = false;
    $scope.chkstatus = true;
    getAllCastCategory();
    $scope.saveCategoryCast = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var cat = {
                Id: $scope.txtcategoryId,
                Name: $scope.txtCategoryName,
                Status: $scope.chkstatus


            }

            var getData = CategoryService.saveCategoryCast(cat);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                // $scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //$scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllCastCategory();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllCastCategory() {
        $("#loader").fadeIn();
        var getAllCastCategory = CategoryService.getAllCastCategory();
        debugger;
        getAllCastCategory.then(function (emp) {
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    destroy: true,
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }


    $scope.editCategoryId = function (employee) {
        $scope.txtcategoryId = employee.Id;
        $scope.txtCategoryName = employee.Name;

        $scope.chkstatus = employee.Status;

    }

    $scope.deleteCategoryById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteCategoryById = CategoryService.deleteCategoryById(employee.Id)
            deleteCategoryById.then(function (datas) {
                //$scope.IsVisible = $scope.IsVisible ? false : true;
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllCastCategory();

            }, function () {
                alert('Error in getting records');
            });
        }

    }


    $scope.clearAllTexbox = function () {
        $scope.txtcategoryId = null;
        $scope.txtCategoryName = null;


        $scope.chkstatus = true;
    }
});


///////////////////////////----Stream----///////////////////////////////////////
app.controller("StreamController", function ($scope, StreamService, $timeout) {
    $scope.IsVisible = false;
    $scope.chkstatus = true;
    getAllStream();

    //getallClasses();

    function getallClasses() {
        var getallClasses = StreamService.getallClasses();
        debugger;
        getallClasses.then(function (emp) {
            $scope.batch = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.saveStream = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var stream = {
                Id: $scope.txtStreamId,
                Class: $('#ddlClass').val(),
                Name: $scope.txtStreamName,
                Status: $scope.chkstatus,
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = StreamService.saveStream(stream);
            getData.then(function (msg) {
               
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                 $scope.clearAllTexbox();
                getAllStream();
                
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllStream() {
        $("#loader").fadeIn();
        var getAllStream = StreamService.getAllStream();
        debugger;
      
        getAllStream.then(function (emp) {
            var t = $('#example1').DataTable();
            t.destroy();
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    'columnDefs': [{
                        "targets": [-1,-5],
                        "orderable": false
                    }],
                    destroy: true,
                    dom: 'lBfrtip',
                    bRetrieve: true,
                    bDestroy: true,
                    scrollX: true,
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }


    $scope.editStreamById = function (employee) {
        $scope.txtStreamId = employee.Id;
        $('#ddlSchoolName').val(employee["SchoolID"]);
        var SchoolID = getControlValue("ddlSchoolName");
        callServiceMethodForDDl(JSON.stringify([SchoolID]), "/api/MasterAPI", "getAllClassesBYSchool", "ddlClass", false, "", "", "", "");
        $('#ddlClass').val(employee["ClassId"]);
        $scope.txtStreamName = employee.Name;
        $scope.chkstatus = employee.Status;
    }

    $scope.deleteStreamById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteStreamById = StreamService.deleteStreamById(employee.Id)
            deleteStreamById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllStream();

            }, function () {
                alert('Error in getting records');
            });
        }

    }


    $scope.clearAllTexbox = function () {
        $scope.txtStreamId = null;
        //$scope.ddlClass = null;
        $('#ddlClass').val(-1);
        $scope.txtStreamName = null;
        $scope.chkstatus = true;
       // $("#ddlSchoolName").val(0);

    }
});


///////////////////////////----Document Master----///////////////////////////////////////
app.controller("documentController", function ($scope, DocumentService, $timeout) {
    $scope.IsVisible = false;
    $scope.chkstatus = true;
    getAllDocument();
    getallUserType();
    function getallUserType() {
        var getallUserType = DocumentService.getallUserType();
        debugger;
        getallUserType.then(function (emp) {
            $scope.country = emp.data;
        }, function () {
            alert('Error in getting records');
        });
    }



    $scope.saveDocument = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var doc = {
                Id: $scope.txtDocumentId,
                Name: $scope.txtDocumentName,
                desc: $scope.txtDocumentDesc,
                Status: $scope.chkstatus,
                userType:  $('#ddlUserType').val(),
                SchoolID: $('#ddlSchoolName').val()



            }

            var getData = DocumentService.saveDocument(doc);
            getData.then(function (msg) {
                $("#divsuccesmsg").fadeIn();
                //$scope.IsVisible = $scope.IsVisible ? false : true;
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    // $scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllDocument();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }



    function getAllDocument() {
        $("#loader").fadeIn();
        var getAllDocument = DocumentService.getAllDocument();
        debugger;
        getAllDocument.then(function (emp) {
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


    $scope.editDocumentId = function (employee) {
        $scope.txtDocumentId = employee.Id;
        $scope.txtDocumentName = employee.Name;
        $scope.txtDocumentDesc = employee.desc;
        $scope.chkstatus = employee.Status;
        $('#ddlUserType').val(employee["userId"]);
        $('#ddlSchoolName').val(employee["SchoolID"])

    }

    $scope.deleteDocumentById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteDocumentById = DocumentService.deleteDocumentById(employee.Id)
            deleteDocumentById.then(function (datas) {
                //  $scope.IsVisible = $scope.IsVisible ? false : true;
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    //$scope.IsVisible = false;
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllDocument();

            }, function () {
                alert('Error in getting records');
            });
        }

    }


    $scope.clearAllTexbox = function () {
        $scope.chkstatus = true;
        $scope.txtDocumentId = null;
        $scope.txtDocumentName = null;
        $scope.txtDocumentDesc = null;
        $scope.ddlUserType = null;
    }

});



///////////////////////////----Document Master----///////////////////////////////////////
app.controller("CodeGeController", function ($scope, CodeService, $timeout) {
    $scope.chkStatus = true;
    $scope.ddlDocumentType = "Student";
    getAllDocumentNo();
    $scope.saveCodeGeneration = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var doc = {
                Id: $scope.txtDocumentId,
                DocType: $scope.ddlDocumentType,
                Prefix: $scope.txtPrefix,
                Status: $scope.chkStatus,
                Suffix: $scope.txtSuffix,
                //DocNo: $scope.txtStatt,
                //StartSeries: $scope.txtSeriesStart,
                //Seprator: $scope.txtSeperator,
                DocNo: 1,
                StartSeries: 1,
                Seprator: '/',
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = CodeService.saveCodeGeneration(doc);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllDocumentNo();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    function getAllDocumentNo() {
        $("#loader").fadeIn();
        var getAllDocumentNo = CodeService.getAllDocumentNo();
        debugger;
        getAllDocumentNo.then(function (emp) {
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


    $scope.editCodeDocuemntById = function (employee) {
        $scope.txtDocumentId = employee.Id;
        $scope.ddlDocumentType = employee.DocType;
        $scope.txtPrefix = employee.Prefix;
        $scope.chkStatus = employee.Status;
        $scope.txtSuffix = employee.Suffix;

        $scope.txtStatt = employee.DocNo;
        $scope.txtSeriesStart = employee.StartSeries;
        $scope.txtSeperator = employee.Seprator;
        $('#ddlSchoolName').val(employee["SchoolID"])
    }

    $scope.deleteCodeDocumentById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteDocumentById = CodeService.deleteCodeDocumentById(employee.Id)
            deleteDocumentById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllDocumentNo();

            }, function () {
                alert('Error in getting records');
            });
        }

    }

    $scope.clearAllTexbox = function () {
        $scope.chkstatus = true;


        $scope.txtDocumentId = null;
        $scope.ddlDocumentType = "Student";
        $scope.txtPrefix = null;

        $scope.txtSuffix = null;

        //$scope.txtStatt = null;
        //$scope.txtSeriesStart = null;
        //$scope.txtSeperator = null;
    }
});

///////////////////////////----Subject Master----///////////////////////////////////////
app.controller("subjectss", function ($scope, SubjectService, $timeout) {
    $scope.chkstatus = true;
    getAllSubjects();




    $scope.saveSubjects = function () {
        if ($("#form1").valid()) {
            $("#loader").fadeIn();
            var sub = {
                Id: $scope.txtSubjectId,
                Name: $scope.txtSubjectName,
                Code: $scope.txtSubjectCode,
                Status: $scope.chkstatus,
                Desc: $scope.txtSubjectDescription,
                classid: $('#ddlCourse').val(),
                SchoolID: $('#ddlSchoolName').val()

            }

            var getData = SubjectService.saveSubjectsDetails(sub);
            getData.then(function (msg) {

                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = msg.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllSubjects();
                $scope.clearAllTexbox();
                //clearAllTextBoxes("divAddCourses");
            }, function () {
                alert('Error in adding record');
            });
        }
    }


    function getAllSubjects() {
        $("#loader").fadeIn();
        var OTable = $('#example1').DataTable();
        OTable.destroy();
        var getAllSubjects = SubjectService.getAllSubjects();
        debugger;
        getAllSubjects.then(function (emp) {
            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable({
                    //destroy: true,
                    //dom: 'lBfrtip',
                    //bRetrieve: true,
                    //bDestroy: true,
                    //scrollX: true,
                    columnDefs: [{ orderable: false, targets: -1 }, { orderable: false,targets: -7 }],
                   
                });
            }, 1000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.editSubjectByID = function (employee) {
        $scope.txtSubjectId = employee.Id;
        $scope.txtSubjectName = employee.Name;
        $scope.txtSubjectCode = employee.Code;
        $scope.chkstatus = employee.Status;
        $scope.txtSubjectDescription = employee.Desc;
        $('#ddlSchoolName').val(employee["SchoolID"]);
        $('#ddlCourse').val(employee["classid"])
    }

    $scope.clearAllTexbox = function () {
        $scope.txtSubjectId = null;
        $scope.txtSubjectName = null;
        $scope.txtSubjectCode = null;
        $scope.chkstatus = true;
        $scope.txtSubjectDescription = null;
        //$("#ddlSchoolName").val(0);
        $('#ddlCourse').val(0)
    }

    $scope.deleteSubjectById = function (employee) {
        if (confirm('Are you sure,you want to delete?')) {
            $("#loader").fadeIn();
            var deleteDocumentById = SubjectService.deleteSubjectById(employee.Id)
            deleteDocumentById.then(function (datas) {
                $("#divsuccesmsg").fadeIn();
                $scope.successMsg = datas.data;
                $("#loader").fadeOut();
                $timeout(function () {
                    $("#divsuccesmsg").fadeOut();
                }, 3000);
                getAllSubjects();

            }, function () {
                alert('Error in getting records');
            });
        }

    }


});


///////////////////////////----School Master----///////////////////////////////////////
app.controller("SchoolController", function ($scope, SchoolService, $timeout) {
    $scope.chkstatus = true;
    GetAllSchoolDetail();

    function GetAllSchoolDetail() {

        $("#loader").fadeIn();
        var GetAllSchoolDetail = SchoolService.GetAllSchoolDetail();
        debugger;
        GetAllSchoolDetail.then(function (emp) {

            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').DataTable();
            }, 1000);

            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });

    }
    function getallCities() {
        $("#loader").fadeIn();
        var getCityData = cities.getallCities();
        debugger;
        getCityData.then(function (emp) {

            $scope.employees = emp.data;
            $timeout(function () {
                $('#example1').dataTable({
                    retrieve: true,
                    destroy: true,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
            }, 2000);
            $("#loader").fadeOut();
        }, function () {
            alert('Error in getting records');
        });
    }






    //$scope.deleteSchoolById = function (employee) {
    //    if (confirm('are you sure?')) {
    //        $("#loader").fadeIn();
    //      //  var deleteSchoolById = statss.deleteSchoolById(employee.Id)
    //        var deleteSchoolById = SchoolService.deleteSchoolById(employee.Id);
    //        deleteSchoolById.then(function (datas) {
    //            $("#divsuccesmsg").fadeIn();
    //            // $scope.IsVisible = $scope.IsVisible ? false : true;
    //            $scope.successMsg = datas.data;
    //            $("#loader").fadeOut();
    //            $timeout(function () {
    //                //  $scope.IsVisible = false;
    //                $("#divsuccesmsg").fadeOut();
    //            }, 2000);
    //            //getAllState();

    //        }, function () {
    //            alert('Error in getting records');
    //        });
    //    }
    //}



});
