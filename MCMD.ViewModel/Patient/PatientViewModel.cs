using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Patient;

namespace MCMD.ViewModel.Patient
{
    public class PatientViewModel
    {
        public List<PatientLogin> PatientLogin { get; set; }
        public int? PatientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPhone { get; set; }
        public string InactiveFlag { get; set; }

        public List<ViewPatientDetails> GetPatient { get; set; }

    }
}
