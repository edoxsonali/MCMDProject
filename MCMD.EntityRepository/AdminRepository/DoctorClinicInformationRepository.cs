using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityModel;
using MCMD.EntityRepository.AdminRepository;
using System.Data.Entity;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;

namespace MCMD.EntityRepository.AdminRepository
{
    public class DoctorClinicInformationRepository : IDoctorClinicInformation
    {
        private ApplicationDbContext DBcontext;
        public DoctorClinicInformationRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<DoctorClinicInformation> GetClinics()
        {
            return DBcontext.DoctorsClinicInfos.ToList();
        }
        public IEnumerable<Country> GetCountrys()
        {
            return DBcontext.countrys.ToList();
        }
        public IEnumerable<State> GetStates()
        {
            return DBcontext.states.ToList();
        }
        public IEnumerable<City> GetCities()
        {
            return DBcontext.cities.ToList();
        }
        public DoctorClinicInformation GetClinicById(int clinicInfoId)
        {
            return DBcontext.DoctorsClinicInfos.Find(clinicInfoId);
        }

        public void InsertClinic(DoctorClinicInformation doctorClinic)
        {
            DBcontext.DoctorsClinicInfos.Add(doctorClinic);
        }
        public void UpdateClinic(DoctorClinicInformation doctorClinic)
        {
            DBcontext.Entry(doctorClinic).State = EntityState.Modified;
        }
        public void DeleteClinic(int ClinicInfoId)
        {
            DoctorClinicInformation DoctorClinic = DBcontext.DoctorsClinicInfos.Find(ClinicInfoId);
            DBcontext.DoctorsClinicInfos.Remove(DoctorClinic);
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
