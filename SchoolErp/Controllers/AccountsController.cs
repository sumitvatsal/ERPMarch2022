using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult ExpCategory()
        {
          
            return View();
        }

        public ActionResult CreatePayee()
        {
            return View();
        }

        public ActionResult ViewPayeeAccounts() {

            return View();
        }

        public ActionResult editPayeeAccounts() {
            return View();
        }
        public ActionResult Expenses()
        {
            return View();
        }
    }
}