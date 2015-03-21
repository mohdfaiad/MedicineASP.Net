using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENetCare.Repository.Data;


namespace ENetCare.Repository.Data
{
    class Audit
    {
        public DistributionCentre DistributioonCentre { get; set; }
        public Employee Employee { get; set; }
        public List<Package> Packages { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

    }
}
