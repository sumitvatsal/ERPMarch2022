using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class LicenceController : Controller
    {
        // GET: Licence
        public ActionResult LicenceError()
        {
            return View();
        }
        public ActionResult EditLicence()
        {
            return View();
        }
        public ActionResult LicenceDetails()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LicenceDetailsLicenceError()
        {
            return View();
        }
        
    }
}