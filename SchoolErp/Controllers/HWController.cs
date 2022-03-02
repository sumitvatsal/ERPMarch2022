using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class HWController : Controller
    {
        public ActionResult AssignHW()
        {
            return View();
        }
        public ActionResult CreateHW()
        {
            return View();
        }
        public ActionResult ViewHW()
        {
            return View();
        }
    }
}