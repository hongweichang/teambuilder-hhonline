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
        public Pending() { 
            
        }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Pending"/>的新实例
        /// </summary>
        /// <param name="_CompanyID">公司编号</param>
        /// <param name="_CompanyType">公司类型</param>
        /// <param name="_CreatedTime">创建时间</param>
        /// <param name="_Status">申请审核状态</param>
        /// <param name="_Description">更新状态描述</param>
        public Pending(int _CompanyID, CompanyType _CompanyType, DateTime _CreatedTime, PendingStatus _Status, string _Description)
            : this(0, _CompanyID, _CompanyType, _CreatedTime, _Status, _Description)
        { }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Pending"/>的新实例
        /// </summary>
        /// <param name="_ID">编号</param>
        /// <param name="_CompanyID">公司编号</param>
        /// <param name="_CompanyType">公司类型</param>
        /// <param name="_CreatedTime">创建时间</param>
        /// <param name="_Status">申请审核状态</param>
        /// <param name="_Description">更新状态描述</param>
        public Pending(int _ID,int _CompanyID,CompanyType _CompanyType,DateTime _CreatedTime,PendingStatus _Status,string _Description) 
        {
            this._ID = _ID;
            this._CompanyID = _CompanyID;
            this._CompanyType = _CompanyType;
            this._CreatedTime = _CreatedTime;
            this._Status = _Status;
            this._Description = _Description;
        }
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
        private DateTime _CreatedTime;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
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
    }
}
