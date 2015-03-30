using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class DistributionCentreStock
    {
        public int PackageTypeId { get; set; }
        public string PackageTypeDescription { get; set; }
        public decimal CostPerPackage { get; set; }
        public int DistributionCentreId { get; set; }
        public string DistributionCentreName { get; set; }
        public int NumberOfPackages { get; set; }
        public decimal TotalValue { get; set; }
    }
}
