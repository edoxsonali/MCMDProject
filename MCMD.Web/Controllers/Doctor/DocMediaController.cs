using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.ViewModel.Administration;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityRepository.AdminRepository;
using MCMD.EntityModel;
using MCMD.EntityModel.Administration;



namespace MCMD.Web.Controllers.Doctor
{
    public class DocMediaController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        private IMediaRepository MediaRepository;
        public DocMediaController(IMediaRepository _MediaRepository)
        {
            this.MediaRepository = _MediaRepository;
        }
        // GET: DocMedia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            //Session["Doctor"] = 3;
            int Id = (Convert.ToInt32(Session["Doctor"]));
            MediaViewModel _MediaVM = new MediaViewModel();
            _MediaVM.GetMedialist = MediaRepository.GetMedias().Where(x => x.InactiveFlag == "N" && x.LoginId == Id).ToList();
            return View(_MediaVM);
        }
        [HttpPost]
        public ActionResult Create(Media media, MediaViewModel mediaVM, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                int Id = (Convert.ToInt32(Session["Doctor"]));
                if (file != null)
                {

                    Session["Media"] = Id;
                    int MediaSession = Convert.ToInt32(Session["Media"]);
                    mediaVM.LoginId = MediaSession;
                    MediaRepository.InsertMedia(media, mediaVM, file);
                    MediaRepository.Save();

                    @TempData["SuccessMessage"] = mediaVM.Message;

                    Session["Media"] = null;
                    return RedirectToAction("Create");
                }
            }
            return View(mediaVM);

        }

        #region Delete Media
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