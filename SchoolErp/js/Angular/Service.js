app.service("myService", function ($http) {
    this.AddCountry = function (country) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCountry",
            data: JSON.stringify(country),
            dataType: "json"
        });
        return response;
    }

    this.getAllCountries = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllCountry",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.editCountryById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/testmethod",
            data: "{'Id':'"+Id+"'}",
            dataType: "json"
        });
        return response;
    }


    // get Employee By Id
    this.getCountryById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getCountryById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
    this.deleteCountryById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCountryById",
            params: {
                id: Id
            }
        });
        return response;
    }

    
  



   
})

/////////////--------State--------///////////////
app.service("statss", function ($http) {

    this.AddState = function (statee) {

        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveState",
            data: JSON.stringify(statee),
            dataType: "json"
        });
        return response;
    }


    this.getAllCountriesforState = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllCountryforState",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.getAllState = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllState",
            data: "",
            dataType: "json"
        });
        return response;
    }


    this.editStateById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editStateById",
            params: {
                id: Id
            }
        });
        return response;
    }

    this.deleteStateById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteStateById",
            params: {
                id: Id
            }
        });
        return response;
    }
    

});


    /////////////--Cities---/////////////////

app.service("cities", function ($http) {
    this.AddCity = function (citiess) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCities",
            data: JSON.stringify(citiess),
            dataType: "json"
        });
        return response;
    }


    this.getAllCountriesforcity = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllCountryforState",
            data: "",
            dataType: "json"
        });
        return response;
    }
    this.getstatebyCountryId = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getstatebyCountryId",
            params: {
                id: Id
            }
        });
        return response;
    }

    this.getallCities = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getallCities",
            data: "",
            dataType: "json"
        });
        return response;
    }
    

    this.editCityById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editCityById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
    this.deleteCityById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCityById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
});

    
    

    

//////////----Course-----////////////////////
app.service("courses", function ($http) {
    this.saveCourse = function (course) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCourse",
            data: JSON.stringify(course),
            dataType: "json"
        });
        return response;
    }

    this.getallCourses = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getallCourses",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.editcourseById = function (Id)
    {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editcourseById",
            params: {
                id: Id
            }
        });
        return response;

    }

    this.deleteCourseById = function (Id)
    {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCourseById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
}
)
/////////////--------State--------///////////////
app.service("batchs", function ($http) {
    this.getallCourses = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllClasses",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }


    this.saveBatch = function (batch) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveBatch",
            data: JSON.stringify(batch),
            dataType: "json"
        });
        return response;
    }

    this.markactive = function (batch) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/markactive",
            data: JSON.stringify(batch),
            dataType: "json"
        });
        return response;
    }


    this.getAllBatches = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllBatches",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.editBatchById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editBatchById",
            params: {
                id: Id
            }
        });
        return response;

    }


    this.deleteBatchById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteBatchById",
            params: {
                id: Id
            }
        });
        return response;
    }
    

    

})

///////////////////////////----Section----///////////////////////////////////////
app.service("Sectionss", function ($http) {
    this.getallClasses = function () {

        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllClasses",
            data: "",
            dataType: "json"
        });
        return response;
    }



    this.saveSection = function (batch) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveSection",
            data: JSON.stringify(batch),
            dataType: "json"
        });
        return response;
    }
    
    this.getAllSections = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllSections",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.editSectionById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editSectionById",
            params: {
                id: Id
            }
        });
        return response;

    }

    
    this.deleteSectionById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteSectionById",
            params: {
                id: Id
            }
        });
        return response;
    }

    
    
});


///////////////////////////----Designation----///////////////////////////////////////
app.service("desig", function ($http) {
    
    this.saveDesignation = function (designation) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveDesignation",
            data: JSON.stringify(designation),
            dataType: "json"
        });
        return response;
    }

    this.getAllDesignation = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllDesignation",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.editDesignationById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editDesignationById",
            params: {
                id: Id
            }
        });
        return response;

    }

    this.deleteDesignationById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteDesignationById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
    
})

///////////////////////////----Cast Category----///////////////////////////////////////
app.service("castcat", function ($http) {
    this.saveCasts = function (cast) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCasts",
            data: JSON.stringify(cast),
            dataType: "json"
        });
        return response;
    }

    this.getAllCasts = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllCasts",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.editCastById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/editCastById",
            params: {
                id: Id
            }
        });
        return response;

    }

    
    this.deleteCastById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCastById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
    
})

///////////////////////////----Status---///////////////////////////////////////
app.service("statees", function ($http) {
    this.saveStatus = function (status) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveStatus",
            data: JSON.stringify(status),
            dataType: "json"
        });
        return response;
    }
    
    this.getAllStatus = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllStatus",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.deleteStatusById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteStatusById",
            params: {
                id: Id
            }
        });
        return response;
    }

    
    
})

///////////////////////////----Status---///////////////////////////////////////
app.service("rolesss", function ($http) {

    this.saveRoles = function (role) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveRoles",
            data: JSON.stringify(role),
            dataType: "json"
        });
        return response;
    }
    
    this.getAllRoles = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllRoles",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.deleteRolesById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteRolesById",
            params: {
                id: Id
            }
        });
        return response;
    }
    
    
})


///////////////////////////----Department---///////////////////////////////////////
app.service("DepartService", function ($http) {
    
    this.saveDepartment = function (depart) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveDepartment",
            data: JSON.stringify(depart),
            dataType: "json"
        });
        return response;
    }

    this.getAllDepartment = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllDepartment",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deleteDepartmentById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteDepartmentById",
            params: {
                id: Id
            }
        });
        return response;
    }

    

    
})

//SchoolSms

//app.service("SchoolService", function ($http) {

//    this.saveSchool = function (depart) {
//        var response = $http({
//            method: "post",
//            url: "/api/MasterAPI/saveSchool",
//            data: JSON.stringify(depart),
//            dataType: "json"
//        });
//        return response;
//    }

//    this.getAllDepartment = function () {
//        var loginuser = (sessionStorage.getItem("userId"));
//        var typeuser = sessionStorage.getItem("type");
//        var response = $http({
//            method: "post",
//            url: "/api/MasterAPI/getAllDepartment",
//            data: JSON.stringify([loginuser, typeuser]),
//            dataType: "json"
//        });
//        return response;
//    }

//    this.deleteDepartmentById = function (Id) {
//        var response = $http({
//            method: "post",
//            url: "/api/MasterAPI/deleteDepartmentById",
//            params: {
//                id: Id
//            }
//        });
//        return response;
//    }




//})
//SchoolSMS

///////////////////////////----Department---///////////////////////////////////////
app.service("QualficationService", function ($http) {
    

    this.saveQualfication = function (qual) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveQualfication",
            data: JSON.stringify(qual),
            dataType: "json"
        });
        return response;
    }

    this.getAllQualfication = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllQualfication",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deleteQualficationById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteQualficationById",
            params: {
                id: Id
            }
        });
        return response;
    }


    
});



///////////////////////////----Category Cast---///////////////////////////////////////
app.service("CategoryService", function ($http) {


    this.saveCategoryCast = function (cat) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCategoryCast",
            data: JSON.stringify(cat),
            dataType: "json"
        });
        return response;
    }

    this.getAllCastCategory = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllCastCategory",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.deleteCategoryById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCategoryById",
            params: {
                id: Id
            }
        });
        return response;
    }



});



///////////////////////////----Category Cast---///////////////////////////////////////
app.service("StreamService", function ($http) {

    this.getallClasses = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllClasses",
            data: "",
            dataType: "json"
        });
        return response;
    }

    this.saveStream = function (stream) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveStream",
            data: JSON.stringify(stream),
            dataType: "json"
        });
        return response;
    }

    this.getAllStream = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllStream",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }

    this.deleteStreamById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteStreamById",
            params: {
                id: Id
            }
        });
        return response;
    }


    

});




///////////////////////////----Document---///////////////////////////////////////
app.service("DocumentService", function ($http) {


    this.saveDocument = function (cat) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveDocument",
            data: JSON.stringify(cat),
            dataType: "json"
        });
        return response;
    }

    this.getAllDocument = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllDocument",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }
    this.getallUserType = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getallUserType",
            data: "",
            dataType: "json"
        });
        return response;
    }


    this.deleteDocumentById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteDocumentById",
            params: {
                id: Id
            }
        });
        return response;
    }




});




///////////////////////////----Document---///////////////////////////////////////
app.service("CodeService", function ($http) {


    this.saveCodeGeneration = function (doc) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveCodeGeneration",
            data: JSON.stringify(doc),
            dataType: "json"
        });
        return response;
    }

    this.getAllDocumentNo = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllDocumentNo",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }
 


    this.deleteCodeDocumentById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteCodeDocumentById",
            params: {
                id: Id
            }
        });
        return response;
    }




});

/////////////--------Subjects--------///////////////
app.service("SubjectService", function ($http) {

    this.saveSubjectsDetails = function (sub) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/saveSubjectsDetails",
            data: JSON.stringify(sub),
            dataType: "json"
        });
        return response;
    }

    this.getAllSubjects = function () {
        var loginuser = (sessionStorage.getItem("userId"));
        var typeuser = sessionStorage.getItem("type");
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/getAllSubjects",
            data: JSON.stringify([loginuser, typeuser]),
            dataType: "json"
        });
        return response;
    }


    this.deleteSubjectById = function (Id) {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/deleteSubjectById",
            params: {
                id: Id
            }
        });
        return response;
    }



  

});


///////////////// New School /////////////////
app.service("SchoolService", function ($http) {


    this.GetAllSchoolDetail = function () {
        var response = $http({
            method: "post",
            url: "/api/MasterAPI/GetAllSchoolDetail",
            data: "",
            dataType: "json"
        });
        return response;
    }
});

