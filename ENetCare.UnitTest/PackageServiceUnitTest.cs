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

            DateTime todaysDate = DateTime.Today;
            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, todaysDate);

            Assert.AreEqual<DateTime>(todaysDate.AddMonths(4), expirationDate);
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

            DateTime todaysDate = DateTime.Today;
            DateTime expirationDate = packageService.CalculateExpirationDate(packageType, todaysDate);

            Assert.AreEqual<DateTime>(todaysDate.AddDays(45), expirationDate);
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

            DateTime expirationDate = DateTime.Today.AddMonths(2);
            string barCode;
            var result = packageService.Register(packageType, location, expirationDate, out barCode);

            string compareBarCode = string.Format("00001{0:yyMMdd}00001", expirationDate);
            Assert.AreEqual<string>(compareBarCode, barCode);
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
