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
using System.Data.SqlClient;

namespace MCMD.EntityRepository.AdminRepository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext DBcontext;

        public UserRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public IEnumerable<GetViewUsers> GetAllUser()
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
                                   Role = c.RoleName
                               }).ToList();

            List<GetViewUsers> allUsers = new List<GetViewUsers>();

            foreach (var item in AllUserInfo)
            {
                var s = new GetViewUsers();
                s.LoginId = item.LoginId;
                s.UserName = item.UserName;
                s.FirstName = item.FirstName;
                s.LastName = item.LastName;
                s.EmailID = item.EmailID;
                s.EmployeeId = item.EmployeeId;
                s.UserPhone =item.MobileNo;
                s.RoleName = item.Role;
                allUsers.Add(s);

            }

           return allUsers.ToList();

        }

        public IEnumerable<GetViewDoctor> getAllDoctor()
        {
            var AllUserInfo = (from n in DBcontext.UserLoginRoles
                               join b in DBcontext.UserLogins on n.LoginId equals b.LoginId
                               join c in DBcontext.Roles on n.RoleId equals c.RoleId
                               join ls in DBcontext.UserLoginSpecialitys on b.LoginId equals ls.LoginId
                               join s in DBcontext.Specialitys on ls.SpecialityID equals s.SpecialityID
                               join d in DBcontext.DoctorsClinicInfos on b.LoginId equals d.LoginId
                               join u in DBcontext.upgradeServices on b.LoginId equals u.LoginId
                               join m in DBcontext.Memberships on u.MembershipId equals m.MembershipId 
                               where n.RoleId == 4 && b.InactiveFlag == "N"
                               select new
                               {
                                   LoginId = b.LoginId,
                                   UserName = b.UserName,
                                   FirstName = b.FirstName,
                                   LastName = b.LastName,
                                   Speciality=s.SpecialityName,
                                   ClinicName = d.ClinicName,
                                   MembershipType= m.MembershipType,
                                   EmailID = b.EmailID,
                                   MobileNo = b.UserPhone,
                                   Role = c.RoleName
                               }).ToList();

            List<GetViewDoctor> allUsers = new List<GetViewDoctor>();

            foreach (var item in AllUserInfo)
            {
                var s = new GetViewDoctor();
                s.LoginId = item.LoginId;
                s.UserName = item.UserName;
                s.FirstName = item.FirstName;
                s.LastName = item.LastName;
                s.SpecialityName = item.Speciality;
                s.ClinicName = item.ClinicName;
                s.MembershipType = item.MembershipType;
                s.EmailID = item.EmailID;
                s.UserPhone = item.MobileNo;
                s.RoleName = item.Role;
                allUsers.Add(s);

            }

            return allUsers.ToList();
        }


        public IEnumerable<GetViewUsers> SearchUser(int RoleIdVM, int EmpIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM)
        {


            var UserInfo = DBcontext.Database.SqlQuery<GetViewUsers>("GetViewUsers @RoleId,@EmployeeId, @FirstName,@LastName,@EmailID,@UserPhone",
                                                         new SqlParameter("RoleId", RoleIdVM),
                                                         new SqlParameter("EmployeeId", EmpIdVM),
                                                         new SqlParameter("FirstName", UserFirstNameVm),
                                                         new SqlParameter("LastName", UserLastNameVM),
                                                         new SqlParameter("EmailID", UserEmailIdVM),
                                                         new SqlParameter("UserPhone", UsePhoneVM)
                                                      ).ToList();
                                                      //.OrderByDescending(x => x.LoginId).ToList();


            return UserInfo.ToList();

        }

        public IEnumerable<GetViewDoctor> SearchDoctor(int LogIdVM, int SpeIdVM, int RoleIdVM, string UserFirstNameVm, string UserLastNameVM, string UserEmailIdVM, string UsePhoneVM, int ClinicidVM)
        {
            var UserInfo = DBcontext.Database.SqlQuery<GetViewDoctor>("GetViewDoctor  @LoginId,@SpecialityID, @RoleId, @FirstName,@LastName,@EmailID,@UserPhone,@ClinicInfoId",
                                                             new SqlParameter("LoginId", LogIdVM),
                                                             new SqlParameter("SpecialityID", SpeIdVM),
                                                             new SqlParameter("RoleId", RoleIdVM),
                                                             new SqlParameter("FirstName", UserFirstNameVm),
                                                             new SqlParameter("LastName", UserLastNameVM),
                                                             new SqlParameter("EmailID", UserEmailIdVM),
                                                             new SqlParameter("UserPhone", UsePhoneVM),
                                                             new SqlParameter("ClinicInfoId", ClinicidVM)
                                                      ).ToList();
                                                      //OrderByDescending(x => x.LoginId).ToList();


            return UserInfo.ToList();

        }
       

        public IEnumerable<UserLogin> GetAllUserData()
        {
            return DBcontext.UserLogins.ToList();
        }
        public IEnumerable<UserLoginRole> GetUserLoginRole()
        {
            return DBcontext.UserLoginRoles.ToList();
        }
        public IEnumerable<Speciality> GetSpecialitys()
        {
            return DBcontext.Specialitys.ToList();
        }

        public IEnumerable<Role> GetRoles()
        {
            return DBcontext.Roles.ToList();
        }
        public IEnumerable<DoctorClinicInformation> GetClinicInformation()
        {
            return DBcontext.DoctorsClinicInfos.ToList();
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
           userloginspeciality.SpecialityID = registerVM.SpecialityID;

           DBcontext.UserLoginSpecialitys.Add(userloginspeciality);
       }

     

        public void UpdateUser(UserLogin userlogin)
        {
            
            DBcontext.Entry(userlogin).State = EntityState.Modified;
        }
        public void UpdateUserRole(UserLoginRole userloginRole)
        {

            DBcontext.Entry(userloginRole).State = EntityState.Modified;
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
