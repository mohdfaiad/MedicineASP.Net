using ENetCare.BusinessService;
using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web.Report
{
    /// <summary>
    /// Doctor Activity: This report shows the quantity, package types and values
    /// of packages distributed by a given doctor. It also has a grand total.
    /// </summary>
    public partial class DoctorActivity : System.Web.UI.Page
    {
        private decimal _grandTotalValue = 0M;
        private ReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IReportRepository repository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(repository);

            grd.DataSource = _reportService.GetDoctorActivity();
            grd.DataBind();
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal totalValue = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalPackageValue").ToString());

                _grandTotalValue += totalValue;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Literal litGrandTotalPackageValue = (Literal)e.Row.FindControl("litGrandTotalPackageValue");
                litGrandTotalPackageValue.Text = _grandTotalValue.ToString("C2");

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