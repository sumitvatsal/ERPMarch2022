using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    public class AccountFinancialController : Controller
    {
        // GET: AccountFinancial
        public ActionResult Accounts()
        {
            return View();
        }
        public ActionResult AccountsOfChart()
        {
            return View();
        }
        public ActionResult OpeningBalance()
        {
            return View();
        }
        public ActionResult DebitVoucher()
        {
            return View();
        }
        public ActionResult CreditVoucher()
        {
            return View();
        }
        public ActionResult ContraVoucher()
        {
            return View();
        }
        public ActionResult JournalVoucher()
        {
            return View();
        }
    }
}