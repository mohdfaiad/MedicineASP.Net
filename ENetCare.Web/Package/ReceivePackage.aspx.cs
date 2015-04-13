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

            if (!Page.IsPostBack)
            {
                SetReceiveDateTextBox(DateTime.Today);
            }
            else
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
                    err.ErrorMessage = result.ErrorMessage;
                    Page.Validators.Add(err);

                    pnlErrorMessage.Visible = true;
                    litErrorMessage.Text = "There are errors";
                    return;
                }
            }
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        private void SetReceiveDateTextBox(DateTime receiveDate)
        {
            txtReceiveDate.Text = string.Format("{0:dd/MM/yyyy}", receiveDate);
        }
    }
}