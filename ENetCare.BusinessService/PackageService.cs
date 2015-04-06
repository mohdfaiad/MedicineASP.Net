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

        public StandardPackageType GetStandardPackageType(int packageId)
        {
            return _packageRepository.GetStandardPackageType(packageId);
        }

        private string GenerateBarCode(Package package)
        {
            if (package.PackageType == null) return string.Empty;
            return string.Format("{0:D5}{1:yyMMdd}{2:D5}", package.PackageType.PackageTypeId, package.ExpirationDate, package.PackageId);
            //return string.Format("00001{0:yyMMdd}00001", package.PackageType.PackageTypeId, package.ExpirationDate, package.PackageId);
        }

        public Result Send(string barCode, DistributionCentre senderCentre, DateTime date)
        {                                                          // (P. 24-03-2015)
            Result sendResult = new Result();
            Package package;
            package = _packageRepository.GetPackageWidthBarCode(barCode);
            if (package == null)                         // Case: not found
            {
                sendResult.ErrorMessage = TransitResult.BarCodeNotFound;
                sendResult.Success = false;
                return sendResult;
            }
            if (package.CurrentLocation != senderCentre)    //  Case: not in this centre
            {
                sendResult.ErrorMessage = TransitResult.PackageElsewhere;
                sendResult.Success = false;
                return sendResult;
            }
            if (package.CurrentStatus != PackageStatus.InStock)  // Case: not in stock 
            {
                sendResult.ErrorMessage = TransitResult.PackageNotInStock;
                sendResult.Success = false;
                return sendResult;
            }
            if (package.CurrentLocation == senderCentre)          // Case:  Desitiny = Sending Centre
            {
                sendResult.ErrorMessage = TransitResult.PackageAlreadyAtDestination;
                sendResult.Success = false;
                return sendResult;
            }
            package.CurrentStatus = PackageStatus.InTransit;        // Proceed to set it as intransit
            package.CurrentLocation = null;                         // Remove current location 
            _packageRepository.Update(package);                     // Update package
            sendResult.Success = true;
            return sendResult;
        }

        public Result Receive(string barCode, DistributionCentre receiverCentre, DateTime date)
        {                                                                 // (P. 24-03-2015)
            Result receiveResult = new Result();
            Package package = _packageRepository.GetPackageWidthBarCode(barCode);
            if (package == null)                         // Case: not found
            {
                receiveResult.ErrorMessage = TransitResult.BarCodeNotFound;
                receiveResult.Success = false;
                return receiveResult;
            }
            List<PackageTransit> activeTransits = _packageRepository.GetActiveTransitsByPackage(package);
            if (activeTransits.Count() == 0)                         // Case: not found
            {
                receiveResult.ErrorMessage = TransitResult.TransitNotFound;
                receiveResult.Success = false;
                return receiveResult;
            }
            if (activeTransits.Count() > 1)                         // Case: many found
            {
                receiveResult.ErrorMessage = TransitResult.MoreThanOneTransitForPackage;
                receiveResult.Success = false;
                return receiveResult;
            }
            PackageTransit transit = activeTransits.ElementAt(0);   // get the only item found
            transit.DateReceived = DateTime.Today;                  // set transit as received
            _packageRepository.UpdateTransit(transit);              // update transits DB
            package.CurrentStatus = PackageStatus.InStock;          // set packagestatus
            package.CurrentLocation = receiverCentre;               // set package location
            _packageRepository.Update(package);                     // update packages DB
            receiveResult.Success = true;
            return receiveResult;
        }

        public Result cancelTransit(string barCode, DateTime dateCancelled)        // (P. 07-04-2015)
        {
            Result cResult = new Result();
            Package package = _packageRepository.GetPackageWidthBarCode(barCode);
            if (package == null)                         // Case: not found
            {
                cResult.ErrorMessage = TransitResult.BarCodeNotFound;
                cResult.Success = false;
                return cResult;
            }
            List<PackageTransit> activeTransits = _packageRepository.GetActiveTransitsByPackage(package);
            if (activeTransits.Count() == 0)                         // Case: not found
            {
                cResult.ErrorMessage = TransitResult.TransitNotFound;
                cResult.Success = false;
                return cResult;
            }
            PackageTransit transit = activeTransits.ElementAt(0);
            transit.DateCancelled = DateTime.Today;                  // set transit as cancelled
            _packageRepository.UpdateTransit(transit);              // update transits DB
            package.CurrentStatus = PackageStatus.Lost;             // set packagestatus
            _packageRepository.Update(package);                     // update packages DB
            cResult.Success = true;
            return cResult;
        }


        public Result Distribute(string barCode, DistributionCentre distributionCentre, Employee employee, DateTime expirationDate, StandardPackageType packageType, int packageId)
        {
            var result = new Result
            {
                Success = true
            };

            Package tempPackage = Retrieve(barCode);
            if (tempPackage == null)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.BarCodeNotFound + tempPackage.BarCode;
                return result;
            }

            if (employee.EmployeeType == EmployeeType.Manager)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.EmployeeNotAuthorized;
                return result;
            }

            if (employee.Location.CentreId != tempPackage.CurrentLocation.CentreId)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.EmployeeInDifferentLocation + barCode;
                return result;
            }

            if (tempPackage.CurrentStatus == PackageStatus.Distributed)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.PackageAlreadyDistributed + tempPackage.BarCode;
                return result;
            }

            else if (tempPackage.CurrentStatus == PackageStatus.InTransit)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.PackageInTransit + tempPackage.BarCode;
                return result;
            }

            if (distributionCentre.CentreId != tempPackage.CurrentLocation.CentreId)
            {
                result.Success = false;
                result.ErrorMessage = PackageResult.PackageElsewhere + tempPackage.BarCode;
                return result;
            }

            Package package = new Package
            {
                PackageType = packageType,
                CurrentLocation = distributionCentre,
                CurrentStatus = PackageStatus.Distributed,
                PackageId = packageId,
                ExpirationDate = expirationDate,
                DistributedBy = employee,
                BarCode = barCode
            };

            _packageRepository.Update(package);

            result.Id = package.PackageId;

            return result;
        }

        public Result PerformAudit(Employee employee, StandardPackageType packageType, List<string> barCodes)
        {
            Result result = new Result
            {
                Success = true
            };
            int auditId = _packageRepository.InsertAudit(employee, packageType, barCodes);
            result.Id = auditId;

            _packageRepository.UpdateLostFromAudit(auditId, employee.Location, packageType);

            _packageRepository.UpdateInstockFromAudit(auditId, employee.Location, packageType);

            _packageRepository.UpdateTransitReceivedFromAudit(auditId, employee.Location);

            _packageRepository.UpdateTransitCancelledFromAudit(auditId, employee.Location);
            return result;
        }

    }
}
