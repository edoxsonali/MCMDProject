﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.ViewModel.Account;
using System.Web.Security;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using MCMD.EntityModel;
using MCMD.Common.CommonClass;
using MCMD.EntityModel.Administration;
using System.Globalization;


namespace MCMD.Web.Controllers.Account
{
    public class AccountController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        public string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        public string strApiUserName = ConfigurationManager.AppSettings["ApiUserName"];
        public string strApiPassword = ConfigurationManager.AppSettings["ApiPassword"];
        public string strApiCustomerName = ConfigurationManager.AppSettings["ApiCustomerName"];

        #region Login
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login 
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginVM, string emailId)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var existingUser = db.UserLogins.FirstOrDefault(u => u.EmailID == loginVM.EmailId);


                if (ReferenceEquals(existingUser, null))
                {
                    //Unable to authenticate as user
                    // ModelState.AddModelError("", "User Name does not exist");
                    @TempData["ErrorMessage"] = "Email Id does not exist";
                }
                else
                {
                    //User is active or not
                    if (existingUser.InactiveFlag == "N")
                    {

                        //valid user user.Password
                        if (existingUser.Password == crypto.Compute(loginVM.Password, existingUser.PasswordSalt))
                        {

                            var exitRole = db.UserLoginRoles.FirstOrDefault(u => u.LoginId == existingUser.LoginId);
                            //Valid user
                            FormsAuthentication.SetAuthCookie(loginVM.EmailId, loginVM.RememberMe);
                            Session["UserName"] = loginVM.EmailId;

                            string FullName = existingUser.FirstName + " " + existingUser.LastName;
                            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FullName);
                            Session["Name"] = result;
                            @TempData["Name"] = Session["Name"];

                            if (exitRole.RoleId != 4)
                            {
                                Session["Admin"] = existingUser.LoginId;
                                return RedirectToAction("RegisterUser", "User");
                            }
                            else
                            {
                                Session["Doctor"] = existingUser.LoginId;
                                string Result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FullName);
                                Session["Name"] = "Dr. " + Result;

                                return RedirectToAction("Create", "DocPersonalInfo");

                            }
                        }
                        else
                        {
                            //Invalid Password
                            //  ModelState.AddModelError("", "Invalid Password");
                            @TempData["ErrorMessage"] = "Invalid Password";
                        }
                    }
                    else
                    {
                        //Inactive user
                        // ModelState.AddModelError("", "InActive User, Please Contact Administrator.");
                        @TempData["ErrorMessage"] = "InActive User, Please Contact Administrator.";
                    }

                }
            }

            // For DOCTOR redirect
            // return RedirectToAction("viewpageName", "Controller", new { area = "Doctor" });

            return View(loginVM);
        }


        #endregion

        #region LogOff

        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Forgot Password

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgetPasswordModel forget)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    //check user existance
                    var count = db.UserLogins.Count(u => u.EmailID == forget.EmailId);

                    if (count == 0)
                    {
                        //ModelState.AddModelError("", "Entered Email does not exist.");
                        @TempData["ErrorMessage"] = "Entered Email does not exist.";
                    }
                    else
                    {
                        generatepassword genPass = new generatepassword();
                        var TempPassword = genPass.generate_password();
                        //generate password token
                        var crypto = new SimpleCrypto.PBKDF2();
                        var token = crypto.Compute(TempPassword);

                        var newUser = db.UserLogins.Where(a => a.EmailID == forget.EmailId).FirstOrDefault();
                        if (newUser != null)
                        {
                            newUser.PasswordVerificationToken = token;
                            newUser.PasswordVerificationTokenExpirationDate = System.DateTime.Now.AddHours(48);
                        }

                        db.SaveChanges();

                        //create url with above token
                        var resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new { un = forget.EmailId, rt = token }, "http") + "'>Reset Password</a>";

                        //var resetLink = Url.Action("ResetPassword", "Account", new { un = email, rt = token }, "http");

                        //get user emailid
                        var emailid = (from i in db.UserLogins
                                       where i.EmailID == forget.EmailId
                                       select i.EmailID).FirstOrDefault();
                        //send mail
                        string subject = "Password Reset Token";
                        string body = "<b>You have requested to change the password by Forgot Password option, Please find the Password Reset Token in this mail, You can click on the link or copy and paste the link in you browser</b><br/>" + resetLink; //edit it
                        try
                        {
                            SendEMail sendemail = new SendEMail();
                            sendemail.Send_EMail(emailid, subject, body);
                            //  ViewBag.StatusMessage = "An email has been sent to the email address you registered with. Follow the instruction in this email to complete your password reset.";
                            @TempData["Message"] = "An email has been sent to the email address you registered with. Follow the instruction in this email to complete your password reset.";
                        }
                        catch (Exception ex)
                        {
                            //  ViewBag.StatusMessage = "Error occured while sending email." + ex.Message;
                            @TempData["ErrorMessage"] = "Error occured while sending email." + ex.Message;
                        }
                        ViewBag.Status = 1;
                        return View();
                    }
                }

            }

            return View(forget);
        }


        #endregion

        #region Reset Password

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword(string un, string rt)
        {
            ResetPasswordConfirmModel model = new ResetPasswordConfirmModel();
            model.EmailId = un;
            model.Token = rt;
            Session["EmailId"] = model.EmailId;
            Session["Token"] = model.Token;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordConfirmModel model)
        {
            if (ModelState.IsValid)
            {

                model.EmailId = Session["EmailId"].ToString();
                model.Token = Session["Token"].ToString();
                //TODO: Check the un and rt matching and then perform following
                //get userid of received username
                var EmailId = (from i in db.UserLogins
                               where i.EmailID == model.EmailId
                               select i.EmailID).FirstOrDefault();
                //check userid and token matches

                bool any = (from j in db.UserLogins
                            where (j.EmailID == EmailId)
                            && (j.PasswordVerificationToken == model.Token)
                            //&& (j.PasswordVerificationTokenExpirationDate < DateTime.Now)
                            select j).Any();

                if (any == true)
                {
                    var UpdateUser = db.UserLogins.Where(a => a.EmailID == EmailId).FirstOrDefault();
                    //Setting password expiry date
                    if (UpdateUser.PasswordVerificationTokenExpirationDate > System.DateTime.Now)
                    {
                        if (UpdateUser != null)
                        {
                            var crypto = new SimpleCrypto.PBKDF2();
                            var encrypPass = crypto.Compute(model.NewPassword);
                            UpdateUser.Password = encrypPass;
                            UpdateUser.ConfirmPassword = encrypPass;
                            UpdateUser.PasswordSalt = crypto.Salt;
                        }
                        db.SaveChanges();
                        //reset password
                        //send email
                        string subject = "New Password";
                        string body = "<b>Your password has been changed as per our record, Please login with your new password</b><br/>";//edit it
                        try
                        {
                            SendEMail sendemail = new SendEMail();
                            sendemail.Send_EMail(EmailId, subject, body);
                            ViewBag.StatusMessage = "Password has been reset now and An email has been sent to the email address you registered with for confirmation.";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.StatusMessage = "Error occured while sending email." + ex.Message;
                        }
                        ViewBag.Status = 1;
                        return View();

                    }
                    else
                    {
                        ModelState.AddModelError("", "Reset Password Token is expired, Use Forgot password link from login screen to regenerate your tocken");
                    }

                }
                else
                {
                    if (model.Token == null)
                    {
                        ModelState.AddModelError("", "No Valid Token found, Use Forgot password link from login screen to regenerate your token");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Input Token number is not correct or expired");
                    }
                }

            }
            Session["EmailId"] = null;
            Session["Token"] = null;
            return View(model);
        }

        #endregion

        #region Forgot Password for Patient
        public ActionResult PatientForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PatientForgotPassword(ForgetPasswordModel forgetPatient)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    //check user existance
                    var count = db.patientlogins.Count(u => u.EmailID == forgetPatient.EmailId);

                    if (count == 0)
                    {
                        //ModelState.AddModelError("", "Entered Email does not exist.");
                        @TempData["ErrorMessage"] = "Entered Email does not exist.";
                    }
                    else
                    {
                        generatepassword genPass = new generatepassword();
                        var TempPassword = genPass.generate_password();
                        //generate password token
                        var crypto = new SimpleCrypto.PBKDF2();
                        var token = crypto.Compute(TempPassword);

                        var newUser = db.patientlogins.Where(a => a.EmailID == forgetPatient.EmailId).FirstOrDefault();
                        if (newUser != null)
                        {
                            newUser.PasswordVerificationToken = token;
                            newUser.PasswordVerificationTokenExpirationDate = System.DateTime.Now.AddHours(48);
                        }

                        db.SaveChanges();

                        //create url with above token
                        var resetLink = "<a href='" + Url.Action("ResetPasswordPatient", "Account", new { unp = forgetPatient.EmailId, rtp = token }, "http") + "'>Reset Password</a>";

                        //var resetLink = Url.Action("ResetPassword", "Account", new { un = email, rt = token }, "http");

                        //get user emailid
                        var emailid = (from i in db.patientlogins
                                       where i.EmailID == forgetPatient.EmailId
                                       select i.EmailID).FirstOrDefault();
                        //send mail
                        string subject = "Password Reset Token";
                        string body = "<b>You have requested to change the password by Forgot Password option, Please find the Password Reset Token in this mail, You can click on the link or copy and paste the link in you browser</b><br/>" + resetLink; //edit it
                        try
                        {
                            SendEMail sendemail = new SendEMail();
                            sendemail.Send_EMail(emailid, subject, body);
                            //  ViewBag.StatusMessage = "An email has been sent to the email address you registered with. Follow the instruction in this email to complete your password reset.";
                            @TempData["Message"] = "An email has been sent to the email address you registered with. Follow the instruction in this email to complete your password reset.";
                        }
                        catch (Exception ex)
                        {
                            //  ViewBag.StatusMessage = "Error occured while sending email." + ex.Message;
                            @TempData["ErrorMessage"] = "Error occured while sending email." + ex.Message;
                        }
                        ViewBag.Status = 1;
                        return View();
                    }
                }

            }

            return View(forgetPatient);
        }



        #endregion


        #region Reset Password Patient
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPasswordPatient(string unp, string rtp)
        {
            ResetPasswordConfirmModel model = new ResetPasswordConfirmModel();
            model.EmailId = unp;
            model.Token = rtp;
            Session["EmailId"] = model.EmailId;
            Session["Token"] = model.Token;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPasswordPatient(ResetPasswordConfirmModel model)
        {
            model.EmailId = Session["EmailId"].ToString();
            model.Token = Session["Token"].ToString();
            if (ModelState.IsValid)
            {
                //TODO: Check the un and rt matching and then perform following
                //get userid of received username
                var EmailId = (from i in db.patientlogins
                               where i.EmailID == model.EmailId
                               select i.EmailID).FirstOrDefault();
                //check userid and token matches

                bool any = (from j in db.patientlogins
                            where (j.EmailID == model.EmailId)
                            && (j.PasswordVerificationToken == model.Token)
                            //&& (j.PasswordVerificationTokenExpirationDate < DateTime.Now)
                            select j).Any();

                if (any == true)
                {
                    var UpdateUser = db.patientlogins.Where(a => a.EmailID == EmailId).FirstOrDefault();
                    //Setting password expiry date
                    if (UpdateUser.PasswordVerificationTokenExpirationDate > System.DateTime.Now)
                    {
                        if (UpdateUser != null)
                        {
                            var crypto = new SimpleCrypto.PBKDF2();
                            var encrypPass = crypto.Compute(model.NewPassword);
                            UpdateUser.Password = encrypPass;
                            UpdateUser.ConfirmPassword = encrypPass;
                            UpdateUser.PasswordSalt = crypto.Salt;
                        }
                        db.SaveChanges();
                        //reset password
                        //send email
                        string subject = "New Patient Password";
                        string body = "<b>Your password has been changed as per our record, Please login with your new password</b><br/>";//edit it
                        try
                        {
                            SendEMail sendemail = new SendEMail();
                            sendemail.Send_EMail(EmailId, subject, body);
                            ViewBag.StatusMessage = "Patient Password has been reset now and An email has been sent to the email address you registered with for confirmation.";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.StatusMessage = "Error occured while sending email to Patient." + ex.Message;
                        }
                        ViewBag.Status = 1;
                        return View();

                    }
                    else
                    {
                        ModelState.AddModelError("", "Patient Reset Password Token is expired, Use Forgot password link from login screen to regenerate your tocken");
                    }

                }
                else
                {
                    if (model.Token == null)
                    {
                        ModelState.AddModelError("", "Patient- No Valid Token found, Use Forgot password link from login screen to regenerate your token");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Patient- Input Token number is not correct or expired");
                    }
                }

            }
            return View(model);
        }

        #endregion


        


        #region Doctor Login
        public ActionResult DocLogin()
        {
            //LoginViewModel loginVM = new LoginViewModel();
            //loginVM.categorylist = new List<category>() {
            //    new category {categoryId=1,categoryName="I am Doctor"},
            //    new category {categoryId=2,categoryName="I am Patient"},

            //};

            return View();
        }

        [HttpPost]
        public ActionResult DocLogin(LoginViewModel loginVM, string emailId)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var existingUser = db.UserLogins.FirstOrDefault(u => u.EmailID == loginVM.EmailId);


                if (ReferenceEquals(existingUser, null))
                {
                    //Unable to authenticate as user
                    // ModelState.AddModelError("", "User Name does not exist");
                    @TempData["ErrorMessage"] = "Email Id does not exist";
                }
                else
                {
                    //User is active or not
                    if (existingUser.InactiveFlag == "N")
                    {

                        //valid user user.Password
                        if (existingUser.Password == crypto.Compute(loginVM.Password, existingUser.PasswordSalt))
                        {

                            var exitRole = db.patientlogins.FirstOrDefault(u => u.PatientId == existingUser.LoginId);
                            //Valid user
                            FormsAuthentication.SetAuthCookie(loginVM.EmailId, loginVM.RememberMe);
                            Session["UserName"] = loginVM.EmailId;

                            Session["Doctor"] = existingUser.LoginId;
                            return RedirectToAction("Create", "DocPersonalInfo");


                        }
                        else
                        {
                            //Invalid Password
                            //  ModelState.AddModelError("", "Invalid Password");
                            @TempData["ErrorMessage"] = "Invalid Password";
                        }
                    }
                    else
                    {
                        //Inactive user
                        // ModelState.AddModelError("", "InActive User, Please Contact Administrator.");
                        @TempData["ErrorMessage"] = "InActive User, Please Contact Administrator.";
                    }

                }
            }

            // For DOCTOR redirect
            // return RedirectToAction("viewpageName", "Controller", new { area = "Doctor" });

            return RedirectToAction("DocLogin");
        }

        #endregion


        #region Patient Login
        public ActionResult patientLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult patientLogin(LoginViewModel loginVM, string emailId)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var exitingPatient = db.patientlogins.FirstOrDefault(u => u.EmailID == loginVM.EmailId);


                if (ReferenceEquals(exitingPatient, null))
                {
                    //Unable to authenticate as user
                    // ModelState.AddModelError("", "User Name does not exist");
                    @TempData["ErrorMessage"] = "Email Id does not exist";
                }
                else
                {
                    //User is active or not
                    if (exitingPatient.InactiveFlag == "N")
                    {

                        //valid user user.Password
                        if (exitingPatient.Password == crypto.Compute(loginVM.Password, exitingPatient.PasswordSalt))
                        {

                            var exitRole = db.patientlogins.FirstOrDefault(u => u.PatientId == exitingPatient.PatientId);
                            //Valid user
                            FormsAuthentication.SetAuthCookie(loginVM.EmailId, loginVM.RememberMe);
                            Session["UserName"] = loginVM.EmailId;

                            Session["Patient"] = exitingPatient.PatientId;
                            return RedirectToAction("", "");


                        }
                        else
                        {
                            //Invalid Password
                            //  ModelState.AddModelError("", "Invalid Password");
                            @TempData["ErrorMessage"] = "Invalid Password";
                        }
                    }
                    else
                    {
                        //Inactive user
                        // ModelState.AddModelError("", "InActive User, Please Contact Administrator.");
                        @TempData["ErrorMessage"] = "InActive User, Please Contact Administrator.";
                    }

                }
            }


            return RedirectToAction("patientLogin");
        }

        #endregion

    }

}