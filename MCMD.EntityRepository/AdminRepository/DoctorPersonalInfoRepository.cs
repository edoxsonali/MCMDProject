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

namespace MCMD.EntityRepository.AdminRepository
{
    public class DoctorPersonalInfoRepository : IDoctorPersonalInfoRepository
    {
        private ApplicationDbContext DBcontext;
        public DoctorPersonalInfoRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<DoctorPersonalInformation> GetDoctors()
        {
            return DBcontext.DoctorsPersonals.ToList();
        }
        public IEnumerable<UserLogin> GetUsers()
        {
            return DBcontext.UserLogins.ToList();
        }
        public IEnumerable<DoctorPersonalInformation> GetDocInfo()
        {
            return DBcontext.DoctorsPersonals.ToList();
        }
        public IEnumerable<UserLoginSpeciality> GetUserSpeciality()
        {
            return DBcontext.UserLoginSpecialitys.ToList();
        }

        public DoctorPersonalInformation GetDoctorById(int DoctorId)
        {
            return DBcontext.DoctorsPersonals.Find(DoctorId);
        }
        public IEnumerable<Speciality> GetSpecialitys()
        {

            return DBcontext.Specialitys.ToList();
        }
        public void InsertDoctor(DoctorPersonalInformation Doctor)
        {
            DBcontext.DoctorsPersonals.Add(Doctor);
        }
        public void UpdateDoctorPersonalInfo(DoctorPersonalInformation DoctorPerInfo)
        {
            DBcontext.Entry(DoctorPerInfo).State = EntityState.Modified;
        }
        public void UpdateDoctor(DoctorPersonalInformation Doctor)
        {
            DBcontext.Entry(Doctor).State = EntityState.Modified;
        }
        public void UpdateDocUserLogin(UserLogin userlogins)
        {
            DBcontext.Entry(userlogins).State = EntityState.Modified;
        }
        public void UpdateDocSpeciality(UserLoginSpeciality userloginrole)
        {
            DBcontext.Entry(userloginrole).State = EntityState.Modified;
        }
        public void DeleteDoctor(int DoctorId)
        {
            DoctorPersonalInformation Doctors = DBcontext.DoctorsPersonals.Find(DoctorId);
            DBcontext.DoctorsPersonals.Remove(Doctors);
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
