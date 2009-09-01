using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using HHOnline.Common;
using HHOnline.Framework;
using HHOnline.Framework.Providers;
using System.Diagnostics;

namespace HHOnline.Data
{
    public class SqlCommonDataProvider : CommonDataProvider
    {
        #region SiteSettings
        public override SiteSettings LoadSiteSettings()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_SiteSettings_Get"))
            {
                SiteSettings setting = null;
                if (dr.Read())
                {
                    setting = PopulateSiteSettingsFromIDataReader(dr);
                }
                if (setting == null)
                    setting = new SiteSettings();
                return setting;
            }
        }

        public override void SaveSiteSettings(SiteSettings siteSettings)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@SettingsXML",DbType.String,Serializer.ConvertToString(siteSettings)),
                new ELParameter("@SettingsID",DbType.Int32,siteSettings.SettingsID)};
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_SiteSettings_Save", elParameters);
        }
        #endregion

        #region DisallowedName
        public override int CreateUpdateDeleteDisallowedName(string name, string replacement, DataProviderAction action)
        {
            ELParameter[] elParameters = null;
            if (action == DataProviderAction.Delete)
            {
                elParameters = new ELParameter[]{
                new ELParameter("@DeleteName",DbType.Boolean,1),
                new ELParameter("@Name",DbType.String,name),
                new ELParameter("@Replacement", DbType.String,DBNull.Value)};
            }
            else if (action == DataProviderAction.Update)
            {
                elParameters = new ELParameter[]{
                new ELParameter("@DeleteName",DbType.Boolean,0),
                new ELParameter("@Name",DbType.String,name),
                new ELParameter("@Replacement", DbType.String,replacement)};
            }
            else
            {
                elParameters = new ELParameter[]{
                new ELParameter("@DeleteName",DbType.Boolean,0),
                new ELParameter("@Name",DbType.String,name),
                new ELParameter("@Replacement", DbType.String,DBNull.Value)};
            }
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DisallowedName_CreateUpdateDelete", elParameters);
            return 1;
        }

        public override ArrayList GetDisallowedNames()
        {
            ArrayList names = null;
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_DisallowedNames_Get"))
            {
                names = new ArrayList();
                while (dr.Read())
                    names.Add(DataRecordHelper.GetString(dr, "DisallowedName"));
            }
            return names;
        }
        #endregion

        #region User
        public override CreateUserStatus CreateUpdateUser(User user, Company company)
        {
            ELParameter paramUserID = new ELParameter("@UserID", DbType.Int32, 4, ParameterDirection.Output);
            ELParameter paramCompanyID = new ELParameter("@CompanyID", DbType.Int32, 4, ParameterDirection.Output);
            ELParameter paramReturn = new ELParameter("@ReturnValue", DbType.Int32, 4, ParameterDirection.ReturnValue);
            ELParameter[] elParameters = new ELParameter[]{
                paramUserID,
                paramCompanyID,
                paramReturn,
                new ELParameter("@LoginName", DbType.String, user.UserName),
                new ELParameter("@Password", DbType.String, GlobalSettings.EncodePassword(user.Password)),
                new ELParameter("@UserStatus", DbType.Int32, user.AccountStatus),
                new ELParameter("@DisplayName", DbType.String, user.DisplayName),
                new ELParameter("@UserEmail", DbType.String, user.Email),
                new ELParameter("@UserMobile", DbType.String, user.Mobile),
                new ELParameter("@UserPhone", DbType.String, user.Phone),
                new ELParameter("@UserFax", DbType.String, user.Fax),
                new ELParameter("@UserTitle", DbType.String, user.Title),
                new ELParameter("@PasswordQuestion", DbType.String, user.PasswordQuestion),
                new ELParameter("@PasswordAnswer", DbType.String, GlobalSettings.EncodePassword(user.PasswordAnswer)),
                new ELParameter("@UserMemo", DbType.String, user.Remark),
                new ELParameter("@UserType", DbType.Int32, user.UserType),
                new ELParameter("@OrganizationID",DbType.Int32,DataHelper.IntOrNull(user.OrganizationID)),
                new ELParameter("@IsManager",DbType.Int32,user.IsManager),
                new ELParameter("@CompanyType",DbType.Int32,company.CompanyType),
                new ELParameter("@CompanyRegion",DbType.Int32,DataHelper.IntOrNull(company.CompanyRegion)),
                new ELParameter("@CompanyName",DbType.String,company.CompanyName),
                new ELParameter("@CompanyAddress",DbType.String,company.Address),
                new ELParameter("@CompanyZipcode",DbType.String,company.Zipcode),
                new ELParameter("@CompanyPhone",DbType.String,company.Phone),
                new ELParameter("@CompanyFax",DbType.String,company.Fax),
                new ELParameter("@CompanyWebsite",DbType.String,company.Website),
                new ELParameter("@CompanyOrgcode",DbType.String,company.Orgcode),
                new ELParameter("@CompanyRegcode",DbType.String,company.Regcode),
                new ELParameter("@CompanyMemo",DbType.String,company.Remark),
                new ELParameter("@CompanyStatus",DbType.Int32,company.CompanyStatus),
                new ELParameter("@UserPropertyNames",DbType.String, user.GetSerializerData().Keys),
                new ELParameter("@UserPropertyValues",DbType.String,user.GetSerializerData().Values),
                new ELParameter("@CompanyPropertyNames",DbType.String,company.GetSerializerData().Keys),
                new ELParameter("@CompanyPropertyValues",DbType.String,company.GetSerializerData().Values),
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_UserCompany_Create", elParameters);
            CreateUserStatus status = (CreateUserStatus)Convert.ToInt32(paramReturn.Value);
            if (status == CreateUserStatus.Success)
            {
                user.UserID = Convert.ToInt32(paramUserID.Value);
                company.CompanyID = Convert.ToInt32(paramCompanyID.Value);
            }
            return status;
        }

        public override User CreateUpdateUser(User user, DataProviderAction action, out CreateUserStatus status)
        {
            List<ELParameter> elParameters = new List<ELParameter>();
            elParameters.Add(new ELParameter("@LoginName", DbType.String, user.UserName));
            elParameters.Add(new ELParameter("@Password", DbType.String, GlobalSettings.EncodePassword(user.Password)));
            elParameters.Add(new ELParameter("@UserStatus", DbType.Int32, user.AccountStatus));
            elParameters.Add(new ELParameter("@DisplayName", DbType.String, user.DisplayName));
            elParameters.Add(new ELParameter("@UserEmail", DbType.String, user.Email));
            elParameters.Add(new ELParameter("@UserMobile", DbType.String, user.Mobile));
            elParameters.Add(new ELParameter("@UserPhone", DbType.String, user.Phone));
            elParameters.Add(new ELParameter("@UserFax", DbType.String, user.Fax));
            elParameters.Add(new ELParameter("@UserTitle", DbType.String, user.Title));
            elParameters.Add(new ELParameter("@PasswordQuestion", DbType.String, user.PasswordQuestion));
            elParameters.Add(new ELParameter("@PasswordAnswer", DbType.String, GlobalSettings.EncodePassword(user.PasswordAnswer)));
            elParameters.Add(new ELParameter("@UserMemo", DbType.String, user.Remark));
            elParameters.Add(new ELParameter("@UserType", DbType.Int32, user.UserType));
            elParameters.Add(new ELParameter("@CompanyID", DbType.Int32, DataHelper.IntOrNull(user.CompanyID)));
            elParameters.Add(new ELParameter("@OrganizationID", DbType.Int32, DataHelper.IntOrNull(user.OrganizationID)));
            elParameters.Add(new ELParameter("@IsManager", DbType.Int32, user.IsManager));
            elParameters.Add(new ELParameter("@Operator", DbType.Int32, GlobalSettings.GetCurrentUser().UserID));
            SerializerData data = user.GetSerializerData();
            elParameters.Add(new ELParameter("@PropertyNames", DbType.String, data.Keys));
            elParameters.Add(new ELParameter("@PropertyValues", DbType.String, data.Values));
            elParameters.Add(new ELParameter("@Action", DbType.Int32, action));
            ELParameter paramUserID = null;
            if (action == DataProviderAction.Create)
            {
                paramUserID = new ELParameter("@UserID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramUserID = new ELParameter("@UserID", DbType.Int32, user.UserID);
            }
            elParameters.Add(paramUserID);
            status = (CreateUserStatus)Convert.ToInt32(
               DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_User_CreateUpdate", elParameters.ToArray()));

            if (status == CreateUserStatus.Success && action == DataProviderAction.Create)
                user.UserID = Convert.ToInt32(paramUserID.Value);
            return user;
        }

        public override User GetUser(int userID, string userName, bool isOnline)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@UserID",DbType.Int32,userID),
                new ELParameter("@UserName",DbType.String,userName),
                new ELParameter("@isOnline",DbType.Boolean,isOnline)};
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_User_Get", elParameters))
            {
                User user = null;
                if (dr.Read())
                {
                    user = PopulateUserFromIDataReader(dr);
                }
                return user;
            }

        }

        public override List<int> GetUsersInRole(int roleID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@RoleID",DbType.Int32,roleID),
                new ELParameter("@RoleName",DbType.String,null)
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Permission_FindUsersInRole", elParameters))
            {
                List<int> userIDList = new List<int>();
                while (dr.Read())
                {
                    userIDList.Add(DataRecordHelper.GetInt32(dr, "userID"));
                }
                return userIDList;
            }
        }

        public override List<User> GetUsers(UserQuery query)
        {
            int totalRecords;
            return GetUsers(query, out totalRecords);
        }

        public override List<User> GetUsers(UserQuery query, out int totalRecord)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuildMemberQuery(query)),
                new ELParameter("@SqlPopulateCount",DbType.String,QueryGenerator.BuildMemberQuery(query,true))
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Users_Get", elParameters))
            {
                List<User> userList = new List<User>();
                while (dr.Read())
                    userList.Add(PopulateUserFromIDataReader(dr));
                dr.NextResult();
                dr.Read();
                totalRecord = DataRecordHelper.GetInt32(dr, 0);
                return userList;
            }
        }

        public override bool DeleteUser(string userName)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@UserID",DbType.Int32,0),
                new ELParameter("@UserName",DbType.String,userName)};
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_User_Delete", elParameters)) == 1;
        }

        public override LoginUserStatus ValidateUser(string userName, string password)
        {
            if (TypeHelper.IsNullOrEmpty(userName) || TypeHelper.IsNullOrEmpty(password))
                return LoginUserStatus.InvalidCredentials;

            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@LoginName",DbType.String,userName)
            };

            string storedPwd = string.Empty;

            UserType userType = UserType.InnerUser;
            AccountStatus userStatus = AccountStatus.Authenticated;
            int companyStatus = 0;
            int companyType = 0;
            int organizationStatus = 0;
            int organizationID = 0;

            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Membership_GetPassword", elParameters))
            {
                if (dr.Read())
                {
                    storedPwd = DataRecordHelper.GetString(dr, "Password");
                    if (!CheckPassword(password, storedPwd))
                        return LoginUserStatus.InvalidCredentials;
                    userType = (UserType)DataRecordHelper.GetInt32(dr, "UserType");
                    userStatus = (AccountStatus)DataRecordHelper.GetInt32(dr, "UserStatus");
                    companyStatus = DataRecordHelper.GetInt32(dr, "CompanyStatus", 0);
                    organizationStatus = DataRecordHelper.GetInt32(dr, "OrganizationStatus", 0);
                    organizationID = DataRecordHelper.GetInt32(dr, "OrganizationId", 0);
                    companyType = DataRecordHelper.GetInt32(dr, "CompanyType", 0);
                    return CheckUserStatus(userType, userStatus, organizationID, organizationStatus, companyStatus, companyType);
                }
                else
                {
                    return LoginUserStatus.InvalidCredentials;
                }
            }
        }

        public override bool SetPassword(string userName, string newPassword)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@UserName",DbType.String,userName),
                new ELParameter("@NewPassword",DbType.String,newPassword),
            };
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure,
                "sp_Membership_SetPassword", elParameters)) == 1;
        }

        public override DataActionStatus DeleteUsers(string userIDList)
        {
            ELParameter param = new ELParameter("@UserIDList", DbType.String, userIDList);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Users_Delete", param));
        }
        #endregion

        #region Company
        public override Company CreateUpdateCompany(Company company, DataProviderAction action, out CreateCompanyStatus status)
        {
            List<ELParameter> elParameters = new List<ELParameter>();
            elParameters.Add(new ELParameter("@Action", DbType.Int32, action));
            elParameters.Add(new ELParameter("@CompanyType", DbType.Int32, company.CompanyType));
            elParameters.Add(new ELParameter("@CompanyRegion", DbType.Int32, DataHelper.IntOrNull(company.CompanyRegion)));
            elParameters.Add(new ELParameter("@CompanyName", DbType.String, company.CompanyName));
            elParameters.Add(new ELParameter("@CompanyAddress", DbType.String, company.Address));
            elParameters.Add(new ELParameter("@CompanyZipcode", DbType.String, company.Zipcode));
            elParameters.Add(new ELParameter("@CompanyPhone", DbType.String, company.Phone));
            elParameters.Add(new ELParameter("@CompanyFax", DbType.String, company.Fax));
            elParameters.Add(new ELParameter("@CompanyWebsite", DbType.String, company.Website));
            elParameters.Add(new ELParameter("@CompanyOrgcode", DbType.String, company.Orgcode));
            elParameters.Add(new ELParameter("@CompanyRegcode", DbType.String, company.Regcode));
            elParameters.Add(new ELParameter("@CompanyStatus", DbType.Int32, company.CompanyStatus));
            elParameters.Add(new ELParameter("@Operator", DbType.Int32, GlobalSettings.GetCurrentUser().UserID));
            SerializerData data = company.GetSerializerData();
            elParameters.Add(new ELParameter("@PropertyNames", DbType.String, data.Keys));
            elParameters.Add(new ELParameter("@PropertyValues", DbType.String, data.Values));
            ELParameter paramCompanyID = null;
            if (action == DataProviderAction.Create)
            {
                paramCompanyID = new ELParameter("@CompanyID", DbType.Int32, 4, ParameterDirection.Output);
            }
            else
            {
                paramCompanyID = new ELParameter("@CompanyID", DbType.Int32, company.CompanyID);
            }

            elParameters.Add(paramCompanyID);
            status = (CreateCompanyStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Company_CreateUpdate", elParameters.ToArray()));

            if (status == CreateCompanyStatus.Success && action == DataProviderAction.Create)
                company.CompanyID = Convert.ToInt32(paramCompanyID.Value);

            return company;
        }

        public override bool DeleteCompany(int companyID, string companyName)
        {
            ELParameter[] elParameters = new ELParameter[]{
                  new ELParameter("@CompanyID",DbType.Int32,companyID),
                  new ELParameter("@CompanyName",DbType.String,companyName)};
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Company_Delete", elParameters)) == 1;
        }

        public override Company GetCompany(int companyID, string companyName)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@CompanyID",DbType.Int32,companyID),
                new ELParameter("@CompanyName",DbType.String,companyName)};
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Company_Get", elParameters))
            {
                Company company = null;
                if (dr.Read())
                {
                    company = PopulateCompanyFromIDataReader(dr);
                }
                return company;
            }
        }
        #endregion

        #region CompanyQualification
        public override int CreateQualification(CompanyQualification qualification)
        {
            ELParameter paramID = new ELParameter("@QualificationID", DbType.Int32, 4, ParameterDirection.Output);
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@CompanyID",DbType.Int32,qualification.CompanyID),
                new ELParameter("@QualificationFile",DbType.String,qualification.QualificationFile),
                new ELParameter("@QualificationName",DbType.String,qualification.QualificationName),
                new ELParameter("@QualificationDesc",DbType.String,qualification.QualificationDesc),
                new ELParameter("@QualificationMemo",DbType.String,qualification.QualificationMemo),
                new ELParameter("@QualificationStatus",DbType.Int32,qualification.QualificationStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                paramID
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_CompanyQualification_Create", elParameters);
            return Convert.ToInt32(paramID.Value);
        }

        public override CompanyQualification GetQualification(int qualificationID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@QualificationID",DbType.Int32,qualificationID),
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_CompanyQualification_Get", elParameters))
            {
                CompanyQualification qualification = null;
                if (dr.Read())
                {
                    qualification = PopulateQualificationFromIDataReader(dr);
                }
                return qualification;
            }
        }

        public override List<CompanyQualification> GetQualificaionByCompanyID(int companyID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@CompanyID",DbType.Int32,companyID),
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_CompanyQualification_GetByCompanyID", elParameters))
            {
                List<CompanyQualification> qualifications = new List<CompanyQualification>();
                CompanyQualification qualification = null;
                while (dr.Read())
                {
                    qualification = PopulateQualificationFromIDataReader(dr);
                    qualifications.Add(qualification);
                }
                return qualifications;
            }
        }

        public override bool DeleteQualification(int qualificationID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@QualificationID",DbType.Int32,qualificationID)
            };
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_CompanyQualification_Delete", elParameters)) == 1;
        }
        #endregion

        #region Tag
        public override List<Tag> GetTags()
        {
            IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Tags_Get");
            return PopulateTagListFromIDataReader(dr);
        }

        public override List<Tag> GetTagsByArticle(int articleID)
        {
            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@ArticleID",DbType.Int32,articleID)
            };
            IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_TagArticle_Get", elParameters);
            return PopulateTagListFromIDataReader(dr);
        }

        public override List<Tag> GetTagsByProduct(int productID)
        {
            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@ProductID",DbType.Int32,productID)
            };
            IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_TagProduct_Get", elParameters);
            return PopulateTagListFromIDataReader(dr);
        }

        public override void UpdateTagArticle(int articleID, string tagList)
        {

            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@TagList",DbType.String,tagList),
                new ELParameter("@ArticleID",DbType.Int32,articleID),
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_TagArticle_CreateUpdate", elParameters);
        }

        public override void UpdateTagProduct(int productID, string tagList)
        {

            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@TagList",DbType.String,tagList),
                new ELParameter("@ProductID",DbType.Int32,productID),
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_TagProduct_CreateUpdate", elParameters);
        }
        #endregion

        #region Activity
        public override Dictionary<string, ActivityItem> GetActivityItems()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_ActivityItem_Get"))
            {
                Dictionary<string, ActivityItem> dicItems = new Dictionary<string, ActivityItem>();
                while (dr.Read())
                {
                    ActivityItem item = PopulateActivityItemFromIDataReader(dr);
                    dicItems.Add(item.ActivityKey, item);
                }
                return dicItems;
            }
        }

        public override List<UserActivity> GetUserActivities(UserActivityQuery query, out int totalRecord)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,QueryGenerator.BuildActivityQuery(query))
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Activities_Get", elParameters))
            {
                List<UserActivity> activityList = new List<UserActivity>();
                while (dr.Read())
                    activityList.Add(PopulateUserActivityFromIDataReader(dr));
                dr.NextResult();
                dr.Read();
                totalRecord = DataRecordHelper.GetInt32(dr, 0);
                return activityList;
            }
        }

        public override UserActivity LogUserActivity(UserActivity message)
        {
            ELParameter paramID = new ELParameter("@UserActivityID", DbType.Int32, 4, ParameterDirection.Output);
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@ActivityID",DbType.Int32,message.ActivityID),
                new ELParameter("@ActivityTitle",DbType.String,message.ActivityTitle),
                new ELParameter("@ActivityContent",DbType.String,message.ActivityContent),
                new ELParameter("@Operator",DbType.Int32,message.ActivityUser),
                paramID
            };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Activity_Create", elParameters);
            message.UserActivityID = Convert.ToInt32(paramID.Value);
            return message;
        }

        public override void DeleteUserActivity(int messageID)
        {
            ELParameter paramID = new ELParameter("@UserActivityID", DbType.Int32, messageID);
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Activity_Delete", paramID);
        }
        #endregion

        #region Organization
        public override int CreateUpdateOrganization(Organization organization, DataProviderAction action)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@OrganizationID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@OrganizationID", DbType.Int32, organization.OrganizationID);

            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@OrganizationName",DbType.String,organization.OrganizationName),
                new ELParameter("@OrganizationDesc",DbType.String,organization.OrganizationDesc),
                new ELParameter("@OrganizationMemo",DbType.String,organization.OrganizationMemo),
                new ELParameter("@ParentID",DbType.Int32,DataHelper.IntOrNull(organization.ParentID)),
                new ELParameter("@DisplayOrder",DbType.Int32,organization.DisplayOrder),
                new ELParameter("@OrganizationStatus",DbType.Int32,organization.OrganizationStatus),
                new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                new ELParameter("@PropertyNames",DbType.String,organization.GetSerializerData().Keys),
                new ELParameter("@PropertyValues",DbType.String,organization.GetSerializerData().Values),
                new ELParameter("@Action",DbType.Int32,action),
                paramID
                };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Organization_CreateUpdate", elParameters);
            if (action == DataProviderAction.Create)
                return Convert.ToInt32(paramID.Value);
            else
                return organization.OrganizationID;
        }

        public override bool DeleteOrganization(int organizationID)
        {
            ELParameter paramID = new ELParameter("@OrganizationID", DbType.Int32, organizationID);
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Organization_Delete", paramID)) == 1;
        }
        /// <summary>
        /// 只需要3种结果
        /// <remarks>
        /// 1. DataActionStatus.Success: 成功删除所有
        /// 2. DataActionStatus.UnknownFailure 删除失败
        /// 3. DataActionStatus.RelationshipExist 删除时组织结构下存在关联用户(无关联用户部门继续删除)
        ///             即 部门组织结构下存在关联用户的，将无法被删除！
        /// </remarks>
        /// </summary>
        /// <param name="organizationIDList">组织结构ID列，如"1,12,34"</param>
        /// <returns></returns>
        public override DataActionStatus DeleteOrganization(string organizationIDList)
        {
            ELParameter param = new ELParameter("@OrganizationIDList", DbType.String, organizationIDList);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_Organizations_Delete", param));
        }

        public override List<Organization> GetOrganizations()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Organizations_Get"))
            {
                List<Organization> organizations = new List<Organization>();
                while (dr.Read())
                {
                    organizations.Add(PopulateOrganizationFromIDataReader(dr));
                }
                return organizations;
            }
        }
        #endregion

        #region Area
        public override List<Area> GetAreas()
        {
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Areas_Get"))
            {
                List<Area> areas = new List<Area>();
                while (dr.Read())
                {
                    areas.Add(PopulateAreaFromIDataReader(dr));
                }
                return areas;
            }
        }
        #endregion

        #region Favorite
        public override Favorite GetFavorite(int favoriteID)
        {
            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@FavoriteID",DbType.Int32,favoriteID),};
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Favorite_Get", elParameters))
            {
                Favorite favorite = null;
                if (dr.Read())
                {
                    favorite = PopulateFavoriteFromIDataReader(dr);
                }
                return favorite;
            }
        }

        public override Favorite CreateUpdateFavorite(Favorite favorite, DataProviderAction action)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@FavoriteID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@FavoriteID", DbType.Int32, favorite.FavoriteID);
            ELParameter[] elParameters = new ELParameter[]{
                paramID,
                new ELParameter("@UserID",DbType.Int32,favorite.UserID),
                new ELParameter("@FavoriteType",DbType.Int32,favorite.FavoriteType),
                new ELParameter("@FavoriteTitle",DbType.String,favorite.FavoriteTitle),
                new ELParameter("@RelatedID",DbType.Int32,favorite.RelatedID),
                new ELParameter("@FavoriteUrl",DbType.String,favorite.FavoriteUrl),
                new ELParameter("@FavoriteLevel",DbType.Int32,favorite.FavoriteLevel),
                new ELParameter("@FavoriteMemo",DbType.String,favorite.FavoriteMemo),
                new ELParameter("@Action",DbType.Int32,action),
                };
            DataHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Favorite_CreateUpdate", elParameters);
            if (action == DataProviderAction.Create)
                favorite.FavoriteID = Convert.ToInt32(paramID.Value);
            return favorite;
        }

        public override bool DeleteFavorite(int favoriteID)
        {
            ELParameter paramID = new ELParameter("@FavoriteID", DbType.Int32, favoriteID);
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure,
                "sp_Favorite_Delete", paramID)) == 1;
        }

        public override List<Favorite> GetFavorites(FavoriteQuery query, out int totalRecord)
        {

            ELParameter[] elParameters = new ELParameter[]{
                new ELParameter("@PageIndex",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageIndex)),
                new ELParameter("@PageSize",DbType.Int32,DataHelper.GetSafeSqlInt(query.PageSize)),
                new ELParameter("@SqlPopulate",DbType.String,string.Empty)
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_Favorites_Get", elParameters))
            {
                List<Favorite> favoriteList = new List<Favorite>();
                while (dr.Read())
                    favoriteList.Add(PopulateFavoriteFromIDataReader(dr));
                dr.NextResult();
                dr.Read();
                totalRecord = DataRecordHelper.GetInt32(dr, 0);
                return favoriteList;
            }
        }
        #endregion

        #region UserGrade
        public override UserGrade CreateUpdateUserGrade(UserGrade userGrade, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@GradeID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@GradeID", DbType.Int32, userGrade.GradeID);

            ELParameter[] elParameters = new ELParameter[]{
	                paramID,
                    new ELParameter("@UserID",DbType.Int32,userGrade.UserID),
                    new ELParameter("@GradeLevel",DbType.Int32,userGrade.GradeLevel),
                    new ELParameter("@GradeLimit",DbType.String,userGrade.GradeLimit),
                    new ELParameter("@GradeName",DbType.String,userGrade.GradeName),
                    new ELParameter("@GradeDesc",DbType.String,userGrade.GradeDesc),
                    new ELParameter("@GradeMemo",DbType.String,userGrade.GradeMemo),
                    new ELParameter("@GradeStatus",DbType.Int32,userGrade.GradeStatus),
                    new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                    new ELParameter("@Action",DbType.Int32,action),
            };
            status = (DataActionStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_UserGrade_CreateUpdate", elParameters));

            if (action == DataProviderAction.Create)
                userGrade.GradeID = Convert.ToInt32(paramID.Value);
            return userGrade;
        }

        public override UserGrade GetUserGrade(int userGradeID)
        {
            ELParameter paramID = new ELParameter("@GradeID", DbType.Int32, userGradeID);

            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_UserGrade_Get", paramID))
            {
                UserGrade userGrade = null;
                if (dr.Read())
                {
                    userGrade = PopulateUserGradeFromIDataReader(dr);
                }
                return userGrade;
            }
        }

        public override List<UserGrade> GetUserGradeByUserID(int userID)
        {
            ELParameter paramID = new ELParameter("@UserID", DbType.Int32, userID);

            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_UserGrade_DeleteByUserID", paramID))
            {
                List<UserGrade> userGrades = new List<UserGrade>();
                while (dr.Read())
                {
                    userGrades.Add(PopulateUserGradeFromIDataReader(dr));
                }
                return userGrades;
            }
        }

        public override bool DeleteUserGrade(int userGradeID)
        {
            ELParameter paramID = new ELParameter("@GradeID", DbType.Int32, userGradeID);
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_UserGrade_Delete", paramID)) == 1;
        }

        public override bool ClearUserGrade(int userID)
        {
            ELParameter paramID = new ELParameter("@UserID", DbType.Int32, userID);
            return Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_UserGrade_DeleteByUserID", paramID)) == 1;
        }
        #endregion

        #region TemporaryAttachment
        public override void CreateUpdateTemporaryAttachment(TemporaryAttachment attachment, DataProviderAction action)
        {
            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@AttachmentID",DbType.Guid,attachment.AttachmentID),
                new ELParameter("@UserID",DbType.Int32,attachment.UserID),
                new ELParameter("@AttachmentType",DbType.Int32,attachment.AttachmentType),
                new ELParameter("@FileName",DbType.String,attachment.FileName),
                new ELParameter("@FriendlyFileName",DbType.String,attachment.FriendlyFileName),
                new ELParameter("@DisplayOrder",DbType.Int32,attachment.DisplayOrder),
                new ELParameter("@ContentType",DbType.String,attachment.ContentType),
                new ELParameter("@ContentSize",DbType.Int64,attachment.ContentSize),
                new ELParameter("@Height",DbType.Int32,attachment.Height),
                new ELParameter("@Width",DbType.Int32,attachment.Width),
                new ELParameter("@Action",DbType.Int32,action),
            };
            DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_TemporaryAttachment_CreateUpdate", elParameters);
        }

        public override void DeleteTemporaryAttachment(Guid attachmentID)
        {
            ELParameter paramID = new ELParameter("@AttachmentID", DbType.Guid, attachmentID);
            DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_TemporaryAttachment_Delete", paramID);
        }

        public override TemporaryAttachment GetTemporaryAttachment(Guid attachmentID)
        {
            ELParameter paramID = new ELParameter("@AttachmentID", DbType.Guid, attachmentID);
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_TemporaryAttachment_Get", paramID))
            {
                TemporaryAttachment attachment = null;
                if (dr.Read())
                    attachment = PopulateTemporaryAttachmentFromIDataReader(dr);
                return attachment;
            }
        }

        public override List<TemporaryAttachment> GetTemporaryAttachments(int userID, AttachmentType attachmentType)
        {
            ELParameter[] elParameters = new ELParameter[]{
	            new ELParameter("@UserID",DbType.Int32,userID),
                new ELParameter("@AttachmentType",DbType.Int32,attachmentType)
            };
            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_TemporaryAttachments_Get", elParameters))
            {
                List<TemporaryAttachment> attachments = new List<TemporaryAttachment>();
                while (dr.Read())
                    attachments.Add(PopulateTemporaryAttachmentFromIDataReader(dr));
                return attachments;
            }
        }
        #endregion

        #region CustomerGrade
        public override CustomerGrade CreateUpdateCustomerGrade(CustomerGrade customerGrade, DataProviderAction action, out DataActionStatus status)
        {
            ELParameter paramID = null;
            if (action == DataProviderAction.Create)
                paramID = new ELParameter("@GradeID", DbType.Int32, 4, ParameterDirection.Output);
            else
                paramID = new ELParameter("@GradeID", DbType.Int32, customerGrade.GradeID);

            ELParameter[] elParameters = new ELParameter[]{
	                paramID,
                    new ELParameter("@CompanyID",DbType.Int32,customerGrade.CompanyID),
                    new ELParameter("@GradeLevel",DbType.Int32,customerGrade.GradeLevel),
                    new ELParameter("@GradeLimit",DbType.String,customerGrade.GradeLimit),
                    new ELParameter("@GradeName",DbType.String,customerGrade.GradeName),
                    new ELParameter("@GradeDesc",DbType.String,customerGrade.GradeDesc),
                    new ELParameter("@GradeMemo",DbType.String,customerGrade.GradeMemo),
                    new ELParameter("@GradeStatus",DbType.Int32,customerGrade.GradeStatus),
                    new ELParameter("@Operator",DbType.Int32,GlobalSettings.GetCurrentUser().UserID),
                    new ELParameter("@Action",DbType.Int32,action),
            };
            status = (DataActionStatus)Convert.ToInt32(
                DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_CustomerGrade_CreateUpdate", elParameters));

            if (action == DataProviderAction.Create)
                customerGrade.GradeID = Convert.ToInt32(paramID.Value);
            return customerGrade;
        }

        public override CustomerGrade GetCustomerGrade(int customerGradeID)
        {
            ELParameter paramID = new ELParameter("@GradeID", DbType.Int32, customerGradeID);

            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_CustomerGrade_Get", paramID))
            {
                CustomerGrade customerGrade = null;
                if (dr.Read())
                {
                    customerGrade = PopulateCustomerGradeFromIDataReader(dr);
                }
                return customerGrade;
            }
        }

        public override List<CustomerGrade> GetCustomerGradeByCompanyID(int companyID)
        {
            ELParameter paramID = new ELParameter("@CompanyID", DbType.Int32, companyID);

            using (IDataReader dr = DataHelper.ExecuteReader(CommandType.StoredProcedure, "sp_CustomerGrade_GetByCompanyID", paramID))
            {
                List<CustomerGrade> customerGrades = new List<CustomerGrade>();
                while (dr.Read())
                {
                    customerGrades.Add(PopulateCustomerGradeFromIDataReader(dr));
                }
                return customerGrades;
            }
        }

        public override DataActionStatus DeleteCustomerGrade(int customerGradeID)
        {
            ELParameter paramID = new ELParameter("@GradeID", DbType.Int32, customerGradeID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_CustomerGrade_Delete", paramID));
        }

        public override DataActionStatus ClearCustomerGrade(int companyID)
        {
            ELParameter paramID = new ELParameter("@CompanyID", DbType.Int32, companyID);
            return (DataActionStatus)Convert.ToInt32(DataHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_CustomerGrade_DeleteByCompanyID", paramID));
        }
        #endregion
    }
}
