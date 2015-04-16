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
        public void TestMockGetStocktaking()
        {
            MockReportRepository repo = new MockReportRepository();
            ReportService _reportService = new ReportService(repo);
            //Employee user = MockDataAccess.GetEmployee(2);
            //DistributionCentre centre = user.Location;
            List<StocktakingPackage> list = _reportService.GetStocktaking(4);
            foreach (StocktakingPackage p in list) Debug.WriteLine(p);
            Debug.WriteLine("number of items: " + list.Count());
            Assert.AreEqual(4, 4);
        }

        [TestMethod]
        public void TestGetStocktaking()
        {
            string connS = "Data Source=.\\SQLEXPRESS;Initial Catalog=ENetCare;Integrated Security=True;MultipleActiveResultSets=True";
            ReportRepository repo = new ReportRepository(connS);
            ReportService _reportService = new ReportService(repo);
            Employee user = MockDataAccess.GetEmployee(4);
            DistributionCentre centre = user.Location;
            List<StocktakingPackage> list = _reportService.GetStocktaking(centre.CentreId);

            Assert.AreEqual(4, 4);
        }


    }
}
