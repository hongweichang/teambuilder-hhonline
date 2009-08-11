using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;
using System.Configuration;

namespace HHOnline.Framework.Web.UrlRewrite
{
    /// <summary>
    /// 地址重写配置模块
    /// </summary>
    public sealed class UrlRewriteConfig
    {
        /// <summary>
        /// 获取地址重写规则
        /// </summary>
        /// <returns></returns>
        public static List<UrlRewriteSection> LoadSections()
        {
            return (List<UrlRewriteSection>)ConfigurationManager.GetSection("urlRewrite");
        }
    }
}
