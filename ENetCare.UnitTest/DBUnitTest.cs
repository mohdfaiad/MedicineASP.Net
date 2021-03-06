﻿using System;
using System.Configuration;
using System.Data.SqlClient;                     
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENetCare.Repository.Repository;
using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository;
using System.Diagnostics;


namespace ENetCare.UnitTest                             // (P. 04-04-2015)
{

    [TestClass]
    public class DBUnitTest
    {   
        //static string _connectionString = ConfigurationManager.ConnectionStrings["EnetCare"].ConnectionString;
        static string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ENetCareDiff;Integrated Security=True;MultipleActiveResultSets=True";
        SqlConnection enetConnection = new SqlConnection(_connectionString);
        
 
        [TestMethod]
        public void TestDbAccess_Connection()        // Assertion is true if connection was opened 
        {
            enetConnection.Open();
            System.Data.ConnectionState state = enetConnection.State;
            enetConnection.Close();
            CheckIfItsPopulated();
            Assert.IsTrue(state == System.Data.ConnectionState.Open);
        }

        [TestMethod]
        public void TestDbAccess_GetDistCentres()                           // (P. 04-04-2015) 
        {
            enetConnection.Open();
            List<DistributionCentre> centresList = DataAccess.GetAllDistributionCentres(enetConnection);
            enetConnection.Close();
            int howMany = centresList.Count();
            Debug.WriteLine(howMany + " centres found.");
            Assert.IsTrue(howMany > 0);
        }

        [TestMethod]
        public void TestDbAccess_GetEmployees()                   // (P. 04-04-2015)  
        {
            enetConnection.Open();
            List<Employee> employeeList = DataAccess.GetAllEmployees(enetConnection);
            enetConnection.Close();
            int howMany = employeeList.Count();
            Debug.WriteLine(howMany + " employees found.");
            Assert.IsTrue(howMany > 0);
        }

        [TestMethod]
        public void TestDb_ShowAllTables()
        {
            enetConnection.Open();
            List<DistributionCentre> distList = DataAccess.GetAllDistributionCentres(enetConnection);
            Debug.WriteLine("DISTRIBUTION CENTRES : ");
            foreach (DistributionCentre centre in distList) Debug.WriteLine(centre);

            List<Employee> employeeList = DataAccess.GetAllEmployees(enetConnection);
            Debug.WriteLine("\n\n EMPLOYEES : ");
            foreach (Employee emp in employeeList) Debug.WriteLine(emp);

            List<StandardPackageType> typeList = DataAccess.GetAllStandardPackageTypes(enetConnection);
            Debug.WriteLine("\n\n STANDARD PACKAGE TYPES : ");
            foreach (StandardPackageType t in typeList) Debug.WriteLine(t);

            List<Package> packageList = DataAccess.GetAllPackages(enetConnection);
            Debug.WriteLine("\n\n PACKAGES : ");
            foreach (Package p in packageList) Debug.WriteLine(p);

            List<PackageTransit> transitList = DataAccess.GetAllPackageTransits(enetConnection);
            Debug.WriteLine("\n\n PACKAGE TRANSITS : ");
            foreach (PackageTransit t in transitList) Debug.WriteLine(t);
            enetConnection.Close();
            Assert.IsNotNull(employeeList);
        }


        [TestMethod]
        public void TestDbAccess_GetPackages()                            // (P. 04-04-2015)  
        {
            enetConnection.Open();
            List<Package> packageList = DataAccess.GetAllPackages(enetConnection);
            enetConnection.Close();
            int howMany = packageList.Count();
            Debug.WriteLine(howMany + " packages found.");
            Assert.IsTrue(howMany > 0);
        }


        /*
        [TestMethod]
        public void TestDbAccess_GetTransits()                            // (P. 04-04-2015)  
        {
            enetConnection.Open();
            List<PackageTransit> transitList = DataAccess.getAllPackageTransits(enetConnection);
            enetConnection.Close();
            int howMany = transitList.Count();
            Debug.WriteLine(howMany + " transits found.");
            Assert.IsTrue(howMany > 0);
        }
         */ 
         

        [TestMethod]
        public void TestDbAccess_GetPackageTypes()                              // (P. 04-04-2015) 
        {
            enetConnection.Open();
            List<StandardPackageType> typeList = DataAccess.GetAllStandardPackageTypes(enetConnection);
            enetConnection.Close();
            int howMany = typeList.Count();
            Debug.WriteLine(howMany + " packTypes found.");
            Assert.IsTrue(howMany > 0);
        }

        [TestMethod]
        public void TestRepository_GetEmployees()                   // (P. 04-04-2015) 
        {
            EmployeeRepository eRep = new EmployeeRepository(_connectionString);
            List<Employee> employeeList = eRep.getAllEmployees();
            int howMany = employeeList.Count();
            Debug.WriteLine(howMany + " Employees found via repository.");
            Assert.IsTrue(howMany > 0);
        }

        [TestMethod]
        public void TestRepository_GetPackageTypes()       // (P. 04-04-2015) 
        {
            PackageRepository pRep = new PackageRepository(_connectionString);
            List<StandardPackageType> typeList = pRep.GetAllStandardPackageTypes();
            int howMany = typeList.Count();
            Debug.WriteLine(howMany + " packages found via repository.");
            Assert.IsTrue(howMany > 0);
        }


        public void CheckIfItsPopulated()      // Populates Database if it hasnt been poped yet
        {                                       //               (P. 04-04-2015)
            enetConnection.Open();
            List<Employee> employeeList = DataAccess.GetAllEmployees(enetConnection);
            enetConnection.Close();
            if (employeeList.Count() < 8)         // if there curr are less than 8 employees
            {
                Populator myPopulator = new Populator();
                myPopulator.Run(enetConnection);
                Debug.WriteLine(" Sample Items were added to Database");
            }
        }


    }
}




