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

namespace MCMD.Web.Controllers.Doctor
{
    public class DocPersonalInfoController : Controller
    {

        // GET: DoctorInfo
        public ApplicationDbContext db = new ApplicationDbContext();
        private IDoctorPersonalInfoRepository doctorPersonalInfoRepository;

        public DocPersonalInfoController(IDoctorPersonalInfoRepository _doctorPersonalInfoRepository)
        {
            this.doctorPersonalInfoRepository = _doctorPersonalInfoRepository;
        }
        // GET: DocPersonaliInfo
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            ViewData["PageRole"] = 1;

            //    Session["Doctor"] = 3;
            //This session for Edit doctor
            int editDocInputs = (Session["Doctor"] != null) ? (Convert.ToInt32(Session["Doctor"])) : 0;


            //Session["EditMembership"] = null;
            DoctorPersonalInfoViewModel _doctorVM = new DoctorPersonalInfoViewModel();
            _doctorVM.SpecialityList = doctorPersonalInfoRepository.GetSpecialitys().ToList();
            _doctorVM.UserLogins = doctorPersonalInfoRepository.GetUsers().ToList();



            //This is for edit doctorpersonalinformation populated
            if (editDocInputs != 0)
            {
                List<UserLogin> _NewDoctorloginInfo = doctorPersonalInfoRepository.GetUsers().Where(x => x.LoginId == editDocInputs).ToList();
                List<DoctorPersonalInformation> _NewDoctorinfo = doctorPersonalInfoRepository.GetDocInfo().Where(x => x.LoginId == editDocInputs).ToList();
                List<UserLoginSpeciality> _NewDocSpeciality = doctorPersonalInfoRepository.GetUserSpeciality().Where(x => x.LoginId == editDocInputs).ToList();
                foreach (var item in _NewDoctorloginInfo)
                {
                    _doctorVM.FirstName = item.FirstName;
                    _doctorVM.LastName = item.LastName;
                    _doctorVM.EmailID = item.EmailID;
                    _doctorVM.UserPhone = item.UserPhone;
                }
                foreach (var item in _NewDoctorinfo)
                {
                    _doctorVM.MiddleName = item.MiddleName;
                    _doctorVM.Qualification = item.Qualification;
                    _doctorVM.Qualification1 = item.Qualification1;
                    _doctorVM.RegistrationNo = item.RegistrationNo;
                    _doctorVM.Affiliation = item.Affiliation;
                    _doctorVM.AboutMe = item.AboutMe;
                    _doctorVM.ExperienceInYear = item.ExperienceInYear;
                    _doctorVM.ExperienceInMonth = item.ExperienceInMonth;
                }
                foreach (var item in _NewDocSpeciality)
                {
                    _doctorVM.SpecialityID = item.SpecialityID;

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

                if (ModelState.IsValid)
                {

                    _doctorPersonalInfoVM.LoginId = Convert.ToInt32(Session["Doctor"]);


                    var existingUser = db.DoctorsPersonals.FirstOrDefault(u => u.LoginId == _doctorPersonalInfoVM.LoginId);
                    //Insert the remain data in DoctorPersonalInformation
                    var newDoctor = new DoctorPersonalInformation();
                    newDoctor.LoginId = Convert.ToInt32(Session["Doctor"]);
                    if (ReferenceEquals(existingUser, null))
                    {


                        newDoctor.MiddleName = _doctorPersonalInfoVM.MiddleName;
                        newDoctor.Qualification = _doctorPersonalInfoVM.Qualification;
                        newDoctor.Qualification1 = _doctorPersonalInfoVM.Qualification1;
                        newDoctor.RegistrationNo =_doctorPersonalInfoVM.RegistrationNo;
                        newDoctor.Affiliation = _doctorPersonalInfoVM.Affiliation;
                        newDoctor.AboutMe = _doctorPersonalInfoVM.AboutMe;
                        newDoctor.ExperienceInYear = _doctorPersonalInfoVM.ExperienceInYear;
                        newDoctor.ExperienceInMonth = _doctorPersonalInfoVM.ExperienceInMonth;
                        newDoctor.InactiveFlag = "N";
                        newDoctor.CreatedByID = 1;
                        newDoctor.CreatedDate = DateTime.Now;
                        newDoctor.ModifiedByID = 1;
                        newDoctor.ModifiedDate = DateTime.Now;

                        //insert if not exit
                        doctorPersonalInfoRepository.InsertDoctor(newDoctor);
                        doctorPersonalInfoRepository.Save();

                    }
                    else
                    {

                        existingUser.MiddleName = _doctorPersonalInfoVM.MiddleName;
                        existingUser.Qualification = _doctorPersonalInfoVM.Qualification;
                        existingUser.Qualification1 = _doctorPersonalInfoVM.Qualification1;
                        existingUser.RegistrationNo =_doctorPersonalInfoVM.RegistrationNo;
                        existingUser.Affiliation = _doctorPersonalInfoVM.Affiliation;
                        existingUser.AboutMe = _doctorPersonalInfoVM.AboutMe;
                        existingUser.ExperienceInYear = _doctorPersonalInfoVM.ExperienceInYear;
                        existingUser.ExperienceInMonth = _doctorPersonalInfoVM.ExperienceInMonth;
                        existingUser.InactiveFlag = "N";
                        existingUser.CreatedByID = 1;
                        existingUser.CreatedDate = DateTime.Now;
                        existingUser.ModifiedByID = 1;
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.LoginId = Convert.ToInt32(Session["Doctor"]);

                        //Update if already exit
                        doctorPersonalInfoRepository.UpdateDoctorPersonalInfo(existingUser);
                        doctorPersonalInfoRepository.Save();


                    }
                    //find the data in table using loginId
                    var existUserlogin = db.UserLogins.FirstOrDefault(u => u.LoginId == newDoctor.LoginId);
                   
                    existUserlogin.FirstName = _doctorPersonalInfoVM.FirstName;
                    existUserlogin.LastName = _doctorPersonalInfoVM.LastName;
                    existUserlogin.EmailID = _doctorPersonalInfoVM.EmailID;
                    existUserlogin.UserPhone = _doctorPersonalInfoVM.UserPhone;

                    doctorPersonalInfoRepository.UpdateDocUserLogin(existUserlogin);
                    doctorPersonalInfoRepository.Save();


                    //find the data in table using loginId
                    var ExistUserLogSpeciality = db.UserLoginSpecialitys.FirstOrDefault(u => u.LoginId == newDoctor.LoginId);
                                 
                    ExistUserLogSpeciality.SpecialityID = _doctorPersonalInfoVM.SpecialityID;

                    doctorPersonalInfoRepository.UpdateDocSpeciality(ExistUserLogSpeciality);
                    doctorPersonalInfoRepository.Save();



                    @TempData["SuccessMessage"] = "Succsessfully save data";
                }
            }
            catch (Exception)
            {
                // ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                @TempData["Message"] = "Unable to save changes. Try again, and if the problem persists contact your system administrator.";
            }


            return RedirectToAction("Create");

        }



    }
}