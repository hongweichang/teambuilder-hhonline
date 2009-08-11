using System;

namespace HHOnline.Framework
{

    [Serializable]
    public class UserActivity
    {
        #region --Private Members--
        private int _userActivityID;
        private int _activityID;
        private string _activityTitle;
        private string _activityContent;
        private DateTime _activityTime;
        private int _activityUser;
        #endregion

        #region --Constructor--
        public UserActivity()
        {
        }
        #endregion

        #region --Public Members--
        ///<summary>
        ///用户操作记录编号
        ///</summary>
        public int UserActivityID
        {
            get { return _userActivityID; }
            set { _userActivityID = value; }
        }

        ///<summary>
        ///用户操作动态类型
        ///</summary>
        public int ActivityID
        {
            get { return _activityID; }
            set { _activityID = value; }
        }

        ///<summary>
        ///操作主题描述
        ///</summary>
        public string ActivityTitle
        {
            get { return _activityTitle; }
            set { _activityTitle = value; }
        }

        ///<summary>
        ///操作内容序列化
        ///</summary>
        public string ActivityContent
        {
            get { return _activityContent; }
            set { _activityContent = value; }
        }

        ///<summary>
        ///操作发生时间点
        ///</summary>
        public DateTime ActivityTime
        {
            get { return _activityTime; }
            set { _activityTime = value; }
        }

        ///<summary>
        ///操作执行用户
        ///</summary>
        public int ActivityUser
        {
            get { return _activityUser; }
            set { _activityUser = value; }
        }
        #endregion
    }
}
