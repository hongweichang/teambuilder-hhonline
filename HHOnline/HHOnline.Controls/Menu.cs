using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using HF = HHOnline.Framework;
using System.Web.UI.WebControls;
using System.IO;
using HHOnline.Framework.Web.Pages;
using HHOnline.Framework;

/**
 * *by Jericho
 * */
namespace HHOnline.Controls
{
    internal class MenuTemplate : ITemplate
    {
        public MenuTemplate() { }
        public MenuTemplate(Control child)
        {
            this.child = child;
        }
        private Control child;
        public void InstantiateIn(Control container)
        {
            container.Controls.Add(child);
        }
    }
    /// <summary>
    /// 菜单控件
    /// </summary>
    public class Menu:UserControl
    {
        static Menu() { }
        private static readonly string menu = "<ul class=\"menu-content\">{0}</ul>";
        private static readonly string items = "<li><a href=\"{1}\"  id=\"item_{2}\" parentId=\"menu_{3}\" onclick=\"menu.adaptItem(this)\" onfocus=\"this.blur()\">{0}</a></li>";
        private static readonly string head = "<dt class=\"menu-head\"onclick=\"menu.adaptMenu(this)\"><a href=\"javascript:void(0)\" onfocus=\"this.blur()\">{0}</a></dt>";
        private static readonly string cpMenu = "<dl class=\"cp-menu\" id=\"menu_{2}\">"+
                                                                        "{0}"+ // head goes here
                                                                        "<dd class=\"menu-body\">{1}</dd>"+ // menu goes here
                                                                    "</dl>";

        private string RenderMenu()
        {
            List<HF.Menu> menus = HF.MenuManager.GetMenus();
            /*
            <div class="cp-menu">
                            <h1 class="menu-head"><a href="javascript:{}" onfocus="this.blur()">网站用户管理</a></h1>
                            <div class="menu-topline">&nbsp;</div>
                            <ul class="menu-content">
                                <li><a href="javascript:{}"  onfocus="this.blur()">权限管理</a></li>
                                <li><a href="javascript:{}"  onfocus="this.blur()">用户管理</a></li>
                            </ul>
                        </div>  */
            StringBuilder sb = new StringBuilder();
            HHPrincipal principal = (HHPrincipal)HttpContext.Current.User;
            string _cpMenu = string.Empty;
            string _item = string.Empty;
            string relative = GlobalSettings.RelativeWebRoot;
            foreach (HF.Menu m in menus)
            {
                if (principal.IsInRole(m.Roles))
                {
                    _item = string.Empty;
                    foreach (HF.MenuItem item in m.MenuItems)
                    {
                        if (principal.IsInRole(item.Roles))
                        {
                            _item += string.Format(items, item.ItemTitle, relative + item.Url, item.Name, item.ParentName);
                        }
                    }
                    if (!string.IsNullOrEmpty(_item))
                        sb.Append(string.Format(cpMenu,
                                                             string.Format(head, m.MenuTitle),
                                                             string.Format(menu, _item),
                                                             m.Name
                                                             ));
                }
            }
            return sb.ToString();
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = RenderMenu();
           
            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
