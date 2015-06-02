﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class ClinicTimeInformation
    {
        [Key]
        public int ClinicTimeId { get; set; }
        public int LoginId { get; set; }
        public string Day { get; set; }

        public bool FirstSetting { get; set; }

        public bool IsWorkingDay { get; set; }

        public TimeSpan StartTime { get; set; }
        public string StartSlot { get; set; }

        public TimeSpan EndTime { get; set; }
        public string EndSlot { get; set; }

        public int CreatedById { get; set; }

        public System.DateTime CreatedOnDate { get; set; }

        public int ModifiedById { get; set; }

        public System.DateTime ModifiedOnDate { get; set; }
    }
}
