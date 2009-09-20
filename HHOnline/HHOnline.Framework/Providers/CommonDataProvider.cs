using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml;
using HHOnline.Common;

namespace HHOnline.Framework.Providers
{
    public abstract class CommonDataProvider
    {
        #region cntor
        private static readonly CommonDataProvider _instance = null;

        static CommonDataProvider()
        {
            _instance = HHContainer.Create().Resolve<CommonDataProvider>();
        }

        public static CommonDataProvider Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region SiteSettings

        public abstract SiteSettings LoadSiteSettings();

        public abstract void SaveSiteSettings(SiteSettings siteSettings);

        public static SiteSettings PopulateSiteSettingsFromIDataReader(IDataReader dr)
        {
            SiteSettings settings = null;

            try
            {
                settings = Serializer.ConvertToObject(DataRecordHelper.GetString(dr, "SettingXML", string.Empty)
                                                        , typeof(SiteSettings)) as SiteSettings;
                if (settings == null)
                    settings = new SiteSettings();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            settings.SettingsID = DataRecordHelper.GetInt32(dr, "SettingID");
            settings.SiteKey = DataRecordHelper.GetGuid(dr, "SettingKey", Guid.Empty);
            return settings;
        }
        #endregion

        #region Disallowed Names
        public abstract ArrayList GetDisallowedNames();
        public abstract int CreateUpdateDeleteDisallowedName(string name, string replacement, DataProviderAction action);
        #endregion

        #region Users
        public abstract User CreateUpdateUser(User user, DataProviderAction action, out CreateUserStatus status);

        public abstract CreateUserStatus CreateUpdateUser(User user, Company company);

        public abstract List<int> GetUsersInRole(int roleID);

        public abstract User GetUser(int userID, string userName, bool isOnline);

        public abstract List<User> GetUsers(UserQuery query);

        public abstract List<User> GetUsers(UserQuery query, out int totalRecord);

        public abstract LoginUserStatus ValidateUser(string userName, string password);

        public abstract bool DeleteUser(string userName);

        public abstract DataActionStatus DeleteUsers(string userIDList);

        public abstract bool SetPassword(string userName, string newPassword);

        public static User PopulateUserFromIDataReader(IDataReader dr)
        {
            User user = new User();
            user.UserID = DataRecordHelper.GetInt32(dr, "UserID");
            user.UserType = (UserType)DataRecordHelper.GetInt32(dr, "UserType");
            user.CompanyID = DataRecordHelper.GetInt32(dr, "CompanyID", 0);
            user.OrganizationID = DataRecordHelper.GetInt32(dr, "OrganizationID", 0);
            user.IsManager = DataRecordHelper.GetInt32(dr, "IsManager", 2);
            user.UserName = DataRecordHelper.GetString(dr, "LoginName");
            user.Password = DataRecordHelper.GetString(dr, "Password");
            user.DisplayName = DataRecordHelper.GetString(dr, "DisplayName", string.Empty);
            user.Email = DataRecordHelper.GetString(dr, "UserEmail", string.Empty);
            user.Mobile = DataRecordHelper.GetString(dr, "UserMobile", string.Empty);
            user.Phone = DataRecordHelper.GetString(dr, "UserPhone", string.Empty);
            user.Fax = DataRecordHelper.GetString(dr, "UserFax", string.Empty);
            user.Title = DataRecordHelper.GetString(dr, "UserDepartment", string.Empty);
            user.Title = DataRecordHelper.GetString(dr, "UserTitle", string.Empty);
            user.Remark = DataRecordHelper.GetString(dr, "UserMemo", string.Empty);
            user.AccountStatus = (AccountStatus)DataRecordHelper.GetInt32(dr, "UserStatus");
            user.PasswordQuestion = DataRecordHelper.GetString(dr, "PasswordQuestion", string.Empty);
            user.PasswordAnswer = DataRecordHelper.GetString(dr, "PasswordAnswer", string.Empty);
            user.LastLockonDate = DataRecordHelper.GetDateTime(dr, "LastLockTime", GlobalSettings.MinValue);
            user.LastActiveDate = DataRecordHelper.GetDateTime(dr, "LastActiveTime", GlobalSettings.MinValue);
            user.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime", GlobalSettings.MinValue);
            user.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser", 0);
            user.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime", GlobalSettings.MinValue);
            user.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser", 0);
            user.SetSerializerData(PopulateSerializerDataIDataRecord(dr));
            return user;
        }

        public static bool CheckPassword(string pwd, string storedPwd)
        {
            pwd = GlobalSettings.EncodePassword(pwd);
            if (GlobalSettings.IsNullOrEmpty(pwd))
                return false;
            return pwd.Equals(storedPwd, StringComparison.CurrentCulture);
        }

        public static LoginUserStatus CheckUserStatus(UserType userType, AccountStatus userStatus,
            int organizationID, int organizationStatus, int companyStatus, int companyType)
        {

            //企业客户登陆判断
            if (userType == UserType.CompanyUser)
            {

                switch ((CompanyStatus)companyStatus)
                {
                    case CompanyStatus.Authenticated:
                        switch (userStatus)
                        {
                            case AccountStatus.Authenticated:
                                return LoginUserStatus.Success;
                            case AccountStatus.ApprovalPending:
                                return LoginUserStatus.AccountPending;
                            case AccountStatus.Disapproved:
                                return LoginUserStatus.AccountDisapproved;
                            case AccountStatus.Lockon:
                                return LoginUserStatus.AccountBanned;
                            case AccountStatus.Deleted:
                                return LoginUserStatus.Deleted;
                            default:
                                return LoginUserStatus.Success;
                        }
                    case CompanyStatus.Deleted:
                        return LoginUserStatus.Deleted;
                    case CompanyStatus.Disapproved:
                        return LoginUserStatus.AccountDisapproved;
                    case CompanyStatus.ApprovalPending:
                        //客户类别
                        CompanyType type = (CompanyType)companyType;
                        //如是普通客户申请，则以普通客户登录
                        if (type != CompanyType.Ordinary && (type & CompanyType.Ordinary) == CompanyType.Ordinary)
                            return LoginUserStatus.Success;
                        else
                            return LoginUserStatus.AccountPending;
                    case CompanyStatus.Lockon:
                        return LoginUserStatus.AccountBanned;
                    default:
                        return LoginUserStatus.Success;
                }
            }
            else
            {
                //内部用户登录判断
                if (organizationID == 0)
                    return LoginUserStatus.Success;
                switch ((ComponentStatus)organizationStatus)
                {
                    case ComponentStatus.Deleted:
                        return LoginUserStatus.Deleted;
                    case ComponentStatus.Disabled:
                        return LoginUserStatus.AccountBanned;
                    case ComponentStatus.Enabled:
                        switch (userStatus)
                        {
                            case AccountStatus.Authenticated:
                                return LoginUserStatus.Success;
                            case AccountStatus.ApprovalPending:
                                return LoginUserStatus.AccountPending;
                            case AccountStatus.Disapproved:
                                return LoginUserStatus.AccountDisapproved;
                            case AccountStatus.Lockon:
                                return LoginUserStatus.AccountBanned;
                            case AccountStatus.Deleted:
                                return LoginUserStatus.Deleted;
                            default:
                                return LoginUserStatus.Success;
                        }
                    default:
                        return LoginUserStatus.Success;
                }
            }
        }
        #endregion

        #region Company
        /// <summary>
        /// 添加或删除公司信息
        /// </summary>
        /// <param name="company">Company</param>
        /// <param name="action">Create Or Update</param>
        /// <param name="status">CreateCompanyStatus</param>
        /// <returns>公司信息</returns>
        public abstract Company CreateUpdateCompany(Company company, DataProviderAction action, out CreateCompanyStatus status);

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="companyID">CompanyID</param>
        public abstract bool DeleteCompany(int companyID, string companyName);
        /// <summary>
        /// 获取所有公司信息
        /// </summary>
        /// <returns></returns>
        public abstract List<Company> GetCompanys(int comStatus, int comType, string comName);
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public abstract Company GetCompany(int companyID, string companyName);

        public static Company PopulateCompanyFromIDataReader(IDataReader dr)
        {
            Company company = new Company();
            company.CompanyID = DataRecordHelper.GetInt32(dr, "CompanyID");
            company.CompanyType = (CompanyType)DataRecordHelper.GetInt32(dr, "CompanyType", 1);
            company.CompanyRegion = DataRecordHelper.GetInt32(dr, "CompanyRegion", 0);
            company.CompanyName = DataRecordHelper.GetString(dr, "CompanyName", string.Empty);
            company.Address = DataRecordHelper.GetString(dr, "CompanyAddress", string.Empty);
            company.Zipcode = DataRecordHelper.GetString(dr, "CompanyZipcode");
            company.Phone = DataRecordHelper.GetString(dr, "CompanyPhone");
            company.Fax = DataRecordHelper.GetString(dr, "CompanyFax");
            company.Website = DataRecordHelper.GetString(dr, "CompanyWebsite");
            company.Orgcode = DataRecordHelper.GetString(dr, "CompanyOrgcode");
            company.Regcode = DataRecordHelper.GetString(dr, "CompanyRegcode");
            company.Remark = DataRecordHelper.GetString(dr, "CompanyMemo");
            company.CompanyStatus = (CompanyStatus)DataRecordHelper.GetInt32(dr, "CompanyStatus");
            company.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            company.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            company.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            company.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            company.SetSerializerData(PopulateSerializerDataIDataRecord(dr));
            return company;
        }
        #endregion

        #region CompanyQualification
        public abstract int CreateQualification(CompanyQualification qualification);

        public abstract CompanyQualification GetQualification(int qualificationID);

        public abstract List<CompanyQualification> GetQualificaionByCompanyID(int companyID);

        public abstract bool DeleteQualification(int qualificationID);

        public static CompanyQualification PopulateQualificationFromIDataReader(IDataReader dr)
        {
            CompanyQualification qualification = new CompanyQualification();
            qualification.QualificationID = DataRecordHelper.GetInt32(dr, "QualificationID");
            qualification.CompanyID = DataRecordHelper.GetInt32(dr, "CompanyID");
            qualification.QualificationFile = DataRecordHelper.GetString(dr, "QualificationFile");
            qualification.QualificationName = DataRecordHelper.GetString(dr, "QualificationName");
            qualification.QualificationDesc = DataRecordHelper.GetString(dr, "QualificationDesc");
            qualification.QualificationMemo = DataRecordHelper.GetString(dr, "QualificationMemo");
            qualification.QualificationStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "QualificationStatus");
            qualification.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            qualification.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            qualification.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            qualification.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return qualification;
        }
        #endregion

        #region Tag
        public abstract List<Tag> GetTags();

        public abstract List<Tag> GetTagsByArticle(int articleID);

        public abstract List<Tag> GetTagsByProduct(int productID);

        public abstract void UpdateTagArticle(int articleID, string tagList);

        public abstract void UpdateTagProduct(int productID, string tagList);

        public static Tag PopulateTagFromIDataReader(IDataReader dr)
        {
            Tag tagCloud = new Tag();
            tagCloud.Name = DataRecordHelper.GetString(dr, "TagName");
            tagCloud.ItemCount = DataRecordHelper.GetInt32(dr, "TagHitCount");
            tagCloud.DateCreated = DataRecordHelper.GetDateTime(dr, "CreateTime");
            tagCloud.LastModified = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            return tagCloud;
        }

        public static List<Tag> PopulateTagListFromIDataReader(IDataReader dr)
        {
            using (dr)
            {
                List<Tag> lstTags = new List<Tag>();
                while (dr.Read())
                {
                    lstTags.Add(PopulateTagFromIDataReader(dr));
                }
                return lstTags;
            }
        }
        #endregion

        #region Activity
        public abstract Dictionary<string, ActivityItem> GetActivityItems();

        public abstract List<UserActivity> GetUserActivities(UserActivityQuery query, out int totalRecord);

        public abstract UserActivity LogUserActivity(UserActivity message);

        public abstract void DeleteUserActivity(int messageID);

        public static UserActivity PopulateUserActivityFromIDataReader(IDataReader dr)
        {
            UserActivity userActivity = new UserActivity();
            userActivity.UserActivityID = DataRecordHelper.GetInt32(dr, "UserActivityID");
            userActivity.ActivityID = DataRecordHelper.GetInt32(dr, "ActivityID");
            userActivity.ActivityTitle = DataRecordHelper.GetString(dr, "ActivityTitle");
            userActivity.ActivityContent = DataRecordHelper.GetString(dr, "ActivityContent");
            userActivity.ActivityTime = DataRecordHelper.GetDateTime(dr, "ActivityTime");
            userActivity.ActivityUser = DataRecordHelper.GetInt32(dr, "ActivityUser");
            return userActivity;
        }


        public static ActivityItem PopulateActivityItemFromIDataReader(IDataReader dr)
        {
            ActivityItem codeActivity = new ActivityItem();
            codeActivity.ActivityID = DataRecordHelper.GetInt32(dr, "ActivityID");
            codeActivity.ActivityKey = DataRecordHelper.GetString(dr, "ActivityKey");
            codeActivity.ActivityName = DataRecordHelper.GetString(dr, "ActivityName");
            codeActivity.IsEnabled = DataRecordHelper.GetBoolean(dr, "IsEnabled", true);
            return codeActivity;
        }

        #endregion

        #region Organization
        public abstract int CreateUpdateOrganization(Organization organization, DataProviderAction action);

        public abstract bool DeleteOrganization(int organizationID);

        public abstract List<Organization> GetOrganizations();
        public abstract DataActionStatus DeleteOrganization(string organizationIDList);
        public static Organization PopulateOrganizationFromIDataReader(IDataReader dr)
        {
            Organization organization = new Organization();
            organization.OrganizationID = DataRecordHelper.GetInt32(dr, "OrganizationID");
            organization.OrganizationName = DataRecordHelper.GetString(dr, "OrganizationName");
            organization.OrganizationDesc = DataRecordHelper.GetString(dr, "OrganizationDesc");
            organization.OrganizationMemo = DataRecordHelper.GetString(dr, "OrganizationMemo");
            organization.ParentID = DataRecordHelper.GetInt32(dr, "ParentID");
            organization.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            organization.OrganizationStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "OrganizationStatus");
            organization.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            organization.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            organization.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            organization.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            organization.SetSerializerData(PopulateSerializerDataIDataRecord(dr));
            return organization;
        }
        #endregion

        #region Area
        public abstract List<Area> GetAreas();

        public static Area PopulateAreaFromIDataReader(IDataReader dr)
        {
            Area codeRegion = new Area();
            codeRegion.RegionID = DataRecordHelper.GetInt32(dr, "RegionID");
            codeRegion.RegionType = (AreaType)DataRecordHelper.GetInt32(dr, "RegionType");
            codeRegion.RegionCode = DataRecordHelper.GetString(dr, "RegionCode");
            codeRegion.DistrictCode = DataRecordHelper.GetString(dr, "DistrictCode");
            codeRegion.RegionName = DataRecordHelper.GetString(dr, "RegionName");
            codeRegion.RegionDesc = DataRecordHelper.GetString(dr, "RegionDesc");
            codeRegion.RegionMemo = DataRecordHelper.GetString(dr, "RegionMemo");
            codeRegion.RegionStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "RegionStatus");
            codeRegion.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            codeRegion.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            return codeRegion;
        }
        #endregion

        #region User Favorite
        public abstract Favorite GetFavorite(int favoriteID);

        public abstract Favorite CreateUpdateFavorite(Favorite favorite, DataProviderAction action);

        public abstract bool DeleteFavorite(int favoriteID);

        public abstract List<Favorite> GetFavorites(FavoriteQuery query, out int totalRecord);

        public static Favorite PopulateFavoriteFromIDataReader(IDataReader dr)
        {
            Favorite favorite = new Favorite();
            favorite.FavoriteID = DataRecordHelper.GetInt32(dr, "FavoriteID");
            favorite.UserID = DataRecordHelper.GetInt32(dr, "UserID");
            favorite.FavoriteType = (FavoriteType)DataRecordHelper.GetInt32(dr, "FavoriteType");
            favorite.FavoriteTitle = DataRecordHelper.GetString(dr, "FavoriteTitle");
            favorite.RelatedID = DataRecordHelper.GetInt32(dr, "RelatedID");
            favorite.FavoriteUrl = DataRecordHelper.GetString(dr, "FavoriteUrl");
            favorite.FavoriteLevel = DataRecordHelper.GetInt32(dr, "FavoriteLevel");
            favorite.FavoriteMemo = DataRecordHelper.GetString(dr, "FavoriteMemo");
            favorite.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            favorite.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            return favorite;
        }
        #endregion

        #region User Grade
        public abstract UserGrade CreateUpdateUserGrade(UserGrade userGrade, DataProviderAction action, out DataActionStatus status);

        public abstract UserGrade GetUserGrade(int userGradeID);

        public abstract List<UserGrade> GetUserGradeByUserID(int userID);

        public abstract bool DeleteUserGrade(int userGradeID);

        public abstract bool ClearUserGrade(int userID);

        public static UserGrade PopulateUserGradeFromIDataReader(IDataReader dr)
        {
            UserGrade userGrade = new UserGrade();
            userGrade.GradeID = DataRecordHelper.GetInt32(dr, "GradeID");
            userGrade.UserID = DataRecordHelper.GetInt32(dr, "UserID");
            userGrade.GradeLevel = (UserLevel)DataRecordHelper.GetInt32(dr, "GradeLevel");
            userGrade.GradeLimit = DataRecordHelper.GetString(dr, "GradeLimit");
            userGrade.GradeName = DataRecordHelper.GetString(dr, "GradeName");
            userGrade.GradeDesc = DataRecordHelper.GetString(dr, "GradeDesc");
            userGrade.GradeMemo = DataRecordHelper.GetString(dr, "GradeMemo");
            userGrade.GradeStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "GradeStatus");
            userGrade.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            userGrade.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            userGrade.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            userGrade.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return userGrade;
        }
        #endregion

        #region Customer Grade
        public abstract CustomerGrade CreateUpdateCustomerGrade(CustomerGrade customerGrade, DataProviderAction action, out DataActionStatus status);

        public abstract CustomerGrade GetCustomerGrade(int customerGradeID);

        public abstract List<CustomerGrade> GetCustomerGradeByCompanyID(int companyID);

        public abstract DataActionStatus DeleteCustomerGrade(int customerGradeID);

        public abstract DataActionStatus ClearCustomerGrade(int companyID);

        public static CustomerGrade PopulateCustomerGradeFromIDataReader(IDataReader dr)
        {
            CustomerGrade customerGrade = new CustomerGrade();
            customerGrade.GradeID = DataRecordHelper.GetInt32(dr, "GradeID");
            customerGrade.CompanyID = DataRecordHelper.GetInt32(dr, "CustomerID");
            customerGrade.GradeLevel = (UserLevel)DataRecordHelper.GetInt32(dr, "GradeLevel");
            customerGrade.GradeLimit = DataRecordHelper.GetString(dr, "GradeLimit");
            customerGrade.GradeName = DataRecordHelper.GetString(dr, "GradeName");
            customerGrade.GradeDesc = DataRecordHelper.GetString(dr, "GradeDesc");
            customerGrade.GradeMemo = DataRecordHelper.GetString(dr, "GradeMemo");
            customerGrade.GradeStatus = (ComponentStatus)DataRecordHelper.GetInt32(dr, "GradeStatus");
            customerGrade.CreateTime = DataRecordHelper.GetDateTime(dr, "CreateTime");
            customerGrade.CreateUser = DataRecordHelper.GetInt32(dr, "CreateUser");
            customerGrade.UpdateTime = DataRecordHelper.GetDateTime(dr, "UpdateTime");
            customerGrade.UpdateUser = DataRecordHelper.GetInt32(dr, "UpdateUser");
            return customerGrade;
        }
        #endregion

        #region Common
        public static SerializerData PopulateSerializerDataIDataRecord(IDataRecord record)
        {
            SerializerData data = new SerializerData();
            data.Keys = DataRecordHelper.GetString(record, "PropertyNames", string.Empty);
            data.Values = DataRecordHelper.GetString(record, "PropertyValues", string.Empty);
            return data;
        }
        #endregion

        #region TemporaryAttachment
        public abstract void CreateUpdateTemporaryAttachment(TemporaryAttachment attachment, DataProviderAction action);

        public abstract void DeleteTemporaryAttachment(Guid attachmentID);

        public abstract TemporaryAttachment GetTemporaryAttachment(Guid attachmentID);

        public abstract List<TemporaryAttachment> GetTemporaryAttachments(int userID, AttachmentType attachmentType);

        public static TemporaryAttachment PopulateTemporaryAttachmentFromIDataReader(IDataReader dr)
        {
            TemporaryAttachment temporaryAttachment = new TemporaryAttachment();
            temporaryAttachment.AttachmentID = DataRecordHelper.GetGuid(dr, "AttachmentID");
            temporaryAttachment.UserID = DataRecordHelper.GetInt32(dr, "UserID");
            temporaryAttachment.AttachmentType = (AttachmentType)DataRecordHelper.GetInt32(dr, "AttachmentType");
            temporaryAttachment.FileName = DataRecordHelper.GetString(dr, "FileName");
            temporaryAttachment.FriendlyFileName = DataRecordHelper.GetString(dr, "FriendlyFileName");
            temporaryAttachment.ContentType = DataRecordHelper.GetString(dr, "ContentType");
            temporaryAttachment.ContentSize = DataRecordHelper.GetInt64(dr, "ContentSize");
            temporaryAttachment.Height = DataRecordHelper.GetInt32(dr, "Height");
            temporaryAttachment.Width = DataRecordHelper.GetInt32(dr, "Width");
            temporaryAttachment.DisplayOrder = DataRecordHelper.GetInt32(dr, "DisplayOrder");
            return temporaryAttachment;
        }


        #endregion

        #region WordSearch
        public abstract void InsertWordSearch(string keyword);

        public abstract void StatisticWordSearch();

        public abstract List<string> GetWordSuggest(string startLetter);

        public abstract List<string> GetHotWordSearch(int topCount);
        #endregion

        #region -Pending-
        public abstract List<Pending> PendingsLoad();
        public abstract bool PendingAdd(Pending pending);
        public abstract bool PendingUpdate(Pending pending);
        #endregion
    }
}
