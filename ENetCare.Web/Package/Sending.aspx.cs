using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class Sending : System.Web.UI.Page
    {
        private PackageService _packageService;
        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);
        }
    }
}