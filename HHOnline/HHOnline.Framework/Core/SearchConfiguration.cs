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
    /// <summary>
    /// 查询配置
    /// </summary>
    public class SearchConfiguration
    {
        private string dictionaryDirectory = "~/Utility/IndexDictionary/";
        private string globalIndexDirectory = "~/IndexFiles/";
        private int maxMergeDocs = Int32.MaxValue;
        private int mergeFactor = 10;
        private int minMergeDocs = 100;
        private Dictionary<string, BaseSearchSetting> searchSettings = new Dictionary<string, BaseSearchSetting>();
        private static readonly object configLocker = new object();
        private XmlNode xmlNode = null;

        private SearchConfiguration(XmlNode node)
        {
            xmlNode = node;
            LoadValuesFromConfigurationXml();
        }

        /// <summary>
        /// 获取缓存配置实例
        /// </summary>
        /// <returns></returns>
        public static SearchConfiguration GetConfig()
        {
            SearchConfiguration config = HHCache.Instance.Get(CacheKeyManager.SearchConfigurationKey) as SearchConfiguration;
            if (config == null)
            {
                lock (configLocker)
                {
                    config = HHCache.Instance.Get(CacheKeyManager.SearchConfigurationKey) as SearchConfiguration;
                    if (config == null)
                    {
                        XmlNode node = HHConfiguration.GetConfig().GetConfigSection("HHOnline/LuceneSearch");
                        config = new SearchConfiguration(node);
                        //添加到缓存
                        FileDependency dp = new FileDependency(HHConfiguration.ConfigFilePath);
                        HHCache.Instance.Max(CacheKeyManager.SearchConfigurationKey, config, dp);
                    }
                }
            }
            return config;
        }


        /// <summary>
        /// 加载值
        /// </summary>
        private void LoadValuesFromConfigurationXml()
        {
            if (xmlNode != null)
            {
                XmlAttributeCollection attributeCollection = xmlNode.Attributes;
                XmlAttribute attribute = attributeCollection["globalIndexDirectory"];
                if (attribute != null)
                {
                    globalIndexDirectory = attribute.Value;
                }
                attribute = attributeCollection["maxMergeDocs"];
                if (attribute != null)
                {
                    Int32.TryParse(attribute.Value, out maxMergeDocs);
                }
                attribute = attributeCollection["minMergeDocs"];
                if (attribute != null)
                {
                    Int32.TryParse(attribute.Value, out minMergeDocs);
                }
                attribute = attributeCollection["mergeFactor"];
                if (attribute != null)
                {
                    Int32.TryParse(attribute.Value, out mergeFactor);
                }
                foreach (XmlNode child in xmlNode.ChildNodes)
                {
                    if (child.Name == "settings")
                        GetSearchSettings(child);
                }
            }
        }

        /// <summary>
        /// 获取查询设置
        /// </summary>
        /// <param name="node"></param>
        private void GetSearchSettings(XmlNode node)
        {
            if (node != null)
            {
                string itemName = string.Empty;
                string itemText = string.Empty;
                string itemType = string.Empty;
                string indexFileDirectory = string.Empty;
                BaseSearchSetting setting = null;
                Type t = null;
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        if (n.Name == "add")
                        {
                            itemName = n.Attributes["name"].Value;
                            itemText = n.Attributes["text"].Value;
                            itemType = n.Attributes["type"].Value;
                            if (n.Attributes["indexFileDirectory"] != null)
                                indexFileDirectory = n.Attributes["indexFileDirectory"].Value;
                            t = Type.GetType(itemType);
                            if (t == null)
                                throw new HHException(ExceptionType.TypeNotFount, itemType + "未找到", new TypeInitializationException(t.ToString(), null));
                            setting = Activator.CreateInstance(t) as BaseSearchSetting;
                            if (setting == null)
                                throw new HHException(ExceptionType.TypeInitFail, itemType + "初始化异常", new TypeInitializationException(t.ToString(), null));
                            setting.IndexFileDirectory = indexFileDirectory;
                            setting.SearchName = itemText;
                            searchSettings.Add(itemName, setting);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 分词字典目录
        /// </summary>
        public string DictionaryDirectory
        {
            get
            {
                return dictionaryDirectory;
            }
        }

        /// <summary>
        /// 全局索引目录
        /// </summary>
        public string GlobalIndexDirectory
        {
            get
            {
                return globalIndexDirectory;
            }
        }

        /// <summary>
        /// 内存索引最大个数
        /// </summary>
        public int MaxMergeDocs
        {
            get
            {
                return maxMergeDocs;
            }
        }


        /// <summary>
        /// 内存索引最小个数
        /// </summary>
        public int MinMergeDocs
        {
            get
            {
                return minMergeDocs;
            }
        }

        /// <summary>
        /// 索引合并因子
        /// </summary>
        public int MergeFactor
        {
            get
            {
                return mergeFactor;
            }
        }

        /// <summary>
        /// 查询设置
        /// </summary>
        public Dictionary<string, BaseSearchSetting> SearchSettings
        {
            get
            {
                return searchSettings;
            }
        }
    }
}
