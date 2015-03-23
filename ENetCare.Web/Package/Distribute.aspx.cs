using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class Distribute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> barcodes = ucPackageBarcode.GetBarcodes();
            StringBuilder barcodeListText = new StringBuilder();
            for (int i = 0; i < barcodes.Count(); i++)
            {
                if (i > 0)
                    barcodeListText.Append(", ");

                barcodeListText.Append(barcodes[i]);
            }

            Page.ClientScript.RegisterStartupScript(
                Page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + barcodeListText.ToString() + "');</script>"
            );
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}