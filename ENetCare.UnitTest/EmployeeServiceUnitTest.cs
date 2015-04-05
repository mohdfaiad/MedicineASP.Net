using ENetCare.BusinessService;
using ENetCare.Repository;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.UnitTest
{

    [TestClass]
    public class EmployeeServiceUnitTest
    {

        public EmployeeServiceUnitTest()
        {
            MockDataAccess.LoadMockTables();
        }

        [TestMethod]
        public void TestUpdate()
        {
            IEmployeeRepository employeeRepository = new MockEmployeeRepository();
            EmployeeService employeeService = new EmployeeService(employeeRepository);

            DistributionCentre locationCentre = new DistributionCentre
            {
                CentreId = 1,
                Name = "North Centre"
            };

            var result = employeeService.Update("fsmith", "Fred Smith", "fsmith@a.com", locationCentre, EmployeeType.Doctor);

            Assert.AreEqual<bool>(true, result.Success);
        }
    }
}
