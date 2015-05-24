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
       public timeslot timeslots { get; set; }

       public slot slots { get; set; }
       public enum ClinicHours
       {
          
          [Display(Name = "12 ")]
           twelve=12,
          [Display(Name = "01 ")]
          c = 13,
          [Display(Name = "02 ")]
          v = 14,
          [Display(Name = "03")]
          b = 15,
          [Display(Name = "04")]
          n = 16,
          [Display(Name = "05")]
          m = 17,
          [Display(Name = "06")]
          r = 18,
          [Display(Name = "07")]
          t = 19,
          [Display(Name = "08")]
          y = 20,
          [Display(Name = "09")]
          a = 21,
          [Display(Name = "10")]
          d = 22,
          [Display(Name = "11")]
          l = 23,
        

       }
       public enum timeslot
       {

           [Display(Name = "00 ")]
           twelve = 12,
           [Display(Name = "15 ")]
           c = 13,
           [Display(Name = "30 ")]
           v = 14,
           [Display(Name = "45")]
           b = 15,

       }
       public enum slot
       {
           AM=1,
           PM=2
       }



    }
}
