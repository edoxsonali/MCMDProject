using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;

namespace MCMD.ViewModel.Administration
{
    public class MediaViewModel
    {
        //sonali
        public Media Medias { get; set; }

        public List<Media> GetMediacount { get; set; }
        public List<Media> GetMedialist { get; set; }
        public List<UserLogin> UserLogins { get; set; }
        public int MediaId { get; set; }
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }


    }
}
