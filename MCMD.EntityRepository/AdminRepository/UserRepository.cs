using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.IRepository.AdminInterfaces;
using MCMD.EntityModel;
using System.Data.Entity;
using MCMD.ViewModel.Administration;
using MCMD.EntityModel.Doctor;

namespace MCMD.EntityRepository.AdminRepository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext DBcontext;

        public UserRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        
        public IEnumerable<UserInfo> GetAllUser()
        {
            
            var AllUserInfo = (from n in DBcontext.UserLoginRoles
                               join b in DBcontext.UserLogins on n.LoginId equals b.LoginId
                               join c in DBcontext.Roles on n.RoleId equals c.RoleId
                               where n.RoleId != 4 && b.InactiveFlag == "N"
                               select new
                               {
                                   LoginId = b.LoginId,
                                   UserName = b.UserName,
                                   FirstName = b.FirstName,
                                   LastName = b.LastName,
                                   EmailID = b.EmailID,
                                   EmployeeId=b.EmployeeId,
                                   MobileNo = b.UserPhone,
                                   Role = c.Name
                               }).ToList();

            List<UserInfo> allUsers = new List<UserInfo>();

            foreach (var item in AllUserInfo)
            {
                var s = new UserInfo();
                s.LoginId = item.LoginId;
                s.UserName = item.UserName;
                s.FirstName = item.FirstName;
                s.LastName = item.LastName;
                s.EmailID = item.EmailID;
                s.EmployeeId = item.EmployeeId;
                s.UserPhone =item.MobileNo;
                s.Name = item.Role;
                allUsers.Add(s);

            }

           return allUsers.ToList();

        }

        public IEnumerable<UserInfo> getAllDoctor()
        {
            var AllUserInfo = (from n in DBcontext.UserLoginRoles
                               join b in DBcontext.UserLogins on n.LoginId equals b.LoginId
                               join c in DBcontext.Roles on n.RoleId equals c.RoleId
                               where n.RoleId == 4 && b.InactiveFlag == "N"
                               select new
                               {
                                   LoginId = b.LoginId,
                                   UserName = b.UserName,
                                   FirstName = b.FirstName,
                                   LastName = b.LastName,
                                   EmailID = b.EmailID,
                                   MobileNo = b.UserPhone,
                                   Role = c.Name
                               }).ToList();

            List<UserInfo> allUsers = new List<UserInfo>();

            foreach (var item in AllUserInfo)
            {
                var s = new UserInfo();
                s.LoginId = item.LoginId;
                s.UserName = item.UserName;
                s.FirstName = item.FirstName;
                s.LastName = item.LastName;
                s.EmailID = item.EmailID;
                s.UserPhone = item.MobileNo;
                s.Name = item.Role;
                allUsers.Add(s);

            }

            return allUsers.ToList();
        }


        public IEnumerable<UserInfo> SearchUser(int EmpIdVM, int RoleIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM)
        {
             
            var AllUserInfo = (from n in DBcontext.UserLoginRoles
                               join b in DBcontext.UserLogins on n.LoginId equals b.LoginId
                               join c in DBcontext.Roles on n.RoleId equals c.RoleId
                               where (b.InactiveFlag == "N" && n.RoleId == RoleIdVM) && (b.FirstName == UserFirstNameVm && b.FirstName == null) && (b.LastName == UserLastNameVM || b.LastName == null) && (b.EmailID == UserEmailIdVM || b.EmailID == null) && (b.UserPhone == UsePhoneVM || b.UserPhone == null)
                                select new
                               {
                                   LoginId = b.LoginId,
                                   UserName = b.UserName,
                                   FirstName = b.FirstName,
                                   LastName = b.LastName,
                                   EmailID = b.EmailID,
                                   EmployeeId = b.EmployeeId,
                                   MobileNo = b.UserPhone,
                                   Role = c.Name
                               }).ToList();

            List<UserInfo> allUsers = new List<UserInfo>();

            foreach (var item in AllUserInfo)
            {
                var s = new UserInfo();
                s.LoginId = item.LoginId;
                s.UserName = item.UserName;
                s.FirstName = item.FirstName;
                s.LastName = item.LastName;
                s.EmailID = item.EmailID;
                s.EmployeeId = item.EmployeeId;
                s.UserPhone =item.MobileNo;
                s.Name = item.Role;
                allUsers.Add(s);

            }

            return allUsers.ToList();

        }
       
        public IEnumerable<Speciality> GetSpecialitys()
        {
            return DBcontext.Specialitys.ToList();
        }
        public IEnumerable<Role> GetRoles()
        {
            return DBcontext.Roles.ToList();
        }
        public UserLogin GetUserByID(int id)
        {
            return DBcontext.UserLogins.Find(id);
        }

       public  void InsertUserLogins(UserLogin userlogin, UserRegisterViewModel registerVM)
        {

            var crypto = new SimpleCrypto.PBKDF2();
            var encrypPass = crypto.Compute(registerVM.Userlogins.Password);
            userlogin.UserName = registerVM.Userlogins.UserName;
            userlogin.Password = encrypPass;
            userlogin.ConfirmPassword = encrypPass;
            userlogin.PasswordSalt = crypto.Salt;
            userlogin.FirstName = registerVM.Userlogins.FirstName;
            userlogin.LastName = registerVM.Userlogins.LastName;
            userlogin.EmailID = registerVM.Userlogins.EmailID;
            userlogin.EmployeeId =Convert.ToInt32(registerVM.Userlogins.EmployeeId);
            userlogin.IslockedOut = false;
            userlogin.InactiveFlag = "N";
            userlogin.CreatedByID = 1; // for now we add 1 later we change
            userlogin.CreatedDate = DateTime.Now;
            userlogin.ModifiedByID = 1;  // for now we add 1 later we change
            userlogin.ModifiedDate = DateTime.Now;
            userlogin.UserPhone = registerVM.Userlogins.UserPhone;

            DBcontext.UserLogins.Add(userlogin);
        }

       public void InsertUserLoginRoles(UserLoginRole userloginRole, UserRegisterViewModel registerVM)
       {

           userloginRole.LoginId = registerVM.Userlogins.LoginId;
           userloginRole.RoleId = registerVM.RoleID;

           DBcontext.UserLoginRoles.Add(userloginRole);

       }

       public void UserLoginSpecialitys(UserLoginSpeciality userloginspeciality, UserRegisterViewModel registerVM)
       {

           userloginspeciality.LoginId = registerVM.Userlogins.LoginId;
           userloginspeciality.SpecialityId = registerVM.SpecialityID;

           DBcontext.UserLoginSpecialitys.Add(userloginspeciality);
       }

     

        public void UpdateUser(UserLogin userlogin)
        {
            
            DBcontext.Entry(userlogin).State = EntityState.Modified;
        }

        public void DeleteUser(int userId)
        {
            UserLogin users = DBcontext.UserLogins.Find(userId);
            DBcontext.UserLogins.Remove(users);
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
