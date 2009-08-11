using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    /// <summary>
    /// 编辑器
    /// </summary>
    [PersistChildren(true), ParseChildren(false)]
    public class Editor : TextBox
    {
        #region Private Members
        private bool isStyleCorrected;
        private List<KeyValue> options = null;
        private static Regex FindStyles = new Regex("style\\s*=\\s*\\\"?([^\\\"\\>]*)", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex IsPluginListProperty = new Regex("^plugins$", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex IsButtonListProperty = new Regex("(?:theme_advanced_container_[a-z0-9]+|theme_advanced_buttons[0-9]+.*)", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        #region Properties
        [Browsable(true), Description("HtmlEditor 显示方式"), Category("显示方式")]
        public HtmlEditorMode EditorMode
        {
            get
            {
                if (ViewState["HtmlEditorMode"] == null)
                {
                    return HtmlEditorMode.Standard;
                }
                else
                {
                    return (HtmlEditorMode)ViewState["HtmlEditorMode"];
                }
            }
            set
            {
                ViewState["HtmlEditorMode"] = value;
            }
        }

        /// <summary>
        /// 是否容许HtmlEditing
        /// </summary>
        public bool EnableHtmlModeEditing
        {
            get
            {
                if (ViewState["EnableHtmlModeEditing"] == null)
                    return true;
                return Convert.ToBoolean(ViewState["EnableHtmlModeEditing"]);
            }
            set
            {
                ViewState["EnableHtmlModeEditing"] = value;
            }
        }

        /// <summary>
        /// 参数选项
        /// </summary>
        public List<KeyValue> Options
        {
            get
            {
                if (this.options == null)
                {
                    this.options = new List<KeyValue>();
                    switch (this.EditorMode)
                    {
                        case HtmlEditorMode.PlainText:
                            this.options.Add(new KeyValue("theme", "simple"));
                            break;
                        case HtmlEditorMode.Simple:
                            this.options.Add(new KeyValue("theme", "advanced"));
                            this.options.Add(new KeyValue("theme_advanced_blockformats", "h2,h3,h4,p"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_location", "top"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_align", "left"));
                            this.options.Add(new KeyValue("theme_advanced_statusbar_location", "bottom"));
                            this.options.Add(new KeyValue("theme_advanced_resizing", "true"));
                            this.options.Add(new KeyValue("theme_advanced_resize_horizontal", "false"));
                            this.options.Add(new KeyValue("theme_advanced_buttons1", "bold,italic,underline,strikethrough,separator,indent,outdent,separator,bullist,numlist"));
                            this.options.Add(new KeyValue("theme_advanced_buttons2", ""));
                            this.options.Add(new KeyValue("theme_advanced_buttons3", ""));
                            break;
                        case HtmlEditorMode.Enhanced:
                            this.options.Add(new KeyValue("plugins", "paste,fullscreen,style,insertmedia,contextmenu,inlinepopups,syntaxhl"));
                            this.options.Add(new KeyValue("fix_content_duplication", "true"));
                            this.options.Add(new KeyValue("remove_linebreaks", "false"));
                            this.options.Add(new KeyValue("verify_html", "false"));
                            this.options.Add(new KeyValue("tab_focus", ":prev,:next"));
                            this.options.Add(new KeyValue("gecko_spellcheck", "true"));
                            this.options.Add(new KeyValue("theme_advanced_blockformats", "h2,h3,h4,p"));
                            this.options.Add(new KeyValue("theme", "advanced"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_location", "top"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_align", "left"));
                            this.options.Add(new KeyValue("theme_advanced_statusbar_location", "bottom"));
                            this.options.Add(new KeyValue("theme_advanced_resizing", "true"));
                            this.options.Add(new KeyValue("theme_advanced_buttons1", "fontselect,fontsizeselect,separator,forecolor,backcolor,separator,styleprops,removeformat,cleanup,separator,undo,redo,separator,cut,copy,pastetext,pasteword,separator,code,separator,fullscreen"));
                            this.options.Add(new KeyValue("theme_advanced_buttons2", "bold,italic,underline,strikethrough,separator,indent,outdent,separator,bullist,numlist,table,separator,link,unlink,insertmedia,image"));
                            this.options.Add(new KeyValue("theme_advanced_buttons3", ""));
                            break;
                        case HtmlEditorMode.Standard:
                        default:
                            this.options.Add(new KeyValue("plugins", "paste,fullscreen,style,insertmedia,contextmenu,inlinepopups,syntaxhl"));
                            this.options.Add(new KeyValue("fix_content_duplication", "true"));
                            this.options.Add(new KeyValue("remove_linebreaks", "false"));
                            this.options.Add(new KeyValue("verify_html", "false"));
                            this.options.Add(new KeyValue("tab_focus", ":prev,:next"));
                            this.options.Add(new KeyValue("gecko_spellcheck", "true"));
                            this.options.Add(new KeyValue("theme_advanced_blockformats", "h2,h3,h4,p"));
                            this.options.Add(new KeyValue("theme", "advanced"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_location", "top"));
                            this.options.Add(new KeyValue("theme_advanced_toolbar_align", "left"));
                            this.options.Add(new KeyValue("theme_advanced_statusbar_location", "bottom"));
                            this.options.Add(new KeyValue("theme_advanced_resizing", "true"));
                            this.options.Add(new KeyValue("theme_advanced_buttons1", "fontselect,fontsizeselect,separator,forecolor,backcolor,separator,bold,italic,underline,strikethrough,separator,link,unlink,anchor,separator,image,media,separator"));
                            this.options.Add(new KeyValue("theme_advanced_buttons2", ""));
                            this.options.Add(new KeyValue("theme_advanced_buttons3", ""));
                            break;
                    }
                }
                return this.options;
            }
        }
        #endregion

        #region Event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Height != Unit.Empty)
            {
                base.Style["height"] = this.Height.ToString();
                this.Height = Unit.Empty;
            }
            else
            {
                base.Style["height"] = "200px;";
            }
            if (this.Width != Unit.Empty)
            {
                base.Style["width"] = this.Width.ToString();
                this.Width = Unit.Empty;
            }
            else
            {
                base.Style["width"] = "100%";
            }
            this.TextMode = TextBoxMode.MultiLine;

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.ClientScript.RegisterClientScriptInclude("TinyMCE_3_2_5",
                this.Page.Response.ApplyAppPathModifier("~/tiny_mce/tiny_mce.js"));
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);


            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), "HHOnlineOptions"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "HHOnlineOptions", "");
                writer.Write("\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n// <![CDATA[\r\n");
                writer.Write("tinyMCE_HHOnlineOptions = new Object();\r\n");
                writer.Write(@"tinyMCE_HHOnlineOptions.InsertMediaOpenFunction = function()
                {
                      $.fn.jmodal({
                        title: '插入媒体',
                        initWidth: 500,
                        closable:false,
                        content: function(sender, args) {
                            window.$mceHideJModal = args.cancel;
                                sender.height(260);
                                sender.attr('src', '" + SiteUrlManager.GetInsertMediaUrl() + @"?t='+Math.random());
                            }
                         });
                };");
                writer.Write("\r\n// ]]>\r\n</script>");
            }
            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), this.ClientID))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), this.ClientID, "");
                writer.Write("\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n// <![CDATA[\r\n");
                writer.Write(@"function InsertContentToHtmlEditor(content)
                {{
                    var bm = tinyMCE.activeEditor.selection.getBookmark();
                    tinyMCE.get('{0}').execCommand('mceInsertContent',false,content);
                    tinyMCE.activeEditor.selection.moveToBookmark(bm);
                }}", this.ClientID);

                writer.Write("tinyMCE.init({mode:'exact',relative_urls:false,");
                //writer.Write("setup:new Function('ed', 'tinyMCE_HHOnlineOptions.Setup(ed);'),");
                writer.Write("elements:'{0}',language:'zh'{1}}});", this.ClientID.ToString(), this.GetFormattedOptions());

                writer.Write("\r\n// ]]>\r\n</script>");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获取选择内容书签脚本
        /// </summary>
        /// <returns></returns>
        public string GetBookmarkScript()
        {
            return string.Format("if (window.tinyMCE_{0}_currentBookmark == null) {{ window.tinyMCE_{0}_currentBookmark = tinyMCE.get('{0}').selection.getBookmark(); }}", this.ClientID);
        }

        /// <summary>
        /// 获取插入内容脚本
        /// </summary>
        /// <param name="contentVariableName"></param>
        /// <returns></returns>
        public string GetContentInsertScript(string contentVariableName)
        {
            //if (window.tinyMCE_{0}_currentBookmark) {{ tinyMCE.get('{0}').selection.moveToBookmark(window.tinyMCE_{0}_currentBookmark); }};
            return string.Format(" tinyMCE.get('{0}').execCommand('mceInsertContent', false,'{1}')", this.ClientID, contentVariableName);
        }

        /// <summary>
        /// 获取内容脚本
        /// </summary>
        /// <returns></returns>
        public string GetContentScript()
        {
            return string.Format("tinyMCE.get('{0}').getContent()", this.ClientID);
        }

        /// <summary>
        /// 内容更新脚本
        /// </summary>
        /// <param name="contentVariableName"></param>
        /// <returns></returns>
        public string GetContentUpdateScript(string contentVariableName)
        {
            return string.Format("tinyMCE.get('{0}').execCommand('mceSetContent', false, {1})", this.ClientID, contentVariableName);
        }

        /// <summary>
        /// 焦点设置脚本
        /// </summary>
        /// <returns></returns>
        public string GetFocusScript()
        {
            return string.Format("tinyMCE.get('{0}').focus()", this.ClientID);
        }

        /// <summary>
        /// 获取格式化选项
        /// </summary>
        /// <returns></returns>
        private string GetFormattedOptions()
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValue pair in this.Options)
            {
                if (pair.Name == null || String.IsNullOrEmpty(pair.Name.Trim()))
                    continue;
                builder.Append(",");
                builder.Append(pair.Name);
                builder.Append(":");
                if (IsButtonListProperty.IsMatch(pair.Name))
                {
                    if (!this.EnableHtmlModeEditing)
                        pair.Text = this.RemoveItemFromList(pair.Text, "code");
                }
                builder.Append("'" + pair.Text + "'");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 移除Item项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string RemoveItemFromList(string list, string item)
        {
            if (list.Length <= 2)
            {
                return list;
            }
            List<string> itemList = new List<string>();
            foreach (string str in list.Substring(1, list.Length - 2).Split(new char[] { ',' }))
            {
                if (str.Trim() != item)
                {
                    itemList.Add(str);
                }
            }
            return string.Join(",", itemList.ToArray());
        }

        /// <summary>
        /// 判断插件是否安装
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns></returns>
        private bool IsPluginInstalled(string pluginName)
        {
            foreach (KeyValue pair in this.Options)
            {
                if (IsPluginListProperty.IsMatch(pair.Name))
                {
                    if (pair.Text.Length <= 2)
                        return false;
                    foreach (string str in pair.Text.Substring(1, pair.Text.Length - 2).Split(new char[] { ',' }))
                    {
                        if (str.Trim() == pluginName)
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public override string Text
        {
            get
            {
                if (!this.isStyleCorrected)
                {
                    Match match = FindStyles.Match(base.Text);
                    StringBuilder builder = new StringBuilder();
                    int startIndex = 0;
                    while (match.Success)
                    {
                        if (startIndex != match.Index)
                        {
                            builder.Append(base.Text.Substring(startIndex, match.Index - startIndex));
                        }
                        builder.Append(match.Value.Replace("&quot;", "'").Replace("&#34;", "'").Replace("&#39;", "'"));
                        startIndex = match.Index + match.Length;
                        match = FindStyles.Match(base.Text, startIndex);
                    }
                    if (startIndex < base.Text.Length)
                    {
                        builder.Append(base.Text.Substring(startIndex));
                    }
                    base.Text = builder.ToString();
                    this.isStyleCorrected = true;
                }
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.isStyleCorrected = false;
            }
        }
        #endregion
    }
}
