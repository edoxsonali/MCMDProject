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
using MCMD.ViewModel.Administration;
using System.IO;


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

        public void InsertMedia(Media media, MediaViewModel mediaVM, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string[] formats = new string[] { "image/jpeg", "image/png", "image/gif", "image/Bmp" };
                string[] videoFormat = new string[] { "video/mp4" };


                int CheckImgType = Convert.ToInt32(formats.Contains(file.ContentType));
                int CheckVideoType = Convert.ToInt32(videoFormat.Contains(file.ContentType));

                if (CheckImgType != 0 || CheckVideoType != 0)
                {
                    if (CheckImgType != 0)
                    {
                        mediaVM.GetMediacount = DBcontext.medias.Where(x => x.LoginId == mediaVM.LoginId && x.InactiveFlag == "N").ToList();
                        if (mediaVM.GetMediacount.Count() < 2)
                        {
                            string Imgpath = "~/Media/" + file.FileName;
                            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                            file.SaveAs(path);
                            media.LoginId = mediaVM.LoginId;
                            media.FolderFilePath = Imgpath;
                            media.UploadType = file.ContentType;
                            media.InactiveFlag = "N";
                            media.CreatedByID = 1; // for now we add 1 later we change
                            media.CreatedDate = DateTime.Now;
                            media.ModifiedByID = 1;  // for now we add 1 later we change
                            media.ModifiedDate = DateTime.Now;
                            DBcontext.medias.Add(media);

                            mediaVM.Message = "Succsessfully save data";

                        }
                        else
                        {
                            mediaVM.Message = "Not Allowed to uploade more than 2 image";
                        }
                    }

                    if (CheckVideoType != 0)
                    {
                        string filetype = file.ContentType;
                        mediaVM.GetMediacount = DBcontext.medias.Where(x => x.LoginId == mediaVM.LoginId && x.UploadType == filetype && x.InactiveFlag == "N").ToList();
                        if (mediaVM.GetMediacount.Count() < 1)
                        {
                            //if(file.ContentLength < 1024 * 1024 * 1)
                            //{

                            string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Media/") + file.FileName);
                            file.SaveAs(path);
                            media.FolderFilePath = path;
                            media.LoginId = mediaVM.LoginId;
                            media.UploadType = file.ContentType;
                            media.InactiveFlag = "N";
                            media.CreatedByID = 1; // for now we add 1 later we change
                            media.CreatedDate = DateTime.Now;
                            media.ModifiedByID = 1;  // for now we add 1 later we change
                            media.ModifiedDate = DateTime.Now;
                            DBcontext.medias.Add(media);
                            mediaVM.Message = "Succsessfully save data";
                            //   }

                        }
                        else
                        {
                            mediaVM.Message = "Not Allowed to uploade more than 1 Video";
                        }
                    }
                }
                else
                {
                    mediaVM.Message = "This not Image/Video file type";
                }
               

            }


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
