using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.ViewData
{
    public class StocktakingPackage
    {
        public int PackageId { get; set; }
        public string BarCode { get; set;  }
        public int PackageTypeId { get; set; }
        public string PackageTypeDescription { get; set; }
        public decimal CostPerPackage { get; set; }        
        public DateTime ExpirationDate { get; set; }
        public int DaysLeft { get; set; }

        public override string ToString()
            { return "Id)"+PackageId+" / "+PackageTypeDescription+ " / "+CostPerPackage; }

        public void setDaysLeft() { DaysLeft = DaysLeftToExpiration(); }

        public int DaysLeftToExpiration()
        {
            TimeSpan diff = ExpirationDate.Date - DateTime.Today;
            return (int)diff.TotalDays;
        }
    }
}
