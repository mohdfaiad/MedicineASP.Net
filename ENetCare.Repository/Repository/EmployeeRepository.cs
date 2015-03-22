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
   


        public List<Employee> GetAllEmployees()
        {                                                                    // added by Pablo on 23-03-15
            List<Employee> allEmployees = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                allEmployees = DataAccess.GetAllEmployees(connection);
            }
            return allEmployees;
        }


        public Employee getEmployeeById(int id)
        {                                                                      // Added by Pablo on 23-03-15
            List<Employee> allEmployees = this.GetAllEmployees();
            foreach(Employee e in allEmployees) { if(e.EmployeeId==id) return e; }
            return null;
        }


        public List<Employee> getEmployeesAtCentre(DistributionCentre xCentre)
        {                                                                 // Added by pablo on 23-03-15
            List<Employee> allEmployees = this.GetAllEmployees();         // get all employees
            List<Employee> myEmployees = new List<Employee>();            // create empty list 
            foreach (Employee e in allEmployees)
                { if (e.Location.CentreId==xCentre.CentreId) myEmployees.Add(e); }  // add the ones at centre
            return myEmployees;                                          // return subset of employees
        }

        public List<Employee> getEmployeesByType(EmployeeType xType)
        {                                                                 // Added by pablo on 23-03-15
            List<Employee> allEmployees = this.GetAllEmployees();         // get all employees
            List<Employee> myEmployees = new List<Employee>();            // create empty list 
            foreach (Employee e in allEmployees)
            { if (e.GetType()==xType.GetType()) myEmployees.Add(e); }    // add accordingly (doctors agents or mans)
            return myEmployees;                                          // return subset of employees
        }

        public List<DistributionCentre> GetAllDistributionCentres()
            // C A U T I O N
            // Pablo added this method because the class "EmployeeService" was giving him a headache
            // this method needs to be removed at some point
            // The actual method for getting centres is located in the distCentreRepo class  
            {
            DistributionCentreRepository eRepo = new DistributionCentreRepository(_connectionString);
            return eRepo.GetAllDistributionCentres();
            }



    }
}
