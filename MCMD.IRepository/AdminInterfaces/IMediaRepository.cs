using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.Web;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IMediaRepository: IDisposable
    {
         IEnumerable<Media> GetMedias();
        Media GetMediaByID(int ID);
        void InsertMedia(Media Media, HttpPostedFileBase file);
        void UpdateMedia(Media Media);
        void DeleteMedia(int MediaID);
        void Save();

    }
}
