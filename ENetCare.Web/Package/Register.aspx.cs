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
    public partial class Register : System.Web.UI.Page
    {
        private PackageService _packageService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);
            
            ImageBarcode.Visible = false;
            btnNext.Enabled = false;
            
            if (!Page.IsPostBack)
            {
                IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
                var employeeService = new EmployeeService(repository);

                var centres = employeeService.GetAllDistributionCentres();

                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "CentreId";
                ddlLocation.DataSource = centres;
                ddlLocation.DataBind();

                EmployeeMembershipUser user = (EmployeeMembershipUser) System.Web.Security.Membership.GetUser();

                ddlLocation.SelectedValue = user.DistributionCentreId.ToString();

                var packageTypes = _packageService.GetAllStandardPackageTypes();

                ddlPackageType.DataTextField = "Description";
                ddlPackageType.DataValueField = "PackageTypeId";
                ddlPackageType.DataSource = packageTypes;
                ddlPackageType.DataBind();

                ViewState["PackageTypes"] = packageTypes;
                ViewState["DistributionCentres"] = centres;
            }
        }

        protected void ddlPackageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var packageTypes = (List<StandardPackageType>) ViewState["PackageTypes"];

            int selectedPackageTypeId = int.Parse(ddlPackageType.SelectedValue);

            StandardPackageType selectedPackageType =
                packageTypes.FirstOrDefault(p => p.PackageTypeId == selectedPackageTypeId);

            DateTime expirationDate = _packageService.CalculateExpirationDate(selectedPackageType, DateTime.Today);

            txtExpirationDate.Text = string.Format("{0:dd/MM/yyyy}", expirationDate);            
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
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

            var packageTypes = (List<StandardPackageType>) ViewState["PackageTypes"];
            int selectedPackageTypeId = int.Parse(ddlPackageType.SelectedValue);

            StandardPackageType selectedPackageType =
                packageTypes.FirstOrDefault(p => p.PackageTypeId == selectedPackageTypeId);

            var centres = (List<DistributionCentre>) ViewState["DistributionCentres"];

            int selectedCentreId = int.Parse(ddlLocation.SelectedValue);

            DistributionCentre selectedCentre =
                centres.FirstOrDefault(c => c.CentreId == selectedCentreId);

            DateTime expirationDate = DateTime.Parse(txtExpirationDate.Text);
            string barcode;

            Result result = _packageService.Register(selectedPackageType, selectedCentre, expirationDate, out barcode);
            if (!result.Success)
            {
                var err = new CustomValidator();
                err.ValidationGroup = "userDetails"; 
                err.IsValid = false;
                err.ErrorMessage = result.ErrorMessage;
                Page.Validators.Add(err);

                pnlErrorMessage.Visible = true;
                litErrorMessage.Text = "There are errors";
                return;
            }

            pnlMessage.Visible = true;
            litMessage.Text = "Successfully saved";

            litBarcode.Text = barcode;

            string strImageURL = "~/Handler/GenerateBarcodeImage.ashx?d=" + barcode;
            this.ImageBarcode.ImageUrl = strImageURL;
            this.ImageBarcode.Width = 400;
            this.ImageBarcode.Height = 150;
            this.ImageBarcode.Visible = true;

            ddlPackageType.Enabled = false;
            txtExpirationDate.Enabled = false;
            ddlLocation.Enabled = false;
            btnSave.Enabled = false;
            btnNext.Enabled = true;
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Package/Register.aspx");
        }

        protected void ddlPackageType_DataBound(object sender, EventArgs e)
        {
            ddlPackageType.Items.Insert(0, new ListItem("--- Package Type ---", String.Empty));
        }
    }
}