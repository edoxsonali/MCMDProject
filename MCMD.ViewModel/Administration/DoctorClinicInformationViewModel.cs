using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MCMD.ViewModel.Administration
{
    public class DoctorClinicInformationViewModel
    {
        public List<Country> countyList { get; set; }
        public int CountryId { get; set; }
        public List<State> stateList { get; set; }
        public int StateId { get; set; }
        public List<City> cityList { get; set; }
        public int CityId { get; set; }
        // public DoctorClinicInformation DoctorClinicInformations { get; set; }

        public List<UserLogin> UserLogins { get; set; }
        public int LoginId { get; set; }
        [DisplayName("ClinicName")]
        [Required(ErrorMessage = "Clinic Name is required.")]
        public string ClinicName { get; set; }
        [DisplayName("ClinicAddress")]
        [Required(ErrorMessage = "Clinic Address is required.")]
        public string ClinicAddress { get; set; }
        [DisplayName("ClinicPhoneNo")]
        [Required(ErrorMessage = "Clinic Phone Number is required.")]
        public string ClinicPhoneNo { get; set; }
        public int ClinicFees { get; set; }
        [DisplayName("Country")]
        [Required(ErrorMessage = "Country  is required.")]
        public int Country { get; set; }
        [DisplayName("State")]
        [Required(ErrorMessage = "State is required.")]
        public int State { get; set; }
        [DisplayName("City")]
        [Required(ErrorMessage = "City is required.")]
        public int City { get; set; }
        [DisplayName("ZipCode")]
        [Required(ErrorMessage = "Pin Code is required.")]
        public int? ZipCode { get; set; }
        public string ClinicServices { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string AboutClinic { get; set; }


    }
}

