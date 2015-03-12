using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENetCare.Repository.Repository;
using ENetCare.BusinessService;
using ENetCare.Repository.Data;

namespace ENetCare.UnitTest
{
    [TestClass]
    public class PackageServiceUnitTest
    {
        [TestMethod]
        public void TestCalculateExpirationDate()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = new StandardPackageType
            {
                PackageTypeId = 1,
                Description = "100 Panadol Pills",
                ShelfLifeUnitType = ShelfLifeUnitType.Month,
                ShelfLifeUnits = 4
            };

            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, DateTime.Today);

            Assert.AreNotEqual<DateTime>(DateTime.Today, expirationDate);
        }

        [TestMethod]
        public void TestCalculateExpirationDate2()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = new StandardPackageType
            {
                PackageTypeId = 1,
                Description = "100 Panadol Pills",
                ShelfLifeUnitType = ShelfLifeUnitType.Day,
                ShelfLifeUnits = 45
            };

            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, DateTime.Today);

            Assert.AreNotEqual<DateTime>(DateTime.Today, expirationDate);
        }

        [TestMethod]
        public void TestRegisterPackage()
        {            
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = new StandardPackageType
            {
                PackageTypeId = 1,
                Description = "100 Panadol Pills",
                ShelfLifeUnitType = ShelfLifeUnitType.Month,
                ShelfLifeUnits = 4
            };

            DistributionCentre location = new DistributionCentre
            {
                CentreId = 2,
                Name = "North Centre",
                Address = "Up the river",
                IsHeadOffice = false
            };

            string barCode;
            int packageId = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);

            Assert.AreNotEqual<string>(string.Empty, barCode);
        }

        //ADDED BY IHAB, JUST A TEST CHANGE TO SEE IF GIT IS WORKING OR NOT
        [TestMethod]
        public void TestRegisterPackage2()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = new StandardPackageType
            {
                PackageTypeId = 1,
                Description = "100 Panadol Pills",
                ShelfLifeUnitType = ShelfLifeUnitType.Day,
                ShelfLifeUnits = 45
            };

            DistributionCentre location = new DistributionCentre
            {
                CentreId = 2,
                Name = "North Centre",
                Address = "Up the river",
                IsHeadOffice = false
            };

            string barCode;
            int packageId = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);

            Assert.AreNotEqual<string>(string.Empty, barCode);
        }
    }
}
