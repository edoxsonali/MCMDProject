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
namespace MCMD.Web.Controllers.Doctor
{
    public class DocClinicInfoController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        private IDoctorClinicInformation doctorClinicRepository;

        public DocClinicInfoController(IDoctorClinicInformation _doctorClinicRepository)
        {
            this.doctorClinicRepository = _doctorClinicRepository;
        }
        // GET: DocClinicInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {

            int Id = (Convert.ToInt32(Session["Doctor"]));

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
                //  ModelState.Clear();  // Cleare Model state
                if (ModelState.IsValid)
                {
                    _doctorClinicVM.LoginId = Convert.ToInt32(Session["Doctor"]);
                    var existingUser = db.DoctorsClinicInfos.FirstOrDefault(u => u.LoginId == _doctorClinicVM.LoginId);

                    int Id = (Convert.ToInt32(Session["Doctor"]));


                    if (ReferenceEquals(existingUser, null))
                    {
                        var newClinic = new DoctorClinicInformation();
                        newClinic.ClinicName = _doctorClinicVM.ClinicName;
                        newClinic.ClinicAddress = _doctorClinicVM.ClinicAddress;
                        newClinic.ClinicPhoneNo = _doctorClinicVM.ClinicPhoneNo;
                        newClinic.ClinicFees = _doctorClinicVM.ClinicFees;
                        newClinic.Country = _doctorClinicVM.Country;
                        newClinic.State = _doctorClinicVM.State;
                        newClinic.City = _doctorClinicVM.City;
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
                        existingUser.Country = _doctorClinicVM.Country;
                        existingUser.State = _doctorClinicVM.State;
                        existingUser.City = _doctorClinicVM.City;
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
                @TempData["Message"] = "Unable to save changes. Try again, and if the problem persists contact your system administrator.";
            }
            return RedirectToAction("Create");
        }
    }
}