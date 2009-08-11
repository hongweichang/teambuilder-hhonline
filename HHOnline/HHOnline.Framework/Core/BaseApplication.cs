using System;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using HHOnline.Common;
using HHOnline.Cache;

namespace HHOnline.Framework
{
    /// <summary>
    /// 事件管理基类
    /// </summary>
    public abstract class BaseApplication
    {
        #region --Private Members--
        protected readonly EventHandlerList _events = new EventHandlerList();

        protected readonly Hashtable _modules = new Hashtable();

        protected static readonly object _mutex = new object();
        #endregion

        #region --Load/InitModule--
        /// <summary>
        /// 加载Module
        /// </summary>
        /// <param name="sectionNode"></param>
        protected void LoadModules(XmlNode sectionNode)
        {
            try
            {
                if (sectionNode == null)
                    return;
                foreach (XmlNode node in sectionNode.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Comment)
                        continue;

                    switch (node.Name)
                    {
                        case "clear":
                            _modules.Clear();
                            break;

                        case "remove":

                            XmlAttribute removeNameAtt = node.Attributes["name"];
                            string removeName = removeNameAtt == null ? null : removeNameAtt.Value;

                            if (!String.IsNullOrEmpty(removeName) && _modules.ContainsKey(removeName))
                                _modules.Remove(removeName);
                            break;
                        case "add":
                            XmlAttribute enabledAtt = node.Attributes["enabled"];
                            if (enabledAtt == null || enabledAtt.Value != "false")
                                LoadModule(node);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                new HHException(ExceptionType.ModuleNotFount,
                  string.Format("HHModule Load Fail .Exception Type {0}  occurred: {1}", ex.GetType().Name, ex.Message)).Log();
                return;
            }
        }

        private void LoadModule(XmlNode node)
        {
            string moduleName = null;
            string typeName = null;

            try
            {
                XmlAttribute nameAtt = node.Attributes["name"];
                XmlAttribute typeAtt = node.Attributes["type"];

                moduleName = nameAtt == null ? null : nameAtt.Value;
                typeName = typeAtt == null ? null : typeAtt.Value;

                if (String.IsNullOrEmpty(moduleName))
                {
                    new HHException(ExceptionType.ModuleNotFount, string.Format("HHModule Load Fail.Type:{0}", typeName)).Log();
                    return;
                }

                if (String.IsNullOrEmpty(typeName))
                {
                    new HHException(ExceptionType.ModuleNotFount, string.Format("HHModule Load Fail.ModuleName:{0}", moduleName)).Log();
                    return;
                }

                if (_modules.Contains(moduleName))
                {
                    new HHException(ExceptionType.ModuleNotFount, string.Format("HHModule Load Fail.ModuleName({0}) Has Exists", moduleName)).Log();
                    return;
                }

                Type type = Type.GetType(typeName);

                if (type == null)
                {
                    new HHException(ExceptionType.ModuleNotFount, string.Format("HHModule Load Fail.Type {0} Not Found", typeName)).Log();
                    return;
                }
                var module = InitModule(type, node);

                if (module == null)
                {
                    new HHException(ExceptionType.ModuleNotFount, string.Format("HHModule Load Found.Type {0} Init Fail", typeName)).Log();
                    return;
                }

                _modules.Add(moduleName, module);
            }
            catch (Exception ex)
            {
                new HHException(ExceptionType.ModuleNotFount,
                    string.Format("HHModule({0}) Load Fail .Type {1}  occurred: {2}", moduleName, typeName, ex.Message)).Log();
                return;
            }
        }

        /// <summary>
        /// 初始化Module
        /// </summary>
        /// <param name="moduleType">ModuleType</param>
        /// <param name="node">XMLNode</param>
        /// <returns>Module</returns>
        public abstract object InitModule(Type moduleType, XmlNode node);
        #endregion
    }
}
