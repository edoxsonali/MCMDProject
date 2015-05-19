using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class UpgradeService
    {
        [Key]
        public int UpgradeServiceId { get; set; }
        public int LoginId { get; set; }

        public int MembershipId { get; set; }
        [Required(ErrorMessage = "Duration  is required")]
        public int Durations { get; set; }

        [Required(ErrorMessage = "AutoRenaval  is required")]
        public int AutoRenaval { set; get; }
        public string InactiveFlag { get; set; }

        public int CreatedById { get; set; }
        public System.DateTime? CreatedOnDate { get; set; }
        public int ModifiedById { get; set; }
        public System.DateTime? ModifiedOnDate { get; set; }

    }
}
