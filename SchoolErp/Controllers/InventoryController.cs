using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Brands()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Units()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }
        public ActionResult NewProducts()
        {
            return View();
        }

        public ActionResult DamagedProducts()
        {
            return View();
        }

        public ActionResult DamageProductList()
        {
            return View();
        }
        public ActionResult GoodsReceipt()
        {
            return View();
        }
        public ActionResult GoodsReceiptList()
        {
            return View();
        }
        public ActionResult GoodsIssue()
        {
            return View();
        }
        public ActionResult GoodsIssueList()
        {
            return View();
        }
    }
}