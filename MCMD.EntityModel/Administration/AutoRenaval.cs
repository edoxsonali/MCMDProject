using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.EntityModel.Administration
{
    public class AutoRenaval
    {
        [Key]
        public int AutoRenavalId { get; set; }

        public string Renaval { get; set; }
    }
}
