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
            //Create object of View Model Class
            DoctorClinicInformationViewModel doctorClinicVM = new DoctorClinicInformationViewModel();

            //Convert the into  the List
            doctorClinicVM.countyList = doctorClinicRepository.GetCountrys().ToList();
            doctorClinicVM.stateList = doctorClinicRepository.GetStates().ToList();
            doctorClinicVM.cityList = doctorClinicRepository.GetCities().ToList();
            //doctorClinicVM.doctorClinicList = doctorClinicRepository.GetClinics().ToList();

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

                  
                    var newClinic = new DoctorClinicInformation();


                     Enumeration.GetAll<MCMD.ViewModel.Administration.DoctorClinicInformationViewModel.ClinicHours>();
                   

                    newClinic.ClinicName = _doctorClinicVM.DoctorClinicInformations.ClinicName;
                    newClinic.ClinicAddress = _doctorClinicVM.DoctorClinicInformations.ClinicAddress;
                    newClinic.ClinicPhoneNo = _doctorClinicVM.DoctorClinicInformations.ClinicPhoneNo;
                    newClinic.ClinicFees = _doctorClinicVM.DoctorClinicInformations.ClinicFees;
                    newClinic.ClinicTimeFrom = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicTimeFrom).Substring(0, 8));// TimeSpan.Parse(Convert.ToString(DateTime.Now.TimeOfDay).Substring(0, 8));
                    newClinic.ClinicTimeTo = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicTimeTo).Substring(0, 8));
                    newClinic.ClinicLunchbreakFrom = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicLunchbreakFrom).Substring(0, 8));
                    newClinic.ClinicLunchbreakTo = TimeSpan.Parse(Convert.ToString(_doctorClinicVM.DoctorClinicInformations.ClinicLunchbreakTo).Substring(0, 8));
                    newClinic.Country = _doctorClinicVM.DoctorClinicInformations.Country;
                    newClinic.State = _doctorClinicVM.DoctorClinicInformations.State;
                    newClinic.City = _doctorClinicVM.DoctorClinicInformations.City;
                    newClinic.ZipCode = _doctorClinicVM.DoctorClinicInformations.ZipCode;
                    newClinic.ClinicServices = _doctorClinicVM.DoctorClinicInformations.ClinicServices;
                    newClinic.AwardsAndRecognization = _doctorClinicVM.DoctorClinicInformations.AwardsAndRecognization;
                    newClinic.AboutClinic = _doctorClinicVM.DoctorClinicInformations.AboutClinic;
                    newClinic.InactiveFlag = "N";
                    newClinic.CreatedByID = 1;
                    newClinic.CreatedDate = DateTime.Now;
                    newClinic.ModifiedByID = 1;
                    newClinic.ModifiedDate = DateTime.Now;
                    newClinic.LoginId = 1;// for now we add 1 later we change

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