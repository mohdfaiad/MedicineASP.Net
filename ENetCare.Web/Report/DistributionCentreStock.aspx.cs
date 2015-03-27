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
    public partial class DistributionCentreStock : System.Web.UI.Page
    {
        private decimal _subTotalValue = 0M;
        private decimal _grandTotalValue = 0M;
        private int _centreId = 0;
        private int _rowIndex = 1;
        private ReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IReportRepository repository = new ReportRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _reportService = new ReportService(repository);                        
            
            grd.DataSource = _reportService.GetDistributionCentreStock();
            grd.DataBind();
        }

        protected void grd_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool newRow = false;
            if ((_centreId > 0) && (DataBinder.Eval(e.Row.DataItem, "DistributionCentreId") != null))
            {
                if (_centreId != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DistributionCentreId").ToString()))
                    newRow = true;
            }
            if ((_centreId > 0) && (DataBinder.Eval(e.Row.DataItem, "DistributionCentreId") == null))
            {
                newRow = true;
                _rowIndex = 0;
            }
            if (newRow)
            {
                GridView grd2 = (GridView)sender;
                GridViewRow newTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                newTotalRow.Font.Bold = true;
                newTotalRow.BackColor = System.Drawing.Color.Gray;
                newTotalRow.ForeColor = System.Drawing.Color.White;
                TableCell headerCell = new TableCell();
                headerCell.Text = "Sub Total";
                headerCell.HorizontalAlign = HorizontalAlign.Left;
                headerCell.ColumnSpan = 6;
                newTotalRow.Cells.Add(headerCell);
                headerCell = new TableCell();
                headerCell.HorizontalAlign = HorizontalAlign.Right;
                headerCell.Text = _subTotalValue.ToString("C2");
                newTotalRow.Cells.Add(headerCell);
                grd.Controls[0].Controls.AddAt(e.Row.RowIndex + _rowIndex, newTotalRow);
                _rowIndex++;
                _subTotalValue = 0;
            }

        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _centreId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DistributionCentreId").ToString());
                decimal totalValue = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalValue").ToString());
                _subTotalValue += totalValue;
                _grandTotalValue += totalValue;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Literal litGrandTotalValue = (Literal)e.Row.FindControl("litGrandTotalValue");
                litGrandTotalValue.Text = _grandTotalValue.ToString("C2");

                e.Row.Cells[0].ColumnSpan = 6;
                e.Row.Cells[0].Text = "Grand Total";
                for (int i = 5; i >= 1; i--)
                    e.Row.Cells.RemoveAt(i);
            }
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
}