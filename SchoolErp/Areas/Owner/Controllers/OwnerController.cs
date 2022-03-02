using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Areas.Owner.Controllers
{
    public class OwnerController : Controller
    {
        // GET: Owner/Owner
        public ActionResult OwnerLogin()
        {
            return View();
        }
    }
}