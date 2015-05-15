using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System.ComponentModel.DataAnnotations;

namespace MCMD.ViewModel.Administration
{
    public class DoctorClinicInformationViewModel
    {
        public List<Country> countyList { get; set; }
        public List<State> stateList { get; set; }
        public List<City> cityList { get; set; }
        public DoctorClinicInformation DoctorClinicInformations { get; set; }

        public List<Time> timelist { get; set; }
        public Time time { get; set; }
      //  public Sec sec { get; set; }
        public timeindicator indicator { get; set; }

        public enum Time
        {
            [Display(Name = "01:00")]
            one=1,
            [Display(Name = "02:00")]
            two=2,
            [Display(Name = "03:00")]
            three=3,
            [Display(Name = "04:00")]
            four=4,
            [Display(Name = "05:00")]
            five=5,
            [Display(Name = "06:00")]
            six=6,
            [Display(Name = "07:00")]
            seven=7,
            [Display(Name = "08:00")]
            eight=8,
            [Display(Name = "09:00")]
            nine=9,
            [Display(Name = "10:00")]
            ten=10,
            [Display(Name = "11:00")]
            eleven=11,
            [Display(Name = "12:00")]
            twelve=12
        }
     
        public enum timeindicator
        {
             [Display(Name = "AM")]
            AM=1,
             [Display(Name = "PM")]
            PM=2

        }
    }
}
