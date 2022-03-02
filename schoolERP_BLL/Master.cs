using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace schoolERP_BLL
{
    public class Licence
    {
        public int Id { get; set; }
        public int SchoolID { get; set; }
        public string LicenceNo { get; set; }
        public string NoofStudent { get; set; }
        public string Charges { get; set; }
        public string ValidTo { get; set; }
        public string RenewalDate { get; set; }
        public string Active { get; set; }
        public string UserName { get; set; }
        public string Flag { get; set; }
        public string SchoolName { get; set; }

    }

    public class CHECKLICENCE
    {
        public int Id { get; set; }
        public int SchoolID { get; set; }
        public int NoofStudentLicenced { get; set; }
        public int NoofStudentSchool { get; set; }     
        public string SchoolName { get; set; }
        public string alertmsg { get; set; }


    }
    public class CountyMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statuss { get; set; }
        public List<StateMaster> MasteState {get;set;}

        public string School { get; set; }
        public int SchoolID { get; set; }

    }

    public class RegionMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class CgroupMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ServiceMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Employeemaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
    public class getSuppliers
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class getCustomer
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class getBanks
    {
        public string Code { get; set; }
        public string PName { get; set; }
        public string HLevel { get; set; }
        public string HType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Coa { get; set; }
        public string Coaid { get; set; }
    }
    public class getpurch
    {
        public string Id { get; set; }
        public string Name1 { get; set; }
        public string Name { get; set; }
    }

    public class GetUnits
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetBrands
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class GetProducts
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetCategories
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SgroupMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class StateMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statuss { get; set; }
        public string CountryName { get; set; }
        public List<CityMaster> cities { get; set; }

    }

    public class CityMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string Statuss { get; set; }

    }

    public class CourseMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Desc { get; set; }
        public string Code { get; set; }
       public string MiniumuAttendePer { get; set; }
        public string AttendenceType { get; set; }
        public string TotlWorkingDay { get; set; }
        public string Syllabus { get; set; }
        public string ID { get; set; }
        public string School { get; set; }
        public string Statusshow { get; set; }
        public List<SectionMaster> MasterSection { get; set; }
        public List<StreamMaster> MasterStream { get; set; }

        

    }

    public class BatchMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statuss { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string maxNoOfStudent { get; set; }
        public string Class { get; set; }

        public string School { get; set; }
        public int SchoolID { get; set; }
        public int classid { get; set; }
        public bool CurrActive { get; set; }
        public string CurrActiveShow { get; set; }
        public string StatusShow { get; set; }
    }

    public class SectionMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
       public string Class { get; set; }
        public string School { get; set; }
        public int SchoolID { get; set; }

    }

    public class CastMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statusss { get; set; }

    }

    public class StatusMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }

    public class RoleMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }
    public class DriverEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolID { get; set; }
       
    }

    public class DepartmentMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statuss { get; set; }
        
        public string School { get; set; }
        public int SchoolID { get; set; }
        public string ID { get; set; }
    }

    public class SchoolSMSMASTER
    {
        public int id { get; set; }
        public string authKey { get; set; }
        public string senderId { get; set; }
        public string SchoolID { get; set; }
        public bool active { get; set; }
        public string Statuss { get; set; }
        public string SchoolName { get; set; }
        //
    }
    public class QualficationMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public string  SchoolID { get; set; }

    }

    public class CategoryMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Statusss { get; set; }

    }

    public class StreamMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Class { get; set; }
        public string ClassId { get; set; }
        public int SchoolID { get; set; }
        public string School{ get; set; }
        public string StatusShow { get; set; }
    }

    public class DocumentMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string desc { get; set; }
        public string userType { get; set; }
        public string userId { get; set; }
        public bool Status { get; set; }
        public string Statusshow { get; set; }
        public int SchoolID { get; set; }
        public string School { get; set; }
    }


    public class CodeGenMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DocType { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string DocNo { get; set; }
        public string StartSeries { get; set; }
        public string Seprator { get; set; }
        public string LastSeries { get; set; }

        public bool Status { get; set; }
        public string Statusshow { get; set; }
        public string SchoolID { get; set; }
        public string School { get; set; }

    }

   

      

    public class SendEmail {

        public string Id { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Messsage { get; set; }

        public DateTime date { get; set; }
        public string TaskDate { get; set; }

        public Int64 EmpId { get; set; }
        public int SchoolID { get; set; }
        public string Thought { get; set; }
        public string Todaydate { get; set; }
    }
    public class  Holiday {
        public int count { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string Description { get; set; }
        public string School { get; set; }
        public string msg { get; set; }
        public int days { get; set; }
    }
  

    public class ExpCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Extra10 { get; set; }
        public int SchoolID { get; set; }
        public string School { get; set; }
    }


    public class PurchaseList
    {
        public string Id { get; set; }
        public string Supplier { get; set; }
        public string Address { get; set; }
        public DateTime Today { get; set; }
        public string Invoice { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string Date { get; set; }
        public string dds { get; set; }
        public string Payment { get; set; }
        public string Discount { get; set; }
        public string TotalDiscount { get; set; }
        public string Shiping { get; set; }
        public string GrandTotal { get; set; }
        public string netTotal { get; set; }
        public string Paid { get; set; }
        public string Due { get; set; }
        public string Change { get; set; }
        public string Detail { get; set; }
    }

    public class QuotationList
    {
        public string Id { get; set; }
        public string customer { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime Today { get; set; }
        public string bill { get; set; }
        public string Date { get; set; }
        public string Product { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public string TotalDiscount { get; set; }
        public string Tax { get; set; }
        public string TotalTax { get; set; }
        public string Shiping { get; set; }
        public string GrandTotal { get; set; }
        public string netTotal { get; set; }
        public string Detail { get; set; }
        public string ExpDate { get; set; }
    }
    public class SaleList
    {
       public List<SaleList> subModList { get; set; }

        public string Id { get; set; }
        public string UnitId { get; set; }
        public string Productid { get; set; }
        public string customer { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime Today { get; set; }
        public string bill { get; set; }
        public string Date { get; set; }
        public string Product { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public string TotalDiscount { get; set; }
        public string Tax { get; set; }
        public string TotalTax { get; set; }
        public string Shiping { get; set; }
        public string GrandTotal { get; set; }
        public string netTotal { get; set; }
        public string Detail { get; set; }
        public string ExpDate { get; set; }
    }

    public class ServiceList1
    {
        public string Id { get; set; }
        public string customer { get; set; }
        public string First { get; set; }
        public string second { get; set; }
        public string third { get; set; }
        public string Employee { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime Today { get; set; }
        public string bill { get; set; }
        public string Date { get; set; }
        public string Product { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public string TotalDiscount { get; set; }
        public string Tax { get; set; }
        public string TotalTax { get; set; }
        public string Shiping { get; set; }
        public string GrandTotal { get; set; }
        public string netTotal { get; set; }
        public string Detail { get; set; }
        public string ExpDate { get; set; }
    }
    public class PayeeDetails
    {

        public string Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string PrimaryDate { get; set; }
        public string LastDate { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public string PayingAmount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string Checqno { get; set; }
        public string ChequeDate { get; set; }
        public string BankName { get; set; }

        public string Remarks { get; set; }
        public string DuesAmount { get; set; }
        public string Status { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string School { get; set; }
        public int SchoolID { get; set; }
        public string BankRemarks { get; set; }
        
    }


    public class StructureDetails
    {
        public string ID { get; set; }

        public string FCID { get; set; }
        public string CID { get; set; }
        public string SID { get; set; }
        public string AID { get; set; }
        public string StID { get; set; }


        public string AcYear1 { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo{ get; set; }
        public string Coursename { get; set; }
        public string SectionName { get; set; }
        public string categoryName { get; set; }
        public string FeeCategory { get; set; }
        public string Amount { get; set; }
        public string Academic_Year { get; set; }
        public string BankRemarks { get; set; }

    }
    public class StudentConcessionList
    {
        
        public string Coursename { get; set; }
        public string SectionName { get; set; }
        public string FirstName { get; set; }
        public string FeeCategory { get; set; }
        public string Amount { get; set; }
        public string Academic_Year { get; set; }
       
    }

    public class CustomerReceivable
    {
        public string Name { get; set; }

        public string Receivable { get; set; }
        public string Received { get; set; }
        public string Balance { get; set; }


    }

    public class StockRpt
    {

        //public tblSubModule sm { get; set; }
        //public List<tblModule> modList { get; set; }
       // public List<tblSubModule> subModList { get; set; }

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

    }

    public class ConcessionHead
    {
        public String ID { get; set; }
        public string FeeCategory { get; set; }
        public string TariffName { get; set; }
        public string School { get; set; }

        public string SerialNo { get; set; }

    }

    public class udf_getsfee
    {

        public string Id { get; set; }

        public string FeeCategory { get; set; }
        public string FeeType { get; set; }
        public string Total { get; set; }
        public string Per { get; set; }
        public string concession { get; set; }
        public string nettotal { get; set; }
        public string paid { get; set; }
        public string Balance { get; set; }

    }



    public class RegionsList
    {

        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class TeritoriesList
    {

        public string ID { get; set; }
        public string ID1 { get; set; }
        public string TerritoryDescription { get; set; }
        public string region { get; set; }
    }

    public class Supplier1List
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string RegionID { get; set; }
        public string CountryID { get; set; }
        public string ContactName { get; set; }
        public string GroupID { get; set; }
        public string ContactTitle { get; set; }
        public string Country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string Address { get; set; }
        public string region { get; set; }
        public string group { get; set; }
        public string postal { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string EmailAddress { get; set; }
        public string balance { get; set; }
    }
    public class CustomerList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string RegionID { get; set; }
        public string CountryID { get; set; }
        public string ContactName { get; set; }
        public string GroupID { get; set; }
        public string ContactTitle { get; set; }
        public string Country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string Address { get; set; }
        public string region { get; set; }
        public string group { get; set; }
        public string postal { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string EmailAddress { get; set; }
        public string balance { get; set; }
    }
    public class PurchaseBillList
    {

        public string ID { get; set; }
        public string Supplier { get; set; }
        public string Bill { get; set; }
        public string Date { get; set; }
        public string Quantity { get; set; }
        public string Employee { get; set; }
        public string EmployeeId { get; set; }
        public string first { get; set; }
        public string second { get; set; }
        public string third { get; set; }
        public string Bil { get; set; }
        public string Remarks { get; set; }
        public string JournalRemarks { get; set; }
        public string Reference { get; set; }
        public string CID { get; set; }

    }


    public class GoodIssueDetailList
    {

        public string ID { get; set; }
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
        public string WarehouseId { get; set; }
       

    }

    public class ServiceList
    {

        public string ID { get; set; }
        public string ServiceName { get; set; }
        public string Charge { get; set; }
        public string Desc { get; set; }
        public string Tax { get; set; }
        public string Tax1 { get; set; }
        public string BankID { get; set; }
    }

    public class ShipperList
    {

        public string ID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
    public class UnitList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class DamageList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string PurchasePrice { get; set; }
        public string Quantity { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }
        public string Product { get; set; }
        public string Productid { get; set; }
    }
    public class ProductdetailsList
    {

        public string ID { get; set; }
        public string Product { get; set; }
        public string Desc { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public string quantity { get; set; }
        public string total { get; set; }
    }
    public class BrandList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }


    public class ProductList
    {

        public string ID { get; set; }
        public string Barcode { get; set; }
        public string Sn { get; set; }
        public string productname { get; set; }
        public string model { get; set; }
        public string unitprice { get; set; }
        public string unitinstoke { get; set; }
        public string unitonorder { get; set; }
        public string reorderlevel { get; set; }
        public string category { get; set; }
        public string unit { get; set; }
        public string brand { get; set; }
        public string productDetail { get; set; }
        public string Image { get; set; }
        public string PP { get; set; }
        public string QI { get; set; }
        public string QO { get; set; }
        public string Stock { get; set; }

    }

    public class WarehousesList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Countryid { get; set; }
        public string Stateid { get; set; }
        public string Cityid { get; set; }


    }
    public class custmerList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
    public class SupplierList
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
    public class BankList
    {

        public string ID { get; set; }
        public string BankName { get; set; }
        public string ACName { get; set; }
        public string ACnumber { get; set; }
        public string Branch { get; set; }
        public string Image { get; set; }
    }
    public class ExpenseTypesList
    {

        public string ID { get; set; }
        public string Type { get; set; }
    }


    public class SubjectMaster
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Color { get; set; }
        public bool Status { get; set; }
        public string stStatus { get; set; }
        public string School { get; set; }
        public int SchoolID { get; set; }
        public int classid { get; set; }
        public string classname { get; set; }
    }



    public class VedioAdvertising
    {
        public string Url { get; set; }
        public string Time { get; set; }
       public string Title { get; set; }

    }


}
