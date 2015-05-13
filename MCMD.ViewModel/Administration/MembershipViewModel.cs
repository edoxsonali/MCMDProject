using MCMD.EntityModel.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.Administration
{
    public class MembershipViewModel
    {
        public List<Duration> Duration_s { get; set; }
        public List<AutoRenaval> Months { get; set; }

        public MCMDMembership member { get; set; }
        public List<MCMDMembership> Membership { get; set; }
        public List<GetViewMembership> GetMember { get; set; }
        public int DurationId { get; set; }
        public int AutoRenavalId { get; set; }
        public int MembershipId { get; set; }


        public string MembershipType { get; set; }
        public int? Fees { get; set; }

    }
}
