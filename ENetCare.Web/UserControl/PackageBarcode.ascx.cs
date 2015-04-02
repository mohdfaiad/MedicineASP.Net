using ENetCare.BusinessService;
using ENetCare.Repository.Data;
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
    // A delegate type for hooking up extra Add Barcode validations.
    public delegate void AddValidateEventHandler(object sender, BarCodeAddValidateEventArgs e);

    public partial class PackageBarcode : System.Web.UI.UserControl
    {
        public event AddValidateEventHandler AddValidate;
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
                Literal litPackageId = gvr.FindControl("litPackageId") as Literal;

                dr["Barcode"] = litBarcode.Text;
                dr["PackageType"] = litPackageType.Text;
                dr["ExpirationDate"] = DateTime.Parse(litExpirationDate.Text);
                dr["PackageId"] = int.Parse(litPackageId.Text);

                dt.Rows.Add(dr); // add grid values in to row and add row to the blank table

                if (litBarcode.Text == txtBarcode.Text)
                    duplicateFound = true;
            }

            IPackageRepository repository = new PackageRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            PackageService packageService = new PackageService(repository);

            BarCodeAddValidateEventArgs eventArgs = new BarCodeAddValidateEventArgs()
            {
                Success = true
            };

            Package package = packageService.Retrieve(txtBarcode.Text);
            if (package == null)
                barcodeNotFound = true;
            else            
            {
                eventArgs.Package = package;
                OnAddValidate(eventArgs);
            }

            if (duplicateFound)
            {
                litMessage.Text = "Barcode already selected";
            }
            else if (barcodeNotFound)
            {
                litMessage.Text = "Barcode not found";
            }
            else if (!eventArgs.Success)
            {
                litMessage.Text = eventArgs.ErrorMessage;
            }
            else
            {
                litMessage.Text = string.Empty;

                dr = dt.NewRow(); // add last empty row

                dr["Barcode"] = txtBarcode.Text;
                dr["PackageType"] = package.PackageType.Description;
                dr["ExpirationDate"] = package.ExpirationDate;
                dr["PackageId"] = package.PackageId;

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
                Literal litPackageId = gvr.FindControl("litPackageId") as Literal;

                dr = dt.NewRow();

                dr["Barcode"] = litBarcode.Text;
                dr["PackageType"] = litPackageType.Text;
                dr["ExpirationDate"] = DateTime.Parse(litExpirationDate.Text);
                dr["PackageId"] = int.Parse(litPackageId.Text);

                dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
            }

            grd.DataSource = dt; // bind new datatable to grid
            grd.DataBind();
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnAddValidate(BarCodeAddValidateEventArgs e)
        {
            if (AddValidate != null)
                AddValidate(this, e);
        }

        public DataTable GetTableWithNoData() // returns only structure if the select columns
        {
            DataTable table = new DataTable();
            table.Columns.Add("Barcode", typeof(string));
            table.Columns.Add("PackageType", typeof(string));
            table.Columns.Add("ExpirationDate", typeof(DateTime));
            table.Columns.Add("PackageId", typeof(int));
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

        public void Clear()
        {
            grd.DataSource = GetTableWithNoData(); // bind empty datatable to grid
            grd.DataBind();
        }

        public string GetPackageTypeId(string barcode)
        {
            string packageTypeId = null;

            foreach (GridViewRow gvr in grd.Rows)
            {
                Literal litPackageType = gvr.FindControl("litPackageType") as Literal;
                if (litPackageType != null)
                    packageTypeId = litPackageType.Text;
            }

            return packageTypeId;
        }
    }
}