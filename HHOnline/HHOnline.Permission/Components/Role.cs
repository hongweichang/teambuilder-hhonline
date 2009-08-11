using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Permission.Components
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Serializable]
    public class Role
    {
        #region -Constructor-
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Role"/>的实例
        /// </summary>
        public Role()
            : this(0, string.Empty, string.Empty, string.Empty, 1, null, null, null, null)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Role"/>的实例
        /// </summary>
        /// <param name="_RoleName"></param>
        /// <param name="_Description"></param>
        /// <param name="_Momo"></param>
        /// <param name="_RoleStatus">角色状态，1：启用，2：停用</param>
        /// <param name="_CreateTime"></param>
        /// <param name="_CreateUser"></param>
        /// <param name="_UpdateTime"></param>
        /// <param name="_UpdateUser"></param>
        public Role(string _RoleName, string _Description, string _Memo,
            int _RoleStatus, DateTime? _CreateTime, int? _CreateUser, DateTime? _UpdateTime, int? _UpdateUser)
            : this(0, _RoleName, _Description, _Memo, _RoleStatus,_CreateTime ,_CreateUser,_UpdateTime,_UpdateUser)
        { }
        /// <summary>
        /// 初始化<see cref="HHOnline.Permission.Components.Role"/>的实例
        /// </summary>
        /// <param name="_RoleID"></param>
        /// <param name="_RoleName"></param>
        /// <param name="_Description"></param>
        /// <param name="_Momo"></param>
        /// <param name="_RoleStatus">角色状态，1：启用，2：停用</param>
        /// <param name="_CreateTime"></param>
        /// <param name="_CreateUser"></param>
        /// <param name="_UpdateTime"></param>
        /// <param name="_UpdateUser"></param>
        public Role(int _RoleID, string _RoleName, string _Description,
                string _Momo, int _RoleStatus, DateTime? _CreateTime, int? _CreateUser, DateTime? _UpdateTime, int? _UpdateUser)
        {
            this._RoleID = _RoleID;
            this._RoleName = _RoleName;
            this._Description = _Description;
            this._Memo = _Momo;
            this._RoleStatus = _RoleStatus;
            this._CreateUser = _CreateUser;
            this._CreateTime = _CreateTime;
            this._UpdateTime = _UpdateTime;
            this._UpdateUser = _UpdateUser;
        }
        #endregion

        #region -Properties-
        private int _RoleID;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        private string _RoleName;
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private string _Description;
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Memo;
        /// <summary>
        /// 角色备注
        /// </summary>
        public string Memo
        {
            get { return _Memo; }
            set { _Memo = value; }
        }
        private int _RoleStatus;
        /// <summary>
        /// 角色状态，1：启用，2：停用
        /// </summary>
        public int RoleStatus
        {
            get { return _RoleStatus; }
            set { _RoleStatus = value; }
        }
        private DateTime? _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private int? _CreateUser;
        /// <summary>
        /// 创建用户
        /// </summary>
        public int? CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }
        private int? _UpdateUser;
        /// <summary>
        /// 更新用户
        /// </summary>
        public int? UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
        #endregion
    }
}
