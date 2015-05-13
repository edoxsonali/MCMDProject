using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;

namespace MCMD.ViewModel.Administration
{
    public class MediaDetailsViewModel
    {
           public List<Media> MediaList { get; set; }
         public int MediaId { get; set; }
         public Media Medias { get; set; }
    
    }
}
