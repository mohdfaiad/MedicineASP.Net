using ENetCare.Repository.Data;
using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public interface IReportRepository
    {
        List<DistributionCentreStock> GetDistributionCentreStock();
        List<DistributionCentreLosses> GetDistributionCentreLosses();
        List<DoctorActivity> GetDoctorActivity();
        List<GlobalStock> GetGlobalStock();
        List<ValueInTransit> GetValueInTransit();
        List<ReconciledPackage> GetReconciledPackages(DistributionCentre currentLocation, StandardPackageType packageType, List<string> barCodeList);

    }
}
