using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENetCare.Repository.Repository
{
    public interface IPackageRepository
    {
        int Insert(Package package);
        void Update(Package package);
    }
}
