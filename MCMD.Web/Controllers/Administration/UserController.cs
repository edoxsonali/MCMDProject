using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.ViewModel.Administration;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
using MCMD.EntityModel.Administration;
using System.Web.Security;
using System.Data.Entity.Validation;
using MCMD.Web.Controllers.Account;
using MCMD.EntityModel;
using MCMD.Common.CommonClass;

namespace MCMD.Web.Controllers.Administration
{

    public class UserController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        private IUserRepository userRepository;
        public UserController(IUserRepository _userRepositorys)
        {
            this.userRepository = _userRepositorys;
        }


        #region Create User
        [HttpGet]
        public ActionResult RegisterUser()
        {
            UserRegisterViewModel _userRegisterViewModel = new UserRegisterViewModel();
            _userRegisterViewModel.Roles = userRepository.GetRoles().ToList();
            _userRegisterViewModel.Specialitys = userRepository.GetSpecialitys().ToList();
            _userRegisterViewModel.Userlogins = new UserLogin();



            return View(_userRegisterViewModel);

        }

        [HttpPost]
        public ActionResult RegisterUser(UserRegisterViewModel registerVM)
        {

            //ModelState.Clear();  // Cleare Model state
            if (ModelState.IsValid)
            {

                var cUser = db.UserLogins.FirstOrDefault(x => x.UserName == registerVM.Userlogins.UserName);
                var cUserEmail = db.UserLogins.FirstOrDefault(x => x.EmailID == registerVM.Userlogins.EmailID);
                var cEmpId = db.UserLogins.FirstOrDefault(X => X.EmployeeId == registerVM.Userlogins.EmployeeId);

                if (ReferenceEquals(cUser, null))
                {
                    if (ReferenceEquals(cUserEmail, null))
                    {
                        if (ReferenceEquals(cEmpId, null))
                        {
                            using (var dbContextTransaction = db.Database.BeginTransaction())
                            {
                                try
                                {

                                    //Take the password for email
                                    string password = registerVM.Userlogins.Password;

                                    //Insert data in Userlogins  Table                             
                                    userRepository.InsertUserLogins(registerVM.Userlogins, registerVM);
                                    userRepository.Save();

                                    //Insert data in Login_Role Table
                                    var newUserRole = db.UserLoginRoles.Create();
                                    userRepository.InsertUserLoginRoles(newUserRole, registerVM);
                                    userRepository.Save();


                                    if (registerVM.SpecialityID != 0)
                                    {
                                        //  Insert data in Login_Speciality Table
                                        var newUserspeciality = db.UserLoginSpecialitys.Create();
                                        userRepository.UserLoginSpecialitys(newUserspeciality, registerVM);
                                        userRepository.Save();

                                    }

                                    dbContextTransaction.Commit();
                                    ViewBag.StatusMessage = " User Name with " + registerVM.Userlogins.UserName + " having Email Id " + registerVM.Userlogins.EmailID + " is created successfully";
                                    ViewBag.Status = 1;


                                    //var callbackUrl = Url.Action("ConfirmEmail", "Account",new { userId = user.Id, code = code },protocol: Request.Url.Scheme);

                                    //get user emailid
                                    var emailid = registerVM.Userlogins.EmailID;
                                    //send mail
                                    string subject = "MyCityMyDoctor  Registration";
                                    string body = "Dear " + registerVM.Userlogins.FirstName + " " + registerVM.Userlogins.LastName + "<br/> <br/>" + System.Environment.NewLine + System.Environment.NewLine + "You have been successfully registered at MyCityMyDoctor , Your login credentials are given below<br/><br/>" +
                                    "Username" + " : " + registerVM.Userlogins.UserName + "<br/><br/>Password" + " : " +
                                     password + "<br/><br/><br/>Thank You" + "<br/>Admin" + "<br/>Edox";  //edit it
                                    try
                                    {
                                        SendEMail sendemail = new SendEMail();
                                        sendemail.Send_EMail(emailid, subject, body);
                                        @TempData["SuccessMessage"] = "User has been created successfully. Email sent to " + registerVM.Userlogins.EmailID + "";
                                    }
                                    catch (Exception ex)
                                    {
                                        // ViewBag.StatusMessage = "User has been created successfully but Error occurred while sending email. Error:" + ex.Message;
                                        @TempData["Message"] = "User has been created successfully but Error occurred while sending email. Error:" + ex.Message;
                                    }
                                }
                                catch (DbEntityValidationException)
                                {

                                    dbContextTransaction.Rollback();

                                }
                            }
                        }
                        else
                        {

                            @TempData["Message"] = "Employee ID Already Exist";

                        }

                    }
                    else
                    {

                        @TempData["Message"] = "Email ID Already Exist";
                    }
                }
                else
                {

                    @TempData["Message"] = "User Name Already Exist";

                }
            }
            ViewBag.ExistStatus = 1;
            return RedirectToAction("RegisterUser");

        }

        #endregion

        #region View User Admin/COE
        [HttpGet]
        public ActionResult ViewUser(int EmpId = 0, int RoleId = 0, string UserFirstName = "", string UserLastName = "", string UserEmailId = "", string UsePhone = "")
        {

            UserDetailsViewModel userDetailsVM = new UserDetailsViewModel();
            userDetailsVM.Roles = userRepository.GetRoles().Where(x => x.RoleId != 4).ToList();
            //get All info od users
            if (EmpId == 0 && RoleId == 0 && string.IsNullOrEmpty(UserFirstName) && string.IsNullOrEmpty(UserEmailId) && string.IsNullOrEmpty(UsePhone))
            {

                userDetailsVM.GetViewUsers = userRepository.GetAllUser().ToList();

            }

            //get All info for particular search
            if (RoleId != 0 || EmpId != 0 || !string.IsNullOrEmpty(UserFirstName) || !string.IsNullOrEmpty(UserLastName) || !string.IsNullOrEmpty(UserEmailId) || !string.IsNullOrEmpty(UsePhone))
            {
                userDetailsVM.RoleId = RoleId;

                userDetailsVM.GetViewUsers = userRepository.SearchUser(RoleId, EmpId, UserFirstName, UserLastName, UserEmailId, UsePhone).ToList();
            }



            return View(userDetailsVM);
        }
        [HttpPost]
        public ActionResult ViewUser(UserDetailsViewModel userDetailsVM)
        {
            int? empId = 0;
            int userRollId = 0;
            string Userfirstname = "";
            string useremailid = "";
            string userphone = "";
            string Userlastname = "";

            if (userDetailsVM.EmployeeId != 0 || userDetailsVM.RoleId != 0)
            {
                empId = userDetailsVM.EmployeeId;
                userRollId = userDetailsVM.RoleId;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.FirstName))
            {
                Userfirstname = userDetailsVM.FirstName;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.LastName))
            {
                Userlastname = userDetailsVM.LastName;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.EmailID))
            {
                useremailid = userDetailsVM.EmailID;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.UserPhone))
            {
                userphone = userDetailsVM.UserPhone;
            }
            return RedirectToAction("ViewUser", new { EmpId = empId, RoleId = userRollId, UserFirstName = Userfirstname, UserLastName = Userlastname, UserEmailId = useremailid, UsePhone = userphone });


        }

        #endregion

        #region View Doctor
        [HttpGet]
        public ActionResult ViewDoctor(int LogId = 0, int SpeID = 0, int RoleId = 0, string UserFirstName = "", string UserLastName = "", string UserEmailId = "", string UsePhone = "", int Clinicid = 0)
        {
            UserDetailsViewModel userDetailsVM = new UserDetailsViewModel();
            userDetailsVM.Roles = userRepository.GetRoles().ToList();
            userDetailsVM.DoctorClinicInformation = userRepository.GetClinicInformation().ToList();//Get Clinic Info
            userDetailsVM.speciality = userRepository.GetSpecialitys().ToList();

            if (LogId == 0 && RoleId == 0 && SpeID == 0 && string.IsNullOrEmpty(UserFirstName) && string.IsNullOrEmpty(UserEmailId) && string.IsNullOrEmpty(UsePhone) && Clinicid == 0)
            {
                userDetailsVM.GetViewDoctors = userRepository.getAllDoctor().ToList();

            }

            //get Role Id from dropdown
            if (RoleId != 0 || LogId != 0 || SpeID != 0 || !string.IsNullOrEmpty(UserFirstName) || !string.IsNullOrEmpty(UserLastName) || !string.IsNullOrEmpty(UserEmailId) || !string.IsNullOrEmpty(UsePhone) || Clinicid != 0)
            {
                userDetailsVM.RoleId = RoleId;

                userDetailsVM.GetViewDoctors = userRepository.SearchDoctor(LogId, SpeID, RoleId, UserFirstName, UserLastName, UserEmailId, UsePhone, Clinicid).ToList();
            }



            return View(userDetailsVM);
        }

        [HttpPost]
        public ActionResult ViewDoctor(UserDetailsViewModel userDetailsVM)
        {

            int loginId = 0;
            int userRollId = 0;
            string Userfirstname = "";
            string useremailid = "";
            string userphone = "";
            string Userlastname = "";
            int speID = 0;
            int ClinicID = 0;

            if (userDetailsVM.LoginId != 0 || userDetailsVM.RoleId != 0)
            {
                loginId = userDetailsVM.LoginId;
                userRollId = userDetailsVM.RoleId;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.ClinicName))
            {
                ClinicID = Convert.ToInt32(userDetailsVM.ClinicName);
            }
            if (userDetailsVM.SpecialityID != 0)
            {
                speID = userDetailsVM.SpecialityID;
            }
            if (!string.IsNullOrEmpty(userDetailsVM.FirstName))
            {
                Userfirstname = userDetailsVM.FirstName;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.LastName))
            {
                Userlastname = userDetailsVM.LastName;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.EmailID))
            {
                useremailid = userDetailsVM.EmailID;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.UserPhone))
            {
                userphone = userDetailsVM.UserPhone;
            }
            return RedirectToAction("ViewDoctor", new { LogId = loginId, SpeID = speID, RoleId = userRollId, UserFirstName = Userfirstname, UserLastName = Userlastname, UserEmailId = useremailid, UsePhone = userphone, Clinicid = ClinicID });


        }

        #endregion

        #region Edit User Admin/COE

        public ActionResult EditUserData(int Id)
        {
            Session["EditUser"] = Id;
            var varid = Id;
            return Json(new { redirectUrl = Url.Action("EditUser", "User", new { varid }), isRedirect = true, JsonRequestBehavior.AllowGet });

        }

        [HttpGet]
        public ActionResult EditUser()
        {
            EditUserViewModel _edituserVM = new EditUserViewModel();
            _edituserVM.Roles = userRepository.GetRoles().Where(x => x.RoleId != 4).ToList();

            //This session for Edit Admin/COE
            //int editUserInputs = (Session["EditUser"] != null) ? (Convert.ToInt32(Session["EditUser"])) : 1;
            int editUserInputs = Convert.ToInt32(Session["EditUser"]);


            //This is for populated for edit Admin/COE 
            if (editUserInputs != 0)
            {
                List<UserLogin> _NewUserloginInfo = userRepository.GetAllUserData().Where(x => x.LoginId == editUserInputs).ToList();
                List<UserLoginRole> _NewUserRole = userRepository.GetUserLoginRole().Where(x => x.LoginRoleId == editUserInputs).ToList();
                foreach (var item in _NewUserloginInfo)
                {
                    _edituserVM.UserName = item.UserName;
                    _edituserVM.FirstName = item.FirstName;
                    _edituserVM.LastName = item.LastName;
                    _edituserVM.EmailID = item.EmailID;
                    _edituserVM.UserPhone = item.UserPhone;
                    _edituserVM.EmployeeId = item.EmployeeId;
                }

                foreach (var item in _NewUserRole)
                {
                    _edituserVM.RoleID = item.RoleId;

                }

            }

            return View(_edituserVM);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel editUserVM)
        {
            if (ModelState.IsValid)
            {
                if (Session["EditUser"] != null)
                {

                    editUserVM.LoginId = Convert.ToInt32(Session["EditUser"]);

                    //find the data in table using loginId
                    UserLogin updateUser = db.UserLogins.Find(editUserVM.LoginId);

                    updateUser.UserName = editUserVM.UserName;
                    updateUser.FirstName = editUserVM.FirstName;
                    updateUser.LastName = editUserVM.LastName;
                    updateUser.EmailID = editUserVM.EmailID;
                    updateUser.UserPhone = editUserVM.UserPhone;
                    updateUser.EmployeeId = editUserVM.EmployeeId;

                    userRepository.UpdateUser(updateUser);
                    userRepository.Save();
                    TempData["SuccessMessage"] = "User is Updated successfully";

                    //find the data in table using loginId
                    UserLoginRole updateUserRole = db.UserLoginRoles.Find(editUserVM.LoginId);

                    updateUserRole.RoleId = editUserVM.RoleID;
                    userRepository.UpdateUserRole(updateUserRole);
                    userRepository.Save();


                    Session["EditUser"] = null;
                }


            }


            return RedirectToAction("EditUser");
        }


        #endregion

        #region Cancel Button for edit
        [HttpGet]
        public ActionResult CancelEdit()
        {
            // return RedirectToAction("ViewUser", "User", new { id = Session["DetailsId"] });
            return RedirectToAction("ViewUser", "User");
        }
        #endregion

        #region Delete User Admin/COE
        [HttpPost]
        public ActionResult BatchDelete(int[] deleteInputs)
        {

            if (deleteInputs != null)
            {
                foreach (var item in deleteInputs)
                {

                    UserLogin userlogin = db.UserLogins.Find(item);
                    if (User == null)
                    {
                        // return HttpNotFound();
                    }
                    else
                    {
                        userlogin.InactiveFlag = "Y";
                        userlogin.ModifiedDate = DateTime.Now;
                        userlogin.ModifiedByID = 2;

                        userRepository.UpdateUser(userlogin);
                        userRepository.Save();
                        @TempData["AddNewItemMessage"] = "User having Email Id " + userlogin.EmailID + " is deleted successfully";

                    }
                }

            }
            return Json("User", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Delete Doctor
        [HttpPost]
        public ActionResult BatchDeleteDoc(int[] deleteInputs)
        {

            if (deleteInputs != null)
            {
                foreach (var item in deleteInputs)
                {

                    UserLogin userlogin = db.UserLogins.Find(item);
                    if (User == null)
                    {
                        // return HttpNotFound();
                    }
                    else
                    {
                        userlogin.InactiveFlag = "Y";
                        userlogin.ModifiedDate = DateTime.Now;
                        userlogin.ModifiedByID = 2;

                        userRepository.UpdateUser(userlogin);
                        userRepository.Save();
                        @TempData["AddNewItemMessage"] = "User having Email Id " + userlogin.EmailID + " is deleted successfully";

                    }
                }

            }
            return Json("User", JsonRequestBehavior.AllowGet);
        }

        #endregion



        protected override void Dispose(bool disposing)
        {
            userRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}