using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class EmployeeDashboardController : Controller
    {
        // GET: EmployeeDashboard
        public ActionResult Dashboard()
        {          
            return View();
        }

        public ActionResult EmployeeProfile()
        {
            return View();
        }

        public ActionResult EmployeeDiary()
        {
            return View();
        }

        public ActionResult LeaveRequest()
        {
            return View();
        }

        public ActionResult LeaveStatus()
        {
            return View();
        }

        public ActionResult SalarySlip()
        {
            return View();
        } 
        public ActionResult TakeStudentAttendence()
        {
            return View();
        }

        public ActionResult ViewStudentAttendence()
        {
            return View();
        }

        public ActionResult ViewStudentAttendenceMonthlyReport()
        {
            return View();
        }
        public ActionResult SchoolSuperAdminProfile()
        {
            return View();
        }
        
        public ActionResult AttendenceReports()
        {
            return View();
        }

        public ActionResult ViewTaskDetails()
        {
            return View();
        }

        public ActionResult NoticeBoard()
        {
            return View();
        }
        public ActionResult StudentPromotion()
        {
            return View();
        }

    }
}