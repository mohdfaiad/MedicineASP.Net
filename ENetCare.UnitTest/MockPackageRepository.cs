using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.UnitTest
{
    public class MockPackageRepository : IPackageRepository
    {
        public int Insert(Package package)
        {
            return 1;
        }

        public void Update(Package package)
        {
            return;
        }

        public Package Get(int? packageId, string barcode)
        {
            Package package = new Package
            {
                PackageId = packageId ?? 1,
                PackageType = new StandardPackageType
                {
                    PackageTypeId = 1,
                    Description = "100 Panadol Tablets",
                    NumberOfMedications = 100,
                    ShelfLifeUnitType = ShelfLifeUnitType.Month,
                    ShelfLifeUnits = 6,
                    TemperatureSensitive = false,
                    Value = 3.50M
                },
                BarCode = string.IsNullOrEmpty(barcode) ? "000012015070100001" : barcode,
                ExpirationDate = new DateTime(2015, 7, 1),
                CurrentLocation = new DistributionCentre
                {
                    CentreId = 4,
                    Name = "North Centre",
                    Address = "5km past the rive bend",
                    Phone = "0490 123 456",
                    IsHeadOffice = false
                },
                CurrentStatus = PackageStatus.InStock
            };
            return package;
        }


        public Package Get(string id) { return null; }


    }
}
