using ENetCare.BusinessService;
using ENetCare.Repository.Repository;
using ENetCare.Web.Membership;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web.Report
{
    public partial class Stocktaking : System.Web.UI.Page
    {




        private decimal _grandTotalValue = 0M;
        private ReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IReportRepository repository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(repository);
            EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();
            grd.DataSource = _reportService.GetStocktaking(user.DistributionCentreId);
            grd.DataBind();
            //Debug.WriteLine("grid binded to reportService list");
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal totalValue = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalValue").ToString());

                _grandTotalValue += totalValue;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Literal litGrandTotalValue = (Literal)e.Row.FindControl("litGrandTotalValue");
                litGrandTotalValue.Text = _grandTotalValue.ToString("C2");

                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[0].Text = "Grand Total";
                for (int i = 3; i >= 1; i--)
                    e.Row.Cells.RemoveAt(i);
            }
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
}