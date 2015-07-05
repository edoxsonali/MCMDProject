using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using MCMD.ViewModel.Administration;
using MCMD.EntityModel.Administration;
using System.IO;


namespace MCMD.Web.Controllers.Administration
{
    public class DoctorClinicInfoController : Controller
    {      
        // GET: DoctorClinicInfo 
        public ApplicationDbContext db = new ApplicationDbContext();
        private IDoctorClinicInformation doctorClinicRepository;

        public DoctorClinicInfoController(IDoctorClinicInformation _doctorClinicRepository)
        {
            this.doctorClinicRepository = _doctorClinicRepository;
        }
       
        #region Create/Update Doctor Clinic Info
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            int Id = (Convert.ToInt32(Session["EditDoctor"]));

            Session["EditDoctor"] = Id;
            //Create object of View Model Class
            DoctorClinicInformationViewModel doctorClinicVM = new DoctorClinicInformationViewModel();

            //Convert the into  the List
            //Convert the into  the List
            doctorClinicVM.countyList = doctorClinicRepository.GetCountrys().ToList();
            doctorClinicVM.stateList = doctorClinicRepository.GetStates().ToList();
            doctorClinicVM.cityList = doctorClinicRepository.GetCities().ToList();
            doctorClinicVM.UserLogins = doctorClinicRepository.GetUsers().ToList();

            if (Id != 0)
            {
                List<DoctorClinicInformation> NewClinic = doctorClinicRepository.GetClinic().Where(x => x.LoginId == Id).ToList();
                //List<GetViewCliniInfo> _NewDoctor = doctorClinicRepository.GetClinics().Where(x => x.ClinicInfoId == Id).ToList();

                foreach (var item in NewClinic)
                {

                    doctorClinicVM.ClinicName = item.ClinicName;
                    doctorClinicVM.ClinicAddress = item.ClinicAddress;
                    doctorClinicVM.ClinicPhoneNo = item.ClinicPhoneNo;
                    doctorClinicVM.ClinicFees = item.ClinicFees;
                    doctorClinicVM.CountryId = item.Country;
                    doctorClinicVM.StateId = item.State;
                    doctorClinicVM.CityId = item.City;
                    doctorClinicVM.ClinicServices = item.ClinicServices;
                    doctorClinicVM.AwardsAndRecognization = item.AwardsAndRecognization;
                    doctorClinicVM.AboutClinic = item.AboutClinic;
                    doctorClinicVM.ZipCode = item.ZipCode;
                    doctorClinicVM.FolderFilePath = item.FolderFilePath;
                }


            }
            return View(doctorClinicVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorClinicInformationViewModel _doctorClinicVM, HttpPostedFileBase file)
        {
            try
            {
                ModelState.Clear();  // Cleare Model state
                if (ModelState.IsValid)
                {
                    int Id = (Convert.ToInt32(Session["EditDoctor"]));
                    _doctorClinicVM.LoginId = Convert.ToInt32(Session["EditDoctor"]);
                    var existingUser = db.DoctorsClinicInfos.FirstOrDefault(u => u.LoginId == _doctorClinicVM.LoginId);



                    if (ReferenceEquals(existingUser, null))
                    {
                        var newClinic = new DoctorClinicInformation();
                        newClinic.ClinicName = _doctorClinicVM.ClinicName;
                        newClinic.ClinicAddress = _doctorClinicVM.ClinicAddress;
                        newClinic.ClinicPhoneNo = _doctorClinicVM.ClinicPhoneNo;
                        newClinic.ClinicFees = _doctorClinicVM.ClinicFees;
                        newClinic.Country = _doctorClinicVM.CountryId;
                        newClinic.State = _doctorClinicVM.StateId;
                        newClinic.City = _doctorClinicVM.CityId;
                        newClinic.ZipCode = _doctorClinicVM.ZipCode;
                        newClinic.ClinicServices = _doctorClinicVM.ClinicServices;
                        newClinic.AwardsAndRecognization = _doctorClinicVM.AwardsAndRecognization;
                        newClinic.AboutClinic = _doctorClinicVM.AboutClinic;
                        newClinic.InactiveFlag = "N";
                        newClinic.CreatedByID = 1;// for now we add 1 later we change
                        newClinic.CreatedDate = DateTime.Now;
                        newClinic.ModifiedByID = 1;// for now we add 1 later we change
                        newClinic.ModifiedDate = DateTime.Now;
                        newClinic.LoginId = Id;

                        if (file != null)
                        {
                            string[] formats = new string[] { "image/jpeg", "image/png", "image/gif", "image/Bmp" };
                            int CheckImgType = Convert.ToInt32(formats.Contains(file.ContentType));
                            if (CheckImgType != 0)
                            {
                                string Imgpath = "~/Media/" + file.FileName;
                                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                                file.SaveAs(path);

                                newClinic.FolderFilePath = Imgpath;
                                newClinic.UploadType = file.ContentType;

                            }
                        }

                        doctorClinicRepository.InsertClinic(newClinic);
                        doctorClinicRepository.Save();
                        @TempData["SuccessMessage"] = "Succsessfully save data";
                    }
                    else
                    {

                        existingUser.ClinicName = _doctorClinicVM.ClinicName;
                        existingUser.ClinicAddress = _doctorClinicVM.ClinicAddress;
                        existingUser.ClinicPhoneNo = _doctorClinicVM.ClinicPhoneNo;
                        existingUser.ClinicFees = _doctorClinicVM.ClinicFees;
                        existingUser.Country = _doctorClinicVM.CountryId;
                        existingUser.State = _doctorClinicVM.StateId;
                        existingUser.City = _doctorClinicVM.CityId;
                        existingUser.ZipCode = _doctorClinicVM.ZipCode;
                        existingUser.ClinicServices = _doctorClinicVM.ClinicServices;
                        existingUser.AwardsAndRecognization = _doctorClinicVM.AwardsAndRecognization;
                        existingUser.AboutClinic = _doctorClinicVM.AboutClinic;
                        existingUser.InactiveFlag = "N";
                        existingUser.CreatedByID = 1;// for now we add 1 later we change
                        existingUser.CreatedDate = DateTime.Now;
                        existingUser.ModifiedByID = 1;// for now we add 1 later we change
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.LoginId = Id;

                        if (file != null)
                        {
                            string[] formats = new string[] { "image/jpeg", "image/png", "image/gif", "image/Bmp" };
                            int CheckImgType = Convert.ToInt32(formats.Contains(file.ContentType));
                            if (CheckImgType != 0)
                            {
                                string Imgpath = "~/Media/" + file.FileName;
                                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                                file.SaveAs(path);

                                existingUser.FolderFilePath = Imgpath;
                                existingUser.UploadType = file.ContentType;

                            }
                        }

                        //Update if already exit 
                        doctorClinicRepository.UpdateClinic(existingUser);
                        doctorClinicRepository.Save();
                        @TempData["SuccessMessage"] = "Succsessfully Update data";

                    }
                }


            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
              //  string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                @TempData["Message"] = "Unable to save changes";
            }
            return RedirectToAction("ClinicTiming", "DoctorClinicInfo");
        }
        #endregion

          #region Get City list by state id
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCityByStateId(string StateIDID)
        {
            var varCityResult = (IEnumerable<City>)null;
            if (!String.IsNullOrEmpty(StateIDID))
            {

                int id = Convert.ToInt32(StateIDID);
                varCityResult = (from c in db.cities
                              join s in db.states on c.StateId equals s.StateId
                              where s.StateId == id
                              select (c));
            }
            else
            {
                //varCityResult = db.cities.Where(x => x.InactiveFlag == "N")
                //  .OrderBy(x => x.DispensaryName).ToList();
                varCityResult = db.cities.ToList();

            }

            return Json(new SelectList(varCityResult.ToArray(), "CityId", "CityName"), JsonRequestBehavior.AllowGet);
            //return Json(new { redirectUrl = Url.Action("Create", "DoctorClinicInfo", new { StateIDID }), isRedirect = true, JsonRequestBehavior.AllowGet });
        }
          #endregion

        //  #region add services
        //[HttpPost]
        //public ActionResult addservices(string[] strarry)
        //{
        //    var newClinic = new DoctorClinicInformation();
        //    foreach (var item in strarry)
        //    {
        //        newClinic.ClinicServices = item;
        //    }
        //    return Json("strarry", JsonRequestBehavior.AllowGet);
        //}

        //  #endregion

        #region View Clinic Info
        public ActionResult ViewClinicInfo()
        {
            ClinicDetailsViewModel clinicdetail = new ClinicDetailsViewModel();
            clinicdetail.clinicinfo = doctorClinicRepository.GetClinics().ToList();

            return View(clinicdetail);
        }
        #endregion

        #region set Clinic Timing

        public ActionResult ClinicTiming()
        {
            @TempData["Name"] = Session["Name"];
            ClinicTimingViewModel docClinicTimeVM = new ClinicTimingViewModel();
             int Id = (Convert.ToInt32(Session["EditDoctor"]));

            docClinicTimeVM.GetCheckList = new List<DaysCheckList>() {
                new DaysCheckList {DayscheckId=1,Days="Monday", DayChecked=true},
                new DaysCheckList {DayscheckId=2,Days="Tuesday",DayChecked=true },
                new DaysCheckList {DayscheckId=3,Days="Wednesday" ,DayChecked=true },
                new DaysCheckList {DayscheckId=4,Days="Thursday" ,DayChecked=true },
                new DaysCheckList {DayscheckId=5,Days="Friday" ,DayChecked=true },
                new DaysCheckList {DayscheckId=6,Days="Saturday" ,DayChecked=true },
                new DaysCheckList {DayscheckId=7,Days="Sunday" ,DayChecked=true },
            };
            docClinicTimeVM.SelectedMember1 = docClinicTimeVM.GetCheckList.Select(x => x.DayscheckId).ToArray();

            docClinicTimeVM.getClinicTimie = doctorClinicRepository.GetAllClinicTime().ToList();

               List<ClinicTimeInformation>  GetclinicTimeFirst = doctorClinicRepository.GetAllClinicTime().Where(x => x.LoginId == Id && x.FirstSetting == "1st seating Timing").ToList();
               List<ClinicTimeInformation>  GetclinicTimeSecond = doctorClinicRepository.GetAllClinicTime().Where(x => x.LoginId == Id && x.FirstSetting == "2nd seating Timing").ToList();
               List<ClinicTimeInformation>  GetclinicTimeThird = doctorClinicRepository.GetAllClinicTime().Where(x => x.LoginId == Id && x.FirstSetting == "3rd seating Timing").ToList();
                
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
                if(item.Day=="Monday")
                {
                    docClinicTimeVM.StartTimefs1 = st1+" "+item.StartSlot;
                    docClinicTimeVM.EndTimefs1 = et1+" "+item.EndSlot;
                  }
                if (item.Day == "Tuesday")
                {
                    docClinicTimeVM.StartTimefs2 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    docClinicTimeVM.StartTimefs3 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    docClinicTimeVM.StartTimefs4 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    docClinicTimeVM.StartTimefs5 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    docClinicTimeVM.StartTimefs6 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    docClinicTimeVM.StartTimefs7 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimefs7 = et1 + " " + item.EndSlot;
                }
                   
           
            }
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
                    docClinicTimeVM.StartTimess1 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    docClinicTimeVM.StartTimess2 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    docClinicTimeVM.StartTimess3 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    docClinicTimeVM.StartTimess4 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    docClinicTimeVM.StartTimess5 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    docClinicTimeVM.StartTimess6 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    docClinicTimeVM.StartTimess7 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimess7 = et1 + " " + item.EndSlot;
                }


            }
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
                    docClinicTimeVM.StartTimets1 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets1 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Tuesday")
                {
                    docClinicTimeVM.StartTimets2 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets2 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Wednesday")
                {
                    docClinicTimeVM.StartTimets3 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets3 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Thursday")
                {
                    docClinicTimeVM.StartTimets4 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets4 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Friday")
                {
                    docClinicTimeVM.StartTimets5 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets5 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Saturday")
                {
                    docClinicTimeVM.StartTimets6 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets6 = et1 + " " + item.EndSlot;
                }
                if (item.Day == "Sunday")
                {
                    docClinicTimeVM.StartTimets7 = st1 + " " + item.StartSlot;
                    docClinicTimeVM.EndTimets7 = et1 + " " + item.EndSlot;
                }


            }
         
           
            return View(docClinicTimeVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClinicTiming(ClinicTimingViewModel docClinicTime)
        {
            ModelState.Clear();


            try
            {
                if (ModelState.IsValid)
                {
                    int Id = (Convert.ToInt32(Session["EditDoctor"]));
                    foreach (var item in docClinicTime.GetCheckList)
                    {
                        if (item.DayChecked == true)
                        {

                            string CheckDay = item.Days;
                            var NewClinicTime = new MCMD.EntityModel.Administration.ClinicTimeInformation();

                            if (CheckDay.Equals("Monday"))
                            {
                                //strat timing
                                DateTime timestartMon = DateTime.Parse(docClinicTime.StartTimeMon);
                                string newstartMon = timestartMon.ToString("hh:mm");
                                string newslotstartMon = timestartMon.ToString("tt");             
               
                                //End timing
                                DateTime timeendMon = DateTime.Parse(docClinicTime.EndtTimeMon);
                                string newsendMon = timeendMon.ToString("hh:mm");
                                string newsslotendMon = timeendMon.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartMon);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendMon);
                                NewClinicTime.StartSlot = newslotstartMon;
                                NewClinicTime.EndSlot = newsslotendMon;
                               


                            }

                            if (CheckDay.Equals("Tuesday"))
                            {
                                //strat timing
                                DateTime timestartTue = DateTime.Parse(docClinicTime.StartTimeTue);
                                string newstartTue = timestartTue.ToString("hh:mm");
                                string newslotstartTue = timestartTue.ToString("tt");

                                //End timing
                                DateTime timeendTue = DateTime.Parse(docClinicTime.EndtTimeTue);
                                string newsendTue = timeendTue.ToString("hh:mm");
                                string newsslotendTue = timeendTue.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartTue);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendTue);
                                NewClinicTime.StartSlot = newslotstartTue;
                                NewClinicTime.EndSlot = newsslotendTue;

                             
                            }
                            if (CheckDay.Equals("Wednesday"))
                            {
                                //strat timing
                                DateTime timestartWed = DateTime.Parse(docClinicTime.StartTimeWed);
                                string newstartWed = timestartWed.ToString("hh:mm");
                                string newslotstartWed = timestartWed.ToString("tt");

                                //End timing
                                DateTime timeendWed = DateTime.Parse(docClinicTime.EndtTimeWed);
                                string newsendWed = timeendWed.ToString("hh:mm");
                                string newsslotendWed = timeendWed.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartWed);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendWed);
                                NewClinicTime.StartSlot = newslotstartWed;
                                NewClinicTime.EndSlot = newsslotendWed;

                            }
                            if (CheckDay.Equals("Thursday"))
                            {
                                //strat timing
                                DateTime timestartThu = DateTime.Parse(docClinicTime.StartTimeThu);
                                string newstartThu = timestartThu.ToString("hh:mm");
                                string newslotstartThu = timestartThu.ToString("tt");

                                //End timing
                                DateTime timeendThu = DateTime.Parse(docClinicTime.EndtTimeThu);
                                string newsendThu = timeendThu.ToString("hh:mm");
                                string newsslotendThu = timeendThu.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartThu);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendThu);
                                NewClinicTime.StartSlot = newslotstartThu;
                                NewClinicTime.EndSlot = newsslotendThu;

                            }
                            if (CheckDay.Equals("Friday"))
                            {
                                //strat timing
                                DateTime timestartFri = DateTime.Parse(docClinicTime.StartTimeFri);
                                string newstartFri = timestartFri.ToString("hh:mm");
                                string newslotstartFri = timestartFri.ToString("tt");

                                //End timing
                                DateTime timeendFri = DateTime.Parse(docClinicTime.EndtTimeFri);
                                string newsendFri = timeendFri.ToString("hh:mm");
                                string newsslotendFri = timeendFri.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartFri);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendFri);
                                NewClinicTime.StartSlot = newslotstartFri;
                                NewClinicTime.EndSlot = newsslotendFri;

                            
                            }
                            if (CheckDay.Equals("Saturday"))
                            {
                                //strat timing
                                DateTime timestartSat = DateTime.Parse(docClinicTime.StartTimeSat);
                                string newstartSat = timestartSat.ToString("hh:mm");
                                string newslotstartSat = timestartSat.ToString("tt");

                                //End timing
                                DateTime timeendSat = DateTime.Parse(docClinicTime.EndtTimeSat);
                                string newsendSat = timeendSat.ToString("hh:mm");
                                string newsslotendSat = timeendSat.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartSat);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendSat);
                                NewClinicTime.StartSlot = newslotstartSat;
                                NewClinicTime.EndSlot = newsslotendSat;

                              
                            }
                            if (CheckDay.Equals("Sunday"))
                            {
                                //strat timing
                                DateTime timestartSun = DateTime.Parse(docClinicTime.StartTimeSun);
                                string newstartSun = timestartSun.ToString("hh:mm");
                                string newslotstartSun = timestartSun.ToString("tt");

                                //End timing
                                DateTime timeendSun = DateTime.Parse(docClinicTime.EndtTimeSun);
                                string newsendSun = timeendSun.ToString("hh:mm");
                                string newsslotendSun = timeendSun.ToString("tt");

                                NewClinicTime.StartTime = TimeSpan.Parse(newstartSun);
                                NewClinicTime.EndTime = TimeSpan.Parse(newsendSun);
                                NewClinicTime.StartSlot = newslotstartSun;
                                NewClinicTime.EndSlot = newsslotendSun;

                             
                            }


                            NewClinicTime.LoginId = Id;//add session here
                            NewClinicTime.Day = item.Days;
                            NewClinicTime.FirstSetting = docClinicTime.Setting;
                            NewClinicTime.IsWorkingDay = item.DayChecked;
                            NewClinicTime.CreatedById = 1;
                            NewClinicTime.CreatedOnDate = DateTime.Now;
                            NewClinicTime.ModifiedById = 1;
                            NewClinicTime.ModifiedOnDate = DateTime.Now;


                            doctorClinicRepository.InsertClinicTime(NewClinicTime);
                            doctorClinicRepository.Save();

                            @TempData["SuccessMessage"] = "Clinic Timing Save Successfully";

                        }
                    }

                }

            }
             catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                @TempData["Message"] = "Unable to save";
            }
            //return RedirectToAction("ClinicTiming");
            return View(docClinicTime);
        }
        #endregion
        

    }
}