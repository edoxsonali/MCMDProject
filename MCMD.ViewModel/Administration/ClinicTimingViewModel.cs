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

        public List<ClinicTimeInformation> getClinicTimie { get; set; }
        public string Setting { get; set; }

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

        public List<ClinicTimeInformation> GetclinicTimeFirst { get; set; }
        public List<ClinicTimeInformation> GetclinicTimeSecond { get; set; }
        public List<ClinicTimeInformation> GetclinicTimeThird { get; set; }

        public List<GetAllTime> GetTime1 { get; set; }
        public List<GetAllTime> GetTime2 { get; set; }

      //first seating time
        public string StartTimefs1 { get; set; }
        public string EndTimefs1 { get; set; }
        public string StartTimefs2 { get; set; }
        public string EndTimefs2 { get; set; }
        public string StartTimefs3 { get; set; }
        public string EndTimefs3 { get; set; }
        public string StartTimefs4 { get; set; }
        public string EndTimefs4 { get; set; }
        public string StartTimefs5 { get; set; }
        public string EndTimefs5 { get; set; }
        public string StartTimefs6 { get; set; }
        public string EndTimefs6 { get; set; }
        public string StartTimefs7 { get; set; }
        public string EndTimefs7 { get; set; }

        //Second seating time
        public string StartTimess1 { get; set; }
        public string EndTimess1 { get; set; }
        public string StartTimess2 { get; set; }
        public string EndTimess2 { get; set; }
        public string StartTimess3 { get; set; }
        public string EndTimess3 { get; set; }
        public string StartTimess4 { get; set; }
        public string EndTimess4 { get; set; }
        public string StartTimess5 { get; set; }
        public string EndTimess5 { get; set; }
        public string StartTimess6 { get; set; }
        public string EndTimess6 { get; set; }
        public string StartTimess7 { get; set; }
        public string EndTimess7 { get; set; }

        //third seating time
        public string StartTimets1 { get; set; }
        public string EndTimets1 { get; set; }
        public string StartTimets2 { get; set; }
        public string EndTimets2 { get; set; }
        public string StartTimets3 { get; set; }
        public string EndTimets3 { get; set; }
        public string StartTimets4 { get; set; }
        public string EndTimets4 { get; set; }
        public string StartTimets5 { get; set; }
        public string EndTimets5 { get; set; }
        public string StartTimets6 { get; set; }
        public string EndTimets6 { get; set; }
        public string StartTimets7 { get; set; }
        public string EndTimets7 { get; set; }
      


    }
}
