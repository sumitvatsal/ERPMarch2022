using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        SCHOOLERPEntities entities = new SCHOOLERPEntities();
        // GET: Reporting
        public ActionResult Employees()
        {
            return View();
        }

        public ActionResult AllViewFee()
        {
            return View();
        }

        public ActionResult StudentWiseFeeReport()
        {
            return View();
        }
        public ActionResult StudentsReport()
        {
            return View();
        }
        public ActionResult FeePaidReports()
        {
            return View();
        }

        public ActionResult StudentAttendencReport()
        {
            return View();
        }

        public ActionResult EmployeeAttendenceReports()
        {
            return View();
        }
        public ActionResult ExpensesReports()
        {
            return View();
        }

        public ActionResult ClassTeacherReports()
        {
            return View();
        }

        public ActionResult LeaveRequestReports()
        {
            return View();
        }

        public ActionResult SalaryReports()
        {
            return View();
        }

        public ActionResult UnpaidFeeReports()
        {
            return View();
        }

        public ActionResult EmployeeDailyAttendenceReports()
        {
            return View();
        }

        public ActionResult DayClosing()
        {
            return View();
        }

        public ActionResult TodaysReport()
        {
            return View();
        }

        public ActionResult DailyClosing()
        {
            return View();
        }

        public ActionResult StockReport()
        {
            return View();
        }

        public ActionResult SaleReport()
        {
            return View(from SaleDetails in entities.SaleDetails
                        select SaleDetails);
        }

        public ActionResult ProductWiseSalesReport()
        {
            return View();
        }

        public ActionResult PurchaseRepor()
        {
            return View();
        }

        public ActionResult CustomerReceivableReport()
        {
            return View();
        }

        public ActionResult SupplierPayableReport()
        {
            return View();
        }

    }
}