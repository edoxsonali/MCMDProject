using MCMD.EntityModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;

namespace MCMD.ViewModel.doctor
{
    public class DocRegisterViewModel
    {
        public List<Speciality> Specialitys { get; set; }

      //  public List<Role> Roles { get; set; }
        public int RoleId { get; set; }

        public UserLogin Userlogins { get; set; }

        public int SpecialityID { get; set; }

    }
}
