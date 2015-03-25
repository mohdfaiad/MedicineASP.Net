﻿using System;
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

        public static void UpdateEmployee(SqlConnection connection, Employee employee)
        {
            string cmdStr = "UPDATE dbo.Employee SET Password = @Password, " +
                                "FullName = @FullName, " +
                                "EmailAddress = @EmailAddress, " +
                                "LocationCentreId = @LocationCentreId, " +
                                "EmployeeType = @EmployeeType " +                                
                                "WHERE EmployeeId = @EmployeeId";

            using (var cmd = new SqlCommand(cmdStr, connection))
            {                
                cmd.Parameters.AddWithValue("@Password", employee.Password);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);
                cmd.Parameters.AddWithValue("@EmployeeType", employee.EmployeeType.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@LocationCentreId", employee.Location.CentreId);
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                int effected = cmd.ExecuteNonQuery();
            }
        }

        public static Package GetPackage(SqlConnection connection, int? packageId, string barcode)
        {
            Package package = null;            

            string query = "SELECT PackageId, BarCode, ExpirationDate, PackageTypeId, CurrentLocationCentreId, CurrentStatus, DistributedByEmployeeId FROM Package WHERE PackageId = ISNULL(@packageId, PackageId) AND BarCode = ISNULL(@barcode, BarCode)";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@packageId", packageId.HasValue ? packageId.Value : (object)DBNull.Value);

            cmd.Parameters.AddWithValue("@barcode", string.IsNullOrEmpty(barcode) ? (object)DBNull.Value : barcode);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    package = new Package();
                    package.PackageType = new StandardPackageType();                    

                    package.PackageId = Convert.ToInt32(reader["PackageId"]);
                    package.BarCode = (string)reader["BarCode"];
                    package.ExpirationDate = (DateTime)reader["ExpirationDate"];
                    package.PackageType.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    if (reader["CurrentLocationCentreId"] != DBNull.Value)
                    {
                        package.CurrentLocation = new DistributionCentre();
                        package.CurrentLocation.CentreId = Convert.ToInt32(reader["CurrentLocationCentreId"]);
                    }

                    package.CurrentStatus = (PackageStatus)Enum.Parse(typeof(PackageStatus), (string)reader["CurrentStatus"], true);
                    
                    if (reader["DistributedByEmployeeId"] != DBNull.Value)
                    {
                        package.DistributedBy = new Employee();  
                        package.DistributedBy.EmployeeId = Convert.ToInt32(reader["DistributedByEmployeeId"]);
                    }
                }
            }
            return package;
        }




        public static Employee GetEmployee(SqlConnection connection, int? employeeId, string username)
        {
            Employee employee = null;
            
            string query = "SELECT EmployeeId, UserName, Password, FullName, EmailAddress, EmployeeType, LocationCentreId FROM Employee WHERE EmployeeId = ISNULL(@employeeId, EmployeeId) AND UserName = ISNULL(@username, UserName)";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@employeeId", employeeId.HasValue ? employeeId.Value : (object)DBNull.Value);

            cmd.Parameters.AddWithValue("@username", string.IsNullOrEmpty(username) ? (object)DBNull.Value : username);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    employee = new Employee();
                    employee.Location = new DistributionCentre();                    
                    
                    employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    employee.UserName = (string)reader["UserName"];
                    employee.Password = (string)reader["Password"];
                    employee.FullName = (string)reader["FullName"];
                    employee.EmailAddress = (string)reader["EmailAddress"];
                    employee.EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), (string)reader["EmployeeType"], true);
                    employee.Location.CentreId = Convert.ToInt32(reader["LocationCentreId"]);
                }
            }
            return employee;
        }

        public static DistributionCentre GetDistributionCentre(SqlConnection connection, int centreId)
        {
            DistributionCentre centre = null;
            string query = "SELECT CentreId, Name, Address, Phone, IsHeadOffice FROM DistributionCentre WHERE CentreId = @centreId";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@centreId", centreId);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    centre = new DistributionCentre();

                    centre.CentreId = Convert.ToInt32(reader["CentreId"]);
                    centre.Name = (string)reader["Name"];
                    centre.Address = (string)reader["Address"];
                    centre.Phone = (string)reader["Phone"];
                    centre.IsHeadOffice = (bool)reader["IsHeadOffice"];
                }
            }

            return centre;
        }

        public static StandardPackageType GetStandardPackageType(SqlConnection connection, int packageTypeId)
        {
            StandardPackageType packageType = null;
            string query = "SELECT PackageTypeId, Description, NumberOfMedications, ShelfLifeUnitType, ShelfLifeUnits, TemperatureSensitive, Value FROM StandardPackageType WHERE PackageTypeId = @packageTypeId";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@packageTypeId", packageTypeId);

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.Read())
                {
                    packageType = new StandardPackageType();

                    packageType.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    packageType.Description = (string)reader["Description"];
                    packageType.NumberOfMedications = Convert.ToInt32(reader["NumberOfMedications"]);
                    packageType.ShelfLifeUnitType = (ShelfLifeUnitType)Enum.Parse(typeof(ShelfLifeUnitType), (string)reader["ShelfLifeUnitType"], true);
                    packageType.ShelfLifeUnits = Convert.ToInt32(reader["ShelfLifeUnits"]);
                    packageType.TemperatureSensitive = (bool)reader["TemperatureSensitive"];
                    packageType.Value = (decimal)reader["Value"];
                }
            }

            return packageType;
        }

        public static List<DistributionCentre> GetAllDistributionCentres(SqlConnection connection)
        {
            var centres = new List<DistributionCentre>();
            string query = "SELECT CentreId, Name, Address, Phone, IsHeadOffice FROM DistributionCentre ORDER BY CentreId";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;
            
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    var centre = new DistributionCentre();

                    centre.CentreId = Convert.ToInt32(reader["CentreId"]);
                    centre.Name = (string)reader["Name"];
                    centre.Address = (string)reader["Address"];
                    centre.Phone = (string)reader["Phone"];
                    centre.IsHeadOffice = (bool)reader["IsHeadOffice"];

                    centres.Add(centre);
                }
            }

            return centres;
        }

        public static List<StandardPackageType> GetAllStandardPackageTypes(SqlConnection connection)
        {
            var packageTypes = new List<StandardPackageType>();            
            string query = "SELECT PackageTypeId, Description, NumberOfMedications, ShelfLifeUnitType, ShelfLifeUnits, TemperatureSensitive, Value FROM StandardPackageType ORDER BY PackageTypeId";

            var cmd = new SqlCommand(query);
            cmd.Connection = connection;

            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while(reader.Read())
                {
                    var packageType = new StandardPackageType();

                    packageType.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    packageType.Description = (string)reader["Description"];
                    packageType.NumberOfMedications = Convert.ToInt32(reader["NumberOfMedications"]);
                    packageType.ShelfLifeUnitType = (ShelfLifeUnitType)Enum.Parse(typeof(ShelfLifeUnitType), (string)reader["ShelfLifeUnitType"], true);
                    packageType.ShelfLifeUnits = Convert.ToInt32(reader["ShelfLifeUnits"]);
                    packageType.TemperatureSensitive = (bool)reader["TemperatureSensitive"];
                    packageType.Value = (decimal)reader["Value"];

                    packageTypes.Add(packageType);
                }
            }

            return packageTypes;
        }

        public static List<Package> GetAllPackages(SqlConnection connection)
        {                                                          // Added by Pablo on 24-03-15
            Package package = null;
            string query = "SELECT PackageId, BarCode, ExpirationDate, PackageTypeId, CurrentLocationCentreId, CurrentStatus, DistributedByEmployeeId FROM Package ";
            var cmd = new SqlCommand(query);
            List<Package> allPackages = new List<Package>();
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    package = new Package();
                    package.PackageType = new StandardPackageType();
                    package.PackageId = Convert.ToInt32(reader["PackageId"]);
                    package.BarCode = (string)reader["BarCode"];
                    package.ExpirationDate = (DateTime)reader["ExpirationDate"];
                    package.PackageType.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    if (reader["CurrentLocationCentreId"] != DBNull.Value)
                    {
                        package.CurrentLocation = new DistributionCentre();
                        package.CurrentLocation.CentreId = Convert.ToInt32(reader["CurrentLocationCentreId"]);
                    }
                    package.CurrentStatus = (PackageStatus)Enum.Parse(typeof(PackageStatus), (string)reader["CurrentStatus"], true);
                    if (reader["DistributedByEmployeeId"] != DBNull.Value)
                    {
                        package.DistributedBy = new Employee();
                        package.DistributedBy.EmployeeId = Convert.ToInt32(reader["DistributedByEmployeeId"]);
                    }
                    allPackages.Add(package);
                }
            }
            return allPackages;
        }


        public static List<Employee> GetAllEmployees(SqlConnection connection)
        {                                                   // Added by Pablo on 24-03-15
            var allEmployees = new List<Employee>();
            string query = "SELECT CentreId, Name, Address, Phone, IsHeadOffice FROM DistributionCentre ORDER BY CentreId";
            var cmd = new SqlCommand(query);
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    var employee = new Employee();
                    employee.EmailAddress = (string)reader["EmailAddress"];
                    employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    employee.EmployeeType = (EmployeeType)reader["EmployeeType"];
                    employee.FullName = (string)reader["FullName"];
                    employee.Location = DataAccess.GetDistributionCentre(connection, Convert.ToInt32(reader["EmployeeId"]));
                    employee.Password = (string)reader["Password"];
                    employee.UserName = (string)reader["UserName"];
                    allEmployees.Add(employee);
                }
            }
            return allEmployees;
        }





        public static int InsertPackageTransit(SqlConnection connection, PackageTransit packageT)
        {                                                                       // Added by Pablo on 24/03/15
            // define INSERT query with parameters 
            string query = " INSERT INTO dbo.PackageTransit (Package , SenderCentre,  " +
                           " ReceiverCentre, DateSent, DateReceived, DateCancelled)  " +
                           "  SET @newId = SCOPE_IDENTITY();";
            var cmd = new SqlCommand(query);
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                cmd.Parameters.Add("@Package", SqlDbType.Int).Value = (int)packageT.Package.PackageId;
                cmd.Parameters.Add("@SenderCentre", SqlDbType.Int).Value = (int)packageT.SenderCentre.CentreId;
                cmd.Parameters.Add("@ReceiverCentre", SqlDbType.Int).Value = (int)packageT.ReceiverCentre.CentreId;
                cmd.Parameters.Add("@DateSent", SqlDbType.Date).Value = (DateTime)packageT.DateSent;
                cmd.Parameters.Add("@DateReceived", SqlDbType.Date).Value = (DateTime)packageT.DateReceived;
                cmd.Parameters.Add("@DateReceived", SqlDbType.Date).Value = (DateTime)packageT.DateCancelled;
                cmd.Parameters.Add("@newId", SqlDbType.Int).Direction = ParameterDirection.Output;
            }
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteScalar();
            return (int)cmd.Parameters["@newId"].Value;
        }


        public static void UpdatePackageTransit(SqlConnection connection, PackageTransit transit)
        {

        }

        public static List<PackageTransit> getAllPackageTransits(SqlConnection connection)
        {
            return null;
        }







    }
}
