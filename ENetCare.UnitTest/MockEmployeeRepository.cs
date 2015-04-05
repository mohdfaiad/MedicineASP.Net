using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENetCare.Repository.Data;
using ENetCare.Repository;

namespace ENetCare.UnitTest
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        public MockEmployeeRepository()          // Constructor              (P. 05-04-2015)
        {
            MockDataAccess.LoadMockTables();
        }


        public void Update(Employee employee)                                 // (P. 04-04-2015)
        {
            MockDataAccess.UpdateEmployee(employee);    
        }

        public List<Repository.Data.DistributionCentre> GetAllDistributionCentres()     // (P. 04-04-2015)
        {
            return MockDataAccess.GetAllDistibutionCentres();
        }

        public Repository.Data.DistributionCentre GetDistributionCentre(int centreId)       // (P. 04-04-2015)
        {
            return MockDataAccess.GetDistributionCentre(centreId);
        }

        public Employee Get(int? employeeId, string username)
        {
             return new Employee()
            {
                EmployeeId = 1,
                FullName = "Fred Smith",
                EmployeeType = EmployeeType.Doctor,
                UserName = "fsmith",
                Location = new DistributionCentre
                {
                    CentreId = 1,
                    Name = "North Centre"
                },
                EmailAddress = "fsmith@a.com"
            };
        }
        
          
     
    }
}
