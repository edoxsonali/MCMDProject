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


namespace MCMD.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDocProfile docprofile;
        public HomeController(IDocProfile _docprofile)
        {
            this.docprofile = _docprofile;
        }
        public ActionResult Search()
        {
            return View();
        }
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

        public ActionResult DocProfile()
        {
            DocProfileViewModel _docprofileVM = new DocProfileViewModel();
            Session["DocProfileID"] = 2;
            int DocProfileID = Convert.ToInt32(Session["DocProfileID"]);
            List<DoctorPersonalInformation> _ObDocPersonalInfo = docprofile.GetDocPersonalInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<UserLogin> _ObDocLoginInfo = docprofile.GetDocLoginInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<UserLoginSpeciality> _ObDocLoginSpecialityInfo = docprofile.GetDocLoginSpeciality().Where(x => x.LoginId == DocProfileID).ToList();
            List<DoctorClinicInformation> _ObDocClinicInfo = docprofile.GetDocClinicInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N").ToList();
            List<Media> _obDocMediaInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == DocProfileID && x.InactiveFlag == "N" && x.UploadType != "video/mp4").ToList();
            List<ClinicTimeInformation> _ObDocClinicTimeInfo = docprofile.GetDocClinicTime().Where(x => x.LoginId == DocProfileID && x.FirstSetting == "1st seating Timing").ToList();
            List<ClinicTimeInformation> GetclinicTimeFirst = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.FirstSetting == "1st seating Timing").ToList();
            List<ClinicTimeInformation> GetclinicTimeSecond = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.FirstSetting == "2nd seating Timing").ToList();
            List<ClinicTimeInformation> GetclinicTimeThird = docprofile.GetAllClinicTime().Where(x => x.LoginId == DocProfileID && x.FirstSetting == "3rd seating Timing").ToList();

         
            foreach (var item in _ObDocPersonalInfo)
            {
                _docprofileVM.MiddleName = item.MiddleName;
                _docprofileVM.Qualification = item.Qualification;
                _docprofileVM.AboutExperience = item.AboutExperience;
                _docprofileVM.AboutMe = item.AboutMe;
                //_docprofileVM.Affiliation = item.Affiliation;

                //Profile Photo
                _docprofileVM.FolderFilePath = item.FolderFilePath;

                string straffilation = item.Affiliation;
                string[] strsplitaffil = straffilation.Split(',');
                List<GetAffiliations> getaffiliations = new List<GetAffiliations>();
                foreach (var c in strsplitaffil)
                {
                    var p = new GetAffiliations();
                    p.affiliations = c;
                    getaffiliations.Add(p);
                }
                _docprofileVM.getallaffiliations = getaffiliations;

                string strregistration = item.RegistrationNo;
                string[] strsplitreg = strregistration.Split(',');
                List<GetRegistrations> getregistrations = new List<GetRegistrations>();
                foreach (var d in strsplitreg)
                {
                    var Q = new GetRegistrations();
                    Q.registrations = d;
                    getregistrations.Add(Q);
                }
                _docprofileVM.getallregistrations = getregistrations;

            }
            foreach (var item in _ObDocLoginInfo)
            {
                _docprofileVM.FirstName = item.FirstName;
                _docprofileVM.LastName = item.LastName;

            }
            foreach (var item in _ObDocClinicInfo)
            {
                _docprofileVM.ClinicAddress = item.ClinicAddress;

                string strawards = item.AwardsAndRecognization;
                string[] strsplitaward = strawards.Split(',');
                List<GetAwards> getawardss = new List<GetAwards>();
                foreach (var b in strsplitaward)
                {
                    var t = new GetAwards();
                    t.awards = b;
                    getawardss.Add(t);
                }
                _docprofileVM.getallawards = getawardss;

                _docprofileVM.ClinicName = item.ClinicName;
                _docprofileVM.ClinicServices = item.ClinicServices;
                _docprofileVM.ClinicFees = item.ClinicFees;

                string strservice = item.ClinicServices;
                string[] strspli = strservice.Split(',');
                List<GetService> getservice = new List<GetService>();
                foreach (var a in strspli)
                {
                    var s = new GetService();
                    s.services = a;
                    getservice.Add(s);

                }
                _docprofileVM.getservices = getservice;
            }

            foreach (var item in _ObDocLoginSpecialityInfo)
            {
                List<Speciality> _ObDocSpecialityInfo = docprofile.GetDocSpeciality().Where(x => x.SpecialityID == item.SpecialityID).ToList();
                foreach (var item1 in _ObDocSpecialityInfo)
                {
                    _docprofileVM.SpecialityName = item1.SpecialityName;
                }
            }

            // Clinic Photos frm media
            _docprofileVM.getmedias = _obDocMediaInfo;

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
                    _docprofileVM.StartTimefs1 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _docprofileVM.StartTimefs2 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _docprofileVM.StartTimefs3 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _docprofileVM.StartTimefs4 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _docprofileVM.StartTimefs5 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _docprofileVM.StartTimefs6 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _docprofileVM.StartTimefs7 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimefs7 = et1 + " " + item.EndSlot;
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
                    _docprofileVM.StartTimess1 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _docprofileVM.StartTimess2 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _docprofileVM.StartTimess3 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _docprofileVM.StartTimess4 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _docprofileVM.StartTimess5 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _docprofileVM.StartTimess6 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _docprofileVM.StartTimess7 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimess7 = et1 + " " + item.EndSlot;
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
                    _docprofileVM.StartTimets1 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _docprofileVM.StartTimets2 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _docprofileVM.StartTimets3 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _docprofileVM.StartTimets4 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _docprofileVM.StartTimets5 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _docprofileVM.StartTimets6 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _docprofileVM.StartTimets7 = st1 + " " + item.StartSlot;
                    _docprofileVM.EndTimets7 = et1 + " " + item.EndSlot;
                }


            }

            return View(_docprofileVM);
        }

        #endregion DoctorProfile
        public ActionResult Doctors()
        {
            return View();
        }

        #region ClinicProfile
        public ActionResult ClinicProfile()
        {
            ClinicProfileViewModel _clinicprofileVM = new ClinicProfileViewModel();
            Session["ClinicProfileID"] = 2;
            int ClinicProfileID = Convert.ToInt32(Session["ClinicProfileID"]);

            List<DoctorClinicInformation> _ObClinicInfo = docprofile.GetDocClinicInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N").ToList();
            List<UserLoginSpeciality> _ObclinicLoginSpecialityInfo = docprofile.GetDocLoginSpeciality().Where(x => x.LoginId == ClinicProfileID).ToList();
            List<Media> _obclinicMediaInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N" && x.UploadType != "video/mp4").ToList();
            List<Media> _obclinicVideoInfo = docprofile.GetDocMediaInfo().Where(x => x.LoginId == ClinicProfileID && x.InactiveFlag == "N" && x.UploadType == "video/mp4").ToList();
            List<ClinicTimeInformation> GetclinicTimeFirst = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.FirstSetting == "1st seating Timing").ToList();
            List<ClinicTimeInformation> GetclinicTimeSecond = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.FirstSetting == "2nd seating Timing").ToList();
            List<ClinicTimeInformation> GetclinicTimeThird = docprofile.GetAllClinicTime().Where(x => x.LoginId == ClinicProfileID && x.FirstSetting == "3rd seating Timing").ToList();

            foreach (var item in _ObClinicInfo)
            {
                _clinicprofileVM.ClinicName = item.ClinicName;
                _clinicprofileVM.ClinicAddress = item.ClinicAddress;
                _clinicprofileVM.AboutClinic = item.AboutClinic;
                _clinicprofileVM.ClinicFees = item.ClinicFees;
                _clinicprofileVM.ClinicPhoneNo = item.ClinicPhoneNo;
                string strservice = item.ClinicServices;
                string[] strspli = strservice.Split(',');
                List<GetService> getservice = new List<GetService>();
                foreach (var a in strspli)
                {
                    var s = new GetService();
                    s.services = a;
                    getservice.Add(s);

                }
                _clinicprofileVM.getservices = getservice;

                //Profile Photo
                _clinicprofileVM.FolderFilePath = item.FolderFilePath;
            }

            foreach (var item in _ObclinicLoginSpecialityInfo)
            {
                List<Speciality> _ObclinicSpecialityInfo = docprofile.GetDocSpeciality().Where(x => x.SpecialityID == item.SpecialityID).ToList();
                foreach (var item1 in _ObclinicSpecialityInfo)
                {
                    _clinicprofileVM.SpecialityName = item1.SpecialityName;
                }
            }

            // Clinic Photos from media
            _clinicprofileVM.getclinicmedias = _obclinicMediaInfo;

            //clinic Videos from media
            _clinicprofileVM.getclinicvideos = _obclinicVideoInfo;

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
                    _clinicprofileVM.StartTimefs1 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _clinicprofileVM.StartTimefs2 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _clinicprofileVM.StartTimefs3 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _clinicprofileVM.StartTimefs4 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _clinicprofileVM.StartTimefs5 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _clinicprofileVM.StartTimefs6 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _clinicprofileVM.StartTimefs7 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimefs7 = et1 + " " + item.EndSlot;
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
                    _clinicprofileVM.StartTimess1 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _clinicprofileVM.StartTimess2 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _clinicprofileVM.StartTimess3 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _clinicprofileVM.StartTimess4 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _clinicprofileVM.StartTimess5 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _clinicprofileVM.StartTimess6 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _clinicprofileVM.StartTimess7 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimess7 = et1 + " " + item.EndSlot;
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
                    _clinicprofileVM.StartTimets1 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    _clinicprofileVM.StartTimets2 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    _clinicprofileVM.StartTimets3 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    _clinicprofileVM.StartTimets4 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    _clinicprofileVM.StartTimets5 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    _clinicprofileVM.StartTimets6 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    _clinicprofileVM.StartTimets7 = st1 + " " + item.StartSlot;
                    _clinicprofileVM.EndTimets7 = et1 + " " + item.EndSlot;
                }


            }

            return View(_clinicprofileVM);
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