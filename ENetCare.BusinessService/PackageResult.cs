using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.BusinessService
{
    class PackageResult
    {
        public const string BarCodeNotFound = "Bar Code not found: ";
        public const string PackageElsewhere = "Package is NOT located in this distribution centre: ";
        public const string PackageAlreadyDistributed = "Package has been already distributed: ";
        public const string PackageInTransit = "The following package is in Transit: ";
        public const string EmployeeNotAuthorized = "You are not authorized to distribute packages";
        public const string EmployeeInDifferentLocation = "The following package is not in the current distribution centre: ";
    }
}
