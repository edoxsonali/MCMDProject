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

        public Time time { get; set; }
        public Sec sec { get; set; }
        public timeindicator indicator { get; set; }

        public enum Time
        {
            [Display(Name = "1")]
            one = 1,
            [Display(Name = "2")]
            two = 2 ,
            [Display(Name = "3")]
            three=3,
            [Display(Name = "4")]
            four=4,
            [Display(Name = "5")]
            five=5,
            [Display(Name = "6")]
            six=6,
            [Display(Name = "7")]
            seven=7,
            [Display(Name = "8")]
            eight=8,
            [Display(Name = "9")]
            nine=9,
            [Display(Name = "10")]
            ten=10,
            [Display(Name = "11")]
            eleven=11,
            [Display(Name = "12")]
            twelve=12
        }
        public enum Sec
        {
            [Display(Name = "00")]
            Zero=1,
            [Display(Name = "15")]
            fifteen=2,
            [Display(Name = "30")]
            thirty=3,
            [Display(Name = "45")]
            fortyfive=4,
            [Display(Name = "60")]
            sixty=5
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
