using System;
using System.Collections.Generic;
using ENetCare.BusinessService;
using ENetCare.Repository.Repository;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENetCare.Repository.ViewData;
using System.Diagnostics;
using ENetCare.Web.Membership;


namespace ENetCare.Web.Report
{
    public partial class Stocktaking : System.Web.UI.Page
    {

        private decimal _grandTotalValue = 0M;
        private ReportService _reportService;
        private Dictionary<int, StocktakingPackage> sPackagesById = new Dictionary<int, StocktakingPackage>();

        protected void Page_Load(object sender, EventArgs e)
        {
            IReportRepository repository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(repository);

            EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();

            grd.DataSource = _reportService.GetStocktaking(user.DistributionCentreId);
            grd.DataBind();
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal totalValue = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CostPerPackage").ToString());
                _grandTotalValue += totalValue;
                int daysLeft = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DaysLeft").ToString());
                if (daysLeft < 8) e.Row.ForeColor = System.Drawing.Color.Orange;
                if (daysLeft < 1) e.Row.ForeColor = System.Drawing.Color.Red;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Literal litGrandTotalValue = (Literal)e.Row.FindControl("litGrandTotalValue");
                litGrandTotalValue.Text = _grandTotalValue.ToString("C2");

                e.Row.Cells[0].ColumnSpan = 5;
                e.Row.Cells[0].Text = "Grand Total";
                for (int i = 4; i >= 1; i--)
                    e.Row.Cells.RemoveAt(i);
            }

        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

    }
}