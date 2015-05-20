using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.Web;
using MCMD.ViewModel.Administration;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IMediaRepository: IDisposable
    {
         IEnumerable<Media> GetMedias();
        Media GetMediaByID(int ID);
        void InsertMedia(Media Media,MediaViewModel mediaVM, HttpPostedFileBase file);
        void UpdateMedia(Media Media);
        void DeleteMedia(int MediaID);
        void Save();

    }
}
