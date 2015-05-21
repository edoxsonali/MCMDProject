using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.Administration
{
   public  class ClinicTimingViewModel
    {

       public ClinicHours clinicHours { get; set; }
       public Timeslot timeslot { get; set; }
       public enum ClinicHours
       {
          [Display(Name = "01:00 AM")]
           one=1,
          [Display(Name = "02:00 AM")]
           two=2,
          [Display(Name = "03:00 AM")]
           three=3,
          [Display(Name = "04:00 AM")]
           four=4,
          [Display(Name = "05:00 AM")]
           five=5,
          [Display(Name = "06:00 AM")]
           six=6,
          [Display(Name = "07:00 AM")]
           seven=7,
          [Display(Name = "08:00 AM")]
           eight=8,
          [Display(Name = "09:00 AM")]
           nine=9,
          [Display(Name = "10:00 AM")]
           ten=10,
          [Display(Name = "11:00 AM")]
           eleven=11,
          [Display(Name = "12:00 PM")]
           twelve=12,
          [Display(Name = "01:00 PM")]
          c = 13,
          [Display(Name = "02:00 PM")]
          v = 14,
          [Display(Name = "03:00 PM")]
          b = 15,
          [Display(Name = "04:00 PM")]
          n = 16,
          [Display(Name = "05:00 PM")]
          m = 17,
          [Display(Name = "06:00 PM")]
          r = 18,
          [Display(Name = "07:00 PM")]
          t = 19,
          [Display(Name = "08:00 PM")]
          y = 20,
          [Display(Name = "09:00 PM")]
          a = 21,
          [Display(Name = "10:00 PM")]
          d = 22,
          [Display(Name = "11:00 PM")]
          l = 23,
          [Display(Name = "12:00 AM")]
          z = 24

       }
       public enum Timeslot
       {
           AM=1,
           PM=2
       }



    }
}
