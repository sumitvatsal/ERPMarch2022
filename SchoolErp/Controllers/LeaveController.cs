using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult LeaveCategory()
        {
            return View();
        }

        public ActionResult LeaveDetails()
        {
            return View();
        }

        public ActionResult EmployeeLeaveRequest()
        {
            return View();
        }

   

    }
}