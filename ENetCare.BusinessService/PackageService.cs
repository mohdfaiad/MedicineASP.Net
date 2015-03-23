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

        private string GenerateBarCode(Package package)
        {
            if (package.PackageType == null)
                return string.Empty;
            
            return string.Format("{0:D5}{1:yyyyMMdd}{2:D5}", package.PackageType.PackageTypeId, package.ExpirationDate, package.PackageId);
        }




        public Result Send(string barCode, DistributionCentre senderCentre, DateTime date )
        {                                                   // Created by Pablo on 22-03-15
            Result sendResult = new Result();
            Package package;
            //package = _packageRepository.G
            package = _packageRepository.Get(barCode);
            if (package == null)                         // Case: not found
                {
                    sendResult.ErrorMessage = "Bar Code not found";
                    sendResult.Success = false;
                    return sendResult;
                }
            if(package.CurrentLocation!=senderCentre)    //  Case: not in this centre
                {
                    sendResult.ErrorMessage = "Package appears as located elsewhere";
                    sendResult.Success = false;
                    return sendResult;                    
                }
            if (package.CurrentStatus!=PackageStatus.InStock )  // Case: not in stock 
            {                                               
                sendResult.ErrorMessage = "Package appears not to be in Stock";
                sendResult.Success = false;
                return sendResult;
            }
            if (package.CurrentLocation==senderCentre)          // Case:  Desitiny = Sending Centre
            {
                sendResult.ErrorMessage = "Package appears as being already at the Destination Centre";
                sendResult.Success = false;
                return sendResult;
            }
            package.CurrentStatus = PackageStatus.InTransit;        // Proceed to set it as intransit
            package.CurrentLocation = null;                         // Remove current location 
            _packageRepository.Update(package);                     // Update package
                                                                    // Add transit
            sendResult.Success = true;
            return sendResult;
        }


        public Result Receive(string barCode, DistributionCentre senderCentre, DateTime date)
        {                                                   // Created by Pablo on 22-03-15
            Result receiveResult = new Result();


            return receiveResult;
        }

    }
}
