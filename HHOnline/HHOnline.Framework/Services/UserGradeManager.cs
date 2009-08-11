using System;
using System.Collections.Generic;
using System.Text;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户级别管理
    /// </summary>
    public class UserGradeManager
    {
        /// <summary>
        /// 添加用户级别
        /// </summary>
        /// <param name="userGrade"></param>
        public static DataActionStatus AddUserGrade(UserGrade userGrade)
        {
            DataActionStatus status;
            userGrade = CommonDataProvider.Instance.CreateUpdateUserGrade(userGrade, DataProviderAction.Create, out status);
            HHCache.Instance.Remove(CacheKeyManager.GetUserGradeKeyByUserID(userGrade.UserID));
            return status;
        }

        /// <summary>
        /// 更新用户级别
        /// </summary>
        /// <param name="userGrade"></param>
        /// <returns></returns>
        public static DataActionStatus UpdateUserGrade(UserGrade userGrade)
        {
            DataActionStatus status;
            CommonDataProvider.Instance.CreateUpdateUserGrade(userGrade, DataProviderAction.Update, out status);
            HHCache.Instance.Remove(CacheKeyManager.GetUserGradeKey(userGrade.GradeID));
            HHCache.Instance.Remove(CacheKeyManager.GetUserGradeKeyByUserID(userGrade.UserID));
            return status;
        }

        /// <summary>
        /// 根据ID获取用户级别信息
        /// </summary>
        /// <param name="userGradeID"></param>
        /// <returns></returns>
        public static UserGrade GetUserGrade(int userGradeID)
        {
            string cacheKey = CacheKeyManager.GetUserGradeKey(userGradeID);
            UserGrade userGrade = HHCache.Instance.Get(cacheKey) as UserGrade;
            if (userGrade == null)
            {
                userGrade = CommonDataProvider.Instance.GetUserGrade(userGradeID);
                HHCache.Instance.Insert(cacheKey, userGrade);
            }
            return userGrade;
        }

        /// <summary>
        /// 获取该用户所有级别信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<UserGrade> GetUserGrades(int userID)
        {
            string cacheKey = CacheKeyManager.GetUserGradeKeyByUserID(userID);

            List<UserGrade> userGrades = HHCache.Instance.Get(cacheKey) as List<UserGrade>;

            if (userGrades == null)
            {
                userGrades = CommonDataProvider.Instance.GetUserGradeByUserID(userID);
                HHCache.Instance.Insert(cacheKey, userGrades);
            }

            return userGrades;
        }

        /// <summary>
        /// 清理用户级别
        /// </summary>
        /// <param name="userID"></param>
        public static bool ClearUserGrade(int userID)
        {
            bool flag = CommonDataProvider.Instance.ClearUserGrade(userID);
            if (flag)
                HHCache.Instance.Remove(CacheKeyManager.GetUserGradeKeyByUserID(userID));
            return flag;
        }

        /// <summary>
        /// 删除用户级别
        /// </summary>
        /// <param name="gradeID"></param>
        public static bool DeleteUserGrade(int gradeID)
        {
            bool flag = CommonDataProvider.Instance.DeleteUserGrade(gradeID);
            if (flag)
                HHCache.Instance.Remove(CacheKeyManager.GetUserGradeKey(gradeID));
            return flag;
        }
    }
}
