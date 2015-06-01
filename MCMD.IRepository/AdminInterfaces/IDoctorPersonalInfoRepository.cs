using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;
using MCMD.EntityModel.Administration;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IDoctorPersonalInfoRepository : IDisposable
    {
        IEnumerable<Speciality> GetSpecialitys();
        IEnumerable<DoctorPersonalInformation> GetDoctors();
        IEnumerable<UserLogin> GetUsers();
        IEnumerable<UserLoginSpeciality> GetUserSpeciality();
        IEnumerable<DoctorPersonalInformation> GetDocInfo();
        DoctorPersonalInformation GetDoctorById(int DoctorId);
        void InsertDoctor(DoctorPersonalInformation Doctor);
        void UpdateDoctorPersonalInfo(DoctorPersonalInformation DoctorPerInfo);
        void UpdateDoctor(DoctorPersonalInformation Doctor);

        void UpdateDocUserLogin(UserLogin userlogins);

        void UpdateDocSpeciality(UserLoginSpeciality userloginrole);
        void DeleteDoctor(int DoctorId);
        void Save();
    }
}
