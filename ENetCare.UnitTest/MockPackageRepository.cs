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

        public StandardPackageType GetStandardPackageType(int packageTypeId)
        {
            StandardPackageType stp = null;


            return stp;
        }

        public List<StandardPackageType> GetAllStandardPackageTypes()
        {
            var packageTypes = new List<StandardPackageType>();
            packageTypes.Add(new StandardPackageType()
            {
                PackageTypeId = 1,
                Description = "100 Panadol tablets",
                ShelfLifeUnitType = ShelfLifeUnitType.Month,
                ShelfLifeUnits = 3
            });
            
            packageTypes.Add(new StandardPackageType()
            {
                PackageTypeId = 2,
                Description = "25X200 Clorophin tablets",
                ShelfLifeUnitType = ShelfLifeUnitType.Day,
                ShelfLifeUnits = 67
            });

            packageTypes.Add(new StandardPackageType()
            {
                PackageTypeId = 3,
                Description = "50 Felix Tablets",
                ShelfLifeUnitType = ShelfLifeUnitType.Month,
                ShelfLifeUnits = 2
            });

            return packageTypes;
        }


        public Package Get(int? packageId)                                           // Added by Pablo on 24-03-15
        {            return null;        }


        public Package GetPackageWidthBarCode(string barCode)                           // Added by Pablo on 24-03-15
        {            return null;        }


        public string getConnectionString()
        {           return null;         }
        public int InsertTransit(PackageTransit pt)
        {
            Package tempPackage = new Package();
            tempPackage.PackageId = 1;
            tempPackage.BarCode = "012365423";
            DateTime dateTemp = new DateTime(2015, 05, 20);
            tempPackage.ExpirationDate = dateTemp;
            tempPackage.PackageType.PackageTypeId = 1;
            tempPackage.CurrentLocation.CentreId = 1;
            tempPackage.CurrentStatus = PackageStatus.InStock;
            // need more work to complete!
            return 1;
        }
        public void UpdateTransit(PackageTransit pt)
        {
            
        }
        public PackageTransit GetTransit(Package pk, DistributionCentre dc)
        {
            PackageTransit packetT = new PackageTransit();
            return packetT;
        }


    }
}
