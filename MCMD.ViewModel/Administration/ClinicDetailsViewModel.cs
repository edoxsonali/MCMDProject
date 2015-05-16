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
    public class ClinicDetailsViewModel
    {
        public List<Country> countyList { get; set; }
        public List<State> stateList { get; set; }
        public List<City> cityList { get; set; }
        public List<GetViewCliniInfo> clinicinfo { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int ClinicInfoId { get; set; }
            
    }
}
