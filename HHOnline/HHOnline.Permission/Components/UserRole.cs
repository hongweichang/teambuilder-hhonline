using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    [Serializable]
    public class UserRole
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Permission.Components.UserRole"/>的新实例
        /// </summary>
        public UserRole() { }
        private int _UserRoleID;
        /// <summary>
        /// 角色用户ID
        /// </summary>
        public int UserRoleID
        {
            get { return _UserRoleID; }
            set { _UserRoleID = value; }
        }
        private int _UserID;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _RoleID;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        private UserRoleStatus _UserRoleStatus;
        /// <summary>
        /// 状态
        /// </summary>
        public UserRoleStatus UserRoleStatus
        {
            get { return _UserRoleStatus; }
            set { _UserRoleStatus = value; }
        }
        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private int _CreateUser;
        /// <summary>
        /// 创建用户 
        /// </summary>
        public int CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime _UpdateTime;
        /// <summary>
        /// 最近更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }
        private int _UpdateUser;
        /// <summary>
        /// 最近更新操作用户
        /// </summary>
        public int UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
    }
}
