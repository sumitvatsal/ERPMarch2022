using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using schoolERP_BLL;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;

using System.Security.Cryptography;
using System.Net.Mail;
using System.Net.Configuration;


namespace schoolERP_BLL
{
    public class StudnetAttendenceDetailss
    {

        public static string saveStudentAttendence(StudentAttendenceDetails[] attendence)
        {
            sqlHelper obj = new sqlHelper();
            foreach (var emp in attendence)
            {
                string exits = obj.ExecuteScaler("select StudentId from tblStudentAttendence where StudentId=" + emp.StudentId + " and  AttendenceDate='" + emp.AttendenceDate + "' and SchoolID ='"+emp.SchoolID+"' ");

                if (exits == emp.StudentId)
                {
                    //string[] cols1 = { "AttendenceType", "AttendenceDate", "TeacherId", "ClassId", "SectionId" };
                    //object[] vals1 = { emp.AttendenceType, emp.AttendenceDate, emp.EmployeeId, emp.ClassID, emp.SectionId };
                    //obj.updateValIntoTable("tblStudentAttendence", cols1, vals1, "StudentId", emp.StudentId);

                    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    string query = @"update tblStudentAttendence set AttendenceType='" + emp.AttendenceType + "',AttendenceDate='"
                        + emp.AttendenceDate + "',TeacherId='" + emp.EmployeeId + "',ClassId='" + emp.ClassID + "',SectionId='" + emp.SectionId
                        + "'  where StudentId=" + emp.StudentId + " and AttendenceDate='" + emp.AttendenceDate + "' and SchoolID='"+emp.SchoolID+"'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    string[] cols = { "StudentId", "AttendenceType", "AttendenceDate", "TeacherId", "ClassId", "SectionId" ,"SchoolID"};
                    object[] vals = { emp.StudentId, emp.AttendenceType, emp.AttendenceDate, emp.EmployeeId, emp.ClassID, emp.SectionId,emp.SchoolID };
                    obj.insertValIntoTable("tblStudentAttendence", cols, vals);

         
                }
            }
            return "Attendence has been Taken Successfully Thank u!";

             
        }

    }
    public class StudentAttendenceDetails
    {
        public int Idd { get; set; }
        public string Id { get; set; }
        public string ClassID { get; set; }
        public string SectionId { get; set; }
        public string StudentId { get; set; }

        public string AttendenceDate { get; set; }
        public string AttendenceType { get; set; }
        public string EmployeeId { get; set; }
        public string LeaveType { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string LeaveStatus { get; set; }
        public string AttendenceMonth { get; set; }
       
        public string AttendenceYear { get; set; }
        public int AttendenceMonth1 { get; set; }

        public int AttendenceYear1 { get; set; }
        public int AttendenceDay1 { get; set; }
        public DateTime AttendenceRealDate { get; set; }
    }
    public class datess
    {
      public  DateTime betdates { get; set; }
        public DateTime finalbetdates { get; set; }
    }
    public class StudentAttendenceDetailsAPP1
    {
        public int AttendenceDay { get; set; }
        public string AttendenceType { get; set; }

        public string AttendenceDate { get; set; }
        public DateTime AttendenceRealDate { get; set; }
    }

    public class StudentAttendenceDetailsAPP
    {
     
        public List<STUAttendence> Attendence { get; set; }

    }
    public class STUAttendence
    {

        public int AttendenceDay { get; set; }
        public string AttendenceType { get; set; }

        public string AttendenceDate { get; set; }

    }
    //public class DEMOATTENDENCE
    //{
        
    //    public List<STUAttendence> Attendence { get; set; }

    //}
}
