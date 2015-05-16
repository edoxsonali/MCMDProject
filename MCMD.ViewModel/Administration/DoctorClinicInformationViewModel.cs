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

        //public List<clinicHours> timelist { get; set; }
        public ClinicHours clinichours { get; set; }
        //public timeindicator indicator { get; set; }
    public enum ClinicHours : int
    {
       
        01:00AM=1,
        02:00AM=2,
        03:00AM=3,
        04:00AM=4,
        05:00AM=5,
        06:00AM=6,
        07:00AM=7,
        08:00AM=8,
        09:00AM=9, 
        10:00AM=10,
        11:00AM=11,
        12:00AM=12
    }

    }   
}

