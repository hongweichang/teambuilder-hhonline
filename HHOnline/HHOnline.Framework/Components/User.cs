using System;

namespace HHOnline.Framework
{
    [Serializable]
    public class User : ExtendedAttributes
    {
        #region cntor
        public User()
        { }

        public User(string userName)
        {
            this.userName = userName;
        }
        #endregion

        #region Members
        private int userID;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string password;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private AccountStatus accountStatus = AccountStatus.ApprovalPending;
        /// <summary>
        /// 帐号状态
        /// </summary>
        public AccountStatus AccountStatus
        {
            get { return accountStatus; }
            set { accountStatus = value; }
        }
        private string displayName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        private string email;
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string mobile;
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        private string phone;
        /// <summary>
        /// 固话
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string fax;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        private string title;
        /// <summary>
        /// 职务
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string passwordQuestion;
        /// <summary>
        /// 密保问题
        /// </summary>
        public string PasswordQuestion
        {
            get { return passwordQuestion; }
            set { passwordQuestion = value; }
        }
        private string passwordAnswer;
        /// <summary>
        /// 密保答案
        /// </summary>
        public string PasswordAnswer
        {
            get { return passwordAnswer; }
            set { passwordAnswer = value; }
        }
        private string comment;
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        private DateTime lastLockonDate;
        /// <summary>
        /// 最近锁定时间
        /// </summary>
        public DateTime LastLockonDate
        {
            get { return lastLockonDate; }
            set { lastLockonDate = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }
        private int createdUser;
        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser
        {
            get { return createdUser; }
            set { createdUser = value; }
        }
        private DateTime updateTime;
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
        private int updateUser;
        /// <summary>
        /// 修改用户
        /// </summary>
        public int UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        private int isManager = 2;
        /// <summary>
        /// 是否领导
        /// </summary>
        public int IsManager
        {
            get { return isManager; }
            set { isManager = value; }
        }
        private UserType userType = UserType.CompanyUser;
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType
        {
            get { return userType; }
            set { userType = value; }
        }
        private int companyId;
        /// <summary>
        /// 企业ID
        /// </summary>
        public int CompanyID
        {
            get { return companyId; }
            set { companyId = value; }
        }
        private int organizationId;
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrganizationID
        {
            get { return organizationId; }
            set { organizationId = value; }
        }
        private DateTime lastActiveDate;
        /// <summary>
        /// 最近登录
        /// </summary>
        public DateTime LastActiveDate
        {
            get { return lastActiveDate; }
            set { lastActiveDate = value; }
        }

        /// <summary>
        /// 部门信息
        /// </summary>
        public string Department
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 公司信息
        /// </summary>
        public Company Company
        {
            get
            {
                if (userType == UserType.CompanyUser)
                    return Companys.GetCompany(companyId);
                return null;
            }
        }

        /// <summary>
        /// 组织机构信息
        /// </summary>
        public Organization Organization
        {
            get
            {
                if (userType == UserType.InnerUser)
                    return Organizations.GetOrganization(organizationId);
                return null;
            }
        }
        #endregion

    }
}
