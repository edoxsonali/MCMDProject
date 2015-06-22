using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.ViewModel.doctor;
using MCMD.EntityModel.Doctor;


namespace MCMD.IRepository.DoctorInterfaces
{
    public interface IDocRegister : IDisposable
    {

        void InsertDoctor(UserLogin userlogin, DocRegisterViewModel docRegVM);
        IEnumerable<Speciality> GetSpeciality();

        void InsertDoctorRoles(UserLoginRole userloginRole, DocRegisterViewModel docRegVM);

        void DocLoginSpecialitys(UserLoginSpeciality userloginspeciality, DocRegisterViewModel docRegVM);

        void Save();

    }
}
