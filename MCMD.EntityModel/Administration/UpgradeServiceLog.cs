using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class UpgradeServiceLog
    {
        public int UpgradeServLogId { get; set; }
        public int MembershipId { get; set; }
        public int LoginId { get; set; }
        public int Durations { get; set; }
        public int AutoRenaval { get; set; }
        public bool CheckedStatus { get; set; }
        public int CreatedById { get; set; }
        public string InactiveFlag { get; set; }
        public System.DateTime? CreatedOnDate { get; set; }
        public int ModifiedById { get; set; }
        public System.DateTime? ModifiedOnDate { get; set; }

    }
}
