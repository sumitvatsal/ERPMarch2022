using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolERP_BLL;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SchoolErp.Controllers.WebApi
{
    public class PayrollAPIController : ApiController
    {

        [System.Web.Http.Route("api/PayrollAPI/saveSalaryHead")]
        [System.Web.Http.HttpPost]
        public string saveSalaryHead(saleryHead sal)
        {

            string b = PayrollDetails.saveSalaryHead(sal);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/PayrollAPI/getAllSalryHead")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getAllSalryHead(List <string > aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<saleryHead> list = new List<saleryHead>();
            sqlHelper obj = new sqlHelper();
           
             if (typeuser==2)
            {

                DataTable dt = obj.getDataTable(@"select a.School,b.SchoolID, b.Id,b.SalaryType,b.Code,b.Name,b.Status from tblSalaryHead b
 left outer join tblSchoolDetails a on a.ID=b.SchoolID
  where  b.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    saleryHead usr = new saleryHead();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Type = dr["SalaryType"].ToString();
                    usr.Code = dr["Code"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    usr.School = dr["School"].ToString();
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
                    list.Add(usr);
                }
               
            }
             else
            {
                DataTable dt = obj.getDataTable(@"select a.School,b.SchoolID, b.Id,b.SalaryType,b.Code,b.Name,b.Status from tblSalaryHead b
 left outer join tblSchoolDetails a on a.ID=b.SchoolID
 left outer join tblemployee em on em.SchoolID= b.SchoolID
  where em.UserID='"+ loginuser + "' and em.IsDeleted is null and  b.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    saleryHead usr = new saleryHead();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Type = dr["SalaryType"].ToString();
                    usr.Code = dr["Code"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    usr.School = dr["School"].ToString();
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
                    list.Add(usr);
                }
            }

            return list.ToArray();


        }




        [System.Web.Http.Route("api/PayrollAPI/deletesalaryHeadById")]
        [System.Web.Http.HttpPost]
        public string deletesalaryHeadById(string id)
        {
            bool b = PayrollDetails.deletesalaryHeadById(id);
            if (b)
            {
                return "Salary Head Deleted Successfully";
            }
            else
            {
                return "Salary Head Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/PayrollAPI/getEmployeePayrollHead")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getEmployeePayrollHead(List<string> id)
        {
           // string loginuser = id[1];
            int SchoolID = Convert.ToInt32( id[1]);
            sqlHelper obj = new sqlHelper();
            List<saleryHead> list = new List<saleryHead>();
           
          
                DataTable dt = obj.getDataTable(@"select a.School,b.SchoolID,b.Id,SalaryType,Name,Code,Status from tblSalaryHead b left outer join tblSchoolDetails a on a.ID =b.SchoolID where SalaryType='" + id[0] + "' and SchoolID='"+SchoolID+ "' and b.Isdeleted is null and b.Status=1 ");      
                foreach (DataRow dr in dt.Rows)
                {
                    saleryHead usr = new saleryHead();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Code = dr["SalaryType"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            
         
            return list.ToArray();
        }


        [System.Web.Http.Route("api/PayrollAPI/saveGradeDetails")]
        [System.Web.Http.HttpPost]
        public string saveGradeDetails(GradeDetails grade)
        {

            string b = PayrollDetails.saveGradeDetails(grade);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/PayrollAPI/saveGradesalaryAssignDetails")]
        [System.Web.Http.HttpPost]
        public string saveGradesalaryAssignDetails(GradeDetails[] grade)
        {

            string b = PayrollDetails.saveGradesalaryAssignDetails(grade);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/PayrollAPI/getAllGradeByDesignationbyschool")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getAllGradeByDesignationbyschool(List<string> val)
        {
            int SchoolIDD = Convert.ToInt32(val[1]);
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblMasterGrade where DesignationId='" + val[0] + "' and Status=1 and SchoolID='" + SchoolIDD + "' ORDER BY LOWER(GradeName)");
            List<saleryHead> list = new List<saleryHead>();
            foreach (DataRow dr in dt.Rows)
            {
                saleryHead usr = new saleryHead();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["GradeName"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/PayrollAPI/getAllGradeByDesignation")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getAllGradeByDesignation(List<string> val)
        {

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblMasterGrade where DesignationId='" + val[0] + "' and Status=1");
            List<saleryHead> list = new List<saleryHead>();
            foreach (DataRow dr in dt.Rows)
            {
                saleryHead usr = new saleryHead();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["GradeName"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/PayrollAPI/getEmployeePayrollHeadByGradId")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getEmployeePayrollHeadByGradId(GradeDetails salry)
        {
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"
select a.School,ga.SchoolID, sh.Name,ga.* from tblGradeAssignSalaryHead ga
 inner join tblSalaryHead sh on sh.Id=ga.HeadId left outer join tblSchoolDetails a on a.ID=ga.SchoolID where GradeId=" + salry.Id + " and headType='" + salry.Type + "' and ga.SchoolID='"+salry.SchoolID+"' ");
            List<saleryHead> list = new List<saleryHead>();
            foreach (DataRow dr in dt.Rows)
            {
                saleryHead usr = new saleryHead();
                usr.Id = dr["HeadId"].ToString();
                usr.Name = dr["Name"].ToString();
                usr.Extra10 = dr["Amount"].ToString();
                usr.Code = dr["Type"].ToString();
                usr.School = dr["School"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/PayrollAPI/getAllDesignationbySchoolID")]
        [System.Web.Http.HttpPost]
        public BatchMaster[] getAllDesignationbySchoolID(List<string> val)
        {

            List<BatchMaster> list = new List<BatchMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@" select DesigID,Designation,Status from tblDesignation where Status=1 and SchoolID='"+val[0]+ "' and IsDeleted is null ORDER BY LOWER(Designation)");

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
        //avn
        [System.Web.Http.Route("api/PayrollAPI/getAllEmployeeByDesignationBySchool")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getAllEmployeeByDesignationBySchool(List<string> val)
        {
            int SchoolIDD = Convert.ToInt32(val[1]);
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select Id,FirstName+' '+MiddleName+' '+LastName+' ('+d.DepartmentName+')' as Name from tblEmployee emp
                                     left outer join tblDepartmnet d on d.DepartmentId=emp.DeptID
                                       where  emp.DesigID='" + val[0] + "' and emp.Status=1 and emp.SchoolID='" + SchoolIDD + "' and emp.IsDeleted is null ORDER BY LOWER(FirstName) ");
            //DataTable dt = obj.getDataTable(@" select Id,FirstName+' '+MiddleName+' '+LastName+' ('+d.DepartmentName+')' as Name from tblEmployee emp
            //                         left outer join tblDepartmnet d on d.DepartmentId=emp.DeptID
            //                          where  emp.DesigID='" + val[0] + "' and emp.Status=1");
            List<saleryHead> list = new List<saleryHead>();
            foreach (DataRow dr in dt.Rows)
            {
                saleryHead usr = new saleryHead();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["Name"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/PayrollAPI/getAllEmployeeByDesignation")]
        [System.Web.Http.HttpPost]
        public saleryHead[] getAllEmployeeByDesignation(List<string> val)
        {
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@" select Id,FirstName+' '+MiddleName+' '+LastName+' ('+d.DepartmentName+')' as Name from tblEmployee emp
                                     left outer join tblDepartmnet d on d.DepartmentId=emp.DeptID
                                       where  emp.DesigID='" + val[0] + "' and emp.Status=1");
            //DataTable dt = obj.getDataTable(@" select Id,FirstName+' '+MiddleName+' '+LastName+' ('+d.DepartmentName+')' as Name from tblEmployee emp
            //                         left outer join tblDepartmnet d on d.DepartmentId=emp.DeptID
            //                          where  emp.DesigID='" + val[0] + "' and emp.Status=1");
            List<saleryHead> list = new List<saleryHead>();
            foreach (DataRow dr in dt.Rows)
            {
                saleryHead usr = new saleryHead();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["Name"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/PayrollAPI/saveEmployeeGrade")]
        [System.Web.Http.HttpPost]
        public string saveEmployeeGrade(GradeDetails grade)
        {

            string b = PayrollDetails.saveEmployeeGrade(grade);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }

        [System.Web.Http.Route("api/PayrollAPI/getAllAssginGradEmployee")]
        [System.Web.Http.HttpPost]
        public GradeDetails[] getAllAssginGradEmployee(List<string > aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<GradeDetails> list = new List<GradeDetails>();
          
           if (typeuser==2)
                    {
                DataTable dt = obj.getDataTable(@"select a.School, e.FirstName+' '+e.MiddleName+' '+e.LastName as Name,d.Designation,td.DepartmentName,GradeName,Ea.* from  tblEmployeeAccountInformation Ea
                                        left outer join tblEmployee e on e.Id=Ea.EmployeeId
                                        left outer join tblDesignation d on d.DesigID=Ea.DesignationId
                                        left outer join tblDepartmnet td on td.DepartmentId=e.DeptID
                                        left outer join tblMasterGrade mg on mg.Id=Ea.GradeId 
										left outer join tblSchoolDetails a on a.ID=ea.SchoolID");
              
                foreach (DataRow dr in dt.Rows)
                {
                    GradeDetails usr = new GradeDetails();
                    usr.HeadId = dr["Id"].ToString();
                    usr.Id = dr["EmployeeId"].ToString();
                    usr.EmployeeName = dr["Name"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Extra10 = dr["DepartmentName"].ToString();
                    usr.GradeName = dr["GradeName"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    usr.School = dr["School"].ToString();
                    if (dr["Status"].ToString() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra11 = "btn btn-block btn-success btn-sm";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra11 = "btn btn-block btn-danger btn-sm";

                    }
                    list.Add(usr);
                }
            }
           else
            {
                DataTable dt = obj.getDataTable(@"select a.School, e.FirstName+' '+e.MiddleName+' '+e.LastName as Name,d.Designation,td.DepartmentName,GradeName,Ea.* from  tblEmployeeAccountInformation Ea
                                        left outer join tblEmployee e on e.Id=Ea.EmployeeId
                                        left outer join tblDesignation d on d.DesigID=Ea.DesignationId
                                        left outer join tblDepartmnet td on td.DepartmentId=e.DeptID
                                        left outer join tblMasterGrade mg on mg.Id=Ea.GradeId 
										left outer join tblSchoolDetails a on a.ID=ea.SchoolID
										left outer join tblEmployee em on em.SchoolID=Ea.SchoolID
										where em.UserID='" + loginuser + "' and em.IsDeleted is null");

                foreach (DataRow dr in dt.Rows)
                {
                    GradeDetails usr = new GradeDetails();
                    usr.HeadId = dr["Id"].ToString();
                    usr.Id = dr["EmployeeId"].ToString();
                    usr.EmployeeName = dr["Name"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Extra10 = dr["DepartmentName"].ToString();
                    usr.GradeName = dr["GradeName"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    usr.School = dr["School"].ToString();
                    if (dr["Status"].ToString() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra11 = "btn btn-block btn-success btn-sm";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra11 = "btn btn-block btn-danger btn-sm";

                    }
                    list.Add(usr);
                }
            }
            return list.ToArray();
        }
               
            
     


        [System.Web.Http.Route("api/PayrollAPI/editGradeAssignById")]
        [System.Web.Http.HttpPost]
        public GradeDetails editGradeAssignById(List<string> id)
        {
            List<GradeDetails> list = new List<GradeDetails>();
           sqlHelper obj = new sqlHelper();
            SqlDataReader dr = obj.GetReader(" select * from tblEmployeeAccountInformation where Id=" + id[0]);
           GradeDetails usr = new GradeDetails();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.EmployeeId = dr["EmployeeId"].ToString();
                usr.Designation = dr["DesignationId"].ToString();
                usr.GradeId = dr["GradeId"].ToString();
                usr.Status = dr["Status"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
             }

             return usr;
 

        }



        [System.Web.Http.Route("api/PayrollAPI/getAllMasterGrade")]
        [System.Web.Http.HttpPost]
        public GradeDetails[] getAllMasterGrade(List <string > aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<GradeDetails> list = new List<GradeDetails>();
           
             if (typeuser==2)
            {
                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable(@"select a.School, d.Designation,g.* from  tblMasterGrade  g 
                                       left outer join tblDesignation d on d.DesigID = g.DesignationId left outer join tblSchoolDetails a on a.ID=g.SchoolID");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GradeDetails usr = new GradeDetails();
                        usr.Id = dr["Id"].ToString();
                        usr.Designation = dr["Designation"].ToString();
                        usr.GradeName = dr["GradeName"].ToString();
                        usr.SchoolID = dr["SchoolID"].ToString();
                        usr.School = dr["School"].ToString();
                        if (dr["Status"].ToString() == "True")
                        {
                            usr.Status = "Activate";
                            usr.Extra11 = "btn btn-block btn-success btn-sm";
                        }
                        else
                        {
                            usr.Status = "DeActivate";
                            usr.Extra11 = "btn btn-block btn-danger btn-sm";
                        }
                        list.Add(usr);
                    }

                }
            }
             else
            {
                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable(@"select a.School, d.Designation,g.* from  tblMasterGrade  g 
                                        left outer join tblDesignation d on d.DesigID = g.DesignationId
									    left outer join tblSchoolDetails a on a.ID=g.SchoolID
										left outer join tblEmployee em on em.SchoolID=g.SchoolID
										where em.UserID='" + loginuser + "' and em.IsDeleted is null ");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GradeDetails usr = new GradeDetails();
                        usr.Id = dr["Id"].ToString();
                        usr.Designation = dr["Designation"].ToString();
                        usr.GradeName = dr["GradeName"].ToString();
                        usr.SchoolID = dr["SchoolID"].ToString();
                        usr.School = dr["School"].ToString();
                        if (dr["Status"].ToString() == "True")
                        {
                            usr.Status = "Activate";
                            usr.Extra11 = "btn btn-block btn-success btn-sm";
                        }
                        else
                        {
                            usr.Status = "DeActivate";
                            usr.Extra11 = "btn btn-block btn-danger btn-sm";
                        }
                        list.Add(usr);
                    }

                }
            }
            return list.ToArray();
 
        }

        [System.Web.Http.Route("api/PayrollAPI/editGradeDetasilById")]
        [System.Web.Http.HttpPost]
        public GradeDetails editGradeDetasilById(List<string> id)
        {
            List<GradeDetails> list = new List<GradeDetails>();
            sqlHelper obj = new sqlHelper();
            SqlDataReader dr = obj.GetReader("select * from tblMasterGrade where Id=" + id[0]);
            GradeDetails usr = new GradeDetails();
            if (dr.Read())
            {
                usr.Id = dr["Id"].ToString();
                usr.Designation = dr["DesignationId"].ToString();
                usr.GradeName = dr["GradeName"].ToString();
                usr.Status = dr["Status"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
            }

            return usr;


        }
 

        [System.Web.Http.Route("api/PayrollAPI/getsalayHeadDetails")]
        [System.Web.Http.HttpPost]
        public GradeDetails[] getsalayHeadDetails(List<string> id)
        {
            List<GradeDetails> list = new List<GradeDetails>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblGradeAssignSalaryHead where GradeId= "+ id[0]);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GradeDetails usr = new GradeDetails();
                    usr.Id = dr["Id"].ToString();
                    usr.HeadId = dr["HeadId"].ToString();
                    usr.HeadAmount = dr["Amount"].ToString();
                    usr.Type = dr["Type"].ToString();
                    //if (dr["Status"].ToString() == "True")
                    //{
                    //    usr.Status = "Activate";
                    //    usr.Extra11 = "btn btn-block btn-success btn-sm";
                    //}
                    //else
                    //{
                    //    usr.Status = "DeActivate";
                    //    usr.Extra11 = "btn btn-block btn-danger btn-sm";
                    //}
                    list.Add(usr);
                }

            }
            return list.ToArray();

        }


        [System.Web.Http.Route("api/PayrollAPI/getGradebeEmployeeId")]
        [System.Web.Http.HttpPost]
        public GradeDetails[] getGradebeEmployeeId(List<string> id)
        {

            int SchoolID = Convert.ToInt32(id[1]);
            List<GradeDetails> list = new List<GradeDetails>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select mg.GradeName,em.* from tblEmployeeAccountInformation em
                                            left outer join tblMasterGrade mg on mg.Id=em.GradeId
                                             where EmployeeId=" + id[0] + " and em.Status=1 and em.SchoolID='" + SchoolID + "' ORDER BY LOWER(mg.GradeName)");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GradeDetails usr = new GradeDetails();
                    usr.Id = dr["GradeId"].ToString();
                    usr.GradeName = dr["GradeName"].ToString();

                    list.Add(usr);
                }

            }
            return list.ToArray();

        }

        [System.Web.Http.Route("api/PayrollAPI/saveEmployeeSalaryDetails")]
        [System.Web.Http.HttpPost]
        public string saveEmployeeSalaryDetails(EmployeeSalaryDerails salary)
        {
            string b = "";
            sqlHelper obj = new sqlHelper();
            string empidd = obj.ExecuteScaler(" select EmployeeId from tblEmployeeSalaryDetails where FromDate='"+salary.FromDate+"' and toDate='"+salary.ToDate+"' and EmployeeId='"+ salary.EmployeeName + "' and SchoolID='"+salary.SchoolID+"' ");
            if (string.IsNullOrEmpty(empidd))
            {
                  b = PayrollDetails.saveEmployeeSalaryDetails(salary);
            }
            else
            {
                return "Salary Already Paid for this Months";
            }
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }


        [System.Web.Http.Route("api/PayrollAPI/saveEmployeeSalaryDettailsStructure")]
        [System.Web.Http.HttpPost]
        public string saveEmployeeSalaryDettailsStructure(EmployeeSalaryDerails[] salary)
        {

            string b = PayrollDetails.saveEmployeeSalaryDettailsStructure(salary);
            if (b != "")
            {
                return b;
            }
            else
            {

                return "";
            }


        }



        [System.Web.Http.Route("api/PayrollAPI/searchsalryDetails")]
        [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails[] searchsalryDetails(EmployeeSalaryDerails employye)
        {
            // string enddate = ((DateTime)Convert.ToDateTime(employye.Extra6)).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime startDate = DateTime.ParseExact(employye.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(employye.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //   DateTime startDate= DateTime.ParseExact(employye.Extra6, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //   DateTime enddate = DateTime.ParseExact(employye.Extra6, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            sqlHelper obj = new sqlHelper();
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();

            string[] cols = { "@empcode", "@empName", "@startdate", "@enddate", "@designation","@School"};
            object[] vals = { employye.Id, employye.EmployeeName, startDate, endDate, employye.Designation,employye.SchoolID };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeSalryDetails", cols, vals);
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
                usr.SchoolID =dr["SchoolID"].ToString();
                usr.School = dr["School"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/PayrollAPI/getSchoolDetails")]
        [System.Web.Http.HttpPost]
        //public tblSchoolDetail getSchoolDetails(List<string> id)
        ////{
        //    List<tblSchoolDetail> list = new List<tblSchoolDetail>();
        //    sqlHelper obj = new sqlHelper();
        //    int SchoolID = Convert.ToInt32(id[0]);
        //    SqlDataReader dr = obj.GetReader(@"select s.StateName,sd.* from tblSchoolDetails sd 
        //                             left outer join tblState s on s.StateId = sd.State where sd.ID='" + SchoolID + "'");
        //    tblSchoolDetail usr = new tblSchoolDetail();
        //    if (dr.Read())
        //    {

        //        usr.School = dr["School"].ToString();
        //        usr.Address = dr["Address"].ToString();
        //        //usr.City = dr["City"].ToString();
        //        usr.SchoolCode = dr["StateName"].ToString();
        //        usr.District = dr["District"].ToString();
        //        usr.Pincode = Int64.Parse(dr["Pincode"].ToString());

        //    }

        //    return usr;


        //}



        [System.Web.Http.Route("api/PayrollAPI/getEmployeeSalaryDetailForSlip")]
      //  [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails getEmployeeSalaryDetailForSlip(List<string> id)
        {
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();
            sqlHelper obj = new sqlHelper();
            //SqlDataReader dr = obj.GetReader(@" select e.Id empId,e.Empcode,e.ESINO,e.AdharNo, e.FirstName+e.MiddleName+e.LastName as FullName,d.Designation,dp.DepartmentName,
            //                                     e.DOB,e.AccountNumber,e.JoiningDt,mg.GradeName,e.PANNO,e.PFNo,e.PaymentMod,DATENAME(month,es.FromDate) 'MonthName',DATENAME(year,es.FromDate) 'YearName',e.PermAddress,es.* from tblEmployeeSalaryDetails es
            //                                     left outer join tblEmployee e on e.id=es.EmployeeId
            //                                      left outer join tblDesignation d on d.DesigID=es.DesignationId
            //                                       left outer join tblDepartmnet dp on dp.DepartmentId=e.DeptID
            //                                       left outer join tblMasterGrade mg on mg.Id=es.GradeId
            //                                       where es.Id=" + id[0]);
            SqlDataReader dr = obj.GetReader(@"	select e.Id empId,StateName,sch.School,sch.Address,sch.Pincode,sch.District,sch.State,sch.SchoolCode,e.Empcode,e.ESINO,e.AdharNo, CONCAT(CONCAT(e.FirstName, ' '),CONCAT(e.MiddleName, ' '),e.LastName) as FullName,d.Designation,dp.DepartmentName,
                                                 e.DOB,e.AccountNumber,e.JoiningDt,mg.GradeName,e.PANNO,e.PFNo,e.PaymentMod,DATENAME(month,es.FromDate) 'MonthName',DATENAME(year,es.FromDate) 'YearName',e.PermAddress,es.* from tblEmployeeSalaryDetails es
                                                 left outer join tblEmployee e on e.id=es.EmployeeId
                                                  left outer join tblDesignation d on d.DesigID=es.DesignationId
                                                   left outer join tblDepartmnet dp on dp.DepartmentId=e.DeptID
                                                   left outer join tblMasterGrade mg on mg.Id=es.GradeId
												   left outer join tblSchoolDetails sch on 	es.SchoolID=sch.ID		
												   left outer join tblState sta on 	sch.State=sta.StateId	
                                                   where es.Id=" + id[0]);

            EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
            if (dr.Read())
            {

                usr.Month = dr["MonthName"].ToString();
                usr.Year = dr["YearName"].ToString();
                usr.AccountNumber = dr["AccountNumber"].ToString();
                usr.PfNo = dr["PFNo"].ToString();
                usr.PaymentMode = dr["PaymentMod"].ToString();
                usr.GradeName = dr["GradeName"].ToString();
                usr.JoiningDate = ((DateTime)dr["JoiningDt"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  
                usr.Dob = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);  
                usr.Designation = dr["Designation"].ToString();
                usr.GrossDeduction = dr["DepartmentName"].ToString();
                usr.EmployeeId = dr["Empcode"].ToString();

                usr.EmployeeName = dr["FullName"].ToString();
                usr.adhaarNo = dr["AdharNo"].ToString();
                usr.NoOfLeave = dr["NoOfLeave"].ToString();
                usr.TotalDayInMonth = dr["totalDayInMonth"].ToString();

                usr.PanNo = dr["PANNO"].ToString();
                 usr.esiNumber = dr["ESINO"].ToString();
                usr.EmpId = dr["empId"].ToString();
                usr.grade = dr["GradeId"].ToString();

                usr.MonthlyGross= dr["MonthlyGross"].ToString();
                usr.GrossEarning = dr["GrossEarning"].ToString();
                usr.NetSalary = dr["NetSalary"].ToString();
                usr.MonthlyDeduction = dr["GrossDeduction"].ToString();
                usr.Location = dr["PermAddress"].ToString();
                usr.School = dr["School"].ToString();
                 usr.Address = dr["Address"].ToString();
                      //usr.City = dr["City"].ToString();
                 usr.SchoolCode = dr["StateName"].ToString();
               usr.District = dr["District"].ToString();
                usr.Pincode = Int64.Parse(dr["Pincode"].ToString());

            }

            return usr;


        }



        [System.Web.Http.Route("api/PayrollAPI/getSalarySlipStrcture")]
        [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails[] getSalarySlipStrcture(EmployeeSalaryDerails ed)
        {
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();
            sqlHelper obj = new sqlHelper();
            string[] cols = { "@Id", "@gradeId", "@employeeId", "@Month" };
            object[] vals = {ed.Id,ed.grade,ed.EmpId,ed.Month };
            DataTable dt = obj.sp_GetDataTable("SalarySlipGenerate", cols, vals);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
                    usr.Desc = dr["description"].ToString();
                    usr.Rate = dr["Rate"].ToString();

                    usr.arrear = dr["ArrearAmt"].ToString();
                    usr.Earning = dr["Earning"].ToString();
                    usr.totalAmt = dr["TotalAmt"].ToString();

                    list.Add(usr);
                }

            }
            return list.ToArray();

        }


        




    }
}
