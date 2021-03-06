﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENetCare.Web.Membership;
using ENetCare.Repository.Data;

namespace ENetCare.Web.MasterPages
{
    public partial class General : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                EmployeeMembershipUser user = (EmployeeMembershipUser)System.Web.Security.Membership.GetUser();

                litEmployeeUsername.Text = user.UserName;
                litEmployeeFullName.Text = user.Comment;
                litEmployeeType.Text = user.EmployeeType.ToString();
                litLocationCentreName.Text = user.DistributionCentreName;

                switch (user.EmployeeType)
                {
                    case EmployeeType.Doctor:
                        SiteMapDataSource1.SiteMapProvider = "DoctorSiteMapProvider";
                        break;

                    case EmployeeType.Agent:
                        SiteMapDataSource1.SiteMapProvider = "AgencySiteMapProvider";
                        break;

                    case EmployeeType.Manager:
                        SiteMapDataSource1.SiteMapProvider = "ManagerSiteMapProvider";
                        break;
                }
            }
            
        }
    }
}