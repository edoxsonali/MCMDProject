using MCMD.EntityModel;
using MCMD.EntityModel.Administration;
using MCMD.IRepository.AdminInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityRepository.AdminRepository
{
    public class MembershipRepository : IMembershipRepository
    {
        private ApplicationDbContext DBcontext;

        public MembershipRepository(ApplicationDbContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public IEnumerable<MCMDMembership> GetMembers()
        {
            return DBcontext.Memberships.ToList();
        }
        public IEnumerable<Duration> GetDuration()
        {
            return DBcontext.DurationList.ToList();
        }
        public IEnumerable<AutoRenaval> GetMonths()
        {
            return DBcontext.AutoRenavals.ToList();
        }

        public MCMDMembership GetMemberById(int memberId)
        {
            return DBcontext.Memberships.Find(memberId);
        }
        public void InsertMember(MCMDMembership member)
        {
            DBcontext.Memberships.Add(member);
        }
        public void UpdateMember(MCMDMembership member)
        {
            DBcontext.Entry(member).State = EntityState.Modified;
        }
        public void DeleteMember(int MembershipId)
        {
            MCMDMembership members = DBcontext.Memberships.Find(MembershipId);
            DBcontext.Memberships.Remove(members);
        }
        public void Save()
        {
            DBcontext.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBcontext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
