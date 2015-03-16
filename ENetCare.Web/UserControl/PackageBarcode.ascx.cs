using ENetCare.BusinessService;
using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web.UserControl
{
    public partial class PackageBarcode : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grd.DataSource = GetTableWithNoData(); // get first initial data
                grd.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;
            bool duplicateFound = false;
            bool barcodeNotFound = false;

            foreach (GridViewRow gvr in grd.Rows)
            {
                dr = dt.NewRow();

                Literal litBarcode = gvr.FindControl("litBarcode") as Literal;
                Literal litPackageType = gvr.FindControl("litPackageType") as Literal;
                Literal litExpirationDate = gvr.FindControl("litExpirationDate") as Literal;

                dr["Barcode"] = litBarcode.Text;
                dr["PackageType"] = litPackageType.Text;
                dr["ExpirationDate"] = DateTime.Parse(litExpirationDate.Text);

                dt.Rows.Add(dr); // add grid values in to row and add row to the blank table

                if (litBarcode.Text == txtBarcode.Text)
                    duplicateFound = true;
            }

            IPackageRepository repository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            PackageService packageService = new PackageService(repository);

            if (txtBarcode.Text == "1234567890")
                barcodeNotFound = true;

            if (duplicateFound)
            {
                litMessage.Text = "Barcode already selected";
            }
            else if (barcodeNotFound)
            {
                litMessage.Text = "Barcode not found";
            }
            else
            {
                litMessage.Text = string.Empty;

                dr = dt.NewRow(); // add last empty row

                dr["Barcode"] = txtBarcode.Text;
                dr["PackageType"] = "100 tablets Panadol";
                dr["ExpirationDate"] = DateTime.Today.AddMonths(3);

                dt.Rows.Add(dr);
            }

            grd.DataSource = dt; // bind new datatable to grid
            grd.DataBind();
        }

        protected void grd_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = GetTableWithNoData(); // get select column header only records not required
            DataRow dr;

            foreach (GridViewRow gvr in grd.Rows)
            {
                if (gvr.RowIndex == e.RowIndex)
                    continue;
                
                Literal litBarcode = gvr.FindControl("litBarcode") as Literal;
                Literal litPackageType = gvr.FindControl("litPackageType") as Literal;
                Literal litExpirationDate = gvr.FindControl("litExpirationDate") as Literal;

                dr = dt.NewRow();

                dr["Barcode"] = litBarcode.Text;
                dr["PackageType"] = litPackageType.Text;
                dr["ExpirationDate"] = DateTime.Parse(litExpirationDate.Text);

                dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
            }

            grd.DataSource = dt; // bind new datatable to grid
            grd.DataBind();
        }

        public DataTable GetTableWithNoData() // returns only structure if the select columns
        {
            DataTable table = new DataTable();
            table.Columns.Add("Barcode", typeof(string));
            table.Columns.Add("PackageType", typeof(string));
            table.Columns.Add("ExpirationDate", typeof(DateTime));
            return table;
        }

        public List<string> GetBarcodes()
        {
            var barcodes = new List<string>();
            foreach (GridViewRow gvr in grd.Rows)
            {
                Literal litBarcode = gvr.FindControl("litBarcode") as Literal;
                if (litBarcode != null)
                    barcodes.Add(litBarcode.Text);
            }
            return barcodes;
        }
    }
}