using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string _connectionString;
        
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Update(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DataAccess.UpdateEmployee(connection, employee);
            }
            return;
        }

        public Employee Get(int? employeeId, string username)
        {
            Employee employee = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                employee = DataAccess.GetEmployee(connection, employeeId, username);

                if (employee != null)
                    employee.Location = DataAccess.GetDistributionCentre(connection, employee.Location.CentreId);
            }
            return employee;
        }

        public List<DistributionCentre> GetAllDistributionCentres()
        {
            List<DistributionCentre> centres = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                centres = DataAccess.GetAllDistributionCentres(connection);
            }
            return centres;
        }
    }
}
