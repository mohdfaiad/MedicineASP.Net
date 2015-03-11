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
    }
}
