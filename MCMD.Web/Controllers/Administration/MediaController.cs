using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel;


namespace MCMD.Web.Controllers.Administration
{
    public class MediaController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: Media
        public ActionResult Index()
        {
            var customer = db.medias.ToList();
            return View(customer); 
        }

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

                    string path = string.Format("{0}/{1}{2}", System.IO.Path.Combine(
                                   Server.MapPath("~/Media/")+file.FileName));
                    file.SaveAs(path);
                    media.FolderFilePath = path;
                    media.UploadType = file.ContentType;
                    media.LoginId = 1;// for now we add 1 later we change
                }
                db.medias.Add(media);
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(media);
           
        }
    }
}