using MCMD.EntityModel.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.Administration
{
   public class UserVM
    {
      public List<Role> Roles { get; set; }
      public List<User> GetAllUsers { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
      
       
    }
}
