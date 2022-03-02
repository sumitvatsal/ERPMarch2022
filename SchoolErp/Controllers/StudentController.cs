using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult AddStudent()
        {
           
            return View();
        }
        public ActionResult EditStudent()
        {
           
            return View();
        }

        public ActionResult ViewStudent()
        {
           
            return View();
        }

        public ActionResult StudentProfile()
        {
           
            return View();
        }

        public ActionResult StudentPrint()
        {
           
            return View();
        }

        public ActionResult AdmitCard()
        {
           
            return View();
        }

        public ActionResult StudentRegistration()
        {
          
            return View();
        }


        public ActionResult SendSMStoParent()
        {
         
            return View();
        }
        public ActionResult ProfileforStudent()
        {
          
            return View();
        }
        public ActionResult StudentLeave()
        {
            return View();
        }

        public ActionResult AdminViewLeaves()
        {
          
            return View();
        }

        public ActionResult studentMonthlyAttendenceReport()
        {
            
            return View();
        }


        public ActionResult sentEmailStudents()
        {
          
            return View();
        }

        public ActionResult ViewTaskDetails()
        {
           
            return View();
        }

        public ActionResult NoticeBoard()
        {
        
            return View();
        }

        public ActionResult ViewStudentDetails()
        {
         
            return View();
        }
        public ActionResult StudentVehicleCurrentLocation()
        {
            return View();
        }
        //  public ActionResult TakeStudentAttendence
    }
}