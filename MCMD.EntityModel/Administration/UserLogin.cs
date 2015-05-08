using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
   #region Login
    
    public class UserLogin
    {
        
        public int LoginId { get; set; }

        [DisplayName("UserName")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "UserName is not valid.")]
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(100, ErrorMessage = "UserName cannot be longer than 100 characters.")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "First Name is not valid.")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Last Name is not valid.")]
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string EmailID { get; set; }

        public int EmployeeId { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PasswordSalt { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        [Required(ErrorMessage = "Mobile No is required.")]
        public string UserPhone { get; set; }

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

    #endregion
}
