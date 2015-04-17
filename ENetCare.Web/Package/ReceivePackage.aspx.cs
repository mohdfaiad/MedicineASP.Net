using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
using ENetCare.Web.UserControl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class ReceivePackage : System.Web.UI.Page
    {
        private PackageService _packageService;
        private EmployeeService _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _employeeService = new EmployeeService(repository);

            ucPackageBarcode.AddValidate += PackageBarcodeOnAdd;

            if (!Page.IsPostBack)
            {
                SetReceiveDateTextBox(DateTime.Today);
            }
            else if (!string.IsNullOrEmpty(Request.Form[txtReceiveDate.UniqueID]))
            {
                DateTime receiveDate = DateTime.Parse(Request.Form[txtReceiveDate.UniqueID]);
                SetReceiveDateTextBox(receiveDate);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                pnlErrorMessage.Visible = true;
                litErrorMessage.Text = "There are errors";
                return;
            }

            StringBuilder successMessage = new StringBuilder(10);

            DateTime receiveDate = DateTime.Parse(Request.Form[txtReceiveDate.UniqueID]);            

            EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
            var destinationCentre = _employeeService.GetDistributionCentre(user.DistributionCentreId);

            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                Result result = _packageService.Receive(barcodes[i], destinationCentre, receiveDate);
                if (!result.Success)
                {
                    var err = new CustomValidator();
                    err.ValidationGroup = "receiveDetails";
                    err.IsValid = false;
                    err.ErrorMessage = string.Format("{0} - {1}", barcodes[i], result.ErrorMessage);
                    Page.Validators.Add(err);

                    pnlErrorMessage.Visible = true;
                    litErrorMessage.Text = "There are errors";                    
                }
                else
                {
                    if (successMessage.Length == 0)
                    {
                        successMessage.Append("The following barcodes were received");
                    }

                    successMessage.AppendFormat(", {0}", barcodes[i]);
                }
            }

            if (successMessage.Length > 0)
            {
                pnlMessage.Visible = true;
                litMessage.Text = successMessage.ToString();
            }

            ucPackageBarcode.Visible = false;
            txtReceiveDate.Enabled = false;
            btnSave.Enabled = false;
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        private void PackageBarcodeOnAdd(object sender, BarCodeAddValidateEventArgs eventArgs)
        {
            eventArgs.Success = true;

            EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();            

            if (eventArgs.Package.CurrentStatus == PackageStatus.InStock && eventArgs.Package.CurrentLocation.CentreId == user.DistributionCentreId)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = "Package is already in stock at " + eventArgs.Package.CurrentLocation.Name;
            }
        }

        private void SetReceiveDateTextBox(DateTime receiveDate)
        {
            txtReceiveDate.Text = string.Format("{0:dd/MM/yyyy}", receiveDate);
        }
    }
}