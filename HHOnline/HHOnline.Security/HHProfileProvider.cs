using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Configuration;
using HHOnline.Framework;

namespace HHOnline.Security
{
    public class HHProfileProvider:ProfileProvider
    {
        private static string applicationName = "HHProfileProvider";
        private const string accountInfo = "AccountInfo";
        private const string shoppingCart = "ShopingCart";

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
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "HHOnline Custom Profile Provider");
            }

            if (string.IsNullOrEmpty(name))
                name = "HHOnlineProfileProvider";

            if (config["applicationName"] != null && !string.IsNullOrEmpty(config["applicationName"].Trim()))
                applicationName = config["applicationName"];	 

            base.Initialize(name, config);
        }
        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            string[] users = new string[0];
            GetInactiveUsers(authenticationOption, userInactiveSinceDate).ToArray().CopyTo(users, 0);
            return DeleteProfiles(users);            
        }
        public override int DeleteProfiles(string[] usernames)
        {
            int deleteCount = 0;
            foreach (string u in usernames)
                if (DeleteUser(u))
                    deleteCount++;
            return deleteCount;
        }
        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            int deleteCount = 0;
            foreach (ProfileInfo pi in profiles)
                if (DeleteUser(pi.UserName))
                    deleteCount++;
            return deleteCount;
        }
        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProfiles(authenticationOption, userInactiveSinceDate, usernameToMatch, pageIndex, pageSize, out totalRecords);
        }
        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProfiles(authenticationOption, null, usernameToMatch, pageIndex, pageSize, out totalRecords);
        }
        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProfiles(authenticationOption, userInactiveSinceDate, null, pageIndex, pageSize, out totalRecords);
        }
        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetProfiles(authenticationOption, null, null, pageIndex, pageSize, out totalRecords);
        }
        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            int inactiveNumber = 0;
            ProfileInfoCollection profiles = GetProfiles(authenticationOption, userInactiveSinceDate, null, 0, 0, out inactiveNumber);
            return inactiveNumber;
        }
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            string userName = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];

            SettingsPropertyValueCollection spvc = new SettingsPropertyValueCollection();
            SettingsPropertyValue spv=null;
            foreach (SettingsProperty sp in collection)
            {
                spv = new SettingsPropertyValue(sp);
                switch (spv.Property.Name)
                {
                    case shoppingCart:
                        //spv.PropertyValue = //user
                        break;
                    case accountInfo:
                        if (isAuthenticated)
                        {
                            spv.PropertyValue = GetAccountInfo(userName);
                        }
                        break;
                }
                spvc.Add(spv);
            }
            return spvc;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            string userName = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];
            int uniqueId = GetUniqueId(userName);

            foreach (SettingsPropertyValue spv in collection)
            {
                if (spv.PropertyValue != null)
                {
                    switch (spv.Property.Name)
                    { 
                        case shoppingCart:
                            break;
                        case accountInfo:
                            if (isAuthenticated)
                            {
                                SetAccountInfo(uniqueId,(User)spv.PropertyValue);
                            }
                            break;
                    }
                }
            }
        }

        #region -Private Method-
        int GetUniqueId(string userName)
        {
            return Users.GetUniqueId(userName);
        }

        User GetAccountInfo(string userName)
        {
            return Users.GetUser(userName);
        }
        void SetAccountInfo(int uniqueId, User user)
        {
            user.UserID = uniqueId;
            Users.UpdateUser(user);
        }
        bool DeleteUser(string userName)
        {
            return Users.DeleteUser(userName);
        }
        List<string> GetInactiveUsers(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            return Users.GetInactiveUsers(authenticationOption,userInactiveSinceDate);
        }
        ProfileInfoCollection GetProfiles(ProfileAuthenticationOption authenticationOption, object userInactiveSinceDate, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            ProfileInfoCollection profiles = new ProfileInfoCollection();

            totalRecords = 0;
            DateTime dt = new DateTime(1900, 1, 1);
            if (userInactiveSinceDate != null)
                dt = (DateTime)userInactiveSinceDate;

            if (pageSize == 0)
            {
                totalRecords = Users.GetInactiveUsers(authenticationOption, dt).Count;
                return profiles;
            }

            int counter = 0;
            int startIndex = pageSize * (pageIndex - 1);
            int endIndex = startIndex + pageSize - 1;


            DateTime ld = new DateTime(1900, 1, 1);
            foreach (User profile in Users.GetProfiles(authenticationOption, dt, usernameToMatch, out totalRecords))
            {
                if (counter >= startIndex)
                {
                    ProfileInfo p = new ProfileInfo(profile.UserName, (profile.AccountStatus == AccountStatus.Anonymous ? true : false), profile.LastActiveDate, profile.UpdateTime, 0);
                    profiles.Add(p);
                }

                if (counter >= endIndex)
                {
                    break;
                }

                counter++;
            }

            return profiles;
        }
        #endregion
    }
}
