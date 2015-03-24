using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Data
{
    class PackageTransit
    {
        public int TransitId { get; set; }
        public Package Package { get; set; }
        public DistributionCentre SenderCentre { get; set; }
        public DistributionCentre ReceiverCentre { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateCancelled { get; set; }

    }
}
