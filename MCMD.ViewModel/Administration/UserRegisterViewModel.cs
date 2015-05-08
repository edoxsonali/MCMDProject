using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.ComponentModel.DataAnnotations;
using MCMD.EntityModel.Doctor;


namespace MCMD.ViewModel.Administration
{
    public class UserRegisterViewModel
    {

        public List<Role> Roles { get; set; }
        
        public List<Speciality> Specialitys { get; set; }
        public UserLogin Userlogins { get; set; }

        public int RoleID { get; set; }

        public int SpecialityID { get; set; }
       
       
    }
}
