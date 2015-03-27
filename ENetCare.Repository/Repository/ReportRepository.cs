using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public class ReportRepository : IReportRepository
    {
        private string _connectionString;
        
        public ReportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }     
        public List<DistributionCentreStock> GetDistributionCentreStock()
        {
            List<DistributionCentreStock> stockList = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                stockList = ViewDataAccess.GetDistributionCentreStock(connection);
            }
            return stockList;
        }
    }
}
