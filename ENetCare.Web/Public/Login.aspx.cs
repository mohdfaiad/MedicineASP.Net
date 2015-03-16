using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ENetCare.Web.Public
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ValidateUser(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;

            if (System.Web.Security.Membership.ValidateUser(username, password))
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
        }

    }
}