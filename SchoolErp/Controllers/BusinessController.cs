using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class BusinessController : Controller
    {
        // GET: Business
        public ActionResult NewCustomers()
        {
            return View();
        }

        public ActionResult NewSupplier()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }
        public ActionResult Suppliers()
        {
            return View();
        }
    }
}