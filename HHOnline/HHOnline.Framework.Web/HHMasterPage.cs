using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;

namespace HHOnline.Framework.Web
{
    public class HHMasterPage : MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (settings == null)
                settings = HHContext.Current.SiteSettings;
            OnPageLoaded();
        }
        #region -Properties-
        private SiteSettings settings = null;
        private string _ShortTitle;
        /// <summary>
        /// 页面Title，将与SiteName组合成 Title - SiteName 的形式作为Title显示
        /// </summary>
        public string ShortTitle
        {
            get { return _ShortTitle; }
            set { _ShortTitle = value; }
        }
        #endregion

        #region -Abstract method-
        /// <summary>
        /// 添加编码规范，默认为UTF-8
        /// </summary>
        public virtual void AddEncodeMeta()
        {
            //<meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes.Add("content", "text/html; charset=utf-8");
            meta.Attributes.Add("http-equiv", "Content-Type");
            Page.Header.Controls.AddAt(0, meta);
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string CopyRight
        {
            get
            {
                if (settings == null)
                    settings = HHContext.Current.SiteSettings;
                return settings.Copyright;
            }
        }
        /// <summary>
        /// 自定义验证控件外观
        /// </summary>
        /// <param name="autoClose">是否自动关闭</param>
        /// <param name="filterClose">是否使用滤镜</param>
        /// <param name="duration">关闭延时(s)</param>
        public virtual void SetValidator(bool autoClose, bool filterClose, int duration)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("window.$validatorparams={isAutoClose:");
            sb.Append(autoClose.ToString().ToLower());
            sb.Append(",isFilterClose:");
            sb.Append(filterClose.ToString().ToLower());
            sb.Append(",duration:");
            sb.Append((duration * 1000).ToString());
            sb.Append("};");
            this.ExecuteJs(sb.ToString(), false);
            this.AddJavaScriptInclude("scripts/validator.js", true, true);
        }
        /// <summary>
        /// 设置页面标题
        /// </summary>
        public virtual void SetTitle()
        {
            if (!GlobalSettings.IsNullOrEmpty(_ShortTitle))
            {
                int splitIndex = _ShortTitle.IndexOf(" - ");    //判断组合标题+关键字的组合格式
                Page.Title = 0 > splitIndex ? _ShortTitle + " - " + settings.SiteName : _ShortTitle.Substring(0, splitIndex) + " - " + settings.SiteName + _ShortTitle.Substring(splitIndex);
            }
            else
                Page.Title = settings.SiteName;
        }

        /// <summary>
        /// 页面加载完成时的触发事件, 基类中已加载基本的js引用和皮肤
        /// </summary>
        public virtual void OnPageLoaded()
        {
            AddEncodeMeta();
            CompressCss();
            AddGenericLink("image/x-icon", "shortcut icon", "ICON", "images/favicon.ico");
            AddJavaScriptInclude("scripts/jquery-min.js", false, false);
            AddJavaScriptInclude("scripts/util.js", false, false);
            //AddJavaScriptInclude(GlobalSettings.RelativeWebRoot + "javascript/plugins/jquery-1.3.2.min.js", false, true);

            SetTitle();
            AddKeywords(settings.SearchMetaKeywords);
            AddDescription(settings.SiteDescription);

        }
        /// <summary>
        /// 注册js脚本到客户端
        /// </summary>
        /// <param name="js">待注册的js脚本</param>
        public virtual void RegJs(string js)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "regjs", js, true);
        }
        /// <summary>
        /// 新增或更新页面keywords
        /// </summary>
        /// <param name="value">keywords 值</param>
        public virtual void AddKeywords(string value)
        {
            bool isExist = false;
            foreach (Control ctrl in Page.Header.Controls)
            {
                if (false == (ctrl is HtmlMeta))
                    continue;
                HtmlMeta meta = (HtmlMeta)ctrl;
                if (meta.Name.ToLower() == "keywords")
                {
                    meta.Content = value + "; " + meta.Content;
                    isExist = true;
                    break;
                }
            }
            if (false == isExist)
            {
                HtmlMeta meta = new HtmlMeta();
                meta.Name = "keywords";
                meta.Content = value;

                Page.Header.Controls.Add(meta);
            }
        }
        /// <summary>
        /// 批量新增页面keywords
        /// </summary>
        /// <param name="values">keywords 值</param>
        public virtual void AddKeywords(params string[] values)
        {
            if (null != values && 0 < values.Length)
                AddKeywords(string.Join(",", values));
        }
        /// <summary>
        /// 执行Js语句
        /// </summary>
        /// <param name="script">待注入脚本</param>
        /// <param name="isInHead">是否放置在Head中</param>
        public virtual void ExecuteJs(string script, bool isInHead)
        {
            string str = string.Format("\n<script  type=\"text/javascript\">\n{0}\n</script>\n", script);
            if (!isInHead)
            {
                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), Guid.NewGuid().ToString(), str);
            }
            else
            {
                HtmlGenericControl child = new HtmlGenericControl("script");
                child.Attributes.Add("type", "text/javascript");
                child.InnerHtml = "\n" + script + "\n";
                Page.Header.Controls.Add(child);
            }
        }
        /// <summary>
        /// 新增或更新页面description
        /// </summary>
        /// <param name="value">description 值</param>
        public virtual void AddDescription(string value)
        {
            bool isExist = false;
            foreach (Control ctrl in Page.Header.Controls)
            {
                if (false == (ctrl is HtmlMeta))
                    continue;
                HtmlMeta meta = (HtmlMeta)ctrl;
                if (meta.Name.ToLower() == "description")
                {
                    meta.Content = value + "; " + meta.Content;
                    isExist = true;
                    break;
                }
            }
            if (false == isExist)
            {
                HtmlMeta meta = new HtmlMeta();
                meta.Name = "description";
                meta.Content = value;

                Page.Header.Controls.Add(meta);
            }
        }
        /// <summary>
        /// 批量新增页面description
        /// </summary>
        /// <param name="values">description 值</param>
        public virtual void AddDescription(params string[] values)
        {
            if (null != values && 0 < values.Length)
                AddDescription(string.Join(",", values));
        }

        /// <summary>
        /// 在head中添加link标记
        /// </summary>
        /// <param name="relation">rel值</param>
        /// <param name="title">title值</param>
        /// <param name="href">链接(相对于根目录，不包含'/'，如css/a.css)</param>
        public virtual void AddGenericLink(string relation, string title, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = GlobalSettings.RelativeWebRoot + href;
            Page.Header.Controls.Add(link);
        }
        /// <summary>
        /// 在head中添加link标记
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="relation">rel值</param>
        /// <param name="title">title值</param>
        /// <param name="href">链接(相对于根目录，不包含'/'，如css/a.css)</param>
        public virtual void AddGenericLink(string type, string relation, string title, string href)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = type;
            link.Attributes["rel"] = relation;
            link.Attributes["title"] = title;
            link.Attributes["href"] = GlobalSettings.RelativeWebRoot + href;
            Page.Header.Controls.Add(link);
        }
        /// <summary>
        /// 添加javascript引用
        /// </summary>
        /// <param name="url">Url(相对于根目录，不以'/'开头，如scripts/a.js)</param>
        /// <param name="placeInBottom">false:放置在head中, true:放置在body尾端</param>
        /// <param name="addDeferAttribute">加载完成后不执行</param>
        public virtual void AddJavaScriptInclude(string url, bool placeInBottom, bool addDeferAttribute)
        {
            url = GlobalSettings.RelativeWebRoot + url;
            if (placeInBottom)
            {
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ? string.Empty : " defer=\"defer\"") + " src=\"" + ResolveScriptUrl(url) + "\"></script>";
                Page.ClientScript.RegisterStartupScript(GetType(), url.GetHashCode().ToString(), script);
            }
            else
            {
                HtmlGenericControl script = new HtmlGenericControl("script");
                script.Attributes["type"] = "text/javascript";
                script.Attributes["src"] = ResolveScriptUrl(url);
                if (addDeferAttribute)
                {
                    script.Attributes["defer"] = "defer";
                }

                Page.Header.Controls.Add(script);
            }
        }
        #endregion

        #region -Private method-
        protected string ResolveScriptUrl(string url)
        {
            return GlobalSettings.RelativeWebRoot + "jscss.axd?path=" + HttpUtility.UrlEncode(url);
        }

        /// <summary>
        /// 当使用web.config设置样式时用此方法来压缩css
        /// </summary> 
        protected void CompressCss()
        {
            foreach (Control c in Page.Header.Controls)
            {
                HtmlControl hc = c as HtmlControl;
                if (hc != null && hc.Attributes["type"] != null && hc.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase))
                {
                    if (!hc.Attributes["href"].StartsWith("http"))
                    {
                        string path = hc.Attributes["href"];
                        if (path.StartsWith("~/"))
                        {
                            path = GlobalSettings.RelativeWebRoot + path.Substring(2);
                        }
                        if (GlobalSettings.CompressCss)
                            hc.Attributes["href"] = GlobalSettings.RelativeWebRoot + "jscss.axd?path=" + path;
                        else
                            hc.Attributes["href"] = path;
                        hc.EnableViewState = false;
                    }
                }
            }
        }
        #endregion
    }
}
