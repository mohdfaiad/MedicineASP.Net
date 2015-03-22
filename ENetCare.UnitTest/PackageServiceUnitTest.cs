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
            IPackageTransitRepository transitRepository = new MockPackageTransitRepository();

            PackageService packageService = new PackageService(packageRepository, transitRepository);
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
            IPackageTransitRepository transitRepository = new MockPackageTransitRepository();
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository, transitRepository); // new MockPackageTransitRepository();
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
        public void TestCalculateExpirationDate3()
        {
            IPackageRepository packageRepository = new MockPackageRepository();
            PackageService packageService = new PackageService(packageRepository);
            StandardPackageType packageType = new StandardPackageType
            {
                PackageTypeId = 1,
                Description = "100 Panadol Pills",
                ShelfLifeUnitType = ShelfLifeUnitType.Day,
                ShelfLifeUnits = 68
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
            var result = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);

            Assert.AreNotEqual<string>(string.Empty, barCode);
        }

        //ADDED BY IHAB, UNIT TEST
        //NEEDS MORE WORK
        [TestMethod]
        public void TestRegisterPackage_Test2()
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
            var result = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);

            Assert.AreNotEqual<string>(string.Empty, barCode);
        }



        [TestMethod]
        public void TestRegisterPackage_3()    // This is just a copy of the first testRegister method 
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
                CentreId = 27,
                Name = "North Centre",
                Address = "Up the river",
                IsHeadOffice = false
            };

            string barCode;
            var result = packageService.Register(packageType, location, DateTime.Today.AddMonths(2), out barCode);

            Assert.AreNotEqual<string>(string.Empty, barCode);
        }

    }
}
