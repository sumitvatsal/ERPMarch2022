using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class SaleAccController : Controller
    {
        SCHOOLERPEntities entities = new SCHOOLERPEntities();
        // GET: SaleAcc
        public ActionResult NewsaleQuotation()
        {
            return View();
        }
        public ActionResult NewSale()
        {
            return View(from SaleDetails in entities.SaleDetails
                        select SaleDetails);
        }
        public ActionResult Sales()
        {
            return View();
        }
        public ActionResult SaleQuotation()
        {
            return View();
        }
        public ActionResult QuotationPDF()
        {
            return View();
        }
        public ActionResult SaleRPT()
        {
            return View();
        }
    }
}