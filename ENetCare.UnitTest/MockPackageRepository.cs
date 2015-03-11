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
    }
}
