using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using MCMD.ViewModel.Administration;


namespace MCMD.Web.Controllers.Administration
{
    public class DoctorInfoController : Controller
    {
        // GET: DoctorInfo
        public ApplicationDbContext db = new ApplicationDbContext();
        private IDoctorPersonalInfoRepository doctorPersonalInfoRepository;

        public DoctorInfoController(IDoctorPersonalInfoRepository _doctorPersonalInfoRepository)
        {
            this.doctorPersonalInfoRepository = _doctorPersonalInfoRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {

            ViewData["PageRole"] = 1;

            int editInputs = (Session["EditMembership"] != null) ? (Convert.ToInt32(Session["EditMembership"])) : 1;
            //Session["EditMembership"] = null;
            DoctorPersonalInfoViewModel _doctorVM = new DoctorPersonalInfoViewModel();

            _doctorVM.SpecialityList = doctorPersonalInfoRepository.GetSpecialitys().ToList();
            _doctorVM.Speciality = doctorPersonalInfoRepository.GetUserSpeciality().ToList().ToString();
            _doctorVM.doctorPersonalInfo = doctorPersonalInfoRepository.GetDoctors().ToList();

            if (editInputs != 0)
            {
                //List<MCMDMembership> _NewMembership = membershipRepository.GetMembers().Where(x => x.MembershipId == editInputs).ToList();
                List<UserLogin> _NewDoctor = doctorPersonalInfoRepository.GetUsers().Where(x => x.LoginId == editInputs).ToList();
                List<UserLoginSpeciality> _NewSpeciality = doctorPersonalInfoRepository.GetUserSpeciality().Where(x => x.LoginSpecialityId == editInputs).ToList();
                foreach (var item in _NewDoctor)
                {
                    _doctorVM.FirstName = item.FirstName;
                    _doctorVM.LastName = item.LastName;
                    _doctorVM.EmailId = item.EmailID;
                    _doctorVM.PersonalPhoneNo = item.UserPhone;
                    //_doctorVM.Speciality=item.


                    //        _memberShipVM.Durations = Convert.ToInt32(item.Durations);
                    //        _memberShipVM.Renaval = Convert.ToInt32(item.AutoRenaval);
                }
                foreach (var item in _NewSpeciality)
                {
                    _doctorVM.SpecialityID= Convert.ToInt32(item.SpecialityID);

                }

            }

            return View(_doctorVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorPersonalInfoViewModel _doctorPersonalInfoVM)
        {

            try
            {
                //  ModelState.Clear();  // Cleare Model state
                if (ModelState.IsValid)
                {


                    var newDoctor = new DoctorPersonalInformation();

                    newDoctor.FirstName = _doctorPersonalInfoVM.FirstName;
                    newDoctor.MiddleName = _doctorPersonalInfoVM.MiddleName;
                    newDoctor.LastName = _doctorPersonalInfoVM.LastName;
                    newDoctor.Qualification = _doctorPersonalInfoVM.Qualification;
                    newDoctor.Speciality = _doctorPersonalInfoVM.SpecialityID;//"1";
                    newDoctor.PersonalPhoneNo = _doctorPersonalInfoVM.PersonalPhoneNo; //457896237;
                    newDoctor.EmailId = _doctorPersonalInfoVM.EmailId;
                    newDoctor.RegistrationNo = Convert.ToInt32(_doctorPersonalInfoVM.RegistrationNo);
                    newDoctor.Affiliation = _doctorPersonalInfoVM.Affiliation;
                    newDoctor.AboutMe = _doctorPersonalInfoVM.AboutMe;
                    newDoctor.AboutExperience = _doctorPersonalInfoVM.AboutExperience;
                    newDoctor.InactiveFlag = "N";
                    newDoctor.CreatedByID = 1;
                    newDoctor.CreatedDate = DateTime.Now;
                    newDoctor.ModifiedByID = 1;
                    newDoctor.ModifiedDate = DateTime.Now;

                    doctorPersonalInfoRepository.InsertDoctor(newDoctor);
                    doctorPersonalInfoRepository.Save();
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