using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
using MCMD.ViewModel.Administration;

namespace MCMD.Web.Controllers.Administration
{
    public class SpecialityController : Controller
    {
        public MCMD.EntityModel.ApplicationDbContext db = new MCMD.EntityModel.ApplicationDbContext();

        private ISpecialityRepository specialityRepository;
        public SpecialityController(ISpecialityRepository _specialityRepository)
        {
            this.specialityRepository = _specialityRepository;
        }
        // [HttpGet]
        // GET: Speciality
        public ActionResult Index()
        {
            return View();
        }


        #region Create/View Speciality
        public ActionResult Create()
        {

            SpecialityViewModel _specialityVM = new SpecialityViewModel();
            _specialityVM.SpecialityList = specialityRepository.GetSpecialitys().Where(x => x.InactiveFlag == "N").ToList();

            return View(_specialityVM);

        }

        [HttpPost]
        public ActionResult Create(SpecialityViewModel specialityVM)
        {
     //     var cSpeciality = specialityRepository.CheckSpeciality(specialityVM.specialitys.SpecialityName);

            var cSpeciality = db.Specialitys.FirstOrDefault(x => x.SpecialityName == specialityVM.specialitys.SpecialityName);
            try
            {
                if (ReferenceEquals(cSpeciality, null))
                {
                    if (ModelState.IsValid)
                    {
                        
                        specialityRepository.InsertSpeciality(specialityVM.specialitys);
                        specialityRepository.Save();
                       
                        @TempData["AddNewItemMessage"] = "Added successfully....";
                    }
                }
                else
                {
                    ModelState.AddModelError("Speciality.SpecialityName", "Speciality Name Already Exist");
                }
           }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to Add changes. Try again, and if the problem persists contact your system administrator.");
            }
            return RedirectToAction("Create");
        }

        #endregion


        #region Delete Speciality
        [HttpPost]
        public ActionResult BatchDelete(int[] deleteInputs)
        {

            if (deleteInputs != null)
            {
                foreach (var item in deleteInputs)
                {

                    Speciality specialitys = db.Specialitys.Find(item);
                    if (User == null)
                    {
                        // return HttpNotFound();
                    }
                    else
                    {
                        specialitys.InactiveFlag = "Y";
                        specialitys.ModifiedDate = DateTime.Now;

                        specialityRepository.UpdateSpeciality(specialitys);
                        specialityRepository.Save();
                        @TempData["AddNewItemMessage"] = "User having speciality " + specialitys.SpecialityName + " is deleted successfully";

                    }
                    
                       
                    
                }

            }
            return Json("User", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}