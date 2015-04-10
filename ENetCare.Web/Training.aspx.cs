using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class Training : System.Web.UI.Page
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

                if (barcodes[i] == "012365423")
                    throw new ArgumentException("Invalid barcode");
            }

            litBarcodeList.Text = barcodeListText.ToString();
        }

    }
}