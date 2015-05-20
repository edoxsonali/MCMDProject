﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;

namespace MCMD.ViewModel.Administration
{
    public class MediaViewModel
    {
        public Media Medias { get; set; }

        public List<Media> GetMediacount { get; set; }
        public List<Media> GetMedialist { get; set; }
        public int MediaId { get; set; }
        public int LoginId { get; set; }
        public string Message { get; set; }


    }
}
