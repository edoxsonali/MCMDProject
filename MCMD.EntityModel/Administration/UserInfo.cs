using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class UserInfo
    {

        [Key]
        public int LoginId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string EmailID { get; set; }

        public int EmployeeId { get; set; }
        public string UserPhone { get; set; }

     
        //for getting Role Name 
        public string Name { get; set; }


    }
}
