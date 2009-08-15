using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HHOnline.Framework
{
    /// <summary>
    /// 站点设置
    /// </summary>
    [Serializable]
    public class SiteSettings
    {
        #region 私有变量
        private Guid siteKey;
        private int settingsID;
        //基础设置
        private string defaultSiteName = "华宏在线";
        private string defaultSiteDescription = "华宏在线商务平台";
        private string defaultDateFormat = "yyyy-MM-dd";
        private string defaultTimeFormat = "HH:mm";
        private string searchMetaDescription = "华宏在线";
        private string searchMetaKeywords = "华宏在线";
        private string copyright = "CopyRight &copy; EHuaHo 2009";
        private string termsOfServiceUrl = "";
        private string rawAdditionalHeader = "";
        private string googleAnalytics = "";
        private string companyIdea = "";
        private string companyICP = "";
        private string companyService = "";
        private string serviceTel = "";
        private string showPicture = "";

        //邮件设置
        private bool defaultEnableEmail = true;
        private string defaultAdminEmailAddress = "";
        private string smtpServer = "";
        private bool smtpServerUsingNtlm = false;
        private bool smtpServerUsingSsl = false;
        private bool smtpServerRequiredLogin = false;
        private string smtpServerUserName = "";
        private string smtpServerPassword = "";
        private int smtpPortNumber = 25;
        private int emailThrottle = -1;
        private List<ShowPicture> showPictures = new List<ShowPicture>();

        //皮肤
        private string skinId = "Default";

        #endregion

        #region 全局设置
        /// <summary>
        /// 网站皮肤
        /// </summary>
        public string SkinId
        {
            get { return skinId; }
            set { skinId = value; }
        }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get { return defaultSiteName; }
            set { defaultSiteName = value; }
        }

        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDescription
        {
            get { return defaultSiteDescription; }
            set { defaultSiteDescription = value; }
        }

        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat
        {
            get { return defaultDateFormat; }
            set { defaultDateFormat = value; }
        }

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat
        {
            get { return defaultTimeFormat; }
            set { defaultTimeFormat = value; }
        }

        /// <summary>
        /// 页面头信息描述
        /// </summary>
        public string SearchMetaDescription
        {
            get { return searchMetaDescription; }
            set { searchMetaDescription = value; }
        }

        /// <summary>
        /// 页面头信息关键字
        /// </summary>
        public string SearchMetaKeywords
        {
            get { return searchMetaKeywords; }
            set { searchMetaKeywords = value; }
        }

        /// <summary>
        /// 版权信息
        /// </summary>
        public string Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }

        /// <summary>
        /// 备案信息
        /// </summary>
        public string CompanyICP
        {
            get { return companyICP; }
            set { companyICP = value; }
        }

        /// <summary>
        /// 服务条款
        /// </summary>
        public string TermsOfServiceUrl
        {
            get { return termsOfServiceUrl; }
            set { termsOfServiceUrl = value; }
        }

        /// <summary>
        /// HTTP头信息
        /// </summary>
        public string RawAdditionalHeader
        {
            get { return rawAdditionalHeader; }
            set { rawAdditionalHeader = value; }
        }

        /// <summary>
        /// 公司理念
        /// </summary>
        public string CompanyIdea
        {
            get { return companyIdea; }
            set { companyIdea = value; }
        }

        /// <summary>
        /// 公司服务
        /// </summary>
        public string CompanyService
        {
            get { return companyService; }
            set { companyService = value; }
        }

        /// <summary>
        /// 客服电话
        /// </summary>
        public string ServiceTel
        {
            get { return serviceTel; }
            set { serviceTel = value; }
        }

        /// <summary>
        /// 展示图片
        /// </summary>
        public List<ShowPicture> ShowPictures
        {
            get
            {
                return showPictures;
            }
            set
            {
                showPictures = value;
            }
        }

        /// <summary>
        /// 展示图片
        /// </summary>
        public string ShowPicture
        {
            get
            {
                return showPicture;
            }
            set
            {
                showPicture = value;
            }
        }

        /// <summary>
        /// 统计脚本
        /// </summary>
        public string GoogleAnalytics
        {
            get { return googleAnalytics; }
            set { googleAnalytics = value; }
        }
        #endregion

        #region 邮件设置
        public string AdminEmailAddress
        {
            get { return defaultAdminEmailAddress; }
            set { defaultAdminEmailAddress = value; }
        }

        /// <summary>
        /// 启用邮件设置
        /// </summary>
        public bool EnaleEmail
        {
            get { return defaultEnableEmail; }
            set { defaultEnableEmail = value; }
        }

        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string SmtpServer
        {
            get { return smtpServer; }
            set { smtpServer = value; }
        }

        /// <summary>
        /// 每次邮件队列最大数量
        /// </summary>
        public int EmailThrottle
        {
            get
            {
                return emailThrottle;
            }
            set
            {
                emailThrottle = value;
            }
        }

        /// <summary>
        /// 使用 Windows 集成身份认证
        /// </summary>
        public bool SmtpServerUsingNtlm
        {
            get { return smtpServerUsingNtlm; }
            set { smtpServerUsingNtlm = value; }
        }

        /// <summary>
        /// SMTP上启用SSL
        /// </summary>
        public bool SmtpServerUsingSsl
        {
            get { return smtpServerUsingSsl; }
            set { smtpServerUsingSsl = value; }
        }

        /// <summary>
        /// SMTP需登录
        /// </summary>
        public bool SmtpServerRequiredLogin
        {
            get { return smtpServerRequiredLogin; }
            set { smtpServerRequiredLogin = value; }
        }

        /// <summary>
        /// SMTP端口号
        /// </summary>
        public int SmtpPortNumber
        {
            get { return smtpPortNumber; }
            set { smtpPortNumber = value; }
        }

        /// <summary>
        /// SMTP用户名
        /// </summary>
        public string SmtpServerUserName
        {
            get { return smtpServerUserName; }
            set { smtpServerUserName = value; }
        }

        /// <summary>
        /// SMTP密码
        /// </summary>
        public string SmtpServerPassword
        {
            get { return smtpServerPassword; }
            set { smtpServerPassword = value; }
        }
        #endregion

        #region XMLIgnore
        [XmlIgnore]
        public int SettingsID
        {
            get
            {
                return settingsID;
            }
            set
            {
                settingsID = value;
            }
        }
        [XmlIgnore]
        public Guid SiteKey
        {
            get { return siteKey; }
            set { siteKey = value; }
        }
        #endregion
    }
}
