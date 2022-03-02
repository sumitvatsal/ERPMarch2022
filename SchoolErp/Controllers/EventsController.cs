using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult EventType()
        {
            return View();
        }
        
             public ActionResult EmpNoticeBoard()
        {
            
            return View();
        }
        public ActionResult AddEvents()
        {
            ViewBag.datetime = System.DateTime.Now;
            return View();
        }

        public ActionResult AssignTask()
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
    }
}