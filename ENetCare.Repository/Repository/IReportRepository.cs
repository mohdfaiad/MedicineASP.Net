﻿using ENetCare.Repository.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public interface IReportRepository
    {
        List<DistributionCentreStock> GetDistributionCentreStock();
    }
}
