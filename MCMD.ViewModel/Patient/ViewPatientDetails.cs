using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.Patient
{
    public class ViewPatientDetails
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPhone { get; set; }
        public string InactiveFlag { get; set; }

    }
}
