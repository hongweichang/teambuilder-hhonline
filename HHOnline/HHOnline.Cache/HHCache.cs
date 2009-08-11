using System;
using System.Xml;
using System.Collections.Generic;

namespace HHOnline.Cache
{
    /// <summary>
    /// Cache封装类，使用XML组织关键字
    /// </summary>
    public class HHCache
    {
        #region Private Member
        private XmlElement objectXmlMap;
        private static ICacheStrategy strategy;
        private static HHCache instance;
        private static object lockObject = new object();
        private XmlDocument rootXml = new XmlDocument();
        #endregion

        #region .cntor
        private void Initialize()
        {
            rootXml.RemoveAll();
            objectXmlMap = rootXml.CreateElement("Cache");
            rootXml.AppendChild(objectXmlMap);
        }

        private HHCache()
        {
            strategy = new DefaultCacheStrategy();
            Initialize();
        }

        public static HHCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                            instance = new HHCache();
                    }
                }
                return instance;
            }
        }
        #endregion

        #region CacheFactor
        public void ResetFactor(int factor)
        {
            strategy.ResetFactor(factor);
        }
        #endregion

        #region Insert
        public virtual void Insert(string key, object obj)
        {
            strategy.Insert(CreateNodeMap(key), obj);
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="multiple">缓存时间，实际值为 multiple*factor 分钟，factor默认值为5分钟</param>
        public virtual void Insert(string key, object obj, double multiple)
        {
            strategy.Insert(CreateNodeMap(key), obj, multiple);
        }

        public virtual void Insert(string key, object obj, params ICacheItemExpiration[] deps)
        {
            strategy.Insert(CreateNodeMap(key), obj, deps);
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="multiple">缓存时间，实际值为 multiple*factor 分钟，factor默认值为5分钟</param>
        /// <param name="priority"></param>
        public virtual void Insert(string key, object obj, double multiple, CacheItemPriority priority)
        {
            strategy.Insert(CreateNodeMap(key), obj, multiple, priority);
        }

        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="prioprity"></param>
        /// <param name="deps"></param>
        public virtual void Insert(string key, object obj, CacheItemPriority prioprity, params ICacheItemExpiration[] deps)
        {
            strategy.Insert(CreateNodeMap(key), obj, prioprity, deps);
        }

        /// <summary>
        /// 缓存对象永久不过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public virtual void Max(string key, object obj)
        {
            strategy.Max(CreateNodeMap(key), obj);
        }

        /// <summary>
        /// 缓存对象永久不过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="deps"></param>
        public virtual void Max(string key, object obj, params ICacheItemExpiration[] deps)
        {
            strategy.Max(CreateNodeMap(key), obj, deps);
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public virtual object Get(string key)
        {
            object o = null;
            XmlNode node = objectXmlMap.SelectSingleNode(PrepareXpath(key));
            if (node != null)
            {
                if (node.Attributes["objectID"] != null)
                {
                    string objectId = node.Attributes["objectId"].Value;
                    o = strategy.Get(objectId);
                }
            }
            return o;
        }

        /// <summary>
        /// 获取Key其下对象数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int GetCount(string key)
        {
            XmlNode group = objectXmlMap.SelectSingleNode(PrepareXpath(key));
            if (group != null)
            {
                XmlNodeList results = group.SelectNodes(PrepareXpath(key) + "//*[@objectId]");
                return results.Count == 0 ? 1 : results.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取Key其下所有对象
        /// </summary>
        /// <param name="key">层次位置</param>
        /// <returns>所有对象</returns>
        public virtual object[] GetList(string key)
        {
            XmlNode group = objectXmlMap.SelectSingleNode(PrepareXpath(key));
            if (group != null)
            {
                XmlNodeList results = group.SelectNodes(PrepareXpath(key) + "/*[@objectId]");
                List<object> objects = new List<object>();
                string objectId = null;
                foreach (XmlNode result in results)
                {
                    objectId = result.Attributes["objectId"].Value;
                    objects.Add(strategy.Get(objectId));
                }
                return objects.ToArray();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Remove
        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key"></param>
        public virtual void Remove(string key)
        {
            XmlNode result = objectXmlMap.SelectSingleNode(PrepareXpath(key));
            Remove(result);
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="result"></param>
        public virtual void Remove(XmlNode result)
        {
            if (result != null)
            {
                if (result.HasChildNodes)
                {
                    XmlNodeList objects = result.SelectNodes("*[@objectId]");
                    string objectId = "";
                    foreach (XmlNode node in objects)
                    {
                        objectId = node.Attributes["objectId"].Value;
                        node.ParentNode.RemoveChild(node);
                        strategy.Remove(objectId);
                    }
                }
                else
                {
                    if (result.Attributes["objectId"] != null)
                    {
                        string objectId = result.Attributes["objectId"].Value;
                        result.ParentNode.RemoveChild(result);
                        strategy.Remove(objectId);
                    }
                }
            }
        }

        /// <summary>
        /// 使用XPath移除对象
        /// </summary>
        /// <param name="key"></param>
        public virtual void RemoveByPattern(string key)
        {
            string xpath = key;
            if (key.StartsWith("/"))
                xpath = "/Cache" + key;
            XmlNodeList results = objectXmlMap.SelectNodes(xpath);
            if (results != null)
            {
                foreach (XmlNode result in results)
                {
                    Remove(result);
                }
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public virtual void Clear()
        {
            Initialize();
            strategy.Clear();
        }
        #endregion

        #region Helper Method
        private string CreateNodeMap(string key)
        {
            string newXpath = PrepareXpath(key);
            XmlNode node = objectXmlMap.SelectSingleNode(newXpath);
            if (node != null)
            {
                if (node.Attributes["objectId"] == null)
                {
                    string objectId = System.Guid.NewGuid().ToString();
                    XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                    objectAttribute.Value = objectId;
                    node.Attributes.Append(objectAttribute);
                    return objectId;
                }
                else
                {
                    return node.Attributes["objectId"].Value;
                }
            }
            else
            {
                int separator = newXpath.LastIndexOf("/");
                string group = newXpath.Substring(0, separator);
                string element = newXpath.Substring(separator + 1);
                XmlNode groupNode = objectXmlMap.SelectSingleNode(group);
                if (groupNode == null)
                {
                    lock (lockObject)
                    {
                        groupNode = CreateNode(group);
                    }
                }
                string objectId = System.Guid.NewGuid().ToString();
                XmlElement objectElement = objectXmlMap.OwnerDocument.CreateElement(element);
                XmlAttribute objectAttribute = objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                objectAttribute.Value = objectId;
                objectElement.Attributes.Append(objectAttribute);
                groupNode.AppendChild(objectElement);
                return objectId;
            }
        }

        private XmlNode CreateNode(string xpath)
        {
            string[] xpathArray = xpath.Split('/');
            string root = "";
            XmlNode parentNode = (XmlNode)objectXmlMap;
            for (int i = 1; i < xpathArray.Length; i++)
            {
                XmlNode node = objectXmlMap.SelectSingleNode(root + "/" + xpathArray[i]);
                if (node == null)
                {
                    XmlElement newElement = objectXmlMap.OwnerDocument.CreateElement(xpathArray[i]);
                    parentNode.AppendChild(newElement);
                }
                root = root + "/" + xpathArray[i];
                parentNode = objectXmlMap.SelectSingleNode(root);
            }
            return parentNode;
        }

        private string PrepareXpath(string xpath)
        {
            string[] xpathArray = xpath.Split('/');
            xpath = "/Cache";
            foreach (string s in xpathArray)
            {
                if (s != "")
                {
                    xpath = xpath + "/" + s;
                }
            }
            return xpath;
        }
        #endregion

        #region Get DOM
        public System.IO.Stream GetCacheKeyXml()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            rootXml.Save(ms);
            ms.Position = 0;
            return ms;
        }
        #endregion
    }
}
