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
    public class ArticleList : Control
    {
        static ArticleList()
        {
            ArticleManager.Updated += delegate { _Html = string.Empty; };
        }
        private int _Num = 10;
        /// <summary>
        /// 显示的列表数
        /// </summary>
        public int Num
        {
            get { return _Num; }
            set { _Num = value; }
        }

        public static object _lock = new object();
        private static string _Html;
        public string HTML
        {
            get
            {
                if (string.IsNullOrEmpty(_Html))
                {
                    lock (_lock)
                    {
                        if (string.IsNullOrEmpty(_Html))
                        {
                            HtmlGenericControl ul = BindArticleList();
                            StringWriter sw = new StringWriter();
                            ul.RenderControl(new HtmlTextWriter(sw));
                            _Html = sw.ToString();
                        }
                    }
                }
                return _Html;
            }
        }
        HtmlGenericControl BindArticleList()
        {
            List<Article> artilesTemp = ArticleManager.GetAllArticles();
            List<Article> articles = artilesTemp.GetRange(0, Math.Min(_Num, artilesTemp.Count));

            if (articles.Count == 0)
            {
                HtmlGenericControl p = new HtmlGenericControl("P");
                p.InnerText = "没有资讯信息！";
                return p;
            }

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.ID = "ulArticleList";

            HtmlGenericControl li = null;
            HtmlAnchor anchor = null;
            foreach (var b in articles)
            {
                li = new HtmlGenericControl("LI");
                anchor = new HtmlAnchor();
                anchor.HRef = GlobalSettings.RelativeWebRoot + "pages/view.aspx?news-newsdetail&ID=" + GlobalSettings.Encrypt(b.ID.ToString());
                anchor.InnerText = GlobalSettings.SubString(b.Title, 30);
                anchor.Title = b.Title;
                anchor.Target = "_blank";
                li.Controls.Add(anchor);

                ul.Controls.Add(li);
            }
            return ul;
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(HTML);
            writer.Write(Environment.NewLine);
        }
    }
}
