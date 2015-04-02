using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENetCare.Repository.Data;

namespace ENetCare.UnitTest
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        public void Update(Employee employee)
        {
            
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

        public List<Repository.Data.DistributionCentre> GetAllDistributionCentres()
        {
            throw new NotImplementedException();
        }

        public Repository.Data.DistributionCentre GetDistributionCentre(int centreid)
        {
            throw new NotImplementedException();
        }
    }
}
