using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using HHOnline.Framework;
using System.Web.UI.HtmlControls;

namespace HHOnline.Controls
{
    [PersistChildren(true), ParseChildren(false)]
    public class Head : Control
    {
        #region Static Keys
        private static readonly string titleKey = "HHOnline.Controls.Title.Value";
        private static readonly string titleFormat = "{0}-{1}";
        private static readonly string metaKey = "HHOnline.Controls.MetaTags";
        private static readonly string metaFormat = "<meta name=\"{0}\" content=\"{1}\" />";
        private static readonly string styleKey = "HHOnline.Controls.StyleTags";
        private static readonly string styleFormat = "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" {1} />";
        private static readonly string linkKey = "HHOnline.Controls.LinkTags";
        private static readonly string linkFormat = "<link rel=\"{0}\" href=\"{1}\" />";
        private static readonly string rawContentKey = "HHOnline.Controls.RawHeaderContent";
        #endregion

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    if (Page.Header != null)
        //    {
        //        if (string.IsNullOrEmpty(Page.Header.Title))
        //        {
        //            string title = Context.Items[titleKey] as string;
        //            if (GlobalSettings.IsNullOrEmpty(title))
        //            {
        //                title = HHContext.Current.SiteSettings.SiteName;
        //            }
        //            Page.Header.Title = title;
        //        }
        //    }
        //}

        protected override void Render(HtmlTextWriter writer)
        {
            SiteSettings settings = HHContext.Current.SiteSettings;
            if (Page.Header == null)
            {
                writer.WriteBeginTag("head");
                writer.AddAttribute("runat", "server");
            }
            RenderTitle(settings, writer);
            RenderMetaTags(settings, writer);
            RenderLinkTags(settings, writer);
            RenderStyleSheets(writer);
            base.Render(writer);
            RenderAdditionHeader(settings, writer);
            RenderRawContent(writer);
            if (Page.Header == null)
            {
                writer.WriteEndTag("head");
            }
        }

        #region -Render Methods-
        protected virtual void RenderTitle(SiteSettings settings, HtmlTextWriter writer)
        {
            string title = Context.Items[titleKey] as string;
            if (GlobalSettings.IsNullOrEmpty(title))
            {
                title = settings.SiteName;
                writer.WriteBeginTag("title");
                writer.Write(title);
                writer.WriteEndTag("title");
            }
        }

        /// <summary>
        /// 输出meta tags
        /// </summary>
        /// <param name="settings">站点设置</param>
        /// <param name="writer">The writer.</param>
        protected virtual void RenderMetaTags(SiteSettings settings, HtmlTextWriter writer)
        {
            NameValueCollection metaTags = Context.Items[metaKey] as NameValueCollection;
            if (metaTags == null)
                metaTags = new NameValueCollection();
            if (GlobalSettings.IsNullOrEmpty(metaTags["description"]) && !GlobalSettings.IsNullOrEmpty(settings.SiteDescription))
            {
                metaTags["description"] = settings.SiteDescription;
            }
            if (GlobalSettings.IsNullOrEmpty(metaTags["keywords"]) && !GlobalSettings.IsNullOrEmpty(settings.SearchMetaKeywords))
            {
                metaTags["keywords"] = settings.SearchMetaKeywords;
            }
            foreach (string key in metaTags.Keys)
            {
                writer.WriteLine(metaFormat, key, GlobalSettings.EnsureHtmlEncoded(metaTags[key]));
            }
        }

        /// <summary>
        /// 输出样式表.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void RenderStyleSheets(HtmlTextWriter writer)
        {
            RenderStyleSheets(writer, StyleRelativePosition.First);
            RenderStyleSheets(writer, StyleRelativePosition.Unspecified);
            RenderStyleSheets(writer, StyleRelativePosition.Last);
        }

        /// <summary>
        /// 输出样式表
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="position">The position.</param>
        protected virtual void RenderStyleSheets(HtmlTextWriter writer, StyleRelativePosition position)
        {
            Queue queue = Context.Items[styleKey] as Queue;
            if (queue != null && queue.Count > 0)
            {
                IEnumerator ie = queue.GetEnumerator();
                while (ie.MoveNext())
                {
                    StyleQueueItem si = (StyleQueueItem)ie.Current;
                    if (si.Position == position)
                        writer.WriteLine(si.StyleTag);
                }
            }
        }
       
        /// <summary>
        /// 输出link tags.
        /// </summary>
        /// <param name="settings">站点设置</param>
        /// <param name="writer">The writer.</param>
        protected virtual void RenderLinkTags(SiteSettings settings, HtmlTextWriter writer)
        {
            NameValueCollection linkTags = Context.Items[linkKey] as NameValueCollection;
            if (linkTags == null)
                linkTags = new NameValueCollection();

            foreach (string key in linkTags.Keys)
            {
                writer.WriteLine(linkFormat, key, GlobalSettings.EnsureHtmlEncoded(linkTags[key]));
            }
        }

        /// <summary>
        /// 输出附加头信息
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="writer"></param>
        protected virtual void RenderAdditionHeader(SiteSettings settings, HtmlTextWriter writer)
        {
            if (!GlobalSettings.IsNullOrEmpty(settings.RawAdditionalHeader))
            {
                writer.WriteLine(settings.RawAdditionalHeader);
            }
        }

        /// <summary>
        /// 输出原始内容
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void RenderRawContent(HtmlTextWriter writer)
        {
            ArrayList rawContent = Context.Items[rawContentKey] as ArrayList;
            if (rawContent == null)
                rawContent = new ArrayList();

            foreach (string item in rawContent)
            {
                writer.WriteLine(item);
            }
        }
        #endregion

        #region -MetaTags-
        /// <summary>
        /// 添加description meta tag.
        /// </summary>
        public void AddMetaDescription(string value)
        {
            AddMetaTag("description", value);
        }

        /// <summary>
        /// 添加keyword meta tag
        /// </summary>
        public void AddMetaKeywords(string value)
        {
            AddMetaTag("keywords", value);
        }

        /// <summary>
        /// 添加新meta tag key and value
        /// </summary>
        private void AddMetaTag(string key, string value)
        {
            if (!GlobalSettings.IsNullOrEmpty(key) && !GlobalSettings.IsNullOrEmpty(value))
            {
                NameValueCollection mc = HttpContext.Current.Items[metaKey] as NameValueCollection;
                if (mc == null)
                {
                    mc = new NameValueCollection();
                    HttpContext.Current.Items.Add(metaKey, mc);
                }
                mc[key] = value;
            }
        }
        #endregion

        #region -Title-

        /// <summary>
        /// 设置标题.
        /// </summary>
        /// <param name="title">标题.</param>
        public void AddTitle(string title)
        {
            HttpContext.Current.Items[titleKey] = title;

            Page page = HttpContext.Current.Handler as Page;
            if (page != null && page.Header != null)
                page.Title = title;
        }

        /// <summary>
        /// 设置标题, 使用站点名称格式化.
        /// </summary>
        /// <param name="title">标题.</param>
        /// <param name="context">The context.</param>
        public void AddSiteNameTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                AddTitle(HHContext.Current.SiteSettings.SiteName);
            else
                AddTitle(string.Format(titleFormat, title, HHContext.Current.SiteSettings.SiteName));
        }
        #endregion

        #region -Style-
        /// <summary>
        /// 批量增加样式
        /// </summary>
        /// <param name="urls">样式串</param>
        public void AddStyles(params string[] urls)
        {
            foreach (string url in urls)
            {
                AddStyle(url);
            }
        }
       /// <summary>
       /// 新增样式
       /// </summary>
       /// <param name="url">样式Url</param>
        public void AddStyle(string url)
        {
            AddStyle(url, HtmlLinkMedia.none, StyleRelativePosition.Unspecified);
        }
        /// <summary>
        /// 添加样式表.
        /// </summary>
        /// <param name="url">样式 Url</param>
        /// <param name="media">media类型.</param>
        public void AddStyle(string url, HtmlLinkMedia media)
        {
            AddStyle(url, media, StyleRelativePosition.Unspecified);
        }

        /// <summary>
        /// 添加样式表
        /// </summary>
        /// <param name="url">样式 URL.</param>
        /// <param name="media"> media 类型.</param>
        /// <param name="position">The position.</param>
        public void AddStyle(string url, HtmlLinkMedia media, StyleRelativePosition position)
        {
            if (GlobalSettings.IsNullOrEmpty(url))
                return;
            HttpContext context = HttpContext.Current;
            Queue styleQueue = context.Items[styleKey] as Queue;
            if (styleQueue == null)
            {
                styleQueue = new Queue();
                context.Items[styleKey] = styleQueue;
            }
            styleQueue.Enqueue(new StyleQueueItem(string.Format(styleFormat, 
                                                                                              GlobalSettings.EnsureHtmlEncoded(url),
                                                                                              (media==HtmlLinkMedia.none?"":" media=\""+media.ToString()+"\"")), position));
        }
        #endregion

        #region -Link Tags-
        /// <summary>
        /// 添加Link标签
        /// </summary>
        /// <param name="rel">Link标签类型</param>
        /// <param name="href">链接Url</param>
        public void AddLinkTag(HtmlLinkRel rel, string href)
        {
            if (rel != HtmlLinkRel.None)
            {
                HttpContext context = HttpContext.Current;
                NameValueCollection lc = context.Items[linkKey] as NameValueCollection;
                if (lc == null)
                {
                    lc = new NameValueCollection();
                    context.Items.Add(linkKey, lc);
                }
                lc[rel.ToString()] = href;
            }
        }
        #endregion

        #region -Raw Content-

        /// <summary>
        /// 添加原始内容到HTML Header, 例如script blocks or custom tags
        /// </summary>
        public void AddRawContent(string content)
        {
            if (!GlobalSettings.IsNullOrEmpty(content))
            {
                HttpContext context = HttpContext.Current;
                ArrayList mc = context.Items[rawContentKey] as ArrayList;
                if (mc == null)
                {
                    mc = new ArrayList();
                    context.Items.Add(rawContentKey, mc);
                }
                mc.Add(content);
            }
        }

        #endregion
    }

    #region -Enumerator-
    /// <summary>
    /// HtmlLink rel 类型
    /// </summary>
    public enum HtmlLinkRel
    {
        None,
        Alternate,
        Appendix,
        Bookmark,
        Chapter,
        Contents,
        Copyright,
        Glossary,
        Help,
        Index,
        Next,
        Prev,
        Section,
        Start,
        Stylesheet,
        Subsection
    }
    /// <summary>
    /// Html Link Media 类型
    /// </summary>
    public enum HtmlLinkMedia
    { 
        none,
        all,
        aural,
        braille,
        handheld,
        print,
        projection,
        screen,
        tty,
        tv
    }
    #endregion
}
