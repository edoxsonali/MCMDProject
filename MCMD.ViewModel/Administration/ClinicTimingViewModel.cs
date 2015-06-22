using MCMD.EntityModel.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MCMD.ViewModel.Administration
{
    public class ClinicTimingViewModel
    {

        public List<UserLogin> UserLogins { get; set; }
        public int LoginId { get; set; }

        public List<DaysCheckList> GetCheckList { get; set; }
        public bool DayChecked { get; set; }

        

        public int[] SelectedMember1 { get; set; }
        

        public string StartTimeMon { get; set; }
        public string EndtTimeMon { get; set; }
        public string StartTimeTue { get; set; }
        public string EndtTimeTue { get; set; }
        public string StartTimeWed { get; set; }
        public string EndtTimeWed { get; set; }
        public string StartTimeThu { get; set; }
        public string EndtTimeThu { get; set; }
        public string StartTimeFri { get; set; }
        public string EndtTimeFri { get; set; }
        public string StartTimeSat { get; set; }
        public string EndtTimeSat { get; set; }
        public string StartTimeSun { get; set; }
        public string EndtTimeSun { get; set; }


    }
}
