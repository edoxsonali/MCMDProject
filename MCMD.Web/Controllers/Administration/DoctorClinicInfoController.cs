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
            return View();
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