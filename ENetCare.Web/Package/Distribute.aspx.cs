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
    /// <summary>
    /// This class implements all the methods that is required to distrubte a package
    /// This class is also the code behind for the Distribute Page
    /// </summary>
    public partial class Distribute : System.Web.UI.Page
    {
        private PackageService _packageService;
        private Employee employee;
        protected void Page_Load(object sender, EventArgs e)
        {
            IPackageRepository packageRepository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _packageService = new PackageService(packageRepository);

            //Use the custom Barcode event which is triggered when a new barcode is to be added
            ucPackageBarcode.AddValidate += PackageBarcodeOnAdd;

            if (!Page.IsPostBack)
            {
                IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
                var employeeService = new EmployeeService(repository);

                EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
                DistributionCentre centre = employeeService.GetDistributionCentre(user.DistributionCentreId);

                //Save centre and username in the viewstate
                ViewState["DistributionCentre"] = centre;
                ViewState["EmployeeUsername"] = user.UserName;
            }
        }

        /// <summary>
        /// This method is invoked up clicking the save button on the distribute page
        /// This method sets up everything, it also process the items in the Distribute table
        /// then one-by-one each item is processed and updated in the database
        /// If there is any error, it will display an error message on the page
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                pnlErrorMessage.Visible = true;
                litErrorMessage.Text = "There are errors";
                return;
            }

            StringBuilder successMessage = new StringBuilder(10);

            DistributionCentre centre = (DistributionCentre)ViewState["DistributionCentre"];
            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            var employeeService = new EmployeeService(repository);

            //Retrieve the viewstate for Employee Username
            string employeeUsername = (string)ViewState["EmployeeUsername"];

            employee = employeeService.Retrieve(employeeUsername);

            DateTime expirationDate = DateTime.Now;

            //Go through each item in the list of packages that are to be distributed, and process them
            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                string packageTypeId = ucPackageBarcode.GetPackageTypeId(barcodes[i]);

                Package package = _packageService.Retrieve(barcodes[i]);

                StandardPackageType spt = _packageService.GetStandardPackageType(package.PackageType.PackageTypeId);

                //Update the database and change status to distrubted for selected packages
                var result = _packageService.Distribute(barcodes[i], centre, employee, expirationDate, spt, package.PackageId);
                if (!result.Success)
                {
                    //if there was error updating the package then show error message
                    var err = new CustomValidator();
                    err.ValidationGroup = "destinationDetails";
                    err.IsValid = false;
                    err.ErrorMessage = string.Format("{0} - {1}", barcodes[i], result.ErrorMessage);
                    Page.Validators.Add(err);

                    pnlErrorMessage.Visible = true;
                    litErrorMessage.Text = "There are errors";
                }
                else
                {
                    //Else if the item was successfully updated, append a success message to a placeholder
                    if (successMessage.Length == 0)
                    {
                        successMessage.Append("The following barcodes were distributed");
                    }

                    successMessage.AppendFormat(", {0}", barcodes[i]);
                }
            }

            //If the packages were successfully Distributed then show a success message
            if (successMessage.Length > 0)
            {
                pnlMessage.Visible = true;
                litMessage.Text = successMessage.ToString();
            }

            //Disable Save button, and hide the barcode control
            ucPackageBarcode.Visible = false;
            btnSave.Enabled = false;
            btnClose.Enabled = true;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        /// <summary>
        /// In this method all the validations are carried out, they check for package availability, the correct centre
        /// Permission to dsitribute and if any of them fail the validation an error message is shown to the user and the
        /// item is never added to the table.
        /// </summary>
        private void PackageBarcodeOnAdd(object sender, BarCodeAddValidateEventArgs eventArgs)
        {
            eventArgs.Success = true;

            DistributionCentre centre = (DistributionCentre)ViewState["DistributionCentre"];

            if (eventArgs.Package == null)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.BarCodeNotFound;
            }
            else if (eventArgs.Package.CurrentStatus == PackageStatus.InTransit)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageInTransit;
            }
            else if (eventArgs.Package.CurrentLocation.CentreId != centre.CentreId)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageElsewhere;
            }
            else if (eventArgs.Package.CurrentStatus == PackageStatus.Lost)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageIsLost;
            }
            else if (eventArgs.Package.CurrentStatus == PackageStatus.Distributed)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageAlreadyDistributed;
            }
            else if (eventArgs.Package.CurrentStatus == PackageStatus.Discarded)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageAlreadyDiscarded;
            }
            else if (eventArgs.Package.ExpirationDate <= DateTime.Now)
            {
                eventArgs.Success = false;
                eventArgs.ErrorMessage = PackageResult.PackageHasExpired;
            }
        }
    }
}