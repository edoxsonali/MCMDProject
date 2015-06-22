using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Patient;

namespace MCMD.IRepository.PatientInterfaces
{
    public interface  IPatientRegister : IDisposable
    {
        void InsertPatient(PatientLogin patientlogin);
        void Save();
    }
}
