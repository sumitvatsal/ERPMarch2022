using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers.WebApi
{
    public class demoController : Controller
    {
        // GET: demo
        public ActionResult Index()
        {
            SCHOOLERPEntities entities = new SCHOOLERPEntities();
            return View(from Sales in entities.Sales 
                        select Sales);
            //return View();
        }
    }
}