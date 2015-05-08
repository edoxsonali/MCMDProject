using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.EntityModel.Administration
{
    
    public class Role
    {
        public int RoleId { get; set; }
       public string Description { get; set; }
       public string Name { get; set; }

     
    }
}
