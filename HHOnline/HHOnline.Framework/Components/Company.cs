using System;

namespace HHOnline.Framework
{
    [Serializable]
    public class Company : ExtendedAttributes
    {
        private CompanyType companyType = CompanyType.Ordinary;
        private CompanyStauts companyStatus = CompanyStauts.ApprovalPending;
        private int createdUser;
        private DateTime createdDate;
        private int updateUser;
        private DateTime updateTime;

        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyType CompanyType
        {
            get { return companyType; }
            set { companyType = value; }
        }

        /// <summary>
        /// CompanyID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public int CompanyRegion { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string Zipcode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 公司主页
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 公司组织机构代码
        /// </summary>
        public string Orgcode { get; set; }

        /// <summary>
        /// 公司工商注册登记证号
        /// </summary>
        public string Regcode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CompanyStauts CompanyStatus
        {
            get
            {
                return companyStatus;
            }
            set
            {
                companyStatus = value;
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser
        {
            get { return createdUser; }
            set { createdUser = value; }
        }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public int UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
    }
}