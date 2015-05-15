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

                    //newClinic.ClinicName = _doctorClinicVM.DoctorClinicInformations.ClinicName;
                    //newClinic.ClinicAddress = _doctorClinicVM.DoctorClinicInformations.ClinicAddress;
                    //newClinic.ClinicPhoneNo = _doctorClinicVM.DoctorClinicInformations.ClinicPhoneNo;
                    //newClinic.ClinicFees = _doctorClinicVM.DoctorClinicInformations.ClinicFees;
                    //newClinic.ClinicTimeFrom = _doctorClinicVM.time;
                    //newClinic.ClinicTimeTo = _doctorClinicVM.Sec;
                    //newClinic.Country = _doctorClinicVM.DoctorClinicInformations.Country;
                    //newClinic.State = _doctorClinicVM.DoctorClinicInformations.State;
                    //newClinic.City = _doctorClinicVM.DoctorClinicInformations.City;
                    //newClinic.ZipCode = _doctorClinicVM.DoctorClinicInformations.ZipCode;
                    //newClinic.ClinicServices = _doctorClinicVM.DoctorClinicInformations.ClinicServices;
                    //newClinic.AwardsAndRecognization = _doctorClinicVM.DoctorClinicInformations.AwardsAndRecognization;
                    //newClinic.AboutClinic = _doctorClinicVM.DoctorClinicInformations.AboutClinic;
                    //newClinic.InactiveFlag = "N";
                    //newClinic.CreatedByID = 1;
                    //newClinic.CreatedDate = DateTime.Now;
                    //newClinic.ModifiedByID = 1;
                    //newClinic.ModifiedDate = DateTime.Now;
                    //newClinic.LoginId = 1;// for now we add 1 later we change

                    //doctorClinicRepository.InsertClinic(newClinic);
                    //doctorClinicRepository.Save();
                    //ViewBag.Message = "Succsessfully added..";
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