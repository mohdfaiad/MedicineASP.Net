using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
using ENetCare.Web.UserControl;
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
        private EmployeeService _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            IEmployeeRepository employeeRepository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _employeeService = new EmployeeService(employeeRepository);

            IReportRepository reportRepository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(reportRepository);

            ucPackageBarcode.AddValidate += PackageBarcodeOnAdd;
           
            if (!Page.IsPostBack)
            {
                var packageTypes = _packageService.GetAllStandardPackageTypes();

                ddlPackageType.DataTextField = "Description";
                ddlPackageType.DataValueField = "PackageTypeId";
                ddlPackageType.DataSource = packageTypes;
                ddlPackageType.DataBind();               
            }
        }

        private void PackageBarcodeOnAdd(object sender, BarCodeAddValidateEventArgs eventArgs)
        {
            eventArgs.Success = true;

            if (ddlPackageType.SelectedValue == string.Empty)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = "Please select a Package Type";
            }
            else if (int.Parse(ddlPackageType.SelectedValue) != eventArgs.Package.PackageType.PackageTypeId)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = "The package with this barcode isn't the same type as the selected package type";
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

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex == 1)
            {
                var employeeUser = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();

                Employee employee = _employeeService.Retrieve(employeeUser.UserName); 

                StandardPackageType packageType = new StandardPackageType()
                {
                    PackageTypeId = int.Parse(ddlPackageType.SelectedValue)
                };

                List<string> barCodeList = ucPackageBarcode.GetBarcodes();

                Result result = _packageService.PerformAudit(employee, packageType, barCodeList);
                
                if (result.Success)
                {
                    litCompleteMessage.Text = "Audit completed successfully";
                }
                else
                {
                    litCompleteMessage.Text = string.Format("Audit Failed: {0}", result.ErrorMessage);
                }
            }
        }

        protected void Wizard1_Load(object sender, EventArgs e)
        {

        }

        protected void ddlPackageType_DataBound(object sender, EventArgs e)
        {
            ddlPackageType.Items.Insert(0, new ListItem("--- Package Type ---", String.Empty));
        }

        protected void ddlPackageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucPackageBarcode.Clear();
        }
    }
}