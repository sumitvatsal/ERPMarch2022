using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;

namespace SchoolErp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult SuperAdminLogin()
        {
            return View();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()

        {
            
            return View();
        }
        public ActionResult LogoutSuperAdmin()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("SuperAdminLogin", "Home"); //action & then controller

            //return View();
        }
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Login", "Home"); //action & then controller

            //return View();
        }
        [Authorize]
        public ActionResult NewStudentReg()
        {
            return View();
        }
        [Authorize]
        public ActionResult NewRegApproval()
        {
            return View();
        }
        [Authorize]
        public ActionResult ViewRegApplication()
        {
            return View();
        }
       

    }
}