using System;

namespace HHOnline.Framework
{
    /// <summary>
    /// 客户申请审核
    /// </summary>
    public class Pending
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Pending"/>的新实例
        /// </summary>
        public Pending() { }
        private int _ID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _CompanyID;
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        private CompanyType _CompanyType;
        /// <summary>
        /// 申请类型
        /// </summary>
        public CompanyType CompanyType
        {
            get { return _CompanyType; }
            set { _CompanyType = value; }
        }
        private PendingStatus _Status;
        /// <summary>
        /// 申请审核状态
        /// </summary>
        public PendingStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Description;
        /// <summary>
        /// 状态更新描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _DenyReason;
        /// <summary>
        /// 状态更新描述
        /// </summary>
        public string DenyReason
        {
            get { return _DenyReason; }
            set { _DenyReason = value; }
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
        private DateTime _CreateTime;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private int _UpdateUser;
        /// <summary>
        /// 更新用户
        /// </summary>
        public int UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
        private DateTime _UpdateTime;
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }
    }
}
