using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using HHOnline.Cache;

namespace HHOnline.Framework
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class ResourceManager
    {
        #region ResourceManagerType
        enum ResourceManagerType
        {
            String,
            ErrorMessage
        }
        #endregion

        #region SupportedLanguages
        /// <summary>
        /// 获取支持语言
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetSupportedLanguages()
        {
            HHContext hhContext = HHContext.Current;

            string cacheKey =CacheKeyManager.ResourceKey;

            NameValueCollection supportedLanguages = HHCache.Instance.Get(cacheKey) as NameValueCollection;
            if (supportedLanguages == null)
            {
                string filePath = GlobalSettings.MapPath("~/Languages/languages.xml");
                FileDependency dp = new FileDependency(filePath);
                supportedLanguages = new NameValueCollection();

                XmlDocument d = new XmlDocument();
                d.Load(filePath);

                foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        supportedLanguages.Add(n.Attributes["key"].Value, n.Attributes["name"].Value);
                    }
                }

                HHCache.Instance.Max(cacheKey, supportedLanguages, dp);
            }

            return supportedLanguages;
        }

        /// <summary>
        /// 判断是否支持<paramref name="language"/>语言
        /// </summary>
        /// <param name="language"></param>
        /// <returns>支持：language 反之默认语言</returns>
        public static string GetSupportedLanguage(string language)
        {
            return GetSupportedLanguage(language, HHConfiguration.GetConfig().DefaultLanguage);
        }

        /// <summary>
        /// 判断是否支持<paramref name="language"/>语言
        /// </summary>
        /// <param name="language"></param>
        /// <param name="languageDefault"></param>
        /// <returns>支持：language 反之languageDefault</returns>
        public static string GetSupportedLanguage(string language, string languageDefault)
        {
            NameValueCollection supportedLanguages = GetSupportedLanguages();
            string supportedLanguage = supportedLanguages[language];
            if (!GlobalSettings.IsNullOrEmpty(supportedLanguage))
                return language;
            else
                return languageDefault;
        }
        #endregion

        #region GetString
        public static string GetString(string name)
        {
            return GetString(name, false);
        }

        public static string GetString(string name, bool defaultOnly)
        {
            return GetString(name, "Resources.xml", defaultOnly);
        }

        public static string GetString(string name, string fileName)
        {
            return GetString(name, fileName, false);
        }

        public static string GetString(string name, string fileName, bool defaultOnly)
        {
            Hashtable resources = null;
            string userLanguage = "zh-CN";
            if (fileName != null && fileName != "")
                resources = GetResource(ResourceManagerType.String, userLanguage, fileName, defaultOnly);
            else
                resources = GetResource(ResourceManagerType.String, userLanguage, "Resources.xml", defaultOnly);
            string text = resources[name] as string;
            if (text == null && fileName != null && fileName != "")
            {
                resources = GetResource(ResourceManagerType.String, userLanguage, "Resources.xml", true);
                text = resources[name] as string;
            }
            if (text == null)
            {
                text = string.Empty;
            }
            return text;
        }
        #endregion

        #region GetMessage
        public static Message GetMessage(int exceptionType)
        {
            Hashtable resources = GetResource(ResourceManagerType.ErrorMessage, "zh-CN", "Messages.xml", false);
            return (Message)resources[exceptionType];
        }
        #endregion

        #region GetResource
        private static Hashtable GetResource(ResourceManagerType resourceType, string userLanguage, string fileName, bool defaultOnly)
        {
            string defaultLanguage = HHConfiguration.GetConfig().DefaultLanguage;
            string cacheKey = "HHOnline/Framework/" + resourceType.ToString() + defaultLanguage + userLanguage + fileName;

            if (GlobalSettings.IsNullOrEmpty(userLanguage) || defaultOnly)
                userLanguage = defaultLanguage;

            Hashtable resources = HHCache.Instance.Get(cacheKey) as Hashtable;

            if (resources == null)
            {
                resources = new Hashtable();
                resources = LoadResource(resourceType, resources, defaultLanguage, cacheKey, fileName);
                if (defaultLanguage != userLanguage)
                    resources = LoadResource(resourceType, resources, userLanguage, cacheKey, fileName);
            }
            return resources;
        }

        private static Hashtable LoadResource(ResourceManagerType resourceType, Hashtable target, string language, string cacheKey, string fileName)
        {
            string filePath = GlobalSettings.PhysicalPath("Languages\\" + language + "\\" + fileName);

            FileDependency dp;
            XmlDocument d = new XmlDocument();
            try
            {
                dp = new FileDependency(filePath);
                d.Load(filePath);
            }
            catch
            {
                return target;
            }
            foreach (XmlNode n in d.SelectSingleNode("root").ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    switch (resourceType)
                    {
                        case ResourceManagerType.ErrorMessage:
                            Message m = new Message(n);
                            target[m.MessageID] = m;
                            break;

                        case ResourceManagerType.String:
                            if (target[n.Attributes["name"].Value] == null)
                                target.Add(n.Attributes["name"].Value, n.InnerText);
                            else
                                target[n.Attributes["name"].Value] = n.InnerText;
                            break;
                    }
                }
            }
            HHCache.Instance.Max(cacheKey, target, dp);
            return target;
        }
        #endregion
    }
}
