using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolErp.Models;
using schoolERP_BLL;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.IO;
using System.Globalization;
using System.Collections;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
//using Microsoft.AspNet.WebApi.Core;
//using Microsoft.AspNet.WebApi.Owin;
 
using System.Web.Helpers;

namespace SchoolErp.Controllers.WebApi
{
    public class EmployeeAPIController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/EmployeeAPI/deleteEmployeeById")]
        [System.Web.Http.HttpGet]
        public string deleteEmployeeById(Int32 id)
        {
            int idd = Convert.ToInt32(id);
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            // SqlCommand cmd = new SqlCommand("delete from tblCountry where CountryID=" + id, con);
            SqlCommand cmd = new SqlCommand("Update tblemployee set IsDeleted=1,Deleted_on='" + DateTime.Now + "' where  id=" + id, con);
            cmd.ExecuteNonQuery();
            con.Close();
           
            return "Employee Deleted Successfully";

        }

      



        [System.Web.Http.Route("api/EmployeeAPI/getAllDepartmentsBySchoolID")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllDepartmentsBySchoolID(List <string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select DepartmentId,DepartmentName,Status from tblDepartmnet where Status=1 and SchoolID='"+ SchoolID + "' and IsDeleted is null order by LOWER(DepartmentName)");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["DepartmentId"].ToString();
                usr.Name = dr["DepartmentName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllDepartments")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllDepartments()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)HttpContext.Current.Session["schoolid"];
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select DepartmentId,DepartmentName,Status from tblDepartmnet where Status=1 and schoolid='" + Schoolid + "' and isdeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["DepartmentId"].ToString();
                usr.Name = dr["DepartmentName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getRegCode")]
        [System.Web.Http.HttpPost]
        public CodeGenMaster getRegCode(List<string> aa)
        {           
            int SchoolID = Convert.ToInt32(aa[0]);
            sqlHelper obj = new sqlHelper();
            CodeGenMaster usr = new CodeGenMaster();
           
                SqlDataReader dr = obj.GetReader("select * from tblDocumentNo where Status=1 and UserType='Employee' and SchoolID='"+ SchoolID + "' and IsDeleted is null ");
                //SqlDataReader dr1 = obj.GetReader("select Top 1 FirstName from tblstudent ORDER BY id DESC");
                
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





        [System.Web.Http.Route("api/EmployeeAPI/getAllDepartmentsNew")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllDepartmentsNew(List<string> val)
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            string i = val[0].ToString();
            DataTable dt = obj.getDataTable(@"select DepartmentId,DepartmentName,Status from tblDepartmnet where Status=1 and SchoolID=" + Convert.ToInt32(i) + "");

            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["DepartmentId"].ToString();
                usr.Name = dr["DepartmentName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllDesignationNew")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllDesignationNew(List<string> val)
        {
            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            string i = val[0].ToString();
            DataTable dt = obj.getDataTable(@"select DesigID,Designation,Status from tblDesignation where Status=1 and SchoolID=" + Convert.ToInt32(i) + "");
            foreach (DataRow dr in dt.Rows)
            {
                BatchMaster usr = new BatchMaster();
                usr.Id = dr["DesigID"].ToString();
                usr.Name = dr["Designation"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllDesignationBySchoolID")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllDesignationBySchoolID(List<string> id)
        {
         

            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select DesigID,Designation,Status from tblDesignation where Status=1 and SchoolID =" + id[0] + " and IsDeleted is null  ORDER BY LOWER(Designation)");
            foreach (DataRow dr in dt.Rows)
            {
                BatchMaster usr = new BatchMaster();
                usr.Id = dr["DesigID"].ToString();
                usr.Name = dr["Designation"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllDesignation")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllDesignation()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)HttpContext.Current.Session["schoolid"];
            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select DesigID,Designation,Status from tblDesignation where Status=1 and schoolid='" + Schoolid + "' and isdeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                BatchMaster usr = new BatchMaster();
                usr.Id = dr["DesigID"].ToString();
                usr.Name = dr["Designation"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }
        [System.Web.Http.Route("api/EmployeeAPI/getAllQualficationbySchool")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllQualficationbySchool(List <string > id)
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select QualificationId,QualificationName,Status from tblQualifications where Status=1 and SchoolID='"+ id[0]+"' and IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["QualificationId"].ToString();
                usr.Name = dr["QualificationName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllQualfication")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllQualfication()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)HttpContext.Current.Session["schoolid"];
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select QualificationId,QualificationName,Status from tblQualifications where Status=1 and schoolid='" + Schoolid + "'  and isdeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["QualificationId"].ToString();
                usr.Name = dr["QualificationName"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllCountry")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllCountry()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select * from tblCountry where Status=1 and IsDeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/getAllstatebyCountryId")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllstatebyCountryId(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = "select * from  tblState where countryId=" + id[0] + " and Status=1 and IsDeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["StateName"].ToString();
                usr.Id = dr["StateId"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/getCityByStateId")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getCityByStateId(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select Id,CityName from tblCity where Status=1 and StateId=" + id[0] + " and IsDeleted is null ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CityName"].ToString();
                usr.Id = dr["Id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllDocumentForEmployee")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllDocumentForEmployee(List<string> aa)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            string userlogin = aa[0];
            int   usertype = Convert.ToInt32( aa[1]);
            if (usertype==1 || usertype==3)
            {
                con.Open();


                string query = "select * from tbldocument doc left join tblEmployee em on doc.SchoolID = em.SchoolID where doc.userId = 3 and  em.UserID = '"+ userlogin + "' and doc.IsDeleted is null ";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CountyMaster usr = new CountyMaster();
                    usr.Name = dr["DocumentName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    //  usr.Status = bool.Parse(dr["Status"].ToString());
                    list.Add(usr);
                }
                con.Close();
            }
            if (usertype==6)
            {
                con.Open();


                string query = " select * from tbldocument doc left join tblSchoolDetails sc on sc.ID = doc.SchoolID where doc.userId = 3 and sc.SchoolCode = '"+userlogin+"' and doc.IsDeleted is null";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    CountyMaster usr = new CountyMaster();
                    usr.Name = dr["DocumentName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    //  usr.Status = bool.Parse(dr["Status"].ToString());
                    list.Add(usr);
                }
                con.Close();
            }
           
           
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllGender")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllGender()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = "select * from tblGender where Status=1";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["GenderName"].ToString();
                usr.Id = dr["gender_id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }
        [System.Web.Http.Route("api/EmployeeAPI/getAllGenderbyid")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllGenderbyid(List<string> ab)
        {
            int idd = Convert.ToInt32(ab[0]);
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = " select * from tblGender where Status=1 and gender_id='" + idd + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["GenderName"].ToString();
                usr.Id = dr["gender_id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllMaritalStatus")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllMaritalStatus()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = "select * from tblMaritalStatus where Status=1";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["Id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllReligon")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllReligon()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();


            string query = "select * from tblReligion where Status=1";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["ReligionName"].ToString();
                usr.Id = dr["ReligionId"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllStaff")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllStaff()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblStaffCategory where Status=1";

             SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["Id"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }
        //[System.Web.Http.Route("api/EmployeeAPI/GetMachineAssigntoSchool")]
        //[System.Web.Http.HttpPost]
        //public string GetMachineAssigntoSchool(EmployeeATT ATT)
        //{
        //    int SchoolID = Convert.ToInt32(ATT.SchoolID);
         
        //    //var Machinenos = db.tblEmployees.Where(x => x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
        //    //EmployeeATT A = new EmployeeATT();
        //    return "";

        //}

        [System.Web.Http.Route("api/EmployeeAPI/GetMachineAssigntoSchool")]
        [System.Web.Http.HttpPost]
        public EmployeeATT GetMachineAssigntoSchool(List<int> VAL)
        {
            EmployeeATT avi = new EmployeeATT();

            int SchoolID = Convert.ToInt32(VAL[0]);
           
            var getmachines = db.tblAttendenceMachineMasters.Where(ma => ma.SchoolID == SchoolID && ma.IsActice == true).ToList();
            if (getmachines!=null)
            {
                avi.machineNOO = new List<MachineNO>();
               
                foreach (var mno in getmachines)
                {
                    MachineNO mnoo = new MachineNO();
                    mnoo.MachineNumber  = mno.MachineNo;
                    avi.machineNOO.Add(mnoo);

                }

            }

           

            return avi;



        }
        //
      
            [System.Web.Http.Route("api/EmployeeAPI/LoadOnedayAttendenceEmployee")]
        [System.Web.Http.HttpPost]
        public string LoadOnedayAttendenceEmployee(EmployeeATT ATT)
        {
            int SchoolID = Convert.ToInt32(ATT.SchoolID);
            string StartDate1 = Convert.ToDateTime(ATT.StartDate).ToString("yyyy/MM/dd");
            string EndDate1 = Convert.ToDateTime(ATT.EndDate).ToString("yyyy/MM/dd");

            DateTime StartDate = Convert.ToDateTime(EndDate1);
            DateTime EndDate = Convert.ToDateTime(StartDate1);
            string AA = "";
            int count = 0;
            var getmachines = db.tblAttendenceMachineMasters.Where(ma => ma.SchoolID == SchoolID && ma.IsActice == true && ma.machinetype == "Employee").Select(y => y.MachineNo).ToList();
            if (getmachines != null)
            {
                List<DateTime> allDates = new List<DateTime>();
                for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                {
                    string pdate = date.ToString("yyyy/MM/dd");
                    DateTime punchdate = Convert.ToDateTime(pdate);

                    var Totalemployee = db.tblEmployees.Where(em => em.SchoolID == SchoolID && em.IsDeleted == null).ToList();
                    if (Totalemployee != null)
                    {
                        foreach (var employee in Totalemployee)
                        {
                            tblEmployee em = new tblEmployee();
                            em.Id = employee.Id;
                            //if (employee.BiometricID!=null)  CASE WHEN BIOMATRIC ID NOT ASSIGNED
                            //{
                            var MachineRawPunch = db.Tran_MachineRawPunch.Where(mp => mp.CardNo == employee.BiometricID && mp.PunchDatetime.Year == punchdate.Year && mp.PunchDatetime.Month == punchdate.Month && mp.PunchDatetime.Day == punchdate.Day && getmachines.Contains(mp.MachineNo)).FirstOrDefault();
                            if (MachineRawPunch != null)
                            {
                                var checkattendence = db.tblEmployeeAttendences.Where(emp => emp.EmployeeId == employee.Id && emp.AttendenceDate.Value.Year == MachineRawPunch.PunchDatetime.Year && emp.AttendenceDate.Value.Month == MachineRawPunch.PunchDatetime.Month && emp.AttendenceDate.Value.Day == MachineRawPunch.PunchDatetime.Day).FirstOrDefault();
                                if (checkattendence == null)
                                {
                                    //present
                                    count++;
                                    tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                    InATT.EmployeeId = employee.Id;
                                    InATT.DesigId = employee.DesigID;
                                    InATT.DepId = employee.DeptID;
                                    InATT.AttendenceType = "Present";
                                    InATT.AttendenceDate = MachineRawPunch.PunchDatetime;
                                    InATT.DateCreated = DateTime.Now;
                                    InATT.IsBiometric = true;
                                    InATT.SchoolID = SchoolID;
                                    count++;
                                    db.tblEmployeeAttendences.Add(InATT);
                                    db.SaveChanges();

                                }
                                else
                                {

                                    //cant change manual updated record
                                    if (checkattendence.IsBiometric != false)
                                    {
                                        count++;
                                        tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                        checkattendence.EmployeeId = employee.Id;
                                        checkattendence.DesigId = employee.DesigID;
                                        checkattendence.DepId = employee.DeptID;
                                        checkattendence.AttendenceType = "Present";
                                        checkattendence.AttendenceDate = MachineRawPunch.PunchDatetime;
                                        checkattendence.DateCreated = DateTime.Now;
                                        checkattendence.IsBiometric = true;
                                        checkattendence.SchoolID = SchoolID;
                                        db.SaveChanges();

                                    }


                                }


                            }
                            else
                            {
                                var checkattendence = db.tblEmployeeAttendences.Where(emp => emp.EmployeeId == employee.Id && emp.AttendenceDate.Value.Year == punchdate.Year && emp.AttendenceDate.Value.Month == punchdate.Month && emp.AttendenceDate.Value.Day == punchdate.Day).FirstOrDefault();
                                if (checkattendence == null)
                                {
                                    //mark absent
                                    count++;
                                    tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                    InATT.EmployeeId = employee.Id;
                                    InATT.DesigId = employee.DesigID;
                                    InATT.DepId = employee.DeptID;
                                    InATT.AttendenceType = "Absent";
                                    InATT.DateCreated = DateTime.Now;
                                    InATT.AttendenceDate = punchdate;
                                    InATT.IsBiometric = true;
                                    InATT.SchoolID = SchoolID;

                                    db.tblEmployeeAttendences.Add(InATT);
                                    db.SaveChanges();

                                }
                                //else
                                //{ 

                                // it is obvious that attendence is manualy updated   

                                //}

                            }





                        }




                    }






                }
                if (count > 0)
                {
                    return AA = "Attendence data Loaded";
                }
                else
                {
                    return AA = "No attendence data found";
                }


            }
            else
            {

                //if no machine assinged
                return AA = "No machine assinged to School";
            }
        }
       

       [System.Web.Http.Route("api/EmployeeAPI/LoadAttendenceEmployee1")]
        [System.Web.Http.HttpPost]
        public string LoadAttendenceEmployee1(EmployeeATT ATT)
        {
            int SchoolID = Convert.ToInt32(ATT.SchoolID);
            string StartDate1 = Convert.ToDateTime(ATT.StartDate).ToString("yyyy/MM/dd");
            string EndDate1 = Convert.ToDateTime(ATT.EndDate).ToString("yyyy/MM/dd");

            DateTime StartDate = Convert.ToDateTime(EndDate1);
            DateTime EndDate = Convert.ToDateTime(StartDate1);


            string AttendenceTime = DateTime.Now.ToString("HH: mm:ss tt");
            string TodayDate1 = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime todatdate = Convert.ToDateTime(TodayDate1);


            string AA = "";
            int count = 0;
            var getmachines = db.tblAttendenceMachineMasters.Where(ma => ma.SchoolID == SchoolID && ma.IsActice == true && ma.machinetype== "Employee").Select(y => y.MachineNo).ToList();
            if (getmachines != null)
            {
                

                sqlHelper obj = new sqlHelper();
                List<DateTime> allDates = new List<DateTime>();
                for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                {
                    


                    string pdate = date.ToString("yyyy/MM/dd");
                    DateTime punchdate = Convert.ToDateTime(pdate);

                    var Totalemployee = db.tblEmployees.Where(em => em.SchoolID == SchoolID && em.IsDeleted == null).ToList();
                    if (Totalemployee != null)
                    {
                        foreach (var employee in Totalemployee)
                        {
                            tblEmployee em = new tblEmployee();
                            em.Id = employee.Id;
                            //if (employee.BiometricID!=null)  CASE WHEN BIOMATRIC ID NOT ASSIGNED
                            //{
                            var MachineRawPunch = db.Tran_MachineRawPunch.Where(mp => mp.CardNo == employee.BiometricID && mp.PunchDatetime.Year == punchdate.Year && mp.PunchDatetime.Month== punchdate.Month && mp.PunchDatetime.Day== punchdate.Day && getmachines.Contains(mp.MachineNo)).FirstOrDefault();
                            if (MachineRawPunch != null)
                            {
                                var checkattendence = db.tblEmployeeAttendences.Where(emp => emp.EmployeeId == employee.Id && emp.AttendenceDate.Value.Year == MachineRawPunch.PunchDatetime.Year && emp.AttendenceDate.Value.Month == MachineRawPunch.PunchDatetime.Month && emp.AttendenceDate.Value.Day == MachineRawPunch.PunchDatetime.Day).FirstOrDefault();
                                if (checkattendence == null)
                                {
                                    //present
                                    count++;
                                    tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                    InATT.EmployeeId = employee.Id;
                                    InATT.DesigId = employee.DesigID;
                                    InATT.DepId = employee.DeptID;
                                    InATT.AttendenceType = "Present";
                                    InATT.AttendenceDate = MachineRawPunch.PunchDatetime;
                                    InATT.DateCreated = DateTime.Now;
                                    InATT.IsBiometric = true;
                                    InATT.SchoolID = SchoolID;
                                    count++;
                                    db.tblEmployeeAttendences.Add(InATT);
                                    db.SaveChanges();
                                    string attendencedate1 = Convert.ToDateTime(InATT.AttendenceDate).ToString("yyyy/MM/dd");
                                    DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                    if (todatdate == attendencedatee)
                                    {
                                        //sms

                                        string SMSmobileNo = obj.ExecuteScaler("SELECT Mobile  from tblEmployee where ID='" + employee.Id + "'");
                                        string Employeename = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from tblEmployee where ID='" + employee.Id + "'");
                                        string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + employee.SchoolID + "'");

                                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                        if (Checksmsservice != null)
                                        {
                                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");

                                            string authKey = GetauthKey;
                                            //Multiple mobiles numbers separated by comma
                                            string mobileNumber = SMSmobileNo;
                                            //Sender ID,While using route4 sender id should be 6 characters long.
                                            string senderId = GetsenderId;
                                            //Your message to send, Add URL encoding here.
                                            StringBuilder st = new StringBuilder();
                                            st.AppendLine("Hi Sir/mam,");
                                            st.AppendLine("Employee Name :" + Employeename);
                                            st.AppendLine("Employee Attendence Status :" + InATT.AttendenceType);
                                            st.AppendLine("Attendence Date :" + InATT.AttendenceDate);
                                            st.AppendLine("Attendence Time :" + AttendenceTime);
                                            st.AppendLine("");
                                            st.AppendLine("Regards");
                                            st.AppendLine(Schoolname);
                                            string message = HttpUtility.UrlEncode(st.ToString());

                                            //Prepare you post parameters
                                            StringBuilder sbPostData = new StringBuilder();
                                            sbPostData.AppendFormat("authkey={0}", authKey);
                                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                            sbPostData.AppendFormat("&message={0}", message);
                                            sbPostData.AppendFormat("&sender={0}", senderId);
                                            sbPostData.AppendFormat("&route={0}", 4);
                                            sbPostData.AppendFormat("&unicode={0}", 1);

                                            try
                                            {
                                                //Call Send SMS API
                                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                                //Create HTTPWebrequest
                                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                                //Prepare and Add URL Encoded data
                                                UTF8Encoding encoding = new UTF8Encoding();
                                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                                //Specify post method
                                                httpWReq.Method = "POST";
                                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                                httpWReq.ContentLength = data1.Length;
                                                using (Stream stream = httpWReq.GetRequestStream())
                                                {
                                                    stream.Write(data1, 0, data1.Length);
                                                }
                                                //Get the response
                                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                                string responseString = reader.ReadToEnd();

                                                //Close the response
                                                reader.Close();
                                                response1.Close();
                                            }
                                            catch (SystemException ex)
                                            {
                                                ex.Message.ToString();
                                            }
                                        }

                                      

                                    }


                                }
                                else
                                {
                                   
                                    //cant change manual updated record
                                    if (checkattendence.IsBiometric != false)
                                    {
                                        count++;
                                        tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                        checkattendence.EmployeeId = employee.Id;
                                        checkattendence.DesigId = employee.DesigID;
                                        checkattendence.DepId = employee.DeptID;
                                        checkattendence.AttendenceType = "Present";
                                        checkattendence.AttendenceDate = MachineRawPunch.PunchDatetime;
                                        checkattendence.DateCreated = DateTime.Now;
                                        checkattendence.IsBiometric = true;
                                        checkattendence.SchoolID = SchoolID;
                                        db.SaveChanges();
                                        string attendencedate1 = Convert.ToDateTime(checkattendence.AttendenceDate).ToString("yyyy/MM/dd");
                                        DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                        if (todatdate == attendencedatee)
                                        {
                                            //sms



                                            string SMSmobileNo = obj.ExecuteScaler("SELECT Mobile  from tblEmployee where ID='" + employee.Id + "'");
                                            string Employeename = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from tblEmployee where ID='" + employee.Id + "'");
                                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + employee.SchoolID + "'");
                                            string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                            if (Checksmsservice != null)
                                            {
                                                string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                                string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");

                                                string authKey = GetauthKey;
                                                //Multiple mobiles numbers separated by comma
                                                string mobileNumber = SMSmobileNo;
                                                //Sender ID,While using route4 sender id should be 6 characters long.
                                                string senderId = GetsenderId;
                                                //Your message to send, Add URL encoding here.
                                                StringBuilder st = new StringBuilder();
                                                st.AppendLine("Hi Sir/mam,");
                                                st.AppendLine("Employee Name :" + Employeename);
                                                st.AppendLine("Employee Attendence Status :" + checkattendence.AttendenceType);
                                                st.AppendLine("Attendence Date :" + checkattendence.AttendenceDate);
                                                st.AppendLine("Attendence Time :" + AttendenceTime);
                                                st.AppendLine("");
                                                st.AppendLine("Regards");
                                                st.AppendLine(Schoolname);
                                                string message = HttpUtility.UrlEncode(st.ToString());

                                                //Prepare you post parameters
                                                StringBuilder sbPostData = new StringBuilder();
                                                sbPostData.AppendFormat("authkey={0}", authKey);
                                                sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                                sbPostData.AppendFormat("&message={0}", message);
                                                sbPostData.AppendFormat("&sender={0}", senderId);
                                                sbPostData.AppendFormat("&route={0}", 4);
                                                sbPostData.AppendFormat("&unicode={0}", 1);

                                                try
                                                {
                                                    //Call Send SMS API
                                                    string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                                    //Create HTTPWebrequest
                                                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                                    //Prepare and Add URL Encoded data
                                                    UTF8Encoding encoding = new UTF8Encoding();
                                                    byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                                    //Specify post method
                                                    httpWReq.Method = "POST";
                                                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                                                    httpWReq.ContentLength = data1.Length;
                                                    using (Stream stream = httpWReq.GetRequestStream())
                                                    {
                                                        stream.Write(data1, 0, data1.Length);
                                                    }
                                                    //Get the response
                                                    HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                                    StreamReader reader = new StreamReader(response1.GetResponseStream());
                                                    string responseString = reader.ReadToEnd();

                                                    //Close the response
                                                    reader.Close();
                                                    response1.Close();
                                                }
                                                catch (SystemException ex)
                                                {
                                                    ex.Message.ToString();
                                                }
                                            }

                                            

                                        }


                                    }


                                }


                            }
                          else
                            {
                                var checkattendence = db.tblEmployeeAttendences.Where(emp => emp.EmployeeId == employee.Id && emp.AttendenceDate.Value.Year == punchdate.Year && emp.AttendenceDate.Value.Month == punchdate.Month && emp.AttendenceDate.Value.Day == punchdate.Day).FirstOrDefault();
                                if (checkattendence==null)
                                {
                                    //mark absent
                                    count++;
                                    tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                    InATT.EmployeeId = employee.Id;
                                    InATT.DesigId = employee.DesigID;
                                    InATT.DepId = employee.DeptID;
                                    InATT.AttendenceType = "Absent";
                                    InATT.DateCreated = DateTime.Now;
                                    InATT.AttendenceDate = punchdate;
                                    InATT.IsBiometric = true;
                                    InATT.SchoolID = SchoolID;
                                   
                                    db.tblEmployeeAttendences.Add(InATT);
                                    db.SaveChanges();
                                    string attendencedate1 = Convert.ToDateTime(InATT.AttendenceDate).ToString("yyyy/MM/dd");
                                    DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                    if (todatdate == attendencedatee)
                                    {
                                        //sms
                                        string SMSmobileNo = obj.ExecuteScaler("SELECT Mobile  from tblEmployee where ID='" + employee.Id + "'");
                                        string Employeename = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from tblEmployee where ID='" + employee.Id + "'");
                                        string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + employee.SchoolID + "'");

                                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                        if (Checksmsservice != null)
                                        {
                                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");
                                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + employee.SchoolID + "' and  active=1");


                                            string authKey = GetauthKey;
                                            //Multiple mobiles numbers separated by comma
                                            string mobileNumber = SMSmobileNo;
                                            //Sender ID,While using route4 sender id should be 6 characters long.
                                            string senderId = GetsenderId;
                                            //Your message to send, Add URL encoding here.
                                            StringBuilder st = new StringBuilder();
                                            st.AppendLine("Hi Sir/mam,");
                                            st.AppendLine("Employee Name :" + Employeename);
                                            st.AppendLine("Employee Attendence Status :" + InATT.AttendenceType);
                                            st.AppendLine("Attendence Date :" + InATT.AttendenceDate);
                                            st.AppendLine("Attendence Time :" + AttendenceTime);
                                            st.AppendLine("");
                                            st.AppendLine("Regards");
                                            st.AppendLine(Schoolname);
                                            string message = HttpUtility.UrlEncode(st.ToString());

                                            //Prepare you post parameters
                                            StringBuilder sbPostData = new StringBuilder();
                                            sbPostData.AppendFormat("authkey={0}", authKey);
                                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                            sbPostData.AppendFormat("&message={0}", message);
                                            sbPostData.AppendFormat("&sender={0}", senderId);
                                            sbPostData.AppendFormat("&route={0}", 4);
                                            sbPostData.AppendFormat("&unicode={0}", 1);

                                            try
                                            {
                                                //Call Send SMS API
                                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                                //Create HTTPWebrequest
                                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                                //Prepare and Add URL Encoded data
                                                UTF8Encoding encoding = new UTF8Encoding();
                                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                                //Specify post method
                                                httpWReq.Method = "POST";
                                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                                httpWReq.ContentLength = data1.Length;
                                                using (Stream stream = httpWReq.GetRequestStream())
                                                {
                                                    stream.Write(data1, 0, data1.Length);
                                                }
                                                //Get the response
                                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                                string responseString = reader.ReadToEnd();

                                                //Close the response
                                                reader.Close();
                                                response1.Close();
                                            }
                                            catch (SystemException ex)
                                            {
                                                ex.Message.ToString();
                                            }

                                        }




                                    }

                                }
                                //else
                                //{ 

                                // it is obvious that attendence is manualy updated   

                                //}

                            }





                        }




                    }
                  


                  


                }
                if (count>0)
                {
                    return AA="Attendence data Loaded";
                }
                else
                {
                    return AA= "No attendence data found";
                }
               

            }
            else
            {

                //if no machine assinged
                return AA = "No machine assinged to School";
            }
        }



        [System.Web.Http.Route("api/EmployeeAPI/LoadAttendenceEmployee")]
        [System.Web.Http.HttpPost]
        public string  LoadAttendenceEmployee(EmployeeATT ATT)
        {
            int SchoolID = Convert.ToInt32(ATT.SchoolID);
            string StartDate1= Convert.ToDateTime(ATT.StartDate).ToString("yyyy/MM/dd");
            string EndDate1 = Convert.ToDateTime(ATT.EndDate).ToString("yyyy/MM/dd");
          
            DateTime StartDate = Convert.ToDateTime(EndDate1);
            DateTime EndDate  = Convert.ToDateTime(StartDate1);
            string AA = "";
            int count = 0;
           var getmachines = db.tblAttendenceMachineMasters.Where(ma => ma.SchoolID == SchoolID && ma.IsActice == true).Select(y=>y.MachineNo).ToList();
            if (getmachines!=null)
            {
                var MachineRawPunch = db.Tran_MachineRawPunch.Where(mp => (mp.PunchDatetime >= StartDate ) && ( mp.PunchDatetime <= EndDate ) && getmachines.Contains(mp.MachineNo)).ToList();

                if (MachineRawPunch != null)
                {
                    foreach (var punchs in MachineRawPunch)
                    {
                        var employee = db.tblEmployees.Where(em => em.SchoolID == SchoolID && em.IsDeleted == null && em.BiometricID== punchs.CardNo).FirstOrDefault();
                        if (employee != null)
                        {
                            string punchdate = Convert.ToDateTime(punchs.PunchDatetime).ToString("yyyy/MM/dd");
                            DateTime punchdatee = Convert.ToDateTime(punchdate);
                            var EmployeeAttendence = db.tblEmployeeAttendences.Where(o => o.EmployeeId == employee.Id && o.AttendenceDate == punchdatee).FirstOrDefault();
                            if (EmployeeAttendence ==null)
                            {
                                tblEmployeeAttendence InATT = new tblEmployeeAttendence();
                                InATT.EmployeeId = employee.Id;
                                InATT.DesigId = employee.DesigID;
                                InATT.DepId = employee.DeptID;
                                InATT.AttendenceType = "Present";
                                InATT.AttendenceDate = punchs.PunchDatetime;
                                InATT.DateCreated = DateTime.Now;
                                InATT.IsBiometric = true;
                                InATT.SchoolID = SchoolID;
                                count++;
                                db.tblEmployeeAttendences.Add(InATT);
                                db.SaveChanges();

                           }
                          

                          

                        }
                        //else
                        //{
                        //    //employee Not Found with Card id
                        //}
                    }
                 
                   if (count>0)
                    {
                        return AA = "Attendce loaded";
                    }
                   else
                    {
                        return AA = "NO attendece found to be loaded";
                    }

                }
                else
                {

                    //if machine count is Not 0 and there is NO punch OF ANYONE
                    return AA = "No punch found ";
                }
            }
            else
            {

                //if no machine assinged
                return AA = "No machine assinged to School";
            }                         

        }

        [System.Web.Http.Route("api/EmployeeAPI/empleaveEntry")]
        [System.Web.Http.HttpPost]
        public string empleaveEntry ()
        {

            return "";
        }
        [System.Web.Http.Route("api/EmployeeAPI/saveEmployeeDetails")]
        [System.Web.Http.HttpPost]
        public string saveEmployeeDetails(EmployeeEm employye)
        {
            if (string.IsNullOrEmpty(employye.Id))
            {
                if (employye.BiometricId != "" && employye.BiometricId != null)
                {
                    int SchoolID = Convert.ToInt32(employye.School);
                    var checkbiomatric = db.tblEmployees.Where(d => d.BiometricID == employye.BiometricId && d.SchoolID == SchoolID && d.IsDeleted == null).FirstOrDefault();
                    if (checkbiomatric != null)
                    {
                        return "1";


                    }
                    else
                    {
                        string b = EmployeeDetails.saveEmployeeDetails(employye);
                        if (b != "")
                        {
                            return b;
                        }
                        else
                        {

                            return "";
                        }

                    }

                }
                else
                {
                    string b = EmployeeDetails.saveEmployeeDetails(employye);


                    if (b != "")
                    {
                        return b;
                    }
                    else
                    {

                        return "";
                    }
                }
            }
            else
            {
                if (employye.BiometricId != "" && employye.BiometricId != null)
                {
                    int empID = Convert.ToInt32(employye.Id);
                    int SchoolID = Convert.ToInt32(employye.School);
                    var checkbiomatric = db.tblEmployees.Where(d => d.BiometricID == employye.BiometricId && d.SchoolID == SchoolID && d.IsDeleted == null && d.Id != empID).FirstOrDefault();
                    if (checkbiomatric != null)
                    {

                        return "1";


                    }
                    else
                    {
                        string b = EmployeeDetails.saveEmployeeDetails(employye);
                        if (b != "")
                        {
                            return b;
                        }
                        else
                        {

                            return "";
                        }

                    }

                }
                else
                {
                    string b = EmployeeDetails.saveEmployeeDetails(employye);


                    if (b != "")
                    {
                        return b;
                    }
                    else
                    {

                        return "";
                    }
                }
            }
           
            



        }
        [System.Web.Http.Route("api/EmployeeAPI/SchoolAdminChangePassword")]
        [System.Web.Http.HttpPost]
        public string SchoolAdminChangePassword(Changepassword pass)
        {

            string b = EmployeeDetails.SchoolAdminChangePassword(pass);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllStaffbySchool")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllStaffbySchool(List<string> AB)
        {
            int IDD = Convert.ToInt32(AB);
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = "select * from tblStaffCategory where Status=1 and id='" + IDD + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["Id"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/EmployeeUploadFile")]
        [System.Web.Http.HttpPost]
        public string UploadFile()
        {

            int jk = 0;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {

                string EmployeeID = HttpContext.Current.Request.Params["Id"];
                string EmployeeCode = HttpContext.Current.Request.Params["EmployeeCode"];
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                string ImageExtensions = "";
                string ImageExtensions1 = "";
                string ImageFileName = "";
                string ImageFileName1 = "";
                string ImageFile = "";
                string ImageFile1 = "";
                string FullImagePath = "/Images/Employee/EmployyeImage/";
                string FullImagePath2 = "/Images/Employee/Documents/";
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
                string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
                if (httpPostedFile != null)
                {




                    // Validate the uploaded image(optional) 

                    // Get the complete file path
                    ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                    if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" || ImageExtensions.ToLower() == ".gif" || ImageExtensions.ToLower() == ".pdf")
                    {
                        ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                        System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                        ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                        ImageFileName = ImageFile;
                        ImageFile = FullImagePath + ImageFile;
                        HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                        sqlHelper obj = new sqlHelper();
                        string[] cols = { "Image" };
                        object[] vals = { ImageFile };
                        obj.updateValIntoTable("tblEmployee", cols, vals, "Id", EmployeeID);
                    }

                }

                for (int i = 0; i <= HttpContext.Current.Request.Files.Count - 1; i++)
                {
                    var imgval = (HttpContext.Current.Request.Files.AllKeys[i].ToString().Split('e'))[2];
                    if (!string.IsNullOrEmpty(imgval))
                    {
                        var httpPostedFile1 = HttpContext.Current.Request.Files["UploadedImage" + imgval + ""];
                        //var httpPostedFilde1 = HttpContext.Current.Request.Files["UploadedImage" + i + ""].FileName;
                        if (httpPostedFile1 != null)
                        {

                            ImageExtensions1 = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage" + imgval + ""].FileName).ToString();
                            if (ImageExtensions1.ToLower() == ".jpg" || ImageExtensions1.ToLower() == ".png" || ImageExtensions1.ToLower() == ".jpeg" || ImageExtensions1.ToLower() == ".gif" || ImageExtensions1.ToLower() == ".pdf")
                            {
                                ImageFile1 = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage" + imgval + ""].FileName);
                                System.IO.FileInfo filename2 = new System.IO.FileInfo(serverPath2 + ImageFile1);

                                ImageFile1 = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile1;

                                ImageFileName1 = ImageFile1;
                                ImageFile1 = FullImagePath2 + ImageFile1;
                                HttpContext.Current.Request.Files["UploadedImage" + imgval + ""].SaveAs(Path.Combine(serverPath2, ImageFileName1));
                                sqlHelper obj = new sqlHelper();
                                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                                SqlConnection con = new SqlConnection(constr);
                                con.Open();
                                string query = "";
                                string docId = HttpContext.Current.Request.Params["UploadedImageId" + imgval + ""];
                                string dociddd = obj.ExecuteScaler("select DocumentId from tblEmployeeDocuments where DocumentId=" + docId + " and EmployeeId=" + EmployeeID);
                                if (dociddd == docId)
                                {
                                    query = "update tblEmployeeDocuments set DocImage='" + ImageFile1 + "' where DocumentId=" + docId + " and EmployeeId=" + EmployeeID;
                                    jk = 1;
                                }
                                else
                                {
                                    query = "Insert into tblEmployeeDocuments(DocumentId,EmployeeId,DocImage) values(" + docId + "," + EmployeeID + ",'" + ImageFile1 + "')";
                                }
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }


            }

            if (jk == 0)
            {
                return "Employee Added Successfully";
            }
            else
            {
                return "Employee Updated Successfully";
            }
        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeDetaulsBYSchool")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeDetaulsBYSchool(List<string> aa)
        {
           
            
            int SchoolID = Convert.ToInt32 ( aa[0]);
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();
            if (SchoolID==0 && SchoolID ==-1 )
            {
                DataTable dt = obj.sp_GetDataTableNoParam("sp_getAllEmployeeDetails");
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Id = dr["Id"].ToString();
                    usr.Employeecode = dr["Empcode"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.UserType = dr["staff"].ToString();
                    usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    // string rahl = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    usr.Gender = dr["GenderName"].ToString();

                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra10 = "btn btn-block btn-success btn-sm";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";

                    }
                    usr.qualfication = dr["QualificationName"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.TypeName = dr["Name"].ToString();

                    list.Add(usr);
                }
            }
            else
            {


                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

                SqlConnection con = new SqlConnection(constr);
                con.Open();

                string query = "select ut.Name,s.School,em.SchoolID,em.Id,Empcode,td.DepartmentName,desg.Designation,sc.Name staff,qc.QualificationName,FirstName,MiddleName,LastName,Cast(DOB AS DATE) DOB,	gen.GenderName,em.Image,em.Status, em.Mobile,em.Email from tblEmployee em left outer join tblDepartmnet td on td.DepartmentId = em.DeptID left outer join tblDesignation desg on desg.DesigID = em.DesigID left outer join tblStaffCategory sc on sc.Id = em.StaffCategory left outer join tblQualifications qc on qc.QualificationId = em.Qualification left outer join tblGender gen on gen.gender_id = em.GenderID left outer join tblSchoolDetails s on em.SchoolID = s.ID left outer join tblusertype ut on ut.id = em.UserType where em.IsDeleted is null and em.SchoolID = '"+SchoolID+ "' order by em.id desc  ";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Id = dr["Id"].ToString();
                    usr.Employeecode = dr["Empcode"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.UserType = dr["staff"].ToString();
                    usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    // string rahl = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    usr.Gender = dr["GenderName"].ToString();

                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra10 = "btn btn-block btn-success btn-sm";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";

                    }
                    usr.qualfication = dr["QualificationName"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.TypeName = dr["Name"].ToString();

                    list.Add(usr);
                }

            }

            
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeDetauls")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeDetauls()
        {
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();


            DataTable dt = obj.sp_GetDataTableNoParam("sp_getAllEmployeeDetails");
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                // string rahl = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                usr.Gender = dr["GenderName"].ToString();

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "True")
                {

                    usr.Status = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.Status = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.qualfication = dr["QualificationName"].ToString();
                usr.School = dr["School"].ToString();
                usr.TypeName = dr["Name"].ToString();

                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeDocuemnts")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getEmployeeDocuemnts(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = @"select DocImage,d.DocumentName from tblEmployeeDocuments ed
                                        inner join tblDocument d on d.Id = ed.DocumentId where d.userId = 3
                                        and ed.EmployeeId =" + id[0];

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["DocumentName"].ToString();
                usr.Id = dr["DocImage"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/EmployeStatusUpdate")]
        [System.Web.Http.HttpPost]
        public string EmployeStatusUpdate(EmployeeEm employye)
        {

            string b = EmployeeDetails.EmployeStatusUpdate(employye);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }


        [System.Web.Http.Route("api/EmployeeAPI/editEmployeeDetailsById")]
        [System.Web.Http.HttpPost]
        public EmployeeEm editEmployeeDetailsById(List<string> id)
        {
            List<EmployeeEm> list = new List<EmployeeEm>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select * from tblEmployee where Id=" + id[0]);


            EmployeeEm usr = new EmployeeEm();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.JoiningDate = ((DateTime)dr["JoiningDt"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Department = dr["DeptID"].ToString();
                usr.Designation = dr["DesigID"].ToString();
                usr.qualfication = dr["Qualification"].ToString();
                usr.TotalExperience = dr["Experience"].ToString();
                usr.UserType = dr["StaffCategory"].ToString();
                usr.FName = dr["FirstName"].ToString();
                usr.MName = dr["MiddleName"].ToString();
                usr.LName = dr["LastName"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  // dr["DOB"].ToString();
                usr.Gender = dr["GenderID"].ToString();
                usr.MaritalStaus = dr["MaritalSatus"].ToString();
                usr.Religon = dr["Religion"].ToString();
                usr.MotherName = dr["MotherName"].ToString();
                usr.FatherName = dr["FatherName"].ToString();
                usr.Country = dr["Country"].ToString();
                usr.State = dr["State"].ToString();
                usr.city = dr["City"].ToString();
                usr.PermanentAddress = dr["PermAddress"].ToString();
                usr.PresentAddress = dr["PresentAddress"].ToString();
                usr.Pin = dr["Pincode"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();

                //usr.AdhaarNo = dr["CountryID"].ToString();

                //usr.u = dr["CountryID"].ToString();
                usr.AdhaarNo = dr["AdharNo"].ToString();
                usr.UserName = dr["UserID"].ToString();
                usr.Password = dr["Pwd"].ToString();

                usr.ImageUpload = dr["Image"].ToString();

                usr.PaymentMode = dr["PaymentMod"].ToString();
                usr.AccountNumber = dr["AccountNumber"].ToString();
                usr.IfscCode = dr["IFSCCode"].ToString();
                usr.PfNo = dr["PFNo"].ToString();
                usr.PANNo = dr["PANNO"].ToString();
                usr.EsiNo = dr["ESINO"].ToString();
                usr.BankName = dr["BankName"].ToString();
                usr.School = dr["SchoolID"].ToString();
                usr.UserType = dr["UserType"].ToString();
                usr.staffCategory = dr["StaffCategory"].ToString();
                usr.BiometricId = dr["BiometricID"].ToString();
              
            }

            return usr;






        }


        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeCode")]
        [System.Web.Http.HttpPost]
        public CodeGenMaster getEmployeeCode()
        {
            sqlHelper obj = new sqlHelper();
            SqlDataReader dr = obj.GetReader("select * from tblDocumentNo where Status=1 and UserType='Employee'");
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
            }

            return usr;



        }


        [System.Web.Http.Route("api/EmployeeAPI/ViewEmployeeDetailsById")]
        [System.Web.Http.HttpPost]
        public EmployeeEm ViewEmployeeDetailsById(List<string> id)
        {
            List<EmployeeEm> list = new List<EmployeeEm>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select ut.Name,d.DepartmentName,desg.Designation,qualf.QualificationName,sc.Name staff
                                            , g.GenderName, cit.CityName, st.StateName, c.CountryName, ms.Name Marital,
                                            rg.ReligionName, em.* from tblEmployee em
                                             left outer join tblDepartmnet d on d.DepartmentId = em.DeptID
                                            left outer join tblDesignation desg on desg.DesigID = em.DesigID
                                            left outer join tblQualifications qualf on qualf.QualificationId = em.Qualification
                                            left outer join tblStaffCategory sc on sc.Id = em.StaffCategory
                                            left outer join tblGender g on g.gender_id = em.GenderID
                                            left outer join tblCity cit   on cit.Id = em.City
                                            left outer join tblState st   on st.StateId = em.State
                                            left outer join tblCountry c   on c.CountryID = em.Country
                                            left outer join tblMaritalStatus ms   on ms.Id = em.MaritalSatus
                                            left outer join tblReligion rg   on rg.ReligionId = em.Religion
											left outer join tblUserType ut on ut.id=em.UserType
                                            where em.Id = '"+ id[0] + "' and em.IsDeleted is null ");



            EmployeeEm usr = new EmployeeEm();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.JoiningDate = ((DateTime)dr["JoiningDt"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.qualfication = dr["QualificationName"].ToString();
                usr.TotalExperience = dr["Experience"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.FName = dr["FirstName"].ToString();
                usr.MName = dr["MiddleName"].ToString();
                usr.LName = dr["LastName"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  // dr["DOB"].ToString();
                usr.Gender = dr["GenderName"].ToString();
                usr.MaritalStaus = dr["Marital"].ToString();
                usr.Religon = dr["ReligionName"].ToString();
                usr.MotherName = dr["MotherName"].ToString();
                usr.FatherName = dr["FatherName"].ToString();
                usr.Country = dr["CountryName"].ToString();
                usr.State = dr["StateName"].ToString();
                usr.city = dr["CityName"].ToString();
                usr.PermanentAddress = dr["PermAddress"].ToString();
                usr.PresentAddress = dr["PresentAddress"].ToString();
                usr.Pin = dr["Pincode"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();
                usr.DesigId = dr["DesigID"].ToString();
                //usr.AdhaarNo = dr["CountryID"].ToString();

                //usr.u = dr["CountryID"].ToString();
                usr.AdhaarNo = dr["AdharNo"].ToString();
                usr.UserName = dr["UserID"].ToString();
                usr.Password = dr["Pwd"].ToString();

                usr.ImageUpload = dr["Image"].ToString();


                usr.PaymentMode = dr["PaymentMod"].ToString();
                usr.AccountNumber = dr["AccountNumber"].ToString();
                usr.IfscCode = dr["IFSCCode"].ToString();
                usr.PfNo = dr["PFNo"].ToString();
                usr.PANNo = dr["PANNO"].ToString();
                usr.EsiNo = dr["ESINO"].ToString();


                usr.BankName = dr["BankName"].ToString();
                if (dr["Status"].ToString() == "True")
                {
                    usr.Status = "Activate";
                }
                else
                {
                    usr.Status = "De-Activate";
                }
                usr.TypeName = dr["Name"].ToString();
            }

            return usr;






        }


        [System.Web.Http.Route("api/EmployeeAPI/ViewSchoolDetailsById")]
        [System.Web.Http.HttpPost]
        public EmployeeEm ViewSchoolDetailsById(List<string> id)
        {
            List<EmployeeEm> list = new List<EmployeeEm>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"	select cit.CityName, st.StateName, c.CountryName, em.* from tblSchoolDetails em
                                            left outer join tblCity cit   on cit.Id = em.CityID
                                            left outer join tblState st   on st.StateId = em.State
                                            left outer join tblCountry c   on c.CountryID = em.CountryID                                           
                                            where em.Id = " + id[0]);



            EmployeeEm usr = new EmployeeEm();
            if (dr.Read())
            {
                usr.Id = dr["ID"].ToString();
                usr.Employeecode = dr["SchoolCode"].ToString();
                usr.FName = dr["School"].ToString();
                usr.Country = dr["CountryName"].ToString();
                usr.State = dr["StateName"].ToString();
                usr.city = dr["CityName"].ToString();
                usr.PermanentAddress = dr["Address"].ToString();
                usr.Pin = dr["Pincode"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();    
                usr.UserName = dr["SchoolCode"].ToString();
                usr.Password = dr["Password"].ToString();
                usr.Religon = dr["Fax"].ToString();
                usr.ImageUpload = dr["LogoPic"].ToString();


               


            }

            return usr;






        }

        [System.Web.Http.Route("api/EmployeeAPI/fetchIdentityCardDetailsByEmpId")]
        [System.Web.Http.HttpPost]
        public EmployeeEm fetchIdentityCardDetailsByEmpId(List<string> id)
        {
            List<EmployeeEm> list = new List<EmployeeEm>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select d.DepartmentName,desg.Designation,qualf.QualificationName,sc.Name staff
                                            , g.GenderName, cit.CityName, st.StateName, c.CountryName, ms.Name Marital,
                                            rg.ReligionName, em.* from tblEmployee em
                                             left outer join tblDepartmnet d on d.DepartmentId = em.DeptID
                                            left outer join tblDesignation desg on desg.DesigID = em.DesigID
                                            left outer join tblQualifications qualf on qualf.QualificationId = em.Qualification
                                            left outer join tblStaffCategory sc on sc.Id = em.StaffCategory
                                            left outer join tblGender g on g.gender_id = em.GenderID
                                            left outer join tblCity cit   on cit.Id = em.City
                                            left outer join tblState st   on st.StateId = em.State
                                            left outer join tblCountry c   on c.CountryID = em.Country
                                            left outer join tblMaritalStatus ms   on ms.Id = em.MaritalSatus
                                            left outer join tblReligion rg   on rg.ReligionId = em.Religion
                                            where em.Id = " + id[0]);


            EmployeeEm usr = new EmployeeEm();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.JoiningDate = ((DateTime)dr["JoiningDt"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.qualfication = dr["QualificationName"].ToString();
                usr.TotalExperience = dr["Experience"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.FName = dr["FirstName"].ToString();
                usr.MName = dr["MiddleName"].ToString();
                usr.LName = dr["LastName"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  // dr["DOB"].ToString();
                usr.Gender = dr["GenderName"].ToString();
                usr.MaritalStaus = dr["Marital"].ToString();
                usr.Religon = dr["ReligionName"].ToString();
                usr.MotherName = dr["MotherName"].ToString();
                usr.FatherName = dr["FatherName"].ToString();
                usr.Country = dr["CountryName"].ToString();
                usr.State = dr["StateName"].ToString();
                usr.city = dr["CityName"].ToString();
                usr.PermanentAddress = dr["PermAddress"].ToString();
                usr.PresentAddress = dr["PresentAddress"].ToString();
                usr.Pin = dr["Pincode"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();

                // usr.ImageUpload = dr["Image"].ToString();

                //usr.u = dr["CountryID"].ToString();
                usr.AdhaarNo = dr["AdharNo"].ToString();
                usr.UserName = dr["UserID"].ToString();
                usr.Password = dr["Pwd"].ToString();

                usr.ImageUpload = dr["Image"].ToString();
                if (dr["Status"].ToString() == "True")
                {
                    usr.Status = "Activate";
                }
                else
                {
                    usr.Status = "De-Activate";
                }
            }

            return usr;






        }


        [System.Web.Http.Route("api/EmployeeAPI/fetchSchoolDetails")]
        [System.Web.Http.HttpPost]
        public SchoolDetails fetchSchoolDetails()
        {
            List<SchoolDetails> list = new List<SchoolDetails>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select ID,School,SchoolCode,Address,State,City,District,Board,Pincode,Phone,Fax,Mobile,Email,Website,LogoPic,PrincipalID from  tblSchoolDetails");


            SchoolDetails usr = new SchoolDetails();
            if (dr.Read())
            {
                //usr.ID = Int64.Parse(dr["Id"].ToString());
                usr.LogoPic = dr["LogoPic"].ToString();
                usr.School = dr["School"].ToString();
                //usr.JoiningDate = ((DateTime)dr["JoiningDt"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //usr.Department = dr["DepartmentName"].ToString();
                usr.Address = dr["Address"].ToString();
                usr.State = dr["State"].ToString();
                usr.City = dr["City"].ToString();
                usr.District = dr["District"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Pincode = dr["Pincode"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                //usr.DOB = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  // dr["DOB"].ToString();
                usr.PrincipalID = int.Parse(dr["PrincipalID"].ToString());

            }

            return usr;

        }




        [System.Web.Http.Route("api/EmployeeAPI/getPrincipleSignature")]
        [System.Web.Http.HttpPost]
        public SchoolDetails getPrincipleSignature(List<string> id)
        {
            List<SchoolDetails> list = new List<SchoolDetails>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@" select * from tblUser where ID=" + id[0]);


            SchoolDetails usr = new SchoolDetails();
            if (dr.Read())
            {

                usr.SchoolCode = dr["Signature"].ToString();

            }

            return usr;

        }


        [System.Web.Http.Route("api/EmployeeAPI/searchEmployeeForSms")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeForSms(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();

            string[] cols = { "@empcode", "@empName", "@department", "@designation", "@staff", "@School" };
            object[] vals = { employye.Employeecode, employye.FName, employye.Department, employye.Designation, employye.UserType, employye.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeForSms", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.School = dr["School"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.Email = dr["Email"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                usr.Email = dr["Email"].ToString();
                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "True")
                {

                    usr.Status = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.Status = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.qualfication = dr["QualificationName"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/sendSmsForEmployee")]
        [System.Web.Http.HttpPost]
        public string sendSmsForEmployee(EmployeeEm[] employye)
        {
            sqlHelper obj = new sqlHelper();
            int smsservicecount = 0;
            foreach (var emp in employye)
            {
                if (emp.Employeecode != "")
                {
                    string phoneno = obj.ExecuteScaler("select Mobile from tblEmployee where Id='" + emp.Employeecode + "' and SchoolID='" + emp.SchoolID + "'");
                    var SchoolName = db.tblSchoolDetails.Where(sc => sc.ID == emp.SchoolID).FirstOrDefault().School;
                    string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                    if (Checksmsservice != null)
                    {
                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");

                        string authKey = GetauthKey;
                        //Multiple mobiles numbers separated by comma
                        string mobileNumber = phoneno;
                        //Sender ID,While using route4 sender id should be 6 characters long.
                        string senderId = GetsenderId;
                        //Your message to send, Add URL encoding here.
                        StringBuilder st = new StringBuilder();
                        st.AppendLine("Hi Sir/mam,");
                        st.AppendLine(emp.Department);
                        st.AppendLine("");
                        st.AppendLine("Regards");
                        st.AppendLine(SchoolName);
                        string message = HttpUtility.UrlEncode(st.ToString());

                        //Prepare you post parameters
                        StringBuilder sbPostData = new StringBuilder();
                        sbPostData.AppendFormat("authkey={0}", authKey);
                        sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                        sbPostData.AppendFormat("&message={0}", message);
                        sbPostData.AppendFormat("&sender={0}", senderId);
                        sbPostData.AppendFormat("&route={0}", 4);
                        sbPostData.AppendFormat("&unicode={0}", 1);

                        try
                        {
                            //Call Send SMS API
                            string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                            //Create HTTPWebrequest
                            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                            //Prepare and Add URL Encoded data
                            UTF8Encoding encoding = new UTF8Encoding();
                            byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                            //Specify post method
                            httpWReq.Method = "POST";
                            httpWReq.ContentType = "application/x-www-form-urlencoded";
                            httpWReq.ContentLength = data1.Length;
                            using (Stream stream = httpWReq.GetRequestStream())
                            {
                                stream.Write(data1, 0, data1.Length);
                            }
                            //Get the response
                            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response1.GetResponseStream());
                            string responseString = reader.ReadToEnd();

                            //Close the response
                            reader.Close();
                            response1.Close();
                        }
                        catch (System.Exception ex)
                        {
                            ex.Message.ToString();
                        }

                    }
                    else
                    {
                        smsservicecount++;
                    }







                }
            }
            if(smsservicecount==0)
            {
              
                return "Sms Sent Successfully";
            }
            else
            {
                  return "Sms service not available";

            }
           
        }


        [System.Web.Http.Route("api/EmployeeAPI/checkOldPwdOfEmployee")]
        [System.Web.Http.HttpPost]
        public string checkOldPwdOfEmployee(ChangePwd check)
            {
            sqlHelper obj = new sqlHelper();

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(check.OldPwd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string oldpassword = strBuilderPPWD.ToString();


            string Pwd = obj.ExecuteScaler("select pwd  from tblEmployee where pwd='" + oldpassword + "'  and UserID='" + check.USerID + "' and SchoolID='"+ check .SchoolID+ "'  and  Id=" + check.Id);
            if (!string.IsNullOrEmpty(Pwd))
            {
                return "";
            }
            else
            {
                return "Please Enter Correct Pwd";
            }

        }


        [System.Web.Http.Route("api/EmployeeAPI/changeEmployeePWd")]
        [System.Web.Http.HttpPost]
        public string changeEmployeePWd(ChangePwd check)
        {
            sqlHelper obj = new sqlHelper();

            string Pwd = EmployeeDetails.changePwdOfEmployee(check);
            if (!string.IsNullOrEmpty(Pwd))
            {

                return Pwd;
            }
            else
            {
                return "Error Occured";
            }

        }
        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeLeavesByIddd")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getEmployeeLeavesByIddd(List<string> id)
        {
            
            sqlHelper obj = new sqlHelper();
            List<leaveDetails> list = new List<leaveDetails>();


            DataTable dt = obj.getDataTable(@"select lt.LeaveName, d.Designation, ld.* from tblLeaveDetails ld
                                               left outer join tblLeaveType lt on lt.LeaveId = ld.LeaveCategory
                                               left outer join tblDesignation d on d.DesigID = ld.leaveDesgination
                                               where ld.Status = 1 and ld.leaveDesgination='" + id[0] + "' and ld.SchoolID='"+id[1]+"' and ld.IsDeleted is null ");

            foreach (DataRow dr in dt.Rows)
            {
                leaveDetails usr = new leaveDetails();
                usr.leaveCategory = dr["LeaveName"].ToString();
                usr.Desig = dr["Designation"].ToString();
                usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.leaveAssign = dr["leaveAssgin"].ToString();
                list.Add(usr);
            }

            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeLeavesByIdd")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getEmployeeLeavesByIdd(List<string> id)
        {
           
            sqlHelper obj = new sqlHelper();
            List<leaveDetails> list = new List<leaveDetails>();

           
                DataTable dt = obj.getDataTable(@"select lt.LeaveName, d.Designation, ld.* from tblLeaveDetails ld
                                               left outer join tblLeaveType lt on lt.LeaveId = ld.LeaveCategory
                                               left outer join tblDesignation d on d.DesigID = ld.leaveDesgination
                                               where ld.Status = 1 and ld.leaveDesgination='" + id[0] + "' and ld.IsDeleted is null and ld.SchoolID='"+id[1]+"' ");

                foreach (DataRow dr in dt.Rows)
                {
                    leaveDetails usr = new leaveDetails();
                    usr.leaveCategory = dr["LeaveName"].ToString();
                    usr.Desig = dr["Designation"].ToString();
                    usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.leaveAssign = dr["leaveAssgin"].ToString();
                    list.Add(usr);
                }
            
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeLeavesById")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getEmployeeLeavesById(List<string> id)
        {
            string loginuser = id[1];
            int typeuser = Convert.ToInt32( id[2]);
            sqlHelper obj = new sqlHelper();
            List<leaveDetails> list = new List<leaveDetails>();
         
            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select lt.LeaveName, d.Designation, ld.* from tblLeaveDetails ld
                                               left outer join tblLeaveType lt on lt.LeaveId = ld.LeaveCategory
                                               left outer join tblDesignation d on d.DesigID = ld.leaveDesgination
                                               where ld.Status = 1 and ld.leaveDesgination='" + id[0] + "' and ld.IsDeleted is null ");

                foreach (DataRow dr in dt.Rows)
                {
                    leaveDetails usr = new leaveDetails();
                    usr.leaveCategory = dr["LeaveName"].ToString();
                    usr.Desig = dr["Designation"].ToString();
                    usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.leaveAssign = dr["leaveAssgin"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select lt.LeaveName, d.Designation, ld.* from tblLeaveDetails ld
                                               left outer join tblLeaveType lt on lt.LeaveId = ld.LeaveCategory
                                             left outer join tblDesignation d on d.DesigID = ld.leaveDesgination
											 left outer join tblEmployee em on em.SchoolID=ld.SchoolID
                                             where ld.Status = 1 and em.UserID='" + loginuser + "' and em.IsDeleted is null and ld.leaveDesgination='" + id[0] + "' and ld.IsDeleted is null ");

                foreach (DataRow dr in dt.Rows)
                {
                    leaveDetails usr = new leaveDetails();
                    usr.leaveCategory = dr["LeaveName"].ToString();
                    usr.Desig = dr["Designation"].ToString();
                    usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.leaveAssign = dr["leaveAssgin"].ToString();
                    list.Add(usr);
                }
            }
            
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeLeaveRequest")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeLeaveRequest(List<string> id)
        {
            sqlHelper obj = new sqlHelper();
            string loginuser = id[0];
            int typeuser =Convert .ToInt32 ( id[1]);
            List<EmployeeEm> list = new List<EmployeeEm>();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select lr.SchoolID,sc.School,e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,lt.LeaveName,s.Status,e.Empcode ,lr.* from tblemployeeLeaveRequest lr
                                        left outer join tblEmployee e on e.Id=lr.EmployeeID
                                        left outer join tblDepartmnet d on d.DepartmentId=lr.Department
                                        left outer join tblDesignation dd on dd.DesigID=lr.Designation
                                        left outer join tblLeaveType lt on lt.LeaveId=lr.LeaveType
                                        left outer join tblStatus s on s.StatusID=lr.LeavStatus 
                                        left outer join tblSchoolDetails sc on sc.ID= lr.SchoolID where lr.IsDeleted is null order by lr.DateCreated desc ");

                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Extra1 = dr["LeaveName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "Pending")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-warning btn-sm";
                    }
                    else if (dr["Status"].ToString() == "Approved")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";
                    }

                    usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Extra3 = dr["TakeLeaveCount"].ToString();
                    usr.Extra4 = dr["LeaveName"].ToString();

                    usr.Employeecode = dr["Empcode"].ToString();

                    usr.Id = dr["Id"].ToString();
                    usr.Month = dr["Reason"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select lr.SchoolID,sc.School,e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,lt.LeaveName,s.Status,e.Empcode ,lr.* from tblemployeeLeaveRequest lr
                                        left outer join tblEmployee e on e.Id=lr.EmployeeID
                                        left outer join tblDepartmnet d on d.DepartmentId=lr.Department
                                        left outer join tblDesignation dd on dd.DesigID=lr.Designation
                                        left outer join tblLeaveType lt on lt.LeaveId=lr.LeaveType
                                        left outer join tblStatus s on s.StatusID=lr.LeavStatus 
                                        left outer join tblSchoolDetails sc on sc.ID= lr.SchoolID 
										left outer join tblEmployee em on em.SchoolID=lr.SchoolID
										where em.UserID='" + loginuser + "' and em.IsDeleted is null and lr.IsDeleted is null  order by lr.DateCreated desc");

                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Extra1 = dr["LeaveName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "Pending")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-warning btn-sm";
                    }
                    else if (dr["Status"].ToString() == "Approved")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";
                    }

                    usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Extra3 = dr["TakeLeaveCount"].ToString();
                    usr.Extra4 = dr["LeaveName"].ToString();

                    usr.Employeecode = dr["Empcode"].ToString();

                    usr.Id = dr["Id"].ToString();
                    usr.Month = dr["Reason"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/leaveStatus")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] leaveStaus()
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblstatus where stStatus=1 and LeaveStatus=1 and IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["StatusID"].ToString();
                usr.Name = dr["Status"].ToString();
               // usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            return list.ToArray();


        }


        //load Issue in Employee Profile


        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeLeaveRequestById")]
       [System.Web.Http.HttpPost]
        public EmployeeEm[] getEmployeeLeaveRequestById(Parameters param)
        {
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,lt.LeaveName,s.Status ,lr.* from tblemployeeLeaveRequest lr
                                                left outer join tblEmployee e on e.Id=lr.EmployeeID
                                                left outer join tblDepartmnet d on d.DepartmentId=lr.Department
                                                left outer join tblDesignation dd on dd.DesigID=lr.Designation
                                                left outer join tblLeaveType lt on lt.LeaveId=lr.LeaveType
                                                left outer join tblStatus s on s.StatusID=lr.LeavStatus
        			   where lr.EmployeeID=" + param.val + " and LR.SchoolID='" + param.val1 + "' and lr.IsDeleted is null order by lr.DateCreated desc");

            List<EmployeeEm> list = new List<EmployeeEm>();
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Extra1 = dr["LeaveName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "Pending")
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-warning btn-sm";

                }
                else if (dr["Status"].ToString() == "Approved")
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }

                usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Extra3 = dr["TakeLeaveCount"].ToString();
                usr.Extra4 = dr["LeaveName"].ToString();

                usr.Employeecode = dr["EmployeeID"].ToString();

                usr.Id = dr["Id"].ToString();

                list.Add(usr);
            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/searchEmployeeForLeaveRequest")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeForLeaveRequest(EmployeeEm employye)
        {
            // string enddate = ((DateTime)Convert.ToDateTime(employye.Extra6)).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime startDate = DateTime.ParseExact(employye.Extra5, "MM/dd/yyyy", CultureInfo.InvariantCulture);   
            DateTime endDate = DateTime.ParseExact(employye.Extra6, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //   DateTime startDate= DateTime.ParseExact(employye.Extra6, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //   DateTime enddate = DateTime.ParseExact(employye.Extra6, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();

            string[] cols = { "@empcode", "@empName", "@startdate", "@enddate", "@status", "SchoolID" };
            object[] vals = { employye.Employeecode, employye.FName, startDate, endDate, employye.UserType, employye.SchoolID };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeForLeaveRequest", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Extra1 = dr["LeaveName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "Pending")
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-warning btn-sm";

                }
                else if (dr["Status"].ToString() == "Approved")
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {

                    usr.Status = dr["Status"].ToString();
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }





                usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Extra3 = dr["TakeLeaveCount"].ToString();
                usr.Extra4 = dr["LeaveName"].ToString();

                usr.Employeecode = dr["EmployeeID"].ToString();
                usr.Month = dr["Reason"].ToString();
                usr.Id = dr["Id"].ToString();
                usr.School = dr["School"].ToString();
                usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                list.Add(usr);
            }
            return list.ToArray();
        }





        [System.Web.Http.Route("api/EmployeeAPI/employeeSalarySlips")]
        [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails[] employeeSalarySlips(List<string> id)
        {
      
            sqlHelper obj = new sqlHelper();
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();
 
            DataTable dt = obj.getDataTable(@"select e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,s.* from tblEmployeeSalaryDetails s
left outer join tblEmployee e on e.Id=s.EmployeeId
left outer join tblDesignation dd on dd.DesigID=s.DesignationId
left outer join tblDepartmnet d on d.DepartmentId=e.DeptID
                                            where s.EmployeeId='" + id[0] + "' and e.IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
                usr.FromDate = ((DateTime)dr["FromDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.ToDate = ((DateTime)dr["toDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.Designation = dr["Designation"].ToString();
                usr.NoOfLeave = dr["NoOfLeave"].ToString();
                usr.NetSalary = dr["NetSalary"].ToString();
                usr.EmployeeName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();

                usr.MonthlyGross = dr["MonthlyGross"].ToString();

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }


                //usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //usr.Extra3 = dr["TakeLeaveCount"].ToString();
                //usr.Extra4 = dr["LeaveName"].ToString();

                //usr.Employeecode = dr["EmployeeID"].ToString();

                usr.Id = dr["Id"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/searchEmployeeForAttendence1")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeForAttendence1(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();

            string[] cols = { "@empcode", "@empName", "@department", "@designation", "@staff", "@School" };
            object[] vals = { employye.Employeecode, employye.FName, employye.Department, employye.Designation, employye.UserType, employye.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeForAttendence", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.School = dr["School"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "True")
                {

                    usr.Status = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.Status = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.departmentID = dr["DeptID"].ToString();
                usr.DesigId = dr["DesigID"].ToString();
                usr.qualfication = dr["QualificationName"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/searchEmployeeForAttendence")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeForAttendence(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();

            string[] cols = { "@empcode", "@empName", "@department", "@designation", "@staff", "@School" };
            object[] vals = { employye.Employeecode, employye.FName, employye.Department, employye.Designation, employye.UserType, employye.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeForAttendence", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.School = dr["School"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "True")
                {

                    usr.Status = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.Status = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.departmentID = dr["DeptID"].ToString();
                usr.DesigId = dr["DesigID"].ToString();
                usr.qualfication = dr["QualificationName"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/EmployeeAPI/getEmployeeLeavesByDate")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getEmployeeLeavesByDate(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<leaveDetails> list = new List<leaveDetails>();

        

           DataTable dt = obj.getDataTable(@"select d.Designation,dp.DepartmentName,lt.LeaveName,s.Status,er.* from tblemployeeLeaveRequest er
                                                left outer join tblDesignation d on d.DesigID = er.Designation
                                                left outer join tblDepartmnet dp on dp.DepartmentId = er.Department
                                                left outer join tblLeaveType lt on lt.LeaveId = er.LeaveType
                                                left outer join tblStatus s on s.StatusID = er.LeavStatus
                                                 where er.SchoolID='"+employye.SchoolID+"' and er.StatDate <= convert(date, '" + employye.Employeecode + "', 101) and er.EndDate >= convert(date, '" + employye.Employeecode+"', 101) and s.Status = 'Approved'");
            foreach (DataRow dr in dt.Rows)
            {
                leaveDetails usr = new leaveDetails();
                usr.Id = dr["EmployeeID"].ToString();
                usr.DesigId = dr["Designation"].ToString();
                usr.Desig = dr["Designation"].ToString();
                usr.leaveCategory = dr["LeaveName"].ToString();

                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeDetaulsforAttendence")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeDetaulsforAttendence(List<string > employye)
        {
            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();
            string[] cols = { "@School" };
           object[] vals = { employye[0] };

            DataTable dt = obj.sp_GetDataTable("sp_getAllEmployeeDetailsForAttendence",cols,vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.Id = dr["Id"].ToString();
                usr.Employeecode = dr["Empcode"].ToString();
                usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.Department = dr["DepartmentName"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.UserType = dr["staff"].ToString();
                usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                // string rahl = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                usr.Gender = dr["GenderName"].ToString();

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                if (dr["Status"].ToString() == "True")
                {

                    usr.Status = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.Status = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.qualfication = dr["QualificationName"].ToString();
                usr.departmentID = dr["DeptID"].ToString();
                usr.DesigId = dr["DesigID"].ToString();
                usr.School = dr["School"].ToString();
                
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/TakeEmployeeAttendence")]
        [System.Web.Http.HttpPost]
        public string TakeEmployeeAttendence(takeAttedencDetails[] attendence)
        {
         
            string save = EmployeeAttendenceDetailss.saveEmployeeAttendencee(attendence);
            return save;
        }
        public class EmployeeAttendenceDetailss
        {
            public static string saveEmployeeAttendencee(takeAttedencDetails[] attendence)
            {
                string AttendenceTime = DateTime.Now.ToString("HH: mm:ss tt");
                sqlHelper obj = new sqlHelper();
                foreach (var emp in attendence)
                {
                    string exits = obj.ExecuteScaler("select EmployeeId from tblEmployeeAttendence where SchoolID='" + emp.SchoolID + "' and EmployeeId=" + emp.empID + " and  AttendenceDate='" + emp.DateCurrent + "' ");

                    if (exits == emp.empID)
                    {
                        string exits2 = obj.ExecuteScaler("select EmployeeId from tblEmployeeAttendence where SchoolID='" + emp.SchoolID + "' and EmployeeId=" + emp.empID + " and  AttendenceDate='" + emp.DateCurrent + "' and AttendenceType!='" + emp.AttendenceType + "' ");
                        if (exits2 == emp.empID)
                        {
                            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                            SqlConnection con = new SqlConnection(constr);
                            con.Open();
                            string query = @"update tblEmployeeAttendence set DesigId='" + emp.DesigId + "',DepId='"
                                + emp.DepId + "',LeaveType='" + emp.LeaveType + "',AttendenceType='" + emp.AttendenceType + "',AttendenceDate='" + emp.DateCurrent
                                + "' ,IsBiometric='" + false + "'  where SchoolID='" + emp.SchoolID + "' and EmployeeId=" + emp.empID + " and AttendenceDate='" + emp.DateCurrent + "' ";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //SEND SMS TO EMPLOYEE
                            string SMSmobileNo = obj.ExecuteScaler("SELECT Mobile  from tblEmployee where ID='" +emp.empID+ "'");
                            string Employeename = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from tblEmployee where ID='" + emp.empID + "'");
                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + emp.SchoolID + "'");

                            string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                            if (Checksmsservice != null)
                            {
                                string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                                string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                                string authKey = GetauthKey;
                                //Multiple mobiles numbers separated by comma
                                string mobileNumber = SMSmobileNo;
                                //Sender ID,While using route4 sender id should be 6 characters long.
                                string senderId = GetsenderId;
                                //Your message to send, Add URL encoding here.
                                StringBuilder st = new StringBuilder();
                                st.AppendLine("Hi Sir/mam,");
                                st.AppendLine("Employee Name :" + Employeename);
                                st.AppendLine("Employee Attendence Updated Status :" + emp.AttendenceType);
                                st.AppendLine("Attendence Date :" + emp.DateCurrent);
                                st.AppendLine("Attendence Time :" + AttendenceTime);
                                st.AppendLine("");
                                st.AppendLine("Regards");
                                st.AppendLine(Schoolname);
                                string message = HttpUtility.UrlEncode(st.ToString());

                                //Prepare you post parameters
                                StringBuilder sbPostData = new StringBuilder();
                                sbPostData.AppendFormat("authkey={0}", authKey);
                                sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                sbPostData.AppendFormat("&message={0}", message);
                                sbPostData.AppendFormat("&sender={0}", senderId);
                                sbPostData.AppendFormat("&route={0}", 4);
                                sbPostData.AppendFormat("&unicode={0}", 1);

                                try
                                {
                                    //Call Send SMS API
                                    string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                    //Create HTTPWebrequest
                                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                    //Prepare and Add URL Encoded data
                                    UTF8Encoding encoding = new UTF8Encoding();
                                    byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                    //Specify post method
                                    httpWReq.Method = "POST";
                                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                                    httpWReq.ContentLength = data1.Length;
                                    using (Stream stream = httpWReq.GetRequestStream())
                                    {
                                        stream.Write(data1, 0, data1.Length);
                                    }
                                    //Get the response
                                    HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                    StreamReader reader = new StreamReader(response1.GetResponseStream());
                                    string responseString = reader.ReadToEnd();

                                    //Close the response
                                    reader.Close();
                                    response1.Close();
                                }
                                catch (SystemException ex)
                                {
                                    ex.Message.ToString();
                                }

                            }
                            


                        }

                         





                        //string[] cols1 = { "DesigId", "DepId", "LeaveType", "AttendenceType", "AttendenceDate" };
                        //object[] vals1 = {  emp.DesigId, emp.DesigId, emp.LeaveType, emp.AttendenceType, emp.DateCurrent };
                        //obj.updateValIntoTable("tblEmployeeAttendence", cols1, vals1, "EmployeeId", emp.empID);
                    }
                    else
                    {
                        string[] cols = { "EmployeeId", "DesigId", "DepId", "LeaveType", "AttendenceType", "AttendenceDate", "SchoolID", "IsBiometric" };
                        object[] vals = { emp.empID, emp.DesigId, emp.DepId, emp.LeaveType, emp.AttendenceType, emp.DateCurrent, emp.SchoolID, false };
                        obj.insertValIntoTable("tblEmployeeAttendence", cols, vals);
                        //SEND SMS TO EMPLOYEE
                        string SMSmobileNo = obj.ExecuteScaler("SELECT Mobile  from tblEmployee where ID='" + emp.empID + "'");
                        string Employeename = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from tblEmployee where ID='" + emp.empID + "'");
                        string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + emp.SchoolID + "'");

                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");

                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = SMSmobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Employee Name :" + Employeename);
                            st.AppendLine("Employee Attendence Status :" + emp.AttendenceType);
                            st.AppendLine("Attendence Date :" + emp.DateCurrent);
                            st.AppendLine("Attendence Time :" + AttendenceTime);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);

                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }
                        }

                        

                    }
                }
                return "Attendence Submitted Successfully";
                //string[] desg = leave.Designation;
                //for (int i = 0; i < desg.Length; i++)
                //{
                //    sqlHelper obj = new sqlHelper();
                //    string[] cols = { "LeaveCategory", "leaveDesgination", "leaveAssgin", "StartDate", "EndDate", "Status" };
                //    object[] vals = { leave.leaveCategory, desg[i], leave.leaveAssign, leave.StartDate, leave.EndDate, leave.Status };
                //    if (string.IsNullOrEmpty(leave.Id))
                //    {
                //        string exists = obj.ExecuteScaler(" select leaveCategory from tblLeaveDetails where leaveCategory=" + leave.leaveCategory + " and leaveDesgination=" + desg[i]);
                //        if (exists == leave.leaveCategory)
                //        {
                //            return "Leave " + leave.Name + " already Exists";
                //        }
                //        else
                //        {
                //            obj.insertValIntoTable("tblLeaveDetails", cols, vals);

                //        }
                //    }
                //    else
                //    {
                //        obj.updateValIntoTable("tblLeaveDetails", cols, vals, "Id", leave.Id);

                //    }
                //}
                //if (string.IsNullOrEmpty(leave.Id))
                //{
                //    return "Leave Assign Inserted Successfully";
                //}
                //else
                //{
                //    return "Leave Assign Updated Successfully";
                //}

                //  return "";
            }
        }
        [System.Web.Http.Route("api/EmployeeAPI/getEmployyeAttendenceCurrentDate")]
        [System.Web.Http.HttpPost]
        public takeAttedencDetails[] getEmployyeAttendenceCurrentDate(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<takeAttedencDetails> list = new List<takeAttedencDetails>();



            DataTable dt = obj.getDataTable(@"select * from tblEmployeeAttendence where SchoolID='"+employye.SchoolID+"' and AttendenceDate = convert(date, '" + employye.Employeecode + "', 101) ");
            foreach (DataRow dr in dt.Rows)
            {
                takeAttedencDetails usr = new takeAttedencDetails();
                usr.Id = dr["EmployeeId"].ToString();
                usr.DesigId = dr["DesigId"].ToString();
                usr.DepId = dr["DepId"].ToString();
                usr.AttendenceType = dr["AttendenceType"].ToString();
                usr.LeaveType = dr["LeaveType"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

     


        [System.Web.Http.Route("api/EmployeeAPI/checkStudentAttendeceAllocation")]
        [System.Web.Http.HttpPost]
        public ClassTeacherAllocation checkStudentAttendeceAllocation(List<string> id)
        {
            List<ClassTeacherAllocation> list = new List<ClassTeacherAllocation>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select c.CourseName ClassName,s.SectionName,ca.* from tblClassTeacherAllocation ca
                                                 left outer join tblCourses c on c.Id=ca.ClassID 
                                                inner join   tblSections s on s.Id=ca.SectionID and ca.ClassID=s.ClassId
                                                  where ca.SchoolID='"+id[1]+"' and ca.intEmpID=" + id[0]);
 
            ClassTeacherAllocation usr = new ClassTeacherAllocation();
            if (dr.Read())
            {
                usr.Id =  dr["ID"].ToString();
                usr.SectionId = dr["SectionID"].ToString();
                usr.ClassID = dr["ClassID"].ToString();
                usr.Class = dr["ClassName"].ToString();
                usr.section = dr["SectionName"].ToString();
                
             }

            return usr;
        }
        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeForAttendenceReport")]
        [System.Web.Http.HttpPost]
        public Student[] getAllEmployeeForAttendenceReport(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                //dt = obj.getDataTable(@"select * from tblEmployee where Status=1");
                dt = obj.getDataTable(@"select * from tblEmployee where Status=1 and IsDeleted is null and SchoolID='" + em.SchoolID + "'");
            }

            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["Empcode"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                //usr.RollNo = dr["RollNo"].ToString();
                //usr.emailID = em.Employeecode;
                //usr.Class = dr["ClassID"].ToString();
                //usr.Section = dr["SectionID"].ToString();
                ////  usr.Gender = dr["GenderName"].ToString();

                //if (dr["PicPath"].ToString() == "")
                //{
                //    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                //}
                //else
                //{
                //    usr.PicPath = dr["PicPath"].ToString();
                //}

                DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblEmployeeAttendence where   cast(Month(AttendenceDate) as varchar(50))='" + em.Month + "' and EmployeeId=" + dr["Id"].ToString() + " and  cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    //usr.StudentId = dr["StudentId"].ToString();
                    //usr.ClassID = dr["ClassId"].ToString();

                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    usr1.LeaveType = dr1["LeaveType"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    else if (dr1["AttendenceType"].ToString() == "Absent")
                    {
                        usr1.AttendenceType = "A";
                    }
                    else
                    {
                        usr1.AttendenceType = "L";
                    }
                    usr.attendenceList.Add(usr1);
                }

                list.Add(usr);


            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeForAttendenceReport1")]
        [System.Web.Http.HttpPost]
        public Student[] getAllEmployeeForAttendenceReport1(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                //dt = obj.getDataTable(@"select * from tblEmployee where Status=1");
                dt = obj.getDataTable(@"select * from tblEmployee where Status=1 and IsDeleted is null and SchoolID='"+em.SchoolID+"'");
            }
          
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["Empcode"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
             

                DataTable dt1 = obj.getDataTable(@"select AttendenceDate,cast(Day(AttendenceDate) as varchar(50)) CurrentDay,cast(Month(AttendenceDate) as varchar(50)) CurrentMonth,cast(year(AttendenceDate) as varchar(50) )CurrentYear,* from tblEmployeeAttendence where cast(Month(AttendenceDate) as varchar(50))='" + em.Month + "' and EmployeeId=" + dr["Id"].ToString()+ " and  cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();

                    usr1.AttendenceRealDate =Convert.ToDateTime(dr1["AttendenceDate"]);
                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceMonth= dr1["CurrentMonth"].ToString();
                    usr1.AttendenceYear= dr1["CurrentYear"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    usr1.LeaveType= dr1["LeaveType"].ToString();
                 //   usr1.LeaveStatus = dr1["LeavStatus"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    //else if (dr1["AttendenceType"].ToString() == "Absent" && Convert.ToBoolean(dr1["IsBiometric"]) == false)
                    //{
                       
                    //    usr1.AttendenceType = "A";
                    //}
                    //CONDITIONS OF L H ELSE -
                    else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                    {
                        //Leave 
                   
                        var checkleave = db.tblemployeeLeaveRequests.Where(lv => lv.EmployeeID == usr.ID && lv.StatDate<= usr1.AttendenceRealDate && lv.EndDate>= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.LeavStatus == 5).FirstOrDefault();
                        if (checkleave!=null)
                        {
                            usr1.AttendenceType = "L";

                        }
                        //Holiday
                        else
                        {
                            var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == em.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
                            if (checkHoliday!=null)
                            {
                                usr1.AttendenceType = "H";

                            }
                            else
                            {
                                usr1.AttendenceType = "A";
                            }
                        }



                    }
                  //else
                  //  {
                  //      -
                  //  }
                    usr.attendenceList.Add(usr1);
                }

                list.Add(usr);


            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/EmployeeAPI/SearchEmployeeAttendenceByfilter")]
        [System.Web.Http.HttpPost]
        public Student[] SearchEmployeeAttendenceByfilter(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            string[] cols = { "@empcode", "@empName", "@department", "@designation", "@staff", "@School" };
            object[] vals = { em.Employeecode, em.FName, em.Department, em.Designation, em.UserType, em.School };
            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeAttendenceForFilter", cols, vals);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Student usr = new Student();
                    usr.attendenceList = new List<StudentAttendenceDetails>();
                    usr.ID = int.Parse(dr["Id"].ToString());
                    usr.RegNo = dr["Empcode"].ToString();
                    //usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToString(dr["SchoolID"]);
                    int SchoolIDD = Convert.ToInt32(dr["SchoolID"]);
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    //DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblEmployeeAttendence where   MONTH(AttendenceDate)='" + em.Month + "' and EmployeeId=" + dr["Id"].ToString() + "  and  year(AttendenceDate)='" + em.Year + "'");
                    DataTable dt1 = obj.getDataTable(@"select AttendenceDate, cast(Day(AttendenceDate) as varchar(50)) CurrentDay,cast(Month(AttendenceDate) as varchar(50)) CurrentMonth,cast(year(AttendenceDate) as varchar(50))CurrentYear ,*from tblEmployeeAttendence where  MONTH(AttendenceDate)='" + em.Month + "' and EmployeeId='" + dr["Id"].ToString() + " '  and year(AttendenceDate)='" + em.Year + "' and SchoolID='"+ em.School + "'"); /*group by AttendenceDate,AttendenceType*/
                    //DataTable dt1 = obj.getDataTable(@"select count(attendenceid), cast(Day(AttendenceDate) as varchar(50)) CurrentDay,AttendenceType from tblEmployeeAttendence where  MONTH(AttendenceDate)='" + em.Month + "' and EmployeeId='" + dr["Id"].ToString() + " '  and year(AttendenceDate)='" + em.Year + "' and SchoolID='" + em.School + "' group by AttendenceDate,AttendenceType");

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        //StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                        //usr.StudentId = dr["StudentId"].ToString();
                        //usr.ClassID = dr["ClassId"].ToString();

                        //usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                        //usr1.AttendenceType = dr1["AttendenceType"].ToString();
                        //if (dr1["AttendenceType"].ToString() == "Present")
                        //{
                        //    usr1.AttendenceType = "P";
                        //}
                        //else if (dr1["AttendenceType"].ToString() == "Absent")
                        //{
                        //    usr1.AttendenceType = "A";
                        //}
                        //else
                        //{
                        //    usr1.AttendenceType = "L";
                        //}
                        StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                        //usr.StudentId = dr["StudentId"].ToString();
                        //usr.ClassID = dr["ClassId"].ToString();
                        usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                        usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                        usr1.AttendenceMonth = dr1["CurrentMonth"].ToString();
                        usr1.AttendenceYear = dr1["CurrentYear"].ToString();
                        usr1.AttendenceType = dr1["AttendenceType"].ToString();
                        usr1.LeaveType = dr1["LeaveType"].ToString();


                        if (dr1["AttendenceType"].ToString() == "Present")
                        {
                            usr1.AttendenceType = "P";
                        }
                        //else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == false*/)
                        //{

                        //    usr1.AttendenceType = "A";
                        //}
                        //CONDITIONS OF L H ELSE -
                        else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                        {
                            //Leave 

                            var checkleave = db.tblemployeeLeaveRequests.Where(lv => lv.EmployeeID == usr.ID && lv.StatDate <= usr1.AttendenceRealDate && lv.EndDate >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.LeavStatus == 5).FirstOrDefault();
                            if (checkleave != null)
                            {
                                usr1.AttendenceType = "L";

                            }
                            //Holiday
                            else
                            {
                                var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == SchoolIDD && Hl.IsDeleted == null).FirstOrDefault();
                                if (checkHoliday != null)
                                {
                                    usr1.AttendenceType = "H";

                                }
                                else
                                {
                                    usr1.AttendenceType = "A";
                                }
                            }



                        }
                        usr.attendenceList.Add(usr1);
                    }

                    list.Add(usr);


                }

            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/EmployeeAPI/EmployeeAttendenceReportByEmployeeId1")]
        [System.Web.Http.HttpPost]
        public Student[] EmployeeAttendenceReportByEmployeeId1(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                dt = obj.getDataTable(@"select * from tblEmployee where Status=1 and IsDeleted is null and SchoolID='" + em.SchoolID + "' and Id=" + em.DesigId);
            }

            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["Empcode"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                DataTable dt1 = obj.getDataTable(@"select AttendenceDate,cast(Day(AttendenceDate) as varchar(50)) CurrentDay,cast(Month(AttendenceDate) as varchar(50)) CurrentMonth,cast(year(AttendenceDate) as varchar(50) )CurrentYear,* from tblEmployeeAttendence where   cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and EmployeeId=" + dr["Id"].ToString() + " and SchoolID='" + em.SchoolID + "' and  cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceMonth = dr1["CurrentMonth"].ToString();
                    usr1.AttendenceYear = dr1["CurrentYear"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    usr1.LeaveType = dr1["LeaveType"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    //else if (dr1["AttendenceType"].ToString() == "Absent"/* && Convert.ToBoolean(dr1["IsBiometric"]) == false*/)
                    //{

                    //    usr1.AttendenceType = "A";
                    //}
                    //CONDITIONS OF L H ELSE -
                    else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                    {
                        //Leave 

                        var checkleave = db.tblemployeeLeaveRequests.Where(lv => lv.EmployeeID == usr.ID && lv.StatDate <= usr1.AttendenceRealDate && lv.EndDate >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.LeavStatus == 5).FirstOrDefault();
                        if (checkleave != null)
                        {
                            usr1.AttendenceType = "L";

                        }
                        //Holiday
                        else
                        {
                            var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == em.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
                            if (checkHoliday != null)
                            {
                                usr1.AttendenceType = "H";

                            }
                            else
                            {
                                usr1.AttendenceType = "A";
                            }
                        }



                    }
                    usr.attendenceList.Add(usr1);
                    //else
                    //  {
                    //      -
                    //  }
                }
                list.Add(usr);
               
            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/EmployeeAPI/EmployeeAttendenceReportByEmployeeId")]
        [System.Web.Http.HttpPost]
        public Student[] EmployeeAttendenceReportByEmployeeId(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                dt = obj.getDataTable(@"select * from tblEmployee where Status=1 and IsDeleted is null and SchoolID='"+em.SchoolID+"' and Id="+em.DesigId);
            }

            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["Empcode"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
              

                DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblEmployeeAttendence where   cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and EmployeeId=" + dr["Id"].ToString() + " and SchoolID='"+em.SchoolID+"' and  cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();

                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    else if (dr1["AttendenceType"].ToString() == "Absent")
                    {
                        usr1.AttendenceType = "A";
                    }
                    else
                    {
                        usr1.AttendenceType = "L";
                    }
                    usr.attendenceList.Add(usr1);
                }

                list.Add(usr);


            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeEventsBySchool")]
        [System.Web.Http.HttpPost]
        public EventsDetails[] getAllEmployeeEventsBySchool(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();


            DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e  left outer join tblEventType et on et.EventId=e.EventType where EventFor=1 and e.SchoolID='"+events.SchoolID+"' and e.IsDeleted is null  union   select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType  where e.EventFor=3 and e.SchoolID='"+ events.SchoolID + "' and e.IsDeleted is null and e.DepartmentId=" + events.Department );
            foreach (DataRow dr in dt.Rows)
            {
                EventsDetails usr = new EventsDetails();
                usr.EventName = dr["EventName"].ToString();
                usr.EventType = dr["Type"].ToString();
                usr.StartDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                usr.EndDate = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];


                list.Add(usr);

            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/EmployeeAPI/getAllEmployeeEvents")]
        [System.Web.Http.HttpPost]
        public EventsDetails[] getAllEmployeeEvents(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();


            DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e  left outer join tblEventType et on et.EventId=e.EventType where EventFor=1   union   select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType  where EventFor=3 and DepartmentId="+events.Department);
            foreach (DataRow dr in dt.Rows)
            {
                EventsDetails usr = new EventsDetails();
                 usr.EventName = dr["EventName"].ToString();
                usr.EventType =  dr["Type"].ToString();
                usr.StartDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                usr.EndDate = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];
                
                
                list.Add(usr);
 
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/EmployeeAPI/sendEmailForEmployee")]
        [System.Web.Http.HttpPost]
        public string sendEmailForEmployee(EmployeeEm[] employye)
        {
            sqlHelper obj = new sqlHelper();
            foreach (var emp in employye)
            {
                if (!string.IsNullOrEmpty(emp.Employeecode))
                {
                    //var school = db.tblSchoolDetails.Single(x => x.ID == emp.SchoolID).School;
                    string EmailId = obj.ExecuteScaler("select Email from tblEmployee where SchoolID='"+emp.SchoolID+"' and Id=" + emp.Employeecode);
                    if (!string.IsNullOrEmpty(EmailId.ToString().Trim()))
                    {
                        SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                        using (MailMessage mm = new MailMessage())
                        {
                       


                            StringBuilder st = new StringBuilder();
                            st.AppendLine(emp.Department);
                            //st.AppendLine("");
                            //st.AppendLine(school);

                            mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                            mm.To.Add(EmailId); //---- Email address of the recipient.

                            mm.Subject = emp.Extra5; //---- Subject of email.
                            mm.Body = (st.ToString()); //---- Content of email.

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                            smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                            NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                            //---Your Email and password
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = NetworkCred;
                            //smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                            smtp.Port = 587;
                            smtp.Send(mm);
                            
                           
                        }
                      
                    }
                }
            }
            return "Email Sent Successfully";
        }


        
            [System.Web.Http.Route("api/EmployeeAPI/getEmployeeTaskByEmpId")]
        [System.Web.Http.HttpPost]
        public List<TaskDetails> getEmployeeTaskByEmpId(Parameters param)
        {
            List<TaskDetails> list = new List<TaskDetails>();
            sqlHelper obj = new sqlHelper();
            try
            {
                DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno, ee.FirstName+' '+ee.MiddleName+' '+ee.LastName FullName,dd.DepartmentName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
left outer join tblemployee ee on ee.Id = td.Employee
left outer join tblDepartmnet dd on dd.DepartmentId = td.Department
where td.UserType = 2 and td.IsDeleted is null  and td.SchoolID='" + param.val1+"' and Employee=" + param.val + " order by TaskDate desc");

                foreach (DataRow dr in dt.Rows)
                {
                    TaskDetails items = new TaskDetails();
                    items.sno = dr["sno"].ToString();
                    items.TaskName = dr["TaskName"].ToString();
                    items.Department = dr["DepartmentName"].ToString();

                    //  items.Class = dr["CourseName"].ToString();
                    items.EmployeeName = dr["FullName"].ToString();

                    items.TaskDate = dr["TaskDatets"].ToString();

                    items.Status = dr["Status"].ToString();
                    if (items.Status == "On hold")
                    {
                        items.color = "#ff5722";
                    }
                    else if (items.Status == "Open")
                    {
                        items.color = "#00bcd4";
                    }
                    else if (items.Status == "Resolved")
                    {
                        items.color = "#4caf50";
                    }
                    else
                    {
                        items.color = "#999";
                    }
                    items.Priority = dr["TaskPriority"].ToString();
                    if (items.Priority == "Highest Priority")
                    {
                        items.priorityColor = "#f44336";
                    }
                    else if (items.Priority == "High Priority")
                    {
                        items.priorityColor = "#00bcd4";
                    }
                    else if (items.Priority == "Normal Priority")
                    {
                        items.priorityColor = "#2196f3";
                    }
                    else
                    {
                        items.priorityColor = "#4caf50";
                    }
                    items.Description = dr["Description"].ToString();
                    items.Id = dr["Id"].ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/EmployeeAPI/changeEmployeeTaskStatus")]
        [System.Web.Http.HttpPost]
        public string changeEmployeeTaskStatus(List<string> id)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int idd = Convert.ToInt16(id[0]);
            var result = db.tblTaskDetails.SingleOrDefault(s => s.Id == idd);
            result.Status = "Resolved";
            db.SaveChanges();
            return "";
        }

        [System.Web.Http.Route("api/EmployeeAPI/viewTaskDetasilForEmployeeById")]
        [System.Web.Http.HttpPost]
        public TaskDetails viewTaskDetasilForEmployeeById(List<string> id)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int iddd = Convert.ToInt32(id[0]);
            var result = db.tblTaskDetails.SingleOrDefault(s => s.Id == iddd); 
            TaskDetails usr = new TaskDetails();
            usr.TaskName = result.TaskName;
            usr.Description = result.Description;
            usr.Priority = result.TaskPriority;
            usr.TaskDate = Convert.ToDateTime(result.TaskDate).ToString("MMM dd, yyyy");
            usr.Status = result.Status;
            if (usr.Status == "On hold")
            {
                usr.color = "#ff5722";
            }
            else if (usr.Status == "Open")
            {
                usr.color = "#00bcd4";
            }
            else if (usr.Status == "Resolved")
            {
                usr.color = "#4caf50";
            }
            else
            {
                usr.color = "#999";
            }
            
            if (usr.Priority == "Highest Priority")
            {
                usr.priorityColor = "#f44336";
            }
            else if (usr.Priority == "High Priority")
            {
                usr.priorityColor = "#00bcd4";
            }
            else if (usr.Priority == "Normal Priority")
            {
                usr.priorityColor = "#2196f3";
            }
            else
            {
                usr.priorityColor = "#4caf50";
            }


            return usr;

        }



        [System.Web.Http.Route("api/EmployeeAPI/getAllCountrySateCity")]
        [System.Web.Http.HttpPost]
        public List<CountyMaster> getAllCountrySateCity()
        {
            try
            {
                SCHOOLERPEntities db = new SCHOOLERPEntities();
                List<CountyMaster> list = new List<CountyMaster>();
                var countryMaster = db.tblCountries.Where(s => s.Status == true && s.IsDeleted==null).ToList();

                foreach (var c in countryMaster)
                {
                    CountyMaster usr = new CountyMaster();
                    usr.Name = c.CountryName;

                    var stateMaster = db.tblStates.Where(s => s.status == true && s.countryId == c.CountryID && s.IsDeleted == null).ToList();
                    usr.MasteState = new List<StateMaster>();
                    foreach (var s in stateMaster)
                    {
                        StateMaster usr1 = new StateMaster();
                        usr1.Name = s.StateName;

                        var citiMas = db.tblCities.Where(x => x.Status == true && x.StateId == s.StateId && s.IsDeleted == null).ToList();

                        usr1.cities = new List<CityMaster>();
                        foreach (var ccc in citiMas)
                        {
                            CityMaster usr2 = new CityMaster();
                            usr2.Name = ccc.CityName;
                            usr1.cities.Add(usr2);
                        }
                        usr.MasteState.Add(usr1);

                    }
                    list.Add(usr);

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [System.Web.Http.Route("api/EmployeeAPI/SaveAttendanceDetails")]
        [System.Web.Http.HttpGet]
        public void SaveAttendanceDetails()
        {
            try
            {
                DateTime strtdate = DateTime.Now.Date;
                DateTime enddate = DateTime.Now.Date.AddDays(-2);
                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable(@"select * from Tran_MachineRawPunch where CONVERT(date, PunchDatetime) <='" + strtdate.ToString("yyyy-MM-dd") + "' and CONVERT(date, PunchDatetime) >='" + enddate.ToString("yyyy-MM-dd") + "' order by PunchDatetime");
                foreach (DataRow dr in dt.Rows)
                {
                    int tc1 = 0;
                    string empid = "";
                    string deptid = "";
                    string desigid = "";
                    string empcode = Convert.ToString(dr["CardNo"]);
                    string punchdt = Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd");
                    SqlDataReader dr1 = obj.GetReader("select id,DeptID,DesigID from tblemployee where isdeleted is null and biometricid='" + dr["CardNo"] + "'");
                    if (dr1.Read())
                    {
                        empid = Convert.ToString(dr1["id"]);
                        deptid = Convert.ToString(dr1["DeptID"]);
                        desigid = Convert.ToString(dr1["DesigID"]);
                    }
                    if (empid == "")
                    {
                    }
                    else
                    {
                        int tc = Convert.ToInt32(obj.ExecuteScaler1("select count(*) from tblEmployeeAttendence where CONVERT(date, attendencedate) ='" + punchdt + "' and EmployeeId=(select id from tblEmployee where IsDeleted is null and biometricid='" + dr["CardNo"] + "')"));
                        if (tc == 0)//if no record exist for selected date and for selected employee
                        {
                            string[] cols = { "EmployeeId", "DesigId", "DepId", "AttendenceType", "AttendenceDate", "DateCreated", "IsBiometric", "TimeIn" };
                            object[] vals = { empid, desigid, deptid, "Present", Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd"), DateTime.Now, "1", dr["PunchDatetime"] };
                            obj.insertAttendanceValIntoTable("tblEmployeeAttendence", cols, vals);
                        }
                        else
                        {
                            tc1 = Convert.ToInt32(obj.ExecuteScaler1("select count(*) from tblEmployeeAttendence where (TimeIn='" + Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd HH:mm") + "' or TimeOut='" + Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd HH:mm") + "') and EmployeeId=(select id from tblEmployee where IsDeleted is null and biometricid='" + dr["CardNo"] + "')"));
                            if (tc1 == 0)
                            {
                                string chkinout = Convert.ToString(obj.ExecuteScaler1("select timeout from tblEmployeeAttendence where attendenceid=(select max(attendenceid) from tblEmployeeAttendence where CONVERT(date, attendencedate) ='" + punchdt + "' and EmployeeId=(select id from tblEmployee where IsDeleted is null and biometricid='" + dr["CardNo"] + "'))"));
                                int chkid = Convert.ToInt32(obj.ExecuteScaler1("select max(attendenceid) from tblEmployeeAttendence where CONVERT(date, attendencedate) ='" + punchdt + "' and EmployeeId=(select id from tblEmployee where IsDeleted is null and biometricid='" + dr["CardNo"] + "')"));
                                if (String.IsNullOrEmpty(chkinout))
                                {
                                    string[] cols = { "EmployeeId", "DesigId", "DepId", "AttendenceType", "AttendenceDate", "DateCreated", "IsBiometric", "TimeOut" };
                                    object[] vals = { empid, desigid, deptid, "Present", Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd"), DateTime.Now, "1", dr["PunchDatetime"] };
                                    obj.updateValIntoTable("tblEmployeeAttendence", cols, vals, "attendenceid", chkid);
                                }
                                else
                                {
                                    string[] cols = { "EmployeeId", "DesigId", "DepId", "AttendenceType", "AttendenceDate", "DateCreated", "IsBiometric", "TimeIn" };
                                    object[] vals = { empid, desigid, deptid, "Present", Convert.ToDateTime(dr["PunchDatetime"]).ToString("yyyy-MM-dd"), DateTime.Now, "1", dr["PunchDatetime"] };
                                    obj.insertAttendanceValIntoTable("tblEmployeeAttendence", cols, vals);

                                }

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        [System.Web.Http.Route("api/EmployeeAPI/CheckLeave")]
        [System.Web.Http.HttpGet]
        public int CheckLeave(string id, string date)
        {
            try
            {
                int i = 0;
                sqlHelper obj = new sqlHelper();
              
                int tc = Convert.ToInt32(obj.ExecuteScaler1("select count(*) from tblemployeeleaverequest where EmployeeID='" + id + "' and '" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "' between StatDate and EndDate"));
                if (tc > 0)
                {
                    i = 1;
                }
                return i;
            }
            catch
            {
                return 0;
            }
        }

        [System.Web.Http.Route("api/EmployeeAPI/SearchDailyEmployeeAttendenceByfilter")]
        [System.Web.Http.HttpPost]
        public Student[] SearchDailyEmployeeAttendenceByfilter(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            string[] cols = { "@empcode", "@empName", "@department", "@designation", "@staff", "@School" };
            object[] vals = { em.Employeecode, em.FName, em.Department, em.Designation, em.UserType, em.School };
            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeAttendenceForFilter", cols, vals);
            string attdate = em.Month.Split('/')[2] + '-' + em.Month.Split('/')[1] + '-' + em.Month.Split('/')[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Student usr = new Student();
                    usr.attendenceList = new List<StudentAttendenceDetails>();
                    usr.ID = int.Parse(dr["Id"].ToString());
                    usr.RegNo = dr["Empcode"].ToString();
                    //usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToString(dr["SchoolID"]);
                    int SchoolIDD = Convert.ToInt32(dr["SchoolID"]);
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,cast(Month(AttendenceDate) as varchar(50)) CurrentMonth,cast(year(AttendenceDate) as varchar(50) )CurrentYear, TimeIn,TimeOut,AttendenceType,AttendenceDate from tblEmployeeAttendence where employeeid='" + usr.ID + "' and AttendenceDate='" + attdate + "' and SchoolID='"+em.School+"'");
                    usr.Total = dt1.Rows.Count;
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                            usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                            usr1.AttendenceDay1 = Convert.ToInt32( dr1["CurrentDay"].ToString());
                            usr1.AttendenceMonth1 = Convert.ToInt32( dr1["CurrentMonth"].ToString());
                            usr1.AttendenceYear1 = Convert.ToInt32 ( dr1["CurrentYear"].ToString());

                        if (String.IsNullOrEmpty(dr1["TimeIn"].ToString()))
                        {
                            usr1.InTime = "";
                        }
                        else
                        {
                            usr1.InTime = Convert.ToDateTime(dr1["TimeIn"].ToString()).ToString("HH:mm");
                        }


                        if (String.IsNullOrEmpty(dr1["TimeOut"].ToString()))
                        {
                            usr1.OutTime = "";
                        }
                        else
                        {
                            usr1.OutTime = Convert.ToDateTime(dr1["TimeOut"].ToString()).ToString("HH:mm");
                        }

                        if (dr1["AttendenceType"].ToString() == "Present")
                        {
                            usr1.AttendenceType = "Present";
                        }
                        
                        else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                        {
                           

                            var checkleave = db.tblemployeeLeaveRequests.Where(lv => lv.EmployeeID == usr.ID && lv.StatDate <= usr1.AttendenceRealDate && lv.EndDate >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.LeavStatus == 5).FirstOrDefault();
                            if (checkleave != null)
                            {
                                usr1.AttendenceType = "Leave";

                            }
                           
                            else
                            {
                                var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == SchoolIDD && Hl.IsDeleted == null).FirstOrDefault();
                                if (checkHoliday != null)
                                {
                                    usr1.AttendenceType = "Holiday";

                                }
                                else
                                {
                                    usr1.AttendenceType = "Absent";
                                }
                            }



                        }
                        usr.attendenceList.Add(usr1);                        
                       
                    }
                    list.Add(usr);

                }

            }
            var newList = list.OrderByDescending(x => x.Total).ToList();
            return newList.ToArray();
        }
        //Realtime machine
        [System.Web.Http.Route("api/EmployeeAPI/saveMachineDetails")]
        [System.Web.Http.HttpPost]
        public string saveMachineDetails(AttendenceMachineMaster val)
        {
            string a = "";
            int SchoolID = Convert.ToInt32(val.SchoolID);
            if (val.Id==0)
            {
               
                var check = db.tblAttendenceMachineMasters.Where(x => x.MachineNo == val.MachineNo && x.IsActice == true).FirstOrDefault();
                if (check!=null)
                {
                    string SchoolName = db.tblSchoolDetails.SingleOrDefault(x => x.ID == check.SchoolID).School;
                    return a="This Machine ID Is Already Active for School:"+SchoolName;

                }
                else
                {
                    tblAttendenceMachineMaster avi = new tblAttendenceMachineMaster();
                    avi.MachineNo = val.MachineNo;
                    avi.SchoolID = Convert.ToInt32(val.SchoolID);
                    avi.IsActice = val.IsActice;
                    avi.machinetype = val.machinetype;
                    db.tblAttendenceMachineMasters.Add(avi);
                    db.SaveChanges();
                    return a = "Machine assigned Successfully";

                }


               



            }
            else
            {
                var check = db.tblAttendenceMachineMasters.Where(m => m.MachineNo == val.MachineNo && m.IsActice == true && m.Id!=val.Id).FirstOrDefault();
                if (check!=null)
                {
                    string SchoolName = db.tblSchoolDetails.SingleOrDefault(x => x.ID == check.SchoolID).School;
                    return a = "This Machine ID Is Already Active for School:" + SchoolName;
                }

                else
                {
                    var updatemachine = db.tblAttendenceMachineMasters.Where(u => u.Id == val.Id ).FirstOrDefault();
                    tblAttendenceMachineMaster avi = new tblAttendenceMachineMaster();
                    updatemachine.MachineNo = val.MachineNo;
                    updatemachine.IsActice = val.IsActice;
                    updatemachine.SchoolID = Convert.ToInt32(val.SchoolID);
                    updatemachine.machinetype = val.machinetype;
                    db.SaveChanges();
                    return a = "Machine Record Updated Sucessfully";

                }



            }
           
            
          }

       
        [System.Web.Http.Route("api/EmployeeAPI/GetALLRealtimeMachine")]
        [System.Web.Http.HttpPost]
        public AttendenceMachineMaster[] GetALLRealtimeMachine (List<string> val )
        {
            var getallmachine = db.tblAttendenceMachineMasters.ToList();
            List<AttendenceMachineMaster> list = new List<AttendenceMachineMaster>();
            if ( getallmachine!=null)
            {
               
                foreach (var avi in getallmachine)
                {
                    AttendenceMachineMaster ab = new AttendenceMachineMaster();
                    var school = db.tblSchoolDetails.Where(sc => sc.ID == avi.SchoolID).FirstOrDefault();
                    if (school!=null)
                    {
                        ab.School = school.School;
                    }
                    else
                    {
                        ab.School = "Not Found";             
                    }
                    ab.MachineNo = avi.MachineNo;
                    ab.IsActice = avi.IsActice;
                    if (ab.IsActice==true)
                    {
                        ab.Status = "Active";
                    }
                    else
                    {
                        ab.Status = "DeActive";
                    }
                    ab.Id = avi.Id;
                    ab.machinetype = avi.machinetype;
                    list.Add(ab);

                }
            }
          


            return list.ToArray();
        }

        //api/EmployeeAPI/EditMachineDetails
        [System.Web.Http.Route("api/EmployeeAPI/EditRealtimeMachine")]
        [System.Web.Http.HttpPost]
        public AttendenceMachineMaster EditRealtimeMachine(List<string> id)
        {
            int idd = Convert.ToInt32(id[0]);
            AttendenceMachineMaster usr = new AttendenceMachineMaster();
            var fatch = db.tblAttendenceMachineMasters.Where(d => d.Id == idd).FirstOrDefault();
            usr.MachineNo= fatch.MachineNo;
            usr.IsActice = fatch.IsActice;
            usr.SchoolID = fatch.SchoolID.ToString();
            usr.machinetype = fatch.machinetype.ToString();
            usr.Id = fatch.Id;

            return usr;


        }


        [System.Web.Http.Route("api/EmployeeAPI/CheckClassteacherallowcationforpromotion")]
        [System.Web.Http.HttpPost]
        public string CheckClassteacherallowcationforpromotion(List<string> value)
        {
            List<ClassTeacherAllocation> list = new List<ClassTeacherAllocation>();
            string valueee = "-1";
            sqlHelper obj = new sqlHelper();
             try
            {
                int SchoolID = Convert.ToInt32( value[0]);
                int EmployeeID = Convert.ToInt32( value[1]);
                var checkclassteacherOrnot = db.tblClassTeacherAllocations.Where(t => t.SchoolID == SchoolID && t.intEmpID == EmployeeID && t.Status == 1).FirstOrDefault();
                if (checkclassteacherOrnot!=null)
                {
                    valueee = "1";
                }
                else
                {
                    valueee = "-1";

                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
           

           

            return valueee;
        }

    }
}
