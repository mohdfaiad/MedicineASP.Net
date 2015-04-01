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
    public partial class Audit : System.Web.UI.Page
    {
        private PackageService _packageService;
        private ReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            IReportRepository reportRepository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(reportRepository);
                                    
            if (!Page.IsPostBack)
            {
                var packageTypes = _packageService.GetAllStandardPackageTypes();

                ddlPackageType.DataTextField = "Description";
                ddlPackageType.DataValueField = "PackageTypeId";
                ddlPackageType.DataSource = packageTypes;
                ddlPackageType.DataBind();               
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex == 0)
            {
                List<string> barCodeList = ucPackageBarcode.GetBarcodes();
                StandardPackageType packageType = new StandardPackageType()
                {
                    PackageTypeId = int.Parse(ddlPackageType.SelectedValue)
                };

                var employeeUser = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
                var currentLocation = new DistributionCentre()
                {
                    CentreId = employeeUser.DistributionCentreId
                };

                var reconciledPackages = _reportService.GetReconciledPackages(currentLocation, packageType, barCodeList);
               
                grd.DataSource = reconciledPackages;
                grd.DataBind();
            }
        }

        protected void Wizard1_Load(object sender, EventArgs e)
        {

        }

        protected void ddlPackageType_DataBound(object sender, EventArgs e)
        {
            ddlPackageType.Items.Insert(0, new ListItem("--- Package Type ---", String.Empty));
        }
    }
}