using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using System.ComponentModel.DataAnnotations;


namespace MCMD.ViewModel.Administration
{
   public class UserDetailsViewModel
    {
       public List<Role> Roles { get; set; }
       public int RoleId { get; set; }
       public List<UserLogin> UserLogins { get; set; }
       public int LoginId { get; set; }

       public List<UserInfo> UserInfos { get; set; }
     //  public string UserName { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string EmailID { get; set; }
       public string UserPhone { get; set; }
       public string Name { get; set; }

       public int EmployeeId { get; set; }
    


    }
}
