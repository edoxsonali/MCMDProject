using MCMD.EntityModel.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.ViewModel.Administration
{
    public class MembershipViewModel
    {
        public List<Duration> Duration_s { get; set; }
        public List<AutoRenaval> Months { get; set; }

        public MCMDMembership member { get; set; }
        public List<MCMDMembership> Membership { get; set; }
        public List<GetViewMembership> GetMember { get; set; }

        public List<MCMDMembership> GetMembers { get; set; }
             
        public int MembershipId { get; set; }

        [DisplayName("Membership Type")]
        [Required(ErrorMessage = "Membership Type is required.")]
        public string MembershipType { get; set; }
     
        public int Fees { get; set; }

    }
}
