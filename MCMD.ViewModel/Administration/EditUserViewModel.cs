using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.ComponentModel.DataAnnotations;
using MCMD.EntityModel.Doctor;
using System.ComponentModel;


namespace MCMD.ViewModel.Administration
{
    public class EditUserViewModel
    {
        public List<Role> Roles { get; set; }

        public UserLogin Userlogins { get; set; }

        public int LoginId { get; set; }

        [DisplayName("User Role")]
        [Required(ErrorMessage = "User Role is required.")]
        public int RoleID { get; set; }

        //[DisplayName("UserName")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "UserName is not valid.")]
        //[Required(ErrorMessage = "UserName is required.")]
        //[StringLength(100, ErrorMessage = "UserName cannot be longer than 100 characters.")]
        //public string UserName { get; set; }

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

        [DisplayName("Phone No")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "{0} must be a Number and only 10 digit.")]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string UserPhone { get; set; }

        [DisplayName("Employee Id")]
        [Required(ErrorMessage = "Employee Id is required.")]
        public int? EmployeeId { get; set; }

 
    }
}
