using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Data
{
    public class PackageTransit
    {
        public int TransitId { get; set; }
        public Package Package { get; set; }
        public DistributionCentre SenderCentre { get; set; }
        public DistributionCentre ReceiverCentre { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateCancelled { get; set; }

        public override string ToString()
        {
            string head = "pack)" + Package.GetShortDescription() + " / From:" + SenderCentre.Name + " / To:" + ReceiverCentre.Name + " / ";
            string tail="";
            if(WasReceived())  tail = " / Sent:" + DateSent + " / Received:" + DateReceived;
            if(WasCancelled()) tail = " / Sent:" + DateSent + " / Cancelled:" + DateCancelled;
            if(IsInTransit())  tail = " / Sent:" + DateSent + " / Received:" + DateReceived;    
            return head + tail;
        }

        public bool IsPastTransit()
        { return (DateReceived != null || DateCancelled != null); }

        public bool IsInTransit()
        { return (DateReceived == null && DateCancelled == null); }

        public bool WasCancelled()
        { return DateCancelled!=null; }

        public bool WasReceived()
        { return DateReceived != null; }

    }
}
