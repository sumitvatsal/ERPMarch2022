using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult salaryHead()
        {
            return View();
        }

        public ActionResult GradeMaster()
        {
            return View();
        }

        public ActionResult ViewGradeMaster()
        {
            return View();
        }

        public ActionResult EmployeeAccountInformation()
        { return View();
        }

        public ActionResult ViewAssginGrade()
        {
            return View();
        }

        public ActionResult GeneratePayslip()
        {
            return View();
        }


        public ActionResult ViewSalaryStructure()
        {
            return View();
        }

        public ActionResult ViewEmployeeSalaryDetails()
        {
            return View();

        }

        public ActionResult SalarySlipGenerate()
        {
            return View();
        }


    }
}