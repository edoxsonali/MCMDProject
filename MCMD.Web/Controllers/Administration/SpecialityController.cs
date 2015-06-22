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
      

        #region Edit speciality
        public ActionResult UserEditSpeciality(int Id)
        {
            Session["EditSpeciality"] = Id;
            var varid = Id;
            return Json(new { varid }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create/View Speciality
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            SpecialityViewModel _specialityVM = new SpecialityViewModel();
            _specialityVM.SpecialityList = specialityRepository.GetSpecialitys().Where(x => x.InactiveFlag == "N").ToList();

            int editInputs = (Session["EditSpeciality"] != null) ? (Convert.ToInt32(Session["EditSpeciality"])) : 0;

            if (editInputs != 0)
            {
                List<Speciality> _NewSpeciality = specialityRepository.GetSpecialitys().Where(x => x.SpecialityID == editInputs).ToList();

                foreach (var item in _NewSpeciality)
                {
                    _specialityVM.SpecialityName = item.SpecialityName;

                   // Session["EditSpeciality"] = null;
                }

            }

            return View(_specialityVM);

        }
     

        [HttpPost]
        public ActionResult Create(SpecialityViewModel specialityVM)
        {
     //     var cSpeciality = specialityRepository.CheckSpeciality(specialityVM.specialitys.SpecialityName);

            var cSpeciality = db.Specialitys.FirstOrDefault(x => x.SpecialityName == specialityVM.SpecialityName);
            try
            {
                if (ReferenceEquals(cSpeciality, null))
                {
                    if (ModelState.IsValid)
                    {

                        var newSpeciality = new Speciality();
                        if (Session["EditSpeciality"] != null)
                        {
                            newSpeciality.SpecialityID = Convert.ToInt32(Session["EditSpeciality"]);
                            newSpeciality.SpecialityName = specialityVM.SpecialityName;
                            newSpeciality.InactiveFlag = "N";
                            newSpeciality.ModifiedDate = DateTime.Now;
                            specialityRepository.UpdateSpeciality(newSpeciality);
                            Session["EditSpeciality"] = null;
                           

                        }else
                        {
                            specialityRepository.InsertSpeciality(specialityVM, specialityVM.specialityss);
                            
                        }
                        specialityRepository.Save();
                        @TempData["SuccessMessage"] = "Added Successfully....";
                       
                    }
                }
                else
                {
                 //   ModelState.AddModelError("Speciality.SpecialityName", "Speciality Name Already Exist");
                    @TempData["Message"] =""+ specialityVM.SpecialityName+ "Speciality Name Already Exist";

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
                        @TempData["SuccessMessage"] = "User having speciality " + specialitys.SpecialityName + " is deleted successfully";

                    }
                    
                       
                    
                }

            }
            return Json("User", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}