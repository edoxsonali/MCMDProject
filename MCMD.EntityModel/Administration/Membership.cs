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
     // [Table("Membership")]
    public class MCMDMembership
    {
        [Key]
        public int MembershipId { get; set; }

        [DisplayName("Membership Type")]
        [Required(ErrorMessage = "Membership Type is required.")]
        [StringLength(30, ErrorMessage = "Membership Type cannot be longer than 30 characters.")]
        public string MembershipType { get; set; }

        [DisplayName("Fees")]
        [Required(ErrorMessage = "Fees must be required.")]
        public int Fees { get; set; }

        public bool CheckedStatus { get; set; }
        public string InactiveFlag { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
