using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel;
using MCMD.ViewModel.Administration;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;


namespace MCMD.Web.Controllers.Administration
{
    public class MediaController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();

         private IMediaRepository  MediaRepository;
         public MediaController(IMediaRepository _MediaRepository)
        {
            this.MediaRepository = _MediaRepository;
        }
        // GET: Media
        public ActionResult Index()
        {
            var customer = db.medias.ToList();
            return View(customer); 
        }

        #region Create User
        public ActionResult Create()
        {

            return View(); 

        }
        [HttpPost]
        public ActionResult Create(Media media,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    MediaRepository.InsertMedia(media,file);
                    MediaRepository.Save();
                       
                return RedirectToAction("Create");
                }
            }
            return View(media);

        }

        #endregion
        public ActionResult ViewMedia(MediaDetailsViewModel mediaDetailVM)
        {
            mediaDetailVM.MediaList = MediaRepository.GetMedias().Where(x => x.InactiveFlag == "N").ToList();
            return View(mediaDetailVM);
        }

        #region Delete Speciality
        [HttpPost]
        public ActionResult BatchDelete(int[] deleteInputs)
        {

            if (deleteInputs != null)
            {
                foreach (var item in deleteInputs)
                {

                    Media medias = db.medias.Find(item);
                    if (User == null)
                    {
                        // return HttpNotFound();
                    }
                    else
                    {
                        medias.InactiveFlag = "Y";
                        medias.ModifiedDate = DateTime.Now;

                        MediaRepository.UpdateMedia(medias);
                        MediaRepository.Save();
                        @TempData["AddNewItemMessage"] = "User having Media is deleted successfully";

                    }



                }

            }
            return Json("Media", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}