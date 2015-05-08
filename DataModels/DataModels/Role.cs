using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.DataModels.DataModels
{
     [Table("MCMDCreateRoles")]
    public class Role
    {
        [Key]
         public int RoleID { get; set; }

        public string RoleName { get; set; }
        public string InactiveFlag { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }

       
    }
}
