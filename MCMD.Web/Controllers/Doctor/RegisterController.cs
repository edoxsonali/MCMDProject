using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel;
using MCMD.ViewModel.Administration;
using MCMD.ViewModel.doctor;
using MCMD.IRepository.DoctorInterfaces;
using MCMD.EntityRepository.DoctorRepository;
using System.Data.Entity;
using System.Data.Entity.Validation;
using MCMD.Common.CommonClass;
using MCMD.EntityModel.Patient;


namespace MCMD.Web.Controllers.Doctor
{
    public class RegisterController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        private IDocRegister docregistor;
        public RegisterController(IDocRegister _docRegister)
        {
            this.docregistor = _docRegister;
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            DocRegisterViewModel _docRegisterViewModel = new DocRegisterViewModel();

            _docRegisterViewModel.Specialitys = docregistor.GetSpeciality().ToList();
            // _docRegisterViewModel.Userlogins = new UserLogin();

            return View(_docRegisterViewModel);
        }
        [HttpPost]
        public ActionResult Create(DocRegisterViewModel docRegVM)
        {
            ModelState.Clear();

            if (ModelState.IsValid)
            {

                var cUserEmail = db.UserLogins.FirstOrDefault(x => x.EmailID == docRegVM.Userlogins.EmailID);

                if (ReferenceEquals(cUserEmail, null))
                {

                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            docRegVM.RoleId = 4;

                            generatepassword genpass = new generatepassword();

                            var TempPassword = genpass.generate_password();

                            //Take the password for email
                            string password = TempPassword.ToString();

                            docRegVM.Userlogins.Password = password;

                            //Insert data in Userlogins  Table                             
                            docregistor.InsertDoctor(docRegVM.Userlogins, docRegVM);
                            docregistor.Save();



                            //Insert data in Login_Role Table
                            var newUserRole = db.UserLoginRoles.Create();
                            docregistor.InsertDoctorRoles(newUserRole, docRegVM);
                            docregistor.Save();


                            //if (registerVM.SpecialityID!=0)
                            //{
                            //Insert data in Login_Speciality Table
                            var newUserspeciality = db.UserLoginSpecialitys.Create();
                            docregistor.DocLoginSpecialitys(newUserspeciality, docRegVM);
                            docregistor.Save();

                            // }

                            dbContextTransaction.Commit();
                          //  ViewBag.StatusMessage = " User Name with " + docRegVM.Userlogins.UserName + " having Email Id " + docRegVM.Userlogins.EmailID + " is created successfully";
                          //  ViewBag.Status = 1;
                            @TempData["SuccessMessage"] = " User Name with " + docRegVM.Userlogins.FirstName + " " + docRegVM.Userlogins.LastName + " having Email Id " + docRegVM.Userlogins.EmailID + " is created successfully";

                            //var callbackUrl = Url.Action("ConfirmEmail", "Account",new { userId = user.Id, code = code },protocol: Request.Url.Scheme);

                            //get user emailid
                            var emailid = docRegVM.Userlogins.EmailID;
                            //send mail
                            string subject = "MyCityMyDoctor  Registration";
                            string body = "Dear " + docRegVM.Userlogins.FirstName + " " + docRegVM.Userlogins.LastName + "<br/> <br/>" + System.Environment.NewLine + System.Environment.NewLine + "You have been successfully registered at MyCityMyDoctor , Your login credentials are given below<br/><br/>" 
                           + "<br/><br/>Password" + " : " +
                             password + "<br/><br/><br/>Thank You" + "<br/>Admin" + "<br/>Edox";  //edit it
                            try
                            {
                                SendEMail sendemail = new SendEMail();
                                sendemail.Send_EMail(emailid, subject, body);
                                @TempData["SuccessMessage"] = "User has been created Successfully. Email sent to " + docRegVM.Userlogins.EmailID + "";
                            }
                            catch (Exception ex)
                            {
                                //ViewBag.StatusMessage = "User has been created successfully but Error occurred while sending email. Error:" + ex.Message;
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

                    @TempData["Message"] = "Email ID Already Exist";
                }

            
            }
            return RedirectToAction("Create");
        }

      
    }
}