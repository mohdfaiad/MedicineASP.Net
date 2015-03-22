using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private string _connectionString;
        
        public PackageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Insert(Package package)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return DataAccess.InsertPackage(connection, package);
            }
        }

        public void Update(Package package)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DataAccess.UpdatePackage(connection, package);
            }
            return;
        }

        public Package Get(int? packageId, string barcode)
        {
            Package package = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                package = DataAccess.GetPackage(connection, packageId, barcode);
                if (package == null)
                    return null;

                package.PackageType = DataAccess.GetStandardPackageType(connection, package.PackageType.PackageTypeId);

                if (package.CurrentLocation != null)
                {
                    package.CurrentLocation = DataAccess.GetDistributionCentre(connection, package.CurrentLocation.CentreId);
                }

                if (package.DistributedBy != null)
                {
                    package.DistributedBy = DataAccess.GetEmployee(connection, package.DistributedBy.EmployeeId, null);
                    package.DistributedBy.Location = DataAccess.GetDistributionCentre(connection, package.DistributedBy.Location.CentreId);
                }
            }
            return package;
        }


        public Package Get(string barcode)                    // Overload with one argument
        {                                                             // Added by Pablo on 23-03-15 
            Package package = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                package = DataAccess.GetPackage(connection, barcode);
                if (package == null)
                    return null;

                package.PackageType = DataAccess.GetStandardPackageType(connection, package.PackageType.PackageTypeId);

                if (package.CurrentLocation != null)
                {
                    package.CurrentLocation = DataAccess.GetDistributionCentre(connection, package.CurrentLocation.CentreId);
                }

                if (package.DistributedBy != null)
                {
                    package.DistributedBy = DataAccess.GetEmployee(connection, package.DistributedBy.EmployeeId, null);
                    package.DistributedBy.Location = DataAccess.GetDistributionCentre(connection, package.DistributedBy.Location.CentreId);
                }
            }
            return package;
        }




        public List<Package> GetAllPackages()
        {
            List<Package> allPackages = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                allPackages = DataAccess.GetAllPackages(connection);
            }
            return allPackages;
        }




        public List<Package> getPackagesByLocation(DistributionCentre  xCentre)
        {                                                              // Added by pablo on 23-03-15
            List<Package> allPackages = this.GetAllPackages();         // get all packages
            List<Package> myPackages = new List<Package>();            // create empty list 
            foreach (Package p in allPackages)
            { if (p.CurrentLocation==xCentre) myPackages.Add(p); }
            return myPackages;                                          // return subset of packages
        }


        public List<Package> getPackagesByStatus(PackageStatus s)
        {                                                              // Added by pablo on 23-03-15
            List<Package> allPackages = this.GetAllPackages();         // get all packages
            List<Package> myPackages = new List<Package>();            // create empty list 
            foreach (Package p in allPackages)
            { if (p.CurrentStatus.Equals(s)) myPackages.Add(p); }
            return myPackages;                                          // return subset of packages
        }


        public List<Package> getPackagesInTransit()                      // added by Pablo on 23-03-15
        {
            return this.getPackagesByStatus(PackageStatus.InTransit);
        }





    }
}
