using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENetCare.Repository.ViewData;
using ENetCare.Repository;
using ENetCare.Repository.Data;

namespace ENetCare.UnitTest
{
    public class MockReportRepository : IReportRepository
    {

        public MockReportRepository()          // Constructor              (P. 05-04-2015)
        {
            MockDataAccess.LoadMockTables();
        }

        public List<DistributionCentreStock> GetDistributionCentreStock()
        {
            var stockList = new List<DistributionCentreStock>();
            var stock = new DistributionCentreStock()
            {
                PackageTypeId = 1,
                PackageTypeDescription = "100 Panadol Tablets",
                CostPerPackage = 10,
                DistributionCentreId = 1,
                DistributionCentreName = "North Centre",
                NumberOfPackages = 2,
                TotalValue = 20
            };

            stockList.Add(stock);
            return stockList;
        }

        public List<DistributionCentreLosses> GetDistributionCentreLosses()
        {
            var lossesList = new List<DistributionCentreLosses>();
            var loss = new DistributionCentreLosses()
            {
                DistributionCentreId = 1,
                DistributionCentreName = "North Centre",
                LossRatioNumerator = 5,
                LossRatioDenominator = 15,
                TotalLossDiscardedValue = 500
            };

            lossesList.Add(loss);
            return lossesList;
        }

        public List<DoctorActivity> GetDoctorActivity()
        {
            throw new NotImplementedException();
        }

        public List<GlobalStock> GetGlobalStock()
        {
            throw new NotImplementedException();
        }

        public List<StocktakingPackage> GetStocktaking(int CentreId)
        {
            throw new NotImplementedException();
        }

        public List<ValueInTransit> GetValueInTransit()
        {
            throw new NotImplementedException();
        }

        public List<ReconciledPackage> GetReconciledPackages(Repository.Data.DistributionCentre currentLocation, Repository.Data.StandardPackageType packageType, List<string> barCodeList)
        {
            throw new NotImplementedException();
        }
    }
}
