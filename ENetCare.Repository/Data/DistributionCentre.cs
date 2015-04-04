using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Data
{
    [Serializable]
    public class DistributionCentre
    {
        public int CentreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsHeadOffice { get; set; }

        public string ToString()
        {
            return "Id)" + CentreId + " / " + Name + " / " + Address + " / " + Phone; 
        }

    }

   

}
