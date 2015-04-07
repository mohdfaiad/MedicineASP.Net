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
        public void TestDistribute_EmployeeNotAuthorizedError()
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

        [TestMethod]
        public void TestReceivePackage_Successfully()
        {
            MockPackageRepository myMockPackageRepo = new MockPackageRepository();
            PackageService packageService = new PackageService(myMockPackageRepo);
            Package package1 = MockDataAccess.GetPackage(3);
            DistributionCentre myReceiverCentre = MockDataAccess.GetDistributionCentre(3);
            int newTransitId = InsertMockTransit(package1, 2, 3);                                       // insert transit
            packageService.Receive(package1.BarCode, myReceiverCentre, DateTime.Today);
            PackageTransit finishedTransit = MockDataAccess.GetPackageTransit(newTransitId);
            Debug.WriteLine(finishedTransit.ToString());
            Assert.IsTrue(finishedTransit.IsPastTransit() && finishedTransit.ReceiverCentre==myReceiverCentre);
        }

        [TestMethod]
        public void TestReceivePackage_NotFound()
        {
            MockPackageRepository myMockPackageRepo = new MockPackageRepository();
            PackageService packageService = new PackageService(myMockPackageRepo);
            Package package1 = MockDataAccess.GetPackage(3);
            DistributionCentre myReceiverCentre = MockDataAccess.GetDistributionCentre(3);
            Result res = packageService.Receive(package1.BarCode, myReceiverCentre, DateTime.Today);
            Assert.AreEqual("Transit not found", res.ErrorMessage);
        }

        [TestMethod]
        public void TestReceivePackage_WrongLocation()
        {
            MockPackageRepository myMockPackageRepo = new MockPackageRepository();
            PackageService packageService = new PackageService(myMockPackageRepo);
            Package package1 = MockDataAccess.GetPackage(3);
            DistributionCentre myReceiverCentre = MockDataAccess.GetDistributionCentre(5);
            int newTransitId = InsertMockTransit(package1, 2, 3);                                       // insert transit
            Result res = packageService.Receive(package1.BarCode, myReceiverCentre, DateTime.Today);
            PackageTransit finishedTransit = MockDataAccess.GetPackageTransit(newTransitId);
            Debug.WriteLine(finishedTransit.ToString());
            Result wrongDestiResult = new Result();
            wrongDestiResult.ErrorMessage = TransitResult.WrongReceiver;
            Assert.IsTrue(finishedTransit.IsPastTransit() &&  res.ErrorMessage==wrongDestiResult.ErrorMessage);
        }

        [TestMethod]
        public void TestReceivePackage_CancelTransit()
        {
            MockPackageRepository myMockPackageRepo = new MockPackageRepository();
            PackageService packageService = new PackageService(myMockPackageRepo);
            Package package1 = MockDataAccess.GetPackage(3);
            DistributionCentre myReceiverCentre = MockDataAccess.GetDistributionCentre(3);
            int newTransitId = InsertMockTransit(package1, 2, 3);                                       // insert transit
            Result res = packageService.CancelTransit(package1.BarCode, DateTime.Today);                // cancel transit
            int foundTransits = myMockPackageRepo.GetActiveTransitsByPackage(package1).Count;
            Assert.IsTrue(res.Success && foundTransits == 0);
        }

        public int InsertMockTransit(Package Package, int SenderId, int ReceiverId)
        {
            DistributionCentre mySenderCentre = MockDataAccess.GetDistributionCentre(SenderId);
            DistributionCentre myReceiverCentre = MockDataAccess.GetDistributionCentre(ReceiverId);
            PackageTransit newTransit = new PackageTransit();
            newTransit.Package = Package;
            newTransit.DateSent = DateTime.Today.AddDays(-2);
            newTransit.SenderCentre = mySenderCentre;
            newTransit.ReceiverCentre = myReceiverCentre;
            int newTransitId = MockDataAccess.InsertPackageTransit(newTransit);
            return newTransitId;
        }

        
    }
}
