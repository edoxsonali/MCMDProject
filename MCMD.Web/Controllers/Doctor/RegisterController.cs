using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.IRepository.DoctorInterfaces;
using MCMD.EntityRepository.DoctorRepository;
using MCMD.ViewModel.doctor;
using MCMD.EntityModel.Doctor;


namespace MCMD.Web.Controllers.Doctor
{
    public class RegisterController : Controller
    {
         private IRegisterRepository registerRepository;
         public RegisterController(IRegisterRepository _registerRepositorys)
        {
            this.registerRepository = _registerRepositorys;
        }
       

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register/Create
        public ActionResult Create()        
        {
            ViewData["PageRole"] = 1;
            RegisterVM _registerVM = new RegisterVM();
            _registerVM.Specialitys = registerRepository.GetSpecialitys().ToList();
            _registerVM.Titles = registerRepository.GetTitles().ToList();
            _registerVM.Registers = new Register();
            _registerVM.ID = 1;
            
            return View(_registerVM);          
          
        }

        //
        // POST: /CreateUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterVM registerVM) 
        {
            try
            {
                //ModelState.Remove("Speciality");
                //ModelState.Remove("Title");
                ////ModelState.Clear();

       
                if (ModelState.IsValid)
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrypPass = crypto.Compute(registerVM.Registers.Password);
                    var newRegister = new Register();// db.Register.Create();
                    newRegister.Title = registerVM.TitleId.ToString();
                    newRegister.FirstName = registerVM.Registers.FirstName;
                    newRegister.LastName = registerVM.Registers.LastName;
                    newRegister.EmailId = registerVM.Registers.EmailId;
                    newRegister.Password = encrypPass;
                    newRegister.ConfirmPassword = encrypPass;
                    newRegister.PasswordSalt = crypto.Salt;
                    newRegister.Speciality = registerVM.ID.ToString();
                    newRegister.Phone = registerVM.Registers.Phone;
                    newRegister.CreatedDate = DateTime.Now;


                    registerRepository.InsertRegDoc(newRegister);
                    registerRepository.Save();
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