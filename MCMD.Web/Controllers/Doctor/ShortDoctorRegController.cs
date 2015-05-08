using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCMD.Web.Controllers.Doctor
{
    public class ShortDoctorRegController : Controller
    {
        // GET: ShortDoctorReg
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}