using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.ViewModel.Administration;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IMembershipRepository : IDisposable
    {

        IEnumerable<Duration> GetDuration();
        IEnumerable<AutoRenaval> GetMonths();
        IEnumerable<MCMDMembership> GetMembers();
        MCMDMembership GetMemberById(int memberId);
        void InsertMember(MCMDMembership member);
        void UpdateMember(MCMDMembership member);
        void DeleteMember(int MembershipId);
        void Save();
    }
}
