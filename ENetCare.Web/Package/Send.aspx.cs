using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
using ENetCare.Web.UserControl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class Send : System.Web.UI.Page
    {
        private PackageService _packageService;
        private EmployeeService _employeeService;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _employeeService = new EmployeeService(repository);

            if (!Page.IsPostBack)
            {
                var centres = _employeeService.GetAllDistributionCentres();

                ddlDestination.DataTextField = "Name";
                ddlDestination.DataValueField = "CentreId";
                ddlDestination.DataSource = centres;
                ddlDestination.DataBind();
                
                SetSendDateTextBox(DateTime.Today);
                ViewState["DistributionCentre"] = centres;
            }
            else if (!string.IsNullOrEmpty(Request.Form[txtSendDate.UniqueID]))
            {
                DateTime receiveDate = DateTime.Parse(Request.Form[txtSendDate.UniqueID]);
                SetSendDateTextBox(receiveDate);
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

            //string employeeUsername = (string)ViewState["EmployeeUsername"];
            DateTime sendDate = DateTime.Parse(Request.Form[txtSendDate.UniqueID]);

            int selectedCentreId = int.Parse(ddlDestination.SelectedValue);
            DistributionCentre _receiverCentre = _employeeService.GetDistributionCentre(selectedCentreId);

            EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
            DistributionCentre _senderCentre = _employeeService.GetDistributionCentre(user.DistributionCentreId);
            
            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                string packageTypeId = ucPackageBarcode.GetPackageTypeId(barcodes[i]);

                Package package = _packageService.Retrieve(barcodes[i]);

                // Remove Current Lcoation and Set to InTransit the Package Status
                var result = _packageService.Send(package, _senderCentre, _receiverCentre, sendDate);
                if (!result.Success)
                {
                    var err = new CustomValidator();
                    err.ValidationGroup = "sendDetails";
                    err.IsValid = false;
                    err.ErrorMessage = result.ErrorMessage;
                    Page.Validators.Add(err);

                    pnlErrorMessage.Visible = true;
                    litErrorMessage.Text = "There are errors";
                    return;
                }
                else
                {
                    pnlSuccessMsg.Visible = true;
                    LitSuccessMsg.Text = "Package(s) Send Successfully! Click OK to Continue";
                }
            }

            txtSendDate.Enabled = false;
            ddlDestination.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Text = "OK";
            btnCancel.Enabled = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Send.aspx");
        }
        private void PackageBarcodeOnAdd(object sender, BarCodeAddValidateEventArgs eventArgs)
        {
            eventArgs.Success = true;

            DistributionCentre centre = (DistributionCentre)ViewState["DistributionCentre"];

            if (eventArgs.Package == null)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.BarCodeNotFound;
            }
            if (eventArgs.Package.CurrentLocation.CentreId != centre.CentreId)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageElsewhere;
            }
            if (eventArgs.Package.CurrentStatus == PackageStatus.Distributed)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageAlreadyDistributed;
            }
            if (eventArgs.Package.CurrentStatus == PackageStatus.InTransit)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageInTransit;
            }
            if (eventArgs.Package.CurrentStatus == PackageStatus.Discarded)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageAlreadyDiscarded;
            }
        }

        private void SetSendDateTextBox(DateTime sendDate)
        {
            txtSendDate.Text = string.Format("{0:dd/MM/yyyy}", sendDate);
        }
    }
}