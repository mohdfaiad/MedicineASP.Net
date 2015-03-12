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
    }
}
