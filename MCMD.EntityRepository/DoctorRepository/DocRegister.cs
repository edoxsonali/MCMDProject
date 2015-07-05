using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.ViewModel.doctor;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.DoctorInterfaces;


namespace MCMD.EntityRepository.DoctorRepository
{
    public class DocRegister : IDocRegister
    {
        private ApplicationDbContext DBcontext;

        public DocRegister(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public void InsertDoctor(UserLogin userlogin, DocRegisterViewModel docRegVM)
        {

            var crypto = new SimpleCrypto.PBKDF2();
            var encrypPass = crypto.Compute(docRegVM.Userlogins.Password);
            //userlogin.UserName = docRegVM.Userlogins.FirstName;
            userlogin.Password = encrypPass;
            userlogin.ConfirmPassword = encrypPass;
            userlogin.PasswordSalt = crypto.Salt;
            userlogin.FirstName = docRegVM.Userlogins.FirstName;
            userlogin.LastName = docRegVM.Userlogins.LastName;
            userlogin.EmailID = docRegVM.Userlogins.EmailID;
            userlogin.EmployeeId = 0;
            userlogin.IslockedOut = false;
            userlogin.InactiveFlag = "N";
            userlogin.CreatedByID = 1; // for now we add 1 later we change
            userlogin.CreatedDate = DateTime.Now;
            userlogin.ModifiedByID = 1;  // for now we add 1 later we change
            userlogin.ModifiedDate = DateTime.Now;
            userlogin.UserPhone = docRegVM.Userlogins.UserPhone;

            DBcontext.UserLogins.Add(userlogin);
        }

        public IEnumerable<Speciality> GetSpeciality()
        {
            return DBcontext.Specialitys.ToList();
        }

        public void InsertDoctorRoles(UserLoginRole userloginRole, DocRegisterViewModel docRegVM)
        {

            userloginRole.LoginId = docRegVM.Userlogins.LoginId;
            userloginRole.RoleId = docRegVM.RoleId;

            DBcontext.UserLoginRoles.Add(userloginRole);

        }

        public void DocLoginSpecialitys(UserLoginSpeciality userloginspeciality, DocRegisterViewModel docRegVM)
        {

            userloginspeciality.LoginId = docRegVM.Userlogins.LoginId;
            userloginspeciality.SpecialityID = docRegVM.SpecialityID;

            DBcontext.UserLoginSpecialitys.Add(userloginspeciality);
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
