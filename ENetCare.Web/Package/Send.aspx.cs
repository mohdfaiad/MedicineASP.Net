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

            if (!Page.IsPostBack)
            {
                IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
                var employeeService = new EmployeeService(repository);

                EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();

                ddlDestination.DataTextField = "Choose Destination";
                var centres = employeeService.GetAllDistributionCentres();

                ddlDestination.DataTextField = "Name";
                ddlDestination.DataValueField = "CentreId";
                ddlDestination.DataSource = centres;
                ddlDestination.DataBind();

                ddlDestination.SelectedValue = user.DistributionCentreId.ToString();
            }
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!Page.IsValid)
        //    {
        //        pnlErrorMessage.Visible = true;
        //        //pnlErrorMessage.text = "There are errors";
        //        return;
        //    }
        //}
    }
}