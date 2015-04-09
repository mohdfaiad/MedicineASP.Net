using ENetCare.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ENetCare.Web.Membership
{
    public class EmployeeMembershipUser : MembershipUser
    {
        private readonly EmployeeType _employeeType;
        private readonly int _distributionCentreId;
        private readonly string _distributionCentreName;

        public EmployeeMembershipUser(string providerName, string name, object providerUserKey, string email,
            string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate,
            DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate,
            DateTime lastLockoutDate, EmployeeType employeeType, int distributionCentreId, string distributionCentreName)
            : base(
                providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut,
                creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            _employeeType = employeeType;
            _distributionCentreId = distributionCentreId;
            _distributionCentreName = distributionCentreName;
        }

        public EmployeeType EmployeeType
        {
            get { return _employeeType; }
        }

        public int DistributionCentreId
        {
            get { return _distributionCentreId; }
        }

        public string DistributionCentreName
        {
            get { return _distributionCentreName; }
        }
    }
}