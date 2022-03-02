using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class ParentsController : Controller
    {
        // GET: Parents
        public ActionResult ParentDashboard()
        {
            //ViewBag.CurrntDate = DateTime.Now.ToString("dddd ,MMMM dd, yyyy");
            //SCHOOLERPEntities db = new SCHOOLERPEntities();
            //var thoughts = db.tblThoughtsOfDays.SingleOrDefault();
            //ViewBag.thoughts = thoughts.Thoughts;
            return View();
        }

        public ActionResult ProfileForParents()
        {
            return View();
        }

        public ActionResult ViewStudentTimeTable()
        {
            return View();
        }
        public ActionResult studentMonthlyAttendenceReport()
        {
            return View();
        }
        public ActionResult StudentLeave()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult ViewHW()
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
        
             public ActionResult StudentVehicleCurrentLocation()
        {
            return View();
        }

    }
}