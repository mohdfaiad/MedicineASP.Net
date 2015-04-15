using ENetCare.Repository.Data;
using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public List<DistributionCentreLosses> GetDistributionCentreLosses()
        {
            List<DistributionCentreLosses> centreList = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                centreList = ViewDataAccess.GetDistributionCentreLosses(connection);
            }
            return centreList;
        }

        public List<DoctorActivity> GetDoctorActivity()
        {
            List<DoctorActivity> doctors = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                doctors = ViewDataAccess.GetDoctorActivity(connection);
            }
            return doctors;
        }

        public List<GlobalStock> GetGlobalStock()
        {
            List<GlobalStock> stocks = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                stocks = ViewDataAccess.GetGlobalStock(connection);
            }
            return stocks;
        }

        public List<ValueInTransit> GetValueInTransit()
        {
            List<ValueInTransit> valueList = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                valueList = ViewDataAccess.GetValueInTransit(connection);
            }
            return valueList;
        }

        public List<ReconciledPackage> GetReconciledPackages(DistributionCentre currentLocation, StandardPackageType packageType, List<string> barCodeList)
        {
            List<ReconciledPackage> packageList = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                XElement barCodeXml = barCodeList.GetBarCodeXML();

                packageList = ViewDataAccess.GetReconciledPackages(connection, currentLocation, packageType, barCodeXml);
            }
            return packageList;
        }


        public List<StocktakingPackage> GetStocktaking(int CentreId)
        {
            List<StocktakingPackage> packageList = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                packageList = ViewDataAccess.GetStocktaking(connection, CentreId);
            }
            return packageList;
        }


    }
}
