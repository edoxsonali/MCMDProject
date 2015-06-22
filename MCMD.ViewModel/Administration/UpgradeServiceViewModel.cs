using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.Administration
{
    public class UpgradeServiceViewModel
    {
        public List<UpgradeService> upgradeServiceList { get; set; }
        public UpgradeService upgradeService { get; set; }
        public int UpgradeServiceId { get; set; }
        public List<MCMDMembership> MembershipList { get; set; }
        public int MembershipId { get; set; }
        public List<AutoRenaval> MonthsList { get; set; }
        public int AutoRenavalId { get; set; }
        public List<Duration> DurationList { get; set; }
        public List<GetViewUpgradeService> GetUpgrdService { get; set; }
        public int DurationId { get; set; }
        public int[] SelectedMember { get; set; }

        public bool check { get; set; }
        public List<MembershipTwo> membershipListTwo { get; set; }
        public MembershipTwo membership { get; set; }
        public UpgradeServiceLog UpgrdServiceLog { get; set; }
        public int UpgradeServLogId { get; set; }
        public int LoginId { get; set; }
    }
}
