using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCMD.Web.Controllers.Administration
{
    public class ManageAppointmentController : Controller
    {
        // GET: ManageAppointment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Appointment()
        {
            @TempData["Name"] = Session["Name"];
            return View();
        }
    }
}