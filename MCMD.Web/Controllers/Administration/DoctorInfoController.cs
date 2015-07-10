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
using System.IO;
using System.Data.Entity.Validation;


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

        public ActionResult UserEditDoctor(int Id)
        {
            Session["EditDoctor"] = Id;
            var varid = Id;
            return Json(new { redirectUrl = Url.Action("Create", "DoctorInfo", new { varid }), isRedirect = true, JsonRequestBehavior.AllowGet });

        }
        #region Create/Update DoctorPersonal Info
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            ViewData["PageRole"] = 1;


            //This session for Edit doctor
            int editDocInputs = (Session["EditDoctor"] != null) ? (Convert.ToInt32(Session["EditDoctor"])) : 0;


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
                    _doctorVM.FolderFilePath = item.FolderFilePath;
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
        public ActionResult Create(DoctorPersonalInfoViewModel _doctorPersonalInfoVM, HttpPostedFileBase file)
        {
            try
            {
                ModelState.Clear();

                if (ModelState.IsValid)
                {

                    _doctorPersonalInfoVM.LoginId = Convert.ToInt32(Session["EditDoctor"]);

                    var existingUser = db.DoctorsPersonals.FirstOrDefault(u => u.LoginId == _doctorPersonalInfoVM.LoginId);

                    //Insert the remain data in DoctorPersonalInformation
                    var newDoctor = new DoctorPersonalInformation();

                    newDoctor.LoginId = Convert.ToInt32(Session["EditDoctor"]);
                    if (ReferenceEquals(existingUser, null))
                    {
                        if (!ReferenceEquals(newDoctor.MiddleName, null))
                        {
                            newDoctor.MiddleName = _doctorPersonalInfoVM.MiddleName;
                        }

                        newDoctor.Qualification = _doctorPersonalInfoVM.Qualification;

                        if (!ReferenceEquals(newDoctor.Qualification1, null))
                        {
                            newDoctor.Qualification1 = _doctorPersonalInfoVM.Qualification1;
                        }

                        newDoctor.RegistrationNo = _doctorPersonalInfoVM.RegistrationNo;

                        if (!ReferenceEquals(newDoctor.Affiliation, null))
                        {

                            newDoctor.Affiliation = _doctorPersonalInfoVM.Affiliation;
                        }

                        if (!ReferenceEquals(newDoctor.AboutMe, null))
                        {
                            newDoctor.AboutMe = _doctorPersonalInfoVM.AboutMe;
                        }

                        if (!ReferenceEquals(newDoctor.ExperienceInYear, null))
                        {
                            newDoctor.ExperienceInYear = _doctorPersonalInfoVM.ExperienceInYear;
                        }
                        if (!ReferenceEquals(newDoctor.ExperienceInMonth, null))
                        {
                            newDoctor.ExperienceInMonth = _doctorPersonalInfoVM.ExperienceInMonth;
                        }
                        newDoctor.InactiveFlag = "N";
                        newDoctor.CreatedByID = 1;
                        newDoctor.CreatedDate = DateTime.Now;
                        newDoctor.ModifiedByID = 1;
                        newDoctor.ModifiedDate = DateTime.Now;

                        if (file != null)
                        {
                            string[] formats = new string[] { "image/jpeg", "image/png", "image/gif", "image/Bmp" };
                            int CheckImgType = Convert.ToInt32(formats.Contains(file.ContentType));
                            if (CheckImgType != 0)
                            {
                                string Imgpath = "~/Media/" + file.FileName;
                                string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                                file.SaveAs(path);

                                newDoctor.FolderFilePath = Imgpath;
                                newDoctor.UploadType = file.ContentType;

                            }
                        }

                        //insert if not exit
                        doctorPersonalInfoRepository.InsertDoctor(newDoctor);
                        doctorPersonalInfoRepository.Save();

                    }
                    else
                    {

                        existingUser.MiddleName = _doctorPersonalInfoVM.MiddleName;
                        existingUser.Qualification = _doctorPersonalInfoVM.Qualification;
                        existingUser.Qualification1 = _doctorPersonalInfoVM.Qualification1;
                        existingUser.RegistrationNo = _doctorPersonalInfoVM.RegistrationNo;
                        existingUser.Affiliation = _doctorPersonalInfoVM.Affiliation;
                        existingUser.AboutMe = _doctorPersonalInfoVM.AboutMe;
                        existingUser.ExperienceInYear = _doctorPersonalInfoVM.ExperienceInYear;
                        existingUser.ExperienceInMonth = _doctorPersonalInfoVM.ExperienceInMonth;
                        existingUser.InactiveFlag = "N";
                        existingUser.CreatedByID = 1;
                        existingUser.CreatedDate = DateTime.Now;
                        existingUser.ModifiedByID = 1;
                        existingUser.ModifiedDate = DateTime.Now;
                        existingUser.LoginId = Convert.ToInt32(Session["EditDoctor"]);

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
                        doctorPersonalInfoRepository.UpdateDoctorPersonalInfo(existingUser);
                        doctorPersonalInfoRepository.Save();


                    }

                    //find the data in Login table using loginId
                    var NewUserlogin = db.UserLogins.FirstOrDefault(x => x.LoginId == newDoctor.LoginId);
                    NewUserlogin.FirstName = _doctorPersonalInfoVM.FirstName;
                    NewUserlogin.LastName = _doctorPersonalInfoVM.LastName;
                    NewUserlogin.EmailID = _doctorPersonalInfoVM.EmailID;
                    NewUserlogin.UserPhone = _doctorPersonalInfoVM.UserPhone;

                    doctorPersonalInfoRepository.UpdateDocUserLogin(NewUserlogin);
                    doctorPersonalInfoRepository.Save();
                    ViewBag.Message1 = "Record Update Succsessfully ..";

                    //find the data in Login_Speciality table using loginId
                    var NewUserLogSpeciality = db.UserLoginSpecialitys.FirstOrDefault(x => x.LoginId == newDoctor.LoginId);
                    NewUserLogSpeciality.SpecialityID = _doctorPersonalInfoVM.SpecialityID;

                    doctorPersonalInfoRepository.UpdateDocSpeciality(NewUserLogSpeciality);
                    doctorPersonalInfoRepository.Save();

                    // ViewBag.Message2 = " Update Login Speciality Succsessfully ..";

                    @TempData["SuccessMessage"] = "Record Saved  Successfully";
                }


            }


            catch (DbEntityValidationException ex)
            {
                // ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                var errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                //  var newException = new FormattedDbEntityValidationException(ex);
                @TempData["Message"] = errorMessages;
                return RedirectToAction("Create", "DoctorInfo");
            }
            return RedirectToAction("Create", "DoctorClinicInfo");

        }

        #endregion

    }
}