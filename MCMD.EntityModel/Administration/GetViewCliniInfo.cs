using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class GetViewCliniInfo
    {
        public int ClinicInfoId { get; set; }
        public int LoginId { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicPhoneNo { get; set; }
        public int ClinicFees { get; set; }
        public TimeSpan ClinicTimeFrom { get; set; }
        //for geting Duration
        public TimeSpan ClinicTimeTo { get; set; }
        //for geting Autorenaval
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string ClinicServices { get; set; }
    }
}
