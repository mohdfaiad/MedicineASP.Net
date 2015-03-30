using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class ValueInTransit
    {
        public int SenderDistributionCentreId { get; set; }
        public string SenderDistributionCentreName { get; set; }
        public int ReceiverDistributionCentreId { get; set; }
        public string ReceiverDistributionCentreName { get; set; }
        public int TotalPackages { get; set; }
        public decimal TotalValue { get; set; }
    }
}
