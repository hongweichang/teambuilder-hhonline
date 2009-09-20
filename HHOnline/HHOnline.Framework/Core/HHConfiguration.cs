using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Serialization;
using HHOnline.Cache;
using System.Collections.Generic;

namespace HHOnline.Framework
{
    public class HHConfiguration
    {
        private XmlDocument XmlDoc = null;
        private static readonly object configLocker = new object();

        #region cnstr
        public HHConfiguration(XmlDocument doc)
        {
            XmlDoc = doc;
            LoadValuesFromConfigurationXml();
        }

        public XmlNode GetConfigSection(string nodePath)
        {
            return XmlDoc.SelectSingleNode(nodePath);
        }

        public static HHConfiguration GetConfig()
        {
            HHConfiguration config = HHCache.Instance.Get(CacheKeyManager.HHConfigurationKey) as HHConfiguration;
            if (config == null)
            {
                lock (configLocker)
                {
                    config = HHCache.Instance.Get(CacheKeyManager.HHConfigurationKey) as HHConfiguration;
                    if (config == null)
                    {
                        string path = HHConfiguration.ConfigFilePath;//GlobalSettings.MapPath("~/HHOnline.config");
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        config = new HHConfiguration(doc);
                        //更新缓存因子
                        HHCache.Instance.ResetFactor(config.CacheFactor);
                        //添加到缓存
                        FileDependency dp = new FileDependency(path);
                        HHCache.Instance.Max(CacheKeyManager.HHConfigurationKey, config, dp);
                    }
                }
            }
            return config;
        }

        internal void LoadValuesFromConfigurationXml()
        {
            XmlNode node = GetConfigSection("HHOnline/Core");
            XmlAttributeCollection attributeCollection = node.Attributes;
            for (int i = 0; i < attributeCollection.Count; i++)
            {
                attr.Add(attributeCollection[i].Name, attributeCollection[i].Value);
            }
        }
        #endregion

        private Dictionary<string, object> attr = new Dictionary<string, object>();
        /// <summary>
        /// 根据配置名称返回配置值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                if (attr.ContainsKey(name))
                    return attr[name];
                else
                    return string.Empty;
            }
        }

        #region 默认语言
        private bool defaultLanguageHasBeenValidated = false;
        private string defaultLanguage = "zh-CN";
        public string DefaultLanguage
        {
            get
            {
                if (!defaultLanguageHasBeenValidated)
                    defaultLanguage = ResourceManager.GetSupportedLanguage(defaultLanguage, "zh-CN");

                return defaultLanguage;
            }
        }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigFilePath
        {
            get
            {
                return GlobalSettings.MapPath("~/HHOnline.config");
            }
        }

        public int CacheFactor
        {
            get
            {
                if (attr.ContainsKey("cacheFactor"))
                    return Convert.ToInt32(this["cacheFactor"]);
                else
                    return 5;
            }
        }
        #endregion
    }
}
