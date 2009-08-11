using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HHOnline.Cache;
using HHOnline.Framework.Providers;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户动态管理
    /// </summary>
    public class ActivityManager
    {
        /// <summary>
        /// 获取所有动态项
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ActivityItem> GetActivityItems()
        {
            string userKey = CacheKeyManager.ActivityItemKey;
            Dictionary<string, ActivityItem> dicItems = HHCache.Instance.Get(userKey) as Dictionary<string, ActivityItem>;
            if (dicItems == null)
            {
                dicItems = CommonDataProvider.Instance.GetActivityItems();
                HHCache.Instance.Max(userKey, dicItems);
            }
            return dicItems;
        }

        /// <summary>
        /// 通过Key获取动态项
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static ActivityItem GetActivityItem(string itemKey)
        {
            Dictionary<string, ActivityItem> dicItems = GetActivityItems();
            if (dicItems.ContainsKey(itemKey))
                return dicItems[itemKey];
            return null;
        }

        /// <summary>
        /// 获取所有动态信息
        /// </summary>
        /// <returns></returns>
        public static List<UserActivity> GetUserActivities()
        {
            UserActivityQuery query = new UserActivityQuery();
            query.PageSize = Int32.MaxValue;
            return GetUserActivities(query).Records;
        }

        /// <summary>
        /// 根据<paramref name="query"/>查询动态信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static PagingDataSet<UserActivity> GetUserActivities(UserActivityQuery query)
        {

            PagingDataSet<UserActivity> activities = null;
            string cacheKey = CacheKeyManager.GetUserActivityQueryKey(query);
            //从缓存读取
            if (HttpContext.Current != null)
                activities = HttpContext.Current.Items[cacheKey] as PagingDataSet<UserActivity>;

            if (activities != null)
                return activities;

            activities = HHCache.Instance.Get(cacheKey) as PagingDataSet<UserActivity>;

            if (activities == null)
            {
                int totalRecods;
                List<UserActivity> activityList = CommonDataProvider.Instance.GetUserActivities(query, out totalRecods);
                activities = new PagingDataSet<UserActivity>();
                activities.Records = activityList;
                activities.TotalRecords = totalRecods;

                HHCache.Instance.Insert(cacheKey, activities, 1);
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items[cacheKey] = activities;
                }
            }
            return activities;
        }

        /// <summary>
        /// 记录动态信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void LogUserActivity(UserActivity message)
        {
            CommonDataProvider.Instance.LogUserActivity(message);
            HHCache.Instance.Remove(CacheKeyManager.ActivityPrefix);
        }

        /// <summary>
        /// 删除动态信息
        /// </summary>
        /// <param name="messageID"></param>
        public static void DeleteUserActivity(int messageID)
        {
            CommonDataProvider.Instance.DeleteUserActivity(messageID);
            HHCache.Instance.Remove(CacheKeyManager.ActivityPrefix);
        }
    }
}
