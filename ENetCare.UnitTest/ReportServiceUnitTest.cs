using ENetCare.BusinessService;
using ENetCare.Repository;
using ENetCare.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.UnitTest
{
    [TestClass]
    public class ReportServiceUnitTest
    {

        public ReportServiceUnitTest()
        {
            MockDataAccess.LoadMockTables();
        }

        [TestMethod]
        public void TestGetDistributionCentreStock()
        {
            IReportRepository reportRepository = new MockReportRepository();
            ReportService reportService = new ReportService(reportRepository);

            var stockList = reportService.GetDistributionCentreStock();

            Assert.AreEqual<int>(1, stockList.Count());
            Assert.AreEqual<decimal>(10, stockList[0].CostPerPackage);
        }

        [TestMethod]
        public void TestGetDistributionCentreLosses()
        {
            IReportRepository reportRepository = new MockReportRepository();
            ReportService reportService = new ReportService(reportRepository);

            var lossesList = reportService.GetDistributionCentreLosses();

            Assert.AreEqual<int>(1, lossesList.Count());
            Assert.AreEqual<decimal>(15, lossesList[0].LossRatioDenominator);
        }
    }
}
