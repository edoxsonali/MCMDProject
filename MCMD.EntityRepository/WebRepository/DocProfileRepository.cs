using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.WebInterfaces;
using MCMD.EntityModel;
using System.Globalization;
using System.Data.SqlClient;

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
        public IEnumerable<City> GetCity()
        {
            var city = (from c in DBcontext.cities
                        orderby c.CityName ascending
                        select c).ToList();
            return city.ToList();
        }
        public IEnumerable<DoctorClinicInformation> GetClinicInformation()
        {
            var clinic = (from c in DBcontext.DoctorsClinicInfos
                          orderby c.ClinicName ascending
                          select c).ToList();
            return clinic.ToList();

        }
        public IEnumerable<GetViewDoctor> getAllDoctor()
        {
            var AllUserInfo = (from n in DBcontext.UserLoginRoles
                               join b in DBcontext.UserLogins on n.LoginId equals b.LoginId
                               join c in DBcontext.Roles on n.RoleId equals c.RoleId
                               join ls in DBcontext.UserLoginSpecialitys on b.LoginId equals ls.LoginId
                               join s in DBcontext.Specialitys on ls.SpecialityID equals s.SpecialityID
                               join d in DBcontext.DoctorsClinicInfos on b.LoginId equals d.LoginId into bd
                               from d in bd.DefaultIfEmpty()
                               join p in DBcontext.DoctorsPersonals on b.LoginId equals p.LoginId into dp
                               from p in dp.DefaultIfEmpty()
                               join u in DBcontext.upgradeServices on b.LoginId equals u.LoginId into bu
                               from u in bu.DefaultIfEmpty()
                               join m in DBcontext.Memberships on u.MembershipId equals m.MembershipId into um
                               from m in um.DefaultIfEmpty()
                               where n.RoleId == 4 && b.InactiveFlag == "N"
                               orderby b.FirstName ascending
                               orderby d.ClinicName ascending
                               select new
                               {
                                   LoginId = b.LoginId == null ? 0 : b.LoginId,
                                   FirstName = b.FirstName == null ? null : b.FirstName,
                                   LastName = b.LastName == null ? null : b.LastName,
                                   Speciality = s.SpecialityName == null ? null : s.SpecialityName,
                                   ClinicName = d.ClinicName == null ? null : d.ClinicName,
                                   ClinicAddress = d.ClinicAddress == null ? null : d.ClinicAddress,
                                   ClinicPhoneNo = d.ClinicPhoneNo == null ? null : d.ClinicPhoneNo,
                                   ClinicFees = d.ClinicFees == null ? 0 : d.ClinicFees,
                                   City = d.City == null ? 0 : d.City,
                                   ZipCode = d.ZipCode == null ? 0 : d.ZipCode,
                                   ClinicServices = d.ClinicServices == null ? null : d.ClinicServices,
                                   AwardsAndRecognization = d.AwardsAndRecognization == null ? null : d.AwardsAndRecognization,
                                   AboutClinic = d.AboutClinic == null ? null : d.AboutClinic,
                                   MiddleName = p.MiddleName == null ? null : p.MiddleName,
                                   Qualification = p.Qualification == null ? null : p.Qualification,
                                   RegistrationNo = p.RegistrationNo == null ? null : p.RegistrationNo,
                                   Affiliation = p.Affiliation == null ? null : p.Affiliation,
                                   AboutMe = p.AboutMe == null ? null : p.AboutMe,
                                   ExperienceInYear = p.ExperienceInYear == null ? null : p.ExperienceInYear,
                                   ExperienceInMonth = p.ExperienceInMonth == null ? null : p.ExperienceInMonth,
                                   MembershipType = m.MembershipType == null ? null : m.MembershipType,
                                   EmailID = b.EmailID == null ? null : b.EmailID,
                                   MobileNo = b.UserPhone == null ? null : b.UserPhone,
                                   Role = c.RoleName == null ? null : c.RoleName
                               }).ToList();

            List<GetViewDoctor> allUsers = new List<GetViewDoctor>();

            foreach (var item in AllUserInfo)
            {
                var s = new GetViewDoctor();
                s.LoginId = item.LoginId;
                s.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FirstName + " " + item.LastName);
                s.LastName = item.LastName;
                s.SpecialityName = item.Speciality;
                s.ClinicName = item.ClinicName;
                s.ClinicAddress = item.ClinicAddress;
                s.ClinicPhoneNo = item.ClinicPhoneNo;
                s.ClinicFees = item.ClinicFees;
                s.City = item.City;
                s.ZipCode = item.ZipCode;
                s.ClinicServices = item.ClinicServices;
                s.AwardsAndRecognization = item.AwardsAndRecognization;
                s.AboutClinic = item.AboutClinic;
                s.MiddleName = item.MiddleName;
                s.Qualification = item.Qualification;
                s.RegistrationNo = item.RegistrationNo;
                s.Affiliation = item.Affiliation;
                s.AboutMe = item.AboutMe;
                s.ExperienceInYear = item.ExperienceInYear;
                s.ExperienceInMonth = item.ExperienceInMonth;
                s.MembershipType = item.MembershipType;
                s.EmailID = item.EmailID;
                s.UserPhone = item.MobileNo;
                s.RoleName = item.Role;

                allUsers.Add(s);

            }

            return allUsers.OrderBy(x => x.FirstName).ToList();
        }

        public IEnumerable<GetAllData> SearchAllDoctor(int SpecialityIDVM, int RoleIdVM, int CityIdVM, string UserFirstNameVm, string UserLastNameVM, string @ClinicNameVM)
        {
            var UserInfo = DBcontext.Database.SqlQuery<GetAllData>("SearchDoctor @SpecialityID, @RoleId,@CityId, @FirstName,@LastName,@ClinicName",
                                                             new SqlParameter("SpecialityID", SpecialityIDVM),
                                                             new SqlParameter("RoleId", RoleIdVM),
                                                             new SqlParameter("CityId", CityIdVM),
                                                             new SqlParameter("FirstName", UserFirstNameVm),
                                                             new SqlParameter("LastName", UserLastNameVM),
                                                             new SqlParameter("ClinicName", ClinicNameVM)


                                                      ).ToList();
            //OrderByDescending(x => x.LoginId).ToList();


            return UserInfo.ToList();

        }

        public IEnumerable<Role> GetRoles()
        {
            return DBcontext.Roles.ToList();
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
