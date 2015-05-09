using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;

namespace MCMD.ViewModel.Administration
{
    public class DoctorClinicInformationViewModel
    {
        public List<Country> countyList { get; set; }
        public List<State> stateList { get; set; }
        public List<City> cityList { get; set; }
        public List<DoctorClinicInformation> doctorClinicList { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int ClinicInfoId { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicPhoneNo { get; set; }
        public int ClinicFees { get; set; }
        public DateTime ClinicTimeFrom { get; set; }
        public DateTime ClinicTimeTo { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string ClinicServices { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string AboutClinic { get; set; }
        public string InactiveFlag { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedByID { get; set; }
        public System.DateTime ModifiedDate { get; set; }

    }
}
