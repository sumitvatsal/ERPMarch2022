using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolERP_BLL;
using System.Data;
using System.Globalization;

namespace SchoolErp.Controllers.WebApi
{
    public class LeaveAPIController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();



        [System.Web.Http.Route("api/LeaveAPI/saveLeaveTypeDetails")]
        [System.Web.Http.HttpPost]
        public string saveLeaveTypeDetails(leaveType leavetype)
        {
            string b = LeaveManagement.saveLeaveTypeDetails(leavetype);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }
        [System.Web.Http.Route("api/LeaveAPI/getAllLeaveTypeBySchool")]
        [System.Web.Http.HttpPost]
        public leaveType[] getAllLeaveTypeBySchool(List<string> aa)
        {
          
            int SchoolID = Convert.ToInt32(aa[0]);

            List<leaveType> list = new List<leaveType>();
            sqlHelper obj = new sqlHelper();

                DataTable dt = obj.getDataTable(@"select s.SchoolID,sc.School,s.LeaveId,s.LeaveName,s.Status from tblLeaveType s
 inner join tblSchoolDetails sc on sc.ID= s.SchoolID
where s.SchoolID='" + SchoolID + "' and  s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    // usr.Status = bool.Parse(dr["Status"].ToString());
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
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/LeaveAPI/getAllLeaveType")]
        [System.Web.Http.HttpPost]
        public leaveType[] getAllLeaveType(List <string > aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32 (aa[1]);
           
            List<leaveType> list = new List<leaveType>();
            sqlHelper obj = new sqlHelper();
          
             if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select s.SchoolID,sc.School,LeaveId,LeaveName,Status from tblLeaveType s inner join tblSchoolDetails sc on sc.ID= s.SchoolID where s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    // usr.Status = bool.Parse(dr["Status"].ToString());
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
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
             else
            {
                DataTable dt = obj.getDataTable(@"select s.SchoolID,sc.School,s.LeaveId,s.LeaveName,s.Status from tblLeaveType s
 inner join tblSchoolDetails sc on sc.ID= s.SchoolID
 join tblEmployee em on em.SchoolID=s.SchoolID
  where em.UserID='" + loginuser + "' and em.IsDeleted is null and s.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    // usr.Status = bool.Parse(dr["Status"].ToString());
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
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/LeaveAPI/deleteLeaveTypeById")]
        [System.Web.Http.HttpPost]
        public string deleteLeaveTypeById(string id)
        {
            bool b = LeaveManagement.deleteLeaveTypeById(id);
            if (b)
            {
                return "Leave Type Deleted Successfully";
            }
            else
            {
                return "Leave Type Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/LeaveAPI/getAllActiveLeaveTypebySchool")]
        [System.Web.Http.HttpPost]
        public leaveType[] getAllActiveLeaveTypebySchool(List<string> aa)
        {

            int SchoolID = Convert.ToInt32(aa[0]);
            List<leaveType> list = new List<leaveType>();
            sqlHelper obj = new sqlHelper();

           
                DataTable dt = obj.getDataTable(@"select LeaveId,LeaveName,Status from tblLeaveType where Status=1 and SchoolID='"+ SchoolID + "' and IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    list.Add(usr);
                }
           
               
            

            return list.ToArray();
        }

        [System.Web.Http.Route("api/LeaveAPI/getAllActiveLeaveType")]
        [System.Web.Http.HttpPost]
        public leaveType[] getAllActiveLeaveType(List <string > aa)
        {
            
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32( aa[1]);
            List<leaveType> list = new List<leaveType>();
            sqlHelper obj = new sqlHelper();
           
            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select LeaveId,LeaveName,Status from tblLeaveType where Status=1 and IsDeleted is null ");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select LT.LeaveId,LT.LeaveName,LT.Status from tblLeaveType LT 
											 left outer join tblEmployee em on em.SchoolID=LT.SchoolID
											 where em.UserID='" + loginuser + "' and em.IsDeleted is null and LT.Status=1");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveType usr = new leaveType();
                    usr.Id = dr["LeaveId"].ToString();
                    usr.Name = dr["LeaveName"].ToString();


                    list.Add(usr);
                }
            }

            return list.ToArray();
        }


        [System.Web.Http.Route("api/LeaveAPI/saveLeaveDetails")]
        [System.Web.Http.HttpPost]
        public string saveLeaveDetails(leaveDetails leavedeta)
        {
            string b =  LeaveManagement.saveLeaveDetails(leavedeta);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }
        /////////Vehicle/////////////


        [System.Web.Http.Route("api/TransportApi/saveDestinationDetails")]
        [System.Web.Http.HttpPost]

        public string saveDestinationDetails(DestinationDetails vd)
        {
            string b = LeaveManagement.saveDestinationDetails(vd);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }


        [System.Web.Http.Route("api/TransportApi/saveVehicleDetails")]
        [System.Web.Http.HttpPost]

        public string saveVehicleDetails(VehicleDetails vd)
        {
            string b = LeaveManagement.saveVehicleDetails(vd);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }

        [System.Web.Http.Route("api/TransportApi/deleteVehicleById")]
        [System.Web.Http.HttpPost]
        public string deleteVehicleById(string id)
        {
            bool b = LeaveManagement.deleteVehicleById(id);
            if (b)
            {
                return "Vehicle Details  Deleted Successfully";
            }
            else
            {
                return "Vehicle Details not Deleted ";
            }

        }
        //////////////////////

        [System.Web.Http.Route("api/TransportApi/saveRouteDetails")]
        [System.Web.Http.HttpPost]

        public string saveRouteDetails(RouteDetails vd)
        {
            string b = LeaveManagement.saveRouteDetails(vd);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }


        [System.Web.Http.Route("api/LeaveAPI/getAllLeaveDetails")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getAllLeaveDetails(List <string > aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32( aa[1]);
            List<leaveDetails> list = new List<leaveDetails>();
            sqlHelper obj = new sqlHelper();
          
            if (typeuser == 2)
            {

                DataTable dt = obj.getDataTable(@"select sc.School,ld.SchoolID, lt.LeaveName,d.Designation,ld.* from tblLeaveDetails ld 
                                            left outer join tblLeaveType lt on lt.LeaveId=ld.LeaveCategory
                                            left outer join tblDesignation d on d.DesigID=ld.leaveDesgination
											left outer join tblschoolDetails sc on ld.SchoolID=sc.ID where ld.IsDeleted is null");
                              
                foreach (DataRow dr in dt.Rows)
                {
                    leaveDetails usr = new leaveDetails();
                    usr.Id = dr["Id"].ToString();
                    usr.leaveCategory = dr["LeaveName"].ToString();
                    usr.leaveCategoryId = dr["LeaveCategory"].ToString();

                    usr.Desig = dr["Designation"].ToString();
                    usr.DesigId = dr["leaveDesgination"].ToString();
                    usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.leaveAssign = dr["leaveAssgin"].ToString();
                    usr.Status2 = bool.Parse(dr["Status"].ToString());
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
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@" select sc.School,ld.SchoolID, lt.LeaveName,d.Designation,ld.* from tblLeaveDetails ld 
                                            left outer join tblLeaveType lt on lt.LeaveId=ld.LeaveCategory
                                            left outer join tblDesignation d on d.DesigID=ld.leaveDesgination
											left outer join tblschoolDetails sc on ld.SchoolID=sc.ID 
											left outer join tblemployee em on em.SchoolID=ld.SchoolID
											where em.UserID='" + loginuser + "' and em.IsDeleted is null and ld.IsDeleted is null");
                foreach (DataRow dr in dt.Rows)
                {
                    leaveDetails usr = new leaveDetails();
                    usr.Id = dr["Id"].ToString();
                    usr.leaveCategory = dr["LeaveName"].ToString();
                    usr.leaveCategoryId = dr["LeaveCategory"].ToString();

                    usr.Desig = dr["Designation"].ToString();
                    usr.DesigId = dr["leaveDesgination"].ToString();
                    usr.StartDate = ((DateTime)dr["StartDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.EndDate = ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.leaveAssign = dr["leaveAssgin"].ToString();
                    usr.Status2 = bool.Parse(dr["Status"].ToString());
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
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }

            return list.ToArray();
        }


        [System.Web.Http.Route("api/LeaveAPI/deleteLeaveDetailsById")]
        [System.Web.Http.HttpPost]
        public string deleteLeaveDetailsById(string id)
        {
            bool b = LeaveManagement.deleteLeaveDetailsById(id);
            if (b)
            {
                return "Leave Assign Deleted Successfully";
            }
            else
            {
                return "Leave Assign Not Deleted Successfully";
            }

        }


       

        [System.Web.Http.Route("api/LeaveAPI/fetchEmployeeTakeAssgnLeave")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] fetchEmployeeTakeAssgnLeave(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
          
            List<EmployeeEm> list = new List<EmployeeEm>();
            string SchoolID = obj.ExecuteScaler( "select SchoolID from tblEmployee where UserID='"+ em.Extra5 + "' and IsDeleted is null");
          
            string[] cols = {"@desgId","@EmpId","@leaveType", "@School" };
            object[] vals={em.Designation,em.Id,em.Extra4,SchoolID};

            DataTable dt = obj.sp_GetDataTable("sp_fetchEmployeeTakeAssgnLeave", cols, vals);
            //    SqlDataReader dr = obj.GetReader(@" select * from tblUser where ID=" + id[0]);


            EmployeeEm usr = new EmployeeEm();
            if (dt.Rows.Count>0)
            {

                usr.Extra1 = dt.Rows[0]["fldType"].ToString();
                usr.Extra2 = dt.Rows[0]["leaveAssign"].ToString();
                usr.Extra3 = dt.Rows[0]["takeLeave"].ToString();
                usr.Extra4 = dt.Rows[0]["remainingLeave"].ToString();
                list.Add(usr);
            }

            return list.ToArray();

        }


        [System.Web.Http.Route("api/LeaveAPI/saveLeaveRequestByEmployyee")]
        [System.Web.Http.HttpPost]
        public string saveLeaveRequestByEmployyee([FromBody]EmployeeEm emp)
        {
            string b = LeaveManagement.saveLeaveRequestByEmployyee(emp);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }
        }


        [System.Web.Http.Route("api/LeaveAPI/checkDateAvalabiltyInLeave")]
        [System.Web.Http.HttpPost]
        public string checkDateAvalabiltyInLeave(EmployeeEm emp)
        {
             
            string startDate = ((DateTime)Convert.ToDateTime(emp.Extra5)).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
           string endDate= ((DateTime)Convert.ToDateTime(emp.Extra6)).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string SchoolID = emp.Extra9;
            sqlHelper obj = new sqlHelper();
            string[] cols = {"@startdate","@enddate","@leaveType","@desig","@SchoolID" };
            object[] vals = { startDate, endDate,emp.Extra7,emp.Extra8, SchoolID };
            DataTable dt = obj.sp_GetDataTable("Sp_checkLeaveYearforEmployee", cols, vals);
            if (dt.Rows.Count > 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
         
           
        }


        [System.Web.Http.Route("api/LeaveAPI/approvedUnapproveLeaveRequest")]
        [System.Web.Http.HttpPost]
        public string approvedUnapproveLeaveRequest(EmployeeEm emp)
        {
            string b = LeaveManagement.approvedUnapproveLeaveRequest(emp);
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


