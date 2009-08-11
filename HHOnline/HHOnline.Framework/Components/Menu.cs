using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HHOnline.Framework
{
    /// <summary>
    /// 母菜单
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 创建<see cref=" HHOnline.Framework.Menu"/>的新实例
        /// </summary>
        public Menu()
            : this(string.Empty, string.Empty, null, null)
        {
        }
        /// <summary>
        /// 创建<see cref=" HHOnline.Framework.Menu"/>的新实例
        /// </summary>
        /// <param name="_name">唯一别名</param>
        /// <param name="_menuTitle">母菜单标题</param>
        /// <param name="_roles">母菜单角色列表</param>
        /// <param name="_menuItems">子菜单列表</param>
        public Menu(string _name, string _menuTitle, string _roles, List<MenuItem> _menuItems)
        {
            this._name = _name;
            this._menuTitle = _menuTitle;
            this._roles = _roles;
            this._menuItems = _menuItems;
        }
        private string _name;
        /// <summary>
        /// 唯一别名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _menuTitle;
        /// <summary>
        /// 母菜单标题
        /// </summary>
        public string MenuTitle
        {
            get { return _menuTitle; }
            set { _menuTitle = value; }
        }
        private string _roles;
        /// <summary>
        /// 母菜单角色列表
        /// </summary>
        public string Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        private List<MenuItem> _menuItems;
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; }
        }
    }
    /// <summary>
    /// 子菜单
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 创建<see cref="HHOnline.Framework"/>的新实例
        /// </summary>
        public MenuItem()
            : this(string.Empty, string.Empty, string.Empty, string.Empty, null)
        { }
        /// <summary>
        /// 创建<see cref="HHOnline.Framework"/>的新实例
        /// </summary>
        /// <param name="_name">唯一别名</param>
        /// <param name="_parentName">母菜单唯一别名</param>
        /// <param name="_itemTitle">子菜单标题</param>
        /// <param name="_url">页面Url(相对于网站根目录)</param>
        /// <param name="_roles">子菜单角色</param>
        public MenuItem(string _name,string _parentName, string _itemTitle,string _url,string _roles)
        {
            this._parentName = _parentName;
            this._name = _name;
            this._itemTitle = _itemTitle;
            this._url = _url;
            this._roles = _roles;
        }
        private string _name;
        /// <summary>
        /// 唯一别名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _parentName;
        /// <summary>
        /// 母菜单唯一别名
        /// </summary>
        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; }
        }
        private string _itemTitle;
        /// <summary>
        /// 子菜单标题 
        /// </summary>
        public string ItemTitle
        {
            get { return _itemTitle; }
            set { _itemTitle = value; }
        }
        private string _url;
        /// <summary>
        /// 页面Url(相对于网站根目录)
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _roles;
        /// <summary>
        /// 子菜单角色
        /// </summary>
        public string Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
    }
}
