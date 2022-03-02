using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolERP_BLL;
using SchoolErp.Models;
using System.IO;

using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net.Configuration;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;




namespace SchoolErp.Controllers.WebApi
{
    public class TransportApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        //[System.Web.Http.Route("api/TransportApi/saveVehicleDetails")]
        //[System.Web.Http.HttpPost]
        //public string saveVehicleDetails(VehicleDetails vd)
        //{
        //    if (vd.Id == 0)
        //    {
        //        tblTransportVehicalDetail tbl = new tblTransportVehicalDetail();
        //        tbl.VehicalNo = vd.VehicleNumber;
        //        tbl.TotalSeats = vd.TotalSeats;
        //        tbl.AllowedSeats = vd.AllowedSeats;
        //        tbl.OwnershipType = vd.OwnershipType;
        //        tbl.InsuranceDate = vd.InsuranceExpire;
        //        tbl.PollutionDate = vd.PolutionExpire;
        //        tbl.TrackNumber = vd.TrackNo;
        //        tbl.DateCreated = DateTime.Now;

        //        db.tblTransportVehicalDetails.Add(tbl);
        //        db.SaveChanges();
        //        return "Vehicle Inserted Successfully";
        //    }
        //    else
        //    {
        //        var result = db.tblTransportVehicalDetails.Where(s => s.Id == vd.Id).SingleOrDefault();
        //        result.VehicalNo = vd.VehicleNumber;
        //        result.TotalSeats = vd.TotalSeats;
        //        result.AllowedSeats = vd.AllowedSeats;
        //        result.OwnershipType = vd.OwnershipType;
        //        result.InsuranceDate = vd.InsuranceExpire;
        //        result.PollutionDate = vd.PolutionExpire;
        //        result.TrackNumber = vd.TrackNo;
        //        result.DateCreated = DateTime.Now;
        //        db.SaveChanges();
        //        return "Vehicle Updated Successfully";
        //    }


        //}

        [System.Web.Http.Route("api/SmsApi/saveDeviceAuthkeyDetails")]
        [System.Web.Http.HttpPost]
        public string saveDeviceAuthkeyDetails(SchoolSMSMASTER val)
        {
            try
            {
                int SchoolID = Convert.ToInt32(val.SchoolID);
                if (val.id == 0)
                {
                   

                    var getdata = db.tblSchoolSms.Where(x => x.SchoolID == SchoolID && x.active==true).FirstOrDefault();
                    if(getdata!=null)
                    {
                        return "One Of your Sms Credential Already Active";
                    }
                    else
                    {
                        tblSchoolSm aa = new tblSchoolSm();
                        aa.authKey = val.authKey;
                        aa.senderId = val.senderId;
                        aa.SchoolID = Convert.ToInt32(val.SchoolID);
                        aa.active = val.active;
                        db.tblSchoolSms.Add(aa);
                        db.SaveChanges();
                        return "School Sms Details Saved Successfully";
                    }
                
                }
                else
                {
                    var checkactive = db.tblSchoolSms.Where(x => x.SchoolID == SchoolID && x.active==true && x.id !=val.id).FirstOrDefault();
                    if (checkactive!=null)
                    {

                        return "One Of your Sms Credential Already Active";
                    }
                    else
                    {
                        var result = db.tblSchoolSms.Where(x => x.id == val.id).FirstOrDefault();
                        result.authKey = val.authKey;
                        result.senderId = val.senderId;
                        result.active = val.active;
                        result.SchoolID = Convert.ToInt32(val.SchoolID);
                        db.SaveChanges();
                        return "School Sms Details Updated Successfully";
                    }
                  
                }

            }
            catch
            {
                return "something Went Wrong TrY again";
            }


        }


        [System.Web.Http.Route("api/SmsApi/GetSchoolSmsDetailsforedit")]
        [System.Web.Http.HttpPost]
        public SchoolSMSMASTER GetSchoolSmsDetailsforedit(List<string> avi)
        {
            SchoolSMSMASTER qq = new SchoolSMSMASTER();
            try
            {
                int SchoolID = Convert.ToInt32(avi[1]);
                int id = Convert.ToInt32(avi[0]);
                var s = db.tblSchoolSms.Where(x => x.SchoolID == SchoolID && x.id == id).FirstOrDefault();
               
                if (s!=null)
                {
                    qq.id = s.id;
                    qq.senderId = s.senderId;
                    qq.authKey = s.authKey;
                    qq.SchoolID = Convert.ToString(s.SchoolID);
                    string Schoolname = db.tblSchoolDetails.SingleOrDefault(x => x.ID == s.SchoolID).School;
                    qq.SchoolName = Schoolname;
                    qq.active = Convert.ToBoolean(s.active);
                    if (qq.active == true)
                    {
                        qq.Statuss = "Active";
                    }
                    else
                    {
                        qq.Statuss = "DeActive";
                    }

                }
                  
                   
                   
                
            }
            catch  (Exception ex)
            {

            }
          

            return qq;

        }


        [System.Web.Http.Route("api/TransportApi/saveGpsDeviceDetails")]
        [System.Web.Http.HttpPost]
        public string saveGpsDeviceDetails(GpsDeviceRecord val)
        {
            try
            {
                if (val.ID==0)
                {
                    tblGpsDeviceRecord aa = new tblGpsDeviceRecord();
                    aa.GpsDeviceNO = val.GpsDeviceNO;
                    aa.MobileNo = val.MobileNo;
                    aa.SchoolID = Convert.ToInt32(val.SchoolID);
                    aa.IsActice = val.IsActice;
                    db.tblGpsDeviceRecords.Add(aa);
                    db.SaveChanges();
                    return "Gps Device Details Seved Successfully";
                }
                else
                {
                    var result = db.tblGpsDeviceRecords.Where(x => x.ID == val.ID).FirstOrDefault();
                    result.GpsDeviceNO = val.GpsDeviceNO;
                    result.MobileNo = val.MobileNo;
                    result.IsActice = val.IsActice;
                    result.SchoolID = Convert.ToInt32(val.SchoolID) ;
                    db.SaveChanges();
                    return "Gps Device Details Updated Successfully";
                }
               
            }
            catch
            {
                return "something Went Wrong TrY again";
            }


        }
        [System.Web.Http.Route("api/TransportApi/GetVehicleLocation")]
        [System.Web.Http.HttpPost]
        public GpsDeviceRecord[] GetVehicleLocation(List<string> AAA)
        {
            List<GpsDeviceRecord> list = new  List<GpsDeviceRecord>();
            

            return list.ToArray();
           // GetCurrentLocation



        }

        [System.Web.Http.Route("api/Smsapi/GetSMSDETAILS")]
        [System.Web.Http.HttpPost]
        public SchoolSMSMASTER[] GetSMSDETAILS(List<string> AAA)
        {
            List<SchoolSMSMASTER> list = new List<SchoolSMSMASTER>();

            try
            {
                int Schoolid = Convert.ToInt32(AAA[0]);
                var smsdetsils = db.tblSchoolSms.Where(X => X.SchoolID == Schoolid).ToList();
                foreach (var s in smsdetsils)
                {

                    SchoolSMSMASTER qq = new SchoolSMSMASTER();
                    qq.id = s.id;
                    qq.senderId = s.senderId;
                    qq.authKey = s.authKey;
                    qq.SchoolID = Convert.ToString(s.SchoolID);
                    string Schoolname = db.tblSchoolDetails.SingleOrDefault(x => x.ID == s.SchoolID).School;
                    qq.SchoolName = Schoolname;
                    qq.active = Convert.ToBoolean(s.active);
                    if (qq.active == true)
                    {
                        qq.Statuss = "Active";
                    }
                    else
                    {
                        qq.Statuss = "DeActive";
                    }
                    list.Add(qq);
                }
            }
            catch 
            {

            }
           
          

            return list.ToArray();

        }

        [System.Web.Http.Route("api/TransportApi/GetALLGpsDeviceDetails")]
        [System.Web.Http.HttpPost]
        public GpsDeviceRecord[] GetALLGpsDeviceDetails (List<string> AAA)
        {
            List<GpsDeviceRecord> list = new List<GpsDeviceRecord>();

            var DeviceDetails = db.tblGpsDeviceRecords.ToList();
            foreach ( var s in DeviceDetails)
            {
                
                GpsDeviceRecord qq = new GpsDeviceRecord();
                qq.ID = s.ID;
                qq.GpsDeviceNO = s.GpsDeviceNO;
                qq.MobileNo = s.MobileNo;
                qq.SchoolID = Convert.ToString(s.SchoolID) ;
                string Schoolname = db.tblSchoolDetails.SingleOrDefault(x => x.ID == s.SchoolID).School;
                qq.SchoolName = Schoolname;
                qq.IsActice = s.IsActice;
                if (qq.IsActice==true)
                {
                    qq.Active = "Active";
                }
                else
                {
                    qq.Active = "DeActive";
                }
                list.Add(qq);
            }

           return list.ToArray();
            
        }

        [System.Web.Http.Route("api/TransportApi/EditGpsDeviceDetails")]
        [System.Web.Http.HttpPost]
        public GpsDeviceRecord EditGpsDeviceDetails (List<string> id)
        {
          
                int idd = Convert.ToInt32(id[0]);
                var check = db.tblGpsDeviceRecords.Where(x => x.ID == idd).FirstOrDefault();
                GpsDeviceRecord usr = new GpsDeviceRecord();
                if (check!=null)
                {
                   
                    usr.GpsDeviceNO = check.GpsDeviceNO;
                    usr.MobileNo = check.MobileNo;
                    usr.SchoolID = Convert.ToString( check.SchoolID);
                    usr.ID = check.ID;
                    usr.IsActice = check.IsActice;

                }
                return usr;

            
           
        }

        [System.Web.Http.Route("api/TransportApi/GpsDeviceList")]
        [System.Web.Http.HttpPost]
        public GpsDeviceRecord1[] GpsDeviceList(List<string> val)
        {
            List<GpsDeviceRecord1> list = new List<GpsDeviceRecord1>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                int Schoolid = Convert.ToInt32( val[0]);
                var result = db.tblGpsDeviceRecords.Where(x => x.SchoolID == Schoolid && x.IsActice==true).ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    GpsDeviceRecord1 items = new GpsDeviceRecord1();
                    
                    items.GpsDeviceNO = a.GpsDeviceNO;
                    items.Id = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TransportApi/getAllDriverbySchool")]
        [System.Web.Http.HttpPost]
        public DriverEmployee[] getAllDriverbySchool(List<string> aa)

        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<DriverEmployee> list = new List<DriverEmployee>();
            var DesignationID =  db.tblDesignations.Where(de => de.Designation.Contains("Driver") && de.SchoolID==SchoolID && de.IsDeleted==null).FirstOrDefault();
            if (DesignationID!=null)
            {
                int DesignationIDD = Convert.ToInt32(DesignationID.DesigID);
                var DriverName = db.tblEmployees.Where(em => em.SchoolID == SchoolID && em.DesigID == DesignationIDD && em.IsDeleted == null).ToList();
                foreach (var a in DriverName)
                {
                    DriverEmployee usr = new DriverEmployee();
                    usr.Name = a.FirstName + " " + a.MiddleName + " " + a.LastName;
                    usr.Id = a.Id;
                    list.Add(usr);
                }
            }
            

            return list.ToArray();


        }
        [System.Web.Http.Route("api/TransportApi/viewAllVehicleDetails")]
        [System.Web.Http.HttpPost]
        public VehicleDetails[] viewAllVehicleDetails()
        {
            int avi = 0;
            List<VehicleDetails> list = new List<VehicleDetails>();
            sqlHelper obj = new sqlHelper();
          
            DataTable dt = obj.getDataTable(@"select  a.id,a.VehicalNo,a.TotalSeats,a.AllowedSeats,a.OwnershipType,a.InsuranceDate,a.PollutionDate,a.FitnessExpiryDate,a.TrackNumber,a.DateCreated,a.SchoolID,b.School,d.GpsDeviceNO,a.IsActive from tblTransportVehicalDetails a
                                              left outer join tblSchoolDetails b on b.ID=a.SchoolID
                                              left outer join tblGpsDeviceRecords d on d.ID=a.TrackNumber 
                                               where a.IsDeleted is null "); /*and d.IsActice = 1*/
            foreach (DataRow dr in dt.Rows)
            {
                avi++;
                VehicleDetails usr = new VehicleDetails();
                usr.Id = int.Parse(dr["Id"].ToString());
                usr.VehicleNumber = dr["VehicalNo"].ToString();
                usr.TotalSeats = dr["TotalSeats"].ToString();
                usr.AllowedSeats = dr["AllowedSeats"].ToString();

                usr.OwnershipType = dr["OwnershipType"].ToString();

                usr.InsuranceExpire1 = dr["InsuranceDate"].ToString();
                usr.PolutionExpire1 = dr["PollutionDate"].ToString();
                usr.FitnessExpiry = dr["FitnessExpiryDate"].ToString();
                //int DeviceNoId = Convert.ToInt32(dr["TrackNumber"]);
                //string GpsDeviceNO = db.tblGpsDeviceRecords.SingleOrDefault(x => x.ID == DeviceNoId).GpsDeviceNO;
                usr.TrackNo = dr["GpsDeviceNO"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.School = dr["School"].ToString();
                usr.Sno = avi.ToString();
                usr.IsActive = Convert.ToBoolean(dr["IsActive"]);
                if (usr.IsActive == true)
                {
                    usr.status = "Active";
                }
                else
                {
                    usr.status = "DeActive";
                }
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/TransportApi/viewAllVehicleDetailsbySchool")]
        [System.Web.Http.HttpPost]
        public VehicleDetails[] viewAllVehicleDetailsbySchool(List<string> aa)
        {
            int avi = 0;
            List<VehicleDetails> list = new List<VehicleDetails>();
            sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(aa[0]);
            DataTable dt = obj.getDataTable(@"select a.id,a.VehicalNo,a.TotalSeats,a.AllowedSeats,a.OwnershipType,a.InsuranceDate,a.PollutionDate,a.FitnessExpiryDate,a.TrackNumber,a.DateCreated,a.SchoolID,B.School,d.GpsDeviceNO,a.IsActive from tblTransportVehicalDetails a
                                              left outer join tblSchoolDetails b on b.ID=a.SchoolID
                                              left outer join tblGpsDeviceRecords d on d.ID=a.TrackNumber 
                                               where  a.SchoolID='" + SchoolID + "' and  a.IsDeleted is null "); /*and d.IsActice = 1*/
            foreach (DataRow dr in dt.Rows)
            {
                avi++;
                VehicleDetails usr = new VehicleDetails();
                usr.Id = int.Parse(dr["Id"].ToString());
                usr.VehicleNumber = dr["VehicalNo"].ToString();
                usr.TotalSeats = dr["TotalSeats"].ToString();
                usr.AllowedSeats = dr["AllowedSeats"].ToString();

                usr.OwnershipType = dr["OwnershipType"].ToString();

                usr.InsuranceExpire1 = dr["InsuranceDate"].ToString();
                usr.PolutionExpire1 = dr["PollutionDate"].ToString();
                usr.FitnessExpiry = dr["FitnessExpiryDate"].ToString();
                //int DeviceNoId = Convert.ToInt32(dr["TrackNumber"]);
                //string GpsDeviceNO = db.tblGpsDeviceRecords.SingleOrDefault(x => x.ID == DeviceNoId).GpsDeviceNO;
                usr.TrackNo = dr["GpsDeviceNO"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.School = dr["School"].ToString();
                usr.Sno = avi.ToString();
                usr.IsActive = Convert.ToBoolean(dr["IsActive"]);
                if (usr.IsActive==true)
                {
                    usr.status = "Active";
                }
                else
                {
                    usr.status = "DeActive";
                }
                list.Add(usr);
            }
            return list.ToArray();
        }


        //[System.Web.Http.Route("api/TransportApi/viewAllVehicleDetails")]
        //[System.Web.Http.HttpPost]
        //public VehicleDetails[] viewAllVehicleDetails()
        //{
        //    List<VehicleDetails> list = new List<VehicleDetails>();
        //    sqlHelper obj = new sqlHelper();
        //    DataTable dt = obj.getDataTable(@"select a.id,a.VehicalNo,a.TotalSeats,a.AllowedSeats,a.OwnershipType,a.InsuranceDate,a.PollutionDate,a.TrackNumber,a.DateCreated,a.SchoolID,B.School from tblTransportVehicalDetails a left outer join tblSchoolDetails b on b.ID=a.SchoolID where a.IsDeleted is null");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        VehicleDetails usr = new VehicleDetails();
        //        usr.Id = int.Parse(dr["Id"].ToString());
        //        usr.VehicleNumber = dr["VehicalNo"].ToString();
        //        usr.TotalSeats = dr["TotalSeats"].ToString();
        //        usr.AllowedSeats = dr["AllowedSeats"].ToString();

        //        usr.OwnershipType = dr["OwnershipType"].ToString();

        //        usr.InsuranceExpire1 = dr["InsuranceDate"].ToString();
        //        usr.PolutionExpire1 = dr["PollutionDate"].ToString();
        //        usr.TrackNo = dr["TrackNumber"].ToString();
        //        usr.SchoolID = dr["SchoolID"].ToString();
        //        usr.School = dr["School"].ToString();


        //        list.Add(usr);
        //    }
        //    return list.ToArray();
        //}

        // [System.Web.Http.Route("api/TransportApi/viewAllVehicleDetails")]
        // [System.Web.Http.HttpPost]
        //  public List<VehicleDetails> viewAllVehicleDetails()
        // {




        //sqlHelper obj = new sqlHelper();
        //List<VehicleDetails> list = new List<VehicleDetails>();

        //var vehicle = db.tblTransportVehicalDetails.ToList();
        //foreach (var a in vehicle)
        //{
        //    VehicleDetails usr = new VehicleDetails();
        //    usr.Id = a.Id;
        //    usr.VehicleNumber = a.VehicalNo;
        //    usr.TotalSeats = a.TotalSeats;
        //    usr.AllowedSeats = a.AllowedSeats;
        //    usr.OwnershipType = a.OwnershipType;
        //    usr.InsuranceExpire1 = a.InsuranceDate;
        //    usr.PolutionExpire1 = a.PollutionDate;
        //    usr.TrackNo = a.TrackNumber;
        //    usr.SchoolID = a.SchoolID;
        //    list.Add(usr);


        //  }
        //
        //  return list;
        //  }
        [System.Web.Http.Route("api/TransportApi/editDriverDetails")]
        [System.Web.Http.HttpPost]
        public DriverDetailss1 editDriverDetails (List<string> aa)
        {
            int idd = Convert.ToInt32( aa[0]);
            var ss = db.tblTransportDrivers.Where(d => d.Id == idd).FirstOrDefault();

         

            DriverDetailss1 usr = new DriverDetailss1();
           if (ss!=null)
            { 
                usr.DriveId = ss.DriveId;
                usr.VehicleId = ss.VehicleId;
                bool checkvehicle = Convert.ToBoolean( db.tblTransportVehicalDetails.SingleOrDefault(x => x.Id == usr.VehicleId).IsActive);
                if (checkvehicle ==true)
                {
                    usr.VehicleId = ss.VehicleId;
                }
                else
                {
                    usr.VehicleId = 0;
                }
                usr.LicenseNo = ss.LicenseNo;
                usr.Id = ss.Id;
                usr.SchoolID = ss.SchoolID;
                usr.LicenseExpDate = ss.LicenseExpDate;
                usr.IsActive = Convert.ToBoolean( ss.IsActive);
            }
            return usr;

        }

        [System.Web.Http.Route("api/TransportApi/editVehicleById")]
        [System.Web.Http.HttpPost]
        public VehicleDetails editVehicleById(List<string> id)
        {
            int idd = Convert.ToInt16(id[0]);
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblTransportVehicalDetails where Id='" + idd + "'");
            VehicleDetails usr = new VehicleDetails();
            foreach (DataRow dr in dt.Rows)
            {

                usr.Id = int.Parse(dr["Id"].ToString());
                usr.VehicleNumber = dr["VehicalNo"].ToString();
                usr.FitnessExpiry = dr["FitnessExpiryDate"].ToString();
                usr.TotalSeats = dr["TotalSeats"].ToString();
                usr.AllowedSeats = dr["AllowedSeats"].ToString();

                usr.OwnershipType = dr["OwnershipType"].ToString();

                usr.InsuranceExpire1 = dr["InsuranceDate"].ToString();
                usr.PolutionExpire1 = dr["PollutionDate"].ToString();
               
                //if device is Inative 
                int deviceid = Convert.ToInt32(dr["TrackNumber"]);
                bool check = Convert.ToBoolean( db.tblGpsDeviceRecords.SingleOrDefault(c => c.ID == deviceid ).IsActice);
                if (check ==true)
                {
                   
                    usr.TrackNo = dr["TrackNumber"].ToString();
                }
                else
                {
                    usr.TrackNo = "0";

                }
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.IsActive = Convert.ToBoolean(dr["IsActive"]);
                // usr.School = dr["School"].ToString();



            }
            return usr;
        }
        [System.Web.Http.Route("api/TransportApi/deleteVehicleById")]
        [System.Web.Http.HttpPost]
        public string deleteVehicleById(string Id)
        {
            bool b = LeaveManagement.deleteVehicleById(Id);
            if (b)
            {
                return "Vehicle Details  Deleted Successfully";
            }
            else
            {
                return "Vehicle Details not Deleted ";
            }

        }

        //[System.Web.Http.Route("api/TransportApi/editVehicleById")]
        //[System.Web.Http.HttpPost]
        //public VehicleDetails editVehicleById(List<string> id)
        //{
        //    int idd = Convert.ToInt16(id[0]);
        //    var result = db.tblTransportVehicalDetails.Where(s => s.Id == idd).SingleOrDefault();
        //    VehicleDetails usr = new VehicleDetails();
        //    if (result != null)
        //    {
        //        usr.Id = result.Id;
        //        usr.VehicleNumber = result.VehicalNo;
        //        usr.TotalSeats = result.TotalSeats;
        //        usr.AllowedSeats = result.AllowedSeats;
        //        usr.OwnershipType = result.OwnershipType;
        //        usr.InsuranceExpire1 = result.InsuranceDate;
        //        usr.PolutionExpire1 = result.PollutionDate;
        //        usr.TrackNo = result.TrackNumber;
        //        usr.SchoolID = result.SchoolID;
        //    }
        //    return usr;

        //}


        [System.Web.Http.Route("api/TransportApi/getAllVehiclebySchool")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllVehiclebySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select Id,VehicalNo from tblTransportVehicalDetails where SchoolID='"+ SchoolID + "' and IsDeleted is null and IsActive=1");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["VehicalNo"].ToString();

                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getHubDetailByIDD")]
        [System.Web.Http.HttpPost]
        public HubDetails getHubDetailByIDD (HubDetails Value )
        {
          
            var GetHubDetails = db.tblHubDetails.SingleOrDefault(x => x.ID == Value.ID && x.IsDeleted==null);
            HubDetails usr = new HubDetails();
            if (GetHubDetails !=null)
            {
                usr.HubLat = GetHubDetails.HubLat;
                usr.HubLong = GetHubDetails.HubLong;
                usr.HubAddress = GetHubDetails.HubAddress;


            }
            return usr;

        }


        [System.Web.Http.Route("api/TransportApi/getAllhubBySchool")]
        [System.Web.Http.HttpPost]
        public HubDetails[] getAllhubBySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            string VehicleID = aa[1].ToString();
            List<HubDetails> list = new List<HubDetails>();
            var getHub = db.tblHubDetails.Where(x => x.VehicleID == VehicleID && x.IsDeleted == null).ToList();
            foreach (var a in getHub)
            {
                HubDetails usr = new HubDetails();
                usr.ID = a.ID;
                usr.HubAddress = a.HubAddress;

                list.Add(usr);
            }

            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getRouteDetails")]
        [System.Web.Http.HttpPost]
        public tblTransRoute getRouteDetails(List<string> val)
        {
            int SchoolID = Convert.ToInt32(val[0]);
            int VehicleID = Convert.ToInt32(val[1]);
            tblTransRoute Usr = new tblTransRoute();
            var getRdetails = db.tblTransRoutes.SingleOrDefault(x => x.VehicleId == VehicleID && x.SchoolID == SchoolID && x.IsActive == true);
            if (getRdetails!=null)
            {
                Usr.Id = getRdetails.Id;
                Usr.RouteCode = getRdetails.RouteCode;
                Usr.StartPlace = getRdetails.StartPlace;          
                Usr.StartPlaceLatt = getRdetails.StartPlaceLatt;
                Usr.StartPlaceLongt = getRdetails.StartPlaceLongt;
                Usr.EndPlace = getRdetails.EndPlace;
                Usr.EndPlaceLatt = getRdetails.EndPlaceLatt;
                Usr.EndPlaceLongt = getRdetails.EndPlaceLongt;
            }
          
            return Usr;
        }


        [System.Web.Http.Route("api/TransportApi/getAllVehicle")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllVehicle()
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select Id,VehicalNo from tblTransportVehicalDetails");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["VehicalNo"].ToString();

                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/fetchAllDetailsDriverByDriverId")]
        [System.Web.Http.HttpPost]
        public EmployeeEm fetchAllDetailsDriverByDriverId(List<string> val)
        {

            int idd = Convert.ToInt32(val[0]);
            var result = db.tblEmployees.SingleOrDefault(s => s.Id == idd);
            EmployeeEm usr = new EmployeeEm();
            usr.PresentAddress = result.PresentAddress;
            usr.PermanentAddress = result.PermAddress;
            usr.Mobile = result.Mobile;
            usr.DOB = result.DOB.ToString();


            return usr;
        }

        [System.Web.Http.Route("api/TransportApi/saveDriverDetails")]
        [System.Web.Http.HttpPost]
        public string saveDriverDetails(DriverDetailss vd)
        {
            string b = LeaveManagement.saveDriverDetails(vd);
            if (b != "")
            {
                return b;
            }
            else
            {
                return "";
            }

        }

        //[System.Web.Http.Route("api/TransportApi/saveDriverDetails")]
        //[System.Web.Http.HttpPost]
        //public string saveDriverDetails(DriverDetails vd)
        //{
        //    if (vd.Id == 0)
        //    {
        //        tblTransportDriver tbl = new tblTransportDriver();
        //        tbl.VehicleId = Convert.ToInt16(vd.VehicleNumber);
        //        tbl.DriveId = Convert.ToInt16(vd.Name);
        //        tbl.PresentAddress = vd.PresentAddress;
        //        tbl.PermanentAddress = vd.PramnentAddress;
        //        tbl.DOB = Convert.ToDateTime(vd.DOB);
        //        tbl.phone = vd.Phoneno;
        //        tbl.LicenseNo = vd.LicenseNo;
        //        tbl.DateCreated = DateTime.Now;

        //        db.tblTransportDrivers.Add(tbl);
        //        db.SaveChanges();
        //        return "Driver Inserted Successfully";
        //    }
        //    else
        //    {
        //        var result = db.tblTransportDrivers.Where(s => s.Id == vd.Id).SingleOrDefault();
        //        result.VehicleId = Convert.ToInt16(vd.VehicleNumber);
        //        result.DriveId = Convert.ToInt16(vd.Name);
        //        result.PresentAddress = vd.PresentAddress;
        //        result.PermanentAddress = vd.PramnentAddress;
        //        result.DOB = Convert.ToDateTime(vd.DOB);
        //        result.phone = vd.Phoneno;
        //        result.LicenseNo = vd.LicenseNo;
        //        result.DateCreated = DateTime.Now;

        //        db.SaveChanges();
        //        return "Driver Updated Successfully";
        //    }


        //}
        [System.Web.Http.Route("api/TransportApi/saveHubDetails")]
        [System.Web.Http.HttpPost]
        public string saveDriverDetails(tblHubDetail Hd)
        {
           
                tblHubDetail tbl = new tblHubDetail();
                tbl.HubName = Hd.HubName;
                tbl.HubAddress = Hd.HubAddress;
                tbl.HubLat = Hd.HubLat;
                tbl.HubLong = Hd.HubLong;
                tbl.VehicleID = Hd.VehicleID;
                tbl.SchoolID = Hd.SchoolID;
                db.tblHubDetails.Add(tbl);
                db.SaveChanges();
                return "Hub Saved Successfully";
          


        }



        [System.Web.Http.Route("api/TransportApi/viewAllDriverDetailsDetailssBySchool")]
        [System.Web.Http.HttpPost]
        public DriverDetailss[] viewAllDriverDetailsDetailssBySchool(List<string> aa)
        {
            int avi = 0;

            int SchoolID = Convert.ToInt32(aa[0]);
            List<DriverDetailss> list = new List<DriverDetailss>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select a.school,d.SchoolID,d.LicenseNo, d.Id,d.LicenseExpDate, v.VehicalNo,name= e.FirstName+' '+ e.MiddleName +' '+ e.LastName,e.Mobile,e.PermAddress,e.PresentAddress,e.DOB,d.IsActive  from tblTransportDriver d 
	join  tblEmployee e on d.DriveId = e.Id 
	join  tblTransportVehicalDetails v on d.VehicleId = v.Id
	left outer join tblSchoolDetails a on d.SchoolID=a.ID
	where d.SchoolID='" + SchoolID + "' and d.IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {

                avi++;
                DriverDetailss usr = new DriverDetailss();
                usr.Id = int.Parse(dr["Id"].ToString());
                usr.VehicleNumber = dr["VehicalNo"].ToString();
                usr.Name = dr["name"].ToString();                
                usr.PramnentAddress = dr["PermAddress"].ToString();
                if (usr.PramnentAddress=="")
                {
                    usr.PramnentAddress = "Not Available";
                }
                usr.PresentAddress = dr["PresentAddress"].ToString();
                if (usr.PresentAddress == "")
                {
                    usr.PresentAddress = "Not Available";
                }
               
                usr.DOB = Convert.ToDateTime(dr["DOB"]).ToString("dd MMMM yyyy");
                usr.Phoneno = dr["Mobile"].ToString();
                usr.LicenseNo = dr["LicenseNo"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.School = dr["School"].ToString();
                usr.Sno = avi.ToString();
                usr.LicenseExpiryDate = dr["LicenseExpDate"].ToString();
                usr.IsActive = Convert.ToBoolean(dr["IsActive"]);
                if (usr.IsActive==true)
                {
                    usr.Status = "Active";
                }
                else
                {
                    usr.Status = "DeActive";
                }
                
                list.Add(usr);
            }
            return list.ToArray();
        }






        [System.Web.Http.Route("api/TransportApi/viewAllDriverDetailsDetailss")]
        [System.Web.Http.HttpPost]
        public DriverDetailss[] viewAllDriverDetailsDetailss()
        {
            List<DriverDetailss> list = new List<DriverDetailss>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select a.school,d.SchoolID, d.Id,v.VehicalNo,name= e.FirstName + e.MiddleName + e.LastName,d.PermanentAddress,d.PresentAddress,d.DOB,d.phone  from tblTransportDriver d join  tblEmployee e on d.DriveId = e.Id join  tblTransportVehicalDetails v on d.VehicleId = v.Id left outer join tblSchoolDetails a on d.SchoolID=a.ID");
            foreach (DataRow dr in dt.Rows)
            {


                DriverDetailss usr = new DriverDetailss();
                usr.Id = int.Parse(dr["Id"].ToString());
                usr.VehicleNumber = dr["VehicalNo"].ToString();
                usr.Name = dr["name"].ToString();
                usr.PramnentAddress = dr["PermanentAddress"].ToString();
                usr.PresentAddress = dr["PresentAddress"].ToString();

                usr.DOB = dr["DOB"].ToString();

                usr.Phoneno = dr["phone"].ToString();
                usr.LicenseNo = dr["VehicalNo"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.School = dr["School"].ToString();


                list.Add(usr);
            }
            return list.ToArray();
        }


        //[System.Web.Http.Route("api/TransportApi/viewAllDriverDetailsDetails")]
        //[System.Web.Http.HttpPost]
        //public List<DriverDetails> viewAllDriverDetailsDetails()
        //{
        //    sqlHelper obj = new sqlHelper();
        //    List<DriverDetails> list = new List<DriverDetails>();

        //    var vehicle = (from d in db.tblTransportDrivers
        //                   join e in db.tblEmployees on d.DriveId equals e.Id
        //                   join v in db.tblTransportVehicalDetails on d.VehicleId equals v.Id
        //                   select new { d, Name = e.FirstName + " " + e.MiddleName + " " + e.LastName, v.VehicalNo }).ToList();
        //    foreach (var a in vehicle)
        //    {
        //        DriverDetails usr = new DriverDetails();
        //        usr.Id = a.d.Id;
        //        usr.VehicleNumber = a.VehicalNo;
        //        usr.Name = a.Name;
        //        usr.PramnentAddress = a.d.PermanentAddress;
        //        usr.PresentAddress = a.d.PresentAddress;
        //        usr.DOB = Convert.ToString(a.d.DOB);
        //        usr.Phoneno = a.d.phone;
        //        list.Add(usr);
        //    }



        //    return list;
        //}


        //[System.Web.Http.Route("api/TransportApi/saveRouteDetails")]
        //[System.Web.Http.HttpPost]
        //public string saveRouteDetails(RouteDetails vd)
        //{
        //    if (vd.Id == 0)
        //    {
        //        tblTransRoute tbl = new tblTransRoute();
        //        tbl.VehicleId = Convert.ToInt16(vd.VehicleNumber);

        //        tbl.RouteCode = vd.RouteCode;
        //        tbl.StartPlace = vd.StartPlace;
        //        tbl.EndPlace = vd.EndPlace;

        //        tbl.DateCreated = DateTime.Now;

        //        db.tblTransRoutes.Add(tbl);
        //        db.SaveChanges();
        //        return "Route Inserted Successfully";
        //    }
        //    else
        //    {
        //        var result = db.tblTransRoutes.Where(s => s.Id == vd.Id).SingleOrDefault();
        //        result.VehicleId = Convert.ToInt16(vd.VehicleNumber);
        //        result.RouteCode = vd.RouteCode;
        //        result.StartPlace = vd.StartPlace;
        //        result.EndPlace = vd.EndPlace;
        //        result.DateCreated = DateTime.Now;
        //        db.SaveChanges();
        //        return "Route Updated Successfully";
        //    }


        //}


        [System.Web.Http.Route("api/TransportApi/viewAllRouteDetails")]
        [System.Web.Http.HttpPost]
        public RouteDetails[] viewAllRouteDetails(List<string> id)
        {
            int avi = 0;
            int SchoolID = Convert.ToInt32(id[0]);
            sqlHelper obj = new sqlHelper();
            List<RouteDetails> list = new List<RouteDetails>();

            var vehicle = (from d in db.tblTransRoutes
                           join v in db.tblTransportVehicalDetails on d.VehicleId equals v.Id
                           where d.SchoolID==SchoolID && d.IsDeleted==null
                           select new { d, v.VehicalNo }).ToList();
            foreach (var a in vehicle)
            {
                avi++;
                RouteDetails usr = new RouteDetails();
                usr.sno = avi.ToString();
                usr.Id = a.d.Id;
                usr.VehicleNumber = a.VehicalNo;
                usr.RouteCode = a.d.RouteCode;
                usr.StartPlace = a.d.StartPlace;
                usr.EndPlace = a.d.EndPlace;
                usr.IsActive = Convert.ToBoolean( a.d.IsActive);

                list.Add(usr);
            }



            return list.ToArray();
        }

        [System.Web.Http.Route("api/TransportApi/viewAllHubDetails")]
        [System.Web.Http.HttpPost]
        public HubDetails[] viewAllHubDetails(List<string> id)
        {
            int avi = 0;
            int SchoolID = Convert.ToInt32(id[0]);
            List<HubDetails> list = new List<HubDetails>();
            var getHubs = db.tblHubDetails.Where(x => x.SchoolID == SchoolID && x.IsDeleted==null).ToList();
            foreach ( var a in getHubs)
            {
                avi++;
                HubDetails usr = new HubDetails();
                usr.Sno = avi.ToString();
                usr.ID = a.ID;
                usr.HubName = a.HubName;
                usr.HubAddress = a.HubAddress;
                usr.HubLat = a.HubLat;
                usr.HubLong = a.HubLong;
                int vehicleid = Convert.ToInt32(a.VehicleID);
                var vehicleNO = db.tblTransportVehicalDetails.SingleOrDefault(x => x.Id == vehicleid).VehicalNo;
                usr.VehicleID = vehicleNO;
                list.Add(usr);

            }

            return list.ToArray();

        }



        //        [System.Web.Http.Route("api/TransportApi/viewAllRouteDetails")]
        //        [System.Web.Http.HttpPost]
        //        public RouteDetails[] viewAllRouteDetails()
        //        {
        //            List<RouteDetails> list = new List<RouteDetails>();
        //            sqlHelper obj = new sqlHelper();
        //            DataTable dt = obj.getDataTable(@"select d.Id,v.VehicalNo,d.RouteCode,d.StartPlace,d.EndPlace,a.School,d.SchoolID from tblTransRoute  d join  tblTransportVehicalDetails v on d.VehicleId= v.Id 
        //left outer join tblSchoolDetails a on a.ID=d.SchoolID
        // where d.IsDeleted is null");
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                RouteDetails usr = new RouteDetails();
        //                usr.Id = int.Parse(dr["Id"].ToString());
        //                usr.VehicleNumber = dr["VehicalNo"].ToString();
        //                usr.RouteCode = dr["RouteCode"].ToString();
        //                usr.StartPlace = dr["StartPlace"].ToString();

            //                usr.EndPlace = dr["EndPlace"].ToString();


            //                usr.SchoolID = dr["SchoolID"].ToString();
            //                usr.School = dr["School"].ToString();


            //                list.Add(usr);
            //            }
            //            return list.ToArray();
            //        }



            //[System.Web.Http.Route("api/TransportApi/editRouteById")]
            //[System.Web.Http.HttpPost]
            //public RouteDetails editRouteById(List<string> id)
            //{
            //    int idd = Convert.ToInt16(id[0]);
            //    sqlHelper obj = new sqlHelper();
            //    DataTable dt = obj.getDataTable(@"select * from tblTransRoute where Id='" + idd + "'");
            //    RouteDetails usr = new RouteDetails();
            //    foreach (DataRow dr in dt.Rows)
            //    {

            //        usr.Id = int.Parse(dr["Id"].ToString());
            //        usr.VehicleNumber = dr["VehicleId"].ToString();
            //        usr.RouteCode = dr["RouteCode"].ToString();
            //        usr.StartPlace = dr["StartPlace"].ToString();
            //        usr.EndPlace = dr["EndPlace"].ToString();

            //        usr.SchoolID = dr["SchoolID"].ToString();
            //    }
            //    return usr;
            //}
        [System.Web.Http.Route("api/TransportApi/editRouteById")]
        [System.Web.Http.HttpPost]
        public RouteDetails editRouteById(List<string> id)
        {
            int idd = Convert.ToInt16(id[0]);
            var result = db.tblTransRoutes.Where(s => s.Id == idd).SingleOrDefault();
            RouteDetails usr = new RouteDetails();
            if (result != null)
            {
                usr.Id = result.Id;
                //usr.VehicleNumber = Convert.ToString(result.VehicleId);
                bool checkVehicle = Convert.ToBoolean( db.tblTransportVehicalDetails.SingleOrDefault(x => x.Id == result.VehicleId).IsActive);
                if (checkVehicle==true)
                {
                    usr.VehicleNumber= Convert.ToString(result.VehicleId);
                   
                }
                else
                {
                    usr.VehicleNumber = "0";
                }
                usr.RouteCode = result.RouteCode;
                usr.StartPlace = result.StartPlace;
                usr.EndPlace = result.EndPlace;
                usr.IsActive = Convert.ToBoolean(result.IsActive);
                usr.SchoolID = result.SchoolID.ToString();
                usr.StartPlaceLatt = result.StartPlaceLatt;
                usr.StartPlaceLongt = result.StartPlaceLongt;
                usr.EndPlaceLatt = result.EndPlaceLatt;
                usr.EndPlaceLongt = result.EndPlaceLongt;

            }
            return usr;

        }



        [System.Web.Http.Route("api/TransportApi/deleteRouteById")]
        [System.Web.Http.HttpPost]
        public string deleteRouteById(List<string> id)
        {
            int idd = Convert.ToInt16(id[0]);
            var res = db.tblTransRoutes.SingleOrDefault(s => s.Id == idd);
            res.IsDeleted = 1;
            res.Deleted_on = DateTime.Now;
            res.IsActive = false;
            //db.tblTransRoutes.Remove(res);
            db.SaveChanges();
            return "Route Deleted Successfully";


        }
        [System.Web.Http.Route("api/TransportApi/deleteHubById")]
        [System.Web.Http.HttpPost]
        public string deleteHubById(List<string> id)
        {
            int idd = Convert.ToInt16(id[0]);
            var res = db.tblHubDetails.SingleOrDefault(s => s.ID == idd);
            res.IsDeleted = 1;
            res.Deleted_on = DateTime.Now;           
            //db.tblTransRoutes.Remove(res);
            db.SaveChanges();
            return "Hub Deleted Successfully";


        }
        [System.Web.Http.Route("api/TransportApi/GetRouteDetailByID")]
        [System.Web.Http.HttpPost]
        public RouteDetails GetRouteDetailByID (List<string> id)
        {
            int idd = Convert.ToInt32(id[0]);
            var routedetails = db.tblTransRoutes.SingleOrDefault(x => x.Id == idd);
            RouteDetails usr = new RouteDetails();
            if (routedetails !=null)
            {
                usr.Id = routedetails.Id;
                usr.VehicleNumber =  routedetails.VehicleId.ToString();
                usr.StartPlace = routedetails.StartPlace;
                usr.StartPlaceLatt = routedetails.StartPlaceLatt;
                usr.StartPlaceLongt = routedetails.StartPlaceLongt;
                usr.EndPlace = routedetails.EndPlace;
                usr.EndPlaceLatt = routedetails.EndPlaceLatt;
                usr.EndPlaceLongt = routedetails.EndPlaceLongt;
               

            }
            return usr;

        }

        [System.Web.Http.Route("api/TransportApi/GetHubDetailByID")]
        [System.Web.Http.HttpPost]
        public tblHubDetail GetHubDetailByID(List<string> id)
        {
            int idd = Convert.ToInt32(id[0]);
            var Hubdetails = db.tblHubDetails.SingleOrDefault(x => x.ID == idd);
            tblHubDetail usr = new tblHubDetail();
            if (Hubdetails != null)
            {
                usr.ID = Hubdetails.ID;
                usr.HubLat = Hubdetails.HubLat;
                usr.HubLong = Hubdetails.HubLong;
                usr.HubAddress = Hubdetails.HubAddress;
               


            }
            return usr;

        }
        //HAS TO BE CHANGE
        [System.Web.Http.Route("api/TransportApi/GetAllHublanlong")]
        [System.Web.Http.HttpPost]
        public tblHubDetail[] GetAllHublanlong(List<string> id)
        {
           
            int SchoolID = Convert.ToInt32(id[0]);
            string VehicleID =  id[1].ToString();
            //fatch route as inserted sequence //


            //var Hubdetails = db.tblHubDetails.Where(x => x.VehicleID == VehicleID && x.SchoolID== SchoolID && x.IsDeleted==null).ToList();
            //List<tblHubDetail> list = new List<tblHubDetail>();
            //foreach(var a in Hubdetails)
            //{
            //    tblHubDetail usr = new tblHubDetail();
            //    usr.ID = a.ID;
            //    usr.HubLat = a.HubLat;
            //    usr.HubLong = a.HubLong;
            //    usr.HubAddress = a.HubAddress;
            //    list.Add(usr);
            //}


            //fatch route order by sequence (nearest distence) //
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"SELECT *FROM  tblHubDetails where VehicleID='" + VehicleID + "' and SchoolID='" + SchoolID + "' and IsDeleted is null ORDER BY ABS(HubLat) + ABS(HubLong) ASC");
            //var Hubdetails = db.tblHubDetails.Where(x => x.VehicleID == VehicleID && x.SchoolID== SchoolID && x.IsDeleted==null).ToList();
            List<tblHubDetail> list = new List<tblHubDetail>();
            foreach (DataRow a in dt.Rows)
            {
                tblHubDetail usr = new tblHubDetail();
                usr.ID = Convert.ToInt32(a["ID"]);
                usr.HubLat = a["HubLat"].ToString();
                usr.HubLong = a["HubLong"].ToString(); ;
                usr.HubAddress = a["HubAddress"].ToString();
                list.Add(usr);
            }




            return list.ToArray();

        }

        [System.Web.Http.Route("api/TransportApi/getAllRouteDetailsBySchool")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllRouteDetailsBySchool(List<string> a)
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(a[0]);
            DataTable dt = obj.getDataTable(@"SELECT * FROM tblTransRoute where SchoolID='"+ SchoolID + "' and IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["StartPlace"].ToString() + "-" + dr["EndPlace"].ToString() + " (" + dr["RouteCode"].ToString() + ")";


                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getAllRouteDetails")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllRouteDetails()
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblTransRoute");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["StartPlace"].ToString() + "-" + dr["EndPlace"].ToString() + " (" + dr["RouteCode"].ToString() + ")";


                list.Add(usr);
            }
            return list.ToArray();


        }



        //[System.Web.Http.Route("api/TransportApi/saveDestinationDetails")]
        //[System.Web.Http.HttpPost]
        //public string saveDestinationDetails(DestinationDetails vd)
        //{
        //    if (vd.Id == 0)
        //    {
        //        tblRoteDestination tbl = new tblRoteDestination();
        //        tbl.RouteId = Convert.ToInt16(vd.Route);
        //        tbl.PickAndDrop = vd.Pickdrop;
        //        tbl.StopTime = vd.StopTime;
        //        tbl.DateCreated = DateTime.Now;
        //        db.tblRoteDestinations.Add(tbl);
        //        db.SaveChanges();
        //        return "Route Destination Inserted Successfully";
        //    }
        //    else
        //    {
        //        var result = db.tblRoteDestinations.Where(s => s.Id == vd.Id).SingleOrDefault();
        //        result.RouteId = Convert.ToInt16(vd.Route);
        //        result.PickAndDrop = vd.Pickdrop;
        //        result.StopTime = vd.StopTime;
        //        result.DateCreated = DateTime.Now;
        //        db.SaveChanges();
        //        return "Route Destination Updated Successfully";
        //    }


        //}

        [System.Web.Http.Route("api/TransportApi/ViewAllDestination")]
        [System.Web.Http.HttpPost]
        public DestinationDetails[] ViewAllDestination()
        {
            List<DestinationDetails> list = new List<DestinationDetails>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select StartPlace+'-'+ EndPlace+' ('+RouteCode+')' RouteName,a.School,rd.* from tblRoteDestination rd
                                              inner join tblTransRoute r on r.Id=rd.RouteId left outer join tblSchoolDetails a on a.ID =rd.SchoolID where rd.IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                DestinationDetails usr = new DestinationDetails();
                usr.Id = Convert.ToInt16(dr["Id"].ToString());
                usr.Route = dr["RouteName"].ToString();
                usr.Pickdrop = dr["PickAndDrop"].ToString();
                usr.StopTime = dr["StopTime"].ToString();
                usr.School = dr["School"].ToString();
                usr.SchoolID = dr["SchoolID"].ToString();

                list.Add(usr);
            }
            return list.ToArray();


        }

        //[System.Web.Http.Route("api/TransportApi/editRouteDestinationById")]
        //[System.Web.Http.HttpPost]
        //public DestinationDetails editRouteDestinationById(List<string> id)
        //{
        //    int idd = Convert.ToInt16(id[0]);
        //    var result = db.tblRoteDestinations.Where(s => s.Id == idd).SingleOrDefault();
        //    DestinationDetails usr = new DestinationDetails();
        //    if (result != null)
        //    {
        //        usr.Id = result.Id;
        //        usr.Route = Convert.ToString(result.RouteId);
        //        usr.Pickdrop = result.PickAndDrop;
        //        usr.StopTime = result.StopTime;
        //    }
        //    return usr;

        //}
        [System.Web.Http.Route("api/TransportApi/editRouteDestinationById")]
        [System.Web.Http.HttpPost]
        public DestinationDetails editRouteDestinationById(List<string> id)
        {

            int idd = Convert.ToInt16(id[0]);
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select * from tblRoteDestination where Id='" + idd + "'");
            DestinationDetails usr = new DestinationDetails();
            foreach (DataRow dr in dt.Rows)
            {

                usr.Id = int.Parse(dr["Id"].ToString());
                usr.Route = dr["RouteId"].ToString();
                usr.Pickdrop = dr["PickAndDrop"].ToString();
                usr.StopTime = dr["StopTime"].ToString();


                usr.SchoolID = dr["SchoolID"].ToString();
            }
            return usr;
        }

        [System.Web.Http.Route("api/TransportApi/deleteRouteDestinationById")]
        [System.Web.Http.HttpPost]
        public string deleteRouteDestinationById(List<string> id)
        {
            int idd = Convert.ToInt16(id[0]);
            var res = db.tblRoteDestinations.SingleOrDefault(s => s.Id == idd);
            res.IsDeleted = 1;
            res.Deleted_on = DateTime.Now;
            db.SaveChanges();
            //db.tblRoteDestinations.Remove(res);
            //db.SaveChanges();
            return "Route Destination Deleted Successfully";


        }
    

        [System.Web.Http.Route("api/TransportApi/deleteVehicleByIddddd")]
        [System.Web.Http.HttpGet]
        public string deleteVehicleByIddddd(string Id)
        {
            bool b = LeaveManagement.deleteVehicleById(Id);
            if (b)
            {
                return "Vehicle Details  Deleted Successfully";
            }
            else
            {
                return "Vehicle Details not Deleted ";
            }

        }
        //avneesh
        [System.Web.Http.Route("api/TransportApi/deleteDriverByIDD")]
        [System.Web.Http.HttpGet]
        public string deleteDriverByIDD(string Id)
        {
            int idd = Convert.ToInt32(Id);
            var DriverDetails = db.tblTransportDrivers.SingleOrDefault(d => d.Id == idd);
            if (DriverDetails!=null)
            {
                //tblTransportDriver tb = new tblTransportDriver();
                DriverDetails.IsDeleted = 1;
                DriverDetails.IsActive = false;
                DriverDetails.Deleted_on = DateTime.Now;
                db.SaveChanges();

                return "Driver Details  Deleted Successfully";
            }
            else
            {
                return "Driver Details Not Found";

            }

           

        }

        [System.Web.Http.Route("api/TransportApi/getDestinationByRouteIdBySchool")]
        [System.Web.Http.HttpPost]
        public DestinationDetails[] getDestinationByRouteIdBySchool(List<string> Id)
        {
            int idd = Convert.ToInt16(Id[0]);
            int SchoolID = Convert.ToInt32(Id[1]);
            List<DestinationDetails> list = new List<DestinationDetails>();
            sqlHelper obj = new sqlHelper();
            var result = db.tblRoteDestinations.Where(s => s.RouteId == idd && s.SchoolID== SchoolID && s.IsDeleted==null).ToList();
            foreach (var dr in result)
            {
                DestinationDetails usr = new DestinationDetails();
                usr.Id = dr.Id;
                usr.Route = dr.PickAndDrop;
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getDestinationByRouteId")]
        [System.Web.Http.HttpPost]
        public DestinationDetails[] getDestinationByRouteId(List<string> Id)
        {
            int idd = Convert.ToInt16(Id[0]);
            List<DestinationDetails> list = new List<DestinationDetails>();
            sqlHelper obj = new sqlHelper();
            var result = db.tblRoteDestinations.Where(s => s.RouteId == idd).ToList();
            foreach (var dr in result)
            {
                DestinationDetails usr = new DestinationDetails();
                usr.Id = dr.Id;
                usr.Route = dr.PickAndDrop;
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/TransportApi/getAllStudentByClassSectionIdbySchool")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentByClassSectionIdbySchool(Student std)
        {
            // int idd = Convert.ToInt16(Id[0]);

            List<Student> list = new List<Student>();
            int schoolid = Convert.ToInt32(std.SchoolID);
            var result = db.TBLStudents.Where(s => s.ClassID == std.ClassID && s.SectionID == std.SectionID && s.Status == 3 && s.IsDeleted==null && s.SchoolID== schoolid).ToList();
            foreach (var dr in result)
            {
                Student usr = new Student();
                usr.ID = dr.ID;
                usr.FirstName = dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getAllStudentByClassSectionId")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentByClassSectionId(Student std)
        {
            // int idd = Convert.ToInt16(Id[0]);

            List<Student> list = new List<Student>();
            var result = db.TBLStudents.Where(s => s.ClassID == std.ClassID && s.SectionID == std.SectionID && s.Status == 3).ToList();
            foreach (var dr in result)
            {
                Student usr = new Student();
                usr.ID = dr.ID;
                usr.FirstName = dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;
                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/TransportApi/getAllEmployeeByDepIdBySchool")]
        [System.Web.Http.HttpPost]
        public DestinationDetails[] getAllEmployeeByDepIdBySchool(List<string> Id)
        {
            int idd = Convert.ToInt16(Id[0]);
            int SchoolID = Convert.ToInt32(Id[1]);
            List<DestinationDetails> list = new List<DestinationDetails>();
            sqlHelper obj = new sqlHelper();
            var result = db.tblEmployees.Where(s => s.DeptID == idd && s.SchoolID== SchoolID && s.IsDeleted==null).ToList();
            foreach (var dr in result)
            {
                DestinationDetails usr = new DestinationDetails();
                usr.Id = dr.Id;
                usr.Route = dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;
                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/TransportApi/getAllEmployeeByDepId")]
        [System.Web.Http.HttpPost]
        public DestinationDetails[] getAllEmployeeByDepId(List<string> Id)
        {
            int idd = Convert.ToInt16(Id[0]);
            List<DestinationDetails> list = new List<DestinationDetails>();
            sqlHelper obj = new sqlHelper();
            var result = db.tblEmployees.Where(s => s.DeptID == idd).ToList();
            foreach (var dr in result)
            {
                DestinationDetails usr = new DestinationDetails();
                usr.Id = dr.Id;
                usr.Route = dr.FirstName + " " + dr.MiddleName + " " + dr.LastName;
                list.Add(usr);
            }
            return list.ToArray();


        }

     
        [System.Web.Http.Route("api/TransportApi/deleteAllocationById")]
        [System.Web.Http.HttpPost]
        public string deleteAllocationById(List<string> id)
        {
            int idd = Convert.ToInt32(id[0]);
            var check = db.tblTransportAllocations.Where(del=>del.Id== idd).FirstOrDefault();
            check.IsDeleted = 1;
            check.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "Allocated User Deleted Successfully";

        }

        [System.Web.Http.Route("api/TransportApi/saveTransportAllocationDetails")]
        [System.Web.Http.HttpPost]
        public string saveTransportAllocationDetails(TransportAllocation vd)
        {
            if (vd.UserType == "Student")
            {

                if (vd.Id == 0)
                {





                    foreach (var a in vd.stu.ToList())
                    {
                        var result = db.tblTransportAllocations.Where(s => s.StudentId == a.StudentName).ToList();
                        foreach (var f in result)
                        {

                            db.tblTransportAllocations.Remove(f);
                        }



                        tblTransportAllocation tbl = new tblTransportAllocation();
                        tbl.RouteId = vd.HubID;
                        tbl.DestinationId = vd.VehicleID;
                        tbl.UserType = vd.UserType;
                        tbl.ClassId = a.Class1;
                        tbl.SectionId = a.Section1;
                        tbl.StudentId = a.StudentName;
                        tbl.DepId = "0";
                        tbl.EmployeeId = "0";
                        tbl.DateCreated = DateTime.Now;
                        tbl.Status = vd.Status1 ;
                        tbl.SchoolID = vd.SchoolID;
                        db.tblTransportAllocations.Add(tbl);
                        db.SaveChanges();


                    }
                    return "Transport Allocate Successfully";


                }

                else
                {
                    foreach (var a in vd.stu.ToList())
                    {
                        var tbl = db.tblTransportAllocations.Where(s => s.Id == vd.Id).SingleOrDefault();
                        tbl.RouteId = vd.HubID;
                        tbl.DestinationId = vd.VehicleID;
                        tbl.UserType = vd.UserType;
                        tbl.ClassId = a.Class1;
                        tbl.SectionId = a.Section1;
                        tbl.StudentId = a.StudentName;
                        tbl.SchoolID = vd.SchoolID;
                        tbl.DepId = "0";
                        tbl.EmployeeId = "0";
                        tbl.Status = vd.Status1;
                        tbl.DateCreated = DateTime.Now;
                        //    db.tblTransportAllocations.Add(tbl);
                        db.SaveChanges();
                    }
                    return "Transport Allocated Updated Successfully";


                }
            }
            else
            {
                if (vd.Id == 0)
                {
                    var result = db.tblTransportAllocations.SingleOrDefault(s => s.EmployeeId == vd.EmployeeName);
                    if (result!=null)
                    {
                        db.tblTransportAllocations.Remove(result);
                    }

                    tblTransportAllocation tbl = new tblTransportAllocation();
                    tbl.RouteId = vd.HubID;
                    tbl.DestinationId = vd.VehicleID;
                    tbl.UserType = vd.UserType;
                    tbl.DepId = vd.Department;
                    tbl.EmployeeId = vd.EmployeeName;
                    tbl.DateCreated = DateTime.Now;
                    tbl.Status = vd.Status1;
                    tbl.ClassId = "0";
                    tbl.SectionId = "0";
                    tbl.StudentId = "0";
                    tbl.SchoolID = vd.SchoolID;
                    db.tblTransportAllocations.Add(tbl);
                    db.SaveChanges();
                    return "Transport Allocate Successfully";

                }
                else
                {
                    var emptrans = db.tblTransportAllocations.SingleOrDefault(s => s.Id == vd.Id);
                    emptrans.RouteId = vd.HubID;
                    emptrans.DestinationId = vd.VehicleID;
                    emptrans.UserType = vd.UserType;
                    emptrans.DepId = vd.Department;
                    emptrans.EmployeeId = vd.EmployeeName;
                    emptrans.DateCreated = DateTime.Now;
                    emptrans.ClassId = "0";
                    emptrans.SectionId = "0";
                    emptrans.StudentId = "0";
                    emptrans.Status = vd.Status1;
                    emptrans.SchoolID = vd.SchoolID;
                    db.SaveChanges();
                    return "Transport Allocated Updated Successfully";
                }

            }
           
        }
        [System.Web.Http.Route("api/TransportApi/getAllAllocationDetailsBySchool")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getAllAllocationDetailsBySchool(List<string> aa)
        {
            List<TransportAllocation> list = new List<TransportAllocation>();
            //
            int SchoolID = Convert.ToInt32(aa[0]);
            var StuTraAllowcation = db.tblTransportAllocations.Where(x => x.UserType == "Student" && x.SchoolID== SchoolID && x.IsDeleted == null).ToList();
            foreach( var stu in StuTraAllowcation)
            {
                TransportAllocation usr = new TransportAllocation();

               // usr.VehicleID = stu.DestinationId;
                int vehicleidd = Convert.ToInt32(stu.DestinationId);
                int StudentId = Convert.ToInt32(stu.StudentId);
                int hUBid = Convert.ToInt32(stu.RouteId);
                string VehicleNo = db.tblTransportVehicalDetails.SingleOrDefault(u => u.Id == vehicleidd).VehicalNo;
                if (VehicleNo=="")
                {
                    usr.VehicleID = "Not Found";
                }
                else
                {
                    usr.VehicleID = VehicleNo;
                }

                string StudentfName = db.TBLStudents.SingleOrDefault(s => s.ID == StudentId).FirstName;
                string StudentMName = db.TBLStudents.SingleOrDefault(s => s.ID == StudentId).MiddleName;
                string StudentLName = db.TBLStudents.SingleOrDefault(s => s.ID == StudentId).LastName;
                string USERID = db.TBLStudents.SingleOrDefault(s => s.ID == StudentId).SUserID;
                usr.UserName = StudentfName + "" + StudentMName + ""+ StudentLName;
                usr.UserID = USERID;
                var HUBDETAILS = db.tblHubDetails.Where(HB => HB.ID == hUBid).FirstOrDefault();
                if (HUBDETAILS !=null)
                {
                    usr.Hubname = HUBDETAILS.HubName;
                    usr.Hubaddress = HUBDETAILS.HubAddress;
                }
                usr.UserType = "Student";
                if (stu.Status==true)
                {
                    usr.Status = "Active";

                }
                
                else
                {
                    usr.Status = "Deactive";
                }
                usr.Id = stu.Id;
                list.Add(usr);


            }

            var EmpTraAllowcation = db.tblTransportAllocations.Where(x => x.UserType == "Employee" && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();
            foreach (var stu in EmpTraAllowcation)
            {
                TransportAllocation usr = new TransportAllocation();

                // usr.VehicleID = stu.DestinationId;
                int vehicleidd = Convert.ToInt32(stu.DestinationId);
                int EmployeeId = Convert.ToInt32(stu.EmployeeId);
                int hUBid = Convert.ToInt32(stu.RouteId);
                string VehicleNo = db.tblTransportVehicalDetails.SingleOrDefault(u => u.Id == vehicleidd).VehicalNo;
                if (VehicleNo == "")
                {
                    usr.VehicleID = "Not Found";
                }
                else
                {
                    usr.VehicleID = VehicleNo;
                }

                string EmployeefName = db.tblEmployees.SingleOrDefault(s => s.Id == EmployeeId).FirstName;
                string EmployeeMName = db.tblEmployees.SingleOrDefault(s => s.Id == EmployeeId).MiddleName;
                string EmployeeLName = db.tblEmployees.SingleOrDefault(s => s.Id == EmployeeId).LastName;
                string USERID = db.tblEmployees.SingleOrDefault(s => s.Id == EmployeeId).UserID;
                usr.UserName = EmployeefName + "" + EmployeeMName + "" + EmployeeLName;
                usr.UserID = USERID;
                var HUBDETAILS = db.tblHubDetails.Where(HB => HB.ID == hUBid).FirstOrDefault();
                if (HUBDETAILS != null)
                {
                    usr.Hubname = HUBDETAILS.HubName;
                    usr.Hubaddress = HUBDETAILS.HubAddress;
                }
                usr.UserType = "Employee";
                if (stu.Status == true)
                {
                    usr.Status = "Active";

                }
                else
                {
                    usr.Status = "Deactive";
                }
                usr.Id = stu.Id;
                list.Add(usr);



            }




            return list.ToArray();

        }
        [System.Web.Http.Route("api/TransportApi/EditAllowcationDetails")]
        [System.Web.Http.HttpPost]
        public TransportAllocation EditAllowcationDetails (allocaton val)
        {
            
            var checkalloca = db.tblTransportAllocations.SingleOrDefault(x => x.Id == val.ID);
            TransportAllocation usr = new TransportAllocation();
            if (checkalloca !=null)
            {
                usr.Id = checkalloca.Id;
                usr.HubID = checkalloca.RouteId;
                int hubidd = Convert.ToInt32(checkalloca.RouteId);
                var Hublatlong = db.tblHubDetails.Where(x => x.ID == hubidd && x.IsDeleted == null).FirstOrDefault();
                usr.lat = Hublatlong.HubLat;
                usr.longi = Hublatlong.HubLong;
                usr.Hubaddress = Hublatlong.HubAddress;
                usr.VehicleID = checkalloca.DestinationId;
                usr.UserType = checkalloca.UserType;
                usr.Class = checkalloca.ClassId;
                usr.Section = checkalloca.SectionId;
                usr.StudentName = checkalloca.StudentId;
                usr.EmployeeName = checkalloca.EmployeeId;
                usr.Department = checkalloca.DepId;
                usr.Status1 = checkalloca.Status;
            }
            return usr;
        }

        [System.Web.Http.Route("api/TransportApi/getAllAllocationDetails")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getAllAllocationDetails(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<TransportAllocation> list = new List<TransportAllocation>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select ea.Id,r.RouteCode,ea.UserType,e.Empcode ReisterNo,e.FirstName+' '+isnull(e.MiddleName,'')+' '+e.LastName   as Name,
                                        rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join tblEmployee e on e.Id=ea.EmployeeId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where ea.SchoolID='"+SchoolID+"' and ea.UserType='Employee' union select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name, rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea inner join TBLStudent s on s.Id=ea.StudentId inner join tblTransRoute r on r.Id=ea.RouteId inner join tblRoteDestination rd on rd.RouteId=ea.RouteId  where ea.schoolID='" + SchoolID + "'and ea.UserType='Student'  ");



            //select ea.Id,r.RouteCode,ea.UserType, e.Empcode ReisterNo, e.FirstName + ' ' + isnull(e.MiddleName, '') + ' ' + e.LastName as Name,
            //                            rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
            //                            inner join tblEmployee e on e.Id = ea.EmployeeId
            //                            inner join tblTransRoute r on r.Id = ea.RouteId
            //                            inner join tblRoteDestination rd on rd.RouteId = ea.RouteId
            //                             where ea.SchoolID = 1 and ea.UserType = 'Employee'


            //                             union
            //                          select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo, s.FirstName + ' ' + isnull(s.MiddleName, '') + ' ' + s.LastName as Name,
            //                            rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
            //                            inner join TBLStudent s on s.Id = ea.StudentId
            //                            inner join tblTransRoute r on r.Id = ea.RouteId
            //                            inner join tblRoteDestination rd on rd.RouteId = ea.RouteId
            //                             where ea.UserType = 'Student'

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();
                usr.Id = Convert.ToInt16(dr["Id"].ToString());
                usr.HubID = dr["RouteCode"].ToString();
                usr.Pickdrop = dr["PickAndDrop"].ToString();
                usr.StopTime = dr["StopTime"].ToString();

                usr.UserType = dr["UserType"].ToString();
                usr.StudentName = dr["Name"].ToString();
                usr.EmployeeName = dr["ReisterNo"].ToString();
                if (dr["Status"].ToString() == "True")
                {
                    usr.Status = "True";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";
                }
                else
                {
                    usr.Status = "False";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";
                }

                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/TransportApi/editAllocationDetailsById")]
        [System.Web.Http.HttpPost]
        public TransportAllocation editAllocationDetailsById(Parameters param)
        {

            TransportAllocation usr = new TransportAllocation();
            if (param.val1 == "Student")
            {
                sqlHelper obj = new sqlHelper();
                SqlDataReader dr = obj.GetReader(@"select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name,
                                            rd.PickAndDrop,rd.StopTime,ea.Status,c.CourseName Class,sec.SectionName from  tblTransportAllocation ea
                                            inner join TBLStudent s on s.Id=ea.StudentId
                                            inner join tblCourses c on c.Id=s.ClassID
                                            inner join tblSections sec on sec.Id=s.SectionID and sec.ClassId=s.ClassID
                                            inner join tblTransRoute r on r.Id=ea.RouteId
                                            inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                             where UserType='Student' and ea.Id=11");

                if (dr.Read())
                {

                    usr.Id = Convert.ToInt16(dr["Id"].ToString());
                    usr.HubID = dr["RouteCode"].ToString();
                    usr.Pickdrop = dr["PickAndDrop"].ToString();
                    usr.StopTime = dr["StopTime"].ToString();
                    usr.Class = dr["Class"].ToString() + " (" + dr["SectionName"].ToString() + ")";
                    usr.UserType = dr["UserType"].ToString();
                    usr.StudentName = dr["Name"].ToString();
                    usr.EmployeeName = dr["ReisterNo"].ToString();
                    if (dr["Status"].ToString() == "True")
                    {
                        usr.Status = "Active";
                        usr.Extra10 = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        usr.Status = "In-Active";
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";
                    }




                }


            }
            return usr;

        }

        [System.Web.Http.Route("api/TransportApi/sendSmsForAllTransportAllocationUsers")]
        [System.Web.Http.HttpPost]
        public string sendSmsForAllTransportAllocationUsers(Parameters param)
        {
            sqlHelper obj = new sqlHelper();
            if (param.val == "1")
            {
              
                DataTable dt = obj.getDataTable(@"select ea.Id,r.RouteCode,ea.UserType,e.Empcode ReisterNo,e.FirstName+' '+isnull(e.MiddleName,'')+' '+e.LastName   as Name,
                                        e.Mobile MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join tblEmployee e on e.Id=ea.EmployeeId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Employee'


                                         union 
                                         select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name,
                                        s.SMSmobileNo MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join TBLStudent s on s.Id=ea.StudentId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Student'");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MobileNo"].ToString() != "")
                    {
                        //     string phoneno = obj.ExecuteScaler("select Mobile from tblEmployee where Id=" + emp.Employeecode);
                        string strUrl = "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=78978f2548b73f5edb1db725fcf65127&message=" + param.val1 + "&senderId=DEMOOS&routeId=1&mobileNos=" + dr["MobileNo"].ToString() + "&smsContentType=english";
                        // string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=YourUserName:YourPassword&senderID=YourSenderID&    receipientno=1234567890&msgtxt=This is a test from mVaayoo API&state=4";
                        // Create a request object  
                        WebRequest request = HttpWebRequest.Create(strUrl);
                        // Get the response back  
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream s = (Stream)response.GetResponseStream();
                        StreamReader readStream = new StreamReader(s);
                        string dataString = readStream.ReadToEnd();
                        response.Close();
                        s.Close();
                        readStream.Close();
                    }
                }


            }
            else if (param.val == "2")
            {
                
                DataTable dt = obj.getDataTable(@"select ea.Id,r.RouteCode,ea.UserType,e.Empcode ReisterNo,e.FirstName+' '+isnull(e.MiddleName,'')+' '+e.LastName   as Name,
                                        e.Mobile MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join tblEmployee e on e.Id=ea.EmployeeId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Employee' and ea.RouteId=" + param.val2 + @"


                                         union 
                                         select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name,
                                        s.SMSmobileNo MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join TBLStudent s on s.Id=ea.StudentId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Student' and ea.RouteId=" + param.val2);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MobileNo"].ToString() != "")
                    {
                        //     string phoneno = obj.ExecuteScaler("select Mobile from tblEmployee where Id=" + emp.Employeecode);
                        string strUrl = "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=78978f2548b73f5edb1db725fcf65127&message=" + param.val1 + "&senderId=DEMOOS&routeId=1&mobileNos=" + dr["MobileNo"].ToString() + "&smsContentType=english";
                        // string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=YourUserName:YourPassword&senderID=YourSenderID&    receipientno=1234567890&msgtxt=This is a test from mVaayoo API&state=4";
                        // Create a request object  
                        WebRequest request = HttpWebRequest.Create(strUrl);
                        // Get the response back  
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream s = (Stream)response.GetResponseStream();
                        StreamReader readStream = new StreamReader(s);
                        string dataString = readStream.ReadToEnd();
                        response.Close();
                        s.Close();
                        readStream.Close();
                    }
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select ea.Id,r.RouteCode,ea.UserType,e.Empcode ReisterNo,e.FirstName+' '+isnull(e.MiddleName,'')+' '+e.LastName   as Name,
                                        e.Mobile MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join tblEmployee e on e.Id=ea.EmployeeId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Employee' and ea.RouteId=" + param.val2 +" and ea.DestinationId=" + param.val3 + @"


                                         union 
                                         select ea.Id,r.RouteCode,ea.UserType,s.RegNo ReisterNo,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name,
                                        s.SMSmobileNo MobileNo,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                        inner join TBLStudent s on s.Id=ea.StudentId
                                        inner join tblTransRoute r on r.Id=ea.RouteId
                                        inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                         where UserType='Student'and  ea.RouteId=" + param.val2 + " and ea.DestinationId=" + param.val3);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MobileNo"].ToString() != "")
                    {
                        //     string phoneno = obj.ExecuteScaler("select Mobile from tblEmployee where Id=" + emp.Employeecode);
                        string strUrl = "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=78978f2548b73f5edb1db725fcf65127&message=" + param.val1 + "&senderId=DEMOOS&routeId=1&mobileNos=" + dr["MobileNo"].ToString() + "&smsContentType=english";
                        // string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=YourUserName:YourPassword&senderID=YourSenderID&    receipientno=1234567890&msgtxt=This is a test from mVaayoo API&state=4";
                        // Create a request object  
                        WebRequest request = HttpWebRequest.Create(strUrl);
                        // Get the response back  
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream s = (Stream)response.GetResponseStream();
                        StreamReader readStream = new StreamReader(s);
                        string dataString = readStream.ReadToEnd();
                        response.Close();
                        s.Close();
                        readStream.Close();
                    }
                }
            }
            return "SMS Send Successfully Thank u!";

            //saveTransportAllocationDetails
        }


        [System.Web.Http.Route("api/TransportApi/getAllStudentByClassSectionIdForChangeTransportBySchool")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getAllStudentByClassSectionIdForChangeTransportBySchool(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select ea.StudentId,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name
                                            from  tblTransportAllocation ea
                                            inner join TBLStudent s on s.Id=ea.StudentId and s.ClassID=" + std.ClassID + " and s.SectionID=" + std.SectionID + @"
                                            where UserType='Student' and ea.Status=1 and  ea.SchoolID='"+std.SchoolID+"'");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["StudentId"].ToString());

                usr.StudentName = dr["Name"].ToString();

                list.Add(usr);



            }



            return list.ToArray();

        }



        [System.Web.Http.Route("api/TransportApi/getAllStudentByClassSectionIdForChangeTransport")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getAllStudentByClassSectionIdForChangeTransport(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>(); 
         
                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable(@"select ea.StudentId,s.FirstName+' '+isnull(s.MiddleName,'')+' '+s.LastName   as Name
                                            from  tblTransportAllocation ea
                                            inner join TBLStudent s on s.Id=ea.StudentId and s.ClassID="+std.ClassID +" and s.SectionID="+std.SectionID+@"
                                            where UserType='Student' and ea.Status=1");

              foreach(DataRow dr in dt.Rows)
                {
                    TransportAllocation usr = new TransportAllocation();

                    usr.Id = Convert.ToInt16(dr["StudentId"].ToString());
                  
                    usr.StudentName = dr["Name"].ToString();

                    list.Add(usr);



                }


             
            return list.ToArray() ;

        }

        [System.Web.Http.Route("api/TransportApi/getDesignationFromStaudnetbySchool")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getDesignationFromStaudnetbySchool(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select rd.Id,
                                         rd.PickAndDrop,rd.StopTime from  tblTransportAllocation ea
                                         inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                        where UserType='Student' and ea.StudentId=" + std.SectionID + " and ea.SchoolID='"+std.SchoolID+"' and ea.IsDeleted is null and ea.Status=1 ");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["Id"].ToString());

                usr.StudentName = dr["PickAndDrop"].ToString();

                list.Add(usr);



            }



            return list.ToArray();

        }



        [System.Web.Http.Route("api/TransportApi/getDesignationFromStaudnet")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getDesignationFromStaudnet(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select rd.Id,
                                         rd.PickAndDrop,rd.StopTime from  tblTransportAllocation ea
                                         inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                        where UserType='Student' and ea.StudentId="+std.SectionID+"  and ea.Status=1 ");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["Id"].ToString());

                usr.StudentName = dr["PickAndDrop"].ToString();

                list.Add(usr);



            }



            return list.ToArray();

        }




        [System.Web.Http.Route("api/TransportApi/getAllEmployeeByDepIdChangeTransport")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getAllEmployeeByDepIdChangeTransport(List<string> Id)
        {
            int idd = Convert.ToInt16(Id[0]);
            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select e.Id,e.FirstName+' '+isnull(e.MiddleName,'')+' '+e.LastName   as Name
                                                    from  tblTransportAllocation ea
                                                    inner join tblEmployee e on e.Id=ea.EmployeeId and e.DeptID="+idd+@"
                                                              where ea.UserType='Employee'  and ea.Status=1");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["Id"].ToString());

                usr.EmployeeName = dr["Name"].ToString();

                list.Add(usr);



            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/TransportApi/getDesignationFromEmployeebySchool")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getDesignationFromEmployeebySchool(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select  rd.Id,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                              inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                             where UserType='Employee' and ea.EmployeeId=" + std.SectionID + " and ea.Status=1 and ea.SchoolID='"+std.SchoolID+ "' and ea.IsDeleted is null  ");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["Id"].ToString());

                usr.StudentName = dr["PickAndDrop"].ToString();

                list.Add(usr);



            }



            return list.ToArray();

        }

        [System.Web.Http.Route("api/TransportApi/getDesignationFromEmployee")]
        [System.Web.Http.HttpPost]
        public TransportAllocation[] getDesignationFromEmployee(Student std)
        {

            List<TransportAllocation> list = new List<TransportAllocation>();

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select  rd.Id,rd.PickAndDrop,rd.StopTime,ea.Status from  tblTransportAllocation ea
                                              inner join tblRoteDestination rd on rd.RouteId=ea.RouteId
                                             where UserType='Employee' and ea.EmployeeId="+std.SectionID+" and ea.Status=1 ");

            foreach (DataRow dr in dt.Rows)
            {
                TransportAllocation usr = new TransportAllocation();

                usr.Id = Convert.ToInt16(dr["Id"].ToString());

                usr.StudentName = dr["PickAndDrop"].ToString();

                list.Add(usr);



            }



            return list.ToArray();

        }

        [System.Web.Http.Route("api/TransportApi/changeTransportRouteDestinationBySchool")]
        [System.Web.Http.HttpPost]
        public string changeTransportRouteDestinationBySchool(TransportAllocation vd)
        {
            if (vd.UserType == "Student")
            {
                var result = db.tblTransportAllocations.SingleOrDefault(s => s.StudentId == vd.EmployeeName && s.SchoolID==vd.SchoolID);
                result.RouteId = vd.HubID;
                result.DestinationId = vd.VehicleID;
                result.SchoolID = vd.SchoolID;
                db.SaveChanges();
                return "Route Destinage Allocated Successfully";
            }
            else
            {
                var result = db.tblTransportAllocations.SingleOrDefault(s => s.EmployeeId == vd.EmployeeName && s.SchoolID == vd.SchoolID);
                result.RouteId = vd.HubID;
                result.DestinationId = vd.VehicleID;
                result.SchoolID = vd.SchoolID;
                db.SaveChanges();
                return "Route Destinage Allocated Successfully";
            }

        }





        //[System.Web.Http.Route("api/TransportApi/changeTransportRouteDestination")]
        //[System.Web.Http.HttpPost]
        //public string changeTransportRouteDestination(TransportAllocation vd)
        //{
        //    if (vd.UserType == "Student")
        //    {
        //        var result = db.tblTransportAllocations.SingleOrDefault(s => s.StudentId == vd.EmployeeName);
        //        result.RouteId = vd.route;
        //        result.DestinationId = vd.Destination;
        //        db.SaveChanges();
        //        return "Route Destinage Allocated Successfully";
        //    }
        //    else {
        //        var result = db.tblTransportAllocations.SingleOrDefault(s => s.EmployeeId == vd.EmployeeName);
        //        result.RouteId = vd.route;
        //        result.DestinationId = vd.Destination;
        //        db.SaveChanges();
        //        return "Route Destinage Allocated Successfully";
        //    }

        //}

        //[System.Web.Http.Route("api/TransportApi/AsignmigpsPasswordtoSchool")]
        //[System.Web.Http.HttpPost]
        //public string AsignmigpsPasswordtoSchool(migpsGpsTrackingSystem val)
        //{
        //    if (val.Id==0)
        //    {
        //        var CheckSchoolID = db.migpsGpsTrackingSys.SingleOrDefault(X => X.SchoolID == val.SchoolID);
        //        if (CheckSchoolID != null)
        //        {

        //            return "School Details Already Exist";
        //        }
        //        //else
        //        //{
        //        //    migpsGpsTrackingSy usr = new migpsGpsTrackingSy();
        //        //    usr.UserId = val.UserId;
        //        //    usr.Password = val.Password;
        //        //    usr.ValidationKey = val.ValidationKey;
        //        //    usr.SchoolID = val.SchoolID;
        //        //    db.migpsGpsTrackingSys.Add(usr);
        //        //    db.SaveChanges();
        //        //    return "School migps Details Assigned Sucessfully";
        //        //}

              

        //    }
        //    else
        //    {
        //        var CheckSchoolID = db.migpsGpsTrackingSys.SingleOrDefault(X => X.SchoolID == val.SchoolID && X.Id!=val.Id);
        //        if (CheckSchoolID != null)
        //        {

        //            return "Selected School Details Already Exist";
        //        }
        //        else
        //        {
        //            var UpdateDetails = db.migpsGpsTrackingSys.Where(x => x.Id == val.Id).FirstOrDefault();
        //            UpdateDetails.SchoolID = val.SchoolID;
        //            UpdateDetails.UserId = val.UserId;
        //            UpdateDetails.Password = val.Password;
        //            UpdateDetails.ValidationKey = val.ValidationKey;
        //            db.SaveChanges();
        //            return "School migps Details updated Sucessfully";
        //        }
               
        //    }

          
          

          


        //}

       
        [System.Web.Http.Route("api/TransportApi/viewAllmigpsloginDetails")]
        [System.Web.Http.HttpPost]
        public migpsGpsTrackingSystem[] viewAllmigpsloginDetails()
        {
            int avi = 0;
            List<migpsGpsTrackingSystem> list = new List<migpsGpsTrackingSystem>();
            //getmigpsDetails
            var result = db.migpsGpsTrackingSys.ToList();
            if (result!=null)
            {
                foreach (var a in result)
                {
                    avi++;
                    migpsGpsTrackingSystem usr = new migpsGpsTrackingSystem();
                    usr.Id = a.Id;
                    usr.UserId = a.UserId;
                    usr.Password = a.Password;
                    usr.ValidationKey = a.ValidationKey;              
                    int SchoolIDD = Convert.ToInt32(a.SchoolID);
                    string Schoolname = db.tblSchoolDetails.SingleOrDefault(x => x.ID == SchoolIDD).School;
                    usr.School = Schoolname;
                    usr.Sno = avi.ToString();
                    list.Add(usr);
                   

                }
            }
         

            return list.ToArray();

        }
        [System.Web.Http.Route("api/TransportApi/EditmigpsPasswordtoSchool")]
        [System.Web.Http.HttpPost]
        public migpsGpsTrackingSystem EditmigpsPasswordtoSchool(List<string> id)
        {
            int idd = Convert.ToInt32( id[0]);
            migpsGpsTrackingSystem usr = new migpsGpsTrackingSystem();
            var a = db.migpsGpsTrackingSys.SingleOrDefault(u => u.Id == idd);
            usr.Id = a.Id;
            usr.UserId = a.UserId;
            usr.Password = a.Password;
            usr.ValidationKey = a.ValidationKey;           
            usr.SchoolID = a.SchoolID;
            return usr;

        }
        [System.Web.Http.Route("api/TransportApi/CheckTransportFACILITY")]
        [System.Web.Http.HttpPost]
        public string CheckTransportFACILITY (List<string> id)
        {
            string Vehicleid = "";
            string Vehicleno = "";
            string ValidationKey = "";
            string StudentIDD = Convert.ToString(id[0]);
            var CHECK = db.tblTransportAllocations.Where(u => u.StudentId == StudentIDD && u.Status==true && u.IsDeleted==null).FirstOrDefault();
            if (CHECK!=null)
            {
                
                int vid = Convert.ToInt32(CHECK.DestinationId);
                int schoolidd = Convert.ToInt32(CHECK.SchoolID);
                var vehicleno = db.tblTransportVehicalDetails.Where(u => u.Id == vid && u.SchoolID == schoolidd).FirstOrDefault();
                if (vehicleno!=null)
                {
                    Vehicleid = vehicleno.Id.ToString();
                    Vehicleno = vehicleno.VehicalNo;
                }
                else
                {
                    Vehicleid = "-1";
                    Vehicleno = "-1";
                }
                string SchoolIDD = Convert.ToString(CHECK.SchoolID);
                var CheckVKEY = db.migpsGpsTrackingSys.Where(u => u.SchoolID == SchoolIDD).FirstOrDefault();
                if(CheckVKEY !=null)
                {
                    ValidationKey = CheckVKEY.ValidationKey;
                }
                else
                {
                    ValidationKey = "-1";
                }

                return Vehicleid+"&="+ Vehicleno +"&=" +ValidationKey;
            }
            else
            {
                 Vehicleid = "-1";
                ValidationKey = "-1";
                Vehicleno = "-1";
                return Vehicleid + "&=" + Vehicleno + "&=" + ValidationKey;
            }

            

        }

        [System.Web.Http.Route("api/TransportApi/CheckTransportFACILITYEmployee")]
        [System.Web.Http.HttpPost]
        public string CheckTransportFACILITYEmployee(List<string> id)
        {
            string Vehicleid = "";
            string Vehicleno = "";
            string ValidationKey = "";
            string EmployeeIDD = Convert.ToString(id[0]);
            var CHECK = db.tblTransportAllocations.Where(u => u.EmployeeId == EmployeeIDD && u.Status == true && u.IsDeleted == null).FirstOrDefault();
            if (CHECK != null)
            {

                int vid = Convert.ToInt32(CHECK.DestinationId);
                int schoolidd = Convert.ToInt32(CHECK.SchoolID);
                var vehicleno = db.tblTransportVehicalDetails.Where(u => u.Id == vid && u.SchoolID == schoolidd).FirstOrDefault();
                if (vehicleno != null)
                {
                    Vehicleid = vehicleno.Id.ToString();
                    Vehicleno = vehicleno.VehicalNo;
                }
                else
                {
                    Vehicleid = "-1";
                    Vehicleno = "-1";
                }
                string SchoolIDD = Convert.ToString(CHECK.SchoolID);
                var CheckVKEY = db.migpsGpsTrackingSys.Where(u => u.SchoolID == SchoolIDD).FirstOrDefault();
                if (CheckVKEY != null)
                {
                    ValidationKey = CheckVKEY.ValidationKey;
                }
                else
                {
                    ValidationKey = "-1";
                }

                return Vehicleid + "&=" + Vehicleno + "&=" + ValidationKey;
            }
            else
            {
                Vehicleid = "-1";
                ValidationKey = "-1";
                Vehicleno = "-1";
                return Vehicleid + "&=" + Vehicleno + "&=" + ValidationKey;
            }



        }

       [System.Web.Http.Route("api/TransportApi/GETVehicleidfromNo")]
       [System.Web.Http.HttpPost]
       public string GETVehicleidfromNo(List<string> regno)
        {
            string vehicleid = "";
            string regnoo = regno[0].ToString();
            tblTransportVehicalDetail usr = new tblTransportVehicalDetail();
            var check = db.tblTransportVehicalDetails.Where(u=>u.VehicalNo== regnoo).FirstOrDefault();
            if (check!=null)
            {
                vehicleid = check.Id.ToString();
            }
            else
            {
                vehicleid = "-1";
            }
            return vehicleid;
        }

        [System.Web.Http.Route("api/TransportApi/GetStudentTransportLocation")]
        [System.Web.Http.HttpPost]
        public vehicleLocationapp GetStidentTransportLocation(locationparameters val)
        {
            vehicleLocationapp obj = new vehicleLocationapp();

            if (  "".Equals(val.SchoolID) || val.SchoolID=="0" || val.SchoolID==null )
            {

                obj.status = false;
                obj.message = "Please Enter SchoolID";
               // obj.data = USR;

            }
            else if ( "".Equals(val.StudentID)|| val.StudentID=="0" || val.StudentID == null )
            {
                obj.status = false;
                obj.message = "Please Enter Student ID";
                //  obj.data = USR;

            }
            else
            {
               
                vehicleLocation USR = new Models.vehicleLocation();
                var loginDetails = db.migpsGpsTrackingSys.Where(u => u.SchoolID == val.SchoolID).FirstOrDefault();
                if (loginDetails != null)
                {
                    string userid = loginDetails.UserId;
                    string password = loginDetails.Password;
                    string validationkey = loginDetails.ValidationKey;//validationkey
                    var Vehicle = db.tblTransportAllocations.Where(x => x.StudentId == val.StudentID).FirstOrDefault();
                    if (Vehicle != null)
                    {
                        string vehicleid = Vehicle.DestinationId;
                        int vehicleidd = Convert.ToInt32(vehicleid);
                        var vehicleno = db.tblTransportVehicalDetails.Where(v => v.Id == vehicleidd).FirstOrDefault();//regno
                        if (vehicleno != null)
                        {

                            string regno = vehicleno.VehicalNo.ToString();
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("RegdNo={0}", regno);
                            sbPostData.AppendFormat("&ValidationKey={0}", validationkey);
                            //string sendSMSUri = "http://gps.migps.in/api/MiGpsApi/GetCurrentLocation";
                            //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                            ////Prepare and Add URL Encoded data
                            //UTF8Encoding encoding = new UTF8Encoding();

                            //byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                            ////Specify post method
                            //httpWReq.Method = "GET";
                            //httpWReq.ContentType = "application/x-www-form-urlencoded";
                            //WebResponse responce = httpWReq.GetResponse();
                            WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/GetCurrentLocation?" + sbPostData.ToString());
                            // Set the Method property of the request to POST.  
                            httpWReq.Method = "GET";

                            // Create POST data and convert it to a byte array.  
                            //string postData = sbPostData.ToString();
                            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);





                            //vehicleLocation USR = new vehicleLocation();
                            //Get the response
                            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response1.GetResponseStream());
                            string jsonData = reader.ReadToEnd(); //JSON OBJECT 
                            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(vehicleLocation));
                            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
                            stream.Position = 0;
                            vehicleLocation USR1 = (vehicleLocation)jsonSerializer.ReadObject(stream);
                            USR.RegdNo = USR1.RegdNo;
                            USR.Location = USR1.Location;
                            USR.Lattitude = USR1.Lattitude;
                            USR.Longitude = USR1.Longitude;
                            USR.DeviceNumber = USR1.DeviceNumber;
                            USR.RDateTime = USR1.RDateTime.ToString();
                            USR.AvailableStatus = USR1.AvailableStatus;
                            USR.Speed = USR1.Speed;
                            USR.IgnitionStatus = USR1.IgnitionStatus;
                            USR.DateCreated = Convert.ToDateTime(USR1.RDateTime).ToString("dd MMMM yyyy");
                            USR.TimeCreated = Convert.ToDateTime(USR1.RDateTime).ToString("hh:mm tt");
                            obj.status = true;
                            obj.message = "Success";
                            obj.data = USR;



                            reader.Close();
                            response1.Close();



                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Assigned Vehicle not Exit ";
                        }

                    }
                    else
                    {
                        obj.status = false;
                        obj.message = "Vehicle Not allocated to Student ";
                    }

                }
                else
                {
                    obj.status = false;
                    obj.message = "School Not Registered in GTS ";

                }


            }


            return obj;

        }
        [System.Web.Http.Route("api/TransportApi/GetStidentTransportLocation1")]
        [System.Web.Http.HttpPost]
        public vehicleLocationapp1 GetStidentTransportLocation1(locationparameters val)
        {
            vehicleLocationapp1 obj = new vehicleLocationapp1();

            if ("".Equals(val.SchoolID) || val.SchoolID == "0" || val.SchoolID == null)
            {

                obj.status = false;
                obj.message = "Please Enter SchoolID";
                // obj.data = USR;

            }
            else if ("".Equals(val.StudentID) || val.StudentID == "0" || val.StudentID == null)
            {
                obj.status = false;
                obj.message = "Please Enter Student ID";
                //  obj.data = USR;

            }
            else
            {
                vehicleLocation1 A = new Models.vehicleLocation1();
                A.CurrentLocation = new Getvehiclelocation();
              

              
                var loginDetails = db.migpsGpsTrackingSys.Where(u => u.SchoolID == val.SchoolID).FirstOrDefault();
                if (loginDetails != null)
                {
                    string userid = loginDetails.UserId;
                    string password = loginDetails.Password;
                    string validationkey = loginDetails.ValidationKey;//validationkey
                    var Vehicle = db.tblTransportAllocations.Where(x => x.StudentId == val.StudentID).FirstOrDefault();
                    if (Vehicle != null)
                    {
                        string vehicleid = Vehicle.DestinationId;
                        int vehicleidd = Convert.ToInt32(vehicleid);
                        var vehicleno = db.tblTransportVehicalDetails.Where(v => v.Id == vehicleidd).FirstOrDefault();//regno
                        if (vehicleno != null)
                        {
                            A.HubDetails = new List<Getvehiclehub>();

                            sqlHelper obj1 = new sqlHelper();
                            DataTable dt = obj1.getDataTable(@"SELECT *FROM  tblHubDetails where VehicleID='" + vehicleidd + "' and SchoolID='" + val.SchoolID + "' and IsDeleted is null ORDER BY ABS(HubLat) + ABS(HubLong) ASC");                          
                          
                              foreach (DataRow a in dt.Rows)
                                {
                                    Getvehiclehub usr = new Getvehiclehub();                                
                                    usr.HubLat = a["HubLat"].ToString();
                                    usr.HubLong = a["HubLong"].ToString(); ;
                                    usr.HubAddress = a["HubAddress"].ToString();
                                    usr.HubName = a["HubName"].ToString();
                                    A.HubDetails.Add(usr);
                                }
                           
                          
                          



                            string regno = vehicleno.VehicalNo.ToString();
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("RegdNo={0}", regno);
                            sbPostData.AppendFormat("&ValidationKey={0}", validationkey);
                            //string sendSMSUri = "http://gps.migps.in/api/MiGpsApi/GetCurrentLocation";
                           
                            WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/GetCurrentLocation?" + sbPostData.ToString());
                            // Set the Method property of the request to POST.  
                            httpWReq.Method = "GET";

                            //Get the response
                            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response1.GetResponseStream());
                            string jsonData = reader.ReadToEnd(); //JSON OBJECT 
                            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(vehicleLocation));
                            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
                            stream.Position = 0;
                            vehicleLocation USR1 = (vehicleLocation)jsonSerializer.ReadObject(stream);
                            A.CurrentLocation.RegdNo = USR1.RegdNo;
                            A.CurrentLocation.Location = USR1.Location;
                            A.CurrentLocation.Lattitude = USR1.Lattitude;
                            A.CurrentLocation.Longitude = USR1.Longitude;
                            A.CurrentLocation.DeviceNumber = USR1.DeviceNumber;
                            A.CurrentLocation.RDateTime = USR1.RDateTime.ToString();
                            A.CurrentLocation.AvailableStatus = USR1.AvailableStatus;
                            A.CurrentLocation.Speed = USR1.Speed;
                            A.CurrentLocation.IgnitionStatus = USR1.IgnitionStatus;

                            obj.status = true;
                            obj.message = "Success";
                            obj.data = A;



                            reader.Close();
                            response1.Close();



                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Assigned Vehicle not Exit ";
                        }

                    }
                    else
                    {
                        obj.status = false;
                        obj.message = "Vehicle Not allocated to Student ";
                    }

                }
                else
                {
                    obj.status = false;
                    obj.message = "School Not Registered in GTS ";

                }


            }


            return obj;

        }
        [System.Web.Http.Route("api/TransportApi/getLocation")]
        [System.Web.Http.HttpPost]
        public CurrentLocation getLocation(LocationParameters val)

        {
            CurrentLocation USR = new CurrentLocation();

           
            string regno = val.RegdNo.ToString();
            string validationkey = val.ValidationKey.ToString();
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("RegdNo={0}", regno);
            sbPostData.AppendFormat("&ValidationKey={0}", validationkey);
            WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/GetCurrentLocation?" + sbPostData.ToString());            
            httpWReq.Method = "GET";
            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response1.GetResponseStream());
            string jsonData = reader.ReadToEnd(); 
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(CurrentLocation));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            stream.Position = 0;
            CurrentLocation USR1 = (CurrentLocation)jsonSerializer.ReadObject(stream);
            USR.RegdNo = USR1.RegdNo;
            USR.Location = USR1.Location;
            USR.Lattitude = USR1.Lattitude;
            USR.Longitude = USR1.Longitude;
            USR.DeviceNumber = USR1.DeviceNumber;
            USR.RDateTime = USR1.RDateTime.ToString();
            USR.AvailableStatus = USR1.AvailableStatus;
            USR.Speed = USR1.Speed;
            USR.IgnitionStatus = USR1.IgnitionStatus;
            USR.AcStatus = USR1.AcStatus;
            USR.DayDistance = USR1.DayDistance;

            return USR ;


        }
        //[System.Web.Http.Route("api/TransportApi/GETMiGpsloginDetails")]
        //[System.Web.Http.HttpPost]
        //public migpsGpsTrackingSy GETMiGpsloginDetails(List<string> id)
        //{
        //    migpsGpsTrackingSy usr = new migpsGpsTrackingSy();
        //    string SchoolID = id[0];
        //    var GetLoginDetails = db.migpsGpsTrackingSys.SingleOrDefault(x => x.SchoolID == SchoolID);
        //    if (GetLoginDetails != null)
        //    {
        //        usr.UserId = GetLoginDetails.UserId;
        //        usr.Password = GetLoginDetails.Password;
        //        usr.ValidationKey = GetLoginDetails.ValidationKey;
        //    }
        //    else
        //    {
        //        usr.UserId = "1";
        //    }

        //    return usr;


        //}
        [System.Web.Http.Route("api/TransportApi/ValidateUser")]
        [System.Web.Http.HttpPost]
        //public migpsGpsTrackingSy ValidateUser(ValidateUserPara value)
        //{
        //    migpsGpsTrackingSy USR = new migpsGpsTrackingSy();

        //    string UserId = value.UserId.ToString();
        //    string Password = value.Password.ToString();
        //    string ValidationKey = value.ValidationKey.ToString();
        //    StringBuilder sbPostData = new StringBuilder();
        //    sbPostData.AppendFormat("UserId={0}", UserId);
        //    sbPostData.AppendFormat("&Password={0}", Password);
        //    sbPostData.AppendFormat("&ValidationKey={0}", ValidationKey);
        //    WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/ValidateUser?" + sbPostData.ToString());
        //    httpWReq.Method = "GET";
        //    HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
        //    StreamReader reader = new StreamReader(response1.GetResponseStream());
        //    string jsonData = reader.ReadToEnd();
        //    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(migpsGpsTrackingSy));
        //    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
        //    stream.Position = 0;
        //    migpsGpsTrackingSy USR1 = (migpsGpsTrackingSy)jsonSerializer.ReadObject(stream);
        //    USR.Id = USR1.Id;





        //    return USR;
        //}
        //GetStatusCount


        [System.Web.Http.Route("api/TransportApi/GetStatusCount")]
        //[System.Web.Http.HttpPost]
        public StatusParameters GetStatusCount(statusCountparams value)
        {
            StatusParameters USR = new StatusParameters();


            string  UserId = value.ID.ToString();
            string ValidationKey = value.ValidationKey.ToString();
           
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("UserId={0}", UserId);
            sbPostData.AppendFormat("&ValidationKey={0}", ValidationKey);
            
            WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/GetStatusCount?" + sbPostData.ToString());
            httpWReq.Method = "GET";
            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response1.GetResponseStream());
            string jsonData = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(StatusParameters));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            stream.Position = 0;
            StatusParameters USR1 = (StatusParameters)jsonSerializer.ReadObject(stream);
            USR.Moving = USR1.Moving;
            USR.Idle = USR1.Idle;
            USR.NR = USR1.NR;
            USR.NGF = USR1.NGF;
            USR.Delay = USR1.Delay;
            USR.BD = USR1.BD;
            USR.Total = USR1.Total;







            return USR;

        }
        //GetCurrentLocation
        [System.Web.Http.Route("api/TransportApi/GetCurrentLocationAdmin")]
        [System.Web.Http.HttpPost]
        public CurrentLocation GetCurrentLocationAdmin(LocationParameters val)
        {
            CurrentLocation USR = new CurrentLocation();
            string RegdNo = val.RegdNo;
            string ValidationKey = val.ValidationKey;
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("RegdNo={0}", RegdNo);
            sbPostData.AppendFormat("&ValidationKey={0}", ValidationKey);

            WebRequest httpWReq = WebRequest.Create("http://gps.migps.in/api/MiGpsApi/GetCurrentLocation?" + sbPostData.ToString());
            httpWReq.Method = "GET";
            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response1.GetResponseStream());
            string jsonData = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(CurrentLocation));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            stream.Position = 0;
            CurrentLocation USR1 = (CurrentLocation)jsonSerializer.ReadObject(stream);
            USR.RegdNo = USR1.RegdNo;
            USR.Location = USR1.Location;
            USR.Lattitude = USR1.Lattitude;
            USR.Longitude = USR1.Longitude;
            USR.DeviceNumber = USR1.DeviceNumber;
            USR.RDateTime = USR1.RDateTime.ToString();
            USR.AvailableStatus = USR1.AvailableStatus;
            USR.Speed = USR1.Speed;
            USR.IgnitionStatus = USR1.IgnitionStatus;
            USR.AcStatus = USR1.AcStatus;
            USR.DayDistance = USR1.DayDistance;

            return USR;

        }
        //GetVehicleList
        [System.Web.Http.Route("api/TransportApi/GetVehicleList")]
        [System.Web.Http.HttpPost]
        public GetVehicleList[] GetVehicleList(GetVehicleListPara val)
        {
            GetVehicleList avi = new GetVehicleList();
            List<GetVehicleList> list = new List<GetVehicleList>();
            string UserId = val.UserId;
            string Status = val.Status;
            string ValidationKey = val.ValidationKey;
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("UserId={0}", UserId);
            sbPostData.AppendFormat("&Status={0}", Status);
            sbPostData.AppendFormat("&ValidationKey={0}", ValidationKey);

            WebRequest httpWReq = WebRequest.Create("http://api.migps.in/api/MiGpsApi/GetVehicleList?" + sbPostData.ToString());
            httpWReq.Method = "GET";
            HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response1.GetResponseStream());
            string jsonData = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(GetVehicleList[]));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            stream.Position = 0;
            GetVehicleList[]  USR11 = (GetVehicleList[])jsonSerializer.ReadObject(stream);
            foreach (var USR1 in USR11)
            {
                avi.IgnitionStatus = USR1.IgnitionStatus;
                avi.PowerStatus = USR1.PowerStatus;
                avi.DeviceNumber = USR1.DeviceNumber;
                avi.DeviceMobileNo = USR1.DeviceMobileNo;
                avi.RegdNo = USR1.RegdNo;
                avi.AvailableStatus = USR1.AvailableStatus;
                avi.DayDistance = USR1.DayDistance;
                avi.Speed = USR1.Speed;
                avi.Location = USR1.Location;
                avi.RDateTime = USR1.RDateTime;
                avi.Location = USR1.Location;
                avi.RDateTime = USR1.RDateTime;
                list.Add(avi);
            }
          
            return list.ToArray();

        }
        }
}
