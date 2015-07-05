using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.WebInterfaces;
using MCMD.EntityModel;

namespace MCMD.EntityRepository.WebRepository
{
    public class DocProfileRepository : IDocProfile
    {
        private ApplicationDbContext DBcontext;

        public DocProfileRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<DoctorPersonalInformation> GetDocPersonalInfo()
        {
            return DBcontext.DoctorsPersonals.ToList();
        }
        public IEnumerable<UserLogin> GetDocLoginInfo()
        {
            return DBcontext.UserLogins.ToList();
        }
        public IEnumerable<DoctorClinicInformation> GetDocClinicInfo()
        {
            return DBcontext.DoctorsClinicInfos.ToList();
        }
        public IEnumerable<ClinicTimeInformation> GetDocClinicTime()
        {
            return DBcontext.clinicTimeInformation.ToList();
        }

        public IEnumerable<Speciality> GetDocSpeciality()
        {
            return DBcontext.Specialitys.ToList();
        }
        public IEnumerable<UserLoginSpeciality> GetDocLoginSpeciality()
        {
            return DBcontext.UserLoginSpecialitys.ToList();
        }
        public IEnumerable<Media> GetDocMediaInfo()
        {
            return DBcontext.medias.ToList();
        }
        //IEnumerable<ClinicTimeInformation> GetAllClinicTime();
        public IEnumerable<ClinicTimeInformation> GetAllClinicTime()
        {
            return DBcontext.clinicTimeInformation.ToList();
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
