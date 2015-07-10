using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;


namespace MCMD.IRepository.WebInterfaces
{
    public interface IDocProfile : IDisposable
    {
        IEnumerable<DoctorPersonalInformation> GetDocPersonalInfo();
        IEnumerable<UserLogin> GetDocLoginInfo();
        IEnumerable<DoctorClinicInformation> GetDocClinicInfo();
        IEnumerable<ClinicTimeInformation> GetDocClinicTime();
        IEnumerable<City> GetCity();
        IEnumerable<DoctorClinicInformation> GetClinicInformation();
        IEnumerable<GetAllData> SearchAllDoctor(int SpecialityIDVM, int RoleIdVM, int CityIdVM, string UserFirstNameVm, string UserLastNameVM, string @ClinicNameVM);
        IEnumerable<GetViewDoctor> getAllDoctor();
        IEnumerable<Speciality> GetDocSpeciality();
        IEnumerable<UserLoginSpeciality> GetDocLoginSpeciality();
        IEnumerable<Media> GetDocMediaInfo();
        IEnumerable<ClinicTimeInformation> GetAllClinicTime();


    }
}
