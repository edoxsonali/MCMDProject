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

        // GET: /CreateUser/
        //public ActionResult Index()
        //{
        //    var Model = new UserVM();

        //    var Users = from s in userRepository.GetUsers()
        //                select s;

        //    return View(Users);
        //}



        // GET: /CreateUser/Details/
        //public ViewResult Details(int id)
        //{

        //    User user = userRepository.GetUserByID(id);
        //    return View(user);
        //}

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
            if (ModelState.IsValid)
            {

                var cUser = db.UserLogins.FirstOrDefault(x => x.UserName == registerVM.Userlogins.UserName);
                var cUserEmail = db.UserLogins.FirstOrDefault(x => x.EmailID == registerVM.Userlogins.EmailID);
                if (ReferenceEquals(cUser, null))
                {
                    if (ReferenceEquals(cUserEmail, null))
                    {
                        using (var dbContextTransaction = db.Database.BeginTransaction())
                        {
                            try
                            {

                                //password for email
                                string password = registerVM.Userlogins.Password;

                                //Insert data in Userlogins  Table                             
                                userRepository.InsertUserLogins(registerVM.Userlogins, registerVM);
                                userRepository.Save();

                                //Insert data in Login_Role Table

                                var newUserRole = db.UserLoginRoles.Create();

                                userRepository.InsertUserLoginRoles(newUserRole, registerVM);
                                userRepository.Save();

                                //Insert data in Login_Dispensary Table
                                var newUserspeciality = db.UserLoginSpecialitys.Create();
                                userRepository.UserLoginSpecialitys(newUserspeciality, registerVM);
                                userRepository.Save();

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
                                }
                                catch (Exception ex)
                                {
                                    ViewBag.StatusMessage = "User has been created successfully but Error occurred while sending email. Error:" + ex.Message;
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
                        ModelState.AddModelError("Userlogins.EmailID", "Email ID Already Exist");
                    }
                }
                else
                {
                    ModelState.AddModelError("Userlogins.UserName", "User Name Already Exist");

                }
            }
            ViewBag.ExistStatus = 1;
            return RedirectToAction("RegisterUser");
        }

        #endregion

        #region View User
        [HttpGet]
        public ActionResult ViewUser(int EmpId = 0, int RoleId = 0, string UserFirstName = "", string UserLastName = "", string UserEmailId = "", string UsePhone = "")
        {

            UserDetailsViewModel userDetailsVM = new UserDetailsViewModel();
            userDetailsVM.Roles = userRepository.GetRoles().ToList();
            if (EmpId == 0 && RoleId == 0 && string.IsNullOrEmpty(UserFirstName) && string.IsNullOrEmpty(UserEmailId) && string.IsNullOrEmpty(UsePhone))
            {
                         
                userDetailsVM.GetViewUsers = userRepository.GetAllUser().ToList();
                 
             }

            //get Role Id from dropdown
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
            int empId = 0;
            int userRollId = 0;
            string Userfirstname = "";
            string useremailid = "";
            string userphone = "";
            string Userlastname="";

            if (userDetailsVM.EmployeeId != 0 || userDetailsVM.RoleId != 0)
            {
                empId = userDetailsVM.EmployeeId;
                userRollId = userDetailsVM.RoleId;

            }
            if(!string.IsNullOrEmpty(userDetailsVM.FirstName))
            {
                Userfirstname = userDetailsVM.FirstName;

            }
            if (!string.IsNullOrEmpty(userDetailsVM.LastName))
            {
                Userlastname = userDetailsVM.LastName;

            }
            if(!string.IsNullOrEmpty(userDetailsVM.EmailID))
            {
                useremailid = userDetailsVM.EmailID;

            }
            if(!string.IsNullOrEmpty(userDetailsVM.UserPhone))
            {
                userphone = userDetailsVM.UserPhone;
            }
            return RedirectToAction("ViewUser", new { EmpId = empId, RoleId = userRollId, UserFirstName = Userfirstname, UserLastName = Userlastname, UserEmailId = useremailid, UsePhone = userphone });


        }

        #endregion

        #region View Doctor
          [HttpGet]
        public ActionResult ViewDoctor(int EmpId = 0, int RoleId = 0, string UserFirstName = "", string UserLastName = "", string UserEmailId = "", string UsePhone = "")
        {
            UserDetailsViewModel userDetailsVM = new UserDetailsViewModel();
            userDetailsVM.Roles = userRepository.GetRoles().ToList();
            if (EmpId == 0 && RoleId == 0 && string.IsNullOrEmpty(UserFirstName) && string.IsNullOrEmpty(UserEmailId) && string.IsNullOrEmpty(UsePhone))
            {
                userDetailsVM.GetViewDoctors = userRepository.getAllDoctor().ToList(); 

            }

            //get Role Id from dropdown
            if (RoleId != 0 || EmpId != 0 || !string.IsNullOrEmpty(UserFirstName) || !string.IsNullOrEmpty(UserLastName) || !string.IsNullOrEmpty(UserEmailId) || !string.IsNullOrEmpty(UsePhone))
            {
                userDetailsVM.RoleId = RoleId;

                userDetailsVM.GetViewDoctors = userRepository.SearchDoctor(RoleId, EmpId, UserFirstName, UserLastName, UserEmailId, UsePhone).ToList();
            }



            return View(userDetailsVM);
        }

        [HttpPost]
          public ActionResult ViewDoctor(UserDetailsViewModel userDetailsVM)
          {

              int empId = 0;
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
              return RedirectToAction("ViewDoctor", new { EmpId = empId, RoleId = userRollId, UserFirstName = Userfirstname, UserLastName = Userlastname, UserEmailId = useremailid, UsePhone = userphone });


          }

        #endregion

        #region Edit User
        //
        // GET: /CreateUser/Edit/

        //public ActionResult Edit(int id)
        //{
        //    User user = userRepository.GetUserByID(id);
        //    return View(user);
        //}


        // POST: /CreateUser/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserLogin user)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    userRepository.UpdateUser(user);
                    userRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(user);
        }

        

        #endregion

        #region Delete User
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