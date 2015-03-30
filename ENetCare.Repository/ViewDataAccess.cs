using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository
{
    public class ViewDataAccess
    {
        public static List<DistributionCentreStock> GetDistributionCentreStock(SqlConnection connection)
        {                                                   
            var stockList = new List<DistributionCentreStock>();
            string query = "select PackageTypeId, PackageTypeDescription, CostPerPackage, DistributionCentreId, DistributionCenterName, NumberOfPackages, TotalValue from DistributionCentreStock order by DistributionCentreId, PackageTypeId";
            var cmd = new SqlCommand(query);
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    var stock = new DistributionCentreStock();
                    if (reader["PackageTypeId"] != DBNull.Value)
                        stock.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    if (reader["PackageTypeDescription"] != DBNull.Value)
                        stock.PackageTypeDescription = (string)reader["PackageTypeDescription"];
                    if (reader["CostPerPackage"] != DBNull.Value)
                        stock.CostPerPackage = Convert.ToDecimal(reader["CostPerPackage"]);
                    if (reader["DistributionCentreId"] != DBNull.Value)
                        stock.DistributionCentreId = Convert.ToInt32(reader["DistributionCentreId"]);
                    if (reader["DistributionCenterName"] != DBNull.Value)
                        stock.DistributionCentreName = (string)reader["DistributionCenterName"];
                    if (reader["NumberOfPackages"] != DBNull.Value)
                        stock.NumberOfPackages = Convert.ToInt32(reader["NumberOfPackages"]);
                    if (reader["TotalValue"] != DBNull.Value)
                        stock.TotalValue = Convert.ToDecimal(reader["TotalValue"]);
                    stockList.Add(stock);

                }
            }
            return stockList;
        }

    }
}
