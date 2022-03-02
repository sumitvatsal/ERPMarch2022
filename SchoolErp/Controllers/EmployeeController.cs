using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using schoolERP_BLL;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult AddEmployee()
        {
            return View();
        }
        public ActionResult AddEmployeee()
        {
            return View();
        }
        public ActionResult EmpAdminAddEmployee()
        {
            return View();
        }
        public ActionResult SuperAdminAddEmployee()
        {
            return View();
        }
     
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ViewEmployee()
        {
            return View();
        }

        public ActionResult EmployeeDetails()
        {
            return View();
        }

        public ActionResult ViewEmployeeDetails()
        {
            return View();
        }


        public ActionResult ViewEmployeeDetailsPrint()
        {
            return View();
        }

        public ActionResult EmployeeAttendence()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
           using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

        public ActionResult EmployeeIdentityCard()
        {
            return View();
        }

        public ActionResult EmployeeSendSms()
        {
            return View();
        }


        public ActionResult EmployeeAttendenceReports()
        {
            return View();
        }
        public ActionResult ViewEmployeeSchoolAdmin()
        {
            return View();
        }
        
        public ActionResult SendEmailOfEmployees()
        {
            return View();
        }


        public ActionResult FeeCalculator()
        {
            return View();
        }

        public JsonResult saveFeeDetails(feecalculationdetails[] fee)
        {
           SCHOOLERPEntities db = new SCHOOLERPEntities();
            foreach (var a in fee)
            {
                tblFeeCalculate tfee = new tblFeeCalculate();
                tfee.monthId = a.MonthId;
                tfee.Months = a.Month;
                tfee.monthlyAmount = a.monthlyamt;
                tfee.PaidAmount = a.amount;
                tfee.duesAmount = a.duesAmount;
                db.tblFeeCalculates.Add(tfee);
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFeeDetails()
        {
          
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<tblFeeCalculate> list = new List<tblFeeCalculate>();
            var result = db.tblFeeCalculates.SqlQuery("select * from tblFeeCalculate where duesAmount=0").ToList(); 
            foreach (var a in result)
            {
                tblFeeCalculate tfee = new tblFeeCalculate();
                tfee.monthId = a.monthId;
                tfee.Months = a.Months;
                tfee.monthlyAmount = a.monthlyAmount;
                tfee.PaidAmount = a.PaidAmount;
                tfee.duesAmount = a.duesAmount;
                list.Add(tfee);
            }
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getFeeDetailsWithDues(string month)
        {

            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<tblFeeCalculate> list = new List<tblFeeCalculate>();
            var result = db.tblFeeCalculates.SqlQuery("select top 1 * from tblFeeCalculate where monthId="+ month+ " order by dateCreated desc").ToList();
            foreach (var a in result)
            {
                tblFeeCalculate tfee = new tblFeeCalculate();
                tfee.monthId = a.monthId;
                tfee.Months = a.Months;
                tfee.monthlyAmount = a.monthlyAmount;
                tfee.PaidAmount = a.PaidAmount;
                tfee.duesAmount = a.duesAmount;
                list.Add(tfee);
            }
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }

         public ActionResult LoadEmployeeAttendence()
        {
            return View();
        }


        public ActionResult Validation()
        {
            return View();
        }
        public ActionResult EmployeeBioAttendenceReports()
        {
            return View();
        }
    }
}