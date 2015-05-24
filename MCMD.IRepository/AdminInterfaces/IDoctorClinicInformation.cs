using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IDoctorClinicInformation : IDisposable
    {
        //Declare all Entities Collections
        IEnumerable<DoctorClinicInformation> GetClinic();
        IEnumerable<Country> GetCountrys();
        IEnumerable<State> GetStates();
        IEnumerable<City> GetCities();
        IEnumerable<UserLogin> GetUsers();
        IEnumerable<GetViewCliniInfo> GetClinics();
        DoctorClinicInformation GetClinicById(int clinicInfoId);
        void InsertClinic(DoctorClinicInformation doctorClinic);
        void UpdateClinic(DoctorClinicInformation doctorClinic);
        void DeleteClinic(int ClinicInfoId);
        void Save();



    }
}
