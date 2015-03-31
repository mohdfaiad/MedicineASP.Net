using ENetCare.BusinessService;
using ENetCare.Repository.Data;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
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
    public partial class Send : System.Web.UI.Page
    {
        private PackageService _packageService;
        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            if (!Page.IsPostBack)
            {
                IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
                var employeeService = new EmployeeService(repository);
                

                EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
                DistributionCentre centre = employeeService.GetDistributionCentre(user.DistributionCentreId);              

                ddlDestination.DataTextField = "Choose Destination";
                var centres = employeeService.GetAllDistributionCentres();

                ddlDestination.DataTextField = "Name";
                ddlDestination.DataValueField = "CentreId";
                ddlDestination.DataSource = centres;
                ddlDestination.DataBind();
                ddlDestination.SelectedValue = user.DistributionCentreId.ToString();

                ViewState["DistributionCentre"] = centre;
                ViewState["EmployeeUsername"] = user.UserName;

            }
        }

        protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                pnlErrorMessage.Visible = true;
                litErrorMessage.Text = "There are errors";
                return;
            }

            string employeeUsername = (string)ViewState["EmployeeUsername"];
            DateTime date = DateTime.Now;
            DistributionCentre senderCentre = (DistributionCentre)ViewState["DistributionCentre"];

            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                string packageTypeId = ucPackageBarcode.GetPackageTypeId(barcodes[i]);

                Package package = _packageService.Retrieve(barcodes[i]);

                _packageService.Send(barcodes[i], senderCentre, date );
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Send.aspx");
        }
    }
}