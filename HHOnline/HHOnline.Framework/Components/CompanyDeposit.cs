using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 公司保证金
    /// </summary>
    public class CompanyDeposit
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.CompanyDeposit"/>的新实例
        /// </summary>
        public CompanyDeposit()
        { }
        private int _DepositID;
        /// <summary>
        /// 编号
        /// </summary>
        public int DepositID
        {
            get { return _DepositID; }
            set { _DepositID = value; }
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
        private DepositType _DepositType;
        /// <summary>
        /// 保证金类型
        /// </summary>
        public DepositType DepositType
        {
            get { return _DepositType; }
            set { _DepositType = value; }
        }
        private DateTime _DepositDate;
        /// <summary>
        /// 缴纳日期
        /// </summary>
        public DateTime DepositDate
        {
            get { return _DepositDate; }
            set { _DepositDate = value; }
        }
        private decimal _DepositDelta;
        /// <summary>
        /// 增量金额
        /// </summary>
        public decimal DepositDelta
        {
            get { return _DepositDelta; }
            set { _DepositDelta = value; }
        }
        private decimal _DepositAmount;
        /// <summary>
        /// 保证金额
        /// </summary>
        public decimal DepositAmount
        {
            get { return _DepositAmount; }
            set { _DepositAmount = value; }
        }
        private string _DepositDesc;
        /// <summary>
        /// 保证金增量说明
        /// </summary>
        public string DepositDesc
        {
            get { return _DepositDesc; }
            set { _DepositDesc = value; }
        }
        private string _DepositMemo;
        /// <summary>
        /// 保证金增量备注
        /// </summary>
        public string DepositMemo
        {
            get { return _DepositMemo; }
            set { _DepositMemo = value; }
        }
        private DateTime _CreateTime;
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private int _CreateUser;
        /// <summary>
        /// 记录人
        /// </summary>
        public int CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }

    }
}
