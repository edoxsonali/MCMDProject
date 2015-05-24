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
        // public DoctorClinicInformation DoctorClinicInformations { get; set; }

        public List<UserLogin> UserLogins { get; set; }
        public int LoginId { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicPhoneNo { get; set; }
        public int ClinicFees { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int ZipCode { get; set; }
        public string ClinicServices { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string AboutClinic { get; set; }


    }   
}

