﻿using ENetCare.BusinessService;
using ENetCare.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace ENetCare.Web.Membership
{
    public class EmployeeMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private EmployeeService _employeeService;

        public EmployeeMembershipProvider()
        {
            IEmployeeRepository repository = new EmployeeRepository(ConfigurationManager.ConnectionStrings["ENetCare"].ConnectionString);
            _employeeService = new EmployeeService(repository);            
        }

        private MachineKeySection _machineKey; //Used when determining encryption key values.
        private MembershipPasswordFormat _passwordFormat;

        /// <summary>
        /// Converts a hexadecimal string to a byte array. 
        /// </summary>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// Encode password
        /// </summary>
        private string EncodePassword(string password)
        {
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    return password;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(_machineKey.ValidationKey);
                    return Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                default:
                    throw new ProviderException("Unsupported password format");
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            _passwordFormat = (MembershipPasswordFormat)Enum.Parse(typeof(MembershipPasswordFormat), config["passwordFormat"], true);

            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;
        }

        /// <summary>
        /// Validate user
        /// </summary>
        public override bool ValidateUser(string username, string password)
        {
            var employee = _employeeService.Retrieve(username);
            //return true;
             return employee == null || employee.Password != EncodePassword(password) ? false : true;
        }

        /// <summary>
        /// Change password
        /// </summary>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {            
            var result = _employeeService.ChangePassword(username, EncodePassword(oldPassword), EncodePassword(newPassword), EncodePassword(newPassword));
            
            return result.Success;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string username, string password, string name, int organisationId,
            out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Get password
        /// </summary>
        public override string GetPassword(string username, string answer)
        {
            var employee = _employeeService.Retrieve(username);
            
            return employee == null ? null : employee.Password;
        }

        /// <summary>
        /// Get membership user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="userIsOnline">Update last activity date</param>
        /// <returns>Membership user</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var employee = _employeeService.Retrieve(username);           

            EmployeeMembershipUser user = new EmployeeMembershipUser(
                this.Name, 
                employee.UserName,
                -1,
                string.Empty,
                null,
                employee.FullName,
                true, false,
                DateTime.Today.AddDays(-1),
                DateTime.Today.AddDays(-1),
                DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue,
                employee.EmployeeType,
                employee.Location.CentreId,
                employee.Location.Name);

            return user;
        }

        /// <summary>
        /// Get username by email
        /// </summary>
        public override string GetUserNameByEmail(string username)
        {
            // Username is email.
            return username;
        }

        /// <summary>
        /// Get membership user by id
        /// </summary>
        /// <param name="providerUserKey">User id</param>
        /// <param name="userIsOnline">Update last activity date</param>
        /// <returns>Membership user</returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override string ResetPassword(string username, string answer)
        {
            return "aaa1233";
        }


        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 5; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 1; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

    }
}