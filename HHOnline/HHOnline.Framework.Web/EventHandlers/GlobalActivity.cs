using System;
using System.Xml;
using HHOnline.Framework;

namespace HHOnline.Framework.Web.EventHandlers
{
    /// <summary>
    /// 全局活动记录
    /// </summary>
    public class GlobalActivity : IGlobalModule
    {

        public void Init(GlobalApplication context, XmlNode node)
        {
            context.PostUserUpdate += new UserEventHandler(context_PostUserUpdate);
            context.UserValidated += new UserEventHandler(context_UserValidated);
        }

        void context_UserValidated(User user, HHEventArgs e)
        {
            ActivityItem item = ActivityManager.GetActivityItem("UserLogin");
            if (item != null && item.IsEnabled)
            {
                UserActivity activity = new UserActivity();
                activity.ActivityUser = user.UserID;
                activity.ActivityContent = string.Format("用户{0}于{1}登陆系统", user.UserName,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                activity.ActivityTitle = item.ActivityName;
                activity.ActivityID = item.ActivityID;
                ActivityManager.LogUserActivity(activity);
            }
        }

        void context_PostUserUpdate(User user, HHEventArgs e)
        {
            ActivityItem item = null;
            string content = string.Empty;
            if (e.State == ObjectState.Create)
            {
                item = ActivityManager.GetActivityItem("UserCreate");
                content = string.Format("用户{0}于{1}注册系统", user.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            }
            else if (e.State == ObjectState.Update)
            {
                item = ActivityManager.GetActivityItem("UserUpdate");
                content = string.Format("用户{0}于{1}更新个人信息", user.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            }
            else if (e.State == ObjectState.Delete)
            {
                item = ActivityManager.GetActivityItem("UserDelete");
                content = string.Format("用户{0}于{1}删除账号{2}", GlobalSettings.GetCurrentUser().UserName,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm"), user.UserName);
            }
            if (item != null && item.IsEnabled)
            {
                UserActivity activity = new UserActivity();
                activity.ActivityUser = GlobalSettings.GetCurrentUser().UserID;
                activity.ActivityContent = content;
                activity.ActivityTitle = item.ActivityName;
                activity.ActivityID = item.ActivityID;
                ActivityManager.LogUserActivity(activity);
            }
        }
    }
}
