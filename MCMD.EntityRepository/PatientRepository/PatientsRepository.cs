using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.IRepository.PatientInterfaces;
using MCMD.EntityModel;
using MCMD.EntityModel.Patient;

namespace MCMD.EntityRepository.PatientRepository
{
    public class PatientsRepository : IPatientRegister
    {
         private ApplicationDbContext DBcontext;

         public PatientsRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

         public void InsertPatient(PatientLogin patientlogin)
         {

             var crypto = new SimpleCrypto.PBKDF2();
             var encrypPass = crypto.Compute(patientlogin.Password);
             patientlogin.Password = encrypPass;
             patientlogin.ConfirmPassword = encrypPass;
             patientlogin.PasswordSalt = crypto.Salt;
             patientlogin.FirstName = patientlogin.FirstName;
             patientlogin.LastName = patientlogin.LastName;
             patientlogin.EmailID = patientlogin.EmailID;
             patientlogin.IslockedOut = false;
             patientlogin.InactiveFlag = "N";
             patientlogin.CreatedByID = 1; // for now we add 1 later we change
             patientlogin.CreatedDate = DateTime.Now;
             patientlogin.ModifiedByID = 1;  // for now we add 1 later we change
             patientlogin.ModifiedDate = DateTime.Now;
             patientlogin.UserPhone = patientlogin.UserPhone;

             DBcontext.patientlogins.Add(patientlogin);
         }
         public void Save()
         {
             DBcontext.SaveChanges();
         }
         private bool disposed = false;

         protected virtual void Dispose(bool disposing)
         {
             if (!this.disposed)
             {
                 if (disposing)
                 {
                     DBcontext.Dispose();
                 }
             }
             this.disposed = true;
         }

         public void Dispose()
         {
             Dispose(true);
             GC.SuppressFinalize(this);
         }
    }
}
