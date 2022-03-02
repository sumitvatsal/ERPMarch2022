using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace schoolERP_BLL
{
  public  class VehicleDetails
    {
        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string TotalSeats { get; set; }
        public string AllowedSeats { get; set; }
        public string OwnershipType { get; set; }
        public string InsuranceExpire { get; set; }
        public string PolutionExpire { get; set; }

        public string InsuranceExpire1 { get; set; }
        public string PolutionExpire1 { get; set; }
        public string FitnessExpiry { get; set; }
        public string TrackNo { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string Sno { get; set; }
        public bool IsActive { get; set; }
        public string status { get; set; }
    }

    public class DriverDetailss {

        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string Name { get; set; }
        public string PresentAddress { get; set; }
        public string PramnentAddress  { get; set; }
        public string DOB { get; set; }
        public string Phoneno { get; set; }
        public string LicenseNo { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string Sno { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
    public class DriverDetailss1
    {

        public int Id { get; set; }
        public Nullable<int> VehicleId { get; set; }
        public int DriveId { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string DOB { get; set; }
        public string phone { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string LicenseExpDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class RouteDetails
    {

        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public string RouteCode { get; set; }
        public string StartPlace { get; set; }
        public string StartPlaceLatt { get; set; }
        public string StartPlaceLongt { get; set; }
        public string EndPlace { get; set; }
        public string EndPlaceLatt { get; set; }
        public string EndPlaceLongt { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }    
        public string sno { get; set; }
        public bool IsActive { get; set; }
       
       
    }
    public class HubDetails
    {
        public int ID { get; set; }
        public string VehicleID { get; set; }
        public string HubName { get; set; }
        public string HubAddress { get; set; }
        public string HubLat { get; set; }
        public string HubLong { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string Sno { get; set; }
    }
    public class DestinationDetails
    {
        public int Id { get; set; }
        public string Route { get; set; }
        public string Pickdrop { get; set; }
        public string StopTime { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
    }

    public class StudentTransportAllocation {
        public int Id { get; set; }

        public string route1 { get; set; }
        public string Destination1 { get; set; }
        public string UserType1 { get; set; }
        public string Class1{ get; set; }
        public string Section1 { get; set; }
        public string StudentName { get; set; }
        public string Department1 { get; set; }
        public string EmployeeName1 { get; set; }
    }

    public class TransportAllocation
    {
        public int Id { get; set; }
        public string Hubname { get; set; }
        public string Hubaddress { get; set; }
        public string HubID { get; set; }
        public string VehicleID { get; set; }
        public string UserType { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        public string UserName { get; set; }
        public string StopTime { get; set; }
        public string Pickdrop { get; set; }
        public string Status { get; set; }
        public string Extra10 { get; set; }
        public int SchoolID { get; set; }
        public bool Status1 { get; set; }
        public string lat { get; set; }
        public string longi { get; set; }
        public string UserID { get; set; }
        public List<StudentTransportAllocation> stu { get; set; }



    }
}
