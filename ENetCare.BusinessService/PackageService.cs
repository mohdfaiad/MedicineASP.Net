using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.BusinessService
{
    public class PackageService
    {
        private IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public DateTime CalculateExpirationDate(StandardPackageType packageType, DateTime startDate)
        {
            if (packageType == null)
                return DateTime.MinValue;

            if (packageType.ShelfLifeUnitType == ShelfLifeUnitType.Month)
                return startDate.AddMonths(packageType.ShelfLifeUnits);
            else
                return startDate.AddDays(packageType.ShelfLifeUnits);
        }

        public Result Register(StandardPackageType packageType, DistributionCentre location, DateTime expirationDate, out string barcode)
        {
            var result = new Result
            {
                Success = true
            };

            Package package = new Package
            {
                PackageType = packageType,
                CurrentLocation = location,
                CurrentStatus = PackageStatus.InStock,
                ExpirationDate = expirationDate
            };

            int packageId = _packageRepository.Insert(package);
            package.PackageId = packageId;

            barcode = GenerateBarCode(package);

            package.BarCode = barcode;

            _packageRepository.Update(package);

            result.Id = package.PackageId;
            return result;
        }

        public Package Retrieve(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
                return null;

            return _packageRepository.Get(null, barcode);
        }

        public List<StandardPackageType> GetAllStandardPackageTypes()
        {
            return _packageRepository.GetAllStandardPackageTypes();
        }

        private string GenerateBarCode(Package package)
        {
            if (package.PackageType == null)
                return string.Empty;
            
            return string.Format("{0:D5}{1:yyMMdd}{2:D5}", package.PackageType.PackageTypeId, package.ExpirationDate, package.PackageId);
        }

    }
}
