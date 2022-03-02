using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace schoolERP_BLL
{
    public class PayrollDetails
    {
        public static string saveSalaryHead(saleryHead sal)
        {

            sqlHelper obj = new sqlHelper();
            string[] cols = { "SalaryType", "Name", "Code", "Status","SchoolID" };
            string[] vals = {sal.Type,sal.Name,sal.Code,sal.Status,sal.SchoolID};
            if (string.IsNullOrEmpty(sal.Id))
            {
                obj.insertValIntoTable("tblSalaryHead", cols, vals);
                return "Salry Head Added Successfully";
            }
            else
            {
                obj.updateValIntoTable("tblSalaryHead", cols, vals, "Id", sal.Id);
                return "Salry Head Updated Successfully"; ;
            }

 

        }
        public static bool deletesalaryHeadById(string id)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblSalaryHead where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblSalaryHead set IsDeleted=1 ,Deleted_on=convert(datetime,'" + DateTime.Now + "',103) where Id=" + id+"", con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string saveGradeDetails(GradeDetails grade)
        {

            sqlHelper obj = new sqlHelper();
            string[] cols = { "GradeName", "DesignationId", "Status","SchoolID"};
            string[] vals = { grade.GradeName, grade.Designation,grade.Status,grade.SchoolID };

            
            if (string.IsNullOrEmpty(grade.Id))
            {
                string exists = obj.ExecuteScaler(" select GradeName from tblMasterGrade where GradeName='" + grade.GradeName + "' and SchoolID='"+ grade.SchoolID + "'");
                if (exists == grade.GradeName)
                {
                    return "Grade already Exists";
                }
                else
                {
                    long Id = 0;
                    obj.insertValIntoTableGetId("tblMasterGrade", cols, vals,ref Id);
                    return Id.ToString();
                }
               
            }
            else
            {
                obj.updateValIntoTable("tblMasterGrade", cols, vals, "Id", grade.Id);
                return grade.Id;
            }



        }


        public static string saveGradesalaryAssignDetails(GradeDetails[] grade)
        {

            sqlHelper obj = new sqlHelper();

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            int a = 0;
            string b = "";
            string n = obj.ExecuteScaler("select * from tblGradeAssignSalaryHead where GradeId='" + grade[0].Id + "'");
            if (!string.IsNullOrEmpty(n))
            {
                a++;
            }
            SqlCommand cmd = new SqlCommand("delete from tblGradeAssignSalaryHead where GradeId=" + grade[0].Id, con);
          
                cmd.ExecuteNonQuery();
           
            con.Close();
            foreach (var d in grade)
            {
                string[] cols = { "HeadId", "GradeId", "Amount", "Type", "headType","SchoolID" };
                string[] vals = { d.HeadId, d.Id, d.HeadAmount,d.Type,d.HeadType,d.SchoolID};
                obj.insertValIntoTable("tblGradeAssignSalaryHead", cols, vals);
         }
            //return "Grade Assgin Sucessfully";
            if (a==0)
            {
                b = " Grade Assgin Sucessfully";

            }
            else
            {
                b = " Grade Updated Sucessfully";

            }
            return b;


        }

        public static string saveEmployeeGrade(GradeDetails grade)
        {
            string[] emp = grade.EmployeeId.Split(',');
            for (int i = 0; i < emp.Length; i++)
            {
                if (emp[i] != "")
                {
                    sqlHelper obj = new sqlHelper();
                    string[] cols = { "DesignationId", "EmployeeId", "GradeId", "Status","SchoolID" };
                    object[] vals = { grade.Designation, emp[i], grade.GradeId, grade.Status ,grade.SchoolID};
                    if (string.IsNullOrEmpty(grade.Id))
                    {
                        string exists = obj.ExecuteScaler("select EmployeeId from tblEmployeeAccountInformation where DesignationId=" + grade.Designation + " and EmployeeId=" + emp[i] + " and GradeId=" + grade.GradeId);
                        if (exists == emp[i])
                        {
                            return "This Type Grade already Exists";
                        }
                        string employeeIdd = obj.ExecuteScaler("select EmployeeId from tblEmployeeAccountInformation where DesignationId=" + grade.Designation + " and EmployeeId=" + emp[i]);
                        if (string.IsNullOrEmpty(employeeIdd))
                        {
                            obj.insertValIntoTable("tblEmployeeAccountInformation", cols, vals);
                        }
                        else
                        {


                            //  obj.updateValIntoTable("tblEmployeeAccountInformation", cols, vals, "EmployeeId", grade.EmployeeId);
                            return "Already Assign Grade for This Employee";

                        }
                    }
                    else
                    {
                        obj.updateValIntoTable("tblEmployeeAccountInformation", cols, vals, "Id", grade.Id);

                    }
                }
            }
            if (string.IsNullOrEmpty(grade.Id))
            {
                return "Grade Assign  Successfully";
            }
            else
            {
                return "Grade Assign Updated Successfully";
            }


        }



        public static string saveEmployeeSalaryDetails(EmployeeSalaryDerails salary)
        {

            sqlHelper obj = new sqlHelper();
            string[] cols = { "DesignationId", "EmployeeId", "FromDate", "toDate", "GradeId",
            "MonthlyGross","MonthlyDeduction","GrossEarning","GrossDeduction","NoOfLeave",
                "NetSalary","totalDayInMonth","salayDayInMonth","Status","SchoolID" };
            string[] vals = { salary.Designation, salary.EmployeeName, salary.FromDate,salary.ToDate,salary.GradeName,
            salary.MonthlyGross,salary.MonthlyDeduction,salary.GrossEarning,salary.GrossDeduction,salary.NoOfLeave,
            salary.NetSalary,salary.TotalDayInMonth,salary.SalaryDays,salary.Status,salary.SchoolID};
            if (string.IsNullOrEmpty(salary.Id))
            {
                //string exists = obj.ExecuteScaler(" select GradeName from tblMasterGrade where GradeName='" + grade.GradeName + "'");
                //if (exists == grade.GradeName)
                //{
                //    return "Grade already Exists";
                //}
                //else
                //{
                    long Id = 0;
                    obj.insertValIntoTableGetId("tblEmployeeSalaryDetails", cols, vals, ref Id);
                    return Id.ToString();
                //}

            }
            else
            {
                obj.updateValIntoTable("tblEmployeeSalaryDetails", cols, vals, "Id", salary.Id);
                return salary.Id;
            }
           



        }



        public static string saveEmployeeSalaryDettailsStructure(EmployeeSalaryDerails[] salary)
        {
            try
            {


                sqlHelper obj = new sqlHelper();

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from tblEmployyeMonthSalaryStructure where fldId=" + salary[0].Id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                foreach (var d in salary)
                {
                    string[] cols = { "salaryDetailId", "HeadId", "HeadAmount", "HeadType", "Salrytpe" };
                    string[] vals = { d.Id, d.HeadId, d.Amount, d.Type, d.HeadType };
                    obj.insertValIntoTable("tblEmployyeMonthSalaryStructure", cols, vals);
                }



                return "Employee Salary Build Sucessfully";
            }
            catch(Exception ex)
            {
                throw ex;
            }

      


        }




    }
    public class saleryHead {

        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Statuss { get; set; }
        public string Extra10 { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
    }

    public class GradeDetails
    {

        public string Id { get; set; }
        public string GradeName { get; set; }
        public string Designation { get; set; }
        public string HeadId { get; set; }
        public string HeadName { get; set; }
        public string HeadAmount { get; set; }
        public string HeadType { get; set; }
        public string Status { get; set; }
        public string Extra10 { get; set; }
        public string Extra11 { get; set; }
        public string Type { get; set; }
        public string EmployeeId { get; set; }
        public string GradeId { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeeSalaryDerails
    {
        public string Id { get; set; }
        public string Designation { get; set; }
        public string EmployeeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NoOfLeave { get; set; }
        public string GradeName { get; set; }
        public string Status { get; set; }
        public string GradeId { get; set; }
        public string MonthlyGross { get; set; }
        public string MonthlyDeduction { get; set; }
        public string GrossEarning { get; set; }
        public string GrossDeduction { get; set; }
         public string NetSalary { get; set; }
        public string HeadId { get; set; }
        public string HeadName { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }
        public string HeadType { get; set; }
        public string ImageUpload { get; set; }
        
        public string TotalDayInMonth { get; set; }
        public string SalaryDays { get; set; }

        public string PfNo { get; set; }
        public string AccountNumber { get; set; }
        public string PaymentMode { get; set; }
        public string IfscCode { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string PanNo { get; set; }

        public string Dob { get; set; }
        public string JoiningDate { get; set; }

        public string EmployeeId { get; set; }
        public string adhaarNo { get; set; }

        public string esiNumber { get; set; }

        public string EmpId { get; set; }
        public string grade { get; set; }




        public string Desc { get; set; }
        public string Rate { get; set; }
        public string Earning { get; set; }
        public string arrear { get; set; }
        public string totalAmt { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string loginuser { get; set; }
        public int typeuser { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string SchoolCode { get; set; }
        public string District { get; set; }
        public Nullable<long> Pincode { get; set; }



    }
}
