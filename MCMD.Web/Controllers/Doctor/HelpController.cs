﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCMD.Web.Controllers.Doctor
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            return View();
        }
    }
}