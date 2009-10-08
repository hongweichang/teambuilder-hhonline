using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// 公司信用
    /// </summary>
    public class CompanyCredit
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.CompanyCredit"/>的新实例
        /// </summary>
        public CompanyCredit()
        { }
        private int _CreditID;
        /// <summary>
        /// 编号
        /// </summary>
        public int CreditID
        {
            get { return _CreditID; }
            set { _CreditID = value; }
        }
        private int _SupplierID;
        /// <summary>
        /// 代理商编号
        /// </summary>
        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        private DateTime _CreditDate;
        /// <summary>
        /// 信用日期
        /// </summary>
        public DateTime CreditDate
        {
            get { return _CreditDate; }
            set { _CreditDate = value; }
        }
        private decimal _CreditDelta;
        /// <summary>
        /// 信用增量
        /// </summary>
        public decimal CreditDelta
        {
            get { return _CreditDelta; }
            set { _CreditDelta = value; }
        }
        private decimal _CreditAmount;
        /// <summary>
        /// 信用
        /// </summary>
        public decimal CreditAmount
        {
            get { return _CreditAmount; }
            set { _CreditAmount = value; }
        }
        private string _CreditDesc;
        /// <summary>
        /// 描述
        /// </summary>
        public string CreditDesc
        {
            get { return _CreditDesc; }
            set { _CreditDesc = value; }
        }
        private string _CreditMemo;
        /// <summary>
        /// 备注
        /// </summary>
        public string CreditMemo
        {
            get { return _CreditMemo; }
            set { _CreditMemo = value; }
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
        /// 创建者
        /// </summary>
        public int CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }


    }
}
