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

        private Result DistributePackage(int currentCentreId, string userName, string barCode)
        {
            DistributionCentre centre = new DistributionCentre();
            centre.CentreId = currentCentreId;
            MockPackageRepository packageRepository = new MockPackageRepository();
            PackageService _packageService = new PackageService(packageRepository);
            MockEmployeeRepository repository = new MockEmployeeRepository();
            var employeeService = new EmployeeService(repository);
            Employee authEmployee = employeeService.Retrieve(userName);
            DateTime expirationDate = DateTime.Now;
            Package package = _packageService.Retrieve(barCode);
            StandardPackageType spt2 = _packageService.GetStandardPackageType(package.PackageType.PackageTypeId);
            return _packageService.Distribute(package.BarCode, centre, authEmployee, expirationDate, spt2, package.PackageId);
        }

        [TestMethod]
        public void TestDistributePackage_EmployeeNotAuthorizedError()
        {
            //"rsmith" is a manager who works in centre 4 and so he cannot distribute
            var result = DistributePackage(4, "rsmith", "96854278434");
            Assert.AreEqual("You are not authorized to distribute packages", result.ErrorMessage);
        }

        [TestMethod]
        public void TestDistribute_InStockCurrentLocationUpdate()
        {
            //"ihab" works in centre 4 and the package "12344278431" is also in centre 4
            var result = DistributePackage(4, "ihab", "04983238436");
            Assert.AreEqual(null, result.ErrorMessage);
        }

        [TestMethod]
        public void TestDistribute_DistributedCurrentLocationError()
        {
            //"ihab" works in centre 4 and the package "45634271234" is already distributed
            string barCode = "11623542734";
            var result = DistributePackage(4, "ihab", barCode);
            Assert.AreEqual("Package has been already distributed: " + barCode, result.ErrorMessage);
        }
    }
}
