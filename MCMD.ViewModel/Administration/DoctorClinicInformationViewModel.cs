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
    public enum ClinicHours 
    {
       
        Siddu =1
       
    }

    }   
}

