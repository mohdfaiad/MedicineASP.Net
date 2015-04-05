using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENetCare.Repository.Repository;
using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using System.Diagnostics;
using ENetCare.Repository;

namespace ENetCare.UnitTest
{
    [TestClass]
    public class PackageServiceUnitTest
    {

        public PackageServiceUnitTest()
        {
            MockDataAccess.LoadMockTables();
        }

        [TestMethod]
        public void TestCalculateExpirationDate()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = MockDataAccess.GetPackageType(3);
            DateTime todaysDate = DateTime.Today;
            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, todaysDate);
            Assert.AreEqual<DateTime>(todaysDate.AddMonths(packageType.ShelfLifeUnits), expirationDate);
        }

        [TestMethod]
        public void TestCalculateExpirationDate2()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = MockDataAccess.GetPackageType(5);
            packageType.ShelfLifeUnitType = ShelfLifeUnitType.Day;
            DateTime todaysDate = DateTime.Today;
            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, todaysDate);
            Assert.AreEqual<DateTime>(todaysDate.AddDays(packageType.ShelfLifeUnits), expirationDate);
        }

        [TestMethod]
        public void TestRegisterPackage()
        {            
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = MockDataAccess.GetPackageType(3);
            DistributionCentre location = MockDataAccess.GetDistributionCentre(2);
            DateTime expirationDate = DateTime.Today.AddMonths(2);
            string barCode;
            var result = packageService.Register(packageType, location, expirationDate, out barCode);
            string compareBarCode = string.Format("00001{0:yyMMdd}00001", expirationDate);
            Debug.WriteLine(compareBarCode + " vs " + barCode);
            Assert.AreEqual<string>(compareBarCode, barCode);
        }

        //ADDED BY IHAB, UNIT TEST
        //NEEDS MORE WORK
        [TestMethod]
        public void TestRegisterPackage_Test2()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = MockDataAccess.GetPackageType(3);
            DistributionCentre location = MockDataAccess.GetDistributionCentre(3);
            string barCode;
            var result = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);
            Assert.AreNotEqual<string>(string.Empty, barCode);
        }


        [TestMethod]
        public void TestRegisterPackage_3()    // This is just a copy of the first testRegister method 
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = MockDataAccess.GetPackageType(4);
            DistributionCentre location = MockDataAccess.GetDistributionCentre(3);
            string barCode;
            var result = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);
            Assert.AreNotEqual<string>(string.Empty, barCode);
        }

    }
}
