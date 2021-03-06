﻿using ENetCare.BusinessService;
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
        public void TestReport_Mock_GetDistributionCentreStock()
        {
            IReportRepository reportRepository = new MockReportRepository();
            ReportService reportService = new ReportService(reportRepository);
            var stockList = reportService.GetDistributionCentreStock();

            foreach (DistributionCentreStock s in stockList) Debug.WriteLine(s.ToString());
            Debug.WriteLine("Number of items: " + stockList.Count());
            Assert.AreEqual<int>(1, stockList.Count());
            Assert.AreEqual<decimal>(10, stockList[0].CostPerPackage);
        }

        [TestMethod]
        public void TestReport_Mock_GetDistributionCentreLosses()
        {
            IReportRepository reportRepository = new MockReportRepository();
            ReportService reportService = new ReportService(reportRepository);
            var lossesList = reportService.GetDistributionCentreLosses();

            foreach (DistributionCentreLosses l in lossesList) Debug.WriteLine(l.ToString());
            Debug.WriteLine("Number of items: " + lossesList.Count());
            Assert.AreEqual<int>(1, lossesList.Count());
            Assert.AreEqual<decimal>(15, lossesList[0].LossRatioDenominator);
        }

        [TestMethod]
        public void TestReport_Mock_GetStocktaking()
        {
            MockReportRepository repo = new MockReportRepository();
            ReportService _reportService = new ReportService(repo);
            List<StocktakingPackage> spList = _reportService.GetStocktaking(4);
            foreach (StocktakingPackage p in spList) Debug.WriteLine(p.ToString());
            Debug.WriteLine("Number of items: " + spList.Count());
            Assert.IsTrue(spList.Count() > 0); 
        }
    }
}
