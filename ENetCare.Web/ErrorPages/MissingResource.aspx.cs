using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENetPlay.ErrorPages
{
    public partial class MissingResource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // page responds with status code 200 instead of 404. The response has been rewritten after redriecting to this error page
            // more details here http://connect.microsoft.com/VisualStudio/feedback/details/507171/
            Response.StatusCode = 404;
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
}