using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HHOnline.Framework.Web.UrlRewrite
{
    /// <summary>
    /// 地址重写模块
    /// </summary>
    public class UrlRewriteSection
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Web.UrlRewrite.UrlRewriteSection"/>的新实例
        /// </summary>
        public UrlRewriteSection(){}
        /// <summary>
        ///  创建<see cref="HHOnline.Framework.Web.UrlRewrite.UrlRewriteSection"/>的新实例
        /// </summary>
        /// <param name="node">由此节点读取配置</param>
        public UrlRewriteSection(XmlNode node)
        {
            XmlElement c = (XmlElement)node;
            _ignoreCase = Convert.ToBoolean(c.Attributes["ignoreCase"].Value);
            _name = c.Attributes["name"].Value;
            _virtualUrl = c.Attributes["virtualUrl"].Value;
            _destinationUrl = c.Attributes["destinationUrl"].Value;
        }
       
        private string _name;
        /// <summary>
        /// Section名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _virtualUrl;
        /// <summary>
        /// 重写后的Url
        /// </summary>
        public string VirtualUrl
        {
            get { return _virtualUrl; }
            set { _virtualUrl = value; }
        }
        private string _destinationUrl;
        /// <summary>
        /// 原始Url
        /// </summary>
        public string DestinationUrl
        {
            get { return _destinationUrl; }
            set { _destinationUrl = value; }
        }
        private bool _ignoreCase;
        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IgnoreCase
        {
            get { return _ignoreCase; }
            set { _ignoreCase = value; }
        }
    }
}
