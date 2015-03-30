using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class DistributionCentreLosses
    {
        public int DistributionCentreId { get; set; }
        public string DistributionCentreName { get; set; }
        public int LossRatioNumerator { get; set; }
        public int LossRatioDenominator { get; set; }
        public decimal TotalLossDiscardedValue { get; set; }
    }
}
