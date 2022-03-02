using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult NewPurchase()
        {
            return View();
        }
        public ActionResult Purchases()
        {
            return View();
        }
        public ActionResult PurchasePDF()
        {
            return View();
        }
    }
}