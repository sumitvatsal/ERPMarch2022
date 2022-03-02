using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        //private readonly object sessionStorage;

        // GET: AdminPanel
        public ActionResult Join(int typeid)
        {
            
             ViewBag.isPartialView = "~/Views/Shared/_LayoutMain.cshtml";

            if (typeid== 4)
            {
                ViewBag.isPartialView = "~/Views/Shared/_LayoutStudent.cshtml";

            }
            return View();
        }

       
        public ActionResult Meeting()
        {
            return View();
        }


        public ActionResult CreateMeeting(int typeid)
        {
            ViewBag.isPartialView = "~/Views/Shared/_LayoutMain.cshtml";

            if (typeid == 4)
            {
                ViewBag.isPartialView = "~/Views/Shared/_LayoutStudent.cshtml";

            }
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult SuperAdminDashboard()
        {
            return View();
        }
        
      
        public ActionResult SchoolAdminDashboard()
        {
            return View();
        }
        public ActionResult StudentDashboard()
        {
            //ViewBag.CurrntDate = DateTime.Now.ToString("dddd ,MMMM dd, yyyy");
            //SCHOOLERPEntities db = new SCHOOLERPEntities();
            //var thoughts = db.tblThoughtsOfDays.SingleOrDefault();
            //ViewBag.thoughts = thoughts.Thoughts;
            return View();
        }

        public ActionResult SchoolDetails()
        {
            return View();
        }

        public ActionResult RoleAssignment()
        {
            return View();
        }
        public ActionResult SchoolRoleManagement()
        {
            return View();
        }
        public ActionResult AddAttendenceMachine()
        {
            return View();
        }
        public ActionResult ConferenceRoom()
        {
            return View();
        }

    }
}