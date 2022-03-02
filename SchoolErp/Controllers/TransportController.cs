using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Controllers
{
    [Authorize]
    public class TransportController : Controller
    {
        // GET: Transport
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        public ActionResult AddVehicle()
        {
            return View();
        }

        public ActionResult AddDriver()
        {
            return View();
        }


 
        public JsonResult getDriverAutocomplete(string prefix)
        {

            var customers = (from customer in db.tblEmployees
                             where customer.FirstName.StartsWith(prefix) &&  customer.DeptID==11 && customer.DesigID==19
                             select new
                             {
                                 label = customer.FirstName + " " + customer.LastName,
                                 val = customer.Id
                             }).ToList();
            return Json(customers);


        }

        public ActionResult AddRoute()
        {
            return View();
        }

        public ActionResult AddDestination()
        {
            return View();
        }

        public ActionResult TransportAllocation()
        {
            return View();
        }


        public ActionResult ViewTransportAllocation() {
            return View();
        }

        public ActionResult ViewTransportAllocationDetails() {
            return View();
                 
        }

        public ActionResult TransportSmsAlert()
        {
            return View();
        }


        public ActionResult ManageTransport()
        {
            return View();
        }
        public ActionResult AddGpsDevice()
        {
            return View();
        }
        public ActionResult ShowRoute()
        {
            return View();
        }
            
        public ActionResult AddHub()
        {
            return View();

        }
        public ActionResult ListHub()
        {
            return View();

        }
        public ActionResult ViewHub()
        {
            return View();

        }
        public ActionResult ViewRoute()
        {
            return View();

        }
        public ActionResult ShowVehicleCurrentLocation()
        {
            return View();

        }
        public ActionResult OurGpsVehiclelist()
        {
            return View();

        }
        public ActionResult ChangeGpsService()
        {
            return View();

        }
        public ActionResult EmployeeVehicleCurrentLocation()
        {
            return View();

        }

        





    }
}