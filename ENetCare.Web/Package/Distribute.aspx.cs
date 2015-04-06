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
    public partial class Distribute : System.Web.UI.Page
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

                ViewState["DistributionCentre"] = centre;
                ViewState["EmployeeUsername"] = user.UserName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DistributionCentre centre = (DistributionCentre)ViewState["DistributionCentre"];

            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            var employeeService = new EmployeeService(repository);

            string employeeUsername = (string)ViewState["EmployeeUsername"];

            Employee employee = employeeService.Retrieve(employeeUsername);

            DateTime expirationDate = DateTime.Now;            

            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                string packageTypeId = ucPackageBarcode.GetPackageTypeId(barcodes[i]);

                Package package = _packageService.Retrieve(barcodes[i]);

                StandardPackageType spt = _packageService.GetStandardPackageType(package.PackageType.PackageTypeId);
                
                _packageService.Distribute(barcodes[i], centre, employee, expirationDate, spt, package.PackageId);
            }

            Page.ClientScript.RegisterStartupScript(
                Page.GetType(),  
                "MessageBox",
                "<script language='javascript'>alert('Packages have been successfully Distributed');</script>"
            );

            Response.Redirect("Distribute.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Distribute.aspx");
        }
    }
}