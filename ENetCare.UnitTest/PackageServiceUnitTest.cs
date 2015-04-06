using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENetCare.Repository.Repository;
using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using System.Diagnostics;
using ENetCare.Repository;
using System.Configuration;
using ENetCare.Web.Membership;

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
            int newPackageId = result.Id;
            string compareBarCode = string.Format("{0:D5}{1:yyMMdd}{2:D5}", packageType.PackageTypeId, expirationDate, newPackageId);
            Assert.AreEqual<string>(compareBarCode, barCode);
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


        [TestMethod]
        public void TestDistributePackage_InStockCurrentLocation()
        {
            DistributionCentre centre = new DistributionCentre();
            centre.CentreId = 4;

            MockPackageRepository packageRepository = new MockPackageRepository();
            PackageService _packageService = new PackageService(packageRepository);

            MockEmployeeRepository repository = new MockEmployeeRepository();
            var employeeService = new EmployeeService(repository);

            Employee employee = employeeService.Retrieve("ihab");
            DateTime expirationDate = DateTime.Now;

            Package package = _packageService.Retrieve("45634278436");

            StandardPackageType spt = _packageService.GetStandardPackageType(package.PackageType.PackageTypeId);

            var result = _packageService.Distribute(package.BarCode, centre, employee, expirationDate, spt, package.PackageId);

            Assert.AreEqual(null, result.ErrorMessage);
        }

        [TestMethod]
        public void TestDistributePackage_DistributedCurrentLocation()
        {
            DistributionCentre centre = new DistributionCentre();
            centre.CentreId = 4;

            MockPackageRepository packageRepository = new MockPackageRepository();
            PackageService _packageService = new PackageService(packageRepository);

            MockEmployeeRepository repository = new MockEmployeeRepository();
            var employeeService = new EmployeeService(repository);

            Employee employee = employeeService.Retrieve("ihab");
            DateTime expirationDate = DateTime.Now;

            string barCode = "45634271234";

            Package package = _packageService.Retrieve(barCode);

            StandardPackageType spt = _packageService.GetStandardPackageType(package.PackageType.PackageTypeId);

            var result = _packageService.Distribute(package.BarCode, centre, employee, expirationDate, spt, package.PackageId);

            Assert.AreEqual("Package has been already distributed: " + barCode, result.ErrorMessage);
        }
    }
}
