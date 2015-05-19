using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MCMD.EntityModel.Doctor;


namespace MCMD.ViewModel.Administration
{
   public class UserDetailsViewModel
    {
       public List<Role> Roles { get; set; }

       [DisplayName("User Type")]
       [Required(ErrorMessage = "User Type is required.")]
       public int RoleId { get; set; }
       public List<UserLogin> UserLogins { get; set; }
       public List<Speciality> speciality { get; set; }
       public int SpecialityID { get; set; }

       [DisplayName("Doctor Id")]
       [Required(ErrorMessage = "Doctor Id is required.")]
       public int LoginId { get; set; }

       public List<UserInfo> UserInfos { get; set; }

       public List<GetViewUsers> GetViewUsers { get; set; }

       public List<GetViewDoctor> GetViewDoctors { get; set; }
     //  public string UserName { get; set; }

       [DisplayName("First Name")]
       [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "First Name is not valid.")]    
       [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
       public string FirstName { get; set; }

       [DisplayName("Last Name")]
       [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Last Name is not valid.")]
       [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
       public string LastName { get; set; }

       [DisplayName("Email")]
       [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]     
       [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
       public string EmailID { get; set; }

       [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "{0} must be a Number.")]
       public string UserPhone { get; set; }

       public string RoleName { get; set; }

       //[DisplayName("Employee Id")]
       //[Required(ErrorMessage = "Employee Id is required.")]
       public int EmployeeId { get; set; }
       public List<DoctorClinicInformation> DoctorClinicInformation { get; set; }
       public int ClinicInfoId { get; set; }
       public string ClinicName { get; set; }


    }
}
