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
using System.Configuration;

/**
 * *by Jericho
 * */
namespace HHOnline.Controls
{
    /// <summary>
    /// 菜单控件
    /// </summary>
    public class ProfileMenu:UserControl
    {
        static ProfileMenu() { }
        private static readonly string menu = "<ul class=\"menu-content\">{0}</ul>";
        private static readonly string items = "<li><a href=\"{1}\"  id=\"item_{2}\" parentId=\"menu_{3}\" onclick=\"menu.adaptItem(this)\" onfocus=\"this.blur()\">{0}</a></li>";
        private static readonly string head = "<dt class=\"menu-head\"onclick=\"menu.adaptMenu(this)\"><a href=\"javascript:void(0)\" onfocus=\"this.blur()\">{0}</a></dt>";
        private static readonly string cpMenu = "<dl class=\"cp-menu\" id=\"menu_{2}\">"+
                                                                        "{0}"+ // head goes here
                                                                        "<dd class=\"menu-body\">{1}</dd>"+ // menu goes here
                                                                    "</dl>";
        

        private string RenderMenu()
        {
            List<HF.Menu> menus = HF.MenuManager.GetProfileMenus();

            StringBuilder sb = new StringBuilder();

            SettingsPropertyValueCollection spvc = this.Context.Profile.PropertyValues;
            User u = spvc["AccountInfo"].PropertyValue as User;

            string _cpMenu = string.Empty;
            string _item = string.Empty;
            string relative = GlobalSettings.RelativeWebRoot;
            int role = 2;
            foreach (HF.Menu m in menus)
            {
                role = 2;
                if (m.Roles == "Manager") role = 1;
                if (role == 1 && u.IsManager == 2) continue;
                _item = string.Empty;
                foreach (HF.MenuItem item in m.MenuItems)
                {
                    role = 2; if (item.Roles == "Manager") role = 1;
                    if (role == 1 && u.IsManager == 2) continue;
                    _item += string.Format(items, item.ItemTitle, relative + item.Url, item.Name, item.ParentName);
                }
                if (!string.IsNullOrEmpty(_item))
                    sb.Append(string.Format(cpMenu,
                                             string.Format(head, m.MenuTitle),
                                             string.Format(menu, _item),
                                             m.Name
                                             ));
            }
            return sb.ToString();
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            string html = this.RenderMenu();
           
            writer.Write(html);
            writer.WriteLine(Environment.NewLine);
        }
    }
}
