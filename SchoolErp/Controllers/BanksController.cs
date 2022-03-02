using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class BanksController : Controller
    {
        // GET: Banks
        public ActionResult BankTransaction()
        {
            return View();
        }

        public ActionResult Expenses()
        {
            return View();
        }
        public ActionResult CustomerReceive()
        {
            return View();
        }
        public ActionResult SupplierPayment()
        {
            return View();
        }
        public ActionResult CashAdjustment()
        {
            return View();
        }
    }
}