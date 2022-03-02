using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult NewInvoice()
        {
            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult ServiceRPT()
        {
            return View();
        }
    }
}