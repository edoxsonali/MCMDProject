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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

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
                    doctorClinicVM.Country = item.Country;
                    doctorClinicVM.State = item.State;
                    doctorClinicVM.City = item.City;
                    doctorClinicVM.ClinicServices = item.ClinicServices;
                    doctorClinicVM.AwardsAndRecognization = item.AwardsAndRecognization;
                    doctorClinicVM.AboutClinic = item.AboutClinic;
                    doctorClinicVM.ZipCode = item.ZipCode;
                }



            }
            return View(doctorClinicVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorClinicInformationViewModel _doctorClinicVM)
        {
            try
            {
                ModelState.Clear();  // Cleare Model state
                if (ModelState.IsValid)
                {
                    int Id = (Convert.ToInt32(Session["EditDoctor"]));

                    var newClinic = new DoctorClinicInformation();


                    //Enumeration.GetAll<MCMD.ViewModel.Administration.DoctorClinicInformationViewModel.ClinicHours>();


                    newClinic.ClinicName = _doctorClinicVM.ClinicName;
                    newClinic.ClinicAddress = _doctorClinicVM.ClinicAddress;
                    newClinic.ClinicPhoneNo = _doctorClinicVM.ClinicPhoneNo;
                    newClinic.ClinicFees = _doctorClinicVM.ClinicFees;
                    //newClinic.ClinicTimeFrom = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicTimeFrom).Substring(0, 8));// TimeSpan.Parse(Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8));
                    //newClinic.ClinicTimeTo = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicTimeTo).Substring(0, 8));
                    //newClinic.ClinicLunchbreakFrom = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicLunchbreakFrom).Substring(0, 8));
                    //newClinic.ClinicLunchbreakTo = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicLunchbreakTo).Substring(0, 8));
                    newClinic.Country = _doctorClinicVM.Country;
                    newClinic.State = _doctorClinicVM.State;
                    newClinic.City = _doctorClinicVM.City;
                    newClinic.ZipCode = _doctorClinicVM.ZipCode;
                    newClinic.ClinicServices = _doctorClinicVM.ClinicServices;
                    newClinic.AwardsAndRecognization = _doctorClinicVM.AwardsAndRecognization;
                    newClinic.AboutClinic = _doctorClinicVM.AboutClinic;
                    newClinic.InactiveFlag = "N";
                    newClinic.CreatedByID = 1;
                    newClinic.CreatedDate = DateTime.Now;
                    newClinic.ModifiedByID = 1;
                    newClinic.ModifiedDate = DateTime.Now;
                    newClinic.LoginId = Id;// for now we add 1 later we change

                    doctorClinicRepository.InsertClinic(newClinic);
                    doctorClinicRepository.Save();
                    ViewBag.Message = "Succsessfully added..";
                    @TempData["Message"] = "Succsessfully save data";
                }


            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Create");
        }


        public ActionResult ViewClinicInfo()
        {
            ClinicDetailsViewModel clinicdetail = new ClinicDetailsViewModel();
            clinicdetail.clinicinfo = doctorClinicRepository.GetClinics().ToList();

            return View(clinicdetail);
        }

        public ActionResult ClinicTiming()
        {
            ClinicTimingViewModel docClinicTimeVM = new ClinicTimingViewModel();


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

            docClinicTimeVM.GetHoursList = new List<daysHours>() {
                new daysHours {daysHoursId="00",Hours="00"},
                new daysHours {daysHoursId="01",Hours="01"},
                new daysHours {daysHoursId="02",Hours="02"},
                new daysHours {daysHoursId="03",Hours="03"},
                new daysHours {daysHoursId="04",Hours="04"},
                new daysHours {daysHoursId="05",Hours="05"},
                new daysHours {daysHoursId="06",Hours="06"},
                new daysHours {daysHoursId="07",Hours="07"},
                new daysHours {daysHoursId="08",Hours="08"},
                new daysHours {daysHoursId="09",Hours="09"},
                new daysHours {daysHoursId="10",Hours="10"},
                new daysHours {daysHoursId="11",Hours="11"},
                new daysHours {daysHoursId="12",Hours="12"},

            };


            docClinicTimeVM.GetSecList = new List<daysec>() {
                new daysec {daySecId="00",Sec="00"},
                new daysec {daySecId="15",Sec="15"},
                new daysec {daySecId="30",Sec="30"},
                new daysec {daySecId="45",Sec="45"},

            };

            docClinicTimeVM.GetSlotList = new List<daySlot>() {
                new daySlot {daySlotId="AM",slot="AM"},
                new daySlot {daySlotId="PM",slot="PM"},
             

            };

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

                    foreach (var item in docClinicTime.GetCheckList)
                    {
                        if (item.DayChecked == true)
                        {

                            string CheckDay = item.Days;
                            var NewClinicTime = new MCMD.EntityModel.Administration.ClinicTimeInformation();

                            if (CheckDay.Equals("Monday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursMonStart + ":" + docClinicTime.SecMonStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotMonStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursMonEnd + ":" + docClinicTime.SecMonEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotMonEnd;

                            }

                            if (CheckDay.Equals("Tuesday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursTueStart + ":" + docClinicTime.SecTueStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotTueStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursTueEnd + ":" + docClinicTime.SecTueEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotTueEnd;
                            }
                            if (CheckDay.Equals("Wednesday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursWedStart + ":" + docClinicTime.SecWedStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotWedStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursWedEnd + ":" + docClinicTime.SecWedEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotWedEnd;
                            }
                            if (CheckDay.Equals("Thursday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursThuStart + ":" + docClinicTime.SecThuStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotThuStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursThuEnd + ":" + docClinicTime.SecThuEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotThuEnd;
                            }
                            if (CheckDay.Equals("Friday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursFriStart + ":" + docClinicTime.SecFriStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotFriStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursFriEnd + ":" + docClinicTime.SecFriEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotFriEnd;
                            }
                            if (CheckDay.Equals("Saturday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursSatStart + ":" + docClinicTime.SecSatStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotSatStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursSatEnd + ":" + docClinicTime.SecSatEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotSatEnd;
                            }
                            if (CheckDay.Equals("Sunday"))
                            {
                                NewClinicTime.StartTime = TimeSpan.Parse(docClinicTime.HoursSunStart + ":" + docClinicTime.SecSunStart);
                                NewClinicTime.StartSlot = docClinicTime.SlotSunStart;
                                NewClinicTime.EndTime = TimeSpan.Parse(docClinicTime.HoursSunEnd + ":" + docClinicTime.SecSunEnd);
                                NewClinicTime.EndSlot = docClinicTime.SlotSunEnd;
                            }


                            NewClinicTime.LoginId = 1;//add session here
                            NewClinicTime.Day = item.Days;
                            NewClinicTime.FirstSetting = true;
                            NewClinicTime.IsWorkingDay = item.DayChecked;
                            NewClinicTime.CreatedById = 1;
                            NewClinicTime.CreatedOnDate = DateTime.Now;
                            NewClinicTime.ModifiedById = 1;
                            NewClinicTime.ModifiedOnDate = DateTime.Now;


                            doctorClinicRepository.InsertClinicTime(NewClinicTime);
                            doctorClinicRepository.Save();



                        }
                    }





                }

            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("ClinicTiming");
        }
        public static class Enumeration
        {

            public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
            {
                var enumerationType = typeof(TEnum);

                if (!enumerationType.IsEnum)
                    throw new ArgumentException("Enumeration type is expected.");

                var dictionary = new Dictionary<int, string>();

                foreach (int value in Enum.GetValues(enumerationType))
                {
                    var name = Enum.GetName(enumerationType, value);
                    dictionary.Add(value, name);
                }

                return dictionary;
            }
        }

    }
}