using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace schoolERP_BLL
{

    public class LeaveManagement
    {
        //SCHOOLERPEntities db = new SCHOOLERPEntities();


        public static string saveLeaveTypeDetails(leaveType leave)
        {
            sqlHelper obj = new sqlHelper();
            string aa = obj.ExecuteScaler("");

            string[] cols = { "LeaveName", "Status", "SchoolID" };
            object[] vals = { leave.Name, leave.Status, leave.SchoolID };
            if (string.IsNullOrEmpty(leave.Id))
            {
                string exists = obj.ExecuteScaler("select LeaveName from tblLeaveType where LeaveName='" + leave.Name + "' and SchoolID='" + leave.SchoolID + "' and IsDeleted is null");
                if (exists == leave.Name)
                {
                    return "Leave Type" + leave.Name + " already Exists";
                }
                else
                {
                    obj.insertValIntoTable("tblLeaveType", cols, vals);
                    return "Leave Type Added Successfully";
                }
            }
            else
            {
                obj.updateValIntoTable("tblLeaveType", cols, vals, "LeaveId", leave.Id);
                return "Leave Type Updated Successfully"; ;
            }

        }
        //////////////////add Driver////////////////////
        public static string saveDriverDetails(DriverDetailss vd)
        {

            // string SchoolID = obj.ExecuteScaler("select SchoolID from tblEmployee where UserID='" + emp.Extra9 + "' and IsDeleted is null");

            sqlHelper obj = new sqlHelper();
            DateTime DOBdriver = Convert.ToDateTime(vd.DOB);
            string[] cols = { "VehicleId", "DriveId", "LicenseNo", "LicenseExpDate", "DateCreated", "SchoolID", "IsActive" };
            object[] vals = { vd.VehicleNumber, vd.Name, vd.LicenseNo, vd.LicenseExpiryDate, DateTime.Now, vd.SchoolID, vd.IsActive };



            if (vd.Id == 0)
            {
                string exists = obj.ExecuteScaler("SELECT * FROM tblTransportDriver where DriveId='" + vd.Name + "' and SchoolID ='" + vd.SchoolID + "' and IsActive=1 and IsDeleted is null ");
                string exists2 = obj.ExecuteScaler("select VehicleId from tblTransportDriver  where VehicleId='" + vd.VehicleNumber + "' and SchoolID='" + vd.SchoolID + "' and IsDeleted is null and IsActive=1 ");
                if (exists != null)
                {
                    return "Driver already Exists";
                }
                else if (exists2 != null)
                {
                    return "Vehicle already Exists ,please Check";
                }
                else
                {
                    obj.insertValIntoTable("tblTransportDriver", cols, vals);
                    return "Driver Inserted Successfully";
                }

            }
            else
            {
                string exists = obj.ExecuteScaler("select * from tblTransportDriver where  DriveId='" + vd.Name + "' and SchoolID ='" + vd.SchoolID + "' and id<>'" + vd.Id + "' and IsActive=1 and IsDeleted is null ");
                string exists2 = obj.ExecuteScaler("select VehicleId from tblTransportDriver  where VehicleId='" + vd.VehicleNumber + "' and SchoolID='" + vd.SchoolID + "' and id<>'" + vd.Id + "' and IsDeleted is null and IsActive=1 ");
                if (exists != null)
                {
                    return "Driver already Exists";
                }
                else if (exists2 != null)
                {
                    return "Vehicle already Exists";
                }

                else
                {
                    obj.updateValIntoTable("tblTransportDriver", cols, vals, "Id", vd.Id);
                    return "Driver Updated Successfully";
                }

            }


        }
        //////////////////VehicleDetails////////////////
        public static string saveVehicleDetails(VehicleDetails vd)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "VehicalNo ", "TotalSeats ", "AllowedSeats ", "OwnershipType ", "InsuranceDate ", "PollutionDate ", "TrackNumber ", "DateCreated ", "SchoolID", "FitnessExpiryDate", "IsActive" };
            object[] vals = { vd.VehicleNumber, vd.TotalSeats, vd.AllowedSeats, vd.OwnershipType, vd.InsuranceExpire, vd.PolutionExpire, vd.TrackNo, DateTime.Now, vd.SchoolID, vd.FitnessExpiry, vd.IsActive };
            if (vd.Id == 0)
            {
                string check = obj.ExecuteScaler("select VehicalNo from tblTransportVehicalDetails where VehicalNo='" + vd.VehicleNumber + "' and SchoolID='" + vd.SchoolID + "' and IsDeleted is null and IsActive=1");
                string check2 = obj.ExecuteScaler(" select VehicalNo from tblTransportVehicalDetails where TrackNumber='" + vd.TrackNo + "' and SchoolID='" + vd.SchoolID + "' and IsDeleted IS NULL and IsActive=1");
                if (check != null)
                {
                    return "Document" + vd.VehicleNumber + " VehicalNo already Exist";
                }
                else if (check2 != null)
                {
                    return "Selected Gps Device Already Assigned to Vehicle :" + check2;
                }
                else
                {
                    obj.insertValIntoTable("tblTransportVehicalDetails", cols, vals);
                    return "Vehicle Inserted Successfully";

                }
            }
            else
            {
                string check = obj.ExecuteScaler("select VehicalNo from tblTransportVehicalDetails where VehicalNo='" + vd.VehicleNumber + "' and SchoolID='" + vd.SchoolID + "' and id<>'" + vd.Id + "' and IsDeleted is null and IsActive=1");
                string check2 = obj.ExecuteScaler("select VehicalNo from tblTransportVehicalDetails where TrackNumber='" + vd.TrackNo + "' and SchoolID='" + vd.SchoolID + "' and IsDeleted IS NULL and id<>'" + vd.Id + "' and IsActive=1");
                if (check != null)
                {
                    return "Document" + vd.VehicleNumber + " VehicalNo already Exist";
                }
                else if (check2 != null)
                {
                    return "Selected Gps Device Already Assigned to Vehicle :" + check2;
                }
                else
                {
                    obj.updateValIntoTable("tblTransportVehicalDetails", cols, vals, "Id", vd.Id);
                    return "Vehicle Updated Successfully";

                }

            }

        }

        /////////////////////////DELETE Vehicle//////////////////////
        public static bool deleteVehicleById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                //   SqlCommand cmd = new SqlCommand("delete from tblTransportVehicalDetails where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblTransportVehicalDetails set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "',IsActive=0 where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }

        }






        ///////////////////////////////////////////////////addDestination///
        public static string saveDestinationDetails(DestinationDetails vd)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "RouteId", "PickAndDrop  ", "StopTime", "DateCreated", "SchoolID " };
            object[] vals = { vd.Route, vd.Pickdrop, vd.StopTime, DateTime.Now, vd.SchoolID };
            if (vd.Id == 0)
            {
                obj.insertValIntoTable("tblRoteDestination", cols, vals);
                return "DestinationDetails Inserted Successfully";
            }
            else
            {
                obj.updateValIntoTable("tblRoteDestination", cols, vals, "Id", vd.Id);
                return "DestinationDetails Updated Successfully";
            }

        }

        ///////////////saveRouteDetails//////////////
        public static string saveRouteDetails(RouteDetails vd)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "VehicleId", "RouteCode ", "StartPlace", "StartPlaceLatt ", "StartPlaceLongt", "EndPlace", "EndPlaceLatt", "EndPlaceLongt", "IsActive", "DateCreated", "SchoolID" };
            object[] vals = { vd.VehicleNumber, vd.RouteCode, vd.StartPlace, vd.StartPlaceLatt, vd.StartPlaceLongt, vd.EndPlace, vd.EndPlaceLatt, vd.EndPlaceLongt, vd.IsActive, DateTime.Now, vd.SchoolID };



            if (vd.Id == 0)
            {
                string check = obj.ExecuteScaler("select VehicleId from tblTransRoute where  VehicleId='" + vd.VehicleNumber + "' and SchoolID='" + vd.SchoolID + "' and IsActive=1 and IsDeleted=null");

                if (check != null)
                {
                    return "Route Already exist for Selected Vehicle";
                }

                else
                {
                    obj.insertValIntoTable("tblTransRoute", cols, vals);
                    return "RouteDetails Inserted Successfully";
                }

            }
            else
            {
                string vehicle = obj.ExecuteScaler("select VehicleId from tblTransRoute where SchoolID='" + vd.SchoolID + "' and VehicleId='" + vd.VehicleNumber + "' and IsActive=1 and IsDeleted=null and id<>'" + vd.Id + "'");
                if (vehicle != null)
                {
                    return "Route Already exist for Selected Vehicle";
                }
                else
                {
                    obj.updateValIntoTable("tblTransRoute", cols, vals, "Id", vd.Id);
                    return "RouteDetails Updated Successfully";
                }

            }

        }

        public static bool deleteLeaveTypeById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblLeaveType where LeaveId=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblLeaveType set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where LeaveId=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string saveLeaveDetails(leaveDetails leave)
        {


            //string[] desg = leave.Designation;
            //for(int i = 0;i< desg.Length;i++)
            //{
            sqlHelper obj = new sqlHelper();
            string academicYearStartDate = obj.ExecuteScaler("select DateFrom  from tblAcademicYear where ID='" + leave.AcademicYear + "'");
            if (academicYearStartDate != null)
            {
                leave.StartDate =/* "Convert(datetime,'"+*/ academicYearStartDate/*+"',103)"*/;
            }
            string academicYearEndDate = obj.ExecuteScaler("select DateTo  from tblAcademicYear where ID='" + leave.AcademicYear + "'");
            if (academicYearEndDate != null)
            {
                leave.EndDate = /*"Convert(datetime, '"+*/academicYearEndDate/*+"', 103)"*/;
            }

            DateTime StD = Convert.ToDateTime(leave.StartDate);
            DateTime EnD = Convert.ToDateTime(leave.EndDate);

            string[] cols = { "LeaveCategory", "leaveDesgination", "leaveAssgin", "StartDate", "EndDate", "Status", "SchoolID" };
            object[] vals = { leave.leaveCategory, leave.Designation, leave.leaveAssign, StD, EnD, leave.Status, leave.SchoolID };
            if (string.IsNullOrEmpty(leave.Id))
            {
                //var feestru = db.tbll.Where(c => c.FeeStructureID == feestruid).ToList();

                DataTable result = obj.getDataTable(" select leaveCategory from tblLeaveDetails where leaveCategory='" + leave.leaveCategory + "' and leaveDesgination='" + leave.Designation + "' and SchoolID='" + leave.SchoolID + "' and IsDeleted is null");
                //foreach (DataRow dr1 in result.Rows)
                //{

                //}

                    if (result.Rows.Count > 0)
                {
                    // string exists = obj.ExecuteScaler(" select leaveCategory from tblLeaveDetails where leaveCategory='" + leave.leaveCategory + "' and leaveDesgination='" + leave.Designation + "' and SchoolID='" + leave.SchoolID + "' and IsDeleted is null");

                    // if (exists == leave.leaveCategory)
                    // {
                    return "Leave " + leave.Name + " already Exists";
                    //}

                }
                else
                {
                    obj.insertValIntoTable("tblLeaveDetails", cols, vals);
                    return "Leave Assign Successfully";
                }
            }
            else
            {

                string exists = obj.ExecuteScaler(" select leaveCategory from tblLeaveDetails where leaveCategory=" + leave.leaveCategory + " and leaveDesgination='" + leave.Designation + "' and SchoolID='" + leave.SchoolID + "' and Id !='" + leave.Id + "' and IsDeleted is null");
                if (exists == leave.leaveCategory)
                {
                    return "Leave" + leave.Name + "already Exists";
                }
                else
                {
                    obj.updateValIntoTable("tblLeaveDetails", cols, vals, "Id", leave.Id);
                    return "Leave Assign Updated Successfully";
                }

            }
        }
        //if (string.IsNullOrEmpty(leave.Id))
        //{
        //    return "Leave Assign Inserted Successfully";
        //}
        //else
        //{
        //    return "Leave Assign Updated Successfully";
        //}



        public static bool deleteLeaveDetailsById(string id)
        {
            try
            {

                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                // SqlCommand cmd = new SqlCommand("delete from tblLeaveDetails where Id=" + id, con);
                SqlCommand cmd = new SqlCommand("Update tblLeaveDetails set IsDeleted=1 ,Deleted_on='" + DateTime.Now + "' where Id=" + id, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string saveLeaveRequestByEmployyee(EmployeeEm emp)
        {
            sqlHelper obj = new sqlHelper();
            string SchoolID = obj.ExecuteScaler("select SchoolID from tblEmployee where UserID='" + emp.Extra9 + "' and IsDeleted is null");
            string status = obj.ExecuteScaler(" select StatusID from tblstatus where Status='Pending' and stStatus=1");
            DateTime StD = Convert.ToDateTime(emp.Extra5);
            DateTime EnD = Convert.ToDateTime(emp.Extra6);


            string[] cols = { "EmployeeID", "Department", "Designation", "LeaveType", "StatDate", "EndDate", "TakeLeaveCount", "Reason", "LeavStatus", "SchoolID" };
            object[] vals = { emp.Id, emp.Department, emp.Designation, emp.Extra4, StD, EnD, emp.Extra7, emp.Extra8, status, SchoolID };
            obj.insertValIntoTable("tblemployeeLeaveRequest", cols, vals);
            return "Leave Request Send Successfully";
        }




        public static string approvedUnapproveLeaveRequest(EmployeeEm emp)
        {
            sqlHelper obj = new sqlHelper();
            string status = obj.ExecuteScaler("select Status from tblstatus where StatusID=" + emp.Extra5);

            string[] cols = { "LeavStatus" };
            object[] vals = { emp.Extra5 };
            obj.updateValIntoTable("tblemployeeLeaveRequest", cols, vals, "Id", emp.Id);
            return "Leave Request " + status + " Successfully";
        }





    }
    public class leaveType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string aStatus { get; set; }
        public string Extra10 { get; set; }
        public string School { get; set; }

        public int SchoolID { get; set; }
    }


    public class leaveDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string leaveCategory { get; set; }
        public string leaveCategoryId { get; set; }
        public string Status { get; set; }
        public bool Status2 { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string leaveAssign { get; set; }
        public string Designation { get; set; }
        public string Desig { get; set; }
        public string DesigId { get; set; }
        public string AcademicYear { get; set; }
        public string Extra10 { get; set; }

        public string School { get; set; }
        public int SchoolID { get; set; }

        public string StudentID { get; set; }
    }
}
