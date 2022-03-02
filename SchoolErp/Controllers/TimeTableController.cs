using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class TimeTableController : Controller
    {
        // GET: TimeTable
        public ActionResult ClassTiming()
        {
            return View();
        }

        public ActionResult ClassTimingDetails()
        {
            return View();
        }

        public ActionResult TimeTableConfig()
        {
            return View();
        }
        public ActionResult TimeTableConfigCreate()
        {
            return View();
        }
        public ActionResult CreateTimeTable()
        {
            return View();
        }
        public ActionResult ViewStudentTimeTable()
        {
            return View();
        }
        public ActionResult ViewTeacherTimeTable()
        {
            return View();
        }
        public ActionResult NewTimeTable()
        {
            return View();
        }
        public ActionResult AssignHolliDays()
        {
            return View();
        }
        public ActionResult SectionDetails()
        {
            return View();
        }

    }
}