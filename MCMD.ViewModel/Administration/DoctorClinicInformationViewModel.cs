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

        [DisplayName("CountryId")]
        [Required(ErrorMessage = "Country  is required.")]
        public int CountryId { get; set; }

        public List<State> stateList { get; set; }

        [DisplayName("StateId")]
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }
        public List<City> cityList { get; set; }

        [DisplayName("CityId")]
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        // public DoctorClinicInformation DoctorClinicInformations { get; set; }

        public List<UserLogin> UserLogins { get; set; }
        public int LoginId { get; set; }

        [DisplayName("ClinicName")]
        [Required(ErrorMessage = "Clinic Name is required.")]
        public string ClinicName { get; set; }

        [DisplayName("ClinicType")]
        [Required(ErrorMessage = "Clinic Type is required.")]
        public string ClinicType { get; set; }

        [DisplayName("ClinicAddress")]
        [Required(ErrorMessage = "Clinic Address is required.")]
        public string ClinicAddress { get; set; }

        [DisplayName("ClinicPhoneNo")]
        [Required(ErrorMessage = "Clinic Phone Number is required.")]
        public string ClinicPhoneNo { get; set; }

        public int ClinicFees { get; set; }

       
        public int Country { get; set; }

      
        public int State { get; set; }

       
        public int City { get; set; }

        [DisplayName("ZipCode")]
        [Required(ErrorMessage = "Pin Code is required.")]
        public int? ZipCode { get; set; }
        public string ClinicServices { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string AboutClinic { get; set; }
        public string FolderFilePath { get; set; }

        public string UploadType { get; set; }



    }
}

