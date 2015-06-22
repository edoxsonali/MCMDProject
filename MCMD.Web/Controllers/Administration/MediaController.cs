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

        private IMediaRepository MediaRepository;
        public MediaController(IMediaRepository _MediaRepository)
        {
            this.MediaRepository = _MediaRepository;
        }
        // GET: Media
      

        #region Upload Video/Photo
        public ActionResult Create()
        {
            @TempData["Name"] = Session["Name"];
            int Id = (Convert.ToInt32(Session["EditDoctor"]));
            string FirstName = "";
            string LastName = "";
            MediaViewModel _MediaVM = new MediaViewModel();
            _MediaVM.GetMedialist = MediaRepository.GetMedias().Where(x => x.InactiveFlag == "N" && x.LoginId == Id ).ToList();
            _MediaVM.UserLogins = MediaRepository.GetUsers().ToList();

            if (Id != 0)
            {
                List<UserLogin> _NemUser = MediaRepository.GetUsers().Where(x => x.LoginId == Id).ToList();

                foreach (var item in _NemUser)
                {
                    _MediaVM.FirstName = item.FirstName;
                    _MediaVM.LastName = item.LastName;
                    FirstName = item.FirstName;
                    LastName = item.LastName;
                    @TempData["UserName"] = FirstName + " " + LastName;
                }
            }
            return View(_MediaVM);

        }
        [HttpPost]
        public ActionResult Create(Media media, MediaViewModel mediaVM, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                int Id = (Convert.ToInt32(Session["EditDoctor"]));
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

        #endregion


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
                        @TempData["AddNewItemMessage"] = "User having Media is deleted Successfully";

                    }



                }

            }
            return Json("Media", JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}