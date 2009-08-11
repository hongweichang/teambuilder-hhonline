using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 动态项
    /// </summary>
    public class ActivityItem
    {
        #region --Private Members--
        private int _activityID;
        private string _activityKey;
        private string _activityName;
        private bool _isEnabled;
        #endregion

        #region --Public Members--
        ///<summary>
        ///操作编号
        ///</summary>
        public int ActivityID
        {
            get { return _activityID; }
            set { _activityID = value; }
        }

        ///<summary>
        ///操作键值
        ///</summary>
        public string ActivityKey
        {
            get { return _activityKey; }
            set { _activityKey = value; }
        }

        ///<summary>
        ///操作名称
        ///</summary>
        public string ActivityName
        {
            get { return _activityName; }
            set { _activityName = value; }
        }

        ///<summary>
        ///是否可用
        ///</summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
        #endregion
    }
}
