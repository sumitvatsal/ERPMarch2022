using ClosedXML.Excel;
using SchoolErp.Models;
using schoolERP_BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

using System.Net.Mail;
using System.Net.Configuration;



namespace SchoolErp.Controllers
{
    [Authorize]
    public class ImportExportController : Controller
    {
        // GET: ImportExport
        public ActionResult EmployeeDetailsImport()
        {
            return View();
        }
        public ActionResult UploadExcel()
        {
            return View("EmployeeDetailsImport");
        }

        [HttpPost]
        public ActionResult EmployeeDetailsImport(HttpPostedFileBase FileUpload)
        {
            sqlHelper obj = new sqlHelper();
            List<string> data = new List<string>();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)Session["schoolid"];

            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                  
                   
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);

                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    for (int i = dtable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtable.Rows[i][1] == DBNull.Value)
                            dtable.Rows[i].Delete();
                    }
                    dtable.AcceptChanges();
                    string chkdept = "";
                    string chkdesig = "";
                    string chkquali = "";
                    string chkstafftype = "";
                    string chkgender = "";
                    string chkmaritalstatus = "";
                    string chkreligion = "";
                    string chkcountry = "";
                    string chkstate = "";
                    string chkcity = "";
                    //string chkuserid = "";
                    string chkbiometricid = "";
                    //var repeatempcode = "";
                    //var repeateuserid = "";
                    var repeatbiometriccode = "";
                    var emptyflg = 0;
                    foreach (DataRow dr in dtable.Rows)
                    {
                        string JoiningDate = dr["JoiningDate"].ToString();
                        string Department = dr["Department"].ToString();
                        string Designation = dr["Designation"].ToString();
                        string Qualification = dr["Qualification"].ToString();
                        string TotalExperience = dr["TotalExperience"].ToString();
                        string StaffType = dr["StaffType"].ToString();
                        string FirstName = dr["FirstName"].ToString();
                        string DOB = dr["DOB"].ToString();
                        string Gender = dr["Gender"].ToString();
                        string Country = dr["Country"].ToString();
                        string State = dr["State"].ToString();
                        string City = dr["City"].ToString();
                        string EmailAddress = dr["EmailAddress"].ToString();
                        string PresentAddress = dr["PresentAddress"].ToString();
                        string PermanentAddress = dr["PermanentAddress"].ToString();
                        string MaritalStatus = dr["MaritalStatus"].ToString();
                        //string userid = dr["UserID"].ToString();
                        //string pass = dr["Password"].ToString();
                        if (JoiningDate == "" || Department == "" || Designation == "" || Qualification == "" || TotalExperience == "" || StaffType == "" || FirstName == "" || DOB == "" || Gender == "" || Country == "" || State == "" || City == "" || EmailAddress == "" || PresentAddress == "" || PermanentAddress == "" || MaritalStatus == "" /*|| userid == "" || pass == ""*/)
                        {
                            emptyflg = emptyflg + 1;
                        }
                    }
                    if (emptyflg > 0)
                    {
                        var msg = "JoiningDate, Department, Designation, Qualification, TotalExperience, StaffType, FirstName, DOB, Gender, Country, State, City, EmailAddress, PresentAddress, PermanentAddress, MaritalStatus/*, UserID and Password */fields can not be blank!!";
                        ViewBag.ImportStatusFlag = "error";
                        ViewBag.ImportStatus = msg;
                        return View("EmployeeDetailsImport");
                    }
                    else
                    {

                        //foreach (DataRow dr in dtable.Rows)
                        //{
                        //    var selemp = dr["UserID"].ToString();
                        //    int chktc = 0;
                        //    foreach (DataRow dr1 in dtable.Rows)
                        //    {
                        //        if (selemp == dr1["UserID"].ToString())
                        //        {
                        //            chktc = chktc + 1;
                        //        }
                        //    }
                        //    if (chktc > 1)
                        //    {
                        //        if (repeateuserid == "")
                        //        {
                        //            repeateuserid = selemp;
                        //        }
                        //        else
                        //        {
                        //            if (!repeateuserid.Contains(selemp))
                        //            {
                        //                repeateuserid = repeateuserid + ',' + selemp;
                        //            }
                        //        }
                        //    }
                        //}

                        //if (!String.IsNullOrEmpty(repeatempcode))
                        //{
                        //    var msg = "UserID(s): " + repeateuserid + " is/are repeating";
                        //    ViewBag.ImportStatusFlag = "error";
                        //    ViewBag.ImportStatus = msg;
                        //    return View("EmployeeDetailsImport");
                        //}
                        //else
                        //{
                        foreach (DataRow dr in dtable.Rows)
                        {
                            var selemp = dr["BiometricID"].ToString();
                            int chktc = 0;
                            foreach (DataRow dr1 in dtable.Rows)
                            {
                                if (selemp == "")
                                {
                                }
                                else
                                {
                                    if (selemp == dr1["BiometricID"].ToString())
                                    {
                                        chktc = chktc + 1;
                                    }
                                }
                            }
                            if (chktc > 1)
                            {
                                if (repeatbiometriccode == "")
                                {
                                    repeatbiometriccode = selemp;
                                }
                                else
                                {
                                    if (!repeatbiometriccode.Contains(selemp))
                                    {
                                        repeatbiometriccode = repeatbiometriccode + ',' + selemp;
                                    }
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(repeatbiometriccode))
                        {
                            var msg = "BiometricID(s): " + repeatbiometriccode + " is/are repeating";
                            ViewBag.ImportStatusFlag = "error";
                            ViewBag.ImportStatus = msg;
                            return View("EmployeeDetailsImport");
                        }
                        else
                        {
                            foreach (DataRow dr in dtable.Rows)
                            {
                                //if (chkuserid == "")
                                //{
                                //    chkuserid = dr["UserID"].ToString();
                                //}
                                //else
                                //{
                                //    if (!chkuserid.Contains(dr["UserID"].ToString()))
                                //    {
                                //        chkuserid = chkuserid + ',' + dr["UserID"].ToString();
                                //    }

                                //}

                                if (chkbiometricid == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["BiometricID"].ToString()))
                                    {
                                        chkbiometricid = dr["BiometricID"].ToString();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["BiometricID"].ToString()))
                                    {
                                        if (!chkbiometricid.Contains(dr["BiometricID"].ToString()))
                                        {
                                            chkbiometricid = chkbiometricid + ',' + dr["BiometricID"].ToString();
                                        }
                                    }
                                }

                                if (chkdept == "")
                                {
                                    chkdept = dr["Department"].ToString();
                                }
                                else
                                {
                                    if (!chkdept.Contains(dr["Department"].ToString()))
                                    {
                                        chkdept = chkdept + ',' + dr["Department"].ToString();
                                    }
                                }

                                if (chkdesig == "")
                                {
                                    chkdesig = dr["Designation"].ToString();
                                }
                                else
                                {
                                    if (!chkdesig.Contains(dr["Designation"].ToString()))
                                    {
                                        chkdesig = chkdesig + ',' + dr["Designation"].ToString();
                                    }
                                }

                                if (chkquali == "")
                                {
                                    chkquali = dr["Qualification"].ToString();
                                }
                                else
                                {
                                    if (!chkquali.Contains(dr["Qualification"].ToString()))
                                    {
                                        chkquali = chkquali + ',' + dr["Qualification"].ToString();
                                    }
                                }

                                if (chkstafftype == "")
                                {
                                    chkstafftype = dr["StaffType"].ToString();
                                }
                                else
                                {
                                    if (!chkstafftype.Contains(dr["StaffType"].ToString()))
                                    {
                                        chkstafftype = chkstafftype + ',' + dr["StaffType"].ToString();
                                    }
                                }

                                if (chkgender == "")
                                {
                                    chkgender = dr["Gender"].ToString();
                                }
                                else
                                {
                                    if (!chkgender.Contains(dr["Gender"].ToString()))
                                    {
                                        chkgender = chkgender + ',' + dr["Gender"].ToString();
                                    }
                                }

                                if (chkmaritalstatus == "")
                                {
                                    chkmaritalstatus = dr["MaritalStatus"].ToString();
                                }
                                else
                                {
                                    if (!chkmaritalstatus.Contains(dr["MaritalStatus"].ToString()))
                                    {
                                        chkmaritalstatus = chkmaritalstatus + ',' + dr["MaritalStatus"].ToString();
                                    }
                                }

                                if (chkreligion == "")
                                {
                                    chkreligion = dr["Religon"].ToString();
                                }
                                else
                                {
                                    if (!chkreligion.Contains(dr["Religon"].ToString()))
                                    {
                                        chkreligion = chkreligion + ',' + dr["Religon"].ToString();
                                    }
                                }

                                if (chkcountry == "")
                                {
                                    chkcountry = dr["Country"].ToString();
                                }
                                else
                                {
                                    if (!chkcountry.Contains(dr["Country"].ToString()))
                                    {
                                        chkcountry = chkcountry + ',' + dr["Country"].ToString();
                                    }
                                }

                                if (chkstate == "")
                                {
                                    chkstate = dr["State"].ToString();
                                }
                                else
                                {
                                    if (!chkstate.Contains(dr["State"].ToString()))
                                    {
                                        chkstate = chkstate + ',' + dr["State"].ToString();
                                    }
                                }

                                if (chkcity == "")
                                {
                                    chkcity = dr["City"].ToString();
                                }
                                else
                                {
                                    if (!chkcity.Contains(dr["City"].ToString()))
                                    {
                                        chkcity = chkcity + ',' + dr["City"].ToString();
                                    }
                                }
                            }

                            //int tc11 = 0;
                            //string chkuid = "";
                            //string[] arr11 = chkuserid.Split(',');
                            //for (int i = 0; i < arr11.Length; i++)
                            //{
                            //    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblemployee where isdeleted is null and UserID='" + arr11[i] + "'"));
                            //    if (chktc > 0)
                            //    {
                            //        tc11 = tc11 + 1;
                            //        if (chkuid == "")
                            //        {
                            //            chkuid = arr11[i];
                            //        }
                            //        else
                            //        {
                            //            chkuid = chkuid + ", " + arr11[i];
                            //        }
                            //    }
                            //}

                            //if (tc11 > 0)
                            //{
                            //    var msg = "UserID(s) " + chkuid + " is/are already exist";
                            //    ViewBag.ImportStatusFlag = "error";
                            //    ViewBag.ImportStatus = msg;
                            //    return View("EmployeeDetailsImport");
                            //}
                            //else
                            //{

                            int tc12 = 0;
                            string chkbioid = "";
                            string[] arr12 = chkbiometricid.Split(',');
                            for (int i = 0; i < arr12.Length; i++)
                            {
                                int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblemployee where isdeleted is null and biometricid='" + arr12[i] + "'"));
                                if (chktc > 0)
                                {
                                    tc12 = tc12 + 1;
                                    if (chkbioid == "")
                                    {
                                        chkbioid = arr12[i];
                                    }
                                    else
                                    {
                                        chkbioid = chkbioid + ", " + arr12[i];
                                    }
                                }
                            }

                            if (tc12 > 0)
                            {
                                var msg = "BiometricID(s) " + chkbioid + " is/are already exist";
                                ViewBag.ImportStatusFlag = "error";
                                ViewBag.ImportStatus = msg;
                                return View("EmployeeDetailsImport");

                            }
                            else
                            {

                                int tc1 = 0;
                                string deptname = "";
                                string[] arr1 = chkdept.Split(',');
                                for (int i = 0; i < arr1.Length; i++)
                                {
                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblDepartmnet where IsDeleted is null and schoolid='" + Schoolid + "' and DepartmentName in ('" + arr1[i] + "')"));
                                    if (chktc == 0)
                                    {
                                        tc1 = tc1 + 1;
                                        if (deptname == "")
                                        {
                                            deptname = arr1[i];
                                        }
                                        else
                                        {
                                            deptname = deptname + ", " + arr1[i];
                                        }
                                    }
                                }
                                if (tc1 > 0)
                                {
                                    var msg = "Department(s): " + deptname + " not exist";
                                    ViewBag.ImportStatusFlag = "error";
                                    ViewBag.ImportStatus = msg;
                                    return View("EmployeeDetailsImport");

                                }
                                else
                                {
                                    int tc2 = 0;
                                    string designame = "";
                                    string[] arr2 = chkdesig.Split(',');
                                    for (int i = 0; i < arr2.Length; i++)
                                    {
                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblDesignation where IsDeleted is null and schoolid='" + Schoolid + "' and Designation in ('" + arr2[i] + "')"));
                                        if (chktc == 0)
                                        {
                                            tc2 = tc2 + 1;
                                            if (designame == "")
                                            {
                                                designame = arr2[i];
                                            }
                                            else
                                            {
                                                designame = designame + ", " + arr2[i];
                                            }
                                        }
                                    }
                                    if (tc2 > 0)
                                    {
                                        var msg = "Designation(s): " + designame + " not exist";
                                        ViewBag.ImportStatusFlag = "error";
                                        ViewBag.ImportStatus = msg;
                                        return View("EmployeeDetailsImport");
                                    }
                                    else
                                    {
                                        int tc3 = 0;
                                        string qualiname = "";
                                        string[] arr3 = chkquali.Split(',');
                                        for (int i = 0; i < arr3.Length; i++)
                                        {
                                            int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblqualifications where IsDeleted is null and schoolid='" + Schoolid + "' and QualificationName in ('" + arr3[i] + "')"));
                                            if (chktc == 0)
                                            {
                                                tc3 = tc3 + 1;
                                                if (qualiname == "")
                                                {
                                                    qualiname = arr3[i];
                                                }
                                                else
                                                {
                                                    qualiname = qualiname + ", " + arr3[i];
                                                }
                                            }
                                        }
                                        if (tc3 > 0)
                                        {
                                            var msg = "Qualification(s): " + qualiname + " not exist";
                                            ViewBag.ImportStatusFlag = "error";
                                            ViewBag.ImportStatus = msg;
                                            return View("EmployeeDetailsImport");
                                        }
                                        else
                                        {
                                            int tc4 = 0;
                                            string stafftype = "";
                                            string[] arr4 = chkstafftype.Split(',');
                                            for (int i = 0; i < arr4.Length; i++)
                                            {
                                                int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblstaffcategory where Name in ('" + arr4[i] + "')"));
                                                if (chktc == 0)
                                                {
                                                    tc4 = tc4 + 1;
                                                    if (stafftype == "")
                                                    {
                                                        stafftype = arr4[i];
                                                    }
                                                    else
                                                    {
                                                        stafftype = stafftype + ", " + arr4[i];
                                                    }
                                                }
                                            }
                                            if (tc4 > 0)
                                            {
                                                var msg = "Staff type(s): " + stafftype + " not exist";
                                                ViewBag.ImportStatusFlag = "error";
                                                ViewBag.ImportStatus = msg;
                                                return View("EmployeeDetailsImport");

                                            }
                                            else
                                            {
                                                int tc5 = 0;
                                                string gender = "";
                                                string[] arr5 = chkgender.Split(',');
                                                for (int i = 0; i < arr5.Length; i++)
                                                {
                                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblgender where GenderName in ('" + arr5[i] + "')"));
                                                    if (chktc == 0)
                                                    {
                                                        tc5 = tc5 + 1;
                                                        if (gender == "")
                                                        {
                                                            gender = arr5[i];
                                                        }
                                                        else
                                                        {
                                                            gender = gender + ", " + arr5[i];
                                                        }
                                                    }
                                                }
                                                if (tc5 > 0)
                                                {
                                                    var msg = "Gender(s): " + gender + " not exist";
                                                    ViewBag.ImportStatusFlag = "error";
                                                    ViewBag.ImportStatus = msg;
                                                    return View("EmployeeDetailsImport");
                                                }
                                                else
                                                {
                                                    int tc6 = 0;
                                                    string maritalstatus = "";
                                                    string[] arr6 = chkmaritalstatus.Split(',');
                                                    for (int i = 0; i < arr6.Length; i++)
                                                    {
                                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblmaritalstatus where name in ('" + arr6[i] + "')"));
                                                        if (chktc == 0)
                                                        {
                                                            tc6 = tc6 + 1;
                                                            if (maritalstatus == "")
                                                            {
                                                                maritalstatus = arr6[i];
                                                            }
                                                            else
                                                            {
                                                                maritalstatus = maritalstatus + ", " + arr6[i];
                                                            }
                                                        }
                                                    }
                                                    if (tc6 > 0)
                                                    {
                                                        var msg = "Marital Status(s): " + maritalstatus + " not exist";
                                                        ViewBag.ImportStatusFlag = "error";
                                                        ViewBag.ImportStatus = msg;
                                                        return View("EmployeeDetailsImport");
                                                    }
                                                    else
                                                    {
                                                        int tc7 = 0;
                                                        string religion = "";
                                                        string[] arr7 = chkreligion.Split(',');
                                                        for (int i = 0; i < arr7.Length; i++)
                                                        {
                                                            int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblReligion where Religionname in ('" + arr7[i] + "')"));
                                                            if (chktc == 0)
                                                            {
                                                                tc7 = tc7 + 1;
                                                                if (religion == "")
                                                                {
                                                                    religion = arr7[i];
                                                                }
                                                                else
                                                                {
                                                                    religion = religion + ", " + arr7[i];
                                                                }
                                                            }
                                                        }
                                                        if (tc7 > 0)
                                                        {
                                                            var msg = "Religion (s): " + religion + " not exist";
                                                            ViewBag.ImportStatusFlag = "error";
                                                            ViewBag.ImportStatus = msg;
                                                            return View("EmployeeDetailsImport");
                                                        }
                                                        else
                                                        {
                                                            int tc8 = 0;
                                                            string country = "";
                                                            string[] arr8 = chkcountry.Split(',');
                                                            for (int i = 0; i < arr8.Length; i++)
                                                            {
                                                                int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcountry where isdeleted is null and countryname in ('" + arr8[i] + "')"));
                                                                if (chktc == 0)
                                                                {
                                                                    tc8 = tc8 + 1;
                                                                    if (country == "")
                                                                    {
                                                                        country = arr8[i];
                                                                    }
                                                                    else
                                                                    {
                                                                        country = country + ", " + arr8[i];
                                                                    }
                                                                }
                                                            }
                                                            if (tc8 > 0)
                                                            {
                                                                var msg = "Country Name(s): " + country + " not exist";
                                                                ViewBag.ImportStatusFlag = "error";
                                                                ViewBag.ImportStatus = msg;
                                                                return View("EmployeeDetailsImport");
                                                            }
                                                            else
                                                            {
                                                                int tc9 = 0;
                                                                string state = "";
                                                                string[] arr9 = chkstate.Split(',');
                                                                for (int i = 0; i < arr9.Length; i++)
                                                                {
                                                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblstate where isdeleted is null and statename in ('" + arr9[i] + "')"));
                                                                    if (chktc == 0)
                                                                    {
                                                                        tc9 = tc9 + 1;
                                                                        if (state == "")
                                                                        {
                                                                            state = arr9[i];
                                                                        }
                                                                        else
                                                                        {
                                                                            state = state + ", " + arr9[i];
                                                                        }
                                                                    }
                                                                }
                                                                if (tc9 > 0)
                                                                {
                                                                    var msg = "State Name(s): " + state + " not exist";
                                                                    ViewBag.ImportStatusFlag = "error";
                                                                    ViewBag.ImportStatus = msg;
                                                                    return View("EmployeeDetailsImport");
                                                                }
                                                                else
                                                                {
                                                                    int tc10 = 0;
                                                                    string city = "";
                                                                    string[] arr10 = chkcity.Split(',');
                                                                    for (int i = 0; i < arr10.Length; i++)
                                                                    {
                                                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcity where isdeleted is null and cityname in ('" + arr10[i] + "')"));
                                                                        if (chktc == 0)
                                                                        {
                                                                            tc10 = tc10 + 1;
                                                                            if (city == "")
                                                                            {
                                                                                city = arr10[i];
                                                                            }
                                                                            else
                                                                            {
                                                                                city = city + ", " + arr10[i];
                                                                            }
                                                                        }
                                                                    }
                                                                    if (tc10 > 0)
                                                                    {
                                                                        var msg = "City Name(s): " + city + " not exist";
                                                                        ViewBag.ImportStatusFlag = "error";
                                                                        ViewBag.ImportStatus = msg;
                                                                        return View("EmployeeDetailsImport");
                                                                    }
                                                                    else
                                                                    {
                                                                        try
                                                                        {

                                                                            string consString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                                                                            using (SqlConnection con = new SqlConnection(consString))
                                                                            {

                                                                                foreach (DataRow dr in dtable.Rows)
                                                                                {
                                                                                    CodeGenMaster datas = getRegCode("Employee");
                                                                                    var perfix = datas.Prefix;
                                                                                    var suffix = datas.Suffix;
                                                                                    var last = datas.LastSeries;
                                                                                    var first = datas.StartSeries;
                                                                                    var sep = datas.Seprator;
                                                                                    var incrmentNo = datas.DocNo;
                                                                                    var School = datas.SchoolID;

                                                                                    string EmployeeCode = "";
                                                                                    if (last == "")
                                                                                    {
                                                                                        EmployeeCode = perfix + sep + School + sep + suffix + sep + first;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        int newcode = Convert.ToInt32(last) + Convert.ToInt32(incrmentNo);
                                                                                        EmployeeCode = perfix + sep + School + sep + suffix + sep + newcode.ToString("D" + first.Length + "");

                                                                                    }

                                                                                    //EmployeeCode = EmployeeCode.Replace("/", "").Replace("/", "").Replace("/", "");
                                                                                    //string EmployeeCode = dr["EmployeeCode"].ToString();
                                                                                    string JoiningDate = dr["JoiningDate"].ToString();
                                                                                    string Department = dr["Department"].ToString();
                                                                                    int depid = Convert.ToInt32(obj.ExecuteScaler("select DepartmentId from tblDepartmnet where schoolid='" + Schoolid + "' and IsDeleted is null and DepartmentName='" + Department.Trim() + "'"));
                                                                                    string Designation = dr["Designation"].ToString();
                                                                                    int desigid = Convert.ToInt32(obj.ExecuteScaler("select DesigID from tbldesignation where schoolid='" + Schoolid + "' and  IsDeleted is null and Designation='" + Designation.Trim() + "'"));
                                                                                    string Qualification = dr["Qualification"].ToString();
                                                                                    int Qualificationid = Convert.ToInt32(obj.ExecuteScaler("select QualificationId from tblqualifications where schoolid='" + Schoolid + "' and  IsDeleted is null and QualificationName='" + Qualification.Trim() + "'"));
                                                                                    string TotalExperience = dr["TotalExperience"].ToString();
                                                                                    string StaffType = dr["StaffType"].ToString();
                                                                                    int stafftypeid = Convert.ToInt32(obj.ExecuteScaler("select Id from tblStaffCategory where Status=1 and Name='" + StaffType.Trim() + "'"));
                                                                                    string FirstName = dr["FirstName"].ToString();
                                                                                    string MiddleName = dr["MiddleName"].ToString();
                                                                                    string LastName = dr["LastName"].ToString();
                                                                                    string DOB = dr["DOB"].ToString();
                                                                                    string Gender = dr["Gender"].ToString();
                                                                                    int genderid = Convert.ToInt32(obj.ExecuteScaler("select Gender_id from tblgender where Status=1 and GenderName='" + Gender.Trim() + "'"));
                                                                                    string MaritalStatus = dr["MaritalStatus"].ToString();
                                                                                    int MaritalStatusid = Convert.ToInt32(obj.ExecuteScaler("select id from tblmaritalstatus where Status=1 and name='" + MaritalStatus.Trim() + "'"));
                                                                                    string Religon = dr["Religon"].ToString();
                                                                                    int Religonid = Convert.ToInt32(obj.ExecuteScaler("select religionid from tblReligion where Status=1 and ReligionName='" + Religon.Trim() + "'"));
                                                                                    string FatherName = dr["FatherName"].ToString();
                                                                                    string MotherName = dr["MotherName"].ToString();
                                                                                    string Country = dr["Country"].ToString();
                                                                                    int Countryid = Convert.ToInt32(obj.ExecuteScaler("select Countryid from tblcountry where IsDeleted is null and CountryName='" + Country.Trim() + "'"));
                                                                                    string State = dr["State"].ToString();
                                                                                    int Stateid = Convert.ToInt32(obj.ExecuteScaler("select Stateid from tblState where IsDeleted is null and StateName='" + State.Trim() + "'"));
                                                                                    string City = dr["City"].ToString();
                                                                                    int Cityid = Convert.ToInt32(obj.ExecuteScaler("select id from tblcity where IsDeleted is null and cityname='" + City.Trim() + "'"));
                                                                                    string PinCode = dr["PinCode"].ToString();
                                                                                    string EmailAddress = dr["EmailAddress"].ToString();
                                                                                    string MobileNo = dr["MobileNo"].ToString();
                                                                                    string AdhaarNumber = dr["AdhaarNumber"].ToString();
                                                                                    string PresentAddress = dr["PresentAddress"].ToString();
                                                                                    string PermanentAddress = dr["PermanentAddress"].ToString();
                                                                                    //string userid = dr["UserID"].ToString();
                                                                                    //string password = dr["Password"].ToString();
                                                                                    string userid = EmployeeCode.Replace("/", "").Replace("/", "").Replace("/", "");
                                                                                    string password = userid;
                                                                                    //Encrypt from md5
                                                                                    MD5 md5 = new MD5CryptoServiceProvider();

                                                                                    //compute hash from the bytes of text  
                                                                                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

                                                                                    //get hash result after compute it  
                                                                                    byte[] result = md5.Hash;

                                                                                    StringBuilder strBuilderPPWD = new StringBuilder();
                                                                                    for (int i = 0; i < result.Length; i++)
                                                                                    {
                                                                                        //change it into 2 hexadecimal digits  
                                                                                        //for each byte  
                                                                                        strBuilderPPWD.Append(result[i].ToString("x2"));
                                                                                    }
                                                                                    string EmployeePassword = strBuilderPPWD.ToString();

                                                                                    string pass = EmployeePassword;

                                                                                    //string pass = EmployeePassword;

                                                                                    string BiometricID = dr["BiometricID"].ToString();

                                                                                    string str = "insert into tblemployee(Empcode,JoiningDt,DeptID,DesigID,Qualification,Experience,StaffCategory,firstname,middlename,lastname,dob,GenderID,MaritalSatus,Religion,FatherName,MotherName,Country,State,city,pincode,mobile,Email,AdharNo,PresentAddress,PermAddress,UserID,Pwd,usertype,schoolid,biometricid) values('" + EmployeeCode + "','" + Convert.ToDateTime(JoiningDate).ToString("yyyy-MM-dd") + "','" + depid + "','" + desigid + "','" + Qualificationid + "','" + TotalExperience + "','" + stafftypeid + "','" + FirstName + "','" + MiddleName + "','" + LastName + "','" + Convert.ToDateTime(DOB).ToString("yyyy-MM-dd") + "','" + genderid + "','" + MaritalStatusid + "','" + Religonid + "','" + FatherName + "','" + MotherName + "','" + Countryid + "','" + Stateid + "','" + Cityid + "','" + PinCode + "','" + MobileNo + "','" + EmailAddress + "','" + AdhaarNumber + "','" + PresentAddress + "','" + PermanentAddress + "','" + userid + "','" + pass + "','1','" + Schoolid + "','" + BiometricID + "')";
                                                                                    con.Open();
                                                                                    SqlCommand cmd = new SqlCommand(str, con);
                                                                                    cmd.ExecuteNonQuery();

                                                                                    string lastval = obj.ExecuteScaler("select LastSeries from tblDocumentNo where UserType='Employee' and Status=1 and schoolid='" + Schoolid + "'");
                                                                                    string incemtno = obj.ExecuteScaler("select DocumentNo from tblDocumentNo where UserType='Employee' and Status=1 and schoolid='" + Schoolid + "'");
                                                                                    if (lastval == "")
                                                                                    {
                                                                                        lastval = "0";
                                                                                    }
                                                                                    var lastupdate = Int64.Parse(lastval) + Int64.Parse(incemtno);
                                                                                    string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                                                                                    SqlCommand cmd1 = new SqlCommand("update tblDocumentNo set LastSeries='" + lastupdate + "' where UserType='Employee' and Status=1 and schoolid='" + Schoolid + "'", con);
                                                                                    cmd1.ExecuteNonQuery();
                                                                                    con.Close();
                                                                                    sqlHelper obj1 = new sqlHelper();
                                                                                    string SchoolName = obj1.ExecuteScaler("select School from tblSchoolDetails where IsDeleted is null and ID='" + Schoolid + "'");
                                                                                    string SchoolCode = obj1.ExecuteScaler(" select SchoolCode from tblSchoolDetails where IsDeleted is null and ID='" + Schoolid + "'");

                                                                                    SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                                                                                    //using (MailMessage mm = new MailMessage())
                                                                                    //{


                                                                                    //    StringBuilder st = new StringBuilder();
                                                                                    //    st.AppendLine("Hi Sir/mam,");
                                                                                    //    st.AppendLine("Your Login Information");
                                                                                    //    st.AppendLine("SchoolCode :" + SchoolCode);
                                                                                    //    st.AppendLine("UserID :" + userid);
                                                                                    //    st.AppendLine("Password :" + userid);
                                                                                    //    st.AppendLine("Login URL :" + Request.Url.Authority);
                                                                                    //    st.AppendLine("");
                                                                                    //    st.AppendLine("Regards");
                                                                                    //    st.AppendLine(SchoolName);
                                                                                    //    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                                                                                    //    mm.To.Add(EmailAddress); //---- Email address of the recipient.
                                                                                    //    mm.Subject = "School Login Details"; //---- Subject of email.
                                                                                    //    mm.Body = (st.ToString()); //---- Content of email.

                                                                                    //    SmtpClient smtp = new SmtpClient();
                                                                                    //    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                                                                                    //    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                                                                                    //    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                                                                                    //    //---Your Email and password
                                                                                    //    smtp.UseDefaultCredentials = false;
                                                                                    //    smtp.Credentials = NetworkCred;
                                                                                    //    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                                                                                    //    smtp.Send(mm);





                                                                                    //}


                                                                                }
                                                                            }
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            throw ex;
                                                                        }

                                                                        //deleting excel file from folder  
                                                                        if ((System.IO.File.Exists(pathToExcelFile)))
                                                                        {
                                                                            System.IO.File.Delete(pathToExcelFile);
                                                                        }
                                                                        ViewBag.ImportStatusFlag = "success";
                                                                        ViewBag.ImportStatus = "Data imported successfully...";
                                                                        //return Json("success", JsonRequestBehavior.AllowGet);
                                                                        return View("EmployeeDetailsImport");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //}
                        }
                        //}
                    }
                }
                else
                {
                    //alert message for invalid file format  
                    ViewBag.ImportStatus = "Only Excel file format is allowed!!";
                    return View("EmployeeDetailsImport");
                }
            }
            else
            {
                ViewBag.ImportStatus = "Please choose Excel file!!";
                return View("EmployeeDetailsImport");
            }
        }

        public ActionResult EmployeeAttendenceImport()
        {
            return View();

        }


        [HttpPost]
        public JsonResult UploadEmployeeAttendenceExcel(HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                string yrst = Request.Params["ddlAttendenceYear"].ToString();
                string Months = Request.Params["ddlAttendenceMonths"].ToString();
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    for (int i = dtable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtable.Rows[i][1] == DBNull.Value)
                            dtable.Rows[i].Delete();
                    }
                    dtable.AcceptChanges();
                    sqlHelper obj = new sqlHelper();
                    int size = dtable.Columns.Count;
                    

                    for (int j = 0; j < dtable.Rows.Count; j++)
                    {
                        for (int k = 1; k < size; k++)
                        {
                            string colname = dtable.Columns[k].ColumnName;
                         //   new System.Collections.ArrayList.ArrayListDebugView(dtable.Columns.List).Items[1]
                            string attendenceDate = Months + "/" + colname + "/" + yrst;
                            string val = dtable.Rows[j][k].ToString();
                            if (val != "")
                            {

                                string EmployeeName= dtable.Rows[j]["EmployeeCode"].ToString();
                                string IsValidEmployee = obj.ExecuteScaler("select Empcode EmplyeeName from tblEmployee where Empcode='" + EmployeeName +"'");
                                if (IsValidEmployee != null)
                                {
                                    string empId = obj.ExecuteScaler("select Id from tblEmployee where Empcode='" + EmployeeName + "'");
                                    string department = obj.ExecuteScaler("select DeptID Department from tblEmployee where Empcode='" + EmployeeName + "'");
                                    string designation = obj.ExecuteScaler("select DesigID Designation from tblEmployee where Empcode='" + EmployeeName + "'");

                                    if (val == "P")
                                    {
                                        val = "Present";
                                    }
                                    else
                                    {
                                        val = "Absent";
                                    }

                                    string[] cols = { "EmployeeId", "DesigId", "DepId", "LeaveType", "AttendenceType", "AttendenceDate" };
                                    object[] vals = { empId, designation, department, "", val, attendenceDate };
                                    obj.insertValIntoTable("tblEmployeeAttendence", cols, vals);
                                }

                            }

                        }

                    }
                    
                    //try
                    //{

                    //    string consString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                    //    using (SqlConnection con = new SqlConnection(consString))
                    //    {
                    //        using (SqlCommand cmd = new SqlCommand("ImportEmployeeExcelData"))
                    //        {
                    //            cmd.CommandType = CommandType.StoredProcedure;
                    //            cmd.Connection = con;
                    //            cmd.Parameters.AddWithValue("@tblCustomers", dtable);
                    //            con.Open();
                    //            cmd.ExecuteNonQuery();
                    //            con.Close();
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //    //foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    //    //{

                    //    //    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    //    //    {

                    //    //        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                    //    //    }

                    //    //}
                    //}

                    //var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    //var artistAlbums = from a in excelFile.Worksheet<User>(sheetName) select a;

                    //foreach (var a in artistAlbums)
                    //{
                    //    try
                    //    {
                    //        if (a.Name != "" && a.Address != "" && a.ContactNo != "")
                    //        {
                    //            User TU = new User();
                    //            TU.Name = a.Name;
                    //            TU.Address = a.Address;
                    //            TU.ContactNo = a.ContactNo;
                    //            db.Users.Add(TU);

                    //            db.SaveChanges();



                    //        }
                    //        else
                    //        {
                    //            data.Add("<ul>");
                    //            if (a.Name == "" || a.Name == null) data.Add("<li> name is required</li>");
                    //            if (a.Address == "" || a.Address == null) data.Add("<li> Address is required</li>");
                    //            if (a.ContactNo == "" || a.ContactNo == null) data.Add("<li>ContactNo is required</li>");

                    //            data.Add("</ul>");
                    //            data.ToArray();
                    //            return Json(data, JsonRequestBehavior.AllowGet);
                    //        }
                    //    }

                    //    catch (DbEntityValidationException ex)
                    //    {
                    //        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    //        {

                    //            foreach (var validationError in entityValidationErrors.ValidationErrors)
                    //            {

                    //                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                    //            }

                    //        }
                    //    }
                    //}
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GenerateSpreadsheet(int months,int years)
        {
            int year = years;
            int month = months;
            // Create temp path and file name
            var path = Server.MapPath("~/temp");
            var fileName = "EmployeeAttendence.xlsx";
            int days = DateTime.DaysInMonth(year, month);
            // Create temp path if not exits
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("EmployeeCode", typeof(string));
            for (int i = 1; i <= days; i++)
            {
                table.Columns.Add(Convert.ToString(i), typeof(int));
            }

            //table.Columns.Add("ID", typeof(int));
            //table.Columns.Add("Name", typeof(string));
            //table.Columns.Add("Sex", typeof(string));
            //table.Columns.Add("Subject1", typeof(int));
            //table.Columns.Add("Subject2", typeof(int));
            //table.Columns.Add("Subject3", typeof(int));
            //table.Columns.Add("Subject4", typeof(int));
            //table.Columns.Add("Subject5", typeof(int));
            //table.Columns.Add("Subject6", typeof(int));
            //table.Rows.Add(1, "Amar", "M", 78, 59, 72, 95, 83, 77);
            //table.Rows.Add(2, "Mohit", "M", 76, 65, 85, 87, 72, 90);
            //table.Rows.Add(3, "Garima", "F", 77, 73, 83, 64, 86, 63);
            //table.Rows.Add(4, "jyoti", "F", 55, 77, 85, 69, 70, 86);
            //table.Rows.Add(5, "Avinash", "M", 87, 73, 69, 75, 67, 81);
            //table.Rows.Add(6, "Devesh", "M", 92, 87, 78, 73, 75, 72);

            // Create the sample DataSet
            DataSet dataSet = new DataSet("Hospital");
            dataSet.Tables.Add(table);

            // Create the Excel file in temp path
            string fullPath = Path.Combine(path, fileName);
            CreateExcelFile.CreateExcelDocument(dataSet, fullPath, includeAutoFilter: false);

            // Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = "" });
        }



        [HttpGet]
       // [NoCache]
        public ActionResult DownloadSpreadsheet(string file)
        {
            // Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/temp"), file);

            // Return the file for download, this is an Excel 
            // so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, "application/vnd.ms-excel", file);
        }

        public ActionResult ImportStudentAttendence()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadStudentAttendenceExcel(HttpPostedFileBase FileUpload)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                string yrst = Request.Params["ddlAttendenceYear"].ToString();
                string Months = Request.Params["ddlAttendenceMonths"].ToString();
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0};OLE DB Services=-4; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};OLE DB Services=-4;Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Table1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    for (int i = dtable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtable.Rows[i][1] == DBNull.Value)
                            dtable.Rows[i].Delete();
                    }
                    dtable.AcceptChanges();

                    for (int kk = 0; kk < dtable.Rows.Count; kk++)
                    {
                        if (dtable.Rows[kk][2].ToString().Length > 10)
                        { 
                            string dept = dtable.Rows[kk][2].ToString();
                                var depart = db.tblDepartmnets.Any(s => s.DepartmentName == dept);
                                if (!depart)
                                {
                                    Response.Redirect("ImportStudentAttendence?m=dept");
                                    return View("ImportStudentAttendence");
                                }
                       }


                        if (dtable.Rows[kk][3].ToString().Length > 10)
                        {
                            string desig = dtable.Rows[kk][3].ToString();
                            var designation = db.tblDesignations.Any(s => s.Designation == desig);
                            if (!designation)
                            {
                                Response.Redirect("ImportStudentAttendence?m=desig");
                                return View("ImportStudentAttendence");
                            }
                        }

                        if (dtable.Rows[kk][4].ToString().Length > 10)
                        {
                            string qual = dtable.Rows[kk][4].ToString();
                            var qualfi = db.tblQualifications.Any(s => s.QualificationName == qual);
                            if (!qualfi)
                            {
                                Response.Redirect("ImportStudentAttendence?m=qual");
                                return View("ImportStudentAttendence");
                            }
                        }

                        if (dtable.Rows[kk][6].ToString().Length > 10)
                        {
                            string staff = dtable.Rows[kk][6].ToString();
                            var staffcat = db.tblStaffCategories.Any(s => s.Name == staff);
                            if (!staffcat)
                            {
                                Response.Redirect("ImportStudentAttendence?m=staf");
                                return View("ImportStudentAttendence");
                            }
                        }

                    }
                        sqlHelper obj = new sqlHelper();
                    int size = dtable.Columns.Count;


                    for (int j = 0; j < dtable.Rows.Count; j++)
                    {
                        for (int k = 1; k < size; k++)
                        {
                            string colname = dtable.Columns[k].ColumnName;
                            //   new System.Collections.ArrayList.ArrayListDebugView(dtable.Columns.List).Items[1]
                            string attendenceDate = Months + "/" + colname + "/" + yrst;
                            string val = dtable.Rows[j][k].ToString();
                            if (val != "")
                            {

                                string StudentCode = dtable.Rows[j]["StudentCode"].ToString();
                                string IsValidEmployee = obj.ExecuteScaler("select RegNo EmplyeeName from TBLStudent where RegNo='" + StudentCode + "'");
                                if (IsValidEmployee != null)
                                {
                                    string stdId = obj.ExecuteScaler("select ID from TBLStudent where RegNo='" + StudentCode + "'");
                                    string classId = obj.ExecuteScaler("select ClassID Class from TBLStudent where RegNo='" + StudentCode + "'");
                                    string sectionId = obj.ExecuteScaler("select SectionID Section from TBLStudent where RegNo='" + StudentCode + "'");
                                    string TeacherId = obj.ExecuteScaler("select intEmpID from tblClassTeacherAllocation where ClassID="+classId+" and SectionID="+sectionId);

                                    if (val == "P")
                                    {
                                        val = "Present";
                                    }
                                    else if (val == "A")
                                    {
                                        val = "Absent";
                                    }
                                    else
                                    {
                                        val = "Leave";
                                    }

                                    string[] cols = { "StudentId", "TeacherId", "ClassId", "SectionId", "AttendenceType", "AttendenceDate" };
                                    object[] vals = { stdId, TeacherId, classId, sectionId, val, attendenceDate };
                                    obj.insertValIntoTable("tblStudentAttendence", cols, vals);
                                }

                            }

                        }

                    }

                   

                   
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    Response.Redirect("ImportStudentAttendence?m=1");
                    return View("ImportStudentAttendence");
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult ExportStudentAttendenceSheet(int months, int years)
        {
            int year = years;
            int month = months;
            // Create temp path and file name
            var path = Server.MapPath("~/temp");
            var fileName = "StudentAttendence.xlsx";
            int days = DateTime.DaysInMonth(year, month);
            // Create temp path if not exits
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("StudentCode", typeof(string));
            for (int i = 1; i <= days; i++)
            {
                table.Columns.Add(Convert.ToString(i), typeof(int));
            }
            // Create the sample DataSet
            DataSet dataSet = new DataSet("Hospital");
            dataSet.Tables.Add(table);

            // Create the Excel file in temp path
            string fullPath = Path.Combine(path, fileName);
            CreateExcelFile.CreateExcelDocument(dataSet, fullPath, includeAutoFilter: false);

            // Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = "" });
        }


        public ActionResult StudentsDetailsImport()
        {
            return View();

        }
        //Changed ON 9/8/2019 AVNEESH
        [HttpPost]
        //public ActionResult UploadStudentExcelImport(HttpPostedFileBase FileUpload)
        public ActionResult StudentsDetailsImport(HttpPostedFileBase FileUpload)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<string> data = new List<string>();
            sqlHelper obj = new sqlHelper();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)Session["schoolid"];

            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    int emptyflg = 0;
                    string chkacademicyear = "";
                    string chkclass = "";
                    string chksection = "";
                    string chkstream = "-1";
                    string chkbatch = "";
                    string chknationality = "";
                    string chkgender = "";
                    string chkcategory = "";
                    string chkcity = "";
                    string chkstate = "";
                    string chkcountry = "";
                    string chkstatus = "";
                    string chkrollno = "";
                    string repeatrollno = "";
                    foreach (DataRow dr in dtable.Rows)
                    {
                        string AcadmicYear = dr["AcadmicYear"].ToString();
                        string AdmissionDate = dr["AdmissionDate"].ToString();
                        string Class = dr["Class"].ToString();
                        string Section = dr["Section"].ToString();
                        string Stream = dr["Stream"].ToString();
                        string Batch = dr["Batch"].ToString();
                        string RollNo = dr["RollNo"].ToString();

                        if (AcadmicYear == "" || AdmissionDate == "" || Class == "" || Section == "" || Stream == "" || Batch == "" || RollNo == "")
                        {
                            emptyflg = emptyflg + 1;
                        }
                        if (AcadmicYear == "" || AdmissionDate == "" || Class == "" || Section == "" )
                        {
                            emptyflg = emptyflg + 1;
                        }
                    }
                    if (emptyflg < 0)
                    {
                        var msg = "AcadmicYear, AdmissionDate, Class, Section fields can not be blank!!";
                        ViewBag.ImportStuStatusFlag = "error";
                        ViewBag.ImportStuStatus = msg;
                        return View("StudentsDetailsImport");
                    }
                    else
                    {
                        foreach (DataRow dr in dtable.Rows)
                        {
                            var selemp = (dr["RollNo"].ToString()).Trim();
                            int chktc = 0;
                            foreach (DataRow dr1 in dtable.Rows)
                            {
                                if (selemp == dr1["RollNo"].ToString())
                                {
                                    chktc = chktc + 1;
                                }
                            }
                            if (chktc > 1)
                            {
                                if (repeatrollno == "")
                                {
                                    repeatrollno = selemp;
                                }
                                else
                                {
                                    if (!repeatrollno.Contains(selemp))
                                    {
                                        repeatrollno = repeatrollno + ',' + selemp;
                                    }
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(repeatrollno))
                        {
                            var msg = "RollNo(s): " + repeatrollno + " is/are repeating";
                            ViewBag.ImportStuStatusFlag = "error";
                            ViewBag.ImportStuStatus = msg;
                            return View("StudentsDetailsImport");
                        }
                        else
                        {
                            foreach (DataRow dr in dtable.Rows)
                            {
                                if (chkacademicyear == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["AcadmicYear"].ToString()))
                                    {
                                        chkacademicyear = dr["AcadmicYear"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["AcadmicYear"].ToString()))
                                    {
                                       
                                        //if (!chkacademicyear.Contains(dr["AcadmicYear"].ToString()))
                                        //{
                                            chkacademicyear = chkacademicyear + ',' + dr["AcadmicYear"].ToString().Trim();
                                        //}
                                    }
                                }
                               
                                if (chkrollno == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["RollNo"].ToString()))
                                    {
                                        chkrollno = dr["RollNo"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["RollNo"].ToString()))
                                    {
                                        //if (!chkrollno.Contains(dr["RollNo"].ToString()))
                                        //{
                                        chkrollno = chkrollno + ',' + dr["RollNo"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkclass == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Class"].ToString()))
                                    {
                                        chkclass = dr["Class"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Class"].ToString()))
                                    {
                                        //if (!chkclass.Contains(dr["Class"].ToString()))
                                        //{
                                            chkclass = chkclass + ',' + dr["Class"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chksection == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Section"].ToString()))
                                    {
                                        chksection = dr["Section"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Section"].ToString()))
                                    {
                                        //if (!chksection.Contains(dr["Section"].ToString()))
                                        //{
                                            chksection = chksection + ',' + dr["Section"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkstream == "-1")
                                { //
                                    //if (!string.IsNullOrEmpty(dr["Stream"].ToString()))
                                    //{
                                       
                                    //}
                                    //if ( dr["Stream"].ToString()=="")
                                    //{
                                      
                                        chkstream =  dr["Stream"].ToString().Trim();


                                    //}

                                }
                            
                                else
                                {
                                    //if (!string.IsNullOrEmpty(dr["Stream"].ToString()))
                                    //{
                                        //if (!chkstream.Contains(dr["Stream"].ToString()))
                                        //{
                                            chkstream = chkstream + ',' + dr["Stream"].ToString().Trim();
                                        //}
                                    //}
                                }

                                if (chkbatch == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Batch"].ToString()))
                                    {
                                        chkbatch = dr["Batch"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Batch"].ToString()))
                                    {
                                        if (!chkbatch.Contains(dr["Batch"].ToString()))
                                        {
                                            chkbatch = chkbatch + ',' + dr["Batch"].ToString().Trim();
                                        }
                                    }
                                }

                                if (chknationality == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Nationality"].ToString()))
                                    {
                                        chknationality = dr["Nationality"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Nationality"].ToString()))
                                    {
                                        //if (!chknationality.Contains(dr["Nationality"].ToString()))
                                        //{
                                            chknationality = chknationality + ',' + dr["Nationality"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkgender == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Gender"].ToString()))
                                    {
                                        chkgender = dr["Gender"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Gender"].ToString()))
                                    {
                                        //if (!chkgender.Contains(dr["Gender"].ToString()))
                                        //{
                                            chkgender = chkgender + ',' + dr["Gender"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkcategory == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Category"].ToString()))
                                    {
                                        chkcategory = dr["Category"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Category"].ToString()))
                                    {
                                        //if (!chkcategory.Contains(dr["Category"].ToString()))
                                        //{
                                            chkcategory = chkcategory + ',' + dr["Category"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkstatus == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                                    {
                                        chkstatus = dr["Status"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                                    {
                                        //if (!chkstatus.Contains(dr["Status"].ToString()))
                                        //{
                                            chkstatus = chkstatus + ',' + dr["Status"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkcountry == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["Country"].ToString()))
                                    {
                                        chkcountry = dr["Country"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["Country"].ToString()))
                                    {
                                        //if (!chkcountry.Contains(dr["Country"].ToString()))
                                        //{
                                            chkcountry = chkcountry + ',' + dr["Country"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkstate == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["State"].ToString()))
                                    {
                                        chkstate = dr["State"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["State"].ToString()))
                                    {
                                        //if (!chkstate.Contains(dr["State"].ToString()))
                                        //{
                                            chkstate = chkstate + ',' + dr["State"].ToString().Trim();
                                        //}
                                    }
                                }

                                if (chkcity == "")
                                {
                                    if (!string.IsNullOrEmpty(dr["City"].ToString()))
                                    {
                                        chkcity = dr["City"].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dr["City"].ToString()))
                                    {
                                        //if (!chkcity.Contains(dr["City"].ToString()))
                                        //{
                                            chkcity = chkcity + ',' + dr["City"].ToString().Trim();
                                        //}
                                    }
                                }
                            }

                            try
                            {
                                //else
                                //{
                                    int tc1 = 0;
                                    string academicyear = "";
                                    string[] arr1 = chkacademicyear.Split(',');
                                    for (int i = 0; i < arr1.Length; i++)
                                    {
                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblacademicyear where Status=1 and SchoolID='" + Schoolid + "' and IsDeleted is null and year(datefrom)='" + arr1[i].Split('-')[0] + "' and  RIGHT(CONVERT(VARCHAR(8), dateto, 1),2)='" + arr1[i].Split('-')[1] + "' "));
                                    if (chktc == 0)
                                    {
                                        tc1 = tc1 + 1;
                                        if (academicyear == "")
                                        {
                                            academicyear = arr1[i];
                                        }
                                        else
                                        {
                                            academicyear = academicyear + ", " + arr1[i];
                                        }
                                    }
                                }
                                if (tc1 > 0)
                                {
                                var msg = "AcadmicYear(s): " + academicyear + " not exist";
                                ViewBag.ImportStuStatusFlag = "error";
                                ViewBag.ImportStuStatus = msg;
                                return View("StudentsDetailsImport");
                            }
                                else
                                {
                                    int tc2 = 0;
                                    string stuclass = "";
                                    string[] arr2 = chkclass.Split(',');
                                    for (int i = 0; i < arr2.Length; i++)
                                    {
                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCourses where coursename='" + arr2[i] + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1"));
                                        if (chktc == 0)
                                        {
                                            tc2 = tc2 + 1;
                                            if (stuclass == "")
                                            {
                                                stuclass = arr2[i];
                                            }
                                            else
                                            {
                                                stuclass = stuclass + ", " + arr2[i];
                                            }
                                        }
                                    }
                                    if (tc2 > 0)
                                    {
                                    var msg = "Class(s): " + stuclass + " not exist";
                                    ViewBag.ImportStuStatusFlag = "error";
                                    ViewBag.ImportStuStatus = msg;
                                    return View("StudentsDetailsImport");
                                }
                                    else
                                    {
                                        int tc3 = 0;
                                        string stusection = "";
                                        string[] arr3 = chksection.Split(',');
                                        string[] arrclasss = chkclass.Split(',');
                                        for (int i = 0; i < arr3.Length; i++)
                                        {
                                            int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblsections where SectionName='" + arr3[i] + "' and IsDeleted is  null and ClassId=(select id from tblCourses where coursename='" + arrclasss[i] + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                            if (chktc == 0)
                                            {
                                                tc3 = tc3 + 1;
                                                if (stusection == "")
                                                {
                                                    stusection = arr3[i];
                                                }
                                                else
                                                {
                                                    stusection = stusection + ", " + arr3[i];
                                                }
                                            }
                                        }
                                        if (tc3 > 0)
                                        {
                                        var msg = "Section(s): " + stusection + " not exist in selected Class";
                                        ViewBag.ImportStuStatusFlag = "error";
                                        ViewBag.ImportStuStatus = msg;
                                        return View("StudentsDetailsImport");
                                    }
                                        else
                                        {
                                            int tc4 = 0;
                                            string stustream = "";
                                            string[] arr4 = chkstream.Split(',');
                                            for (int i = 0; i < arr4.Length; i++)
                                            {
                                                if (arr4[i]=="")
                                                {
                                                    tc4 = 0;
                                                }
                                                else
                                                {
                                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblStream where StreamName='" + arr4[i] + "' and IsDeleted is  null and Class=(select id from tblCourses where coursename='" + arrclasss[i] + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                                    if (chktc == 0)
                                                    {
                                                        tc4 = tc4 + 1;
                                                        if (stustream == "")
                                                        {
                                                            stustream = arr4[i];
                                                        }
                                                        else
                                                        {
                                                            stustream = stustream + ", " + arr4[i];
                                                        }
                                                    }
                                                }
                                               
                                            }
                                            if (tc4 > 0)
                                            {
                                                var msg = "Stream(s): " + stustream + " not exist in selected Class";
                                                ViewBag.ImportStuStatusFlag = "error";
                                                ViewBag.ImportStuStatus = msg;
                                                return View("StudentsDetailsImport");
                                            }
                                            else
                                            {

                                                int tc = 0;
                                                string rollno = "";
                                                string[] arr = chkrollno.Split(',');

                                                for (int i = 0; i < arr.Length; i++)
                                                {


                                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblstudent where  IsDeleted is null and schoolid='" + Schoolid + "' and ClassID=(select id from tblCourses where CourseName='" + arrclasss[i] + "' and SchoolID='" + Schoolid + "' and IsDeleted is null) and SectionID=(select id from tblSections where SectionName='" + arr3[i] + "' and SchoolID='" + Schoolid + "'  and IsDeleted is null) and AcademicYear=(select ID from tblacademicyear where Status=1 and SchoolID='" + Schoolid + "' and IsDeleted is null and year(datefrom)='" + arr1[i].Split('-')[0] + "' and  RIGHT(CONVERT(VARCHAR(8), dateto, 1),2)='" + arr1[i].Split('-')[1] + "') and RollNo='" + arr[i] + "'"));
                                                    if (chktc > 0)
                                                    {
                                                        tc = tc + 1;
                                                        if (rollno == "")
                                                        {
                                                            rollno = arr[i];
                                                        }
                                                        else
                                                        {
                                                            rollno = rollno + ", " + arr[i];
                                                        }
                                                    }
                                                }
                                                if (tc > 0)
                                                {
                                                    var msg = "RollNo(s): " + rollno + " already exist for same class & section";
                                                    ViewBag.ImportStuStatusFlag = "error";
                                                    ViewBag.ImportStuStatus = msg;
                                                    return View("StudentsDetailsImport");
                                                }

                                                else
                                                {

                                                    int tc5 = 0;
                                                    string stusbatch = "";
                                                    string[] arr5 = chkbatch.Split(',');
                                                    for (int i = 0; i < arr5.Length; i++)
                                                    {
                                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblbatch where BatchName='" + arr5[i] + "' and Status=1 and Classid=(select id from tblCourses where coursename='" + arrclasss[i] + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                                        if (chktc == 0)
                                                        {
                                                            tc5 = tc5 + 1;
                                                            if (stusbatch == "")
                                                            {
                                                                stusbatch = arr5[i];     
                                                            }
                                                            else
                                                            {
                                                                stusbatch = stusbatch + ", " + arr5[i];
                                                            }
                                                        }
                                                    }
                                                    if (tc5 > 0)
                                                    {
                                                        var msg = "Batch(s): " + stusbatch + " not exist in selected Class";
                                                        ViewBag.ImportStuStatusFlag = "error";
                                                        ViewBag.ImportStuStatus = msg;
                                                        return View("StudentsDetailsImport");
                                                    }
                                                    else
                                                    {

                                                        int tc7 = 0;
                                                    string gender = "";
                                                    string[] arr7 = chkgender.Split(',');
                                                    for (int i = 0; i < arr7.Length; i++)
                                                    {
                                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblgender where GenderName in ('" + arr7[i] + "')"));
                                                        if (chktc == 0)
                                                        {
                                                            tc7 = tc7 + 1;
                                                            if (gender == "")
                                                            {
                                                                gender = arr7[i];
                                                            }
                                                            else
                                                            {
                                                                gender = gender + ", " + arr7[i];
                                                            }
                                                        }
                                                    }
                                                    if (tc7 > 0)
                                                    {
                                                        var msg = "Gender(s): " + gender + " not exist";
                                                        ViewBag.ImportStuStatusFlag = "error";
                                                        ViewBag.ImportStuStatus = msg;
                                                        return View("StudentsDetailsImport");
                                                    }
                                                    else
                                                    {
                                                        int tc8 = 0;
                                                        string category = "";
                                                        string[] arr8 = chkcategory.Split(',');
                                                        for (int i = 0; i < arr8.Length; i++)
                                                        {
                                                            //int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCastCategory where Status=1 and isdeleted is null and CategoryName='" + arr8[i] + "' /*and SchoolID='" + Schoolid + "'*/"));
                                                            int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCastCategory where Status=1 and isdeleted is null and CategoryName='" + arr8[i] + "' "));
                                                            {
                                                                if (chktc == 0)
                                                                {
                                                                    tc8 = tc8 + 1;
                                                                    if (category == "")
                                                                    {
                                                                        category = arr8[i];
                                                                    }
                                                                    else
                                                                    {
                                                                        category = category + ", " + arr8[i];
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        if (tc8 > 0)
                                                        {
                                                            var msg = "Category(s): " + category + " not exist";
                                                            ViewBag.ImportStuStatusFlag = "error";
                                                            ViewBag.ImportStuStatus = msg;
                                                            return View("StudentsDetailsImport");
                                                        }
                                                        else
                                                        {
                                                            int tc9 = 0;
                                                            string status = "";
                                                            string[] arr9 = chkstatus.Split(',');
                                                            for (int i = 0; i < arr9.Length; i++)
                                                            {
                                                                int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblstatus where IsDeleted is null and Status='" + arr9[i] + "' and stStatus='1'"));
                                                                if (chktc == 0)
                                                                {
                                                                    tc9 = tc9 + 1;
                                                                    if (status == "")
                                                                    {
                                                                        status = arr9[i];
                                                                    }
                                                                    else
                                                                    {
                                                                        status = status + ", " + arr9[i];
                                                                    }
                                                                }
                                                            }
                                                            if (tc9 > 0)
                                                            {
                                                                var msg = "Status(s): " + status + " not exist";
                                                                ViewBag.ImportStuStatusFlag = "error";
                                                                ViewBag.ImportStuStatus = msg;
                                                                return View("StudentsDetailsImport");
                                                            }
                                                            else
                                                            {
                                                                int tc12 = 0;
                                                                string country = "";
                                                                string[] arr12 = chkcountry.Split(',');
                                                                for (int i = 0; i < arr12.Length; i++)
                                                                {
                                                                    int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblCountry where Status=1 and isdeleted is null and CountryName='" + arr12[i] + "'"));
                                                                    if (chktc == 0)
                                                                    {
                                                                        tc12 = tc12 + 1;
                                                                        if (country == "")
                                                                        {
                                                                            country = arr12[i];
                                                                        }
                                                                        else
                                                                        {
                                                                            country = country + ", " + arr12[i];
                                                                        }
                                                                    }
                                                                }
                                                                if (tc12 > 0)
                                                                {
                                                                    var msg = "Country Name(s): " + country + " not exist";
                                                                    ViewBag.ImportStuStatusFlag = "error";
                                                                    ViewBag.ImportStuStatus = msg;
                                                                    return View("StudentsDetailsImport");
                                                                }
                                                                else
                                                                {
                                                                    int tc11 = 0;
                                                                    string state = "";
                                                                    string[] arr11 = chkstate.Split(',');
                                                                    for (int i = 0; i < arr11.Length; i++)
                                                                    {
                                                                        int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblstate where isdeleted is null and statename in ('" + arr11[i] + "')"));
                                                                        if (chktc == 0)
                                                                        {
                                                                            tc11 = tc11 + 1;
                                                                            if (state == "")
                                                                            {
                                                                                state = arr11[i];
                                                                            }
                                                                            else
                                                                            {
                                                                                state = state + ", " + arr11[i];
                                                                            }
                                                                        }
                                                                    }
                                                                    if (tc11 > 0)
                                                                    {
                                                                        var msg = "State Name(s): " + state + " not exist";
                                                                        ViewBag.ImportStuStatusFlag = "error";
                                                                        ViewBag.ImportStuStatus = msg;
                                                                        return View("StudentsDetailsImport");
                                                                    }
                                                                    else
                                                                    {
                                                                        int tc10 = 0;
                                                                        string city = "";
                                                                        string[] arr10 = chkcity.Split(',');
                                                                        for (int i = 0; i < arr10.Length; i++)
                                                                        {
                                                                            int chktc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblcity where isdeleted is null and cityname in ('" + arr10[i] + "')"));
                                                                            if (chktc == 0)
                                                                            {
                                                                                tc10 = tc10 + 1;
                                                                                if (city == "")
                                                                                {
                                                                                    city = arr10[i];
                                                                                }
                                                                                else
                                                                                {
                                                                                    city = city + ", " + arr10[i];
                                                                                }
                                                                            }
                                                                        }
                                                                        if (tc10 < 0)
                                                                        {
                                                                    var msg = "City Name(s): " + city + " not exist";
                                                                    ViewBag.ImportStuStatusFlag = "error";
                                                                    ViewBag.ImportStuStatus = msg;
                                                                    return View("StudentsDetailsImport");
                                                                }
                                                                        else
                                                                        {
                                                                            string consString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                                                                            using (SqlConnection con = new SqlConnection(consString))
                                                                            {

                                                                                    foreach (DataRow dr in dtable.Rows)
                                                                                    {

                                                                                        CodeGenMaster datas = getRegCode("Student");
                                                                                        var perfix = datas.Prefix;
                                                                                        var suffix = datas.Suffix;
                                                                                        var last = datas.LastSeries;
                                                                                        var first = datas.StartSeries;
                                                                                        var sep = datas.Seprator;
                                                                                        var incrmentNo = datas.DocNo;
                                                                                        var School = datas.SchoolID;

                                                                                        string StuCode = "";
                                                                                        if (last == "")
                                                                                        {
                                                                                            //StuCode = perfix + sep + School + sep + suffix + sep + first;
                                                                                            StuCode = perfix + sep + suffix + sep + first;
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            int newcode = Convert.ToInt32(last) + Convert.ToInt32(incrmentNo);
                                                                                            StuCode = perfix + sep + suffix + sep + newcode.ToString("D" + first.Length + "");
                                                                                            //StuCode = perfix + sep + School + sep + suffix + sep + newcode.ToString("D" + first.Length + "");
                                                                                        }

                                                                                        string StuUserid = StuCode.Replace("/", "").Replace("/", "").Replace("/", "");
                                                                                        string ParentUserid = "GR" + StuUserid;
                                                                                        MD5 md5 = new MD5CryptoServiceProvider();

                                                                                        //compute hash from the bytes of text  
                                                                                        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(StuUserid));

                                                                                        //get hash result after compute it  
                                                                                        byte[] result = md5.Hash;

                                                                                        StringBuilder strBuilderPPWD = new StringBuilder();
                                                                                        for (int i = 0; i < result.Length; i++)
                                                                                        {
                                                                                            //change it into 2 hexadecimal digits  
                                                                                            //for each byte  
                                                                                            strBuilderPPWD.Append(result[i].ToString("x2"));
                                                                                        }
                                                                                        string StudentPWD = strBuilderPPWD.ToString();



                                                                                        //

                                                                                        MD5 md51 = new MD5CryptoServiceProvider();

                                                                                        //compute hash from the bytes of text  
                                                                                        md51.ComputeHash(ASCIIEncoding.ASCII.GetBytes(ParentUserid));

                                                                                        //get hash result after compute it  
                                                                                        byte[] result1 = md51.Hash;

                                                                                        StringBuilder strBuilderPPWD1 = new StringBuilder();
                                                                                        for (int i = 0; i < result1.Length; i++)
                                                                                        {
                                                                                            //change it into 2 hexadecimal digits  
                                                                                            //for each byte  
                                                                                            strBuilderPPWD1.Append(result1[i].ToString("x2"));
                                                                                        }

                                                                                        string ParentsPWD = strBuilderPPWD1.ToString();

                                                                                        //

                                                                                        //string EmployeeCode = dr["EmployeeCode"].ToString();
                                                                                        string RollNo = dr["RollNo"].ToString(); ;
                                                                                        int AcadmicYear = Convert.ToInt32(obj.ExecuteScaler("select ID from tblacademicyear where Status=1 and SchoolID='" + Schoolid + "' and IsDeleted is null and year(datefrom)='" + (dr["AcadmicYear"].ToString()).Split('-')[0] + "' and  RIGHT(CONVERT(VARCHAR(8), dateto, 1),2)='" + (dr["AcadmicYear"].ToString()).Split('-')[1] + "'"));
                                                                                        
                                                                                        string AdmissionDate = Convert.ToDateTime(dr["AdmissionDate"].ToString()).ToString("dd MMM yyyy").ToString();
                                                                                        int Classid = Convert.ToInt32(obj.ExecuteScaler("select id from tblCourses where coursename='" + dr["Class"].ToString().Trim() + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1"));
                                                                                        int Section = Convert.ToInt32(obj.ExecuteScaler("select id from tblsections where SectionName='" + dr["Section"].ToString().Trim() + "' and IsDeleted is  null and ClassId=(select id from tblCourses where coursename='" + dr["Class"].ToString().Trim() + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                                                                        int Stream = -1;
                                                                                        if (dr["Stream"].ToString() == "")
                                                                                        {
                                                                                            Stream = -1;

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Stream = Convert.ToInt32(obj.ExecuteScaler("select id from tblStream where StreamName='" + dr["Stream"].ToString().Trim() + "' and IsDeleted is  null and Class=(select id from tblCourses where coursename='" + dr["Class"].ToString().Trim() + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                                                                        }

                                                                                    int Batch = Convert.ToInt32(obj.ExecuteScaler("select id from tblbatch where BatchName='" + dr["Batch"].ToString().Trim() + "' and Status=1 and Classid=(select id from tblCourses where coursename='" + dr["Class"].ToString().Trim() + "' and IsDeleted is null and SchoolID='" + Schoolid + "' and status=1)"));
                                                                                    //int Batch = Convert.ToInt32(dr["Batch"]);
                                                                                    string FirstName = dr["FirstName"].ToString();
                                                                                    string MiddleName = dr["MiddleName"].ToString();
                                                                                    string LastName = dr["LastName"].ToString();
                                                                                    string DOB = dr["DOB"].ToString();
                                                                                    int Gender = Convert.ToInt32(obj.ExecuteScaler("select gender_id from tblgender where GenderName in ('" + dr["Gender"].ToString().Trim() + "')"));
                                                                                    string Nationality = dr["Nationality"].ToString();
                                                                                    //int Category = Convert.ToInt32(obj.ExecuteScaler("select catid from tblCastCategory where Status=1 and isdeleted is null and CategoryName='" + dr["Category"].ToString().Trim() + "' and SchoolID='" + Schoolid + "'"));
                                                                                    int Category = Convert.ToInt32(obj.ExecuteScaler("select catid from tblCastCategory where Status=1 and isdeleted is null and CategoryName='" + dr["Category"].ToString().Trim() + "' "));
                                                                                    string ResidentalAddress = dr["ResidentalAddress"].ToString();
                                                                                    int Country = Convert.ToInt32(obj.ExecuteScaler("select countryid from tblCountry where Status=1 and isdeleted is null and CountryName='" + dr["Country"].ToString().Trim() + "'"));
                                                                                    int State = Convert.ToInt32(obj.ExecuteScaler("select stateid from tblstate where isdeleted is null and statename in ('" + dr["State"].ToString().Trim() + "')"));
                                                                                    int City = Convert.ToInt32(obj.ExecuteScaler("select id from tblcity where isdeleted is null and cityname in ('" + dr["City"].ToString().Trim() + "')"));
                                                                                    string PinCode = dr["PinCode"].ToString();
                                                                                    string PhoneNumberForSMS = dr["PhoneNumberForSMS"].ToString();
                                                                                    string FatherName = dr["FatherName"].ToString();
                                                                                    string FMobileNo = dr["FMobileNo"].ToString();
                                                                                    string MMobileNo = dr["MMobileNo"].ToString();
                                                                                    string MotherName = dr["MotherName"].ToString();
                                                                                    string EmergencyContactNo = dr["EmergencyContactNo"].ToString();
                                                                                    string ContactPerson = dr["ContactPerson"].ToString();
                                                                                    string Relationship = dr["Relationship"].ToString();
                                                                                    int Status = Convert.ToInt32(obj.ExecuteScaler("select statusid from tblstatus where IsDeleted is null and Status='" + dr["Status"].ToString().Trim() + "' and stStatus='1'"));

                                                                                    //string str = "insert into TBLStudent(RollNo,regno,suserid,spwd,puserid,ppwd,AcademicYear,JoiningDate,classid,sectionid,StreamID,BatchID,FirstName,MiddleName,LastName,dob,GenderID,Nationality,categoryid,PermanentAddress,PermCountry,PermState,PermCity,CorsCountry,CorsState,CorsCity,PermPincode,SMSmobileNo,FatherName,fmobile,mothername,Mmobile,EmerContPerson,EmergencyNo,Relation,Status,SchoolID)values ('"+RollNo+"','" + StuCode + "','" + StuUserid + "','" + StudentPWD + "','" + ParentUserid + "','" + ParentsPWD + "','" + AcadmicYear + "','" + AdmissionDate + "','" + Classid + "','" + Section + "','" + Stream + "','" + Batch + "','" + FirstName + "','" + MiddleName + "','" + LastName + "','" + DOB + "','" + Gender + "','" + Nationality + "','" + Category + "','" + ResidentalAddress + "','" + Country + "','" + State + "','" + City + "','"+ Country + "','"+ State + "','"+ City + "','" + PinCode + "','" + PhoneNumberForSMS + "','" + FatherName + "','" + FMobileNo + "','" + MotherName + "','" + MMobileNo + "','" + EmergencyContactNo + "','" + ContactPerson + "','" + Relationship + "','" + Status + "','" + Schoolid + "')";,Mmobile,EmerContPerson,EmergencyNo,Relation,Status,SchoolID
                                                                                    string str = "insert into TBLStudent(RollNo,regno,suserid,spwd,puserid,ppwd,AcademicYear,JoiningDate,classid,sectionid,StreamID,BatchID,FirstName,MiddleName,LastName,dob,GenderID,Nationality,categoryid,PermanentAddress,PermCountry,PermState,PermCity,CorsCountry,CorsState,CorsCity,PermPincode,SMSmobileNo,FatherName,fmobile,mothername,Mmobile,EmerContPerson,EmergencyNo,ContPersRelation,Status,SchoolID)values ('" + RollNo + "','" + StuCode + "','" + StuUserid + "','" + StudentPWD + "','" + ParentUserid + "','" + ParentsPWD + "','" + AcadmicYear + "','" + AdmissionDate + "','" + Classid + "','" + Section + "','" + Stream + "','" + Batch + "','" + FirstName + "','" + MiddleName + "','" + LastName + "','" + DOB + "','" + Gender + "','" + Nationality + "','" + Category + "','" + ResidentalAddress + "','" + Country + "','" + State + "','" + City + "','" + Country + "','" + State + "','" + City + "','" + PinCode + "','" + PhoneNumberForSMS + "','" + FatherName + "','" + FMobileNo + "','" + MotherName + "','" + MMobileNo + "','" + ContactPerson  + "','" + EmergencyContactNo + "','" + Relationship + "','" + Status + "','" + Schoolid + "')";
                                                                                    con.Open();
                                                                                    SqlCommand cmd = new SqlCommand(str, con);
                                                                                    cmd.ExecuteNonQuery();
                                                                                    int SID = Convert.ToInt32(obj.ExecuteScaler("select top 1 Id from TBLStudent where IsDeleted is null and schoolid='" + Schoolid + "' order by id desc "));
                                                                                        string strS = "insert into tblStudentFee1(StudentId,AcademicYear,classid,sectionid,SchoolID)values ('"+ SID + "','" + AcadmicYear + "','" + Classid + "','" + Section + "','" + Schoolid + "')";
                                                                                       
                                                                                        SqlCommand cmdS = new SqlCommand(strS, con);
                                                                                        cmdS.ExecuteNonQuery();

                                                                                        string lastval = obj.ExecuteScaler("select LastSeries from tblDocumentNo where UserType='Student' and Status=1 and schoolid='" + Schoolid + "'");
                                                                                    string incemtno = obj.ExecuteScaler("select DocumentNo from tblDocumentNo where UserType='Student' and Status=1 and schoolid='" + Schoolid + "'");
                                                                                    if (lastval == "")
                                                                                    {
                                                                                        lastval = "0";
                                                                                    }

                                                                                    var lastupdate = Int64.Parse(lastval) + Int64.Parse(incemtno);
                                                                                    string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                                                                                    SqlCommand cmd1 = new SqlCommand("update tblDocumentNo set LastSeries='" + lastupdate + "' where UserType='Student' and Status=1 and schoolid='" + Schoolid + "'", con);
                                                                                    cmd1.ExecuteNonQuery();
                                                                                    con.Close();

                                                                            //string SchoolName = obj.ExecuteScaler("select School from tblSchoolDetails where IsDeleted is null and ID='" + Schoolid + "'");
                                                                            //string SchoolCODE = obj.ExecuteScaler(" select SchoolCode from tblSchoolDetails where IsDeleted is null and ID='" + Schoolid + "'");
                                                                            //string authKey = "12920AvJSHCkt5d4cfe2c";
                                                                            ////Multiple mobiles numbers separated by comma
                                                                            //string mobileNumber = FMobileNo;
                                                                            ////Sender ID,While using route4 sender id should be 6 characters long.
                                                                            //string senderId = "ZEUSAD";
                                                                            ////Your message to send, Add URL encoding here.
                                                                            //StringBuilder st = new StringBuilder();
                                                                            //st.AppendLine("Hi Sir/mam,");
                                                                            //st.AppendLine("your Web Login Information");
                                                                            //st.AppendLine("Login URL :" + Request.Url.Authority);
                                                                            //st.AppendLine("SCHOOL CODE :" + SchoolCODE);
                                                                            //st.AppendLine("Student UserID :" + StuUserid);
                                                                            //st.AppendLine("Student Password :" + StuUserid);
                                                                            //st.AppendLine("Parents UserID :" + ParentUserid);
                                                                            //st.AppendLine("Parents Password :" + ParentUserid);
                                                                            //st.AppendLine("your App Login Information");
                                                                            //st.AppendLine("SCHOOL CODE :" + SchoolCODE);
                                                                            //st.AppendLine("UserID :" + StuUserid);
                                                                            //st.AppendLine("Password :" + StuUserid);
                                                                            //st.AppendLine("");
                                                                            //st.AppendLine("Regards");
                                                                            //st.AppendLine(SchoolName);
                                                                            //string message = HttpUtility.UrlEncode(st.ToString());

                                                                            ////Prepare you post parameters
                                                                            //StringBuilder sbPostData = new StringBuilder();
                                                                            //sbPostData.AppendFormat("authkey={0}", authKey);
                                                                            //sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                                                            //sbPostData.AppendFormat("&message={0}", message);
                                                                            //sbPostData.AppendFormat("&sender={0}", senderId);
                                                                            //sbPostData.AppendFormat("&route={0}", 4);

                                                                            //try
                                                                            //{
                                                                            //    //Call Send SMS API
                                                                            //    string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                                                            //    //Create HTTPWebrequest
                                                                            //    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                                                            //    //Prepare and Add URL Encoded data
                                                                            //    UTF8Encoding encoding = new UTF8Encoding();
                                                                            //    byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                                                            //    //Specify post method
                                                                            //    httpWReq.Method = "POST";
                                                                            //    httpWReq.ContentType = "application/x-www-form-urlencoded";
                                                                            //    httpWReq.ContentLength = data1.Length;
                                                                            //    using (Stream stream = httpWReq.GetRequestStream())
                                                                            //    {
                                                                            //        stream.Write(data1, 0, data1.Length);
                                                                            //    }
                                                                            //    //Get the response
                                                                            //    HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                                                            //    StreamReader reader = new StreamReader(response1.GetResponseStream());
                                                                            //    string responseString = reader.ReadToEnd();

                                                                            //    //Close the response
                                                                            //    reader.Close();
                                                                            //    response1.Close();
                                                                            //}
                                                                            //catch (SystemException ex)
                                                                            //{
                                                                            //    ex.Message.ToString();
                                                                            //}
                                                                          
                                                                                }
                                                                        con.Close();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    }

                                                }
                                            }/*Roll no*/ 
                                        }
                                    }
                                }
                                //}
                            }

                            catch (Exception ex)
                            {
                                //throw ex;
                                ViewBag.ImportStuStatusFlag = "success";
                                ViewBag.ImportStuStatus = "Data imported successfully!!";
                                return View("StudentsDetailsImport");

                            }


                            //deleting excel file from folder  
                            if ((System.IO.File.Exists(pathToExcelFile)))
                            {
                                System.IO.File.Delete(pathToExcelFile);
                            }
                            ViewBag.ImportStuStatusFlag = "success";
                            ViewBag.ImportStuStatus = "Data imported successfully!!";
                            return View("StudentsDetailsImport");
                        }
                    }
                }
                else
                {
                    //alert message for invalid file format  
                    var msg = "Only Excel file format is allowed!!";
                    ViewBag.ImportStuStatusFlag = "error";
                    ViewBag.ImportStuStatus = msg;
                    return View("StudentsDetailsImport");
                }
            }
            else
            {
                var msg = "Please choose Excel file!!";
                ViewBag.ImportStuStatusFlag = "error";
                ViewBag.ImportStuStatus = msg;
                return View("StudentsDetailsImport");
            }
        }

        [HttpPost]
        public ActionResult ImportStudentAttendence(HttpPostedFileBase FileUpload)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            string Schoolid = (string)Session["schoolid"];
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                string yrst = Request.Params["ddlAttendenceYear"].ToString();
                string Months = Request.Params["ddlAttendenceMonths"].ToString();
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Table1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    for (int i = dtable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtable.Rows[i][1] == DBNull.Value)
                            dtable.Rows[i].Delete();
                    }
                    dtable.AcceptChanges();

                    for (int kk = 0; kk < dtable.Rows.Count; kk++)
                    {
                        if (dtable.Rows[kk][2].ToString().Length > 10)
                        {
                            string dept = dtable.Rows[kk][2].ToString();
                            var depart = db.tblDepartmnets.Any(s => s.DepartmentName == dept);
                            if (!depart)
                            {
                                Response.Redirect("ImportStudentAttendence?m=dept");
                                return View("ImportStudentAttendence");
                            }
                        }


                        if (dtable.Rows[kk][3].ToString().Length > 10)
                        {
                            string desig = dtable.Rows[kk][3].ToString();
                            var designation = db.tblDesignations.Any(s => s.Designation == desig);
                            if (!designation)
                            {
                                Response.Redirect("ImportStudentAttendence?m=desig");
                                return View("ImportStudentAttendence");
                            }
                        }

                        if (dtable.Rows[kk][4].ToString().Length > 10)
                        {
                            string qual = dtable.Rows[kk][4].ToString();
                            var qualfi = db.tblQualifications.Any(s => s.QualificationName == qual);
                            if (!qualfi)
                            {
                                Response.Redirect("ImportStudentAttendence?m=qual");
                                return View("ImportStudentAttendence");
                            }
                        }

                        if (dtable.Rows[kk][6].ToString().Length > 10)
                        {
                            string staff = dtable.Rows[kk][6].ToString();
                            var staffcat = db.tblStaffCategories.Any(s => s.Name == staff);
                            if (!staffcat)
                            {
                                Response.Redirect("ImportStudentAttendence?m=staf");
                                return View("ImportStudentAttendence");
                            }
                        }

                    }
                    sqlHelper obj = new sqlHelper();
                    int size = dtable.Columns.Count;

                    var invalidcode = "";
                    for (int j = 0; j < dtable.Rows.Count; j++)
                    {
                        for (int k = 1; k < size; k++)
                        {
                            string colname = dtable.Columns[k].ColumnName;
                            //   new System.Collections.ArrayList.ArrayListDebugView(dtable.Columns.List).Items[1]
                            string attendenceDate = Months + "/" + colname + "/" + yrst;
                            string val = dtable.Rows[j][k].ToString();
                            if (val != "")
                            {
                                string StudentCode = dtable.Rows[j]["StudentCode"].ToString();
                                string IsValidEmployee = obj.ExecuteScaler("select RegNo EmplyeeName from TBLStudent where RegNo='" + StudentCode + "' and IsDeleted is null and SchoolID='"+Schoolid+"' ");
                                if (IsValidEmployee != null)
                                {
                                    string stdId = obj.ExecuteScaler("select ID from TBLStudent where RegNo='" + StudentCode + "' and Isdeleted is null and SchoolID='"+Schoolid+"'");
                                    string classId = obj.ExecuteScaler("select ClassID Class from TBLStudent where RegNo='" + StudentCode + "' and IsDeleted is null and SchoolID='"+Schoolid+"'");
                                    string sectionId = obj.ExecuteScaler("select SectionID Section from TBLStudent where RegNo='" + StudentCode + "' and IsDeleted is null and SchoolID='" + Schoolid + "' ");
                                    string TeacherId = obj.ExecuteScaler("select intEmpID from tblClassTeacherAllocation where ClassID=" + classId + " and SectionID='"+sectionId+"' and SchoolID='"+Schoolid+"'");

                                    if (val == "P")
                                    {
                                        val = "Present";
                                    }
                                    else if (val == "A")
                                    {
                                        val = "Absent";
                                    }
                                    else
                                    {
                                        val = "Leave";
                                    }

                                    string[] cols = { "StudentId", "TeacherId", "ClassId", "SectionId", "AttendenceType", "AttendenceDate","SchoolID" };
                                    object[] vals = { stdId, TeacherId, classId, sectionId, val, attendenceDate,Schoolid };
                                    obj.insertValIntoTable("tblStudentAttendence", cols, vals);
                                }
                                else
                                {
                                    if (invalidcode == "")
                                    {
                                        invalidcode = StudentCode;
                                    }
                                    else
                                    {
                                        if (!invalidcode.Contains(StudentCode))
                                        {
                                            invalidcode = invalidcode + ',' + StudentCode;
                                        }
                                    }
                                }
                            }

                        }

                    }


                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    if (invalidcode == "")
                    {
                        ViewBag.ImportFlag = "success";
                        ViewBag.ResultStatus = "Student attendence details imported successfully...";
                    }
                    else
                    {
                        ViewBag.ImportFlag = "error";
                        ViewBag.ResultStatus = "Student code(s): " + invalidcode + " not exist!!";
                    }

                    return View("ImportStudentAttendence");
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UploadStudentExcelImport(HttpPostedFileBase FileUpload)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Images/ImportFile/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    for (int kk = 0; kk < dtable.Rows.Count; kk++)
                    {
                        if (dtable.Rows[kk][17].ToString().Length>10)
                        {
                           Response.Redirect("StudentsDetailsImport?m=p");
                            return View("StudentsDetailsImport");
                        }
                        if (dtable.Rows[kk][19].ToString().Length>10) {
                            Response.Redirect("StudentsDetailsImport?m=f");
                            return View("StudentsDetailsImport");
                        }

                        if (dtable.Rows[kk][21].ToString().Length > 10)
                        {
                            Response.Redirect("StudentsDetailsImport?m=m");
                            return View("StudentsDetailsImport");
                        }
                        if (dtable.Rows[kk][22].ToString().Length > 10)
                        {
                            Response.Redirect("StudentsDetailsImport?m=e");
                            return View("StudentsDetailsImport");
                        }


                        if (dtable.Rows[kk][3].ToString()!="")
                        {
                            string className = dtable.Rows[kk][3].ToString();
                            var classe = db.tblCourses.Any(s => s.CourseName == className);
                            if (!classe)
                            {
                                Response.Redirect("StudentsDetailsImport?m=cl");
                                return View("StudentsDetailsImport");
                            }

                        }


                        if (dtable.Rows[kk][4].ToString() != "")
                        {
                            string section = dtable.Rows[kk][4].ToString();
                            var classe = db.tblSections.Any(s => s.SectionName == section);
                            if (!classe)
                            {
                                Response.Redirect("StudentsDetailsImport?m=sec");
                                return View("StudentsDetailsImport");
                            }

                        }

                        //if (dtable.Rows[kk][5].ToString() != "")
                        //{
                        //    string stream = dtable.Rows[kk][5].ToString();
                        //    var stea = db.tblStreams.Any(s => s.StreamName == stream);
                        //    if (!stea)
                        //    {
                        //        Response.Redirect("StudentsDetailsImport?m=steam");
                        //        return View("StudentsDetailsImport");
                        //    }

                        //}

                        if (dtable.Rows[kk][10].ToString() != "")
                        {
                            string gender = dtable.Rows[kk][10].ToString();
                            var genders = db.tblGenders.Any(s => s.GenderName == gender);
                            if (!genders)
                            {
                                Response.Redirect("StudentsDetailsImport?m=gen");
                                return View("StudentsDetailsImport");
                            }

                        }

                        if (dtable.Rows[kk][12].ToString() != "")
                        {
                            string category = dtable.Rows[kk][12].ToString();
                            var genders = db.tblCastCategories.Any(s => s.CategoryName == category);
                            if (!genders)
                            {
                                Response.Redirect("StudentsDetailsImport?m=cast");
                                return View("StudentsDetailsImport");
                            }

                        }

                        if (dtable.Rows[kk][15].ToString() != "")
                        {
                            string state = dtable.Rows[kk][15].ToString();
                            var states = db.tblStates.Any(s => s.StateName == state);
                            if (!states)
                            {
                                Response.Redirect("StudentsDetailsImport?m=stat");
                                return View("StudentsDetailsImport");
                            }

                        }
                        if (dtable.Rows[kk][25].ToString() != "")
                        {
                            string statuss = dtable.Rows[kk][25].ToString();
                            var staa = db.tblStatus.Any(s => s.Status == statuss);
                            if (!staa)
                            {
                                Response.Redirect("StudentsDetailsImport?m=staa");
                                return View("StudentsDetailsImport");
                            }

                        }

                    }

                    for (int i = dtable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtable.Rows[i][1] == DBNull.Value)
                            dtable.Rows[i].Delete();
                    }
                    dtable.AcceptChanges();

                    try
                    {

                        string consString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_ImportStudentImportExcelData"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@tblStudentsExcel", dtable);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                       
                    }

                   
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    Response.Redirect("StudentsDetailsImport?m=1");
                    return View("StudentsDetailsImport");
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [System.Web.Http.Route("api/EmployeeAPI/getRegCode")]
        [System.Web.Http.HttpPost]
        public CodeGenMaster getRegCode(string type)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)Session["schoolid"];
            sqlHelper obj = new sqlHelper();
            SqlDataReader dr = obj.GetReader("select * from tblDocumentNo where Status=1 and UserType='" + type + "' and schoolid='" + Schoolid + "'");
            //SqlDataReader dr1 = obj.GetReader("select Top 1 FirstName from tblstudent ORDER BY id DESC");
            CodeGenMaster usr = new CodeGenMaster();
            if (dr.Read())
            {
                usr.Id = dr["id"].ToString();
                usr.DocType = dr["UserType"].ToString();
                usr.Prefix = dr["Perfix"].ToString();
                usr.Suffix = dr["Suffix"].ToString();
                usr.DocNo = dr["DocumentNo"].ToString();
                usr.StartSeries = dr["StartSeries"].ToString();
                usr.Seprator = dr["Serprator"].ToString();
                usr.LastSeries = dr["LastSeries"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                usr.SchoolID = dr["SchoolID"].ToString();
            }

            return usr;
        }

    }
}