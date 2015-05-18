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
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserPhone { get; set; }
        public int EmployeeId { get; set; }

 
    }
}
