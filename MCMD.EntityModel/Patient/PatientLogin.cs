using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Patient
{
    public class PatientLogin
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPhone { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PasswordSalt { get; set; }
        public Nullable<bool> IslockedOut { get; set; }

        public Nullable<System.DateTime> LastLockoutDate { get; set; }

        public Nullable<System.DateTime> LastLoginDate { get; set; }

        public Nullable<System.DateTime> LastLogOutDate { get; set; }

        public string IPAddress { get; set; }

        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }

        public string PasswordVerificationToken { get; set; }

        public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate { get; set; }

        public Nullable<int> FailedPasswordAttemptCount { get; set; }

        public string InactiveFlag { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedByID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
