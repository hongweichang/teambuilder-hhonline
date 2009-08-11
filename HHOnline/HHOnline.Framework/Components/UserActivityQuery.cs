using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户动态查询接口
    /// </summary>
    public class UserActivityQuery
    {
        private int pageIndex = 0;
        private int pageSize = 20;
        private int userID = -1;
        private DateTime startTime = GlobalSettings.MinValue;
        private DateTime endTime = GlobalSettings.MaxValue;
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (this.pageIndex >= 0)
                {
                    return this.pageIndex;
                }
                return 0;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }


        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType? UserType;

        /// <summary>
        /// 动态开始时间（小于等于CreateTime)
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        /// <summary>
        /// 动态结束时间（大于等于CreateTime)
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }


    }
}
