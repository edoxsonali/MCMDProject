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

                    newClinic.ClinicName = _doctorClinicVM.ClinicName;
                    newClinic.ClinicAddress = _doctorClinicVM.ClinicAddress;
                    newClinic.ClinicPhoneNo = _doctorClinicVM.ClinicPhoneNo;
                    newClinic.ClinicFees = _doctorClinicVM.ClinicFees;
                    newClinic.ClinicTimeFrom = _doctorClinicVM.ClinicTimeFrom;
                    newClinic.ClinicTimeTo = _doctorClinicVM.ClinicTimeTo;
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

                    doctorClinicRepository.InsertClinic(newClinic);
                    doctorClinicRepository.Save();
                    ViewBag.Message = "Succsessfully added..";
                }


            }
            catch (Exception)
            {
                //ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Create");
        }
    }
}