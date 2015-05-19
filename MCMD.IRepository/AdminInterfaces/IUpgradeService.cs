using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.IRepository.AdminInterfaces
{
    public interface IUpgradeService : IDisposable
    {
        IEnumerable<UpgradeService> GetServices();
        IEnumerable<AutoRenaval> GetMonths();
        IEnumerable<Duration> GetDuration();
        IEnumerable<MCMDMembership> GetMembers();
        IEnumerable<GetViewUpgradeService> GetUpgrdService();

        UpgradeService GetServicesById(int UpgradeServiceId);
        void InsertSrvice(UpgradeService upgradeService);
        void InsertServiceLog(UpgradeServiceLog upgradeServiceLog);
        void UpdateService(UpgradeService upgradeService);
        void DeleteService(int UpgradeServiceId);
        void Save();
    }
}
