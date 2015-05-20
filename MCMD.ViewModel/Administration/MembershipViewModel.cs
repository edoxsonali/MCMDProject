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
        [DisplayName("Duration Id")]
        [Required(ErrorMessage = "Duration is required.")]
        public int DurationId { get; set; }

        [DisplayName("AutoRenaval Id")]
        [Required(ErrorMessage = "AutoRenaval is required.")]
        public int AutoRenavalId { get; set; }
        
        public int MembershipId { get; set; }

        [DisplayName("Membership Type")]
        [Required(ErrorMessage = "Membership Type is required.")]
        public string MembershipType { get; set; }

        [DisplayName("Fees")]
      //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "{0} must be a Number.")]
     //   [Required(ErrorMessage = "Fees must be required.")]
        public int Fees { get; set; }

    }
}
