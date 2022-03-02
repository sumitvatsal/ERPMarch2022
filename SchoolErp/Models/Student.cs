using schoolERP_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Models
{



    public class StudentAPP
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string ClassID { get; set; }
        public string SectionId { get; set; }
        public string StudentId { get; set; }
        public string SchoolID { get; set; }
        public string ClassTeacherID { get; set; }
        public StudentAttendenceDetailsAPP data { get; set; }
        //public DEMOATTENDENCE data1 { get; set; }
    }
    public class feestructureapp
    {
        public bool status { get; set; }
        public string message { get; set; }
        public feestructure data { get; set; }

    }
    public class feestructure
    {
        public int StudentID { get; set; }
        public int AcademicYear { get; set; }
        public string CurrentMonth { get; set; }
        public List<feestructure1> FeeDetails { get; set; }
        public attendencee attendence { get; set; }
        //public  List< StudentAttendenceDetailsAPP1> monthlyattendence { get; set; }
        public string SchoolID { get; set; }
        //public int Present { get; set; }
        //public int Absent { get; set; }
        //public int leave { get; set; }
        //public string Holidays { get; set; }
        //public int TotalDays { get; set; }

    }
    public class feestructure1
    {

        public int TotalFeeAmount { get; set; }
        public int PaidFeeAmount { get; set; }
        public int DueAmount { get; set; }
    }

    public class attendencee
    {
        public int Present { get; set; }
        public int Absent { get; set; }
        public int leave { get; set; }
        public int Holidays { get; set; }
        public int TotalDays { get; set; }
        public List<StudentAttendenceDetailsAPP1> monthlyattendence { get; set; }

    }
    public class Studentcount
    {
        public int StudentCount { get; set; }
    }
    public class StudentAdmitCard
    {
        public int ID { get; set;}
        public string SchoolName { get; set; }
        public string SchoolLogo { get; set; }
        public string SchoolAddress { get; set; }
        public string AffilatedFrom { get; set; }
        public string SchoolPhoneNO { get; set; }
        public string SchoolEmailID { get; set; }
        public string District { get; set; }

        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public DateTime DOB { get; set; }
        public string StudentAddress { get; set; }
        public string country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Mobilenumber { get; set; }
        public string Gender { get; set; }
        public string Pincode { get; set; }
        public string StudentImage { get; set; }

    }



    public class stdfee1
    {
        public int ID { get; set; }
        public int VouchetID { get; set; }
        public decimal Amount { get; set; }
        public decimal Concession { get; set; }
        public decimal Total { get; set; }
        public decimal Nettotal { get; set; }
        public decimal Paid { get; set; }
        public decimal balance { get; set; }
        public string DocNo { get; set; }
        public System.DateTime dated { get; set; }

        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }

        [Display(Name = "Registration No.")]
        public string RegNo { get; set; }

        [Display(Name = "Joining Date")]
        public Nullable<System.DateTime> JoiningDate { get; set; }

        public Nullable<int> ClassID { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }

        [Display(Name = "Academic Year")]
        public int SectionID { get; set; }

        [Display(Name = "Section")]
        public string Section { get; set; }

        [Display(Name = "Roll No.")]
        public string RollNo { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "DOB")]
        public Nullable<System.DateTime> DOB { get; set; }

        public Nullable<int> GenderID { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Birth Place")]
        public string BirthPlace { get; set; }

        [Display(Name = "Religion")]
        public string Religion { get; set; }
        public Nullable<int> ReligionID { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Mother Tongue")]
        public string MotherTongue { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
        public Nullable<int> CategoryID { get; set; }

        [Display(Name = "Caste")]
        public string Caste { get; set; }
        public int CasteID { get; set; }

        [Display(Name = "Aadhaar No")]
        public string AadharNo { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Corresponding Address")]
        public string CorrespondAddress { get; set; }

        [Display(Name = "Pincode")]
        public decimal pincode { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }
        public Nullable<int> CountryID { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }
        public Nullable<int> StateID { get; set; }

        [Display(Name = "Telephone")]
        public Nullable<decimal> phone { get; set; }

        [Display(Name = "Mobile No.")]
        public string mobileNo { get; set; }

        [Display(Name = "Email")]
        public string emailID { get; set; }

        [Display(Name = "Select Image")]
        public string PicPath { get; set; }

        [Display(Name = "Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mobile No.")]
        public string FMobile { get; set; }

        [Display(Name = "Job")]
        public string FJob { get; set; }

        [Display(Name = "Email")]
        public string Fmail { get; set; }

        [Display(Name = "Academic Year")]
        public string FQualification { get; set; }
        public Nullable<int> FQualificationID { get; set; }

        [Display(Name = "Aadhaar No.")]
        public string FAdharNo { get; set; }

        [Display(Name = "Name")]
        public string MotherName { get; set; }

        [Display(Name = "Mobile No.")]
        public string Mmobile { get; set; }

        [Display(Name = "Job")]
        public string MJob { get; set; }

        [Display(Name = "Email")]
        public string Mmail { get; set; }

        [Display(Name = "Academic Year")]
        public string MQualification { get; set; }
        public Nullable<int> MQualificationID { get; set; }

        [Display(Name = "Aadhaar No.")]
        public string MAdharNo { get; set; }

        [Display(Name = "Academic Year")]
        public Nullable<int> IsParentAvail { get; set; }

        [Display(Name = "Income")]
        public string FIncome { get; set; }

        [Display(Name = "Income")]
        public string MIncome { get; set; }

        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        [Display(Name = "Relation")]
        public string Relation { get; set; }

        [Display(Name = "Qualification")]
        public string GQualification { get; set; }
        public Nullable<int> GQualificationID { get; set; }

        [Display(Name = "Job")]
        public string GJob { get; set; }

        [Display(Name = "Income")]
        public string GIncome { get; set; }

        [Display(Name = "Previous School")]
        public string PrevSchool { get; set; }

        [Display(Name = "Previous School Address")]
        public string PSAddress { get; set; }

        [Display(Name = "Class Passed")]
        public string ClassPassed { get; set; }

        [Display(Name = "Academic Year")]
        public string RoleNm { get; set; }

        public Nullable<int> RouteID { get; set; }

        public Nullable<int> DestinationID { get; set; }

        [Display(Name = "Destination")]
        public string DestinationNm { get; set; }

        [Display(Name = "User ID")]
        public string SUserID { get; set; }

        [Display(Name = "Password")]
        public string SPwd { get; set; }

        [Display(Name = "User ID")]
        public string PUserID { get; set; }

        [Display(Name = "Password")]
        public string PPwd { get; set; }
        public Nullable<int> Status { get; set; }

        public string StatusName { get; set; }

        public Nullable<int> PSID { get; set; }
        public string PSYear { get; set; }
        public string School { get; set; }
        public string Board { get; set; }
        public Nullable<int> PSClass { get; set; }
        public string Marks { get; set; }
        public string Awards { get; set; }
        public Nullable<int> PSStatus { get; set; }

        public Nullable<int> PSID2 { get; set; }
        public string PSYear2 { get; set; }
        public string School2 { get; set; }
        public string Board2 { get; set; }
        public Nullable<int> PSClass2 { get; set; }
        public string Marks2 { get; set; }
        public string Awards2 { get; set; }
        public Nullable<int> PSStatus2 { get; set; }

        public Nullable<int> PSID3 { get; set; }
        public string PSYear3 { get; set; }
        public string School3 { get; set; }
        public string Board3 { get; set; }
        public Nullable<int> PSClass3 { get; set; }
        public string Marks3 { get; set; }
        public string Awards3 { get; set; }
        public Nullable<int> PSStatus3 { get; set; }
        public Nullable<int> StudentID { get; set; }

        public Nullable<int> HID { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> VisionLeft { get; set; }
        public Nullable<double> VisionRight { get; set; }
        public string MedicationDetails { get; set; }
        public Nullable<int> HStatus { get; set; }

        public Nullable<int> docLength { get; set; }
        public TBLStudentDoc doc { get; set; }

        public string GNationality { get; set; }
        public string Gmail { get; set; }
        public string Gmobile { get; set; }
        public string GOfficeAddress { get; set; }
        public string GAdharNo { get; set; }
        public string Languages { get; set; }
        public Nullable<System.DateTime> MDOB { get; set; }
        public string MDesig { get; set; }
        public string MNationality { get; set; }
        public string MOfficeAddress { get; set; }

        public Nullable<decimal> PermPincode { get; set; }
        public string PermState { get; set; }
        public string PermStateNm { get; set; }
        public string PermCity { get; set; }
        public Nullable<decimal> CorsPincode { get; set; }
        public string CorsState { get; set; }
        public string CorsStateNm { get; set; }
        public string CorsCity { get; set; }
        //fatch Find Student for id (Edit)
        public string PermcountrynameNm1 { get; set; }
        public string croscountrynameNm1 { get; set; }
        public string PermState1 { get; set; }
        public string CorsState1 { get; set; }
        public string PermCity1 { get; set; }
        public string CorsCity1 { get; set; }

        public string SMSmobileNo { get; set; }
        public Nullable<System.DateTime> FDOB { get; set; }
        public string FDesig { get; set; }
        public string FNationality { get; set; }
        public string FOfficeAddress { get; set; }
        public string GDesig { get; set; }
        public string EmergencyNo { get; set; }
        public string EmerContPerson { get; set; }
        public string ContPersRelation { get; set; }
        public string FPicPath { get; set; }
        public string MPicPath { get; set; }

        public List<TBLPrevSchoolDet> prevlist { get; set; }

        public TBLStudentHealthDet ht { get; set; }
        public tblSchoolDetail sd { get; set; }
        public string SRNo { get; set; }
        public string TCNo { get; set; }

        public string Lang_known { get; set; }

        public string stream { get; set; }
        public Nullable<int> streamID { get; set; }

        public int batchid { get; set; }
        public string batch { get; set; }
        public string SStatus { get; set; }
        public string Extra10 { get; set; }
        public string principalSign { get; set; }
        public string SDOB { get; set; }
        public string jDate { get; set; }
        public List<StudentAttendenceDetails> attendenceList { get; set; }

        public tblStRegistration stReg { get; set; }
        public List<tblStRegPrevSchoolDet> prevlist_reg { get; set; }
        public tblStRegDoc rgDoc { get; set; }
        public tblDocument docName { get; set; }
        public List<Student> rgDoclist { get; set; }
        public tblStRegHealthDet stregHt { get; set; }
        public string SchoolID { get; set; }
        public string Paidamount { get; set; }
        public string Dueamount { get; set; }
        public string loginuser { get; set; }
        public string typeuser { get; set; }
        public string PermCityNm { get; set; }
        public string corrCityNm { get; set; }
        public string PermcountrynameNm { get; set; }
        public string croscountrynameNm { get; set; }
        public string Message { get; set; }
        public string status { get; set; }
        public Student data { get; set; }
        // public string status { get; set; }
      //  public int Total { get; set; }
        public string ResonForLeave { get; set; }
        public int StuID { get; set; }
        public Nullable<System.DateTime> LeaveTo { get; set; }
        public Nullable<System.DateTime> LeaveFrom { get; set; }
        public string BiometricID { get; set; }
        public int StudentCount { get; set; }
    }


    public class Student
    {
        public List<ServiceInvoiceDetails> subModList { get; set; }

        public string DF { get; set; }
        public string DT { get; set; }

        public string Feeamount { get; set; }

        public string Consession { get; set; }

        public int ID { get; set; }
        public int voucherid { get; set; }
        public Nullable<int> FeeHeadID { get; set; }

        [Display(Name = "FeeCategory")]
        public string FeeCategory { get; set; }
        public string ManualNo { get; set; }
        public string Paymentby { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public string DocNo { get; set; }
        public System.DateTime dated { get; set; }

        [Display(Name = "Academic Year")]
        public string AcademicYear { get; set; }

        [Display(Name = "Registration No.")]
        public string RegNo { get; set; }

        [Display(Name = "Joining Date")]
        public Nullable<System.DateTime> JoiningDate { get; set; }

        public Nullable<int> ClassID { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }

        [Display(Name = "Academic Year")]
        public int SectionID { get; set; }

        [Display(Name = "Section")]
        public string Section { get; set; }

        [Display(Name = "Roll No.")]
        public string RollNo { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "DOB")]
        public Nullable<System.DateTime> DOB { get; set; }

        public Nullable<int> GenderID { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Birth Place")]
        public string BirthPlace { get; set; }

        [Display(Name = "Religion")]
        public string Religion { get; set; }
        public Nullable<int> ReligionID { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Mother Tongue")]
        public string MotherTongue { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
        public Nullable<int> CategoryID { get; set; }

        [Display(Name = "Caste")]
        public string Caste { get; set; }
        public int CasteID { get; set; }

        [Display(Name = "Aadhaar No")]
        public string AadharNo { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Corresponding Address")]
        public string CorrespondAddress { get; set; }

        [Display(Name = "Pincode")]
        public decimal pincode { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }
        public Nullable<int> CountryID { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }
        public Nullable<int> StateID { get; set; }

        [Display(Name = "Telephone")]
        public Nullable<decimal> phone { get; set; }

        [Display(Name = "Mobile No.")]
        public string mobileNo { get; set; }

        [Display(Name = "Email")]
        public string emailID { get; set; }

        [Display(Name = "Select Image")]
        public string PicPath { get; set; }

        [Display(Name = "Name")]
        public string FatherName { get; set; }

        [Display(Name = "Mobile No.")]
        public string FMobile { get; set; }

        [Display(Name = "Job")]
        public string FJob { get; set; }

        [Display(Name = "Email")]
        public string Fmail { get; set; }

        [Display(Name = "Academic Year")]
        public string FQualification { get; set; }
        public Nullable<int> FQualificationID { get; set; }

        [Display(Name = "Aadhaar No.")]
        public string FAdharNo { get; set; }

        [Display(Name = "Name")]
        public string MotherName { get; set; }

        [Display(Name = "Mobile No.")]
        public string Mmobile { get; set; }

        [Display(Name = "Job")]
        public string MJob { get; set; }

        [Display(Name = "Email")]
        public string Mmail { get; set; }

        [Display(Name = "Academic Year")]
        public string MQualification { get; set; }
        public Nullable<int> MQualificationID { get; set; }

        [Display(Name = "Aadhaar No.")]
        public string MAdharNo { get; set; }

        [Display(Name = "Academic Year")]
        public Nullable<int> IsParentAvail { get; set; }

        [Display(Name = "Income")]
        public string FIncome { get; set; }

        [Display(Name = "Income")]
        public string MIncome { get; set; }

        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        [Display(Name = "Relation")]
        public string Relation { get; set; }

        [Display(Name = "Qualification")]
        public string GQualification { get; set; }
        public Nullable<int> GQualificationID { get; set; }

        [Display(Name = "Job")]
        public string GJob { get; set; }

        [Display(Name = "Income")]
        public string GIncome { get; set; }

        [Display(Name = "Previous School")]
        public string PrevSchool { get; set; }

        [Display(Name = "Previous School Address")]
        public string PSAddress { get; set; }

        [Display(Name = "Class Passed")]
        public string ClassPassed { get; set; }

        [Display(Name = "Academic Year")]
        public string RoleNm { get; set; }

        public Nullable<int> RouteID { get; set; }

        public string Route { get; set; }

        public Nullable<int> DestinationID { get; set; }

        [Display(Name = "Destination")]
        public string DestinationNm { get; set; }

        [Display(Name = "User ID")]
        public string SUserID { get; set; }

        [Display(Name = "Password")]
        public string SPwd { get; set; }

        [Display(Name = "User ID")]
        public string PUserID { get; set; }

        [Display(Name = "Password")]
        public string PPwd { get; set; }
        public Nullable<int> Status { get; set; }

        public string StatusName { get; set; }

        public Nullable<int> PSID { get; set; }
        public string PSYear { get; set; }
        public string School { get; set; }
        public string Board { get; set; }
        public Nullable<int> PSClass { get; set; }
        public string Marks { get; set; }
        public string Awards { get; set; }
        public Nullable<int> PSStatus { get; set; }

        public Nullable<int> PSID2 { get; set; }
        public string PSYear2 { get; set; }
        public string School2 { get; set; }
        public string Board2 { get; set; }
        public Nullable<int> PSClass2 { get; set; }
        public string Marks2 { get; set; }
        public string Awards2 { get; set; }
        public Nullable<int> PSStatus2 { get; set; }

        public Nullable<int> PSID3 { get; set; }
        public string PSYear3 { get; set; }
        public string School3 { get; set; }
        public string Board3 { get; set; }
        public Nullable<int> PSClass3 { get; set; }
        public string Marks3 { get; set; }
        public string Awards3 { get; set; }
        public Nullable<int> PSStatus3 { get; set; }
        public Nullable<int> StudentID { get; set; }

        public Nullable<int> HID { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> VisionLeft { get; set; }
        public Nullable<double> VisionRight { get; set; }
        public string MedicationDetails { get; set; }
        public Nullable<int> HStatus { get; set; }

        public Nullable<int> docLength { get; set; }
        public TBLStudentDoc doc { get; set; }

        public string GNationality { get; set; }
        public string Gmail { get; set; }
        public string Gmobile { get; set; }
        public string GOfficeAddress { get; set; }
        public string GAdharNo { get; set; }
        public string Languages { get; set; }
        public Nullable<System.DateTime> MDOB { get; set; }
        public string MDesig { get; set; }
        public string MNationality { get; set; }
        public string MOfficeAddress { get; set; }

        public Nullable<decimal> PermPincode { get; set; }
        public string PermState { get; set; }
        public string PermStateNm { get; set; }
        public string PermCity { get; set; }

        public string PPincode { get; set; }

        public string CPincode { get; set; }
        public Nullable<decimal> CorsPincode { get; set; }
        public string CorsState { get; set; }
        public string CorsStateNm { get; set; }
        public string CorsCity { get; set; }
        //fatch Find Student for id (Edit)
        public string PermcountrynameNm1 { get; set; }
        public string croscountrynameNm1 { get; set; }
        public string PermState1 { get; set; }
        public string CorsState1 { get; set; }
        public string PermCity1 { get; set; }
        public string CorsCity1 { get; set; }

        public string SMSmobileNo { get; set; }
        public Nullable<System.DateTime> FDOB { get; set; }
        public string FDesig { get; set; }
        public string FNationality { get; set; }
        public string FOfficeAddress { get; set; }
        public string GDesig { get; set; }
        public string EmergencyNo { get; set; }
        public string EmerContPerson { get; set; }
        public string ContPersRelation { get; set; }
        public string FPicPath { get; set; }
        public string MPicPath { get; set; }

        public List<TBLPrevSchoolDet> prevlist { get; set; }

        public TBLStudentHealthDet ht { get; set; }
        public tblSchoolDetail sd { get; set; }
        public string SRNo { get; set; }
        public string TCNo { get; set; }

        public string Lang_known { get; set; }

        public string stream { get; set; }
        public Nullable<int> streamID { get; set; }

        public int batchid { get; set; }
        public string batch { get; set; }
        public string SStatus { get; set; }
        public string Extra10 { get; set; }
        public string principalSign { get; set; }
        public string SDOB { get; set; }
        public string jDate { get; set; }
        public List<StudentAttendenceDetails> attendenceList { get; set; }

        public tblStRegistration stReg { get; set; }
        public List<tblStRegPrevSchoolDet> prevlist_reg { get; set; }
        public tblStRegDoc rgDoc { get; set; }
        public tblDocument docName { get; set; }
        public List<Student> rgDoclist { get; set; }
        public tblStRegHealthDet stregHt { get; set; }
        public string SchoolID { get; set; }
        public string Paidamount { get; set; }
        public string Dueamount { get; set; }
        public string loginuser { get; set; }
        public string typeuser { get; set; }
        public string PermCityNm { get; set; }
        public string corrCityNm { get; set; }
        public string PermcountrynameNm { get; set; }
        public string croscountrynameNm { get; set; }
        public string Message { get; set; }
        public string status { get; set; }
        public Student data { get; set; }
        // public string status { get; set; }
        public int Total { get; set; }
        public string ResonForLeave { get; set; }
        public int StuID { get; set; }
        public Nullable<System.DateTime> LeaveTo { get; set; }
        public Nullable<System.DateTime> LeaveFrom { get; set; }
        public string BiometricID {get; set;}
        public int StudentCount { get; set; }

        public long Id { get; set; }
        public int customer { get; set; }
        public string customer1 { get; set; }
        public string cphone { get; set; }
        public string cemail { get; set; }
        public string cweb { get; set; }
        public string cAddress { get; set; }
        public string ccountry { get; set; }
        public string ccity { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public DateTime Today { get; set; }
        public string bill { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double TotalDiscount { get; set; }
        public double Tax { get; set; }
        public double TotalTax { get; set; }
        public double Shiping { get; set; }
        public double GrandTotal { get; set; }
        public double netTotal { get; set; }
        public string Detail { get; set; }
        public DateTime ExpDate { get; set; }
    }

    public class sss
    {
        public long Id { get; set; }
        public int supplier { get; set; }
        public string customer1 { get; set; }
        public string cphone { get; set; }
        public string cemail { get; set; }
        public string cweb { get; set; }
        public string cAddress { get; set; }
        public string ccountry { get; set; }
        public string ccity { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public DateTime Today { get; set; }
        public string bill { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double TotalDiscount { get; set; }
        public double Tax { get; set; }
        public double TotalTax { get; set; }
        public double Shiping { get; set; }
        public double GrandTotal { get; set; }
        public double netTotal { get; set; }
        public string Detail { get; set; }
        public DateTime ExpDate { get; set; }
    }



    public class years
    {
        public  int Idd { get; set; }
        public string year { get; set; }
    }

    public class StudentLeaveApplyAPP
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<StudentLeaveApply1> data { get; set; }
    }

    public class StudentLeaveApply
    {
        public int leaveID { get; set; }
        public tblStudentLeaveApply leave { get; set; }
        public string datefrom { get; set; }
        public string dateto { get; set; }
        public string RequestDt { get; set; }
        public string statusNm { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string RollNo { get; set; }

        public string style { get; set; }
        public string color { get; set; }
        public string SchoolID { get; set; }
        public string StudentUserID { get; set; }
    }
    public class StudentLeaveApply1
    {
        public int leaveID { get; set; }
        public tblStudentLeaveApply1 leave { get; set; }
        public string datefrom { get; set; }
        public string dateto { get; set; }
        public string RequestDt { get; set; }
        public string statusNm { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string RollNo { get; set; }

        public string style { get; set; }
        public string color { get; set; }
        public string SchoolID { get; set; }
    }
    public class tblStudentLeaveApply1
    {
        public long ID { get; set; }
        public long StudentID { get; set; }
        public string datefrom { get; set; }
        public string dateto { get; set; }
        public string Reason { get; set; }
        public string RequestDt { get; set; }
        public string ApproveDt { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<int> Status { get; set; }
        public string color { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
    }
    public class studentdetails
    {

        public bool status { get; set; }
        public string message { get; set; }

        public Student data { get; set; }

    }
    public class ClassTeacherAllocation
    {
        public string Id { get; set; }
        public string ClassID { get; set; }
        public string Class { get; set; }
        public string SectionId { get; set; }
        public string section { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string RollNo { get; set; }

        public string style { get; set; }
        public string color { get; set; }
    }


    public class SubjectTeacherAllocatesAppp
    {

        public bool status { get; set; }
        public string message { get; set; }
        public SubjectTeacherAllocates1 data { get; set; }
    }
    public class SubjectTeacherAllocates1
    {
        //public int Id { get; set; }
        public string SchoolID { get; set; }
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public List<AccountsDepartment> AccountsDepartment { get; set; }
        public List<PricipleDetails> PricipleDetail { get; set; }
        public List<ClassTeacherDetails> ClassTeacherDetail { get; set; }
        public List<SubjectTeacherDetails> SubTeacherDetails { get; set; }
    }
    public class SubjectTeacherDetails
    {
        public int TeacherID { get; set; }
        //public int SubjectID { get; set; }
        //public int ClassID { get; set; }
        public string TeacherName { get; set; }
        //public string ClassName { get; set; }
        //public string SubjectName { get; set; }
        public string[] SubjectName { get; set; }
        //public  Subjects  Subjects { get; set; }

        public string PhoneNo { get; set; }


    }
    public class Subjects
    {
        public string SubjectName { get; set; }


    }

    public class PricipleDetails
    {
        public int PrincipleID { get; set; }
        public string DesignationName { get; set; }
        public string PrincipleName { get; set; }
        public string PhoneNo { get; set; }
    }
    public class AccountsDepartment
    {

        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNo { get; set; }

    }
    public class ClassTeacherDetails
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string PhoneNo { get; set; }



    }
    public partial class GalleryImageAPP
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Gallery data { get; set; }


    }
    public partial class Gallery
    {
        public string SchoolID { get; set; }
        public List<GalleryImagess> GalleryImages { get; set; }

    }
    public partial class GalleryImagess
    {
        public string ImageTitle { get; set; }
        public string imagePath { get; set; }
        public string ImageDescription { get; set; }
        public Nullable<bool> status { get; set; }

    }

    public class EmployeeATT
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SchoolID { get; set; }      
        public List<MachineNO> machineNOO { get; set; }

    }
    public class StudentATT
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SchoolID { get; set; }
        public int EmpId { get; set; }

    }
    public class getstudents
    {

        public int classId { get; set; }
        public int  sectionID { get; set; }
        public string CacademicyearID { get; set; }
       

    }
    public class MachineNO
    {
      
        public string MachineNumber { get; set; }
    }
        public class GpsDeviceRecord
    {
        public int ID { get; set; }
        public string GpsDeviceNO { get; set; }
        public string MobileNo { get; set; }
        public string SchoolID { get; set; }
        public Nullable<bool> IsActice { get; set; }
        public string SchoolName { get; set; }
        public string Active { get; set; }
    }

    public class GpsDeviceRecord1
    {
        public int Id { get; set; }
        public string GpsDeviceNO { get; set; }
        public string MobileNo { get; set; }
        public string SchoolID { get; set; }
        public Nullable<bool> IsActice { get; set; }
        public string SchoolName { get; set; }
        public string Active { get; set; }
    }
    public class allocaton
    {
        public int ID { get; set; }
    }
    public partial class migpsGpsTrackingSystem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ValidationKey { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }
        public string Sno { get; set; }
    }

    public partial class vehicleLocationapp
    {
        public bool status { get; set; }
        public string message { get; set; }
        public vehicleLocation data { get; set; }
    }
    public partial class vehicleLocation
    {
        public string RegdNo { get; set; }
        public string Location { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string DeviceNumber { get; set; }
        public string RDateTime { get; set; }
        public string DateCreated { get; set; }
        public string TimeCreated { get; set; }
        public string AvailableStatus { get; set; }
        public string Speed { get; set; }
        public string IgnitionStatus { get; set; }
        
    }
    public partial class locationparameters
    {
        public string SchoolID { get; set; }
        public string StudentID { get; set; }

    }
    // included hub
    public partial class vehicleLocationapp1
    {
        public bool status { get; set; }
        public string message { get; set; }
        public vehicleLocation1 data { get; set; }
    }
    public partial class vehicleLocation1
    {

        public Getvehiclelocation CurrentLocation { get; set; }
        public List<Getvehiclehub> HubDetails { get; set; }
    }
    public partial class Getvehiclelocation
    {
        public string RegdNo { get; set; }
        public string Location { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string DeviceNumber { get; set; }
        public string RDateTime { get; set; }
        public string AvailableStatus { get; set; }
        public string Speed { get; set; }
        public string IgnitionStatus { get; set; }

    }
    public partial class Getvehiclehub
    {
         
        public string HubName { get; set; }
        public string HubAddress { get; set; }
        public string HubLat { get; set; }
        public string HubLong { get; set; }
      

    }
    public partial class CurrentLocation
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }

        public string RegdNo { get; set; }
        public string Location { get; set; }
        public string Lattitude { get; set; }

        public string Longitude { get; set; }
        public string DriverMobile { get; set; }
        public string DriverName { get; set; }

        public string DriverId { get; set; }
        public string Distance { get; set; }
        public string DayDistance { get; set; }

        public string OdoMeter { get; set; }
        public string TripMeter { get; set; }
        public string DeviceNumber { get; set; }
        public string RDateTime { get; set; }

        public string IgnitionTime { get; set; }
        public string IgnitionStatus { get; set; }
        public string PowerStatus { get; set; }

        public string AvailableStatus { get; set; }
        public string AcStatus { get; set; }
        public string Temperature { get; set; }

        public string Speed { get; set; }
        public string WorkTime { get; set; }
        public string AlertStatus { get; set; }
        public string UserId { get; set; }
        public string Trip { get; set; }
        public string GPSStatus { get; set; }
        public string Direction { get; set; }
        public string Bearing { get; set; }
        public string DeviceMobileNo { get; set; }
        public string ClientName { get; set; }
        public string VehicleType { get; set; }
        public string FuelLevel { get; set; }
        public string BatteryLevel { get; set; }
    }
    public partial class GetVehicleList
    {
        public int Id { get; set; }
        public string VehicleId { get; set; }
        public string RegdNo { get; set; }
        public string Location { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string DriverMobile { get; set; }
        public string DriverName { get; set; }
        public string DriverId { get; set; }
        public string Distance { get; set; }

        public string DayDistance { get; set; }
        public string OdoMeter { get; set; }
        public string TripMeter { get; set; }
        public string DeviceNumber { get; set; }
        public string RDateTime { get; set; }
        public string IgnitionTime { get; set; }
        public string IgnitionStatus { get; set; }
        public string PowerStatus { get; set; }
        public string AvailableStatus { get; set; }
        public string AcStatus { get; set; }

        public string Temperature { get; set; }
        public string Speed { get; set; }
        public string WorkTime { get; set; }
        public string AlertStatus { get; set; }
        public string UserId { get; set; }
        public string Trip { get; set; }
        public string GPSStatus { get; set; }
        public string Direction { get; set; }
        public string Bearing { get; set; }
        public string DeviceMobileNo { get; set; }
        public string ClientName { get; set; }
        public string VehicleType { get; set; }
        public string FuelLevel { get; set; }
        public string BatteryLevel { get; set; }
    }
    public partial class StatusParameters
    {
        public string Moving { get; set; }
        public string Idle { get; set; }
        public string NR { get; set; }
        public string NGF { get; set; }
        public string Delay { get; set; }
        public string BD { get; set; }
        public string Total { get; set; }
       
    }
        public partial class LocationParameters
    {
        public string RegdNo { get; set; }
        public string ValidationKey { get; set; }

    }
    public partial class GetVehicleListPara
    {
        
        public string Status { get; set; }
        public string UserId { get; set; }
        public string ValidationKey { get; set; }
    }
    public partial class ValidateUserPara
    {       
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ValidationKey { get; set; }
    }
    public partial class statusCountparams
    {
        public int ID { get; set; }    
        public string ValidationKey { get; set; }
    }
    public partial class getclassSection
    {
        public int SchoolID { get; set; }
        public int EmployeeID { get; set; }

    }
    public partial class getclassSectionDetails
    {
      public int ClassID { get; set; }
      public string ClassName { get; set; }
      public int SectionID { get; set; }
      public string SectionName { get; set; }
      public string message { get; set; }

    }
    public partial class getAcademicYear
    {
        public int ID { get; set; }
        public string AcademicYear { get; set; }

    }

    public partial class GETPromoteStudentParameter
    {
       
        public int SchoolID { get; set; }
        public int StudentID { get; set; }
        public string ClassID { get; set; }
        public string SectionID { get; set; }
        public string StreamID { get; set; }
        public string Academicyear { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string SectionNmae { get; set; }
        public string Result { get; set; }
        public string CurrentClassID { get; set; }
        public string CurrentSectionID { get; set; }
        public string CurrentAcademicYear { get; set; }

    }

  public partial class togetstream { public int ID { get; set; } public string Stream { get; set; } }


   
}