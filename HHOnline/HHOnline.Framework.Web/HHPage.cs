using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web.Pages;
using HHOnline.Permission.Components;
using HHOnline.Framework.Web.Enums;

namespace HHOnline.Framework.Web
{
    /// <summary>
    /// HHOnline.Portal 页面基类，继承自Page
    /// </summary>
    public class HHPage:Page
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (settings == null)
                settings = HHContext.Current.SiteSettings;
            if (User.Identity.IsAuthenticated)
                principal = User as HHPrincipal;
            //base.OnLoad(e);
            OnPageLoaded();
            PermissionCheckingArgs pcArgs = new PermissionCheckingArgs(new Dictionary<string, Control>());
            HandlePermissionChecking(pcArgs);

            bool isCheck = (!pcArgs.Cancel && this._AllowCheckPermission);
            if (isCheck)
            {
                OnPagePermissionChecking();
                HandleControlPermissionChecked(pcArgs);
            }
        }
        public override string StyleSheetTheme
        {
            get
            {
                if (settings == null)
                    settings = HHContext.Current.SiteSettings;
                return settings.SkinId;
            }
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
        protected override void OnInit(EventArgs e)
        {
            this.ControlPermissionChecking += new PermissionCheckingEventHandle(HHPage_ControlPermissionChecking);
            this.ControlPermissionChecked += new ControlPermissionEventHandler(HHPage_ControlPermissionChecked);
            base.OnInit(e);
        }
        /// <summary>
        /// 权限校验结束时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void HHPage_ControlPermissionChecked(object sender, ControlPermissionEventArgs e)
        {

        }
        /// <summary>
        /// 权限校验时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void HHPage_ControlPermissionChecking(object sender, PermissionCheckingArgs e)
        {

        }
        static HHPage() {
            _ControlPermissionChecked = new object();
            _ControlPermissionChecking = new object();
        }
        public HHPage()
        {
            _CheckedControl = new Dictionary<string, Control>();
        }

        #region -Properties-
        private HHPrincipal principal = null;
        private SiteSettings settings = null;
        private InfoType pageInfoType = InfoType.PageInfo;
        /// <summary>
        /// 页面提醒消息类型
        /// </summary>
        public InfoType PageInfoType
        {
            get { return pageInfoType; }
            set { pageInfoType = value; }
        }
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

        #region -CustomMethod-
        /// <summary>
        /// 设置标签名
        /// </summary>
        public virtual void SetTabName(string name)
        {
            Literal lt = this.Master.FindControl("ltPageName") as Literal;
            lt.Text = name;
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
            Page.Header.Controls.Add(meta);
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
                Page.Title = _ShortTitle + " - " + settings.SiteName;
        }
        /// <summary>
        /// 为站点增加Icon
        /// </summary>
        protected void AddIcon()
        {
            AddGenericLink("image/x-icon", "shortcut icon", "ICON", "images/favicon.ico");
        }
        /// <summary>
        /// 页面加载完成时的触发事件, 基类中已加载基本的js引用和皮肤
        /// </summary>
        public virtual void OnPageLoaded()
        {
            AddEncodeMeta();
            CompressCss();
            AddIcon();
            AddJavaScriptInclude("scripts/jquery-min.js", false, false);
            AddJavaScriptInclude( "scripts/util.js", false, false);
            //AddJavaScriptInclude(GlobalSettings.RelativeWebRoot + "javascript/plugins/jquery-1.3.2.min.js", false, true);

            SetTitle();
            AddKeywords(settings.SearchMetaKeywords);
            AddDescription(settings.SiteDescription);

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
                ClientScript.RegisterStartupScript(Type.GetType("System.String"), Guid.NewGuid().ToString(), str);
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
        /// 新增网站Keywords
        /// </summary>
        /// <param name="value">keywords 值</param>
        public virtual void AddKeywords(string value)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = "keywords";
            meta.Content = value;
            Page.Header.Controls.Add(meta);
        }
        /// <summary>
        /// 批量新增网站Keywords
        /// </summary>
        /// <param name="values">keywords 值</param>
        public virtual void AddKeywords(params string[] values)
        {
            HtmlMeta meta = null;
            foreach (string v in values)
            {
                meta = new HtmlMeta();
                meta.Name = "keywords";
                meta.Content = v;
                Page.Header.Controls.Add(meta);
            }
        }
        /// <summary>
        /// 新增网站description
        /// </summary>
        /// <param name="value">description 值</param>
        public virtual void AddDescription(string value)
        {
            HtmlMeta meta = new HtmlMeta();
            meta.Name = "description";
            meta.Content = value;
            Page.Header.Controls.Add(meta);
        }
        /// <summary>
        /// 批量新增网站description
        /// </summary>
        /// <param name="values">description 值</param>
        public virtual void AddDescription(params string[] values)
        {
            HtmlMeta meta = null;
            foreach (string v in values)
            {
                meta = new HtmlMeta();
                meta.Name = "description";
                meta.Content = v;
                Page.Header.Controls.Add(meta);
            }
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
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ?  string.Empty: " defer=\"defer\"") + " src=\"" + ResolveScriptUrl(url) + "\"></script>";
                ClientScript.RegisterStartupScript(GetType(), url.GetHashCode().ToString(), script);
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

        #region -Permission-
        private IDictionary<string, Control> _CheckedControl;
        /// <summary>
        /// 待验证权限的控件集合, (权限，控件)
        /// </summary>
        public IDictionary<string, Control> CheckedControl
        {
            get { return _CheckedControl; }
            set { _CheckedControl = value; }
        }
        private bool _AllowCheckPermission=true;
        /// <summary>
        /// 是否允许当前页检查权限，默认为true
        /// </summary>
        public virtual bool AllowCheckPermission
        {
            get { return _AllowCheckPermission; }
            set { _AllowCheckPermission = value; }
        }
        private string _PagePermission = string.Empty;
        /// <summary>
        /// 页面权限，通常指代访问此页面的权限，构成为 ModuleName-ActionName
        /// <remarks>ModuleName为此页面的模块名, ActionName为页面基本权限，通常为View，两者之间使用'-'连接</remarks>
        /// </summary>
        public virtual string PagePermission
        {
            get { return _PagePermission; }
            set { _PagePermission = value; }
        }
        /// <summary>
        /// 权限队列
        /// </summary>
        public List<ModuleActionKeyValue> UserPermissionList
        {
            get {
                if (principal == null)
                    return null;
                else
                    return principal.Roles;
            }
        }
        private static object _ControlPermissionChecked;
        /// <summary>
        /// 在页面控件经过权限校验后触发
        /// </summary>
        public event ControlPermissionEventHandler ControlPermissionChecked
        {
            add { base.Events.AddHandler(_ControlPermissionChecked, value); }
            remove { base.Events.RemoveHandler(_ControlPermissionChecked, value); }
        }

        private static object _ControlPermissionChecking;
        /// <summary>
        /// 在页面控件在权限校验时触发的事件
        /// </summary>
        public event PermissionCheckingEventHandle ControlPermissionChecking
        {
            add { base.Events.AddHandler(_ControlPermissionChecking, value); }
            remove { base.Events.RemoveHandler(_ControlPermissionChecking, value); }
        }
        private bool HandlePermissionChecking(PermissionCheckingArgs args)
        {
            OnPermissionChecking(args);
            return true;
        }
        /// <summary>
        /// 获取权限验证事件, 
        /// 权限校验触发顺序OnPermissionChecking, OnPagePermissionChecking, OnControlPermissionChecking, OnControlPermissionChecked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPermissionChecking(PermissionCheckingArgs e)
        {
            PermissionCheckingEventHandle pcEvent = (PermissionCheckingEventHandle)base.Events[_ControlPermissionChecking];
            if (pcEvent != null)
            {
                pcEvent(this, e);
            }
        }
        /// <summary>
        /// 页面权限校验，权限值来自于PagePermission成员属性，
        /// 权限校验触发顺序OnPermissionChecking, OnPagePermissionChecking, OnControlPermissionChecking, OnControlPermissionChecked
        /// </summary>
        protected virtual void OnPagePermissionChecking()
        {
            if (!HasPermission())
            {
                if (this.pageInfoType == InfoType.PageInfo)
                    throw new HHException(ExceptionType.AccessDenied, "您没有相应的权限来访问此页面！");
                else
                    throw new HHException(ExceptionType.NoMasterError, "您没有相应的权限来访问此页面！");
            }
        }
        /// <summary>
        /// 页面是否具备访问权限, 权限值来自于PagePermission成员属性
        /// </summary>
        protected virtual bool HasPermission()
        {
            if (string.IsNullOrEmpty(this.PagePermission))
                return true;
            else
            {
                if (UserPermissionList == null || UserPermissionList.Count == 0)
                    return false;
                else
                {
                    return IsInPermission(this.PagePermission);
                }
            }
        }
        private bool IsInPermission(string permission)
        {
            string[] code = permission.Split('-');
            if (code.Length != 2)
            {
                if (this.pageInfoType == InfoType.PageInfo)
                    throw new HHException(ExceptionType.ModuleInitFail, "权限代码必须形如ModuleName-ActionName。");
                else
                 throw new HHException(ExceptionType.NoMasterError, "权限代码必须形如ModuleName-ActionName。");

            }
            else
            {
                foreach (ModuleActionKeyValue makv in this.UserPermissionList)
                {
                    if (makv.ModuleName == code[0] && makv.ActionName == code[1])
                        return true;
                }
            }
            return false;
        }
        private bool HandleControlPermissionChecked(PermissionCheckingArgs e)
        {
            if (e.Cancel)
                return false;
            OnControlPermissionChecking(e.CheckPermissionControls);

            ControlPermissionEventArgs args = new ControlPermissionEventArgs(_CheckedControl);
            OnControlPermissionChecked(args);
            return true;
        }
        /// <summary>
        /// 检查控件权限,
        /// 权限校验触发顺序OnPermissionChecking, OnPagePermissionChecking, OnControlPermissionChecking, OnControlPermissionChecked
        /// </summary>
        /// <param name="controls">待检查控件队列</param>
        protected virtual void OnControlPermissionChecking(IDictionary<string, Control> controls)
        {
            CheckControlPermissionByRightList(controls, this.UserPermissionList);
        }

        /// <summary>
        /// 根据权限列表检查控件权限
        /// </summary>
        /// <param name="checkControls"></param>
        /// <param name="rights"></param>
        private void CheckControlPermissionByRightList(IDictionary<string, Control> checkControls, List<ModuleActionKeyValue> rights)
        {
            IEnumerator<KeyValuePair<string, Control>> controls = checkControls.GetEnumerator();
            while (controls.MoveNext())
            {
                if (!IsInPermission(controls.Current.Key))
                    controls.Current.Value.Visible = false;
            }
        }
        /// <summary>
        /// 权限检查完成时触发,
        /// 权限校验触发顺序OnPermissionChecking, OnPagePermissionChecking, OnControlPermissionChecking, OnControlPermissionChecked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnControlPermissionChecked(ControlPermissionEventArgs e)
        {
            ControlPermissionEventHandler cpEvent = (ControlPermissionEventHandler)base.Events[_ControlPermissionChecked];
            if (cpEvent != null)
            {
                cpEvent(this, e);
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
