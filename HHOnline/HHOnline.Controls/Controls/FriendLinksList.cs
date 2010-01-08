using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using HHOnline.Shops;
using HHOnline.Framework;
using System.IO;
using HHOnline.News.Components;
using HHOnline.News.Services;

namespace HHOnline.Controls
{
    public class FriendLinksList : Control
    {
        private int updateTime = 5;
        public static object _lock = new object();
        private static string _Html;
        public string HTML
        {
            get
            {
                if (string.IsNullOrEmpty(_Html)||DateTime.Now.Minute%updateTime==0)
                {
                    lock (_lock)
                    {
                        _Html = BindLinks();
                    }
                }
                return _Html;
            }
        }
        string BindLinks()
        {
            List<FriendLink> links = FriendLinks.FriendLinkGet();
            if (links.Count == 0)
            {
                return "";
            }
            else {                 
                StringBuilder sb = new StringBuilder();
                foreach (FriendLink fl in links) {
                    sb.AppendFormat("<a href='{0}' target='_blank'>{1}</a>", fl.Url, fl.Title);
                }
                return sb.ToString();
            }
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.Write(Environment.NewLine);
        }
    }
}
