using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.WebInterfaces;
using MCMD.EntityRepository.WebRepository;
using MCMD.ViewModel.doctor;
using GoogleMaps.LocationServices;


namespace MCMD.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDocProfile docprofile;
        public HomeController(IDocProfile _docprofile)
        {
            this.docprofile = _docprofile;
        }
        #region Search
        public ActionResult Search()
        {
            HomeDocSearchViewModel homeDocSearchVM = new HomeDocSearchViewModel();

            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();
            homeDocSearchVM.GetCitys = docprofile.GetCity().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.getDoctor = docprofile.getAllDoctor().ToList();


            return View(homeDocSearchVM);
        }
        [HttpPost]
        public ActionResult Search(HomeDocSearchViewModel homeDocSearchVM)
        {
            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();
            homeDocSearchVM.GetCitys = docprofile.GetCity().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.getDoctor = docprofile.getAllDoctor().ToList();

            int SpecialityID = 0;

            int CityId = 0;
            string Userfirstname = "";
            string Userlastname = "";
            string ClinicName = "";

            if (homeDocSearchVM.SpecialityID != 0 || homeDocSearchVM.RoleId != 0 || homeDocSearchVM.CityId != 0)
            {
                SpecialityID = homeDocSearchVM.SpecialityID;

                CityId = homeDocSearchVM.CityId;

            }

            if (!string.IsNullOrEmpty(homeDocSearchVM.FirstName))
            {
                Userfirstname = homeDocSearchVM.FirstName;

            }
            if (!string.IsNullOrEmpty(homeDocSearchVM.LastName))
            {
                Userlastname = homeDocSearchVM.LastName;

            }
            if (!string.IsNullOrEmpty(homeDocSearchVM.ClinicName))
            {
                ClinicName = homeDocSearchVM.ClinicName;
            }
            return RedirectToAction("Doctors", new { SpeID = SpecialityID, CityId = CityId, UserFirstName = Userfirstname, UserLastName = Userlastname, ClinicName = ClinicName });


            // return View(homeDocSearchVM);

        }

        #endregion
        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult TermServices()
        {
            return View();
        }
        #region DoctorProfile

        public ActionResult DocProfile(int Id = 0)
        {
            HomeDocSearchViewModel homeDocSearchVM = new HomeDocSearchViewModel();

            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();
            homeDocSearchVM.GetCitys = docprofile.GetCity().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.getDoctor = docprofile.getAllDoctor().ToList();

            // DocProfileViewModel _docprofileVM = new DocProfileViewModel();
            Session["DocProfileID"] = Id;
            int DocProfileID = Convert.ToInt32(Session["DocProfileID"]);
            List<DoctorPersonalInformation> _ObDocPersonalInfo = docprofile.GetDocPersonalInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<UserLogin> _ObDocLoginInfo = docprofile.GetDocLoginInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<UserLoginSpeciality> _ObDocLoginSpecialityInfo = docprofile.GetDocLoginSpeciality().Where(x => x.LoginId == DocProfileID).ToList();
            List<DoctorClinicInformation> _ObDocClinicInfo = docprofile.GetDocClinicInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<Media> _obDocMediaInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N" && x.UploadType != "video/mp4").ToList();
            List<ClinicTimeInformation> _ObDocClinicTimeInfo = docprofile.GetDocClinicTime().Where(x => x.LoginId == DocProfileID && x.Setting == 1).ToList();
            List<ClinicTimeInformation> GetclinicTimeFirst = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.Setting == 1).ToList();
            List<ClinicTimeInformation> GetclinicTimeSecond = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.Setting == 2).ToList();
            List<ClinicTimeInformation> GetclinicTimeThird = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.Setting == 3).ToList();


            foreach (var item in _ObDocPersonalInfo)
            {
                homeDocSearchVM.MiddleName = item.MiddleName;
                homeDocSearchVM.Qualification = item.Qualification;
                homeDocSearchVM.ExperienceInYear = item.ExperienceInYear;
                homeDocSearchVM.ExperienceInMonth = item.ExperienceInMonth;
                homeDocSearchVM.AboutMe = item.AboutMe;
                //_docprofileVM.Affiliation = item.Affiliation;

                //Profile Photo
                homeDocSearchVM.FolderFilePath = item.FolderFilePath;

                string straffilation = item.Affiliation;
                string[] strsplitaffil = straffilation.Split(',');
                List<GetAffiliations> getaffiliations = new List<GetAffiliations>();
                foreach (var c in strsplitaffil)
                {
                    var p = new GetAffiliations();
                    p.affiliations = c;
                    getaffiliations.Add(p);
                }
                homeDocSearchVM.getallaffiliations = getaffiliations;

                string strregistration = item.RegistrationNo;
                string[] strsplitreg = strregistration.Split(',');
                List<GetRegistrations> getregistrations = new List<GetRegistrations>();
                foreach (var d in strsplitreg)
                {
                    var Q = new GetRegistrations();
                    Q.registrations = d;
                    getregistrations.Add(Q);
                }
                homeDocSearchVM.getallregistrations = getregistrations;

            }
            foreach (var item in _ObDocLoginInfo)
            {
                homeDocSearchVM.FirstName = item.FirstName;
                homeDocSearchVM.LastName = item.LastName;

            }
            foreach (var item in _ObDocClinicInfo)
            {
                homeDocSearchVM.ClinicAddress = item.ClinicAddress;

                string strawards = item.AwardsAndRecognization;
                string[] strsplitaward = strawards.Split(',');
                List<GetAwards> getawardss = new List<GetAwards>();
                foreach (var b in strsplitaward)
                {
                    var t = new GetAwards();
                    t.awards = b;
                    getawardss.Add(t);
                }
                homeDocSearchVM.getallawards = getawardss;

                homeDocSearchVM.ClinicName = item.ClinicName;
                homeDocSearchVM.ClinicServices = item.ClinicServices;
                homeDocSearchVM.ClinicFees = item.ClinicFees;

                string strservice = item.ClinicServices;
                string[] strspli = strservice.Split(',');
                List<GetService> getservice = new List<GetService>();
                foreach (var a in strspli)
                {
                    var s = new GetService();
                    s.services = a;
                    getservice.Add(s);

                }
                homeDocSearchVM.getservices = getservice;
            }

            foreach (var item in _ObDocLoginSpecialityInfo)
            {
                List<Speciality> _ObDocSpecialityInfo = docprofile.GetDocSpeciality().Where(x => x.SpecialityID == item.SpecialityID).ToList();
                foreach (var item1 in _ObDocSpecialityInfo)
                {
                    homeDocSearchVM.SpecialityName = item1.SpecialityName;
                }
            }

            // Clinic Photos frm media
            homeDocSearchVM.getmedias = _obDocMediaInfo;

            //first time seating
            foreach (var item in GetclinicTimeFirst)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimefs1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimefs2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimefs3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimefs4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimefs5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimefs6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimefs7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs7 = et1 + " " + item.EndSlot;
                }
            }

            //Second time seating
            foreach (var item in GetclinicTimeSecond)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimess1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimess2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimess3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimess4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimess5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimess6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimess7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess7 = et1 + " " + item.EndSlot;
                }
            }

            //Third Seating Time
            foreach (var item in GetclinicTimeThird)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimets1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimets2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimets3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimets4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimets5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimets6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimets7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets7 = et1 + " " + item.EndSlot;
                }


            }

            return View(homeDocSearchVM);
        }

        #endregion 

        #region Doctor Serarch result view
        public ActionResult Doctors(int SpeID = 0, int RoleId = 0, int CityId = 0, string UserFirstName = " ", string UserLastName = " ", string ClinicName = " ")
        {
            HomeDocSearchViewModel homeDocSearchVM = new HomeDocSearchViewModel();
            //Binding the DropDownLists
            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();
            homeDocSearchVM.GetCitys = docprofile.GetCity().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.getDoctor = docprofile.getAllDoctor().ToList();

            @TempData["Name"] = Session["Name"];
            // HomeDocSearchViewModel homeDocSearchVM = new HomeDocSearchViewModel();

           // homeDocSearchVM.getRoles = docprofile.GetRoles().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();




            if (SpeID != 0 || RoleId != 0 || CityId != 0 || !string.IsNullOrEmpty(UserFirstName) || !string.IsNullOrEmpty(UserLastName) || !string.IsNullOrEmpty(ClinicName))
            {

                homeDocSearchVM.SpecialityID = SpeID;
                homeDocSearchVM.RoleId = RoleId;
                homeDocSearchVM.CityId = CityId;
                string FirstName = UserFirstName;//Splitting first name
                string[] strsplitaffil = FirstName.Split(' ');
                UserFirstName = strsplitaffil[0];
                UserLastName = strsplitaffil[1];


                homeDocSearchVM.getSearchDoc = docprofile.SearchAllDoctor(SpeID, RoleId, CityId, UserFirstName, UserLastName, ClinicName).ToList();
            }
           
            return View(homeDocSearchVM);
        }

        [HttpPost]
        public ActionResult Doctors(HomeDocSearchViewModel homeDocSearchVM)
        {
            int SpecialityID = 0;
            int RoleId = 0;
            int CityId = 0;
            string Userfirstname = "";
            string Userlastname = "";
            string ClinicName = "";

            if (homeDocSearchVM.SpecialityID != 0 || homeDocSearchVM.RoleId != 0 || homeDocSearchVM.CityId != 0)
            {
                SpecialityID = homeDocSearchVM.SpecialityID;
                RoleId = homeDocSearchVM.RoleId;
                CityId = homeDocSearchVM.CityId;

            }

            if (!string.IsNullOrEmpty(homeDocSearchVM.FirstName))
            {
                Userfirstname = homeDocSearchVM.FirstName;

            }
            if (!string.IsNullOrEmpty(homeDocSearchVM.LastName))
            {
                Userlastname = homeDocSearchVM.LastName;

            }
            if (!string.IsNullOrEmpty(homeDocSearchVM.ClinicName))
            {
                ClinicName = homeDocSearchVM.ClinicName;
            }
            return RedirectToAction("Doctors", new { SpeID = SpecialityID, RoleId = RoleId, CityId = CityId, UserFirstName = Userfirstname, UserLastName = Userlastname, ClinicName = ClinicName });



        }

        #endregion 

        #region ClinicProfile
        public ActionResult ClinicProfile(int Id = 0)
        {
            //ClinicProfileViewModel _clinicprofileVM = new ClinicProfileViewModel();
            HomeDocSearchViewModel homeDocSearchVM = new HomeDocSearchViewModel();
            homeDocSearchVM.GetSpecialitylist = docprofile.GetDocSpeciality().ToList();
            homeDocSearchVM.GetCitys = docprofile.GetCity().ToList();
            homeDocSearchVM.GetDoctorClinicInformation = docprofile.GetClinicInformation().ToList();
            homeDocSearchVM.getDoctor = docprofile.getAllDoctor().ToList();

            Session["ClinicProfileID"] = Id;
            int ClinicProfileID = Convert.ToInt32(Session["ClinicProfileID"]);

            List<DoctorClinicInformation> _ObClinicInfo = docprofile.GetDocClinicInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N").ToList();
            List<UserLoginSpeciality> _ObclinicLoginSpecialityInfo = docprofile.GetDocLoginSpeciality().Where(x => x.LoginId == ClinicProfileID).ToList();
            List<Media> _obclinicMediaInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N" && x.UploadType != "video/mp4").ToList();
            List<Media> _obclinicVideoInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N" && x.UploadType == "video/mp4").ToList();
            List<ClinicTimeInformation> GetclinicTimeFirst = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.Setting == 1).ToList();
            List<ClinicTimeInformation> GetclinicTimeSecond = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.Setting == 2).ToList();
            List<ClinicTimeInformation> GetclinicTimeThird = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.Setting == 3).ToList();

            foreach (var item in _ObClinicInfo)
            {
                homeDocSearchVM.ClinicName = item.ClinicName;
                homeDocSearchVM.ClinicAddress = item.ClinicAddress;
                homeDocSearchVM.AboutClinic = item.AboutClinic;
                homeDocSearchVM.ClinicFees = item.ClinicFees;
                homeDocSearchVM.ClinicPhoneNo = item.ClinicPhoneNo;

                string address = item.ClinicAddress;
                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(address);
                string Latitude = point.Latitude.ToString();
                string Longitude = point.Longitude.ToString();
                homeDocSearchVM.latitude = Latitude;
                homeDocSearchVM.longitude = Longitude;

                string strservice = item.ClinicServices;
                string[] strspli = strservice.Split(',');
                List<GetService> getservice = new List<GetService>();
                foreach (var a in strspli)
                {
                    var s = new GetService();
                    s.services = a;
                    getservice.Add(s);

                }
                homeDocSearchVM.getservices = getservice;

                //Profile Photo
                //  _clinicprofileVM.FolderFilePath = item.FolderFilePath;
            }

            foreach (var item in _ObclinicLoginSpecialityInfo)
            {
                List<Speciality> _ObclinicSpecialityInfo = docprofile.GetDocSpeciality().Where(x => x.SpecialityID == item.SpecialityID).ToList();
                foreach (var item1 in _ObclinicSpecialityInfo)
                {
                    homeDocSearchVM.SpecialityName = item1.SpecialityName;
                }
            }

            // Clinic Photos from media
            homeDocSearchVM.getclinicmedias = _obclinicMediaInfo;

            //clinic Videos from media
            homeDocSearchVM.getclinicvideos = _obclinicVideoInfo;

            //first time seating
            foreach (var item in GetclinicTimeFirst)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimefs1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimefs2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimefs3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimefs4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimefs5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimefs6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimefs7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimefs7 = et1 + " " + item.EndSlot;
                }
            }

            //Second time seating
            foreach (var item in GetclinicTimeSecond)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimess1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimess2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimess3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimess4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimess5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimess6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimess7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimess7 = et1 + " " + item.EndSlot;
                }
            }

            //Third Seating Time
            foreach (var item in GetclinicTimeThird)
            {

                //convert start time of first seating
                string startime1 = item.StartTime.ToString();
                DateTime sd1 = DateTime.Parse(startime1);
                string st1 = sd1.ToString("hh:mm");
                //convert End time of first seating
                string endtime1 = item.EndTime.ToString();
                DateTime ed1 = DateTime.Parse(endtime1);
                string et1 = ed1.ToString("hh:mm");
                if (item.Day == "Monday")
                {
                    homeDocSearchVM.StartTimets1 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    homeDocSearchVM.StartTimets2 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    homeDocSearchVM.StartTimets3 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    homeDocSearchVM.StartTimets4 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    homeDocSearchVM.StartTimets5 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    homeDocSearchVM.StartTimets6 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    homeDocSearchVM.StartTimets7 = st1 + " " + item.StartSlot;
                    homeDocSearchVM.EndTimets7 = et1 + " " + item.EndSlot;
                }


            }

            return View(homeDocSearchVM);
        }
        #endregion ClinicProfile
       public ActionResult doc()
        {
            return View();
        }
        public ActionResult img()
       {
           return View();
       }
    }
}