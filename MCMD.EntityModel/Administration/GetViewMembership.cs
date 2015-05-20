using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
    public class GetViewMembership
    {
      
        public int MembershipId { get; set; }
        public string MembershipType { get; set; }
        public int Fees { get; set; }
     //   public int Durations { get; set; }
     //   public int AutoRenaval { get; set; }
        public string InactiveFlag { get; set; }
        //for geting Duration
        public string Duration { get; set; }
        //for geting Autorenaval
        public string Renaval { get; set; }
    }
}
