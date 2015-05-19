using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityModel;
using MCMD.EntityModel.Administration;
using System.Data.Entity;
using System.Web;



namespace MCMD.EntityRepository.AdminRepository
{
    public class MediaRepository : IMediaRepository
    {
        private ApplicationDbContext DBcontext;

        public MediaRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public IEnumerable<Media> GetMedias()
        {

            return DBcontext.medias.ToList();
        }

        public Media GetMediaByID(int ID)
        {
            return DBcontext.medias.Find(ID);
        }

        public void InsertMedia(Media media, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var Getcount = DBcontext.medias.FirstOrDefault(x => x.LoginId == media.LoginId );
                 
                string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                file.SaveAs(path);
                media.FolderFilePath = path;
                media.UploadType = file.ContentType;
                media.LoginId = 1;// for now we add 1 later we change
                media.InactiveFlag = "N";
                media.CreatedByID = 1; // for now we add 1 later we change
                media.CreatedDate = DateTime.Now;
                media.ModifiedByID = 1;  // for now we add 1 later we change
                media.ModifiedDate = DateTime.Now;
            }


            DBcontext.medias.Add(media);
        }

        public void UpdateMedia(Media media)
        {
            DBcontext.Entry(media).State = EntityState.Modified;
        }

        public void DeleteMedia(int MediaID)
        {
            Media media = DBcontext.medias.Find(MediaID);
            DBcontext.medias.Remove(media);
        }
        public void Save()
        {
            DBcontext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBcontext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
