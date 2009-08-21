using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    public class Users
    {
        #region GetUser
        /// <summary>
        /// 根据用户编号获取用户信息（使用缓存）
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>用户信息</returns>
        public static User GetUser(int userID)
        {
            return GetUser(userID, null, false, true);
        }

        /// <summary>
        /// 根据用户名称获取用户信息（使用缓存）
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns>用户信息</returns>
        public static User GetUser(string userName)
        {
            return GetUser(0, userName, false, true);
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="isOnline">是否在线（更新ActiveTime)</param>
        /// <param name="useCache">是否使用缓存</param>
        /// <returns>用户信息</returns>
        public static User GetUser(int userID, bool isOnline, bool useCache)
        {
            return GetUser(userID, null, isOnline, useCache);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="isOnline">是否在线（更新ActiveTime)</param>
        /// <param name="useCache">是否使用缓存</param>
        /// <returns>用户信息</returns>
        public static User GetUser(string userName, bool isOnline, bool useCache)
        {
            return GetUser(0, userName, isOnline, useCache);
        }

        /// <summary>
        ///通过用户编号或用户名称获取用户信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="username">用户名称</param>
        /// <param name="isOnline">是否在线：修改LastActiveTime</param>
        /// <param name="useCache">是否从缓存读取</param>
        /// <returns>用户信息</returns>
        public static User GetUser(int userID, string username, bool isOnline, bool useCache)
        {
            User user = null;
            string cacheKey = (userID > 0) ? CacheKeyManager.GetUserKey(userID) : CacheKeyManager.GetUserKey(username);
            if (useCache)
            {
                if (HttpContext.Current != null)
                {
                    user = HttpContext.Current.Items[cacheKey] as User;
                }
                if (user != null)
                    return user;
                user = HHCache.Instance.Get(cacheKey) as User;
            }
            if (user == null)
            {
                user = CommonDataProvider.Instance.GetUser(userID, username, isOnline);
                if (!useCache)
                {
                    return user;
                }

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = user;
                }
                AddCachedUser(user);
            }
            return user;
        }
        #endregion

        #region GetUniqueId
        /// <summary>
        /// 根据用户名获取Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int GetUniqueId(string userName)
        {
            User user = GetUser(userName);
            if (user != null)
                return user.UserID;
            else
                return 0;
        }
        #endregion

        #region CreateUser
        /// <summary>
        /// /创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public static CreateUserStatus Create(User user, Company company)
        {
            return Create(user, company, false);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public static CreateUserStatus Create(User user, Company company, bool ignoreDisallowNames)
        {
            //验证用户名是否合法
            if ((!ignoreDisallowNames) && (DisallowedNames.NameIsDisallowed(user.UserName)))
                return CreateUserStatus.DisallowedUsername;
            //触发事件
            GlobalEvents.BeforeUser(user, ObjectState.Create);
            CreateUserStatus status;
            CommonDataProvider dp = CommonDataProvider.Instance;
            status = dp.CreateUpdateUser(user, company);
            GlobalEvents.AfterUser(user, ObjectState.Create);
            return status;
        }

        public static CreateUserStatus Create(User user)
        {
            return Create(user, false);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="sendEmail">是否发送邮件</param>
        /// <returns>CreateUserStatus</returns>
        public static CreateUserStatus Create(User user, bool sendEmail)
        {
            return Create(user, sendEmail, false);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="sendEmail">是否发送邮件</param>
        /// <param name="ignoreDisallowNames">是否忽略禁用名（管理员忽略）</param>
        /// <returns>CreateUserStatus</returns>
        public static CreateUserStatus Create(User user, bool sendEmail, bool ignoreDisallowNames)
        {
            //验证用户名是否合法
            if ((!ignoreDisallowNames) && (DisallowedNames.NameIsDisallowed(user.UserName)))
                return CreateUserStatus.DisallowedUsername;
            //触发事件
            GlobalEvents.BeforeUser(user, ObjectState.Create);
            CreateUserStatus status;
            CommonDataProvider dp = CommonDataProvider.Instance;
            user = dp.CreateUpdateUser(user, DataProviderAction.Create, out status);
            GlobalEvents.AfterUser(user, ObjectState.Create);
            return status;
        }

        /// <summary>
        /// 快速创建用户
        /// <remarks>
        /// email和username必须都唯一
        /// 需要返回userid
        /// 并提供正确的创建结果MembershipCreateStatus
        /// </remarks>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static MembershipCreateStatus QuickCreate(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out int userId)
        {
            User user = new User();
            user.UserName = username;
            user.Password = password;
            user.PasswordAnswer = passwordAnswer;
            user.PasswordQuestion = passwordQuestion;
            if (isApproved)
                user.AccountStatus = AccountStatus.Authenticated;
            else
                user.AccountStatus = AccountStatus.ApprovalPending;

            CreateUserStatus status;
            CommonDataProvider dp = CommonDataProvider.Instance;
            user = dp.CreateUpdateUser(user, DataProviderAction.Create, out status);

            userId = user.UserID;
            return (MembershipCreateStatus)status;
        }

        #endregion

        #region GetUsers
        public static List<User> GetUsers()
        {
            UserQuery query = new UserQuery();
            query.PageSize = Int32.MaxValue;
            List<User> users = CommonDataProvider.Instance.GetUsers(query);
            return users;
        }

        public static PagingDataSet<User> GetUsers(UserQuery query)
        {
            return GetUsers(query, true);
        }

        public static PagingDataSet<User> GetUsers(UserQuery query, bool cacheable)
        {
            PagingDataSet<User> users = null;
            string usersKey = CacheKeyManager.GetUserQueryKey(query);
            //从缓存读取
            if (HttpContext.Current != null)
                users = HttpContext.Current.Items[usersKey] as PagingDataSet<User>;

            if (users != null)
                return users;

            if (cacheable)
                users = HHCache.Instance.Get(usersKey) as PagingDataSet<User>;

            if (users == null)
            {
                int totalRecods;
                List<User> userList = CommonDataProvider.Instance.GetUsers(query, out totalRecods);
                users = new PagingDataSet<User>();
                users.Records = userList;
                users.TotalRecords = totalRecods;

                //缓存
                if (cacheable)
                {
                    HHCache.Instance.Insert(usersKey, users, 1);
                    AddCachedUser(userList);
                }
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[usersKey] = users;
                }
            }
            return users;
        }

        public static List<int> GetUsersInRole(int roleID)
        {
            return CommonDataProvider.Instance.GetUsersInRole(roleID);
        }

        /// <summary>
        /// 获取所有的非活动用户UserName集合
        /// </summary>
        /// <param name="authenticationOption">匹配类型</param>
        /// <param name="userInactiveSinceDate">最近上线时间</param>
        /// <returns></returns>
        public static List<string> GetInactiveUsers(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            int totalCount;

            List<User> lstUsers = GetProfiles(authenticationOption, userInactiveSinceDate, string.Empty, out totalCount);

            List<string> lstNames = new List<string>();

            foreach (User user in lstUsers)
                lstNames.Add(user.UserName);

            return lstNames;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="authenticationOption"></param>
        /// <param name="userInactiveSinceDate"></param>
        /// <param name="userName">不为空时按UserName模糊搜索，为空时不判断UserName(获取所有)</param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<User> GetProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, string userName, out int totalCount)
        {
            UserQuery userQuery = new UserQuery();
            userQuery.InactiveSinceDate = userInactiveSinceDate;
            switch (authenticationOption)
            {
                case ProfileAuthenticationOption.Anonymous:
                    userQuery.AccountStatus = AccountStatus.Anonymous;
                    break;
                case ProfileAuthenticationOption.Authenticated:
                    userQuery.AccountStatus = AccountStatus.Authenticated;
                    break;
                case ProfileAuthenticationOption.All:
                    userQuery.AccountStatus = AccountStatus.All;
                    break;
                default:
                    userQuery.AccountStatus = AccountStatus.All;
                    break;
            }

            if (!GlobalSettings.IsNullOrEmpty(userName))
                userQuery.LoginNameFilter = userName;

            userQuery.PageSize = Int32.MaxValue;

            PagingDataSet<User> pgUsers = GetUsers(userQuery);

            totalCount = pgUsers.TotalRecords;
            List<User> lstUsers = pgUsers.Records;

            return lstUsers;
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// 根据Id更新User信息
        /// </summary>
        /// <param name="user"></param>
        public static bool UpdateUser(User user)
        {
            GlobalEvents.BeforeUser(user, ObjectState.Update);
            CreateUserStatus status;
            CommonDataProvider.Instance.CreateUpdateUser(user, DataProviderAction.Update, out status);
            GlobalEvents.AfterUser(user, ObjectState.Update);
            RefreshCachedUser(user);
            return status == CreateUserStatus.Success;
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool DeleteUser(string userName)
        {
            if (CommonDataProvider.Instance.DeleteUser(userName))
            {
                RefreshCachedUser(Users.GetUser(userName));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIDList">用户ID列表,例如12,15</param>
        /// <returns></returns>
        public static DataActionStatus DeleteUsers(string userIDList)
        {
            DataActionStatus status = CommonDataProvider.Instance.DeleteUsers(userIDList);
            if (status == DataActionStatus.Success || status == DataActionStatus.RelationshipExist)
                RefreshCachedUser(null);
            return status;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIDList">用户ID列表</param>
        /// <returns></returns>
        public static DataActionStatus DeleteUsers(List<string> userIDList)
        {
            return DeleteUsers(string.Join(",", userIDList.ToArray()));
        }
        #endregion

        #region ValidateUser
        /// <summary>
        /// 验证用户信息
        /// <remarks>
        /// 1. 字段已经做验证
        /// 2. 密码为明文未加密
        /// </remarks>
        /// <example>
        /// 1. 验证成功的话需修改LastActive值至当前
        /// 2. 用户必须是正常用户（不包括锁定或未审核）
        /// </example>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>true:successed, false:failed</returns>
        public static LoginUserStatus ValidateUser(string userName, string password)
        {
            //验证登陆
            LoginUserStatus status = CommonDataProvider.Instance.ValidateUser(userName, password);
            if (status == LoginUserStatus.Success)
            {
                //修改LastActive
                User user = GetUser(userName, true, false);
                //触发系统事件
                GlobalEvents.ValidatedUser(user);
            }
            return status;
        }

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static LoginUserStatus ValidateUser(ref User user)
        {
            if (user == null)
                throw new ArgumentNullException("User must have value");
            //验证登陆
            LoginUserStatus status = ValidateUser(user.UserName, user.Password);
            user = GetUser(user.UserName);
            return status;
        }
        #endregion

        #region ChangePassword
        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (ValidateUser(userName, oldPassword) != LoginUserStatus.InvalidCredentials)
            {
                return CommonDataProvider.Instance.SetPassword(userName, GlobalSettings.EncodePassword(newPassword));
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns>返回重置后的密码</returns>
        public static string ResetPassword(string userName, string email, string question, string answer)
        {
            User user = GetUser(userName);
            if (user == null)
                return string.Empty;
            if ((user.Email == email) &&
                (user.PasswordQuestion == question) &&
                (user.PasswordAnswer == GlobalSettings.EncodePassword(answer)))
            {
                string newPwd = GlobalSettings.GeneratePassword();
                if (CommonDataProvider.Instance.SetPassword(userName, GlobalSettings.EncodePassword(newPwd)))
                    return newPwd;
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region User Cache Management
        /// <summary>
        /// 刷新用户缓存信息
        /// </summary>
        /// <param name="user"></param>
        internal static void RefreshCachedUser(User user)
        {
            if (user != null)
            {
                HHCache.Instance.Remove(CacheKeyManager.GetUserKey(user.UserID));
                HHCache.Instance.Remove(CacheKeyManager.GetUserKey(user.UserName));
            }
            HHCache.Instance.Remove(CacheKeyManager.UserListPrefix);
        }

        /// <summary>
        /// 添加用户到缓存
        /// </summary>
        /// <param name="user"></param>
        internal static void AddCachedUser(User user)
        {
            if (user != null)
            {
                HHCache.Instance.Insert(CacheKeyManager.GetUserKey(user.UserID), user);
                HHCache.Instance.Insert(CacheKeyManager.GetUserKey(user.UserName), user);
            }
        }

        /// <summary>
        /// 添加用户集到缓存
        /// </summary>
        /// <param name="users"></param>
        internal static void AddCachedUser(List<User> users)
        {
            if (users != null)
            {
                foreach (User user in users)
                {
                    AddCachedUser(user);
                }
            }
        }
        #endregion
    }
}
