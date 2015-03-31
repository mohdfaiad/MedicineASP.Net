using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.BusinessService
{
    class TransitResult
    {

        public const string BarCodeNotFound = "Bar Code not found";
        public const string PackageElsewhere = "Package appears as located elsewhere";
        public const string PackageNotInStock = "Package appears not to be in Stock";
        public const string PackageAlreadyAtDestination = "Package appears as being already at the Destination Centre";
        public const string TransitNotFound = "Transit not found";
        public const string MoreThanOneTransitForPackage = "More than one active transit exists for that package";
    
    }
}
