using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.EntityModel.Administration
{
    #region User 
   public class User
    {

        [Key]
        public int UserId { get; set; }

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
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Employee Id is required.")]
        [DisplayName("EmployeeId")]
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

        public string UserRole { get; set; }
        public string Status { get; set; }
        public string InactiveFlag { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int? CreatedID { get; set; }
        public System.DateTime ModifyedDate { get; set; }
        public int? ModifiedID { get; set; }


        
    }
    #endregion
}
