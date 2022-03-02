using System;
using System.Collections.Generic;
using System.Linq;
using SchoolErp.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolERP_BLL;
using System.Data;
using System.Globalization;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.ComponentModel;

using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Text;

namespace SchoolErp.Controllers.WebApi
{

    public class AccountsAPIController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();

        [System.Web.Http.Route("api/AccountsAPI/SaveHolidays")]
        [System.Web.Http.HttpPost]
        public string SaveHolidays(List<string> val)
        {
            //List<DateTime> li = new List<DateTime>();

            //DateTime stdt = Convert.ToDateTime(val[2]);
            //DateTime endt = Convert.ToDateTime(val[3]);
            //while (stdt <= endt)
            //{
            //    li.Add(stdt);
            //    stdt = stdt.AddDays(1);
            //}

            //validation should be there for date already assigned
            Holiday a = new Holiday();


            int ID = Convert.ToInt32(val[6]);

            if (ID == 0)
            {
                int SchoolIDch = Convert.ToInt32(val[5]);
                var checkdates = db.tblHolidays.Where(x => x.IsDeleted == null && x.SchoolID == SchoolIDch).ToList();
                int avi = 0;
                //int count = 0;
                foreach (var qq in checkdates)
                {


                    DateTime dbfromdate = Convert.ToDateTime(qq.DateFrom);
                    DateTime dbToDate = Convert.ToDateTime(qq.DateTo);
                    DateTime fromdate = Convert.ToDateTime(val[2]);
                    DateTime ToDate = Convert.ToDateTime(val[3]);
                    if ((fromdate >= dbfromdate && fromdate <= dbToDate) || (ToDate >= dbfromdate && ToDate <= dbToDate))
                    {


                        avi++;
                        //a.msg = avi+"Date Between Selected date ,already assigned as Holiday ";

                    }
                    else if (fromdate < dbfromdate && ToDate > dbToDate)
                    {
                        avi++;
                        //a.msg = avi + "Date Between Selected date ,already assigned as Holiday ";
                    }



                }
                if (avi != 0)
                {
                    a.msg = avi + "Holidays entry exist in Between Selected date ";
                }
                else
                {
                    tblHoliday hl = new tblHoliday();
                    hl.Name = val[0];
                    hl.Description = val[1];
                    hl.DateFrom = Convert.ToDateTime(val[2]);
                    hl.DateTo = Convert.ToDateTime(val[3]);
                    hl.AcademicYear = Convert.ToInt32(val[4]);
                    hl.SchoolID = Convert.ToInt32(val[5]);
                    hl.leaveCount = Convert.ToInt32(val[7]);
                    db.tblHolidays.Add(hl);
                    db.SaveChanges();
                    a.msg = "Holiday Assigned Successfully";
                }

            }
            else
            {
                var check = db.tblHolidays.Where(x => x.ID == ID).FirstOrDefault();
                if (check != null)
                {
                    int SchoolIDch = Convert.ToInt32(val[5]);
                    var checkdates = db.tblHolidays.Where(x => x.IsDeleted == null && x.SchoolID == SchoolIDch && x.ID != ID).ToList();
                    int avi = 0;
                    //int count = 0;
                    foreach (var qq in checkdates)
                    {


                        DateTime dbfromdate = Convert.ToDateTime(qq.DateFrom);
                        DateTime dbToDate = Convert.ToDateTime(qq.DateTo);
                        DateTime fromdate = Convert.ToDateTime(val[2]);
                        DateTime ToDate = Convert.ToDateTime(val[3]);
                        if ((fromdate >= dbfromdate && fromdate <= dbToDate) || (ToDate >= dbfromdate && ToDate <= dbToDate))
                        {


                            avi++;
                            //a.msg = avi+"Date Between Selected date ,already assigned as Holiday ";

                        }
                        else if (fromdate < dbfromdate && ToDate > dbToDate)
                        {
                            avi++;
                            //a.msg = avi + "Date Between Selected date ,already assigned as Holiday ";
                        }



                    }
                    if (avi != 0)
                    {
                        a.msg = avi + "Holidays entry exist in Between Selected date ";
                    }
                    else
                    {
                        check.Name = val[0];
                        check.Description = val[1];
                        check.DateFrom = Convert.ToDateTime(val[2]);
                        check.DateTo = Convert.ToDateTime(val[3]);
                        check.AcademicYear = Convert.ToInt32(val[4]);
                        check.SchoolID = Convert.ToInt32(val[5]);
                        check.leaveCount = Convert.ToInt32(val[7]);
                        db.SaveChanges();
                        a.msg = "Holiday Updates Successfully";
                    }


                }

            }







            return a.msg;
        }



        [System.Web.Http.Route("api/AccountsAPI/FindPDF")]
        [System.Web.Http.HttpPost]
        public Student FindPDF(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            Quotations s = db.Quotations.Where(x => x.Id == a).FirstOrDefault();

            st.Id = s.Id;
            st.Date = s.Date;
            st.Discount = s.Discount;
            //st.bill = s.Id;
            return st;
        }



        [System.Web.Http.Route("api/AccountsAPI/getAllHolidaysDetails")]
        [System.Web.Http.HttpPost]
        public Holiday[] getAllHolidaysDetails(List<string> val)
        {
            List<Holiday> list = new List<Holiday>();
            int SchoolID = Convert.ToInt32(val[0]);
            int c = 0;
            var result = (from hol in db.tblHolidays
                          join
ac in db.tblAcademicYears on hol.AcademicYear equals ac.ID
                          join sc in db.tblSchoolDetails on hol.SchoolID equals sc.ID
                          where hol.SchoolID == SchoolID && hol.IsDeleted == null
                          select new
                          {
                              model = hol,
                              model1 = ac,
                              Schoolname = sc.School

                          }
                          ).ToList();

            foreach (var a in result)
            {
                c++;
                Holiday Holida = new Holiday();
                Holida.ID = a.model.ID;
                Holida.Name = a.model.Name;
                Holida.Description = a.model.Description;
                Holida.DateFrom = ((DateTime)a.model.DateFrom).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Holida.DateTo = ((DateTime)a.model.DateTo).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Holida.AcademicYear = a.model1.DateFrom.ToString("yyyy") + "-" + a.model1.DateTo.ToString("yyyy");
                Holida.School = a.Schoolname;
                Holida.count = c;
                Holida.days = Convert.ToInt32(a.model.leaveCount);
                list.Add(Holida);
            }

            return list.ToArray();

        }

        [System.Web.Http.Route("api/AccountsAPI/DeleteHolidays")]
        [System.Web.Http.HttpPost]
        public string DeleteHolidays(List<string> aa)
        {
            tblHoliday hl = new tblHoliday();
            int idd = Convert.ToInt32(aa[0]);
            var check = db.tblHolidays.Where(x => x.ID == idd).FirstOrDefault();

            if (check != null)
            {

                check.IsDeleted = 1;
                check.Deleted_on = DateTime.Now;

                db.SaveChanges();
            }
            return "Holiday Deleted Sucessfully";


            //tblHoliday hl = new tblHoliday();
            //int idd = Convert.ToInt32(aa[0]);
            //var check = db.tblHolidays.Where(x => x.ID == idd).FirstOrDefault();
            //if (check != null)
            //{
            //    db.tblHolidays.Remove(check);
            //    db.SaveChanges();
            //}
            //return "Holiday Deleted Sucessfully";

        }

        //[System.Web.Http.Route("api/AccountsAPI/TimeTableNEWWW")]
        //[System.Web.Http.HttpPost]


        //public string TimeTableNEWWW(List<string> val)
        //{

        //     TimeTableNEWW tt = new TimeTableNEWW();
        //    tbltimetableNew tt = new tbltimetableNew();
        //    tt.SchoolID = Convert.ToInt32(val[10]);
        //    tt.AcademicYear = Convert.ToInt32(val[0]);
        //    tt.Class = Convert.ToInt32(val[1]);
        //    tt.Section = Convert.ToInt32(val[2]);
        //    tt.Subject = Convert.ToInt32(val[3]);
        //    tt.TeacherID = Convert.ToInt32(val[4]);
        //    tt.Status = Convert.ToInt32(val[9]);
        //    DateTime starttimee = timeTo24Hrs(val[6]);
        //    TimeSpan ttst = starttimee.TimeOfDay;
        //    tt.StartTime = ttst;
        //    DateTime Endtimee = timeTo24Hrs(val[7]);
        //    TimeSpan ttEt = Endtimee.TimeOfDay;
        //    tt.EndTime = ttEt;
        //    tt.name = val[5];
        //    tt.WeekDays = val[8];
        //    db.tbltimetableNews.Add(tt);
        //    db.SaveChanges();
        //    return "";


        //}
        public DateTime timeTo24Hrs(string TimeFormat)
        {
            DateTime dt = DateTime.Parse(TimeFormat);
            return dt;
        }
        [System.Web.Http.Route("api/AccountsAPI/saveCategory")]
        [System.Web.Http.HttpPost]
        public string saveCategory(ExpCategory exp)
        {
            if (string.IsNullOrEmpty(exp.Id))
            {

                var check = db.tblExpenseCategories.Where(s => s.CategoryName == exp.Name && s.SchoolID == exp.SchoolID && s.IsDeleted == null).FirstOrDefault();
                if (check == null)
                {
                    tblExpenseCategory tb = new tblExpenseCategory();
                    tb.Id = Convert.ToInt16(exp.Id);
                    tb.CategoryName = exp.Name;
                    tb.Status = Convert.ToBoolean(exp.Status);
                    tb.SchoolID = Convert.ToInt32(exp.SchoolID);
                    db.tblExpenseCategories.Add(tb);
                    db.SaveChanges();
                    return "Category Inserted Successfully";
                }
                else
                {
                    return "Category name already Exist ";
                }

            }
            else
            {
                int idd = Convert.ToInt16(exp.Id);
                var check = db.tblExpenseCategories.Where(s => s.CategoryName == exp.Name && s.Id != idd && s.SchoolID == exp.SchoolID && s.IsDeleted == null).FirstOrDefault();
                if (check == null)
                {
                    var result = db.tblExpenseCategories.SingleOrDefault(s => s.Id == idd);
                    result.CategoryName = exp.Name;
                    result.Status = Convert.ToBoolean(exp.Status);
                    result.SchoolID = Convert.ToInt32(exp.SchoolID);
                    db.SaveChanges();
                    return "Category Updated Successfully";
                }
                else
                {
                    return "Category name already Exist ";
                }
            }
        }

        [System.Web.Http.Route("api/AccountsAPI/SaveSale")]
        [System.Web.Http.HttpPost]
        public int SaveSale(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];



                if (a == "SaveSaleMain")
                {

                    var id = Convert.ToInt32(val[1]);
                    var customer = Convert.ToInt32(val[2]);
                    var Date = Convert.ToDateTime(val[3])/*.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)*/;
                    //var = PrimaryDate  ((DateTimeConverter)["PrimaryDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    ////usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var Discountm = Convert.ToDouble(val[4]);
                    var TotalDiscount = Convert.ToDouble(val[5]);
                    var Shiping = Convert.ToDouble(val[6]);
                    var GrandTotal = Convert.ToDouble(val[7]);
                    var TotalTax = Convert.ToDouble(val[8]);
                    var Tax = Convert.ToDouble(val[9]);
                    var NetTotal = Convert.ToDouble(val[10]);
                    var PaidAmount = Convert.ToDouble(val[11]);
                    var Due = Convert.ToDouble(val[12]);
                    var change = Convert.ToDouble(val[13]);
                    var Details = val[14].ToString().Trim();
                    var Payment = Convert.ToInt32(val[15]);
                    int SchoolID = Convert.ToInt32(val[16]);

                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    long Accid = 0;
                    if (Payment == 14)
                    {
                        Accid = 14;
                    }
                    else
                    {
                        var Acc = db.Accounts.Where(ac => ac.BankId == Payment).ToList();
                        Accid = Acc[0].Id;
                    }

                    if (id == 0)
                    {
                        var check = db.Sales.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Sales cta = new SchoolErp.Sales();
                            cta.CustomerId = customer;
                            cta.Date = Date;
                            cta.Discount = Discountm;
                            cta.TotalDiscount = TotalDiscount;
                            cta.Vat = Tax;
                            cta.TotalTax = TotalTax;
                            cta.ShippingCost = Shiping;
                            cta.GrandTotal = GrandTotal;
                            cta.NetTotal = NetTotal;
                            cta.PaidAmount = PaidAmount;
                            cta.Due = Due;
                            cta.Change = change;
                            cta.PaymentAccount = Accid;
                            cta.Details = Details;
                            cta.VNo = Vno;
                            cta.SchoolID = SchoolID;
                            db.Sales.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                        var result = db.Sales.SingleOrDefault(b => b.Id == id);
                        result.CustomerId = customer;
                        result.Date = Date;
                        result.Discount = Discountm;
                        result.TotalDiscount = TotalDiscount;
                        result.Vat = Tax;
                        result.TotalTax = TotalTax;
                        result.ShippingCost = Shiping;
                        result.GrandTotal = GrandTotal;
                        result.NetTotal = NetTotal;
                        result.PaidAmount = PaidAmount;
                        result.Due = Due;
                        result.Change = change;
                        result.PaymentAccount = Accid;
                        result.Details = Details;
                        db.SaveChanges();

                    }


                }


                if (a == "SaveSale")


                {

                    var id = Convert.ToInt32(val[1]);
                    var ProductID = Convert.ToInt32(val[2]);
                    var unit = val[3].ToString().Trim();
                    var quantity = Convert.ToDouble(val[4]);
                    var unitprice = Convert.ToDouble(val[5]);
                    var Discount = Convert.ToDouble(val[6]);
                    var Total = Convert.ToDouble(val[7]);
                    var desc = val[8].ToString().Trim();
                    var unitid = Convert.ToInt32(val[9]);
                    var Name = val[10].ToString().Trim();
                    var Tax = Convert.ToDouble(val[11]);
                    var idd = Convert.ToInt32(val[12]);


                    if (id == 0)
                    {
                        var sale = db.Sales.OrderByDescending(x => x.Id).ToList();
                        long saleid = sale[0].Id;
                        var check = db.SaleDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            SaleDetails cta = new SchoolErp.SaleDetails();
                            cta.ProductId = ProductID;
                            cta.UnitName = unit;
                            cta.Description = desc;
                            cta.Quantity = quantity;
                            cta.UnitPrice = unitprice;
                            cta.Discount = Discount;
                            cta.Total = Total;
                            cta.UnitId = unitid;
                            cta.ProductName = Name;
                            cta.SaleId = saleid;
                            cta.Tax = Tax;
                            db.SaleDetails.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.SaleDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                SaleDetails cta = new SchoolErp.SaleDetails();
                                cta.ProductId = ProductID;
                                cta.UnitName = unit;
                                cta.Description = desc;
                                cta.Quantity = quantity;
                                cta.UnitPrice = unitprice;
                                cta.Discount = Discount;
                                cta.Total = Total;
                                cta.UnitId = unitid;
                                cta.ProductName = Name;
                                cta.SaleId = id;
                                cta.Tax = Tax;
                                db.SaleDetails.Add(cta);
                                db.SaveChanges();
                            }
                            else
                            {

                            }

                        }
                        else

                        {

                            var result = db.SaleDetails.SingleOrDefault(b => b.Id == idd);
                            result.ProductId = ProductID;
                            result.UnitName = unit;
                            result.Description = desc;
                            result.Quantity = quantity;
                            result.UnitPrice = unitprice;
                            result.Discount = Discount;
                            result.Total = Total;
                            result.UnitId = unitid;
                            result.ProductName = Name;
                            result.Tax = Tax;
                            db.SaveChanges();
                        }

                    }


                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }

        [System.Web.Http.Route("api/AccountsAPI/SaveService")]
        [System.Web.Http.HttpPost]
        public int SaveService(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];



                if (a == "SaveServiceMain")
                {

                    var id = Convert.ToInt32(val[1]);
                    var customer = Convert.ToInt32(val[2]);
                    var Employee = Convert.ToInt32(val[3]);
                    var Date = Convert.ToDateTime(val[4])/*.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)*/;
                    //var = PrimaryDate  ((DateTimeConverter)["PrimaryDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    ////usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var Discountm = Convert.ToDouble(val[5]);
                    var TotalDiscount = Convert.ToDouble(val[6]);
                    var Shiping = Convert.ToDouble(val[7]);
                    var GrandTotal = Convert.ToDouble(val[8]);
                    var TotalTax = Convert.ToDouble(val[9]);
                    var Tax = Convert.ToDouble(val[10]);
                    var NetTotal = Convert.ToDouble(val[11]);
                    var PaidAmount = Convert.ToDouble(val[12]);
                    var Due = Convert.ToDouble(val[13]);
                    var change = Convert.ToDouble(val[14]);
                    var Details = val[15].ToString().Trim();
                    var Payment = Convert.ToInt32(val[16]);
                    int SchoolID = Convert.ToInt32(val[17]);

                    if (id == 0)
                    {
                        var check = db.ServiceInvoices.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            ServiceInvoices cta = new SchoolErp.ServiceInvoices();
                            cta.CustomerId = customer;
                            cta.EmployeeId = Employee;
                            cta.Date = Date;
                            cta.Discount = Discountm;
                            cta.TotalDiscount = TotalDiscount;
                            cta.Vat = Tax;
                            cta.TotalTax = TotalTax;
                            cta.ShippingCost = Shiping;
                            cta.GrandTotal = GrandTotal;
                            cta.NetTotal = NetTotal;
                            cta.PaidAmount = PaidAmount;
                            cta.Due = Due;
                            cta.Change = change;
                            cta.PaymentAccount = Payment;
                            cta.Details = Details;
                            cta.SchoolID = SchoolID;
                            db.ServiceInvoices.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                        var result = db.ServiceInvoices.SingleOrDefault(b => b.Id == id);
                        result.CustomerId = customer;
                        result.EmployeeId = Employee;
                        result.Date = Date;
                        result.Discount = Discountm;
                        result.TotalDiscount = TotalDiscount;
                        result.Vat = Tax;
                        result.TotalTax = TotalTax;
                        result.ShippingCost = Shiping;
                        result.GrandTotal = GrandTotal;
                        result.NetTotal = NetTotal;
                        result.PaidAmount = PaidAmount;
                        result.Due = Due;
                        result.Change = change;
                        result.PaymentAccount = Payment;
                        result.Details = Details;
                        db.SaveChanges();

                    }


                }


                if (a == "SaveService")


                {

                    var id = Convert.ToInt32(val[1]);
                    var Service = val[2].ToString().Trim();
                    var ServiceId = Convert.ToInt32(val[3]);
                    var Quantity = Convert.ToDouble(val[4]);
                    var Price = Convert.ToDouble(val[6]);
                    var Discount = Convert.ToDouble(val[5]);
                    var Total = Convert.ToDouble(val[7]);
                    var Tax = Convert.ToDouble(val[8]);
                    var desc = val[9].ToString().Trim();
                    var idd = Convert.ToInt32(val[10]);


                    if (id == 0)
                    {
                        var service1 = db.ServiceInvoices.OrderByDescending(x => x.Id).ToList();
                        long ServiceInvoiceId = service1[0].Id;
                        var check = db.ServiceInvoiceDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            ServiceInvoiceDetails cta = new SchoolErp.ServiceInvoiceDetails();
                            cta.ServiceId = ServiceId;
                            cta.Description = desc;
                            cta.Quantity = Quantity;
                            cta.Discount = Discount;
                            cta.Total = Total;
                            cta.ServiceName = Service;
                            cta.UnitPrice = Price;
                            cta.Tax = Tax;
                            cta.ServiceInvoiceId = ServiceInvoiceId;
                            db.ServiceInvoiceDetails.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.ServiceInvoiceDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                ServiceInvoiceDetails cta = new SchoolErp.ServiceInvoiceDetails();
                                cta.ServiceId = ServiceId;
                                cta.Description = desc;
                                cta.Quantity = Quantity;
                                cta.Discount = Discount;
                                cta.Total = Total;
                                cta.ServiceName = Service;
                                cta.UnitPrice = Price;
                                cta.Tax = Tax;
                                cta.ServiceInvoiceId = id;
                                db.ServiceInvoiceDetails.Add(cta);
                                db.SaveChanges();
                            }
                            else
                            {

                            }

                        }
                        else

                        {

                            var result = db.ServiceInvoiceDetails.SingleOrDefault(b => b.Id == idd);
                            result.ServiceId = ServiceId;
                            result.Description = desc;
                            result.Quantity = Quantity;
                            result.Discount = Discount;
                            result.Total = Total;
                            result.ServiceName = Service;
                            result.UnitPrice = Price;
                            result.Tax = Tax;
                            //result.ServiceInvoiceId = id;
                            db.SaveChanges();
                        }

                    }


                }


                if (a == "DamageProduct")


                {

                    var id = Convert.ToInt32(val[1]);
                    var Category = Convert.ToInt32(val[2]);
                    var Product = Convert.ToInt32(val[3]);
                    var UnitOrder = val[4].ToString().Trim();
                    var Name = val[5].ToString().Trim();
                    var PurchasePrice = Convert.ToDouble(val[6]);
                    var Quantity = Convert.ToDouble(val[7]);
                    var Date = Convert.ToDateTime(val[8]);
                    var Note = val[9].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[10]);


                    if (id == 0)
                    {
                        var check = db.DamagedProducts.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            DamagedProducts cta = new SchoolErp.DamagedProducts();
                            cta.Code = UnitOrder;
                            cta.Name = Name;
                            cta.Quantity = Quantity;
                            cta.CategoryId = Category;
                            cta.PurchasePrice = PurchasePrice;
                            cta.Date = Date;
                            cta.Note = Note;
                            cta.ProductId = Product;
                            cta.SchoolID = SchoolID;
                            db.DamagedProducts.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //var check = db.DamagedProducts.Where(s => s.Id == id).FirstOrDefault();
                        //if (check == null)
                        //{

                        var result = db.DamagedProducts.SingleOrDefault(b => b.Id == id);
                        result.Code = UnitOrder;
                        result.Name = Name;
                        result.Quantity = Quantity;
                        result.CategoryId = Category;
                        result.PurchasePrice = PurchasePrice;
                        result.Date = Date;
                        result.Note = Note;
                        result.ProductId = Product;
                        db.SaveChanges();
                        //}
                        //else
                        //{
                        //}
                    }


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }


        [System.Web.Http.Route("api/AccountsAPI/SaveCashBank")]
        [System.Web.Http.HttpPost]
        public int SaveCashBank(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];




                if (a == "SaveTransaction")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Withdraw = val[2].ToString().Trim();
                    var Amount = Convert.ToDouble(val[3]);
                    var Desc = val[4].ToString().Trim();
                    var Date = Convert.ToDateTime(val[5]);
                    var login = val[6].ToString().Trim();
                    var Coaid = Convert.ToInt32(val[7]);
                    var DC = Convert.ToInt32(val[8]);
                    int SchoolID = Convert.ToInt32(val[9]);

                    var Coa = 0;
                    if (Coaid == 14)
                    {
                        var Acc = db.Accounts.Where(ac => ac.Id == Coaid).ToList();
                        Coa = Convert.ToInt32(Acc[0].HeadCode);
                    }
                    else
                    {
                        var Acc = db.Accounts.Where(ac => ac.BankId == Coaid).ToList();
                        Coaid = Convert.ToInt32(Acc[0].Id);
                        Coa = Convert.ToInt32(Acc[0].HeadCode);
                    }




                    if (DC == 1)
                    {
                        var Amount1 = Convert.ToDouble(val[3]);
                    }
                    else
                    {
                        var Amount1 = Convert.ToDouble(val[3]);
                    }
                    var logindate = DateTime.Now;
                    var vtype = "Trans";
                    var isposted = true;
                    var isaprove = true;
                    var isopning = false;






                    if (id == 0)
                    {
                        var check = db.Transactions.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Transactions cta = new SchoolErp.Transactions();
                            cta.VNo = Withdraw;
                            if (DC == 1)
                            {
                                cta.Debit = Amount;
                            }
                            else
                            {
                                cta.Credit = Amount;
                            }

                            cta.Narration = Desc;
                            cta.VDate = Date;
                            cta.Vtype = vtype;
                            cta.IsPosted = isposted;
                            cta.IsAppove = isaprove;
                            cta.IsOpening = isopning;
                            cta.COA = Coa.ToString();
                            cta.COAId = Coaid;
                            cta.InsertDate = logindate;
                            cta.InsertUserId = login;
                            cta.SchoolID = SchoolID;
                            db.Transactions.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }

                }

                if (a == "SaveAdjustement")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Withdraw = Convert.ToInt32(val[2]);
                    var Amount = Convert.ToDouble(val[3]);
                    var Desc = val[4].ToString().Trim();
                    var Date = Convert.ToDateTime(val[5]);
                    var login = val[6].ToString().Trim();

                    var DC = Convert.ToInt32(val[7]);
                    int SchoolID = Convert.ToInt32(val[8]);


                    var Acc = db.Accounts.Where(ac => ac.HeadCode == Withdraw).ToList();

                    var Coaid = Convert.ToInt32(Acc[0].Id);





                    if (DC == 1)
                    {
                        var Amount1 = Convert.ToDouble(val[3]);
                    }
                    else
                    {
                        var Amount1 = Convert.ToDouble(val[3]);
                    }
                    var logindate = DateTime.Now;
                    var vtype = "AD";
                    var isposted = true;
                    var isaprove = true;
                    var isopning = false;






                    if (id == 0)
                    {
                        var check = db.Transactions.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Transactions cta = new SchoolErp.Transactions();
                            cta.VNo = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            if (DC == 1)
                            {
                                cta.Debit = Amount;
                            }
                            else
                            {
                                cta.Credit = Amount;
                            }

                            cta.Narration = Desc;
                            cta.VDate = Date;
                            cta.Vtype = vtype;
                            cta.IsPosted = isposted;
                            cta.IsAppove = isaprove;
                            cta.IsOpening = isopning;
                            cta.COA = Withdraw.ToString();
                            cta.COAId = Coaid;
                            cta.InsertDate = logindate;
                            cta.InsertUserId = login;
                            cta.SchoolID = SchoolID;
                            db.Transactions.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }

                }



                if (a == "SaveSupplierPayment")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Supplier = Convert.ToInt32(val[3]);
                    var Payment = Convert.ToInt32(val[4]);
                    var NetTotal = Convert.ToDouble(val[5]);
                    var Remark = val[6].ToString().Trim();
                    var login = val[7].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[8]);


                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    if (Payment != 14)
                    {
                        var Acc = db.Accounts.Where(ac => ac.BankId == Payment).ToList();
                        Payment = Convert.ToInt32(Acc[0].Id);

                    }
                    if (id == 0)
                    {
                        var check = db.SupplierPayment.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            SupplierPayment cta = new SchoolErp.SupplierPayment();
                            cta.SupplierId = Supplier;
                            cta.Date = Date;
                            cta.PaidAmount = NetTotal;
                            cta.NetTotal = NetTotal;
                            cta.Details = Remark;
                            cta.PaymentAccount = Payment;
                            cta.VNo = Vno;
                            cta.EmployeeId = login;
                            cta.SchoolID = SchoolID;
                            //cta.Voucher = date1;
                            db.SupplierPayment.Add(cta);

                        }
                    }
                    db.SaveChanges();

                }


                if (a == "SaveCustomerReceive")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Customer = Convert.ToInt32(val[3]);
                    var Payment = Convert.ToInt32(val[4]);
                    var NetTotal = Convert.ToDouble(val[5]);
                    var Remark = val[6].ToString().Trim();
                    var login = val[7].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[8]);

                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    if (Payment != 14)
                    {
                        var Acc = db.Accounts.Where(ac => ac.BankId == Payment).ToList();
                        Payment = Convert.ToInt32(Acc[0].Id);

                    }
                    if (id == 0)
                    {
                        var check = db.PaymentCustomer.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            PaymentCustomer cta = new SchoolErp.PaymentCustomer();
                            cta.CustomerId = Customer;
                            cta.Date = Date;
                            cta.PaidAmount = NetTotal;
                            cta.NetTotal = NetTotal;
                            cta.Details = Remark;
                            cta.PaymentAccount = Payment;
                            cta.VNo = Vno;
                            cta.EmployeeId = login;
                            cta.SchoolID = SchoolID;
                            //cta.Voucher = date1;
                            db.PaymentCustomer.Add(cta);

                        }
                    }
                    db.SaveChanges();

                }

                if (a == "SaveNewExpenses")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Expenses = Convert.ToInt32(val[2]);
                    var Date = Convert.ToDateTime(val[3]);
                    var Payment = Convert.ToInt32(val[4]);
                    var Amount = Convert.ToDouble(val[5]);
                    int SchoolID = Convert.ToInt32(val[6]);

                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

                    if (id == 0)
                    {
                        var check = db.Expenses.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Expenses cta = new SchoolErp.Expenses();
                            cta.ExpenseTypeId = Expenses;
                            cta.Date = Date;
                            cta.PaymentAccount = Payment;
                            cta.Amount = Amount;
                            cta.VNo = Vno;
                            cta.SchoolID = SchoolID;
                            db.Expenses.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Expenses.Where(s => s.ExpenseTypeId == Expenses && s.Date == Date && s.PaymentAccount == Payment && s.Amount == Amount).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Expenses.SingleOrDefault(b => b.Id == id);
                            result.ExpenseTypeId = Expenses;
                            result.Date = Date;
                            result.PaymentAccount = Payment;
                            result.Amount = Amount;
                            result.VNo = Vno;
                            //cta.Reference = ref;
                            db.SaveChanges();
                        }
                        else
                        {
                        }
                    }


                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }




        [System.Web.Http.Route("api/AccountsAPI/SaveFinancial")]
        [System.Web.Http.HttpPost]
        public int SaveFinancial(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];

                if (a == "DayClosing")
                {
                    var id = Convert.ToInt32(val[1]);


                    var logindate = DateTime.Now;
                    var Last = Convert.ToInt64(val[2]);
                    var CashIn = Convert.ToInt64(val[3]);
                    var CashOut = Convert.ToInt64(val[4]);
                    var Balance = Convert.ToInt64(val[5]);
                    var Loginuser = val[6].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[7]);




                    DailyClosing cta = new SchoolErp.DailyClosing();

                    cta.LastDayClosing = Last;
                    cta.CashIn = CashIn;

                    cta.CashOut = CashOut;
                    cta.Amount = Balance;
                    // cta.HeadLevel = HL;
                    cta.InsertDate = logindate;
                    //cta.InsertUserId=Loginuser;
                    // cta.IsTransaction = Trans;
                    //cta.IsGL = Chk;
                    // cta.IsActive = 1;
                    cta.SchoolId = SchoolID;
                    // cta.SchoolID = SchoolID;
                    db.DailyClosing.Add(cta);
                    db.SaveChanges();



                }



                if (a == "Accounts")
                {
                    var id = Convert.ToInt32(val[1]);

                    var Head = val[2].ToString().Trim();
                    var Head1 = val[3].ToString().Trim();

                    var logindate = DateTime.Now;
                    var Code = Convert.ToInt64(val[4]);
                    var HT = val[5].ToString().Trim();
                    var HL = Convert.ToInt32(val[6]);
                    var Trans = Convert.ToBoolean(val[7]);
                    var Chk = Convert.ToBoolean(val[8]);
                    var Loginuser = val[9].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[10]);




                    Accounts cta = new SchoolErp.Accounts();

                    cta.HeadCode = Code;
                    cta.HeadName = Head1;
                    if (id == 0)
                    { cta.ParentHead = null; }
                    else
                    { cta.ParentHead = id; }
                    cta.PHeadName = Head;
                    cta.HeadType = HT;
                    cta.HeadLevel = HL;
                    cta.InsertDate = logindate;
                    cta.InsertUserId = Loginuser;
                    cta.IsTransaction = Trans;
                    cta.IsGL = Chk;
                    cta.IsActive = 1;
                    cta.SchoolID = SchoolID;
                    db.Accounts.Add(cta);
                    db.SaveChanges();



                }


                if (a == "SaveOpeningBalance")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Head = Convert.ToInt32(val[3]);
                    var Acc = db.Accounts.Where(ac => ac.Id == Head).ToList();
                    var Head1 = Acc[0].HeadCode.ToString().Trim();

                    //var Head1 = val[4].ToString().Trim();
                    var DC = Convert.ToInt32(val[5]);
                    if (DC == 1)
                    {
                        var Amount1 = Convert.ToDouble(val[5]);
                    }
                    else
                    {
                        var Amount1 = Convert.ToDouble(val[5]);
                    }
                    var logindate = DateTime.Now;
                    var Amount = Convert.ToDouble(val[6]);
                    var Remark = val[7].ToString().Trim();
                    var Loginuser = val[8].ToString().Trim();
                    var Opening = true;
                    var Vtype = "Opening";
                    var Vno = (DateTime.Now.ToString("yyyyMMddHHmmss"));
                    var isposted = true;
                    var isaprove = true;
                    int SchoolID = Convert.ToInt32(val[9]);

                    if (id == 0)
                    {
                        var check = db.Transactions.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Transactions cta = new SchoolErp.Transactions();
                            cta.VNo = Vno;
                            if (DC == 1)
                            {
                                cta.Debit = Amount;
                            }
                            else
                            {
                                cta.Credit = Amount;
                            }

                            cta.Narration = Remark;
                            cta.VDate = Date;
                            cta.Vtype = Vtype;
                            cta.IsPosted = isposted;
                            cta.IsAppove = isaprove;
                            cta.IsOpening = Opening;
                            cta.InsertDate = logindate;
                            cta.InsertUserId = Loginuser;
                            cta.COA = Head1;
                            cta.COAId = Head;
                            cta.SchoolID = SchoolID;
                            db.Transactions.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }

                }


                if (a == "SaveDebitVoucher")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Payment = Convert.ToInt32(val[3]);
                    var Payment1 = Convert.ToInt32(val[4]);
                    var Amount = Convert.ToDouble(val[5]);
                    var Remark = val[6].ToString().Trim();
                    var loginuser = val[7].ToString().Trim();
                    var inserdate = DateTime.Now;
                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    int SchoolID = Convert.ToInt32(val[8]);

                    if (id == 0)
                    {
                        var check = db.TempDebit.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            TempDebit cta = new SchoolErp.TempDebit();
                            cta.BankId = Payment;
                            //cta.BankId = Payment1;
                            cta.Date = Date;
                            cta.PaidAmount = Amount;
                            cta.NetTotal = Amount;
                            cta.Details = Remark;
                            cta.PaymentAccount = Payment1;
                            cta.EmployeeId = loginuser;
                            //cta.InsertDate = inserdate;
                            cta.VNo = Vno;
                            cta.SchoolID = SchoolID;
                            //cta.Voucher = date1;
                            db.TempDebit.Add(cta);

                        }
                    }
                    db.SaveChanges();

                }



                if (a == "SaveCreditVoucher")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Payment = Convert.ToInt32(val[3]);
                    var Payment1 = Convert.ToInt32(val[4]);
                    var Amount = Convert.ToDouble(val[5]);
                    var Remark = val[6].ToString().Trim();
                    var loginuser = val[7].ToString().Trim();
                    var inserdate = DateTime.Now;
                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    int SchoolID = Convert.ToInt32(val[8]);

                    if (id == 0)
                    {
                        var check = db.TempCredit.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            TempCredit cta = new SchoolErp.TempCredit();
                            cta.BankId = Payment;
                            //cta.BankId = Payment1;
                            cta.Date = Date;
                            cta.PaidAmount = Amount;
                            cta.NetTotal = Amount;
                            cta.Details = Remark;
                            cta.PaymentAccount = Payment1;
                            cta.EmployeeId = loginuser;
                            //cta.InsertDate = inserdate;
                            cta.VNo = Vno;
                            //cta.Voucher = date1;
                            cta.SchoolID = SchoolID;
                            db.TempCredit.Add(cta);

                        }
                    }
                    db.SaveChanges();

                }

                if (a == "SaveContraVoucher")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Payment = Convert.ToInt32(val[3]);
                    var Coa = val[4].ToString().Trim();
                    var logindate = DateTime.Now;
                    var Debit = Convert.ToDouble(val[5]);
                    var Credit = Convert.ToDouble(val[6]);
                    var Remark = val[7].ToString().Trim();
                    var Loginuser = val[8].ToString().Trim();
                    var Opening = false;
                    var Vtype = "Contra";
                    var Vno = (DateTime.Now.ToString("yyyyMMddHHmmss"));
                    var isposted = true;
                    var isaprove = true;
                    int SchoolID = Convert.ToInt32(val[9]);

                    if (id == 0)
                    {
                        var check = db.Transactions.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Transactions cta = new SchoolErp.Transactions();
                            cta.VNo = Vno;
                            cta.Narration = Remark;
                            cta.VDate = Date;
                            cta.Debit = Debit;
                            cta.Credit = Credit;
                            cta.Vtype = Vtype;
                            cta.IsPosted = isposted;
                            cta.IsAppove = isaprove;
                            cta.IsOpening = Opening;
                            cta.InsertDate = logindate;
                            cta.InsertUserId = Loginuser;
                            cta.COA = Coa;
                            cta.COAId = Payment;
                            cta.SchoolID = SchoolID;
                            db.Transactions.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }

                }

                if (a == "SaveJournalVoucher")
                {

                    var id = Convert.ToInt32(val[1]);
                    var Date = Convert.ToDateTime(val[2]);
                    var Payment = Convert.ToInt32(val[3]);
                    var Coa = val[4].ToString().Trim();
                    var logindate = DateTime.Now;
                    var Debit = Convert.ToDouble(val[5]);
                    var Credit = Convert.ToDouble(val[6]);
                    var Remark = val[7].ToString().Trim();
                    var Loginuser = val[8].ToString().Trim();
                    var Opening = false;
                    var Vtype = "Journal";
                    var Vno = (DateTime.Now.ToString("yyyyMMddHHmmss"));
                    var isposted = true;
                    var isaprove = true;
                    int SchoolID = Convert.ToInt32(val[9]);


                    if (id == 0)
                    {
                        var check = db.Transactions.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Transactions cta = new SchoolErp.Transactions();
                            cta.VNo = Vno;
                            cta.Narration = Remark;
                            cta.VDate = Date;
                            cta.Debit = Debit;
                            cta.Credit = Credit;
                            cta.Vtype = Vtype;
                            cta.IsPosted = isposted;
                            cta.IsAppove = isaprove;
                            cta.IsOpening = Opening;
                            cta.InsertDate = logindate;
                            cta.InsertUserId = Loginuser;
                            cta.COA = Coa;
                            cta.COAId = Payment;
                            cta.SchoolID = SchoolID;
                            db.Transactions.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }







        [System.Web.Http.Route("api/AccountsAPI/SaveRecord")]
        [System.Web.Http.HttpPost]
        public int SaveRecord(List<string> val)
        {
            int avi = 0;
            try
            {

                var a = val[0];

                if (a == "NewCustomers")
                {


                    var id = Convert.ToInt32(val[1]);
                    var name = val[2].ToString().Trim();
                    var Cuntact = val[3].ToString().Trim();
                    var Title = val[4].ToString().Trim();
                    var Country = Convert.ToInt32(val[5]);
                    var State = Convert.ToInt32(val[6]);
                    var City = Convert.ToInt32(val[7]);
                    var Group = Convert.ToInt32(val[8]);
                    var Address = val[9].ToString().Trim();
                    var Regin = Convert.ToInt32(val[10]);
                    var Postal = val[11].ToString().Trim();
                    var Phone = val[12].ToString().Trim();
                    var Website = val[13].ToString().Trim();
                    var Fax = val[14].ToString().Trim();
                    var Email = val[15].ToString().Trim();
                    var EmailAddress = val[16].ToString().Trim();
                    var Balance = Convert.ToDouble(val[17]);
                    var loginuser = val[18].ToString().Trim();
                    var dated = DateTime.Now;
                    var active = Convert.ToInt32(val[19]);
                    int SchoolID = Convert.ToInt32(val[20]);

                    if (id == 0)
                    {
                        var check = db.Customers.Where(s => s.Name == name && s.ContactName == Cuntact && s.ContactTitle == Title && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Address == Address && s.RegionId == Regin && s.PostalCode == Postal && s.Phone == Phone && s.Fax == Fax && s.Website == Website && s.Email == Email && s.EmailAddress == EmailAddress && s.PreviousCreditBalance == Balance && s.CustomerGroupId == Group && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {


                            Customers cta = new SchoolErp.Customers();
                            cta.Name = name;
                            cta.ContactName = Cuntact;
                            cta.ContactTitle = Title;
                            cta.CountryId = Country;
                            cta.StateId = State;
                            cta.CityId = City;
                            cta.Address = Address;
                            cta.RegionId = Regin;
                            cta.PostalCode = Postal;
                            cta.Phone = Phone;
                            cta.Fax = Fax;
                            cta.Website = Website;
                            cta.Email = Email;
                            cta.EmailAddress = EmailAddress;
                            cta.PreviousCreditBalance = Balance;
                            cta.CustomerGroupId = Group;
                            cta.InsertUserId = loginuser;
                            cta.InsertDate = dated;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Customers.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }


                    else
                    {
                        var check = db.Customers.Where(s => s.Name == name && s.ContactName == Cuntact && s.ContactTitle == Title && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Address == Address && s.RegionId == Regin && s.PostalCode == Postal && s.Phone == Phone && s.Fax == Fax && s.Website == Website && s.Email == Email && s.EmailAddress == EmailAddress && s.PreviousCreditBalance == Balance && s.CustomerGroupId == Group && s.UpdateDate == dated && s.DeleteDate == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Customers.SingleOrDefault(b => b.Id == id);
                            result.Name = name;
                            result.Name = name;
                            result.ContactName = Cuntact;
                            result.ContactTitle = Title;
                            result.CountryId = Country;
                            result.StateId = State;
                            result.CityId = City;
                            result.Address = Address;
                            result.RegionId = Regin;
                            result.PostalCode = Postal;
                            result.Phone = Phone;
                            result.Fax = Fax;
                            result.Website = Website;
                            result.Email = Email;
                            result.EmailAddress = EmailAddress;
                            result.PreviousCreditBalance = Balance;
                            result.CustomerGroupId = Group;
                            result.UpdateUserId = loginuser;
                            result.InsertDate = dated;
                            result.IsActive = active;

                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }

                if (a == "NewGoodsMain")
                {

                    var id = Convert.ToInt32(val[1]);
                    var PurchaseId = Convert.ToInt32(val[2]);
                    var SupplierId = Convert.ToInt32(val[3]);
                    var EmployeeId = Convert.ToInt32(val[4]);
                    var Date = Convert.ToDateTime(val[5]);
                    var Ref = val[6].ToString().Trim();
                    var Total = Convert.ToDouble(val[7]);
                    var Remark = val[8].ToString().Trim();
                    var Journal = val[9].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[10]);


                    if (id == 0)
                    {
                        var check = db.GoodsReceipt.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            GoodsReceipt cta = new SchoolErp.GoodsReceipt();
                            cta.PurchaseId = PurchaseId;
                            cta.SupplierId = SupplierId;
                            cta.EmployeenId = EmployeeId;
                            cta.Date = Date;
                            cta.TotalQuantity = Total;
                            cta.Remarks = Remark;
                            cta.JournalRemarks = Journal;
                            cta.Reference = Ref;
                            cta.SchoolID = SchoolID;
                            db.GoodsReceipt.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {


                        var result = db.GoodsReceipt.SingleOrDefault(b => b.Id == id);
                        result.PurchaseId = PurchaseId;
                        result.SupplierId = SupplierId;
                        result.EmployeenId = EmployeeId;
                        result.Date = Date;
                        result.TotalQuantity = Total;
                        result.Remarks = Remark;
                        result.JournalRemarks = Journal;
                        result.Reference = Ref;
                        //cta.Reference = ref;
                        db.SaveChanges();

                    }


                }


                if (a == "NewGoods")


                {

                    var id = Convert.ToInt32(val[1]);
                    var Name = val[2].ToString().Trim();
                    var WareHouses = val[3].ToString().Trim();
                    var Product = Convert.ToInt32(val[4]);
                    var Warehouseid = Convert.ToInt32(val[5]);
                    var Quantity = Convert.ToDouble(val[6]);
                    var desc = val[7].ToString().Trim();
                    var idd = Convert.ToInt32(val[8]);



                    if (id == 0)
                    {
                        var receipt = db.GoodsReceipt.OrderByDescending(x => x.Id).ToList();
                        long receiptid = receipt[0].Id;
                        var check = db.GoodsReceiptDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            GoodsReceiptDetails cta = new SchoolErp.GoodsReceiptDetails();
                            cta.ProductName = Name;
                            cta.WarehouseName = WareHouses;
                            cta.ProductId = Product;
                            cta.WarehouseId = Warehouseid;
                            cta.Quantity = Quantity;
                            cta.GoodsReceiptId = receiptid;
                            cta.Description = desc;
                            db.GoodsReceiptDetails.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.GoodsReceiptDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                GoodsReceiptDetails cta = new SchoolErp.GoodsReceiptDetails();
                                cta.ProductName = Name;
                                cta.WarehouseName = WareHouses;
                                cta.ProductId = Product;
                                cta.WarehouseId = Warehouseid;
                                cta.Quantity = Quantity;
                                cta.GoodsReceiptId = id;
                                cta.Description = desc;
                                db.GoodsReceiptDetails.Add(cta);
                                db.SaveChanges();
                            }
                            else
                            {

                            }


                        }
                        else
                        {

                            var result = db.GoodsReceiptDetails.SingleOrDefault(b => b.Id == idd);
                            result.ProductName = Name;
                            result.WarehouseName = WareHouses;
                            result.ProductId = Product;
                            result.WarehouseId = Warehouseid;
                            result.Quantity = Quantity;
                            result.Description = desc;
                            db.SaveChanges();

                        }
                    }


                }

                if (a == "NewGoodsIssueMain")
                {

                    var id = Convert.ToInt32(val[1]);
                    var PurchaseId = Convert.ToInt32(val[2]);
                    var SupplierId = Convert.ToInt32(val[3]);
                    var EmployeeId = Convert.ToInt32(val[4]);
                    var Date = Convert.ToDateTime(val[5]);
                    var Ref = val[6].ToString().Trim();
                    var Total = Convert.ToDouble(val[7]);
                    var Remark = val[8].ToString().Trim();
                    var Journal = val[9].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[10]);


                    if (id == 0)
                    {
                        var check = db.GoodsIssue.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            GoodsIssue cta = new SchoolErp.GoodsIssue();
                            cta.SaleId = PurchaseId;
                            cta.CustomerId = SupplierId;
                            cta.EmployeenId = EmployeeId;
                            cta.Date = Date;
                            cta.TotalQuantity = Total;
                            cta.Remarks = Remark;
                            cta.JournalRemarks = Journal;
                            cta.Reference = Ref;
                            cta.SchoolID = SchoolID;
                            db.GoodsIssue.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                        var result = db.GoodsIssue.SingleOrDefault(b => b.Id == id);
                        result.SaleId = PurchaseId;
                        result.CustomerId = SupplierId;
                        result.EmployeenId = EmployeeId;
                        result.Date = Date;
                        result.TotalQuantity = Total;
                        result.Remarks = Remark;
                        result.JournalRemarks = Journal;
                        result.Reference = Ref;
                        db.SaveChanges();

                    }


                }


                if (a == "NewGoodsIssue")


                {

                    var id = Convert.ToInt32(val[1]);
                    var Name = val[2].ToString().Trim();
                    var WareHouses = val[3].ToString().Trim();
                    var Product = Convert.ToInt32(val[4]);
                    var Warehouseid = Convert.ToInt32(val[5]);
                    var Quantity = Convert.ToDouble(val[6]);
                    var desc = val[7].ToString().Trim();
                    var idd = Convert.ToInt32(val[8]);


                    if (id == 0)
                    {
                        var receipt = db.GoodsIssue.OrderByDescending(x => x.Id).ToList();
                        long receiptid = receipt[0].Id;
                        var check = db.GoodsIssueDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            GoodsIssueDetails cta = new SchoolErp.GoodsIssueDetails();
                            cta.ProductName = Name;
                            cta.WarehouseName = WareHouses;
                            cta.ProductId = Product;
                            cta.WarehouseId = Warehouseid;
                            cta.Quantity = Quantity;
                            cta.GoodsIssueId = receiptid;
                            cta.Description = desc;
                            db.GoodsIssueDetails.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.GoodsIssueDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                GoodsIssueDetails cta = new SchoolErp.GoodsIssueDetails();
                                cta.ProductName = Name;
                                cta.WarehouseName = WareHouses;
                                cta.ProductId = Product;
                                cta.WarehouseId = Warehouseid;
                                cta.Quantity = Quantity;
                                cta.GoodsIssueId = id;
                                cta.Description = desc;
                                db.GoodsIssueDetails.Add(cta);
                                db.SaveChanges();
                            }
                            else
                            {

                            }

                        }
                        else
                        {


                            var result = db.GoodsIssueDetails.SingleOrDefault(b => b.Id == idd);
                            result.ProductName = Name;
                            result.WarehouseName = WareHouses;
                            result.ProductId = Product;
                            result.WarehouseId = Warehouseid;
                            result.Quantity = Quantity;
                            result.Description = desc;
                            db.SaveChanges();

                        }
                    }


                }


                if (a == "NewSuppliers")
                {


                    var id = Convert.ToInt32(val[1]);
                    var name = val[2].ToString().Trim();
                    var Cuntact = val[3].ToString().Trim();
                    var Title = val[4].ToString().Trim();
                    var Country = Convert.ToInt32(val[5]);
                    var State = Convert.ToInt32(val[6]);
                    var City = Convert.ToInt32(val[7]);
                    var Group = Convert.ToInt32(val[8]);
                    var Address = val[9].ToString().Trim();
                    var Regin = Convert.ToInt32(val[10]);
                    var Postal = val[11].ToString().Trim();
                    var Phone = val[12].ToString().Trim();
                    var Website = val[13].ToString().Trim();
                    var Fax = val[14].ToString().Trim();
                    var Email = val[15].ToString().Trim();
                    var EmailAddress = val[16].ToString().Trim();
                    var Balance = Convert.ToDouble(val[17]);
                    var loginuser = val[18].ToString().Trim();
                    var dated = DateTime.Now;
                    var active = Convert.ToInt32(val[19]);
                    int SchoolID = Convert.ToInt32(val[20]);

                    if (id == 0)
                    {
                        var check = db.Suppliers.Where(s => s.CompanyName == name && s.ContactName == Cuntact && s.ContactTitle == Title && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Address == Address && s.RegionId == Regin && s.PostalCode == Postal && s.Phone == Phone && s.Fax == Fax && s.Website == Website && s.Email == Email && s.EmailAddress == EmailAddress && s.PreviousCreditBalance == Balance && s.SupplierGroupId == Group && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Suppliers cta = new SchoolErp.Suppliers();
                            cta.CompanyName = name;
                            cta.ContactName = Cuntact;
                            cta.ContactTitle = Title;
                            cta.CountryId = Country;
                            cta.StateId = State;
                            cta.CityId = City;
                            cta.Address = Address;
                            cta.RegionId = Regin;
                            cta.PostalCode = Postal;
                            cta.Phone = Phone;
                            cta.Fax = Fax;
                            cta.Website = Website;
                            cta.Email = Email;
                            cta.EmailAddress = EmailAddress;
                            cta.PreviousCreditBalance = Balance;
                            cta.SupplierGroupId = Group;
                            cta.InsertUserId = loginuser;
                            cta.InsertDate = dated;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Suppliers.Add(cta);
                            avi = 1;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    else
                    {
                        var check = db.Suppliers.Where(s => s.CompanyName == name && s.ContactName == Cuntact && s.ContactTitle == Title && s.CountryId == Country && s.StateId == State && s.CityId == City && s.Address == Address && s.RegionId == Regin && s.PostalCode == Postal && s.Phone == Phone && s.Fax == Fax && s.Website == Website && s.Email == Email && s.EmailAddress == EmailAddress && s.PreviousCreditBalance == Balance && s.SupplierGroupId == Group && s.UpdateDate == dated && s.UpdateUserId == loginuser && s.DeleteDate == null).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Suppliers.SingleOrDefault(b => b.Id == id);
                            result.CompanyName = name;
                            result.ContactName = Cuntact;
                            result.ContactTitle = Title;
                            result.CountryId = Country;
                            result.StateId = State;
                            result.CityId = City;
                            result.Address = Address;
                            result.RegionId = Regin;
                            result.PostalCode = Postal;
                            result.Phone = Phone;
                            result.Fax = Fax;
                            result.Website = Website;
                            result.Email = Email;
                            result.EmailAddress = EmailAddress;
                            result.PreviousCreditBalance = Balance;
                            result.SupplierGroupId = Group;
                            result.UpdateUserId = loginuser;
                            result.InsertDate = dated;
                            result.IsActive = active;

                            avi = 2;
                        }
                        else
                        {
                            avi = -1;
                        }
                    }
                    db.SaveChanges();

                }

                if (a == "Units")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Name = val[2].ToString().Trim();
                    var Desc = val[3].ToString().Trim();
                    var dated = DateTime.Now;
                    var loginuser = val[4].ToString().Trim();
                    var active = Convert.ToInt32(val[5]);
                    int SchoolID = Convert.ToInt32(val[6]);

                    if (id == 0)
                    {
                        var check = db.Units.Where(s => s.Name == Name && s.Description == Desc && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Units cta = new SchoolErp.Units();
                            cta.Name = Name;
                            cta.Description = Desc;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Units.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Units.Where(s => s.Name == Name && s.Description == Desc && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Units.SingleOrDefault(b => b.Id == id);
                            result.Name = Name;
                            result.Description = Desc;
                            result.UpdateUserId = loginuser; ;
                            result.UpdateDate = dated;

                        }
                        else
                        {
                        }
                    }
                    db.SaveChanges();

                }
                if (a == "Brands")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Name = val[2].ToString().Trim();
                    var Desc = val[3].ToString().Trim();
                    var dated = DateTime.Now;
                    var loginuser = val[4].ToString().Trim();
                    var active = Convert.ToInt32(val[5]);
                    int SchoolID = Convert.ToInt32(val[6]);

                    if (id == 0)
                    {
                        var check = db.Brands.Where(s => s.Name == Name && s.Description == Desc && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Brands cta = new SchoolErp.Brands();
                            cta.Name = Name;
                            cta.Description = Desc;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Brands.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Brands.Where(s => s.Name == Name && s.Description == Desc && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Brands.SingleOrDefault(b => b.Id == id);
                            result.Name = Name;
                            result.Description = Desc;
                            result.UpdateUserId = loginuser; ;
                            result.UpdateDate = dated;

                        }
                        else
                        {
                        }
                    }
                    db.SaveChanges();

                }

                if (a == "Categories")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Name = val[2].ToString().Trim();
                    var Desc = val[3].ToString().Trim();
                    //  var Cate = Convert.ToInt32(val[4]);
                    var dated = DateTime.Now;
                    var loginuser = val[4].ToString().Trim();
                    var active = Convert.ToInt32(val[5]);
                    int SchoolID = Convert.ToInt32(val[6]);

                    if (id == 0)
                    {
                        var check = db.Categories.Where(s => s.Name == Name && s.Description == Desc && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Categories cta = new SchoolErp.Categories();
                            cta.Name = Name;
                            cta.Description = Desc;
                            // cta.ParentCategoryId = Cate;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Categories.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Categories.Where(s => s.Name == Name && s.Description == Desc && s.UpdateDate == dated && s.UpdateUserId == loginuser).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Categories.SingleOrDefault(b => b.Id == id);
                            result.Name = Name;
                            result.Description = Desc;
                            //  result.ParentCategoryId = Cate;
                            result.UpdateUserId = loginuser; ;
                            result.UpdateDate = dated;

                        }
                        else
                        {
                        }
                    }
                    db.SaveChanges();

                }


                if (a == "NewProducts")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Barcode = val[2].ToString().Trim();
                    var SN = val[3].ToString().Trim();
                    var Product = val[4].ToString().Trim();
                    var Model = val[5].ToString().Trim();
                    var UnitOrder = Convert.ToDouble(val[6]);
                    var stoke = Convert.ToDouble(val[7]);
                    var UnitName = Convert.ToDouble(val[8]);
                    var Reorder = Convert.ToDouble(val[9]);
                    var Category = Convert.ToInt32(val[10]);
                    var Unit = Convert.ToInt32(val[11]);
                    var Brand = Convert.ToInt32(val[12]);
                    var ProductDetail = val[13].ToString().Trim();
                    var Image = val[14].ToString().Trim();
                    var dated = DateTime.Now;
                    var loginuser = val[15].ToString().Trim();
                    var active = Convert.ToInt32(val[16]);
                    int SchoolID = Convert.ToInt32(val[17]);

                    if (id == 0)
                    {
                        var check = db.Products.Where(s => s.Barcode == Barcode && s.SN == SN && s.ProductName == Product && s.ProductDetails == ProductDetail && s.Model == Model && s.Image == Image && s.UnitPrice == UnitOrder && s.UnitsInStock == stoke && s.UnitsOnOrder == UnitName && s.ReorderLevel == Reorder && s.CategoryId == Category && s.UnitId == Unit && s.BrandId == Brand && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            Products cta = new SchoolErp.Products();
                            cta.Barcode = Barcode;
                            cta.SN = SN;
                            cta.Image = Image;
                            cta.ProductName = Product;
                            cta.ProductDetails = ProductDetail;
                            cta.Model = Model;
                            cta.UnitPrice = UnitOrder;
                            cta.UnitsInStock = stoke;
                            cta.UnitsOnOrder = UnitName;
                            cta.ReorderLevel = Reorder;
                            cta.CategoryId = Category;
                            cta.UnitId = Unit;
                            cta.BrandId = Brand;
                            cta.InsertDate = dated;
                            cta.InsertUserId = loginuser;
                            cta.IsActive = active;
                            cta.SchoolID = SchoolID;
                            db.Products.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        var check = db.Products.Where(s => s.Barcode == Barcode && s.SN == SN && s.ProductName == Product && s.ProductDetails == ProductDetail && s.Model == Model && s.UnitPrice == UnitOrder && s.UnitsInStock == stoke && s.UnitsOnOrder == UnitName && s.ReorderLevel == Reorder && s.CategoryId == Category && s.UnitId == Unit && s.BrandId == Brand && s.InsertDate == dated && s.InsertUserId == loginuser && s.IsActive == active).FirstOrDefault();
                        if (check == null)
                        {

                            var result = db.Products.SingleOrDefault(b => b.Id == id);
                            result.Barcode = Barcode;
                            result.SN = SN;
                            result.ProductName = Product;
                            result.ProductDetails = ProductDetail;
                            result.Model = Model;
                            result.UnitPrice = UnitOrder;
                            result.UnitsInStock = stoke;
                            result.UnitsOnOrder = UnitName;
                            result.ReorderLevel = Reorder;
                            result.CategoryId = Category;
                            result.UnitId = Unit;
                            result.BrandId = Brand;
                            result.UpdateUserId = loginuser; ;
                            result.UpdateDate = dated;

                        }
                        else
                        {
                        }
                    }
                    db.SaveChanges();

                }



                if (a == "SavePurchase")
                {



                    var id = Convert.ToInt32(val[1]);
                    var ProductID = Convert.ToInt32(val[2]);
                    var unit = val[3].ToString().Trim();
                    var quantity = Convert.ToDouble(val[4]);
                    var unitprice = Convert.ToDouble(val[5]);
                    var Discount = Convert.ToDouble(val[6]);
                    var Total = Convert.ToDouble(val[7]);
                    var desc = val[8].ToString().Trim();
                    var unitid = Convert.ToInt32(val[9]);
                    var Name = val[10].ToString().Trim();
                    var idd = Convert.ToInt32(val[11]);
                    if (id == 0)
                    {
                        var purchase = db.Purchases.OrderByDescending(x => x.Id).ToList();
                        long purchaseid = purchase[0].Id;
                        var check = db.PurchaseDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)

                        {

                            PurchaseDetails cta = new SchoolErp.PurchaseDetails();
                            cta.ProductId = ProductID;
                            cta.UnitName = unit;
                            cta.Description = desc;
                            cta.Quantity = quantity;
                            cta.UnitPrice = unitprice;
                            cta.Discount = Discount;
                            cta.Total = Total;
                            cta.UnitId = unitid;
                            cta.ProductName = Name;
                            cta.PurchaseId = purchaseid;
                            db.PurchaseDetails.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.PurchaseDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                PurchaseDetails cta = new SchoolErp.PurchaseDetails();
                                cta.ProductId = ProductID;
                                cta.UnitName = unit;
                                cta.Description = desc;
                                cta.Quantity = quantity;
                                cta.UnitPrice = unitprice;
                                cta.Discount = Discount;
                                cta.Total = Total;
                                cta.UnitId = unitid;
                                cta.ProductName = Name;
                                cta.PurchaseId = id;
                                db.PurchaseDetails.Add(cta);
                            }
                            else
                            {

                            }

                        }
                        else
                        {

                            var result = db.PurchaseDetails.SingleOrDefault(b => b.Id == idd);
                            result.ProductId = ProductID;
                            result.UnitName = unit;
                            result.Description = desc;
                            result.Quantity = quantity;
                            result.UnitPrice = unitprice;
                            result.Discount = Discount;
                            result.Total = Total;
                            result.UnitId = unitid;
                            result.ProductName = Name;

                        }

                    }
                    db.SaveChanges();

                }


                if (a == "SavePurchaseMain")
                {


                    var id = Convert.ToInt32(val[1]);
                    var Supplier = Convert.ToInt32(val[2]);
                    var Invoice = val[3].ToString().Trim();
                    var Date = Convert.ToDateTime(val[4]);
                    var Discountm = Convert.ToDouble(val[5]);
                    var TotalDiscount = Convert.ToDouble(val[6]);
                    var Shiping = Convert.ToDouble(val[7]);
                    var GrandTotal = Convert.ToDouble(val[8]);
                    var NetTotal = Convert.ToDouble(val[9]);
                    var PaidAmount = Convert.ToDouble(val[10]);
                    var Due = Convert.ToDouble(val[11]);
                    var change = Convert.ToDouble(val[12]);
                    var Details = val[13].ToString().Trim();
                    var Payment = Convert.ToInt32(val[14]);
                    int SchoolID = Convert.ToInt32(val[15]);

                    //var date1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //var date1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    long Vno = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //DateTime myDate = new DateTime(2015, 12, 25, 10, 30, 45);
                    //int year = myDate.Year; // 2015  
                    //int month = myDate.Month; //12  
                    //int day = myDate.Day; // 25  
                    //int hour = myDate.Hour; // 10  
                    //int minute = myDate.Minute; // 30  
                    //int second = myDate.Second; // 45 
                    //var Vno = year+month+day+minute+second;
                    if (id == 0)
                    {
                        var check = db.Purchases.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Purchases cta = new SchoolErp.Purchases();
                            cta.SupplierId = Supplier;
                            cta.InvoiceNo = Invoice;
                            cta.Date = Date;
                            cta.Discount = Discountm;
                            cta.TotalDiscount = TotalDiscount;
                            cta.ShippingCost = Shiping;
                            cta.GrandTotal = GrandTotal;
                            cta.NetTotal = NetTotal;
                            cta.PaidAmount = PaidAmount;
                            cta.Due = Due;
                            cta.Change = change;
                            cta.Details = Details;
                            cta.PaymentAccount = Payment;
                            cta.VNo = Vno;
                            cta.SchoolID = SchoolID;
                            //cta.Voucher = date1;
                            db.Purchases.Add(cta);

                        }
                        else
                        {

                        }
                    }
                    else
                    {


                        var result = db.Purchases.SingleOrDefault(b => b.Id == id);
                        result.SupplierId = Supplier;
                        result.InvoiceNo = Invoice;
                        result.Date = Date;
                        result.Discount = Discountm;
                        result.TotalDiscount = TotalDiscount;
                        result.ShippingCost = Shiping;
                        result.GrandTotal = GrandTotal;
                        result.NetTotal = NetTotal;
                        result.PaidAmount = PaidAmount;
                        result.Due = Due;
                        result.Change = change;
                        result.Details = Details;
                        result.PaymentAccount = Payment;


                    }
                    db.SaveChanges();

                }
                if (a == "NewsaleQuotationMain")
                {


                    var id = Convert.ToInt32(val[1]);
                    var customer = Convert.ToInt32(val[2]);
                    var Date = Convert.ToDateTime(val[3].Trim());
                    var DueDate = Convert.ToDateTime(val[4].Trim());
                    var Discountm = Convert.ToDouble(val[5]);
                    var TotalDiscount = Convert.ToDouble(val[6]);
                    var Shiping = Convert.ToDouble(val[7]);
                    var GrandTotal = Convert.ToDouble(val[8]);
                    var TotalTax = Convert.ToDouble(val[9]);
                    var Tax = Convert.ToDouble(val[10]);
                    var NetTotal = Convert.ToDouble(val[11]);
                    var Details = val[12].ToString().Trim();
                    int SchoolID = Convert.ToInt32(val[13]);

                    if (id == 0)
                    {
                        var check = db.Quotations.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            Quotations cta = new SchoolErp.Quotations();
                            cta.CustomerId = customer;
                            cta.Date = Date;
                            cta.ExpiryDate = DueDate;
                            cta.Discount = Discountm;
                            cta.TotalDiscount = TotalDiscount;
                            cta.ShippingCost = Shiping;
                            cta.GrandTotal = GrandTotal;
                            cta.NetTotal = NetTotal;
                            cta.TotalTax = TotalTax;
                            cta.Vat = Tax;
                            cta.Details = Details;
                            cta.SchoolID = SchoolID;
                            db.Quotations.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                        var result = db.Quotations.SingleOrDefault(b => b.Id == id);
                        result.CustomerId = customer;
                        result.Date = Date;
                        result.ExpiryDate = DueDate;
                        result.Discount = Discountm;
                        result.TotalDiscount = TotalDiscount;
                        result.ShippingCost = Shiping;
                        result.GrandTotal = GrandTotal;
                        result.NetTotal = NetTotal;
                        result.TotalTax = TotalTax;
                        result.Vat = Tax;
                        result.Details = Details;
                        db.SaveChanges();

                    }


                }

                if (a == "NewsaleQuotation")
                {

                    var id = Convert.ToInt32(val[1]);
                    var ProductID = Convert.ToInt32(val[2]);
                    var unit = val[3].ToString().Trim();
                    var quantity = Convert.ToDouble(val[4]);
                    var unitprice = Convert.ToDouble(val[5]);
                    var Discount = Convert.ToDouble(val[6]);
                    var Total = Convert.ToDouble(val[7]);
                    var desc = val[8].ToString().Trim();
                    var unitid = Convert.ToInt32(val[9]);
                    var Name = val[10].ToString().Trim();
                    var Tax = Convert.ToDouble(val[11]);
                    var idd = Convert.ToInt32(val[12]);


                    if (id == 0)
                    {
                        var quotation = db.Quotations.OrderByDescending(x => x.Id).ToList();
                        long quotationId = quotation[0].Id;
                        var check = db.QuotationDetails.Where(s => s.Id == id).FirstOrDefault();
                        if (check == null)
                        {

                            QuotationDetails cta = new SchoolErp.QuotationDetails();
                            cta.ProductId = ProductID;
                            cta.UnitName = unit;
                            cta.Description = desc;
                            cta.Quantity = quantity;
                            cta.UnitPrice = unitprice;
                            cta.Discount = Discount;
                            cta.Total = Total;
                            cta.UnitId = unitid;
                            cta.ProductName = Name;
                            cta.QuotationId = quotationId;
                            cta.Tax = Tax;
                            db.QuotationDetails.Add(cta);
                            db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (idd == 0)
                        {
                            var check = db.QuotationDetails.Where(s => s.Id == idd).FirstOrDefault();
                            if (check == null)
                            {

                                QuotationDetails cta = new SchoolErp.QuotationDetails();
                                cta.ProductId = ProductID;
                                cta.UnitName = unit;
                                cta.Description = desc;
                                cta.Quantity = quantity;
                                cta.UnitPrice = unitprice;
                                cta.Discount = Discount;
                                cta.Total = Total;
                                cta.UnitId = unitid;
                                cta.ProductName = Name;
                                cta.QuotationId = id;
                                cta.Tax = Tax;
                                db.QuotationDetails.Add(cta);
                                db.SaveChanges();

                            }
                            else
                            {

                            }

                        }
                        else

                        {

                            var result = db.QuotationDetails.SingleOrDefault(b => b.Id == idd);
                            result.ProductId = ProductID;
                            result.UnitName = unit;
                            result.Description = desc;
                            result.Quantity = quantity;
                            result.UnitPrice = unitprice;
                            result.Discount = Discount;
                            result.Total = Total;
                            result.UnitId = unitid;
                            result.ProductName = Name;
                            result.Tax = Tax;
                            db.SaveChanges();
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (avi == -1)
            {
                return -1;
            }
            else if (avi == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }

        [System.Web.Http.Route("api/AccountsAPI/CustomerList")]
        [System.Web.Http.HttpPost]
        public List<CustomerList> CustomerList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<CustomerList> list = new List<CustomerList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select cu.id,cu.name,cu.ContactName,cu.ContactTitle,tblcountry.CountryID, tblcountry.CountryName,tblstate.StateName,tblcity.CityName,regions.Name as Region,Regions.Id as RegionID,
                                                  CustomerGroups.Name as Groups,CustomerGroups.Id as Groupid,
                                                  Cu.address,cu.PostalCode,cu.phone,cu.fax,cu.Website,
                                                  cu.email,cu.EmailAddress,cu.PreviousCreditBalance from Customers Cu
                                                  INNER JOIN tblCountry ON cu.CountryId=tblCountry.CountryID
                                                  INNER JOIN tblState ON cu.StateId=tblState.StateId
                                                  INNER JOIN tblCity ON cu.CityId=tblCity.Id
                                                   INNER JOIN Regions ON cu.RegionId=Regions.Id
                                                  INNER JOIN CustomerGroups ON cu.CustomerGroupId=CustomerGroups.Id where cu.DeleteDate is NULL and Cu.SchoolID='" + SchoolID + "' ");
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerList usr = new CustomerList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["name"].ToString();
                    usr.ContactName = dr["ContactName"].ToString();
                    usr.ContactTitle = dr["ContactTitle"].ToString();
                    usr.RegionID = dr["RegionID"].ToString();
                    usr.GroupID = dr["GroupID"].ToString();
                    usr.CountryID = dr["CountryID"].ToString();
                    usr.Country = dr["CountryName"].ToString();
                    usr.state = dr["StateName"].ToString();
                    usr.city = dr["CityName"].ToString();
                    usr.Address = dr["address"].ToString();
                    usr.region = dr["Region"].ToString();
                    usr.group = dr["Groups"].ToString();
                    usr.postal = dr["PostalCode"].ToString();
                    usr.phone = dr["phone"].ToString();
                    usr.fax = dr["fax"].ToString();
                    usr.Website = dr["Website"].ToString();
                    usr.Email = dr["email"].ToString();
                    usr.EmailAddress = dr["EmailAddress"].ToString();
                    usr.balance = dr["PreviousCreditBalance"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/CustomerList1")]
        [System.Web.Http.HttpPost]
        public List<CustomerList> CustomerList1(List<string> aa)
        {
            List<CustomerList> list = new List<CustomerList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select cu.id,cu.name,cu.ContactName,cu.ContactTitle,tblcountry.CountryID, tblcountry.CountryName,tblstate.StateName,Cu.StateId,Cu.CityId,tblcity.CityName,regions.Name as Region,Regions.Id as RegionID,
                                                  CustomerGroups.Name as Groups,CustomerGroups.Id as Groupid,
                                                  Cu.address,cu.PostalCode,cu.phone,cu.fax,cu.Website,
                                                  cu.email,cu.EmailAddress,cu.PreviousCreditBalance from Customers Cu
                                                  INNER JOIN tblCountry ON cu.CountryId=tblCountry.CountryID
                                                  INNER JOIN tblState ON cu.StateId=tblState.StateId
                                                  INNER JOIN tblCity ON cu.CityId=tblCity.Id
                                                   INNER JOIN Regions ON cu.RegionId=Regions.Id
                                                  INNER JOIN CustomerGroups ON cu.CustomerGroupId=CustomerGroups.Id where cu.DeleteDate is NULL and cu.id=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerList usr = new CustomerList();
                        usr.ID = dr["Id"].ToString();
                        usr.Name = dr["name"].ToString();
                        usr.ContactName = dr["ContactName"].ToString();
                        usr.ContactTitle = dr["ContactTitle"].ToString();
                        usr.RegionID = dr["RegionID"].ToString();
                        usr.GroupID = dr["GroupID"].ToString();
                        usr.CountryID = dr["CountryID"].ToString();
                        usr.Country = dr["CountryName"].ToString();
                        usr.state = dr["StateId"].ToString();
                        usr.city = dr["CityId"].ToString();
                        usr.Address = dr["address"].ToString();
                        usr.region = dr["Region"].ToString();
                        usr.group = dr["Groups"].ToString();
                        usr.postal = dr["PostalCode"].ToString();
                        usr.phone = dr["phone"].ToString();
                        usr.fax = dr["fax"].ToString();
                        usr.Website = dr["Website"].ToString();
                        usr.Email = dr["email"].ToString();
                        usr.EmailAddress = dr["EmailAddress"].ToString();
                        usr.balance = dr["PreviousCreditBalance"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/editGradeDetasilById1")]
        [System.Web.Http.HttpPost]
        public PurchaseBillList editGradeDetasilById1(List<string> id)
        {
            List<PurchaseBillList> list = new List<PurchaseBillList>();
            sqlHelper obj = new sqlHelper();
            System.Data.SqlClient.SqlDataReader dr = obj.GetReader("select gr.id,Suppliers.CompanyName,Suppliers.id as PurchaseId,tblEmployee.id as Employee,gr.PurchaseId,CONVERT(varchar, Date, 107)as Date,gr.TotalQuantity,tblEmployee.FirstName,tblEmployee.MiddleName,tblEmployee.LastName,gr.Remarks,gr.Reference from GoodsReceipt gr  INNER JOIN Suppliers ON gr.SupplierId=Suppliers.Id INNER JOIN tblEmployee ON gr.EmployeenId=tblEmployee.Id where gr.Id=" + id[0]);
            PurchaseBillList usr = new PurchaseBillList();
            if (dr.Read())
            {
                var dd = "BILL-";
                usr.ID = dr["Id"].ToString();
                usr.Bil = dd + usr.ID;
                usr.Supplier = dr["CompanyName"].ToString();
                usr.Bill = dr["PurchaseId"].ToString();
                usr.Date = dr["Date"].ToString();
                usr.Quantity = dr["TotalQuantity"].ToString();
                usr.second = dr["MiddleName"].ToString();
                usr.third = dr["LastName"].ToString();
                usr.first = dr["Employee"].ToString();
                usr.Employee = usr.first + " " + usr.second + "" + usr.third;
                usr.Remarks = dr["Remarks"].ToString();
                usr.Reference = dr["Reference"].ToString();
            }

            return usr;


        }


        [System.Web.Http.Route("api/AccountsAPI/PurchaseBillList")]
        [System.Web.Http.HttpPost]
        public List<PurchaseBillList> PurchaseBillList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<PurchaseBillList> list = new List<PurchaseBillList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select gr.id,Suppliers.CompanyName,gr.PurchaseId,CONVERT(varchar, Date, 107)as Date,gr.TotalQuantity,
                                                  tblEmployee.FirstName,tblEmployee.MiddleName,tblEmployee.LastName,gr.Remarks,gr.Reference from GoodsReceipt gr
                                                  INNER JOIN Suppliers ON gr.SupplierId=Suppliers.Id
                                                  INNER JOIN tblEmployee ON gr.EmployeenId=tblEmployee.Id where  gr.SchoolID='" + SchoolID + "' order by gr.Id ");
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseBillList usr = new PurchaseBillList();
                    var dd = "BILL-";
                    usr.ID = dr["Id"].ToString();
                    usr.Bil = dd + usr.ID;
                    usr.Supplier = dr["CompanyName"].ToString();
                    usr.Bill = dr["PurchaseId"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.Quantity = dr["TotalQuantity"].ToString();
                    usr.first = dr["FirstName"].ToString();
                    usr.second = dr["MiddleName"].ToString();
                    usr.third = dr["LastName"].ToString();
                    usr.first = dr["FirstName"].ToString();
                    usr.Employee = usr.first + " " + usr.second + "" + usr.third;
                    usr.Remarks = dr["Remarks"].ToString();
                    usr.Reference = dr["Reference"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/PurchaseBillList1")]
        [System.Web.Http.HttpPost]
        public List<PurchaseBillList> PurchaseBillList1(List<string> aa)
        {
            List<PurchaseBillList> list = new List<PurchaseBillList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select gr.id,Suppliers.CompanyName,gr.PurchaseId,gr.EmployeenId,gr.SupplierId,CONVERT(varchar, Date, 103)as Date,gr.TotalQuantity,
                                                  tblEmployee.FirstName,tblEmployee.MiddleName,tblEmployee.LastName,gr.Remarks,gr.Reference,gr.JournalRemarks from GoodsReceipt gr
                                                  INNER JOIN Suppliers ON gr.SupplierId=Suppliers.Id
                                                  INNER JOIN tblEmployee ON gr.EmployeenId=tblEmployee.Id where gr.id=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        PurchaseBillList usr = new PurchaseBillList();
                        var dd = "BILL-";
                        usr.ID = dr["Id"].ToString();
                        usr.Bil = dd + usr.ID;
                        usr.Supplier = dr["CompanyName"].ToString();
                        usr.Bill = dr["PurchaseId"].ToString();
                        usr.Date = dr["Date"].ToString();
                        usr.Quantity = dr["TotalQuantity"].ToString();
                        usr.first = dr["FirstName"].ToString();
                        usr.second = dr["MiddleName"].ToString();
                        usr.third = dr["LastName"].ToString();
                        usr.first = dr["FirstName"].ToString();
                        usr.Employee = usr.first + " " + usr.second + "" + usr.third;
                        usr.EmployeeId = dr["EmployeenId"].ToString();
                        usr.CID = dr["SupplierId"].ToString();

                        usr.Remarks = dr["Remarks"].ToString();
                        usr.Reference = dr["Reference"].ToString();
                        usr.JournalRemarks = dr["JournalRemarks"].ToString();

                        list.Add(usr);

                    }
                }
            }
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/PurchaseInvoiceList")]
        [System.Web.Http.HttpPost]
        public List<PurchaseBillList> PurchaseInvoiceList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<PurchaseBillList> list = new List<PurchaseBillList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select gr.id,Customers.Name,gr.Saleid,gr.CustomerId,CONVERT(varchar, Date, 107)as Date,gr.TotalQuantity,
                                                  tblEmployee.FirstName,tblEmployee.MiddleName,tblEmployee.LastName,gr.Remarks,gr.Reference,gr.JournalRemarks from GoodsIssue gr
                                                  INNER JOIN Customers ON gr.Customerid=Customers.Id
                                                  INNER JOIN tblEmployee ON gr.EmployeenId=tblEmployee.Id  where  gr.SchoolID='" + SchoolID + "' order by gr.Id");
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseBillList usr = new PurchaseBillList();
                    var dd = "INV-";
                    usr.ID = dr["Id"].ToString();
                    usr.Bil = dd + usr.ID;
                    usr.Supplier = dr["Name"].ToString();
                    usr.Bill = dr["Saleid"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.Quantity = dr["TotalQuantity"].ToString();
                    usr.first = dr["FirstName"].ToString();
                    usr.second = dr["MiddleName"].ToString();
                    usr.third = dr["LastName"].ToString();
                    usr.first = dr["FirstName"].ToString();
                    usr.Employee = usr.first + " " + usr.second + "" + usr.third;
                    usr.Remarks = dr["Remarks"].ToString();
                    usr.Reference = dr["Reference"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/PurchaseInvoiceList1")]
        [System.Web.Http.HttpPost]
        public List<PurchaseBillList> PurchaseInvoiceList1(List<string> aa)
        {
            List<PurchaseBillList> list = new List<PurchaseBillList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select gr.id,Customers.Name,gr.Customerid,gr.Saleid,gr.EmployeenId,CONVERT(varchar, Date, 103)as Date,gr.TotalQuantity,
                                                  tblEmployee.FirstName,tblEmployee.MiddleName,tblEmployee.LastName,gr.Remarks,gr.Reference,gr.JournalRemarks from GoodsIssue gr
                                                  INNER JOIN Customers ON gr.Customerid=Customers.Id
                                                  INNER JOIN tblEmployee ON gr.EmployeenId=tblEmployee.Id where gr.id=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        PurchaseBillList usr = new PurchaseBillList();
                        var dd = "INV-";
                        usr.ID = dr["Id"].ToString();
                        usr.Bil = dd + usr.ID;
                        usr.Supplier = dr["Name"].ToString();
                        usr.Bill = dr["Saleid"].ToString();
                        usr.Date = dr["Date"].ToString();
                        usr.Quantity = dr["TotalQuantity"].ToString();
                        usr.first = dr["FirstName"].ToString();
                        usr.second = dr["MiddleName"].ToString();
                        usr.third = dr["LastName"].ToString();
                        usr.first = dr["FirstName"].ToString();
                        usr.Employee = usr.first + " " + usr.second + "" + usr.third;
                        usr.EmployeeId = dr["EmployeenId"].ToString();
                        usr.CID = dr["Customerid"].ToString();

                        usr.Remarks = dr["Remarks"].ToString();
                        usr.Reference = dr["Reference"].ToString();
                        usr.JournalRemarks = dr["JournalRemarks"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/GoodIssueDetailList")]
        [System.Web.Http.HttpPost]
        public List<GoodIssueDetailList> GoodIssueDetailList(List<string> aa)
        {
            List<GoodIssueDetailList> list = new List<GoodIssueDetailList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@" select Id,ProductName,WarehouseName,Quantity,Description,ProductId,WarehouseId from GoodsIssueDetails where GoodsIssueId=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        GoodIssueDetailList usr = new GoodIssueDetailList();

                        usr.ID = dr["Id"].ToString();
                        usr.ProductName = dr["ProductName"].ToString();
                        usr.WarehouseName = dr["WarehouseName"].ToString();
                        usr.Quantity = dr["Quantity"].ToString();
                        usr.Description = dr["Description"].ToString();
                        usr.ProductId = dr["ProductId"].ToString();
                        usr.WarehouseId = dr["WarehouseId"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/PurchaseDetailList")]
        [System.Web.Http.HttpPost]
        public List<PurchaseDetails> PurchaseDetailList(List<string> aa)
        {
            int id = Convert.ToInt32(aa[0]);
            List<PurchaseDetails> list = new List<PurchaseDetails>();
            var subMods = db.PurchaseDetails.Where(x => x.PurchaseId == id).ToList();
            foreach (var s in subMods)
            {
                PurchaseDetails usr1 = new PurchaseDetails();
                usr1.ProductName = s.ProductName;
                usr1.ProductId = s.ProductId;
                usr1.Id = s.Id;
                usr1.UnitId = s.UnitId;
                usr1.UnitName = s.UnitName;
                usr1.UnitPrice = s.UnitPrice;
                usr1.Quantity = s.Quantity;
                usr1.Discount = s.Discount;
                usr1.Description = s.Description;
                usr1.Total = s.Total;

                list.Add(usr1);
            }





            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/ServiceDetails")]
        [System.Web.Http.HttpPost]
        public List<ServiceInvoiceDetails> ServiceDetails(List<string> aa)
        {
            int id = Convert.ToInt32(aa[0]);
            List<ServiceInvoiceDetails> list = new List<ServiceInvoiceDetails>();
            var subMods = db.ServiceInvoiceDetails.Where(x => x.ServiceInvoiceId == id).ToList();
            foreach (var s in subMods)
            {
                ServiceInvoiceDetails usr1 = new ServiceInvoiceDetails();
                usr1.ServiceName = s.ServiceName;
                usr1.ServiceId = s.ServiceId;
                usr1.Id = s.Id;
                usr1.Tax = s.Tax;
                //usr1.UnitName = s.UnitName;
                usr1.UnitPrice = s.UnitPrice;
                usr1.Quantity = s.Quantity;
                usr1.Discount = s.Discount;
                usr1.Description = s.Description;
                usr1.Total = s.Total;

                list.Add(usr1);
            }





            return list;
        }





        [System.Web.Http.Route("api/AccountsAPI/GoodReceiptDetailList")]
        [System.Web.Http.HttpPost]
        public List<GoodIssueDetailList> GoodReceiptDetailList(List<string> aa)
        {
            List<GoodIssueDetailList> list = new List<GoodIssueDetailList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@" select Id,ProductName,WarehouseName,Quantity,Description,ProductId,WarehouseId from GoodsReceiptDetails where GoodsReceiptId=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        GoodIssueDetailList usr = new GoodIssueDetailList();

                        usr.ID = dr["Id"].ToString();
                        usr.ProductName = dr["ProductName"].ToString();
                        usr.WarehouseName = dr["WarehouseName"].ToString();
                        usr.Quantity = dr["Quantity"].ToString();
                        usr.Description = dr["Description"].ToString();
                        usr.ProductId = dr["ProductId"].ToString();
                        usr.WarehouseId = dr["WarehouseId"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/QuotationRPT")]
        [System.Web.Http.HttpPost]
        public Student QuotationRPT(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            Quotations s = db.Quotations.Where(x => x.Id == a).FirstOrDefault();

            //st.Id = s.Id;
            st.bill = "#Q-000";
            st.Id = s.Id;
            st.bill = st.bill + s.Id;
            st.Date = s.Date;
            st.status = s.Date.ToShortDateString();
            st.Message = "Date:" + DateTime.Now.ToShortDateString();
            st.MDesig = s.Date.AddDays(10).ToShortDateString();
            st.Tax = s.TotalTax;
            st.TotalTax = s.Vat;
            st.Shiping = s.ShippingCost;
            st.netTotal = s.NetTotal;
            st.GrandTotal = s.GrandTotal;
            st.TotalDiscount = s.Discount;
            st.Detail = s.Details;
            st.customer = Convert.ToInt32(s.CustomerId);

            if (s.CustomerId != -1)
            {
                long customer = s.CustomerId;
                Customers sec = db.Customers.Where(x => x.Id == customer).FirstOrDefault();
                st.customer1 = sec.Name;
                st.cphone = sec.Phone;
                st.cemail = sec.Email;
                st.cAddress = sec.Address;
                int country = Convert.ToInt32(sec.CountryId);
                tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
                st.ccountry = cou.CountryName;
                int city = Convert.ToInt32(sec.CityId);
                tblCity cou1 = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
                st.ccity = cou1.CityName;



            }
            st.subModList = new List<ServiceInvoiceDetails>();
            if (s.Id != -1)
            {
                long sale = s.Id;
                var quo1 = db.QuotationDetails.Where(x => x.QuotationId == sale).ToList();
                foreach (var quo in quo1)
                {
                    ServiceInvoiceDetails usr1 = new ServiceInvoiceDetails();
                    usr1.ServiceName = quo.ProductName;
                    usr1.UnitPrice = quo.UnitPrice;
                    usr1.Quantity = quo.Quantity;
                    usr1.Discount = quo.Discount;
                    usr1.Total = quo.Total;
                    st.subModList.Add(usr1);
                }

            }
            return st;
        }






        [System.Web.Http.Route("api/AccountsAPI/SaleRPT")]
        [System.Web.Http.HttpPost]
        public Student SaleRPT(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            Sales s = db.Sales.Where(x => x.Id == a).FirstOrDefault();

            //st.Id = s.Id;
            st.bill = "#INV-000";
            st.Id = s.Id;
            st.bill = st.bill + s.Id;
            st.Date = s.Date;
            st.status = s.Date.ToShortDateString();
            st.Message = "Date:" + DateTime.Now.ToShortDateString();
            st.MDesig = s.Date.AddDays(10).ToShortDateString();
            st.Tax = s.TotalTax;
            st.TotalTax = s.Vat;
            st.Shiping = s.ShippingCost;
            st.netTotal = s.NetTotal;
            st.GrandTotal = s.GrandTotal;
            st.TotalDiscount = s.Discount;
            st.Detail = s.Details;
            st.customer = Convert.ToInt32(s.CustomerId);

            if (s.CustomerId != -1)
            {
                long customer = s.CustomerId;
                Customers sec = db.Customers.Where(x => x.Id == customer).FirstOrDefault();
                st.customer1 = sec.Name;
                st.cphone = sec.Phone;
                st.cemail = sec.Email;
                st.cAddress = sec.Address;
                int country = Convert.ToInt32(sec.CountryId);
                tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
                st.ccountry = cou.CountryName;
                int city = Convert.ToInt32(sec.CityId);
                tblCity cou1 = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
                st.ccity = cou1.CityName;



            }
            st.subModList = new List<ServiceInvoiceDetails>();
            if (s.Id != -1)
            {
                long sale = s.Id;
                var quo1 = db.SaleDetails.Where(x => x.SaleId == sale).ToList();
                foreach (var quo in quo1)
                {
                    ServiceInvoiceDetails usr1 = new ServiceInvoiceDetails();
                    usr1.ServiceName = quo.ProductName;
                    usr1.UnitPrice = quo.UnitPrice;
                    usr1.Quantity = quo.Quantity;
                    usr1.Discount = quo.Discount;
                    usr1.Total = quo.Total;
                    st.subModList.Add(usr1);
                }

            }
            return st;
        }




        [System.Web.Http.Route("api/AccountsAPI/ServiceRPT")]
        [System.Web.Http.HttpPost]
        public Student ServiceRPT(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            ServiceInvoices s = db.ServiceInvoices.Where(x => x.Id == a).FirstOrDefault();

            //st.Id = s.Id;
            st.bill = "#INV-000";
            st.Id = s.Id;
            st.bill = st.bill + s.Id;
            st.Date = s.Date;
            st.status = s.Date.ToShortDateString();
            st.Message = "Date:" + DateTime.Now.ToShortDateString();
            st.MDesig = s.Date.AddDays(10).ToShortDateString();
            st.Tax = s.TotalTax;
            st.TotalTax = s.Vat;
            st.Shiping = s.ShippingCost;
            st.netTotal = s.NetTotal;
            st.GrandTotal = s.GrandTotal;
            st.TotalDiscount = s.Discount;
            st.Detail = s.Details;
            st.customer = Convert.ToInt32(s.CustomerId);

            if (s.CustomerId != -1)
            {
                long customer = s.CustomerId;
                Customers sec = db.Customers.Where(x => x.Id == customer).FirstOrDefault();
                st.customer1 = sec.Name;
                st.cphone = sec.Phone;
                st.cemail = sec.Email;
                st.cAddress = sec.Address;
                int country = Convert.ToInt32(sec.CountryId);
                tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
                st.ccountry = cou.CountryName;
                int city = Convert.ToInt32(sec.CityId);
                tblCity cou1 = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
                st.ccity = cou1.CityName;



            }
            st.subModList = new List<ServiceInvoiceDetails>();
            if (s.Id != -1)
            {
                long sale = s.Id;
                var quo1 = db.ServiceInvoiceDetails.Where(x => x.ServiceInvoiceId == sale).ToList();
                foreach (var quo in quo1)
                {
                    ServiceInvoiceDetails usr1 = new ServiceInvoiceDetails();
                    usr1.ServiceName = quo.ServiceName;
                    usr1.UnitPrice = quo.UnitPrice;
                    usr1.Quantity = quo.Quantity;
                    usr1.Discount = quo.Discount;
                    usr1.Total = quo.Total;
                    st.subModList.Add(usr1);
                }

            }
            return st;
        }

        [System.Web.Http.Route("api/AccountsAPI/PurchaseRPT")]
        [System.Web.Http.HttpPost]
        public Student PurchaseRPT(Parameters param)
        {

            Student st = new Models.Student();
            int a = Convert.ToInt32(param.val);
            Purchases s = db.Purchases.Where(x => x.Id == a).FirstOrDefault();

            //st.Id = s.Id;
            st.bill = "#BL-000";
            st.Id = s.Id;
            st.bill = st.bill + s.Id;
            st.Date = s.Date;
            st.status = s.Date.ToShortDateString();
            st.Message = "Date:" + DateTime.Now.ToShortDateString();
            st.MDesig = s.Date.AddDays(10).ToShortDateString();
            st.Tax = 0;
            st.TotalTax = 0;
            st.Shiping = s.ShippingCost;
            st.netTotal = s.NetTotal;
            st.GrandTotal = s.GrandTotal;
            st.TotalDiscount = s.Discount;
            st.Detail = s.Details;
            st.customer = Convert.ToInt32(s.SupplierId);

            if (s.SupplierId != -1)
            {
                long customer = s.SupplierId;
                Suppliers sec = db.Suppliers.Where(x => x.Id == customer).FirstOrDefault();
                st.customer1 = sec.ContactName;
                st.cphone = sec.Phone;
                st.cemail = sec.Email;
                st.cAddress = sec.Address;
                int country = Convert.ToInt32(sec.CountryId);
                tblCountry cou = db.tblCountries.Where(x => x.CountryID == country).FirstOrDefault();
                st.ccountry = cou.CountryName;
                int city = Convert.ToInt32(sec.CityId);
                tblCity cou1 = db.tblCities.Where(x => x.Id == city).FirstOrDefault();
                st.ccity = cou1.CityName;



            }
            st.subModList = new List<ServiceInvoiceDetails>();
            if (s.Id != -1)
            {
                long sale = s.Id;
                var quo1 = db.PurchaseDetails.Where(x => x.PurchaseId == sale).ToList();
                foreach (var quo in quo1)
                {
                    ServiceInvoiceDetails usr1 = new ServiceInvoiceDetails();
                    usr1.ServiceName = quo.ProductName;
                    usr1.UnitPrice = quo.UnitPrice;
                    usr1.Quantity = quo.Quantity;
                    usr1.Discount = quo.Discount;
                    usr1.Total = quo.Total;
                    st.subModList.Add(usr1);
                }

            }
            return st;
        }



        [System.Web.Http.Route("api/AccountsAPI/QuotationList")]
        [System.Web.Http.HttpPost]

        public List<QuotationList> QuotationList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<QuotationList> list = new List<QuotationList>();

            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select QS.Id, Customers.Name,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 101)as Date,  QS.Discount,QS.TotalDiscount,Qs.Vat As Tax,Qs.TotalTax,
                                                 QS.ShippingCost,QS.GrandTotal,QS.NetTotal,QS.Details,CONVERT(varchar, ExpiryDate, 101)as ExpiryDate from  Quotations QS 
                                                   INNER JOIN Customers ON QS.CustomerId=Customers.Id
												    where QS.SchoolId='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    QuotationList usr = new QuotationList();
                    var dd = "#Q-000";

                    usr.Id = dr["Id"].ToString();
                    usr.bill = dd + usr.Id;
                    usr.customer = dr["Name"].ToString();
                    usr.Address = dr["Address"].ToString();
                    usr.phone = dr["Phone"].ToString();
                    usr.email = dr["email"].ToString();
                    usr.Today = DateTime.Now;
                    usr.Date = dr["Date"].ToString();
                    //usr.Product = dr["ProductName"].ToString();
                    //usr.UnitPrice = dr["UnitPrice"].ToString();
                    //usr.Quantity = dr["Quantity"].ToString();
                    usr.Discount = dr["Discount"].ToString();
                    usr.TotalDiscount = dr["TotalDiscount"].ToString();
                    usr.Tax = dr["Tax"].ToString();
                    usr.TotalTax = (dr["TotalTax"].ToString());
                    usr.Shiping = dr["ShippingCost"].ToString();
                    usr.GrandTotal = dr["GrandTotal"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    usr.Detail = dr["Details"].ToString();
                    usr.ExpDate = dr["ExpiryDate"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/ServiceList")]
        [System.Web.Http.HttpPost]

        public List<ServiceList1> ServiceList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<ServiceList1> list = new List<ServiceList1>();

            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select S.Id,tblEmployee.firstname,tblEmployee.MiddleName,tblEmployee.LastName, Customers.Name,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 101)as Date,CONVERT(varchar, Date+10, 101)as DueDate,  S.Discount,S.TotalDiscount,s.Vat As Tax,s.TotalTax
                                                 ,S.ShippingCost,S.GrandTotal,S.NetTotal,s.PaidAmount,s.Due,s.Change,S.Details from  ServiceInvoices S 
                                                   INNER JOIN Customers ON S.CustomerId=Customers.Id
												     INNER JOIN tblEmployee ON S.EmployeeId=tblEmployee.Id
												   where S.SchoolId='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    ServiceList1 usr = new ServiceList1();
                    var dd = " #INV-000";
                    usr.Id = dr["Id"].ToString();
                    usr.bill = dd + usr.Id;
                    usr.customer = dr["Name"].ToString();
                    usr.First = dr["firstname"].ToString();
                    usr.second = dr["MiddleName"].ToString();
                    usr.third = dr["LastName"].ToString();
                    usr.Employee = usr.First + " " + usr.second + "" + usr.third;
                    usr.Address = dr["Address"].ToString();
                    usr.phone = dr["Phone"].ToString();
                    usr.email = dr["email"].ToString();
                    usr.Today = DateTime.Now;
                    usr.Date = dr["Date"].ToString();
                    //usr.Product = dr["ServiceName"].ToString();
                    //usr.UnitPrice = dr["UnitPrice"].ToString();
                    //usr.Quantity = dr["Quantity"].ToString();
                    usr.Discount = dr["Discount"].ToString();
                    usr.TotalDiscount = dr["TotalDiscount"].ToString();
                    usr.TotalTax = (dr["TotalTax"].ToString());
                    usr.Shiping = dr["ShippingCost"].ToString();
                    usr.GrandTotal = dr["GrandTotal"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    usr.Detail = dr["Details"].ToString();
                    usr.ExpDate = dr["DueDate"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/SaleList")]
        [System.Web.Http.HttpPost]

        public List<SaleList> SaleList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<SaleList> list = new List<SaleList>();

            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select S.Id, Customers.Name,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 101)as Date,CONVERT(varchar, Date+10, 101)as DueDate,  S.Discount,S.TotalDiscount,s.Vat As Tax,s.TotalTax
                                                 ,S.ShippingCost,S.GrandTotal,S.NetTotal,s.PaidAmount,s.Due,s.Change,S.Details from  Sales S 
                                                   INNER JOIN Customers ON S.CustomerId=Customers.Id
												   where S.SchoolId='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    SaleList usr = new SaleList();
                    var dd = "#INV-000";

                    usr.Id = dr["Id"].ToString();
                    usr.bill = dd + usr.Id;
                    usr.customer = dr["Name"].ToString();
                    usr.Address = dr["Address"].ToString();
                    usr.phone = dr["Phone"].ToString();
                    usr.email = dr["email"].ToString();
                    usr.Today = DateTime.Now;
                    usr.Date = dr["Date"].ToString();
                    //usr.Product = dr["ProductName"].ToString();
                    //usr.UnitPrice = dr["UnitPrice"].ToString();
                    //usr.Quantity = dr["Quantity"].ToString();
                    usr.Discount = dr["Discount"].ToString();
                    usr.TotalDiscount = dr["TotalDiscount"].ToString();
                    usr.Tax = dr["Tax"].ToString();
                    usr.TotalTax = (dr["TotalTax"].ToString());
                    usr.Shiping = dr["ShippingCost"].ToString();
                    usr.GrandTotal = dr["GrandTotal"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    usr.Detail = dr["Details"].ToString();
                    usr.ExpDate = dr["DueDate"].ToString();



                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/SaleList1")]
        [System.Web.Http.HttpPost]

        public List<SaleList> SaleList1(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);

            List<SaleList> list = new List<SaleList>();

            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select S.Id,Customers.Id as CID,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 103)as Date,CONVERT(varchar, Date+10, 101)as DueDate,  S.Discount,S.TotalDiscount,s.Vat As Tax,s.TotalTax
                                                 ,S.ShippingCost,S.GrandTotal,S.NetTotal,S.PaymentAccount,s.PaidAmount,s.Due,s.Change,S.Details from  Sales S 
                                                   INNER JOIN Customers ON S.CustomerId=Customers.Id
												   where S.Id='" + aa[0] + "'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        SaleList usr = new SaleList();
                        var dd = "#INV-000";

                        usr.Id = dr["Id"].ToString();
                        usr.bill = dd + usr.Id;
                        usr.customer = dr["CID"].ToString();
                        usr.Address = dr["Address"].ToString();
                        usr.phone = dr["Phone"].ToString();
                        usr.email = dr["email"].ToString();
                        usr.Today = DateTime.Now;
                        usr.Date = dr["Date"].ToString();
                        usr.Product = dr["PaymentAccount"].ToString();
                        usr.UnitPrice = dr["PaidAmount"].ToString();
                        usr.Quantity = dr["Due"].ToString();
                        usr.Discount = dr["Discount"].ToString();
                        usr.TotalDiscount = dr["TotalDiscount"].ToString();
                        usr.Tax = dr["Tax"].ToString();
                        usr.TotalTax = (dr["TotalTax"].ToString());
                        usr.Shiping = dr["ShippingCost"].ToString();
                        usr.GrandTotal = dr["GrandTotal"].ToString();
                        usr.netTotal = dr["NetTotal"].ToString();
                        usr.Detail = dr["Details"].ToString();
                        usr.ExpDate = dr["Change"].ToString();
                        usr.subModList = new List<SaleList>();

                        var sale = Convert.ToInt32(dr["Id"].ToString());
                        var quo1 = db.SaleDetails.Where(x => x.SaleId == sale).ToList();
                        foreach (var quo in quo1)
                        {
                            SaleList usr1 = new SaleList();
                            usr1.Product = quo.ProductName;
                            usr1.customer = quo.UnitName;
                            usr1.Detail = quo.Description;
                            usr1.UnitPrice = Convert.ToString(quo.UnitPrice);
                            usr1.Quantity = Convert.ToString(quo.Quantity);
                            usr1.Discount = Convert.ToString(quo.Discount);
                            usr1.Tax = Convert.ToString(quo.Tax);
                            usr1.netTotal = Convert.ToString(quo.Total);
                            usr1.UnitId = Convert.ToString(quo.UnitId);
                            usr1.Productid = Convert.ToString(quo.ProductId);
                            usr1.Id = Convert.ToString(quo.Id);
                            usr.subModList.Add(usr1);
                        }
                        list.Add(usr);

                    }
                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/QuotationList1")]
        [System.Web.Http.HttpPost]

        public List<SaleList> QuotationList1(List<string> aa)
        {
            //int SchoolID = Convert.ToInt32(aa[0]);

            List<SaleList> list = new List<SaleList>();

            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select QS.Id, Customers.Id as CID,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 103)as Date,  QS.Discount,QS.TotalDiscount,Qs.Vat As Tax,Qs.TotalTax,
                                                 QS.ShippingCost,QS.GrandTotal,QS.NetTotal,QS.Details,CONVERT(varchar, ExpiryDate, 103)as ExpiryDate from  Quotations QS 
                                                   INNER JOIN Customers ON QS.CustomerId=Customers.Id
												    where QS.Id='" + aa[0] + "'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        SaleList usr = new SaleList();
                        var dd = "#INV-000";

                        usr.Id = dr["Id"].ToString();
                        usr.bill = dd + usr.Id;
                        usr.customer = dr["CID"].ToString();
                        usr.Address = dr["Address"].ToString();
                        usr.phone = dr["Phone"].ToString();
                        usr.email = dr["email"].ToString();
                        usr.Today = DateTime.Now;
                        usr.Date = dr["Date"].ToString();
                        //usr.Product = dr["PaymentAccount"].ToString();
                        // usr.UnitPrice = dr["PaidAmount"].ToString();
                        //usr.Quantity = dr["Due"].ToString();
                        usr.Discount = dr["Discount"].ToString();
                        usr.TotalDiscount = dr["TotalDiscount"].ToString();
                        usr.Tax = dr["Tax"].ToString();
                        usr.TotalTax = (dr["TotalTax"].ToString());
                        usr.Shiping = dr["ShippingCost"].ToString();
                        usr.GrandTotal = dr["GrandTotal"].ToString();
                        usr.netTotal = dr["NetTotal"].ToString();
                        usr.Detail = dr["Details"].ToString();
                        usr.ExpDate = dr["ExpiryDate"].ToString();
                        usr.subModList = new List<SaleList>();

                        var sale = Convert.ToInt32(dr["Id"].ToString());
                        var quo1 = db.QuotationDetails.Where(x => x.QuotationId == sale).ToList();
                        foreach (var quo in quo1)
                        {
                            SaleList usr1 = new SaleList();
                            usr1.Product = quo.ProductName;
                            usr1.customer = quo.UnitName;
                            usr1.Detail = quo.Description;
                            usr1.UnitPrice = Convert.ToString(quo.UnitPrice);
                            usr1.Quantity = Convert.ToString(quo.Quantity);
                            usr1.Discount = Convert.ToString(quo.Discount);
                            usr1.Tax = Convert.ToString(quo.Tax);
                            usr1.netTotal = Convert.ToString(quo.Total);
                            usr1.UnitId = Convert.ToString(quo.UnitId);
                            usr1.Productid = Convert.ToString(quo.ProductId);
                            usr1.Id = Convert.ToString(quo.Id);
                            usr.subModList.Add(usr1);
                        }
                        list.Add(usr);

                    }
                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/ServiceList1")]
        [System.Web.Http.HttpPost]

        public List<SaleList> ServiceList1(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);

            List<SaleList> list = new List<SaleList>();

            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select S.Id,s.EmployeeId,tblEmployee.firstname,tblEmployee.MiddleName,tblEmployee.LastName, Customers.Id as CID,Customers.Address,Customers.Phone,Customers.Email,CONVERT(varchar, Date, 103)as Date,CONVERT(varchar, Date+10, 103)as DueDate,  S.Discount,S.TotalDiscount,s.Vat As Tax,s.TotalTax
                                                 ,S.ShippingCost,S.GrandTotal,S.NetTotal,s.PaidAmount,s.Due,s.PaymentAccount,s.Change,S.Details from  ServiceInvoices S 
                                                   INNER JOIN Customers ON S.CustomerId=Customers.Id
												     INNER JOIN tblEmployee ON S.EmployeeId=tblEmployee.Id
												   where S.Id='" + aa[0] + "'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        SaleList usr = new SaleList();
                        var dd = "#INV-000";

                        usr.Id = dr["Id"].ToString();
                        usr.bill = dd + usr.Id;
                        usr.customer = dr["CID"].ToString();

                        usr.Address = dr["Address"].ToString();
                        usr.phone = dr["Phone"].ToString();
                        usr.email = dr["EmployeeId"].ToString();
                        usr.Today = DateTime.Now;
                        usr.Date = dr["Date"].ToString();
                        usr.Product = dr["PaymentAccount"].ToString();
                        usr.UnitPrice = dr["PaidAmount"].ToString();
                        usr.Quantity = dr["Due"].ToString();
                        usr.Discount = dr["Discount"].ToString();
                        usr.TotalDiscount = dr["TotalDiscount"].ToString();
                        usr.Tax = dr["Tax"].ToString();
                        usr.TotalTax = (dr["TotalTax"].ToString());
                        usr.Shiping = dr["ShippingCost"].ToString();
                        usr.GrandTotal = dr["GrandTotal"].ToString();
                        usr.netTotal = dr["NetTotal"].ToString();
                        usr.Detail = dr["Details"].ToString();
                        usr.ExpDate = dr["Change"].ToString();
                        usr.subModList = new List<SaleList>();

                        var sale = Convert.ToInt32(dr["Id"].ToString());
                        var quo1 = db.ServiceInvoiceDetails.Where(x => x.ServiceInvoiceId == sale).ToList();
                        foreach (var quo in quo1)
                        {
                            SaleList usr1 = new SaleList();
                            usr1.Product = quo.ServiceName;
                            // usr1.customer = quo.UnitName;
                            usr1.Detail = quo.Description;
                            usr1.UnitPrice = Convert.ToString(quo.UnitPrice);
                            usr1.Quantity = Convert.ToString(quo.Quantity);
                            usr1.Discount = Convert.ToString(quo.Discount);
                            usr1.Tax = Convert.ToString(quo.Tax);
                            usr1.netTotal = Convert.ToString(quo.Total);
                            //usr1.UnitId = Convert.ToString(quo.UnitId);//
                            usr1.Productid = Convert.ToString(quo.ServiceId);
                            usr1.Id = Convert.ToString(quo.Id);
                            usr.subModList.Add(usr1);
                        }
                        list.Add(usr);

                    }
                }
            }
            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/SupplierList")]
        [System.Web.Http.HttpPost]
        public List<Supplier1List> SupplierList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<Supplier1List> list = new List<Supplier1List>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select cu.id,cu.CompanyName,cu.ContactName,cu.ContactTitle,tblcountry.CountryID, tblcountry.CountryName,tblstate.StateName,tblcity.CityName,regions.Name as Region,Regions.Id as RegionID,
                                                  SupplierGroups.Name as Groups,SupplierGroups.Id as Groupid,
                                                  Cu.address,cu.PostalCode,cu.phone,cu.fax,cu.Website,
                                                  cu.email,cu.EmailAddress,cu.PreviousCreditBalance from Suppliers Cu
                                                  INNER JOIN tblCountry ON cu.CountryId=tblCountry.CountryID
                                                  INNER JOIN tblState ON cu.StateId=tblState.StateId
                                                  INNER JOIN tblCity ON cu.CityId=tblCity.Id
                                                   INNER JOIN Regions ON cu.RegionId=Regions.Id
                                                  INNER JOIN SupplierGroups ON cu.SupplierGroupId=SupplierGroups.Id where cu.DeleteDate is NULL and cu.SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    Supplier1List usr = new Supplier1List();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["CompanyName"].ToString();
                    usr.ContactName = dr["ContactName"].ToString();
                    usr.ContactTitle = dr["ContactTitle"].ToString();
                    usr.RegionID = dr["RegionID"].ToString();
                    usr.GroupID = dr["GroupID"].ToString();
                    usr.CountryID = dr["CountryID"].ToString();
                    usr.Country = dr["CountryName"].ToString();
                    usr.state = dr["StateName"].ToString();
                    usr.city = dr["CityName"].ToString();
                    usr.Address = dr["address"].ToString();
                    usr.region = dr["Region"].ToString();
                    usr.group = dr["Groups"].ToString();
                    usr.postal = dr["PostalCode"].ToString();
                    usr.phone = dr["phone"].ToString();
                    usr.fax = dr["fax"].ToString();
                    usr.Website = dr["Website"].ToString();
                    usr.Email = dr["email"].ToString();
                    usr.EmailAddress = dr["EmailAddress"].ToString();
                    usr.balance = dr["PreviousCreditBalance"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/SupplierList1")]
        [System.Web.Http.HttpPost]
        public List<Supplier1List> SupplierList1(List<string> aa)
        {
            List<Supplier1List> list = new List<Supplier1List>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select cu.id,cu.CompanyName,cu.ContactName,cu.ContactTitle,tblcountry.CountryID, tblcountry.CountryName,tblstate.StateName,tblcity.CityName,Cu.StateId,Cu.CityId,regions.Name as Region,Regions.Id as RegionID,
                                                  SupplierGroups.Name as Groups,SupplierGroups.Id as Groupid,
                                                  Cu.address,cu.PostalCode,cu.phone,cu.fax,cu.Website,
                                                  cu.email,cu.EmailAddress,cu.PreviousCreditBalance from Suppliers Cu
                                                  INNER JOIN tblCountry ON cu.CountryId=tblCountry.CountryID
                                                  INNER JOIN tblState ON cu.StateId=tblState.StateId
                                                  INNER JOIN tblCity ON cu.CityId=tblCity.Id
                                                   INNER JOIN Regions ON cu.RegionId=Regions.Id
                                                  INNER JOIN SupplierGroups ON cu.SupplierGroupId=SupplierGroups.Id where cu.DeleteDate is NULL and cu.id=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        Supplier1List usr = new Supplier1List();
                        usr.ID = dr["Id"].ToString();
                        usr.Name = dr["CompanyName"].ToString();
                        usr.ContactName = dr["ContactName"].ToString();
                        usr.ContactTitle = dr["ContactTitle"].ToString();
                        usr.RegionID = dr["RegionID"].ToString();
                        usr.GroupID = dr["GroupID"].ToString();
                        usr.CountryID = dr["CountryID"].ToString();
                        usr.Country = dr["CountryName"].ToString();
                        usr.state = dr["StateId"].ToString();
                        usr.city = dr["CityId"].ToString();
                        usr.Address = dr["address"].ToString();
                        usr.region = dr["Region"].ToString();
                        usr.group = dr["Groups"].ToString();
                        usr.postal = dr["PostalCode"].ToString();
                        usr.phone = dr["phone"].ToString();
                        usr.fax = dr["fax"].ToString();
                        usr.Website = dr["Website"].ToString();
                        usr.Email = dr["email"].ToString();
                        usr.EmailAddress = dr["EmailAddress"].ToString();
                        usr.balance = dr["PreviousCreditBalance"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/UnitList")]
        [System.Web.Http.HttpPost]
        public List<UnitList> UnitList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<UnitList> list = new List<UnitList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from Units where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    UnitList usr = new UnitList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/DamageList")]
        [System.Web.Http.HttpPost]
        public List<DamageList> DamageList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<DamageList> list = new List<DamageList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select Categories.Name,DS.Id,DS.PurchasePrice,DS.Quantity,CONVERT(varchar, Date, 103)as Date,DS.Note,DS.name as Product  from DamagedProducts DS
                                                 INNER JOIN Categories ON DS.CategoryId=Categories.Id where  DS.SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    DamageList usr = new DamageList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Product = dr["Product"].ToString();
                    usr.Quantity = dr["Quantity"].ToString();
                    usr.PurchasePrice = dr["PurchasePrice"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.Note = dr["Note"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/DamageList1")]
        [System.Web.Http.HttpPost]
        public List<DamageList> DamageList1(List<string> aa)
        {
            List<DamageList> list = new List<DamageList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select Categories.Name,DS.Id,DS.PurchasePrice,DS.CategoryId,DS.ProductId,DS.Quantity,CONVERT(varchar, Date, 103)as Date,DS.Note,DS.name as Product  from DamagedProducts DS
                                                 INNER JOIN Categories ON DS.CategoryId=Categories.Id and DS.Id=" + aa[0] + "");
                    foreach (DataRow dr in dt.Rows)
                    {
                        DamageList usr = new DamageList();
                        usr.ID = dr["Id"].ToString();
                        usr.Name = dr["CategoryId"].ToString();
                        usr.Product = dr["Product"].ToString();
                        usr.Productid = dr["ProductId"].ToString();
                        usr.Quantity = dr["Quantity"].ToString();
                        usr.PurchasePrice = dr["PurchasePrice"].ToString();
                        usr.Date = dr["Date"].ToString();
                        usr.Note = dr["Note"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/ProductdetailsList")]
        [System.Web.Http.HttpPost]
        public List<ProductdetailsList> ProductdetailsList(List<string> aa)
        {
            List<ProductdetailsList> list = new List<ProductdetailsList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select Id, ProductName,Description,UnitName,Quantity,UnitPrice,Discount,Total from PurchaseDetails");
                foreach (DataRow dr in dt.Rows)
                {
                    ProductdetailsList usr = new ProductdetailsList();
                    usr.ID = dr["Id"].ToString();
                    usr.Product = dr["ProductName"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    usr.unit = dr["UnitName"].ToString();
                    usr.price = dr["UnitPrice"].ToString();
                    usr.quantity = dr["Quantity"].ToString();
                    usr.discount = dr["Discount"].ToString();
                    usr.total = dr["Total"].ToString();
                    list.Add(usr);


                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/BrandList")]
        [System.Web.Http.HttpPost]
        public List<BrandList> BrandList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<BrandList> list = new List<BrandList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from Brands where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    BrandList usr = new BrandList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/CategoryList")]
        [System.Web.Http.HttpPost]
        public List<BrandList> CategoryList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<BrandList> list = new List<BrandList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"Select * from Categories where DeleteDate  is  NULL and SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    BrandList usr = new BrandList();
                    usr.ID = dr["Id"].ToString();
                    usr.Name = dr["Name"].ToString();
                    usr.Desc = dr["Description"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/ProductList")]
        [System.Web.Http.HttpPost]
        public List<ProductList> ProductList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<ProductList> list = new List<ProductList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select p.id,p.Barcode,p.SN,p.ProductName,p.Model,p.unitprice,p.UnitId,p.Image,p.UnitsInStock,p.UnitsOnOrder,t.PQ,t.PU,s.SQ,t.PQ-s.SQ as Stock,p.ReorderLevel,p.ProductDetails,p.CategoryId,p.UnitId,p.BrandId,c.Name as CN,u.Name as UN,b.Name as BN
                                                from Products p left join Categories c on c.Id=p.CategoryId left join Units u on u.Id=p.UnitId left join Brands b on b.id=p.BrandId 
                                                left join  (select sum(pd.Quantity) as PQ,avg(pd.UnitPrice) as PU,pd.ProductId from PurchaseDetails pd  group by pd.ProductId) as t on t.ProductId=p.id
                                                left join (select sum(Quantity) as SQ,ProductId from SaleDetails group by ProductId) as s on s.ProductId=p.Id 
                                                where p. DeleteDate  is  NULL and p.SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    ProductList usr = new ProductList();
                    usr.ID = dr["Id"].ToString();
                    usr.Barcode = dr["Barcode"].ToString();
                    usr.Sn = dr["Sn"].ToString();
                    usr.productname = dr["productname"].ToString();
                    usr.model = dr["model"].ToString();
                    usr.unitprice = dr["unitprice"].ToString();
                    usr.unitinstoke = dr["UnitsInStock"].ToString();
                    usr.unitonorder = dr["UnitsOnOrder"].ToString();
                    usr.PP = dr["PU"].ToString();
                    usr.QI = dr["PQ"].ToString();
                    usr.QO = dr["SQ"].ToString();
                    usr.Stock = dr["Stock"].ToString();
                    usr.reorderlevel = dr["reorderlevel"].ToString();
                    usr.category = dr["CN"].ToString();
                    usr.unit = dr["UN"].ToString();
                    usr.brand = dr["BN"].ToString();
                    usr.productDetail = dr["productDetails"].ToString();
                    usr.Image = dr["Image"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/ProductList1")]
        [System.Web.Http.HttpPost]
        public List<ProductList> ProductList1(List<string> aa)
        {
            List<ProductList> list = new List<ProductList>();
            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"Select * from products where DeleteDate  is  NULL and Id=" + aa[0] + " ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductList usr = new ProductList();
                        usr.ID = dr["Id"].ToString();
                        usr.Barcode = dr["Barcode"].ToString();
                        usr.Sn = dr["Sn"].ToString();
                        usr.productname = dr["productname"].ToString();
                        usr.model = dr["model"].ToString();
                        usr.unitprice = dr["unitprice"].ToString();
                        usr.unitinstoke = dr["UnitsInStock"].ToString();
                        usr.unitonorder = dr["UnitsOnOrder"].ToString();
                        usr.reorderlevel = dr["reorderlevel"].ToString();
                        usr.category = dr["categoryid"].ToString();
                        usr.unit = dr["unitid"].ToString();
                        usr.brand = dr["brandid"].ToString();
                        usr.productDetail = dr["productDetails"].ToString();
                        usr.Image = dr["Image"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/GetExpensesList")]
        [System.Web.Http.HttpPost]
        public List<ServiceList> GetExpensesList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<ServiceList> list = new List<ServiceList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"SELECT Ex.Id, ExpenseTypes.Type,ExpenseTypes.ID as ss,CONVERT(varchar, Date, 103)as Date,a.HeadName as BankName,a.Id as Banks,Ex.Amount
                                                    FROM Expenses Ex
                                                    INNER JOIN ExpenseTypes ON Ex.ExpenseTypeId=ExpenseTypes.Id
                                                    INNER JOIN Accounts a ON Ex.PaymentAccount=a.Id where Ex.Deletedate is NULL 
													 and a.id in (Select a.Id from Accounts a where a.PHeadName in ('Cash & Cash Equivalent') and a.HeadName!='Cash At Bank' and a.SchoolID='" + SchoolID + "' union select a.id from  accounts a right join Banks b on b.Id = a.BankId and b.SchoolID=a.SchoolID where a.schoolid='" + SchoolID + "' and b.DeleteUserId is null )");
                foreach (DataRow dr in dt.Rows)
                {
                    ServiceList usr = new ServiceList();
                    usr.ID = dr["Id"].ToString();
                    usr.ServiceName = dr["Type"].ToString();
                    usr.Charge = dr["Date"].ToString();
                    usr.Desc = dr["BankName"].ToString();
                    usr.Tax = dr["Amount"].ToString();
                    usr.Tax1 = dr["ss"].ToString();
                    usr.BankID = dr["Banks"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/GetTodaySale")]
        [System.Web.Http.HttpPost]
        public List<SaleList> GetTodaySale(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<SaleList> list = new List<SaleList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select sales.ID,Customers.Name,CONVERT(varchar, Date, 101)as Date,Sales.NetTotal from sales
                                                    INNER JOIN Customers ON sales.CustomerID=Customers.Id
                                                     WHERE  (Date = { fn CURDATE() }) and sales.SchoolID='" + SchoolID + "' ");
                foreach (DataRow dr in dt.Rows)
                {
                    SaleList usr = new SaleList();
                    var dd = "INV-000";
                    usr.Id = dr["Id"].ToString();
                    usr.bill = dd + usr.Id;
                    usr.customer = dr["Name"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    list.Add(usr);

                }
            }

            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/GetTodayPurchases")]
        [System.Web.Http.HttpPost]
        public List<PurchaseList> GetTodayPurchases(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<PurchaseList> list = new List<PurchaseList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select Purchases.ID,Suppliers.CompanyName,CONVERT(varchar, Date, 101)as Date,Purchases.NetTotal from Purchases
                                                   INNER JOIN Suppliers ON Purchases.SupplierId=Suppliers.Id
                                                    WHERE  (Date = { fn CURDATE() }) and Purchases.SchoolID='" + SchoolID + "' ");
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseList usr = new PurchaseList();
                    var dd = "BILL-000";
                    usr.Id = dr["Id"].ToString();
                    usr.Invoice = dd + usr.Id;
                    usr.Supplier = dr["CompanyName"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    list.Add(usr);

                }
            }

            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/GetTodayClosing")]
        [System.Web.Http.HttpPost]
        public List<PurchaseList> GetTodayClosing(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);

            List<PurchaseList> list = new List<PurchaseList>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select CONVERT(varchar, InsertDate, 101)as Date,LastDayClosing,CashIn,CashOut,Amount from DailyClosing where SchoolId='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseList usr = new PurchaseList();
                    usr.Supplier = dr["LastDayClosing"].ToString();
                    usr.Date = dr["Date"].ToString();
                    usr.netTotal = dr["CashIn"].ToString();
                    usr.GrandTotal = dr["CashOut"].ToString();
                    usr.Shiping = dr["Amount"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/GetStockReport")]
        [System.Web.Http.HttpPost]
        public List<StockRpt> GetStockReport(List<string> aa)
        {

            int SchoolID = Convert.ToInt32(aa[2]);

            List<StockRpt> list = new List<StockRpt>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select distinct PurchaseDetails.ProductId, Products.ProductName, Products.UnitPrice As SalePrice, Categories.Name ,                                               
                                                   isnull(SUM(PurchaseDetails.Quantity),0) as QuantityIN,isnull(Avg(PurchaseDetails.unitprice),0)as PurchasePrice,isnull(SUM(SaleDetails.Quantity),0) as QuantityOut from Products
                                                  INNER JOIN Categories ON Products.CategoryId=Categories.Id
                                                  left JOIN PurchaseDetails ON Products.Id=PurchaseDetails.ProductId 
                                                  left JOIN SaleDetails ON Products.Id=SaleDetails.ProductId
                                                  Where Products.Schoolid='" + SchoolID + "' GROUP BY PurchaseDetails.ProductName,PurchaseDetails.ProductId, Products.ProductName, Categories.Name,Products.UnitPrice");


                foreach (DataRow dr in dt.Rows)

                {
                    StockRpt usr = new StockRpt();
                    usr.ProductId = dr["ProductId"].ToString();
                    usr.ProductName = dr["ProductName"].ToString();
                    usr.Category = dr["Name"].ToString();
                    usr.purchase = dr["PurchasePrice"].ToString();
                    usr.sale = dr["SalePrice"].ToString();
                    usr.Qtyin = dr["QuantityIN"].ToString();
                    usr.QtyOut = dr["QuantityOut"].ToString();
                    //usr.stock = usr.QtyOut + usr.Qtyin;
                    usr.stock = (Convert.ToInt64(usr.Qtyin) - Convert.ToInt64(usr.QtyOut)).ToString();
                    list.Add(usr);

                }
            }

            //result.DuesAmount = (Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount)).ToString();
            return list;
        }


        [System.Web.Http.Route("api/AccountsAPI/GetStockReportSales")]
        [System.Web.Http.HttpPost]
        public List<StockRpt> GetStockReportSales(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);
            List<StockRpt> list = new List<StockRpt>();
            sqlHelper obj = new sqlHelper();
            {
                DataTable dt = new DataTable();
                if (aa[2] == "dd-mm-yyyy" || aa[2] == "")
                {
                    dt = obj.getDataTable(@"select s.id,c.Name,CONVERT(varchar, s.Date, 107) as date,sd.ProductName,sd.Quantity,sd.UnitPrice,sd.Total,sd.Discount,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from sales s left join Accounts ac on ac.id=s.PaymentAccount left join Customers c on c.id=s.CustomerId right join SaleDetails sd on sd.SaleId=s.id where s.SchoolID='" + SchoolID + "' ");
                }
                else
                {
                    dt = obj.getDataTable(@"select s.id,c.Name,CONVERT(varchar, s.Date, 107) as date,sd.ProductName,sd.Quantity,sd.UnitPrice,sd.Total,sd.Discount,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from sales s left join Accounts ac on ac.id=s.PaymentAccount left join Customers c on c.id=s.CustomerId right join SaleDetails sd on sd.SaleId=s.id
                                                  where s.date between convert(datetime,'" + aa[2] + "',103) and convert(datetime,'" + aa[3] + "',103) and s.SchoolID='" + SchoolID + "'");

                }

                foreach (DataRow dr in dt.Rows)
                {
                    StockRpt usr = new StockRpt();
                    usr.stock = dr["id"].ToString();
                    usr.ProductId = "INV-000" + dr["id"].ToString();
                    usr.SaleDate = dr["date"].ToString();
                    usr.CustomerName = dr["Name"].ToString();
                    //usr.ProductId = dr["ProductId"].ToString();
                    usr.ProductName = dr["ProductName"].ToString();
                    usr.Rate = dr["UnitPrice"].ToString();
                    usr.Discount = dr["Discount"].ToString();
                    usr.Total = dr["Total"].ToString();
                    usr.Qtyin = dr["Quantity"].ToString();

                    list.Add(usr);

                }
            }

            //result.DuesAmount = (Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount)).ToString();
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/GetStockReport1")]
        [System.Web.Http.HttpPost]

        public List<SaleListReport> GetStockReport1(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);
            List<SaleListReport> list = new List<SaleListReport>();
            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = new DataTable();
                if (aa[2] == "dd-mm-yyyy" || aa[2] == "")
                {
                    dt = obj.getDataTable(@"select s.id,c.Name,CONVERT(varchar, s.Date, 107) as date,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from sales s left join Accounts ac on ac.id=s.PaymentAccount left join Customers c on c.id=s.CustomerId where s.SchoolID='" + SchoolID + "' ");

                }
                else
                {

                    dt = obj.getDataTable(@"select  s.id,c.Name,CONVERT(varchar, s.Date, 107)as Date,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from sales s left join Accounts ac on ac.id=s.PaymentAccount left join Customers c on c.id=s.CustomerId
                                          where s.date between convert(datetime,'" + aa[2] + "',103) and convert(datetime,'" + aa[3] + "',103) and s.SchoolID='" + SchoolID + "' ");



                }

                foreach (DataRow dr in dt.Rows)

                {
                    SaleListReport usr = new SaleListReport();
                    usr.stock = dr["id"].ToString();
                    usr.ProductId = "INV-000" + dr["id"].ToString();
                    usr.SaleDate = dr["Date"].ToString();
                    usr.CustomerName = dr["Name"].ToString();
                    usr.BankName = dr["Bank"].ToString();
                    usr.Total = dr["PaidAmount"].ToString();
                    usr.subModList = new List<SaleDetails>();
                    var saleid = Convert.ToInt64(usr.stock);
                    var subMods = db.SaleDetails.Where(x => x.SaleId == saleid).ToList();
                    foreach (var s in subMods)
                    {
                        SaleDetails usr1 = new SaleDetails();
                        usr1.ProductName = s.ProductName;
                        usr1.UnitPrice = s.UnitPrice;
                        usr1.Quantity = s.Quantity;
                        usr1.Discount = s.Discount;
                        usr1.Total = s.Total;

                        usr.subModList.Add(usr1);
                    }
                    list.Add(usr);

                }


            }


            //result.DuesAmount = (Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount)).ToString();
            // ViewBag.id = list;
            return list;

        }



        [System.Web.Http.Route("api/AccountsAPI/AccountsChartList")]
        [System.Web.Http.HttpPost]

        public List<SaleListReport> AccountsChartList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);
            List<SaleListReport> list = new List<SaleListReport>();
            sqlHelper obj = new sqlHelper();
            {
                var s = "COA";
                var a = db.Accounts.Where(x => x.PHeadName == s && x.SchoolID == SchoolID).ToList();

                foreach (var quo in a)
                {
                    SaleListReport usr = new SaleListReport();
                    usr.ProductId = quo.HeadCode.ToString();
                    usr.CustomerName = quo.HeadName;
                    double DebitO = 0; double CreditO = 0;
                    double DebitB = 0; double CreditB = 0;

                    var Idlist = new List<long>();
                    //Accounts Idusr = new Accounts();
                    //Idusr.Id = quo.Id;
                    Idlist.Add(quo.Id);




                    // Accounts Idusr1 = new Accounts();

                    var i = 1;
                    while (i > 0)
                    {
                        var Idlist1 = new List<long>();
                        i = 0;
                        foreach (var IDS1 in Idlist)
                        {
                            var a11 = db.Accounts.Where(x => x.ParentHead == IDS1 && x.SchoolID == SchoolID).ToList();

                            foreach (var IDS in a11)
                            {

                                //Idusr1.Id = IDS.Id;
                                Idlist1.Add(IDS.Id);

                                i = 1;

                            }
                            var OB = db.Transactions.Where(x => x.COAId == IDS1 && x.Vtype == "Opening" && x.SchoolID == SchoolID).ToList();
                            foreach (var OB1 in OB)
                            {
                                DebitO = OB1.Debit + DebitO;
                                CreditO = OB1.Credit + CreditO;

                            }
                            var B = db.Transactions.Where(x => x.COAId == IDS1 && x.SchoolID == SchoolID).ToList();
                            foreach (var B1 in B)
                            {
                                DebitB = B1.Debit + DebitB;
                                CreditB = B1.Credit + CreditB;

                            }


                        }

                        Idlist = null;
                        Idlist = Idlist1;
                        Idlist1 = null;
                    }
                    usr.OPB = DebitO - CreditO;
                    usr.Balance = DebitB - CreditB;
                    usr.RList = new List<SaleListReport>();

                    var a1 = db.Accounts.Where(x => x.ParentHead == quo.Id && x.SchoolID == SchoolID).ToList();

                    foreach (var quo1 in a1)
                    {
                        SaleListReport usr1 = new SaleListReport();
                        usr1.ProductId = quo1.HeadCode.ToString();
                        usr1.CustomerName = quo1.HeadName;
                        double DebitO1 = 0; double CreditO1 = 0;
                        double DebitB1 = 0; double CreditB1 = 0;

                        var Idlist2 = new List<long>();
                        //Accounts Idusr = new Accounts();
                        //Idusr.Id = quo.Id;
                        Idlist2.Add(quo1.Id);




                        // Accounts Idusr1 = new Accounts();

                        var i1 = 1;
                        while (i1 > 0)
                        {
                            var Idlist11 = new List<long>();
                            i1 = 0;
                            foreach (var IDS1 in Idlist2)
                            {
                                var a11 = db.Accounts.Where(x => x.ParentHead == IDS1 && x.SchoolID == SchoolID).ToList();

                                foreach (var IDS in a11)
                                {

                                    //Idusr1.Id = IDS.Id;
                                    Idlist11.Add(IDS.Id);

                                    i1 = 1;

                                }
                                var OB = db.Transactions.Where(x => x.COAId == IDS1 && x.Vtype == "Opening" && x.SchoolID == SchoolID).ToList();
                                foreach (var OB1 in OB)
                                {
                                    DebitO1 = OB1.Debit + DebitO1;
                                    CreditO1 = OB1.Credit + CreditO1;

                                }
                                var B = db.Transactions.Where(x => x.COAId == IDS1 && x.SchoolID == SchoolID).ToList();
                                foreach (var B1 in B)
                                {
                                    DebitB1 = B1.Debit + DebitB1;
                                    CreditB1 = B1.Credit + CreditB1;

                                }


                            }

                            Idlist2 = null;
                            Idlist2 = Idlist11;
                            Idlist11 = null;
                        }
                        usr1.OPB = DebitO1 - CreditO1;
                        usr1.Balance = DebitB1 - CreditB1;
                        usr1.RList1 = new List<SaleListReport>();





                        var aa1 = db.Accounts.Where(x => x.ParentHead == quo1.Id && x.SchoolID == SchoolID).ToList();

                        foreach (var quo2 in aa1)
                        {
                            SaleListReport usr3 = new SaleListReport();
                            usr3.ProductId = quo2.HeadCode.ToString();
                            usr3.CustomerName = quo2.HeadName;
                            double DebitO2 = 0; double CreditO2 = 0;
                            double DebitB2 = 0; double CreditB2 = 0;

                            var Idlist3 = new List<long>();
                            //Accounts Idusr = new Accounts();
                            //Idusr.Id = quo.Id;
                            Idlist3.Add(quo2.Id);




                            // Accounts Idusr1 = new Accounts();

                            var i2 = 1;
                            while (i2 > 0)
                            {
                                var Idlist11 = new List<long>();
                                i2 = 0;
                                foreach (var IDS1 in Idlist3)
                                {
                                    var a11 = db.Accounts.Where(x => x.ParentHead == IDS1 && x.SchoolID == SchoolID).ToList();

                                    foreach (var IDS in a11)
                                    {

                                        //Idusr1.Id = IDS.Id;
                                        Idlist11.Add(IDS.Id);

                                        i2 = 1;

                                    }
                                    var OB = db.Transactions.Where(x => x.COAId == IDS1 && x.Vtype == "Opening" && x.SchoolID == SchoolID).ToList();
                                    foreach (var OB1 in OB)
                                    {
                                        DebitO2 = OB1.Debit + DebitO2;
                                        CreditO2 = OB1.Credit + CreditO2;

                                    }
                                    var B = db.Transactions.Where(x => x.COAId == IDS1 && x.SchoolID == SchoolID).ToList();
                                    foreach (var B1 in B)
                                    {
                                        DebitB2 = B1.Debit + DebitB2;
                                        CreditB2 = B1.Credit + CreditB2;

                                    }


                                }

                                Idlist3 = null;
                                Idlist3 = Idlist11;
                                Idlist11 = null;
                            }
                            usr3.OPB = DebitO2 - CreditO2;
                            usr3.Balance = DebitB2 - CreditB2;

                            usr3.RList1 = new List<SaleListReport>();
                            var aa2 = db.Accounts.Where(x => x.ParentHead == quo2.Id && x.SchoolID == SchoolID).ToList();

                            foreach (var quo3 in aa2)
                            {
                                SaleListReport usr4 = new SaleListReport();
                                usr4.ProductId = quo3.HeadCode.ToString();
                                usr4.CustomerName = quo3.HeadName;
                                DebitO2 = 0; CreditO2 = 0;
                                DebitB2 = 0; CreditB2 = 0;

                                var Idlist4 = new List<long>();
                                Idlist4.Add(quo3.Id);
                                i2 = 1;
                                while (i2 > 0)
                                {
                                    var Idlist11 = new List<long>();
                                    i2 = 0;
                                    foreach (var IDS1 in Idlist4)
                                    {
                                        var a11 = db.Accounts.Where(x => x.ParentHead == IDS1 && x.SchoolID == SchoolID).ToList();

                                        foreach (var IDS in a11)
                                        {

                                            Idlist11.Add(IDS.Id);
                                            i2 = 1;

                                        }
                                        var OB = db.Transactions.Where(x => x.COAId == IDS1 && x.Vtype == "Opening" && x.SchoolID == SchoolID).ToList();
                                        foreach (var OB1 in OB)
                                        {
                                            DebitO2 = OB1.Debit + DebitO2;
                                            CreditO2 = OB1.Credit + CreditO2;

                                        }
                                        var B = db.Transactions.Where(x => x.COAId == IDS1 && x.SchoolID == SchoolID).ToList();
                                        foreach (var B1 in B)
                                        {
                                            DebitB2 = B1.Debit + DebitB2;
                                            CreditB2 = B1.Credit + CreditB2;

                                        }


                                    }

                                    Idlist4 = null;
                                    Idlist4 = Idlist11;
                                    Idlist11 = null;
                                }
                                usr4.OPB = DebitO2 - CreditO2;
                                usr4.Balance = DebitB2 - CreditB2;

                                usr4.RList1 = new List<SaleListReport>();
                                var aa3 = db.Accounts.Where(x => x.ParentHead == quo3.Id && x.SchoolID == SchoolID).ToList();

                                foreach (var quo4 in aa3)
                                {
                                    SaleListReport usr5 = new SaleListReport();
                                    usr5.ProductId = quo4.HeadCode.ToString();
                                    usr5.CustomerName = quo4.HeadName;
                                    DebitO2 = 0; CreditO2 = 0;
                                    DebitB2 = 0; CreditB2 = 0;

                                    var Idlist5 = new List<long>();
                                    Idlist5.Add(quo4.Id);
                                    i2 = 1;
                                    while (i2 > 0)
                                    {
                                        var Idlist11 = new List<long>();
                                        i2 = 0;
                                        foreach (var IDS1 in Idlist5)
                                        {
                                            var a11 = db.Accounts.Where(x => x.ParentHead == IDS1 && x.SchoolID == SchoolID).ToList();

                                            foreach (var IDS in a11)
                                            {

                                                Idlist11.Add(IDS.Id);
                                                i2 = 1;

                                            }
                                            var OB = db.Transactions.Where(x => x.COAId == IDS1 && x.Vtype == "Opening" && x.SchoolID == SchoolID).ToList();
                                            foreach (var OB1 in OB)
                                            {
                                                DebitO2 = OB1.Debit + DebitO2;
                                                CreditO2 = OB1.Credit + CreditO2;

                                            }
                                            var B = db.Transactions.Where(x => x.COAId == IDS1 && x.SchoolID == SchoolID).ToList();
                                            foreach (var B1 in B)
                                            {
                                                DebitB2 = B1.Debit + DebitB2;
                                                CreditB2 = B1.Credit + CreditB2;

                                            }


                                        }

                                        Idlist5 = null;
                                        Idlist5 = Idlist11;
                                        Idlist11 = null;
                                    }
                                    usr5.OPB = DebitO2 - CreditO2;
                                    usr5.Balance = DebitB2 - CreditB2;


                                    usr4.RList1.Add(usr5);



                                }

                                usr3.RList1.Add(usr4);



                            }


                            usr1.RList1.Add(usr3);



                        }
                        usr.RList.Add(usr1);







                    }



                    list.Add(usr);



                }


            }


            return list;

        }







        [System.Web.Http.Route("api/AccountsAPI/GetStockReport2")]
        [System.Web.Http.HttpPost]

        public List<SaleListReport> GetStockReport2(List<string> aa)
        {

            int SchoolID = Convert.ToInt32(aa[4]);
            List<SaleListReport> list = new List<SaleListReport>();

            sqlHelper obj = new sqlHelper();
            {
                DataTable dt = new DataTable();
                if (aa[2] == "dd-mm-yyyy" || aa[2] == "")
                {
                    dt = obj.getDataTable(@"select s.id,c.CompanyName as Name,CONVERT(varchar, s.Date, 107) as date,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from Purchases s left join Accounts ac on ac.id=s.PaymentAccount left join Suppliers c on c.id=s.SupplierId where s.SchoolID='" + SchoolID + "'");

                }
                else
                {

                    dt = obj.getDataTable(@"select s.id,c.CompanyName as Name,CONVERT(varchar, s.Date, 107) as date,s.PaidAmount,case when s.PaidAmount > 0  then ac.HeadName else '' end as Bank from Purchases s left join Accounts ac on ac.id=s.PaymentAccount left join Suppliers c on c.id=s.SupplierId
                                          where s.date between convert(datetime,'" + aa[2] + "',103) and convert(datetime,'" + aa[3] + "',103) and s.SchoolID='" + SchoolID + "' ");



                }

                foreach (DataRow dr in dt.Rows)

                {
                    SaleListReport usr = new SaleListReport();
                    usr.stock = dr["id"].ToString();
                    usr.ProductId = "BIL-000" + dr["id"].ToString();
                    usr.SaleDate = dr["Date"].ToString();
                    usr.CustomerName = dr["Name"].ToString();
                    usr.BankName = dr["Bank"].ToString();
                    usr.Total = dr["PaidAmount"].ToString();
                    usr.subModList = new List<SaleDetails>();
                    var saleid = Convert.ToInt64(usr.stock);
                    var subMods = db.PurchaseDetails.Where(x => x.PurchaseId == saleid).ToList();
                    foreach (var s in subMods)
                    {
                        SaleDetails usr1 = new SaleDetails();
                        usr1.ProductName = s.ProductName;
                        usr1.UnitPrice = s.UnitPrice;
                        usr1.Quantity = s.Quantity;
                        usr1.Discount = s.Discount;
                        usr1.Total = s.Total;

                        usr.subModList.Add(usr1);
                    }
                    list.Add(usr);

                }


            }


            //result.DuesAmount = (Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount)).ToString();
            // ViewBag.id = list;
            return list;

        }




        [System.Web.Http.Route("api/AccountsAPI/CustomerReceivable")]
        [System.Web.Http.HttpPost]
        public List<CustomerReceivable> CustomerReceivable(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);
            List<CustomerReceivable> list = new List<CustomerReceivable>();
            sqlHelper obj = new sqlHelper();
            {
                // DataTable dt = new DataTable();


                DataTable dt = obj.getDataTable(@"with tt as
                                            (select t.COAId,sum(t.[Debit]) as d,sum(t.Credit) c,sum(t.[Debit])-sum(t.Credit) as b from [dbo].[Transactions] t where t.[COAId] in
                                            (SELECT  a.Id FROM Accounts a where a.CustomerId in (select id from Customers ))  group by t.COAId 
                                             )
                                             select c.Name,tt.d,tt.c,tt.b from tt left join Accounts a on a.id=tt.COAId left join Customers c on c.id=a.CustomerId
                                             where c.SchoolID='" + SchoolID + "'");




                foreach (DataRow dr in dt.Rows)

                {
                    CustomerReceivable usr = new CustomerReceivable();

                    usr.Name = dr["Name"].ToString();
                    usr.Receivable = dr["d"].ToString();
                    usr.Received = dr["c"].ToString();
                    usr.Balance = dr["b"].ToString();
                    list.Add(usr);

                }
            }

            return list;

        }

        [System.Web.Http.Route("api/AccountsAPI/SupplierPayable")]
        [System.Web.Http.HttpPost]
        public List<CustomerReceivable> SupplierPayable(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[2]);
            List<CustomerReceivable> list = new List<CustomerReceivable>();
            sqlHelper obj = new sqlHelper();
            {
                // DataTable dt = new DataTable();


                DataTable dt = obj.getDataTable(@"with tt as
                                                (select t.COAId,sum(t.[Debit]) as d,sum(t.Credit) c,sum(t.Credit)-sum(t.[Debit]) as b from [dbo].[Transactions] t where t.[COAId] in
                                                (SELECT  a.Id FROM Accounts a where a.SupplierId in (select id from Suppliers))  group by t.COAId 
                                                )
                                                select c.CompanyName as Name,tt.d,tt.c,tt.b from tt left join Accounts a on a.id=tt.COAId left join Suppliers c on c.id=a.SupplierId 
                                                where c.SchoolID='" + SchoolID + "' ");




                foreach (DataRow dr in dt.Rows)

                {
                    CustomerReceivable usr = new CustomerReceivable();

                    usr.Name = dr["Name"].ToString();
                    usr.Receivable = dr["c"].ToString();
                    usr.Received = dr["d"].ToString();
                    usr.Balance = dr["b"].ToString();
                    list.Add(usr);

                }
            }

            return list;

        }


        //[System.Web.Http.Route("api/AccountsAPI/GetStockReport1")]
        //[System.Web.Http.HttpPost]
        //public List<StockRpt> GetStockReport1(List<string> aa)
        //{
        //    List<StockRpt> list = new List<StockRpt>();
        //    sqlHelper obj = new sqlHelper();
        //    {

        //        DataTable dt = obj.getDataTable(@" SELECT   SUM(SaleDetails.Quantity)as QuantityOut
        //                                              FROM SaleDetails
        //                                                GROUP BY SaleDetails.ProductName");


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            StockRpt usr = new StockRpt();
        //            usr.QtyOut = dr["QuantityOut"].ToString();
        //            list.Add(usr);

        //        }
        //    }

        //    return list;
        //}

        [System.Web.Http.Route("api/AccountsAPI/getAllCategory")]
        [System.Web.Http.HttpPost]
        public List<ExpCategory> getAllCategory(List<string> aa)
        {
            List<ExpCategory> list = new List<ExpCategory>();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);


            if (typeuser == 2)
            {
                var result = (from a in db.tblExpenseCategories
                              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                              where a.IsDeleted == null
                              select new
                              {
                                  model = a,
                                  SchoolName = s.School
                              }

                          ).ToList();
                //  var result = db.tblExpenseCategories.ToList();
                foreach (var a in result)
                {
                    ExpCategory usr = new ExpCategory();
                    usr.Name = a.model.CategoryName;
                    usr.Id = a.model.Id.ToString();
                    usr.Status = a.model.Status.ToString();
                    if (a.model.Status.ToString() == "True")
                    {
                        usr.Status = "Activate";
                        usr.Extra10 = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";
                    }
                    usr.School = a.SchoolName;
                    usr.SchoolID = Convert.ToInt32(a.model.SchoolID);
                    list.Add(usr);
                }
            }
            else
            {
                var result = (from a in db.tblExpenseCategories
                              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                              join em in db.tblEmployees on a.SchoolID equals em.SchoolID
                              where em.UserID == loginuser && em.IsDeleted == null && a.IsDeleted == null
                              select new
                              {
                                  model = a,
                                  SchoolName = s.School
                              }

                          ).ToList();
                //  var result = db.tblExpenseCategories.ToList();
                foreach (var a in result)
                {
                    ExpCategory usr = new ExpCategory();
                    usr.Name = a.model.CategoryName;
                    usr.Id = a.model.Id.ToString();
                    usr.Status = a.model.Status.ToString();
                    if (a.model.Status.ToString() == "True")
                    {
                        usr.Status = "Activate";
                        usr.Extra10 = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "btn btn-block btn-danger btn-sm";
                    }
                    usr.School = a.SchoolName;
                    usr.SchoolID = Convert.ToInt32(a.model.SchoolID);
                    list.Add(usr);
                }
            }
            return list;

        }

        [System.Web.Http.Route("api/AccountsAPI/PurchaseList")]
        [System.Web.Http.HttpPost]
        public List<PurchaseList> PurchaseList(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[4]);

            List<PurchaseList> list = new List<PurchaseList>();

            sqlHelper obj = new sqlHelper();
            {

                DataTable dt = obj.getDataTable(@"select Ps.Id, suppliers.CompanyName,suppliers.Address,suppliers.Email,suppliers.Phone,Ps.InvoiceNo,CONVERT(varchar, Date, 101)as Date,CONVERT(varchar, Date+10, 101)as DueDate,ps.PaymentAccount,
                                                   ps.Discount,Ps.TotalDiscount,Ps.ShippingCost,Ps.GrandTotal,Ps.NetTotal,
                                                   Ps.PaidAmount,Ps.Due,ps.Change,Ps.Details from purchases Ps
                                                   INNER JOIN Suppliers ON Ps.SupplierId=Suppliers.Id where Ps.SchoolID='" + SchoolID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseList usr = new PurchaseList();
                    usr.Id = dr["Id"].ToString();
                    usr.Supplier = dr["CompanyName"].ToString();
                    usr.Address = dr["Address"].ToString();
                    usr.email = dr["Email"].ToString();
                    usr.phone = dr["Phone"].ToString();
                    usr.Invoice = dr["InvoiceNo"].ToString();
                    usr.Date = (dr["Date"].ToString());
                    usr.dds = dr["DueDate"].ToString();
                    usr.Payment = dr["PaymentAccount"].ToString();
                    usr.Discount = dr["Discount"].ToString();
                    usr.TotalDiscount = dr["TotalDiscount"].ToString();
                    usr.Shiping = dr["ShippingCost"].ToString();
                    usr.GrandTotal = dr["GrandTotal"].ToString();
                    usr.netTotal = dr["NetTotal"].ToString();
                    usr.Paid = dr["PaidAmount"].ToString();
                    usr.Due = dr["Due"].ToString();
                    usr.Change = dr["Change"].ToString();
                    usr.Detail = dr["Details"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }




        [System.Web.Http.Route("api/AccountsAPI/PurchaseList1")]
        [System.Web.Http.HttpPost]
        public List<PurchaseList> PurchaseList1(List<string> aa)
        {

            List<PurchaseList> list = new List<PurchaseList>();

            sqlHelper obj = new sqlHelper();
            {
                if (aa[0] != null)
                {
                    DataTable dt = obj.getDataTable(@"select Ps.Id, suppliers.CompanyName,suppliers.Id as SID,suppliers.Address,suppliers.Email,suppliers.Phone,Ps.InvoiceNo,CONVERT(varchar, Ps.Date, 103)as Date,CONVERT(varchar, Ps.Date+10, 101)as DueDate,ps.PaymentAccount,
                                                   ps.Discount,Ps.TotalDiscount,Ps.ShippingCost,Ps.GrandTotal,Ps.NetTotal,
                                                   Ps.PaidAmount,Ps.Due,ps.Change,Ps.Details from purchases Ps
                                                   INNER JOIN Suppliers ON Ps.SupplierId=Suppliers.Id where Ps.Id='" + aa[0] + "'");
                    foreach (DataRow dr in dt.Rows)
                    {
                        PurchaseList usr = new PurchaseList();
                        usr.Id = dr["Id"].ToString();
                        usr.Supplier = dr["SID"].ToString();
                        usr.Address = dr["Address"].ToString();
                        usr.email = dr["Email"].ToString();
                        usr.phone = dr["Phone"].ToString();
                        usr.Invoice = dr["InvoiceNo"].ToString();
                        usr.Date = (dr["Date"].ToString());
                        usr.dds = dr["DueDate"].ToString();
                        usr.Payment = dr["PaymentAccount"].ToString();
                        usr.Discount = dr["Discount"].ToString();
                        usr.TotalDiscount = dr["TotalDiscount"].ToString();
                        usr.Shiping = dr["ShippingCost"].ToString();
                        usr.GrandTotal = dr["GrandTotal"].ToString();
                        usr.netTotal = dr["NetTotal"].ToString();
                        usr.Paid = dr["PaidAmount"].ToString();
                        usr.Due = dr["Due"].ToString();
                        usr.Change = dr["Change"].ToString();
                        usr.Detail = dr["Details"].ToString();
                        list.Add(usr);

                    }
                }
            }
            return list;
        }



        [System.Web.Http.Route("api/AccountsAPI/deleteCategoyById")]
        [System.Web.Http.HttpPost]
        public string deleteCategoyById(Int32 id)
        {
            int idd = Convert.ToInt32(id);
            var aa = db.tblExpenseCategories.SingleOrDefault(s => s.Id == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            //var result = new tblExpenseCategory { Id = id };
            //db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return "Category Deleted Successfully";

        }


        [System.Web.Http.Route("api/AccountAPI/DeleteRecord")]
        [System.Web.Http.HttpPost]
        public int DeleteRecord(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            string userid = val[1].ToString().Trim();
            string type = val[2].ToString().Trim();
            try
            {
                if (type == "Suppliers")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Suppliers.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }
                if (type == "Expenses")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Expenses.SingleOrDefault(s => s.Id == idd);
                    aa.Deleteuserid = User;
                    aa.Deletedate = DateTime.Now;
                    db.SaveChanges();

                }

                if (type == "Damage")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.DamagedProducts.SingleOrDefault(s => s.Id == idd);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();

                }

                if (type == "GoodReceipt")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa1 = db.GoodsReceiptDetails.SingleOrDefault(s => s.GoodsReceiptId == idd);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                    }
                    var aa = db.GoodsReceipt.SingleOrDefault(s => s.Id == idd);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();


                }


                if (type == "Purchase")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa1 = db.PurchaseDetails.SingleOrDefault(s => s.PurchaseId == idd);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                    }
                    var aa = db.Purchases.SingleOrDefault(s => s.Id == idd);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();


                }
                if (type == "Sale")
                {
                    var aa1 = db.SaleDetails.SingleOrDefault(s => s.SaleId == id);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                    }
                    var aa = db.Sales.SingleOrDefault(s => s.Id == id);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();
                }
                if (type == "Quotation")
                {
                    var aa1 = db.QuotationDetails.SingleOrDefault(s => s.QuotationId == id);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                    }
                    var aa = db.Quotations.SingleOrDefault(s => s.Id == id);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();
                }
                if (type == "Invoice")
                {
                    var aa1 = db.ServiceInvoiceDetails.SingleOrDefault(s => s.ServiceInvoiceId == id);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();
                    }
                    var aa = db.ServiceInvoices.SingleOrDefault(s => s.Id == id);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();
                }



                if (type == "GoodIssue")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa1 = db.GoodsIssueDetails.SingleOrDefault(s => s.GoodsIssueId == idd);
                    if (aa1 != null)
                    {
                        db.Entry(aa1).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                    var aa = db.GoodsIssue.SingleOrDefault(s => s.Id == idd);
                    db.Entry(aa).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();



                }


                if (type == "Pruducts")
                {

                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Products.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }

                else if (type == "Customers")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Customers.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }


                else if (type == "Units")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Units.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }
                else if (type == "Brands")
                {
                    int idd = Convert.ToInt32(id);
                    string User = val[1].ToString().Trim();
                    var aa = db.Brands.SingleOrDefault(s => s.Id == idd);
                    aa.DeleteUserId = User;
                    aa.DeleteDate = DateTime.Now;
                    db.SaveChanges();

                }


                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }


        [System.Web.Http.Route("api/AccountsAPI/getAllActiveCategoryBySchool")]
        [System.Web.Http.HttpPost]
        public List<ExpCategory> getAllActiveCategoryBySchool(List<string> id)
        {
            int SchoolID = Convert.ToInt32(id[0]);
            List<ExpCategory> list = new List<ExpCategory>();
            var result = db.tblExpenseCategories.Where(s => s.Status == true && s.SchoolID == SchoolID && s.IsDeleted == null).ToList();
            foreach (var a in result)
            {
                ExpCategory usr = new ExpCategory();
                usr.Name = a.CategoryName;
                usr.Id = a.Id.ToString();
                list.Add(usr);
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/getAllActiveCategory")]
        [System.Web.Http.HttpPost]
        public List<ExpCategory> getAllActiveCategory()
        {
            List<ExpCategory> list = new List<ExpCategory>();
            var result = db.tblExpenseCategories.Where(s => s.Status == true).ToList();
            foreach (var a in result)
            {
                ExpCategory usr = new ExpCategory();
                usr.Name = a.CategoryName;
                usr.Id = a.Id.ToString();
                list.Add(usr);
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/savePayeementsDetails")]
        [System.Web.Http.HttpPost]
        public string savePayeementsDetails(PayeeDetails pd)
        {
            if (string.IsNullOrEmpty(pd.Id))
            {
                tblExPayeeDetail pay = new tblExPayeeDetail();
                pay.PayeeName = pd.Name;
                pay.CategoryId = pd.Category;
                pay.PrimaryDate = Convert.ToDateTime(pd.PrimaryDate);
                pay.LastDate = Convert.ToDateTime(pd.LastDate);
                pay.TotalAmount = pd.TotalAmount;
                pay.DuesAmount = pd.DuesAmount;
                pay.PaymentType = Convert.ToInt64(pd.PaymentType);
                pay.SchoolID = Convert.ToInt32(pd.SchoolID);
                if (pay.PaymentType == 3)
                {
                    pay.PayingAmount = pd.PayingAmount;
                }
                if (pay.PaymentType == 1 || pay.PaymentType == 2)
                {
                    pay.PayingAmount = pd.PayingAmount;

                    pay.PaymentDate = Convert.ToDateTime(pd.PaymentDate);
                    pay.PaymentMode = pd.PaymentMode;
                    if (pay.PaymentMode == "Cheque")
                    {
                        pay.ChequeNo = pd.Checqno;
                        pay.ChequeDate = Convert.ToDateTime(pd.ChequeDate);
                        pay.BankName = pd.BankRemarks;
                    }



                    pay.Remarks = pd.Remarks;
                }
                //   pay.Status = Convert.ToBoolean(pd.Status);
                db.tblExPayeeDetails.Add(pay);
                db.SaveChanges();

                int id = pay.Id;
                if (pay.PaymentType != 3)
                {

                    tblTransExPayeeDetail pay1 = new tblTransExPayeeDetail();
                    pay1.ExpDetailsId = Convert.ToInt64(id);
                    pay1.PayingAmount = pd.PayingAmount;
                    pay1.PaymentDate = Convert.ToDateTime(pd.PaymentDate);
                    pay1.PaymentMode = pd.PaymentMode;
                    if (pay1.PaymentMode == "Cheque")
                    {
                        pay1.chequeNo = pd.Checqno;
                        pay1.ChequeDate = Convert.ToDateTime(pd.ChequeDate);
                        pay1.BankName = pd.BankRemarks;
                    }

                    pay1.Remarks = pd.Remarks;
                    pay1.SchoolID = pd.SchoolID;
                    db.tblTransExPayeeDetails.Add(pay1);
                    db.SaveChanges();
                }


                return "Payment Added Successfully";

            }
            else
            {
                int id = Convert.ToInt16(pd.Id);
                var result = db.tblExPayeeDetails.SingleOrDefault(s => s.Id == id);
                result.PayeeName = pd.Name;
                result.CategoryId = pd.Category;
                result.PrimaryDate = Convert.ToDateTime(pd.PrimaryDate);
                result.LastDate = Convert.ToDateTime(pd.LastDate);
                result.TotalAmount = pd.TotalAmount;
                result.PaymentType = Convert.ToInt64(pd.PaymentType);
                result.SchoolID = Convert.ToInt32(pd.SchoolID);
                if (pd.PaymentType == "1" && pd.PaymentType == "2")
                {
                    result.PayingAmount = pd.PayingAmount;
                    result.PaymentDate = Convert.ToDateTime(pd.PaymentDate);
                    if (pd.PaymentMode == "Cheque")
                    {
                        result.PaymentMode = pd.PaymentMode;
                        result.ChequeNo = pd.Checqno;
                        result.ChequeDate = Convert.ToDateTime(pd.ChequeDate);
                        result.BankName = pd.BankRemarks;
                    }
                    result.Remarks = pd.Remarks;
                }
                //  result.Status = Convert.ToBoolean(pd.Status);
                db.SaveChanges();
                return "Payment Updated Successfully";
            }

        }

        [System.Web.Http.Route("api/AccountsAPI/getAllPaymentDetails")]
        [System.Web.Http.HttpPost]
        public List<PayeeDetails> getAllPaymentDetails(List<string> aa)
        {
            List<PayeeDetails> list = new List<PayeeDetails>();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID,ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                                           ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                                           left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
                                           where DuesAmount!='0' and ep.IsDeleted is null ");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    PayeeDetails usr = new PayeeDetails();
                    usr.Category = dr["CategoryName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["PayeeName"].ToString();

                    usr.PaymentType = dr["paymentType"].ToString();
                    usr.TotalAmount = dr["TotalAmount"].ToString();
                    usr.PayingAmount = dr["PayingAmount"].ToString();
                    usr.DuesAmount = dr["DuesAmount"].ToString();
                    usr.PrimaryDate = ((DateTime)dr["PrimaryDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID,ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                                           ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                                           left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
                                           left outer join tblEmployee em on ep.SchoolID =em.SchoolID where em.UserID= '" + loginuser + "' and em.IsDeleted is null and DuesAmount!='0' and ep.IsDeleted is null ");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    PayeeDetails usr = new PayeeDetails();
                    usr.Category = dr["CategoryName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["PayeeName"].ToString();

                    usr.PaymentType = dr["paymentType"].ToString();
                    usr.TotalAmount = dr["TotalAmount"].ToString();
                    usr.PayingAmount = dr["PayingAmount"].ToString();
                    usr.DuesAmount = dr["DuesAmount"].ToString();
                    usr.PrimaryDate = ((DateTime)dr["PrimaryDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            return list;
        }



        //public List<PayeeDetails> getAllPaymentDetails(List<string> aa)
        [System.Web.Http.Route("api/AccountsAPI/getAllPaymentDetails11")]
        [System.Web.Http.HttpGet]
        public string getAllPaymentDetails11(int StudentId,int AcademicYear)
        {
            StringBuilder tBody = new StringBuilder();
            List<udf_getsfee> list = new List<udf_getsfee>();
            int ID = StudentId;
            int Academicyear = AcademicYear;
            //int Academicyear = Convert.ToInt32(db.TBLStudents.Where(x => x.ID == ID).Select(y => y.AcademicYear).FirstOrDefault());
            sqlHelper obj = new sqlHelper();

            if (Academicyear == 2)
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID,ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                                           ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                                           left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
                                           where DuesAmount!='0' and ep.IsDeleted is null ");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    udf_getsfee usr = new udf_getsfee();
                    usr.FeeType = dr["FeeType"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.FeeType = dr["FeeType"].ToString();

                    usr.Total = dr["Total"].ToString();
                    usr.Per = dr["Per"].ToString();
                    usr.concession = dr["concession"].ToString();
                    usr.nettotal = dr["nettotal"].ToString();
                    usr.paid = dr["paid"].ToString();
                    usr.Balance = dr["Balance"].ToString();
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select * from udf_getsfee ('" + ID + "' ,'" + Academicyear + "' )");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    tBody.Append("<tr>");
                    udf_getsfee usr = new udf_getsfee();
                    //usr.FeeType = dr["FeeType"].ToString();
                    //tBody.AppendFormat("<td>{0}</td>", usr.FeeType);
                    usr.Id = dr["Id"].ToString();
                    usr.FeeCategory = dr["FeeCategory"].ToString();
                    tBody.AppendFormat("<td align='left'>{0}</td>", usr.FeeCategory);
                    usr.Total = dr["Total"].ToString();
                    tBody.AppendFormat("<td align='left'>{0}</td>", usr.Total);
                    usr.concession = dr["concession"].ToString();
                    tBody.AppendFormat("<td align='left'>{0}</td>", usr.concession);
                    usr.nettotal = dr["nettotal"].ToString();
                    tBody.AppendFormat("<td align='right'>{0}</td>", usr.nettotal);
                    //usr.paid = dr["paid"].ToString();
                    //tBody.AppendFormat("<td>{0}</td>", usr.paid);
                    //usr.Balance = dr["Balance"].ToString();
                    //tBody.AppendFormat("<td>{0}</td>", usr.Balance);

                    list.Add(usr);
                    tBody.Append("</tr>");
                }
            }
            return tBody.ToString();
        }



        //public List<PayeeDetails> getAllPaymentDetails(List<string> aa)
        [System.Web.Http.Route("api/AccountsAPI/getAllPaymentDetailsnew")]
        [System.Web.Http.HttpGet]
        public udf_getsfee getAllPaymentDetailsnew(int StudentId)
        {
            List<udf_getsfee> list = new List<udf_getsfee>();
            int ID = Convert.ToInt32(StudentId);
            int Academicyear = Convert.ToInt32(db.TBLStudents.Where(x => x.ID == ID).Select(y => y.AcademicYear).FirstOrDefault());
            sqlHelper obj = new sqlHelper();
            string total;
           
                DataTable dt = obj.getDataTable(@"select sum(nettotal) nettotal,sum(paid) paid,sum(Balance) Balance from udf_getsfeepaid('" + ID + "' ,'" + Academicyear + "') ");

            udf_getsfee usr = new udf_getsfee();
            foreach (DataRow dr in dt.Rows)
                {
              
                total= dr["nettotal"].ToString();
                usr.nettotal = dr["nettotal"].ToString();
                    usr.paid = dr["paid"].ToString();
                    usr.Balance = dr["Balance"].ToString();
                    list.Add(usr);
                }

            return usr;
        }


        //public List<PayeeDetails> getAllPaymentDetails(List<string> aa)
        [System.Web.Http.Route("api/AccountsAPI/CollectionDayWeekly")]
        [System.Web.Http.HttpGet]
        public udf_getsfee CollectionDayWeekly(int SchoolID)
        {
            //List<udf_getsfee> list = new List<udf_getsfee>();
            int ID = Convert.ToInt32(SchoolID);
           // int Academicyear = Convert.ToInt32(db.TBLStudents.Where(x => x.ID == ID).Select(y => y.AcademicYear).FirstOrDefault());
            sqlHelper obj = new sqlHelper();
            //string total;

            DataTable dt = obj.getDataTable(@"select isnull(sum(Amount),0) today  from stdfee1 where  CONVERT(char(10), Dated  ,126)=CONVERT(char(10), getdate(),126) and InstituteID='" + SchoolID+"'");

            udf_getsfee usr = new udf_getsfee();
            foreach (DataRow dr in dt.Rows)
            {

                //total = dr["nettotal"].ToString();
                usr.nettotal = dr["today"].ToString();
               // usr.paid = dr["paid"].ToString();
                //usr.Balance = dr["Balance"].ToString();
                //list.Add(usr);
            }

            DataTable dt1 = obj.getDataTable(@"select isnull(sum(Amount),0) Week from stdfee1 where  CONVERT(char(10), Dated  ,126)between Convert(char(10),DateAdd(DD,-7,GETDATE()),126) and CONVERT(char(10), getdate(),126) and InstituteID='"+SchoolID+"'");
            foreach (DataRow dr1 in dt1.Rows)
            {
                usr.paid = dr1["Week"].ToString();
            }
                return usr;
        }






        [System.Web.Http.Route("api/AccountsAPI/getAllFeedetails")]
        [System.Web.Http.HttpGet]
        public string getAllFeedetails(int StudentId)
        {
            StringBuilder tBody = new StringBuilder();
            List<udf_getsfee> list = new List<udf_getsfee>();
            int ID = StudentId;
            int Academicyear = Convert.ToInt32(db.TBLStudents.Where(x => x.ID == ID).Select(y => y.AcademicYear).FirstOrDefault());
            sqlHelper obj = new sqlHelper();

            if (Academicyear == 2)
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID,ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                                           ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                                           left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
                                           where DuesAmount!='0' and ep.IsDeleted is null ");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    udf_getsfee usr = new udf_getsfee();
                    usr.FeeType = dr["FeeType"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.FeeType = dr["FeeType"].ToString();

                    usr.Total = dr["Total"].ToString();
                    usr.Per = dr["Per"].ToString();
                    usr.concession = dr["concession"].ToString();
                    usr.nettotal = dr["nettotal"].ToString();
                    usr.paid = dr["paid"].ToString();
                    usr.Balance = dr["Balance"].ToString();
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select * from udf_getsfee ('" + ID + "' ,'" + Academicyear + "' )");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                //ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                //   left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId where DuesAmount!='0'");
                foreach (DataRow dr in dt.Rows)
                {
                    tBody.Append("<tr>");
                    udf_getsfee usr = new udf_getsfee();
                    //usr.FeeType = dr["FeeType"].ToString();
                    //tBody.AppendFormat("<td>{0}</td>", usr.FeeType);
                    usr.Id = dr["Id"].ToString();
                    usr.FeeCategory = dr["FeeCategory"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.FeeCategory);
                    usr.Total = dr["Total"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.Total);
                    usr.concession = dr["concession"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.concession);
                    usr.nettotal = dr["nettotal"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.nettotal);
                    usr.paid = dr["paid"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.paid);
                    usr.Balance = dr["Balance"].ToString();
                    tBody.AppendFormat("<td>{0}</td>", usr.Balance);

                    list.Add(usr);
                    tBody.Append("</tr>");
                }
            }
            return tBody.ToString();
        }



        [System.Web.Http.Route("api/AccountsAPI/countpaymentAllTypeBySchool")]
        [System.Web.Http.HttpPost]
        public string countpaymentAllTypeBySchool(List<string> id)
        {
            int SchoolID = Convert.ToInt32(id[0]);
            sqlHelper obj = new sqlHelper();

            string Total = obj.ExecuteScaler("select SUM(cast(Total as float)) TotalAmounts from udf_getsfee1('100')");
            string Concession = obj.ExecuteScaler("select SUM(cast(Concession as float)) Concession from udf_getsfee1('100')");
            string TotalAmount = obj.ExecuteScaler("select SUM(cast(nettotal as float)) TotalAmount from udf_getsfee1('100')");
            string PaidAmount = obj.ExecuteScaler("select SUM(cast(paid as float)) PaidAmount from udf_getsfee1('100') ");
            string DuesAmount = obj.ExecuteScaler("select SUM(cast(Balance as float)) Balance from udf_getsfee1('100') ");
            return TotalAmount + "***" + PaidAmount + "***" + DuesAmount + "***" + Total + "***" + Concession;
        }


        [System.Web.Http.Route("api/AccountsAPI/countpaymentAllType")]
        [System.Web.Http.HttpPost]
        public string countpaymentAllType()
        {
            sqlHelper obj = new sqlHelper();
            string TotalAmount = obj.ExecuteScaler("select SUM(cast(Amount as float)) TotalAmount from tblFeeStructureConfig ");
            string PaidAmount = obj.ExecuteScaler("select SUM(cast(PayingAmount as float)) TotalAmount from tblExPayeeDetails where DuesAmount!='0'");
            string DuesAmount = obj.ExecuteScaler("select SUM(cast(DuesAmount as float)) TotalAmount from tblExPayeeDetails where DuesAmount!='0'");
            return TotalAmount + "***" + PaidAmount + "***" + DuesAmount;
        }

        [System.Web.Http.Route("api/AccountsAPI/editPaymentUpdateDetails")]
        [System.Web.Http.HttpPost]
        public PayeeDetails editPaymentUpdateDetails(List<string> id)
        {

            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID, ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
                            ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
                            left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
                            where ep.Id=" + id[0]);
            //DataTable dt = obj.getDataTable(@"select ec.CategoryName,case when ep.PaymentType='1' then 'Pay Now' when
            //                ep.PaymentType = '2' then 'Pay Now' else 'Pay Later' end paymentType, ep.* from tblExPayeeDetails ep
            //                left outer join tblExpenseCategory ec on ec.Id = ep.CategoryId
            //                where ep.Id=" + id[0]);
            PayeeDetails usr = new PayeeDetails();
            foreach (DataRow dr in dt.Rows)
            {
                // PayeeDetails usr = new PayeeDetails();
                usr.Category = dr["CategoryName"].ToString();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["PayeeName"].ToString();
                usr.PaymentType = dr["paymentType"].ToString();
                usr.TotalAmount = dr["TotalAmount"].ToString();
                usr.PayingAmount = dr["PayingAmount"].ToString();
                usr.DuesAmount = dr["DuesAmount"].ToString();
                usr.PrimaryDate = ((DateTime)dr["PrimaryDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.School = dr["School"].ToString();
                usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);

            }
            return usr;
        }

        [System.Web.Http.Route("api/AccountsAPI/updatePayeementsDetails")]
        [System.Web.Http.HttpPost]
        public string updatePayeementsDetails(PayeeDetails pd)
        {

            tblTransExPayeeDetail pay = new tblTransExPayeeDetail();
            pay.ExpDetailsId = Convert.ToInt32(pd.Id);

            pay.PayingAmount = pd.PayingAmount;

            pay.PaymentDate = Convert.ToDateTime(pd.PaymentDate);
            pay.PaymentMode = pd.PaymentMode;
            if (pay.PaymentMode == "Cheque")
            {
                pay.chequeNo = pd.Checqno;
                pay.ChequeDate = Convert.ToDateTime(pd.ChequeDate);
                pay.BankName = pd.BankRemarks;
            }

            pay.Remarks = pd.Remarks;
            pay.SchoolID = pd.SchoolID;
            db.tblTransExPayeeDetails.Add(pay);
            db.SaveChanges();
            sqlHelper obj = new sqlHelper();
            string PreviousDueAmount = obj.ExecuteScaler("select DuesAmount from tblExPayeeDetails where SchoolID='" + pd.SchoolID + "' and IsDeleted is null and Id=" + pd.Id);
            string PreviousPaidAmount = obj.ExecuteScaler("select PayingAmount from tblExPayeeDetails where SchoolID='" + pd.SchoolID + "' and IsDeleted is null and Id=" + pd.Id);
            int id = Convert.ToInt32(pd.Id);
            var result = db.tblExPayeeDetails.SingleOrDefault(s => s.Id == id && s.SchoolID == pd.SchoolID);
            //Int64 due = Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount);
            result.DuesAmount = (Convert.ToInt64(PreviousDueAmount) - Convert.ToInt64(pd.PayingAmount)).ToString();
            result.PayingAmount = (Convert.ToInt64(PreviousPaidAmount) + Convert.ToInt64(pd.PayingAmount)).ToString();
            db.SaveChanges();
            return "OK";





        }


        [System.Web.Http.Route("api/AccountsAPI/deletePayableDetailsById")]
        [System.Web.Http.HttpPost]
        public string deletePayableDetailsById(List<string> id)
        {
            Int32 idd = Convert.ToInt32(id[0]);
            //var  tbl = new tblExPayeeDetail { Id = idd };
            //db.Entry(tbl).State = System.Data.Entity.EntityState.Deleted;
            var aa = db.tblExPayeeDetails.SingleOrDefault(s => s.Id == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "OKK";

        }

        //[System.Web.Http.Route("api/AccountsAPI/getAllPayingPaymentDetailsIntopdf")]
        //[System.Web.Http.HttpPost]
        //public List<PayeeDetails> getAllPayingPaymentDetailsIntopdf(List<string> id)
        //{
        //    DataTable datatable = new DataTable();
        //    List<PayeeDetails> list = new List<PayeeDetails>();
        //    sqlHelper obj = new sqlHelper();
        //    int SchoolID = Convert.ToInt32(id[0]);

        //    datatable.Columns.Add(new DataColumn("Category"));
        //    datatable.Columns.Add(new DataColumn("Payee"));
        //    datatable.Columns.Add(new DataColumn("Payment Mode"));
        //    datatable.Columns.Add(new DataColumn("Paying Amount"));
        //    datatable.Columns.Add(new DataColumn("Remarks"));
        //    datatable.Columns.Add(new DataColumn("Payment Date"));

        //    DataTable dt = obj.getDataTable(@"select ep.SchoolID, ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
        //                  left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
        //                  left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID  
        // where  ep.SchoolID='"+ SchoolID + "'  and ep.IsDeleted is null  order by ep.Id desc");



        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            PayeeDetails usr = new PayeeDetails();
        //            // PayeeDetails usr = new PayeeDetails();
        //            usr.Category = dr["CategoryName"].ToString();
        //            usr.Id = dr["Id"].ToString();
        //            usr.Name = dr["PayeeName"].ToString();
        //            usr.PaymentMode = dr["PaymentMode"].ToString();
        //            usr.Remarks = dr["Remarks"].ToString();
        //            usr.PayingAmount = dr["PayingAmount"].ToString();
        //            // usr.DuesAmount = dr["DuesAmount"].ToString();
        //            usr.PaymentDate = ((DateTime)dr["PaymentDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        //            // usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        //            usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
        //        list.Add(usr);


        //        }
        //    datatable.Rows.Add(list);
        //    iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
        //    rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
        //    Document doc = new Document(rec);
        //    doc.SetPageSize(iTextSharp.text.PageSize.A4);
        //    PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
        //    doc.Open();
        //    //Creating paragraph for header
        //    BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.ORANGE);
        //    Paragraph prgHeading = new Paragraph();
        //    prgHeading.Alignment = Element.ALIGN_LEFT;
        //    prgHeading.Add(new Chunk("Employee Details".ToUpper(), fntHead));
        //    doc.Add(prgHeading);
        //    //Adding paragraph for report generated by
        //    Paragraph prgGeneratedBY = new Paragraph();
        //    BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLUE);
        //    prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
        //    prgGeneratedBY.Add(new Chunk("Report Generated by : ASPArticles", fntAuthor));
        //    prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
        //    doc.Add(prgGeneratedBY);
        //    //Adding a line
        //    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
        //    doc.Add(p);
        //    //Adding line break
        //    doc.Add(new Chunk("\n", fntHead));
        //    //Adding  PdfPTable
        //    PdfPTable table = new PdfPTable(datatable.Columns.Count);
        //    for (int i = 0; i < datatable.Columns.Count; i++)
        //    {
        //        string cellText = HttpContext.Current.Server.HtmlDecode(datatable.Columns[i].ColumnName);
        //        PdfPCell cell = new PdfPCell();
        //        cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#ffffff"))));
        //        cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#990000"));
        //        //cell.Phrase = new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(grdStudent.HeaderStyle.ForeColor)));
        //        //cell.BackgroundColor = new BaseColor(grdStudent.HeaderStyle.BackColor);
        //        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //        cell.PaddingBottom = 5;
        //        table.AddCell(cell);
        //    }
        //    //writing table Data
        //    for (int i = 0; i < datatable.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < datatable.Columns.Count; j++)
        //        {
        //            table.AddCell(datatable.Rows[i][j].ToString());
        //        }
        //    }
        //    doc.Add(table);
        //    doc.Close();
        //    writer.Close();
        //    HttpContext.Current.Response.ContentType ="application/pdf";
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;" + "filename=EmployeeDetails.pdf");
        //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    HttpContext.Current.Response.Write(doc);
        //    HttpContext.Current.Response.End();
        //    //Response.ContentType = "application/pdf";
        //    //Response.AddHeader("content-disposition", "attachment;" + "filename=EmployeeDetails.pdf");
        //    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    //Response.Write(doc);
        //    //Response.End();


        //    return list;
        //}


        [System.Web.Http.Route("api/AccountsAPI/getAllPayingPaymentDetails")]
        [System.Web.Http.HttpPost]
        public List<PayeeDetails> getAllPayingPaymentDetails(List<string> aa)
        {
            List<PayeeDetails> list = new List<PayeeDetails>();
            sqlHelper obj = new sqlHelper();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID, ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                          left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                          left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID where ep.IsDeleted is null
						  order by ep.Id desc");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                //left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                //left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId order by ep.Id desc");

                foreach (DataRow dr in dt.Rows)
                {
                    PayeeDetails usr = new PayeeDetails();
                    // PayeeDetails usr = new PayeeDetails();
                    usr.Category = dr["CategoryName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["PayeeName"].ToString();
                    usr.PaymentMode = dr["PaymentMode"].ToString();
                    usr.Remarks = dr["Remarks"].ToString();
                    usr.PayingAmount = dr["PayingAmount"].ToString();
                    // usr.DuesAmount = dr["DuesAmount"].ToString();
                    usr.PaymentDate = ((DateTime)dr["PaymentDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    // usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);

                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select s.School,ep.SchoolID, ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                          left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                          left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId join tblSchoolDetails s on s.ID=ep.SchoolID
						  left outer join tblEmployee em on em.SchoolID=ep.SchoolID 
						   where em.UserID='" + loginuser + "' and em.IsDeleted is null and ep.IsDeleted is null  order by ep.Id desc");

                //            DataTable dt = obj.getDataTable(@"select ec.CategoryName,ee.PayeeName, ep.* from tblTransExPayeeDetails ep
                //left outer join tblExPayeeDetails ee on ee.Id=ep.ExpDetailsId
                //left outer join tblExpenseCategory ec on ec.Id = ee.CategoryId order by ep.Id desc");

                foreach (DataRow dr in dt.Rows)
                {
                    PayeeDetails usr = new PayeeDetails();
                    // PayeeDetails usr = new PayeeDetails();
                    usr.Category = dr["CategoryName"].ToString();
                    usr.Id = dr["Id"].ToString();
                    usr.Name = dr["PayeeName"].ToString();
                    usr.PaymentMode = dr["PaymentMode"].ToString();
                    usr.Remarks = dr["Remarks"].ToString();
                    usr.PayingAmount = dr["PayingAmount"].ToString();
                    // usr.DuesAmount = dr["DuesAmount"].ToString();
                    usr.PaymentDate = ((DateTime)dr["PaymentDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    // usr.LastDate = ((DateTime)dr["LastDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    usr.School = dr["School"].ToString();
                    list.Add(usr);

                }
            }
            return list;
        }

        [System.Web.Http.Route("api/AccountsAPI/countTotalPayingAmountbySchool")]
        [System.Web.Http.HttpPost]
        public string countTotalPayingAmountbySchool(List<string> id)
        {
            int SchoolID = Convert.ToInt32(id[0]);
            sqlHelper obj = new sqlHelper();

            string payingAmount = obj.ExecuteScaler("select SUM(cast(PayingAmount as float)) TotalAmount from tblTransExPayeeDetails where SchoolID='" + SchoolID + "' and IsDeleted is null");
            return payingAmount;
        }

        [System.Web.Http.Route("api/AccountsAPI/countTotalPayingAmount")]
        [System.Web.Http.HttpPost]
        public string countTotalPayingAmount()
        {
            sqlHelper obj = new sqlHelper();

            string payingAmount = obj.ExecuteScaler("select SUM(cast(PayingAmount as float)) TotalAmount from tblTransExPayeeDetails");
            return payingAmount;
        }


        [System.Web.Http.Route("api/AccountsAPI/searchPaymentDetails")]
        [System.Web.Http.HttpPost]
        public List<PayeeDetails> searchPaymentDetails(PayeeDetails payee)
        {
            sqlHelper obj = new sqlHelper();
            List<PayeeDetails> list = new List<PayeeDetails>();


            string[] cols = { "@name", "@date", "@paymentMode", "@SchoolID" };
            object[] vals = { payee.Name, payee.PaymentDate, payee.PaymentMode, payee.SchoolID };

            DataTable dt = obj.sp_GetDataTable("sp_searchExpensePaymentDetails", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                PayeeDetails usr = new PayeeDetails();
                // PayeeDetails usr = new PayeeDetails();
                usr.Category = dr["CategoryName"].ToString();
                usr.Id = dr["Id"].ToString();
                usr.Name = dr["PayeeName"].ToString();
                usr.PaymentMode = dr["PaymentMode"].ToString();
                usr.Remarks = dr["Remarks"].ToString();
                usr.PayingAmount = dr["PayingAmount"].ToString();
                // usr.DuesAmount = dr["DuesAmount"].ToString();
                usr.PaymentDate = ((DateTime)dr["PaymentDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                usr.School = dr["School"].ToString();
                list.Add(usr);

            }
            return list;
        }





        [System.Web.Http.Route("api/AccountsAPI/deleteExpanseDetailsById")]
        [System.Web.Http.HttpPost]
        public string deleteExpanseDetailsById(List<string> id)
        {
            Int32 idd = Convert.ToInt32(id[0]);
            //var tbl = new tblTransExPayeeDetail{ Id = idd };
            //db.Entry(tbl).State = System.Data.Entity.EntityState.Deleted;
            var aa = db.tblTransExPayeeDetails.SingleOrDefault(s => s.Id == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "OKK";

        }




    }
}
