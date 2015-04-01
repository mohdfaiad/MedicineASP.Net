using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class ReconciledPackage
    {
        public int PackageId { get; set; }
        public string BarCode { get; set; }
        public int CurrentLocationCentreId { get; set; }
        public PackageStatus CurrentStatus { get; set; }
        public PackageStatus NewStatus { get; set; }
    }
}
