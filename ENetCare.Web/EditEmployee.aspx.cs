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
    public partial class EditEmployee : System.Web.UI.Page
    {
        private EmployeeService _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _employeeService = new EmployeeService(repository);                        
            
            if (!Page.IsPostBack)
            {
                var centres = _employeeService.GetAllDistributionCentres();

                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "CentreId";
                ddlLocation.DataSource = centres;
                ddlLocation.DataBind();

                EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
                var employee = _employeeService.Retrieve(user.UserName);
                if (employee != null)
                {
                    litUserName.Text = employee.UserName;
                    txtFullName.Text = employee.FullName;
                    txtEmailAddress.Text = employee.EmailAddress;
                    rblEmployeeType.SelectedValue = employee.EmployeeType.ToString();
                    ddlLocation.SelectedValue = employee.Location.CentreId.ToString();
                }
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

            DistributionCentre centre = new DistributionCentre()
            {
                CentreId = int.Parse(ddlLocation.SelectedValue)
            };

            EmployeeType employeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), rblEmployeeType.SelectedValue);

            Result result = _employeeService.Update(litUserName.Text, txtFullName.Text, txtEmailAddress.Text, centre, employeeType);

            if (result.Success)
            {
                pnlMessage.Visible = true;
                litMessage.Text = "Successfully saved";
            }
            else
            {                
                var err = new CustomValidator();
                err.ValidationGroup = "userDetails";
                err.IsValid = false;
                err.ErrorMessage = result.ErrorMessage;
                Page.Validators.Add(err);

                pnlErrorMessage.Visible = true;
                litErrorMessage.Text = "There are errors";                              
            }
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
}