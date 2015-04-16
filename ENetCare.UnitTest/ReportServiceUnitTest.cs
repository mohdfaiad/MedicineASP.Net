using ENetCare.BusinessService;
using ENetCare.Repository;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Repository.ViewData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI;
//using System.Web.UI.WebControls;
namespace ENetCare.UnitTest
{
    [TestClass]
    public class ReportServiceUnitTest
    {
        private string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ENetCare;Integrated Security=True;MultipleActiveResultSets=True";

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

        [TestMethod]
        public void TestReport_MockStocktaking()
        {
            MockReportRepository repo = new MockReportRepository();
            ReportService _reportService = new ReportService(repo);
            List<StocktakingPackage> spList = _reportService.GetStocktaking(4);
            foreach (StocktakingPackage p in spList) Debug.WriteLine(p);
            Debug.WriteLine("Number of items: " + spList.Count());
            Assert.IsTrue(spList.Count() > 0); 
        }

        [TestMethod]
        public void TestReport_Stocktaking()
        {
            ReportRepository repo = new ReportRepository(connString);
            ReportService _reportService = new ReportService(repo);
            List<StocktakingPackage> spList = _reportService.GetStocktaking(2);
            foreach (StocktakingPackage sp in spList) Debug.WriteLine(sp);
            Debug.WriteLine("Number of items: " + spList.Count());
            Assert.IsTrue(spList.Count() >0);
        }


    }
}
