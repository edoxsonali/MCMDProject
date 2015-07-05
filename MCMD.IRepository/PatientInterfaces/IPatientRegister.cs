using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Patient;
using MCMD.ViewModel.Patient;

namespace MCMD.IRepository.PatientInterfaces
{
    public interface  IPatientRegister : IDisposable
    {
        void InsertPatient(PatientLogin patientlogin);
        void UpdatePatient(PatientLogin patientLogin);
        IEnumerable<ViewPatientDetails> GetPatient();
        IEnumerable<ViewPatientDetails> SearchPatient(int PatientId, string FirstName, string LastName, string EmailId, string UsePhone); 
        void Save();
    }
}
