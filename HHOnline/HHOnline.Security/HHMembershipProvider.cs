using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using HHOnline.Framework;
using System.Collections.Specialized;
using System.Globalization;
using System.Configuration.Provider;
using System.Text.RegularExpressions;

namespace HHOnline.Security
{
    public class HHMembershipProvider : MembershipProvider
    {
        public HHMembershipProvider()
        {
        }

        private string applicationName = "HHMembershipProvider";
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        #region -Properties-
        string hashAlgorithmType = string.Empty;
        private bool enablePasswordReset;
        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        private bool enablePasswordRetrieval;
        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        private int maxInvalidPasswordAttempts;
        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        private int minRequiredNonAlphanumericCharacters;
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        private int minRequiredPasswordLength;
        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        private int passwordAttemptWindow;
        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        private MembershipPasswordFormat passwordFormat;
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        private string passwordStrengthRegularExpression;
        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        private bool requiresQuestionAndAnswer;
        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        private bool requiresUniqueEmail;
        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }
        #endregion

        #region -Private-
        private static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                if (checkForNull)
                    return false;

                return true;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) || (maxSize > 0 && param.Length > maxSize) || (checkForCommas && param.IndexOf(",") != -1))
                return false;

            return true;
        }
        private static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string sValue = config[valueName];
            if (sValue == null)
                return defaultValue;

            if (sValue.ToLower() == "true")
                return true;

            if (sValue.ToLower() == "false")
                return false;

            throw new Exception("The value must be a boolean for property '" + valueName + "'");
        }

        private static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string sValue = config[valueName];

            if (sValue == null)
            {
                return defaultValue;
            }

            int iValue;
            try
            {
                iValue = Convert.ToInt32(sValue, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException e)
            {
                if (zeroAllowed)
                    throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);

                throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);
            }

            if (zeroAllowed && iValue < 0)
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");

            if (!zeroAllowed && iValue <= 0)
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");

            if (maxValueAllowed > 0 && iValue > maxValueAllowed)
                throw new Exception("The value is too big for '" + valueName + "' must be smaller than " + maxValueAllowed.ToString(CultureInfo.InvariantCulture));

            return iValue;
        }
        #endregion

        #region -Unoverride-
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "HHMembershipProvider";

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "HHOnline Membership Provider");
            }

            // Initialize base class
            base.Initialize(name, config);

            // Get values from config
            enablePasswordRetrieval = GetBooleanValue(config, "enablePasswordRetrieval", false);
            enablePasswordReset = GetBooleanValue(config, "enablePasswordReset", true);
            requiresQuestionAndAnswer = GetBooleanValue(config, "requiresQuestionAndAnswer", true);
            requiresUniqueEmail = GetBooleanValue(config, "requiresUniqueEmail", true);
            maxInvalidPasswordAttempts = GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            passwordAttemptWindow = GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            minRequiredPasswordLength = GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);

            // Get hash algorhithm
            hashAlgorithmType = config["hashAlgorithmType"];
            if (String.IsNullOrEmpty(hashAlgorithmType))
                hashAlgorithmType = "SHA1";

            // Get password validation Regular Expression
            passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (passwordStrengthRegularExpression != null)
            {
                passwordStrengthRegularExpression = passwordStrengthRegularExpression.Trim();
                if (passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(passwordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
                passwordStrengthRegularExpression = string.Empty;


            if (applicationName.Length > 255)
                throw new ProviderException("Provider application name is too long, max length is 255.");


            // Get password format
            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";
            switch (strTemp)
            {
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }

            if (passwordFormat == MembershipPasswordFormat.Hashed && enablePasswordRetrieval)
                throw new ProviderException("Provider cannot retrieve hashed password");

            // Clean up config
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("description");
            config.Remove("minRequiredPasswordLength");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("hashAlgorithmType");

            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider unrecognized attribute: " + attribUnrecognized);
            }
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (!ValidateParameter(ref password, true, true, false, 0))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (passwordStrengthRegularExpression != null)
            {
                if (!Regex.IsMatch(password, passwordStrengthRegularExpression))
                {
                    status = MembershipCreateStatus.InvalidPassword;
                    return null;
                }
            }

            if (password.Length < minRequiredPasswordLength)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string pass = GlobalSettings.EncodePassword(password, (PasswordFormat)(int)passwordFormat, hashAlgorithmType);
            if (pass.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (!ValidateParameter(ref username, true, true, true, 255))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            if (!ValidateParameter(ref email, RequiresUniqueEmail, RequiresUniqueEmail, false, 128))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }

            if (!ValidateParameter(ref passwordQuestion, RequiresQuestionAndAnswer, true, false, 255))
            {
                status = MembershipCreateStatus.InvalidQuestion;
                return null;
            }

            if (!ValidateParameter(ref passwordAnswer, RequiresQuestionAndAnswer, true, false, 128))
            {
                status = MembershipCreateStatus.InvalidAnswer;
                return null;
            }

            status = MembershipCreateStatus.UserRejected;
            try
            {
                DateTime dt = DateTime.Now;
                status = MembershipCreateStatus.Success;
                int uid = 0;
                status = Users.QuickCreate(username, password, email, passwordQuestion, passwordAnswer, isApproved, out uid);
                return new MembershipUser(this.Name, username, uid, email, passwordQuestion, null, isApproved, false, dt, dt, dt, dt, DateTime.MinValue);
            }
            catch (Exception)
            {
                if (status == MembershipCreateStatus.Success)
                    status = MembershipCreateStatus.ProviderError;
                return null;
            }
        }
        public override bool ValidateUser(string username, string password)
        {
            if (!ValidateParameter(ref username, true, true, false, 255))
                return false;
            if (!ValidateParameter(ref password, true, true, false, 128))
                return false;

            return Users.ValidateUser(username, password) == LoginUserStatus.Success;
        }
    }
}
