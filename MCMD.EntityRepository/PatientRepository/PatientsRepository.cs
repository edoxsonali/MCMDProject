using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.IRepository.PatientInterfaces;
using MCMD.EntityModel;
using MCMD.EntityModel.Patient;
using MCMD.ViewModel.Patient;
using System.Data.SqlClient;
using System.Data.Entity;

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

         public IEnumerable<ViewPatientDetails> GetPatient()
         {
             var AllPatient = (from n in DBcontext.patientlogins
                               where n.InactiveFlag == "N"
                               select new
                               {
                                   PatientLoginId = n.PatientId,
                                   FirstName = n.FirstName,
                                   LastName = n.LastName,
                                   EmailID = n.EmailID,
                                   UserPhone = n.UserPhone


                               }).ToList();

             List<ViewPatientDetails> PatientList = new List<ViewPatientDetails>();

             foreach (var item in AllPatient)
             {
                 var s = new ViewPatientDetails();
                 s.PatientId = item.PatientLoginId;
                 s.FirstName = item.FirstName;
                 s.LastName = item.LastName;
                 s.EmailID = item.EmailID;
                 s.UserPhone = item.UserPhone;
                 PatientList.Add(s);

             }
             return PatientList.ToList();


         }
         public IEnumerable<ViewPatientDetails> SearchPatient(int PatientId, string FirstName, string LastName, string EmailId, string UsePhone)
         {
             var PatientInfo = DBcontext.Database.SqlQuery<ViewPatientDetails>("GetViewPatient @PatientId,@FirstName, @LastName,@EmailID,@UserPhone",
                                                         new SqlParameter("PatientId", PatientId),
                                                         new SqlParameter("FirstName", FirstName),
                                                         new SqlParameter("LastName", LastName),
                                                         new SqlParameter("EmailID", EmailId),
                                                         new SqlParameter("UserPhone", UsePhone)

                                                      ).ToList();
             //.OrderByDescending(x => x.LoginId).ToList();


             return PatientInfo.ToList();
         }
         public void UpdatePatient(PatientLogin patientLogin)
         {

             DBcontext.Entry(patientLogin).State = EntityState.Modified;
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
