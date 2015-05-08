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
    
        IEnumerable<UserInfo> GetAllUser();
        IEnumerable<UserInfo> getAllDoctor();
        IEnumerable<Speciality> GetSpecialitys();
        IEnumerable<Role> GetRoles();

        IEnumerable<UserInfo> SearchUser(int EmpIdVM, int RoleIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM);
        UserLogin GetUserByID(int UserId);

        void InsertUserLogins(UserLogin userlogin, UserRegisterViewModel registerVM);

        void InsertUserLoginRoles(UserLoginRole userloginRole, UserRegisterViewModel registerVM);
       
        void UserLoginSpecialitys(UserLoginSpeciality userloginspeciality, UserRegisterViewModel registerVM);   
   
        void UpdateUser(UserLogin userlogin);
        void DeleteUser(int UserID);
        void Save();
   

    }
}
