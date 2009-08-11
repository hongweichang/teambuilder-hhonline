using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HHOnline.Framework
{
    public class StrategyFactory
    {
        private static Dictionary<string, Type> gradeStrategies = null;

        private static List<KeyValue> strategies = null;

        private StrategyFactory()
        { }

        static StrategyFactory()
        {
            gradeStrategies = new Dictionary<string, Type>();
            strategies = new List<KeyValue>();
            XmlNode node = HHConfiguration.GetConfig().GetConfigSection("HHOnline/GradeStrategy");
            if (node != null)
            {
                string itemName = string.Empty;
                string itemText = string.Empty;
                string itemType = string.Empty;
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
                            t = Type.GetType(itemType);
                            if (t == null)
                                throw new Exception(itemType);
                            gradeStrategies.Add(itemName, t);
                            strategies.Add(new KeyValue(itemName, itemText));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 根据名称获取策略类
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IGradeStrategy GetGradeStrategy(string key)
        {
            Type type = gradeStrategies[key];
            if (type == null)
                throw new ArgumentException(key + "无效");
            IGradeStrategy strategy = Activator.CreateInstance(type) as IGradeStrategy;
            strategy.Name = key;
            return strategy;
        }

        /// <summary>
        /// 获取所有策略
        /// </summary>
        /// <returns></returns>
        public static List<KeyValue> GetStrategies()
        {
            return strategies;
        }
    }
}
