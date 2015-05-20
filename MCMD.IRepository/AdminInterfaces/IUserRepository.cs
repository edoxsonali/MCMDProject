using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.ViewModel.Administration;
using MCMD.EntityModel.Doctor;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IUserRepository : IDisposable
    {

        IEnumerable<GetViewUsers> GetAllUser();
        IEnumerable<GetViewDoctor> getAllDoctor();
        IEnumerable<Speciality> GetSpecialitys();
        IEnumerable<Role> GetRoles();
        IEnumerable<UserLogin> GetAllUserData();
        IEnumerable<UserLoginRole> GetUserLoginRole();
        IEnumerable<DoctorClinicInformation> GetClinicInformation();

        IEnumerable<GetViewUsers> SearchUser(int RoleIdVM, int EmpIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM);

        IEnumerable<GetViewDoctor> SearchDoctor(int RoleIdVM, int SpeIDVM, int EmpIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM, int ClinicidVM);
        UserLogin GetUserByID(int UserId);

        void InsertUserLogins(UserLogin userlogin, UserRegisterViewModel registerVM);

        void InsertUserLoginRoles(UserLoginRole userloginRole, UserRegisterViewModel registerVM);
       
        void UserLoginSpecialitys(UserLoginSpeciality userloginspeciality, UserRegisterViewModel registerVM);   
   
        void UpdateUser(UserLogin userlogin);
        void UpdateUserRole(UserLoginRole userloginRole);
        void DeleteUser(int UserID);
        void Save();
   

    }
}
