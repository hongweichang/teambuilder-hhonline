using System;
using HHOnline.Common;

namespace HHOnline.Framework
{
    /// <summary>
    /// 用户查询接口
    /// </summary>
    public class UserQuery
    {
        private string displayNameFilter = string.Empty;
        private string emailFilter = string.Empty;
        private string userNameFilter = string.Empty;
        private string companyName = string.Empty;
        private string organizationName = string.Empty;
        private int pageIndex = 0;
        private int pageSize = 20;

        /// <summary>
        /// 排列顺序
        /// </summary>
        public SortOrder SortOrder = SortOrder.Descending;

        /// <summary>
        /// 用户排序
        /// </summary>
        public SortUsersBy SortBy = SortUsersBy.LastActiveDate;

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType? UserType;

        /// <summary>
        /// 公司编号
        /// </summary>
        public int? CompanyID
        {
            get;
            set;
        }

        /// <summary>
        /// Role 编号
        /// </summary>
        public int? RoleID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public AccountStatus AccountStatus = AccountStatus.All;

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
        /// 组织机构ID
        /// </summary>
        public int? OrganizationID { get; set; }

        /// <summary>
        /// 用户显示名查询
        /// </summary>
        public string DisplayNameFilter
        {
            get
            {
                return GlobalSettings.CleanSearchString(displayNameFilter);
            }
            set
            {
                displayNameFilter = value;
            }
        }

        /// <summary>
        /// 登陆名Filter
        /// </summary>
        public string LoginNameFilter
        {
            get
            {
                return GlobalSettings.CleanSearchString(userNameFilter);
            }
            set
            {
                userNameFilter = value;
            }
        }

        /// <summary>
        /// Email查询
        /// </summary>
        public string EmailFilter
        {
            get
            {
                return GlobalSettings.CleanSearchString(emailFilter);
            }
            set
            {
                emailFilter = value;
            }
        }

        /// <summary>
        /// LastActivityDate 值小于或等于 UserInactiveSinceDate 属性值的记录
        /// </summary>
        public DateTime? InactiveSinceDate
        {
            get;
            set;
        }

    }
}
