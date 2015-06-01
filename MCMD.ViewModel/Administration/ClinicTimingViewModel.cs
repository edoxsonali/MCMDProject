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

        public List<daysHours> GetHoursList { get; set; }
        public List<daysec> GetSecList { get; set; }
        public List<daySlot> GetSlotList { get; set; }


        //Monday
        public string HoursMonStart { get; set; }
        public string SecMonStart { get; set; }
        public string HoursMonEnd { get; set; }
        public string SecMonEnd { get; set; }
        public string SlotMonStart { get; set; }
        public string SlotMonEnd { get; set; }

        //Tuesday

        public string HoursTueStart { get; set; }
        public string SecTueStart { get; set; }
        public string HoursTueEnd { get; set; }
        public string SecTueEnd { get; set; }
        public string SlotTueStart { get; set; }
        public string SlotTueEnd { get; set; }

        //Wed

        public string HoursWedStart { get; set; }
        public string SecWedStart { get; set; }
        public string HoursWedEnd { get; set; }
        public string SecWedEnd { get; set; }
        public string SlotWedStart { get; set; }
        public string SlotWedEnd { get; set; }

        //Thu
        public string HoursThuStart { get; set; }
        public string SecThuStart { get; set; }
        public string HoursThuEnd { get; set; }
        public string SecThuEnd { get; set; }
        public string SlotThuStart { get; set; }
        public string SlotThuEnd { get; set; }

        //Friday

        public string HoursFriStart { get; set; }
        public string SecFriStart { get; set; }
        public string HoursFriEnd { get; set; }
        public string SecFriEnd { get; set; }
        public string SlotFriStart { get; set; }
        public string SlotFriEnd { get; set; }

        //Sat
        public string HoursSatStart { get; set; }
        public string SecSatStart { get; set; }
        public string HoursSatEnd { get; set; }
        public string SecSatEnd { get; set; }
        public string SlotSatStart { get; set; }
        public string SlotSatEnd { get; set; }

        //Sunday
        public string HoursSunStart { get; set; }
        public string SecSunStart { get; set; }
        public string HoursSunEnd { get; set; }
        public string SecSunEnd { get; set; }
        public string SlotSunStart { get; set; }
        public string SlotSunEnd { get; set; }






        public string SlotsEnd { get; set; }
        public bool FirstSetting { get; set; }
        public bool IsWorkingDay { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public int[] SelectedMember1 { get; set; }
        //public int[] SelectedMember2 { get; set; }
        //public int[] SelectedMember3 { get; set; }


    }
}
