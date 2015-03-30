using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class DoctorActivity
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PackageTypeId { get; set; }
        public string PackageTypeDescription { get; set; }
        public int PackageCount { get; set; }
        public decimal TotalPackageValue { get; set; }
    }
}
