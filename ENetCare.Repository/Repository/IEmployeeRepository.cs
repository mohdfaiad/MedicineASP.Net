using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public interface IEmployeeRepository
    {
        void Update(Employee employee);

        Employee Get(int? employeeId, string username);
        List<DistributionCentre> GetAllDistributionCentres();        // * * * *
        List<Employee> GetAllEmployees();
        Employee getEmployeeById(int id);
        List<Employee> getEmployeesAtCentre(DistributionCentre xCentre);
        List<Employee> getEmployeesByType(EmployeeType xType);
    }
}
