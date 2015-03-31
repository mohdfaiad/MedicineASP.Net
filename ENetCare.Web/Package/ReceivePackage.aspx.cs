using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetCare.Web
{
    public partial class ReceivePackage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        protected void cancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
}