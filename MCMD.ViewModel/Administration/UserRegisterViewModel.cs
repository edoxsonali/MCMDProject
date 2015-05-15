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
    public class UserRegisterViewModel
    {

        public List<Role> Roles { get; set; }
        
        public List<Speciality> Specialitys { get; set; }
        public UserLogin Userlogins { get; set; }

        [DisplayName("User Role")]
        [Required(ErrorMessage = "User Role is required.")]
        public int RoleID { get; set; }

        [DisplayName("Speciality")]
        [Required(ErrorMessage = "Speciality is required.")]
        public int SpecialityID { get; set; }
       
       
    }
}
