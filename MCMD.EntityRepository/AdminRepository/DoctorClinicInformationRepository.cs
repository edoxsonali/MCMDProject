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
    public class DoctorClinicInformationRepository : IDoctorClinicInformation
    {
        private ApplicationDbContext DBcontext;
        public DoctorClinicInformationRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<DoctorClinicInformation> GetClinic()
        {

            return DBcontext.DoctorsClinicInfos.ToList();
        }
        public IEnumerable<GetViewCliniInfo> GetClinics()
        {
            var AllClinicInfo = (from n in DBcontext.DoctorsClinicInfos
                                 join b in DBcontext.cities on n.City equals b.CityId
                                 join c in DBcontext.states on n.State equals c.StateId
                                 where n.InactiveFlag == "N"
                                 select new
                                 {
                                     ClinicInfoId=n.ClinicInfoId,
                                     LoginId = n.LoginId,
                                     ClinicName = n.ClinicName,
                                     ClinicAddress = n.ClinicAddress,
                                     ClinicPhoneNo = n.ClinicPhoneNo,
                                     ClinicFees = n.ClinicFees,
                                  //   ClinicTimeFrom = n.ClinicTimeFrom,
                                  //   ClinicTimeTo = n.ClinicTimeTo,
                                     StateName = c.StateName,
                                     CityName = b.CityName,
                                     ClinicServices = n.ClinicServices
                                 }).ToList();
            List<GetViewCliniInfo> allClinicInfo = new List<GetViewCliniInfo>();


            foreach (var item in AllClinicInfo)
            {
                var s = new GetViewCliniInfo();

                s.ClinicInfoId = item.ClinicInfoId;
                s.LoginId = item.LoginId;
                s.ClinicName = item.ClinicName;
                s.ClinicAddress = item.ClinicAddress;
                s.ClinicPhoneNo = item.ClinicPhoneNo;
                s.ClinicFees = item.ClinicFees;
              //  s.ClinicTimeFrom = item.ClinicTimeFrom;
              //  s.ClinicTimeTo = item.ClinicTimeTo;
                s.StateName = item.StateName;
                s.CityName = item.CityName;
                s.ClinicServices = item.ClinicServices;
               
                allClinicInfo.Add(s);

            }

            return allClinicInfo.ToList();
           // return DBcontext.DoctorsClinicInfos.ToList();
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
        public IEnumerable<UserLogin> GetUsers()
        {
            return DBcontext.UserLogins.ToList();
        }
        public DoctorClinicInformation GetClinicById(int clinicInfoId)
        {
            return DBcontext.DoctorsClinicInfos.Find(clinicInfoId);
        }

        public void InsertClinic(DoctorClinicInformation doctorClinic)
        {
            DBcontext.DoctorsClinicInfos.Add(doctorClinic);
        }
        public void InsertClinicTime(ClinicTimeInformation dctrClncTime)
        {
            DBcontext.clinicTimeInformation.Add(dctrClncTime);
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
