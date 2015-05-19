using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class GetViewUpgradeService
    {

        public int UpgradeServiceId { get; set; }
        public string MembershipType { get; set; }
        public int LoginId { get; set; }

        public string InactiveFlag { get; set; }
        //for geting Duration
        public string Durations { get; set; }
        //for geting Autorenaval
        public string Renaval { get; set; }

    }
}
