using MCMD.Web.Areas.Administration;
using MCMD.Web.Areas.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MCMD.Web.Areas
{
    public class AreaConfig
    {
        public static void RegisterAreas()
        {

            //Admin
            //var adminArea = new AdministrationAreaRegistration();
            //var adminAreaContext = new AreaRegistrationContext(adminArea.AreaName, RouteTable.Routes);
            //adminArea.RegisterArea(adminAreaContext);
            

            // Doctor area . . .
            var doctorArea = new DoctorAreaRegistration();
            var doctorAreaContext = new AreaRegistrationContext(doctorArea.AreaName, RouteTable.Routes);
          //  doctorArea.RegisterArea(doctorAreaContext);




        }
    }
}