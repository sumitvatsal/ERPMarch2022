using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schoolERP_BLL
{
    public class EmployeeAttendenceDetails
    {
        public static string saveEmployeeAttendence(takeAttedencDetails[] attendence)
        {
            sqlHelper obj = new sqlHelper();
            foreach (var emp in attendence)
            {
                string exits = obj.ExecuteScaler("select EmployeeId from tblEmployeeAttendence where SchoolID='"+emp.SchoolID+"' and EmployeeId=" + emp.empID + " and  AttendenceDate='" + emp.DateCurrent + "' ");
         
                if (exits == emp.empID)
                {
                    string exits2 = obj.ExecuteScaler("select EmployeeId from tblEmployeeAttendence where SchoolID='" + emp.SchoolID + "' and EmployeeId=" + emp.empID + " and  AttendenceDate='" + emp.DateCurrent + "' and AttendenceType!='"+ emp.AttendenceType + "' ");
                    if (exits2==emp.empID)
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
                    }
                 



                    //string[] cols1 = { "DesigId", "DepId", "LeaveType", "AttendenceType", "AttendenceDate" };
                    //object[] vals1 = {  emp.DesigId, emp.DesigId, emp.LeaveType, emp.AttendenceType, emp.DateCurrent };
                    //obj.updateValIntoTable("tblEmployeeAttendence", cols1, vals1, "EmployeeId", emp.empID);
                 }
                else
                {
                    string[] cols = { "EmployeeId", "DesigId", "DepId", "LeaveType", "AttendenceType", "AttendenceDate","SchoolID", "IsBiometric" };
                    object[] vals = { emp.empID, emp.DesigId, emp.DepId, emp.LeaveType, emp.AttendenceType, emp.DateCurrent,emp.SchoolID,false};
                    obj.insertValIntoTable("tblEmployeeAttendence", cols, vals);
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

    public class takeAttedencDetails {
        public string Id { get; set; }
        public string empID { get; set; }
        public string DesigId { get; set; }
        public string DepId { get; set; }
        public string LeaveType { get; set; }
        public string AttendenceType { get; set; }
        public string AttendenceDate { get; set; }
        public string DateCurrent { get; set; }
        public string IdBioMetric { get; set; }
        public string Time { get; set; }
        public string TimeOut { get; set; }
        public string SchoolID { get; set; }


    }

    public class attendenceReporst {
        public string empID { get; set; }
        public string studentId { get; set; }
        public string day { get; set; }
        public string Month { get; set; }
    }
}
