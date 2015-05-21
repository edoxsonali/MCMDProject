using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MCMD.EntityModel.Administration
{
    public class GetViewUsers
    {
        [Key]
        public int LoginId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string EmailID { get; set; }

        public int? EmployeeId { get; set; }
        public string UserPhone { get; set; }


        //for getting Role Name 
        public string RoleName { get; set; }
    }
}
