using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Models
{
    //public class CountyMaster
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Status { get; set; }
         
    //}

    public class deptModel
    {
        public int deptID { get; set; }

        [Display(Name = "Department")]
        public string dept { get; set; }
        [Display(Name ="Status")]
        public bool status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }

        public List<deptModel> DeptList { get; set; }
    }
   
    public class CategoryModel
    {
        public int CatID { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
        [Display(Name = "Status")]
        public bool status { get; set; }
    }

    public class AcademicYrModel
    {
        public int ID { get; set; }

        [Display(Name = "Start Year")]
        public string StartYear { get; set; }

        [Display(Name = "Start Month")]
        public string StartMonth { get; set; }

        [Display(Name = "End Year")]
        public string EndYear { get; set; }

        [Display(Name = "End Month")]
        public string EndMonth { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        public SelectList StYearList { get; set; }
        public SelectList StMnthList { get; set; }
        public SelectList EndYearList { get; set; }
        public SelectList EndMnthList { get; set; }

        
        public List<AcademicYrModel> AcademicYrlist { get; set; }
    }

    public class ClassTeacher
    {
        public int ID { get; set; }
        public int ClassID { get; set; }
        public int SectionID { get; set; }
        public string EmpID { get; set; }
        public Nullable<int> Status { get; set; }
        public int countID { get; set; }
        public tblClassTeacherAllocation cls { get; set; }
        public string Picpath { get; set; }
        public string Name { get; set; }
        public string classNm { get; set; }
        public string sectionNm { get; set; }
        public string statusNm { get; set; }
        public Nullable<int> intEmpID { get; set; }
        public string style { get; set; }
        public int SchoolID { get; set; }
        public string School { get; set; }
    }

    public class TimeTableapp
    {
        public bool status { get; set; }
        public string message { get; set; }
        public TimeTable1 data { get; set; }
       
    }
    public class TimeTable1
    {

        public string ClassID { get; set; }
        public string SectionID { get; set; }
        public string SchoolID { get; set; }
        public TimeTable3 MonthlyTimeTable { get; set; }
        public List<TimeTable2> ttList { get; set; }
        


    }
    public class TimeTable3
    {
       

        public List<TimeTable2> Monday { get; set; }
        public List<TimeTable2> TuesDay { get; set; }
        public List<TimeTable2> WebnessDay { get; set; }
        public List<TimeTable2> thrusday { get; set; }
        public List<TimeTable2> Friday { get; set; }
        public List<TimeTable2> Saterday { get; set; }
        public List<TimeTable2> Sunday { get; set; }
     
    }


    public class TimeTable2
    {
        public string IsBreak { get; set; }
        public string teacherNm { get; set; }
        public string subjectNm { get; set; }
        public string timingNm { get; set; }
        public string WeekDay { get; set; }
    }


    public class TimeTableNEWW
    {
        public long ID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<int> Section { get; set; }
        public Nullable<int> Subject { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string WeekDays { get; set; }
        public string name { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public Nullable<int> Status { get; set; }
    }

    public class TimeTableTeacher
    {
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }


        public string SubjectM { get; set; }
        public string ClassM { get; set; }
        public string SubjectT { get; set; }
        public string ClassT { get; set; }
        public string SubjectW { get; set; }
        public string ClassW { get; set; }
        public string SubjectTh { get; set; }
        public string ClassTh { get; set; }
        public string SubjectF { get; set; }
        public string ClassF { get; set; }
        public string SubjectS { get; set; }
        public string ClassS { get; set; }
        public string SubjectSu { get; set; }
        public string ClassSu { get; set; }
    }
        public class TimeTable
    {
        public long ID { get; set; }
        public int CountID { get; set; }
        public tblClassTiming ct { get; set; }
        public tblClassTimingDet period { get; set; }
        public List<tblClassTimingDet> CTD { get; set; }

        public tblTimeTableConfig con { get; set; }
        public tblTimeTable tt { get; set; }

        public List<tblTimeTable> ttList { get; set; }
        public string StatusNm { get; set; }
        public string Msg { get; set; }
        public string Msg1 { get; set; }
        public string style { get; set; }
        public string action { get; set; }
        public string IsBreak { get; set; }
        public string ViewType { get; set; }
        public string timingNm { get; set; }
        public string AcYear { get; set; }
        public string Course { get; set; }
        public string Section { get; set; }
        public string  fromDT { get; set; }
        public string  ToDt { get; set; }
        public string WeekDaysNm { get; set; }
        public List<TimeTable> TimeTableList { get; set; }
        public string subject { get; set; }
        public string TeacherName { get; set; }
        public List<TimeTableCLS> TimeTableCLS_list { get; set; }
        public int Status { get; set; }
        public string School { get; set; }
        public int SchoolID { get; set; }
        public tblSubjectTeacherAllocate sta { get; set; }
        public int Classid { get; set; }
        public string classname { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int TeacherId { get; set; }

    }

    public class TimeTableCLS
    {
        public long ID { get; set; }
        public Nullable<long> CTDet_ID { get; set; }
        public Nullable<long> CT_ID { get; set; }
        public string WeekDay { get; set; }

        public string WeekDaysNm { get; set; }
        public Nullable<long> SubjectID { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<long> TimeTableID { get; set; }
        public string teacherNm { get; set; }
        public string subjectNm { get; set; }
    }

    //public class bankMaster
    //{
    //    public tblBankMaster b { get; set; }
    //    public int count { get; set; }
    //    public string StatusNm { get; set; }
    //    public string style { get; set; }
    //}

    public class syllabusapp
    {
        public string status { get; set; }
        public string message { get; set; }
        public tblSyllabu data { get; set; }
        
    }
    public class syllabusappp
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<syllabus1>  data { get; set; }

    }
    public class syllabus1
    {
      
        public string Classname { get; set; }
        public string academicyearname { get; set; }
        public string style { get; set; }
        public syllabusAPISHOW Syllabus { get; set; }
        //public tblSyllabu1 Syllabus { get; set; }

    }

    public partial class syllabusAPISHOW
    {
       
            public int ID { get; set; }
            public Nullable<int> AcademicYear { get; set; }
            public Nullable<int> Class { get; set; }
            public string FilePath { get; set; }
            public string Description { get; set; }
            public Nullable<int> SchoolID { get; set; }          
            public string DateCreated { get; set; }
      
    }

    //public partial class tblSyllabu1
    //{
    //    public int ID { get; set; }
    //    public Nullable<int> AcademicYear { get; set; }
    //    public Nullable<int> Class { get; set; }
    //    public string FilePath { get; set; }
    //    public string Description { get; set; }
    //    public Nullable<int> SchoolID { get; set; }

    //}

    public class syllabus
    {
        public int ID { get; set; }
        public string empID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> Class { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string Classname { get; set; }
        public string academicyearname { get; set; }
        public string file { get; set; }
        public string path { get; set; }
        public string style { get; set; }
        public string course { get; set; }
        public string year { get; set; }
        public tblSyllabu hw { get; set; }
        public string msg { get; set; }
       
    }

    public class Elearning
    {
        public int ID { get; set; }
        public string empID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> Class { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string Classname { get; set; }
        public string academicyearname { get; set; }
        public string file { get; set; }
        public string path { get; set; }
        public string style { get; set; }
        public string course { get; set; }
        public string year { get; set; }
        public tblElearning hw { get; set; }
        public string msg { get; set; }

    }




    public class feesAPP
    {
     public bool status { get; set; }
     public string message { get; set; }
     public  feess data { get; set; } 
    }
    public class feesAPPP
    {
        public string status { get; set; }
        public string message { get; set; }
        public feesss data { get; set; }
    }
    public class feess
    {
        public int StudentID { get; set; }
        public int SchoolID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        //public int Amount { get; set; }
        //public int Discount { get; set; }
        //public int Fine { get; set; }
        //public int Paid { get; set; }
        //public int Balance { get; set; }
        public int TotalAmount { get; set; }
        public int TotalDiscount { get; set; }
        public int TotalPaid { get; set; }
        public int TotalDueBalence { get; set; }
        public List< newfee > FeeDetails { get; set; }
      


    }

    public partial class newfee
    {
        //public int fldId { get; set; }
        //public Nullable<int> AcademicYear { get; set; }
        public string Month { get; set; }
        //public Nullable<long> monthId { get; set; }
        public string Amount { get; set; }
        public string PaidAmount { get; set; }
        public string duesAmount { get; set; }
        //public Nullable<long> fldstudentID { get; set; }
        //public Nullable<System.DateTime> dateCreated { get; set; }
        //public string discountPer { get; set; }
        public string discountAmount { get; set; }
        public string Feestatus { get; set; }
        public int LateFeeFine { get; set; }
        public string DueDate { get; set; }
        public string FeePaidDate { get; set; }
        public string Colour { get; set; }
        //public string ActualPayableAmnt { get; set; }
        //public string DiscountReason { get; set; }
        //public Nullable<int> feePaid { get; set; }
        //public Nullable<int> feeStructureID { get; set; }
        //public Nullable<int> SchoolID { get; set; }
    }



    public class feesss
    {

        public tblFeeCalculate_temp cal { get; set; }


    }
    public class fees
    {
        public tblFeeCategory ct { get; set; }
        public tblFeeHead cf { get; set; }
        public tblDestination cd { get; set; }
        public tblFeeStructure fs { get; set; }
        public tblFeeStructureClass fc { get; set; }
        public tblFeeStructureConfig fsc { get; set; }
        public tblFeeCalculate_temp cal { get; set; }
        public tblFeeCalculate fcal { get; set; }
        //public Stdfee std1 { get; set; }
        public Student std { get; set; }
        public TBLStudent _std { get; set; }
        public int count { get; set; }
        public string StatusNm { get; set; }
        public string style { get; set; }
        public List<string> classes { get; set; }
        public string courses { get; set; }
        public string feeCategory { get; set; }
        public long Cat_ID { get; set; }
        public string Refund { get; set; }


        public string year { get; set; }
        public string School { get; set; }
        public string SchoolID { get; set; }
        public List<months> monthList { get; set; }
        public string FeeStatus { get; set; }
        public string FeeFine { get; set; }
        public string feedayy { get; set; }
        public string acy { get; set; }
        public string TariffName { get; set; }
        public int TariffID { get; set; }
        public int ID { get; set; }
        public  System.DateTime Dated { get; set; }

    }


    public class Voucher
    {
        public stdfee1 std { get; set; }
        public int VoucherID { get; set; }
        public string DocNO { get; set; }
        public DateTime Dated { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Amount { get; set; }
        public string Father { get; set; }
    }



    public class monthsAPP
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<months> data { get; set; }
    }
        public class months
    {
        public string month { get; set; }
        public string monthID { get; set; }
        public int month_id { get; set; }
    }

    public class RoleAssignment
    {
        public int ID { get; set; }
        public tblRoleMaster role { get; set; }
        public tblModule m { get; set; }
        public tblSubModule sm { get; set; }
        public List<tblModule> modList { get; set; }
        public List<tblSubModule> subModList { get; set; }
        public List<RoleAssignment> RList { get; set; }
        public List<tblRoleSubModAssign> RmodList { get; set; }
        public List<int> list { get; set; }
        public string name { get; set; }
        public int roleID { get; set; }
        public int SchoolID { get; set; }
    }

    public class SaleListReport
    {

        public tblSubModule sm { get; set; }
        public List<SaleDetails> subModList { get; set; }

        public List<SaleListReport> RList { get; set; }
        public List<SaleListReport> RList1 { get; set; }
        //public List<tblSubModule> subModList { get; set; }
        public string SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string Rate { get; set; }
        public string Discount { get; set; }
        public string Total { get; set; }
        public string BankName { get; set; }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string SalePrice { get; set; }
        public string Category { get; set; }
        public string Qtyin { get; set; }
        public string QtyOut { get; set; }
        public string stock { get; set; }
        public string stock1n { get; set; }
        public string stockout { get; set; }
        public string purchase { get; set; }
        public string sale { get; set; }

        public double OPB { get; set; }
        public double Balance { get; set; }


    }



    public class RoleAssignment1
    {
        public int ID { get; set; }
        public tblRoleMaster role { get; set; }
        public tblSuperAdminModule m { get; set; }
        public tblSuperAdminSubModule sm { get; set; }
        public tblModule m1 { get; set; }
        public tblSubModule sm1 { get; set; }
        public List<tblSubModule> smod { get; set; }
        public List<tblSuperAdminModule> modList { get; set; }
        public List<tblSuperAdminSubModule> subModList { get; set; }
        public List<RoleAssignment1> RList { get; set; }
        public List<tblRoleSubModAssign> RmodList { get; set; }
        public List<int> list { get; set; }
        public string name { get; set; }
        public int roleID { get; set; }
        public int SchoolID { get; set; }
        public int SubModule { get; set; }
    }
    public class AttendenceMachineMaster
    {
        public int Id { get; set; }
        public string MachineNo { get; set; }
        public string SchoolID { get; set; }
        public Nullable<bool> IsActice { get; set; }
        public string School { get; set; }
        public string Status { get; set; }
        public string machinetype { get; set; }
    }

}