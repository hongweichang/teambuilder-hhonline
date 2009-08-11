using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using HHOnline.Cache;
using System.Xml;
using System.Collections;

namespace HHOnline.Framework
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuManager
    {
        private MenuManager() { }
       /// <summary>
       /// 读取所有菜单
       /// </summary>
       /// <returns></returns>
        public static List<Menu> GetMenus()
        {
            string cacheKey = CacheKeyManager.MenuKey;
            List<Menu> menus = HHCache.Instance.Get(cacheKey) as List<Menu>;
            if (menus == null || menus.Count == 0)
            {
                string path = GlobalSettings.MapPath("~/ControlPanel/Masters/menu.config");
                FileDependency fd = new FileDependency(path);
                menus = new List<Menu>();
                Menu menu = null;
                List<MenuItem> items = null;
                MenuItem item = null;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                foreach (XmlNode xn in xmlDoc.SelectSingleNode("menus").ChildNodes)
                {
                    if (xn.NodeType == XmlNodeType.Element)
                    {
                        items=new List<MenuItem>();
                        foreach (XmlNode n in xn.ChildNodes)
                        {
                            if (n.NodeType == XmlNodeType.Element)
                            {
                                item = new MenuItem(n.Attributes["name"].Value,
                                                                xn.Attributes["name"].Value,
                                                                n.Attributes["itemTitle"].Value,
                                                                n.Attributes["url"].Value,
                                                                n.Attributes["roles"].Value);
                                items.Add(item);
                            }
                        }

                        menu = new Menu(xn.Attributes["name"].Value, xn.Attributes["menuTitle"].Value, xn.Attributes["roles"].Value, items);
                        menus.Add(menu);
                    }
                }
                HHCache.Instance.Max(cacheKey, menus, fd);
            }
            return menus;
        }
    }
}
