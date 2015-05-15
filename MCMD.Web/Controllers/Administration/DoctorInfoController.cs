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
        public ActionResult UserEditDoctor(int Id)
        {
            Session["EditDoctor"] = Id;
            var varid = Id;        
            return Json(new { redirectUrl = Url.Action("Create", "DoctorInfo", new { varid }), isRedirect = true, JsonRequestBehavior.AllowGet });
                    
        }
        public ActionResult Create()
        {

            ViewData["PageRole"] = 1;
            //This session for autopopulat the field in login
           int editInputs = (Session["EditMembership"] != null) ? (Convert.ToInt32(Session["EditMembership"])) : 30;
          //  int editInputs = 0;
           
            //This session for Edit doctor
            int editDocInputs = (Session["EditDoctor"] != null) ? (Convert.ToInt32(Session["EditDoctor"])) : 1;
          //  int editDocInputs = 0;

            //Session["EditMembership"] = null;
            DoctorPersonalInfoViewModel _doctorVM = new DoctorPersonalInfoViewModel();
            _doctorVM.SpecialityList = doctorPersonalInfoRepository.GetSpecialitys().ToList();          
            _doctorVM.UserLogins = doctorPersonalInfoRepository.GetUsers().ToList();
               
            //This is for short login data populated
                if (editInputs != 0)
                {
                    List<UserLogin> _NewDoctor = doctorPersonalInfoRepository.GetUsers().Where(x => x.LoginId == editInputs).ToList();
                    List<UserLoginSpeciality> _NewSpeciality = doctorPersonalInfoRepository.GetUserSpeciality().Where(x => x.LoginSpecialityId == editInputs).ToList();
                    foreach (var item in _NewDoctor)
                    {
                        _doctorVM.FirstName = item.FirstName;
                        _doctorVM.LastName = item.LastName;
                        _doctorVM.EmailID = item.EmailID;
                        _doctorVM.UserPhone = item.UserPhone;

                    }
                    foreach (var item in _NewSpeciality)
                    {
                        _doctorVM.SpecialityID = item.SpecialityID;

                    }

                }
                
                //This is for edit doctorpersonalinformation populated
                if(editDocInputs!=0 )
                {
                    List<UserLogin> _NewDoctorloginInfo = doctorPersonalInfoRepository.GetUsers().Where(x => x.LoginId == editDocInputs).ToList();
                    List<DoctorPersonalInformation> _NewDoctorinfo = doctorPersonalInfoRepository.GetDocInfo().Where(x => x.LoginId == editDocInputs).ToList();
                    List<UserLoginSpeciality> _NewDocSpeciality = doctorPersonalInfoRepository.GetUserSpeciality().Where(x => x.LoginSpecialityId == editDocInputs).ToList();
                    foreach(var item in _NewDoctorloginInfo)
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
                        _doctorVM.RegistrationNo = item.RegistrationNo;
                        _doctorVM.Affiliation = item.Affiliation;
                        _doctorVM.AboutMe = item.AboutMe;
                        _doctorVM.AboutExperience = item.AboutExperience;
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

                    //Insert the remain data in DoctorPersonalInformation
                    var newDoctor = new DoctorPersonalInformation();
                    newDoctor.MiddleName = _doctorPersonalInfoVM.MiddleName;
                    newDoctor.Qualification = _doctorPersonalInfoVM.Qualification;
                    newDoctor.RegistrationNo = Convert.ToInt32(_doctorPersonalInfoVM.RegistrationNo);
                    newDoctor.Affiliation = _doctorPersonalInfoVM.Affiliation;
                    newDoctor.AboutMe = _doctorPersonalInfoVM.AboutMe;
                    newDoctor.AboutExperience = _doctorPersonalInfoVM.AboutExperience;
                    newDoctor.InactiveFlag = "N";
                    newDoctor.CreatedByID = 1;
                    newDoctor.CreatedDate = DateTime.Now;
                    newDoctor.ModifiedByID = 1;
                    newDoctor.ModifiedDate = DateTime.Now;
                    newDoctor.LoginId = 1;// for now we add 1 later we change

                    doctorPersonalInfoRepository.InsertDoctor(newDoctor);
                    doctorPersonalInfoRepository.Save();
                    ViewBag.Message = "Succsessfully added..";


                    //find the data in table using loginId
                    UserLogin NewUserlogin = db.UserLogins.Find(newDoctor.LoginId);

                    //Update the AutoPopulate data in UserLogin
                    NewUserlogin.FirstName = _doctorPersonalInfoVM.FirstName;
                    NewUserlogin.LastName = _doctorPersonalInfoVM.LastName;
                    NewUserlogin.EmailID = _doctorPersonalInfoVM.EmailID;
                    NewUserlogin.UserPhone = _doctorPersonalInfoVM.UserPhone;

                    doctorPersonalInfoRepository.UpdateDocUserLogin(NewUserlogin);
                    doctorPersonalInfoRepository.Save();
                    ViewBag.Message1 = " Update Login Succsessfully ..";

                    //find the data in table using loginId
                    UserLoginSpeciality NewUserLogSpeciality = db.UserLoginSpecialitys.Find(newDoctor.LoginId);

                    //Update the AutoPopulate data in UserLoginSpeciality                
                    NewUserLogSpeciality.SpecialityID = _doctorPersonalInfoVM.SpecialityID;

                    doctorPersonalInfoRepository.UpdateDocSpeciality(NewUserLogSpeciality);
                    doctorPersonalInfoRepository.Save();

                    ViewBag.Message2 = " Update Login Speciality Succsessfully ..";

                    @TempData["Message"] = "Succsessfully save data";
                }
            }
            catch (Exception)
            {
                // ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }


            return RedirectToAction("Create");

        }


        
    }
}