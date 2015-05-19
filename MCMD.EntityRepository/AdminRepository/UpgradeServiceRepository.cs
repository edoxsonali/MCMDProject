using MCMD.EntityModel;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using MCMD.IRepository.AdminInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityRepository.AdminRepository
{
    public class UpgradeServiceRepository : IUpgradeService
    {
        private ApplicationDbContext DBcontext;
        public UpgradeServiceRepository(ApplicationDbContext DBcontext)
        {

            this.DBcontext = DBcontext;
        }
        public IEnumerable<UpgradeService> GetServices()
        {

            return DBcontext.upgradeServices.ToList();
        }
        public IEnumerable<GetViewUpgradeService> GetUpgrdService()
        {
            var AllServiceInfo = (from n in DBcontext.upgradeServices
                                  join b in DBcontext.DurationList on n.Durations equals b.DurationId
                                  join c in DBcontext.AutoRenavals on n.AutoRenaval equals c.AutoRenavalId
                                  join d in DBcontext.Memberships on n.MembershipId equals d.MembershipId
                                  where n.InactiveFlag == "N"
                                  select new
                                  {
                                      UpgradeServiceId = n.UpgradeServiceId,
                                      LoginId = n.LoginId,
                                      MembershipName = d.MembershipType,
                                      Duration = b.Durations,
                                      AutoRenaval = c.Renaval

                                  }).ToList();

            List<GetViewUpgradeService> allServices = new List<GetViewUpgradeService>();

            foreach (var item in AllServiceInfo)
            {
                var s = new GetViewUpgradeService();
                s.UpgradeServiceId = item.UpgradeServiceId;
                s.LoginId = item.LoginId;
                s.MembershipType = item.MembershipName;
                s.Durations = item.Duration;
                s.Renaval = item.AutoRenaval;

                allServices.Add(s);

            }
            return allServices.ToList();
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
        public UpgradeService GetServicesById(int UpgradeServiceId)
        {
            return DBcontext.upgradeServices.Find(UpgradeServiceId);
        }
        public void InsertSrvice(UpgradeService upgradeService)
        {
            DBcontext.upgradeServices.Add(upgradeService);
        }

        public void InsertServiceLog(UpgradeServiceLog upgradlog)
        {
            DBcontext.upgradeServiceLog.Add(upgradlog);

        }
        //public void InsertServiceLog(UpgradeServiceLog upgradeServiceLog)
        //{
        //    DBcontext.upgradeServiceLog.Add(upgradeServiceLog);
        //}
        public void UpdateService(UpgradeService upgradeService)
        {
            DBcontext.Entry(upgradeService).State = EntityState.Modified;
        }
        public void DeleteService(int UpgradeServiceId)
        {
            UpgradeService service = DBcontext.upgradeServices.Find(UpgradeServiceId);
            DBcontext.upgradeServices.Remove(service);
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
