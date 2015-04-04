using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Data
{
    public class Package
    {
        public int PackageId { get; set; }
        public string BarCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public StandardPackageType PackageType { get; set; }
        public DistributionCentre CurrentLocation { get; set; }
        public PackageStatus CurrentStatus { get; set; }
        public Employee DistributedBy { get; set; }


        public string GetShortDescription()
        {
            return PackageType + " / Exp: " + ExpirationDate + " / Status:" + CurrentStatus;
        }

        public override string ToString()
        {
            return "Id)" + PackageId + " / " + PackageType + " / Exp: " + ExpirationDate + " / Status:" + CurrentStatus + " / Locaton: " + CurrentLocation;
        }

    }
}
