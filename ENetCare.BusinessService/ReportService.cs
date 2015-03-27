using ENetCare.Repository.Repository;
using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.BusinessService
{
    public class ReportService
    {
        private IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        
        public List<DistributionCentreStock> GetDistributionCentreStock()
        {
            return _reportRepository.GetDistributionCentreStock();
        }
    }
}
