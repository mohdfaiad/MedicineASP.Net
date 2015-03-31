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

        public Package Get(int? packageId)                           // Added by Pablo on 24-03-15
        {                                                             // overload for get with single argument
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<Package> allPackages = DataAccess.GetAllPackages(connection);
                foreach (Package p in allPackages) if (p.PackageId == packageId) return p;
            }
            return null;
        }


       public Package GetPackageWidthBarCode(string barCode)                           // Added by Pablo on 24-03-15
        { 
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();  
            List<Package> allPackages = DataAccess.GetAllPackages(connection);
            foreach(Package p in allPackages) if(p.BarCode==barCode) return p;
        }
           return null;     
        }

        public List<StandardPackageType> GetAllStandardPackageTypes()
        {
            List<StandardPackageType> packageTypes = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                packageTypes = DataAccess.GetAllStandardPackageTypes(connection);
            }
            return packageTypes;
        }

        public StandardPackageType GetStandardPackageType(int packageId)
        {
            StandardPackageType packageTypes = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                packageTypes = DataAccess.GetStandardPackageType(connection, packageId);
            }
            return packageTypes;
        }


        public string getConnectionString() { return _connectionString; }

        public int InsertTransit(PackageTransit packageTransit)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return DataAccess.InsertPackageTransit(connection, packageTransit);
            }
        }

        public void UpdateTransit(PackageTransit packageTransit)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                DataAccess.UpdatePackageTransit(connection, packageTransit);
            }
            return;
        }

        public PackageTransit GetTransit(Package package, DistributionCentre receiver)
        {
            PackageTransit packageTransit = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                packageTransit = DataAccess.GetPackageTransit(connection, package, receiver);

                //packageTransit.Package= DataAccess.GetPackage(connection, package.PackageId, package.BarCode);
                
                //DistributionCentre distributionCenter = DataAccess.GetDistributionCentre(connection, package.CurrentLocation.CentreId);
                
                if (packageTransit == null)
                    return null;
                //
                //
                //
                //
                //
                //
                //
            }
            return packageTransit;
        }
    }
}
