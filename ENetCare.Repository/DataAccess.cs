using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENetCare.Repository.Data;
using System.Data;

namespace ENetCare.Repository
{
    public class DataAccess
    {
        public static int InsertPackage(SqlConnection connection, Package package)
        {
            // define INSERT query with parameters 
            string query = "INSERT INTO dbo.Package (BarCode, ExpirationDate, PackageTypeId, CurrentStatus) " +
                           "VALUES (@BarCode, @ExpirationDate, @PackageTypeId, @CurrentStatus) " +
                           "SET @newId = SCOPE_IDENTITY();";

            using (var cmd = new SqlCommand(query, connection))
            {
                // define parameters and their values 
                cmd.Parameters.Add("@BarCode", SqlDbType.VarChar, 20).Value = package.BarCode ?? string.Empty;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = package.ExpirationDate;
                cmd.Parameters.Add("@PackageTypeId", SqlDbType.Int).Value = package.PackageType.PackageTypeId;
                cmd.Parameters.Add("@CurrentStatus", SqlDbType.VarChar, 20).Value =
                    package.CurrentStatus.ToString().ToUpper();
                cmd.Parameters.Add("@newId", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.Text;
                
                cmd.ExecuteScalar();

                return (int)cmd.Parameters["@newId"].Value;
            }            
        }

        public static void UpdatePackage(SqlConnection connection, Package package)
        {
            string cmdStr = "UPDATE dbo.Package SET BarCode = @BarCode, " +
                                "CurrentLocationCentreId = @CurrentLocationCentreId, " +
                                "CurrentStatus = @CurrentStatus, " +
                                "DistributedByEmployeeId = @DistributedByEmployeeId " +
                                "WHERE PackageId = @PackageId";

            using (var cmd = new SqlCommand(cmdStr, connection))
            {
                cmd.Parameters.AddWithValue("@BarCode", package.BarCode);
                cmd.Parameters.AddWithValue("@CurrentLocationCentreId", package.CurrentLocation == null ? DBNull.Value : (object)package.CurrentLocation.CentreId);
                cmd.Parameters.AddWithValue("@CurrentStatus", package.CurrentStatus.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@DistributedByEmployeeId", package.DistributedBy == null ? DBNull.Value : (object)package.DistributedBy.EmployeeId);
                cmd.Parameters.AddWithValue("@PackageId", package.PackageId);

                int effected = cmd.ExecuteNonQuery();                
            }
        }

        public static Package GetPackage(SqlConnection connection, int? packageId, string barcode)
        {
            Package package = new Package();
            package.PackageType = new StandardPackageType();

            string query = "SELECT PackageId, BarCode, ExpirationDate, PackageTypeId, CurrentLocationCentreId, CurrentStatus, DistributedByEmployeeId FROM Package WHERE PackageId = ISNULL(@packageId, PackageId) AND BarCode = ISNULL(@barcode, BarCode)";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@packageId", packageId.HasValue ? packageId.Value : (object)DBNull.Value);

            cmd.Parameters.AddWithValue("@barcode", string.IsNullOrEmpty(barcode) ? (object)DBNull.Value : barcode);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    package.PackageId = Convert.ToInt32(reader["PackageId"]);
                    package.BarCode = (string)reader["BarCode"];
                    package.ExpirationDate = (DateTime)reader["ExpirationDate"];
                    package.PackageType.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    if (reader["CurrentLocationCentreId"] != DBNull.Value)
                    {
                        package.CurrentLocation = new DistributionCentre();
                        package.CurrentLocation.CentreId = Convert.ToInt32(reader["CurrentLocationCentreId"]);
                    }

                    if (reader["CurrentStatus"] != DBNull.Value)
                    {
                        package.CurrentStatus = (PackageStatus)Enum.Parse(typeof(PackageStatus), (string)reader["CurrentStatus"]);
                    }

                    if (reader["DistributedByEmployeeId"] != DBNull.Value)
                    {
                        package.DistributedBy = new Employee();  
                        package.DistributedBy.EmployeeId = Convert.ToInt32(reader["DistributedByEmployeeId"]);
                    }
                }
            }
            return package;
        }


    }
}
