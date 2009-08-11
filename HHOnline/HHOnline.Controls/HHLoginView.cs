using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;
using HHOnline.Framework;
using System.Configuration;

namespace HHOnline.Controls
{
    public class HHLoginView:UserControl
    {
        #region -Private-
        static readonly string _leftPanel = "<div class=\"{0}\"></div>";
        static readonly string _middlePanel = "<div class=\"{0}\">{1}</div>";
        static readonly string _msgPanel = "<div class=\"{0}\">{1}</div>";
        static readonly string _loggedPanel = "<a href=\"{0}\" onclick=\"this.blur()\" class=\"{1}\">&nbsp;</a>" +
                                                              "<a href=\"{2}\" onclick=\"this.blur()\" class=\"{3}\">&nbsp;</a>";
        static readonly string _infoPanel = "<table class=\"{0}\">{1}</table>";
        static readonly string _detailPanel = "<tr><th>{0}</th><td>{1}</td></tr>";
        static readonly string _rightPanel = "<div class=\"{0}\"></div>";
        #endregion

        #region -Properties-
        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginUrl { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterUrl { get; set; }
        /// <summary>
        /// 框体左侧样式
        /// </summary>
        public string PanelLeftCss { get; set; }
        /// <summary>
        /// 框体中间样式
        /// </summary>
        public string PanelMiddleCss { get; set; }
        /// <summary>
        /// 框体右侧样式
        /// </summary>
        public string PanelRightCss { get; set; }

        /// <summary>
        /// 登录链接样式
        /// </summary>
        public string LoginCss { get; set; }
        /// <summary>
        /// 注册链接样式
        /// </summary>
        public string RegisterCss { get; set; }

        /// <summary>
        /// 提示消息样式
        /// </summary>
        public string MsgCss { get; set; }

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 用户信息样式
        /// </summary>
        public string InfoCss { get; set; }

        /// <summary>
        /// 个人信息中心Url
        /// </summary>
        public string ProfileUrl { get; set; }
        #endregion

        string RenderView()
        {
            HttpContext context = this.Context;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(_leftPanel, this.PanelLeftCss));
            if (context.User.Identity.IsAuthenticated)
            {
                SettingsPropertyValueCollection spvc = context.Profile.PropertyValues;
                User u = spvc["AccountInfo"].PropertyValue as User;
                StringBuilder sb1 = new StringBuilder();
                sb1.AppendFormat(_detailPanel, "登录名", u.UserName);
                sb1.AppendFormat(_detailPanel, "显示昵称", u.DisplayName);
                sb1.AppendFormat(_detailPanel, "注册时间", u.CreateTime.ToString("yyyy/MM/dd HH:mm:ss"));
                sb1.AppendFormat(_detailPanel, "我的华宏", "<a href=\"" + GlobalSettings.RelativeWebRoot + this.ProfileUrl + "\">[控制面板]</a>");
                sb.AppendFormat(_middlePanel, this.PanelMiddleCss,
                                    string.Format(_infoPanel, this.InfoCss, sb1.ToString())
                                    );
            }
            else
            {
                sb.AppendFormat(_middlePanel, this.PanelMiddleCss,
                    string.Format(_loggedPanel, GlobalSettings.RelativeWebRoot + this.LoginUrl,
                                                              this.LoginCss,
                                                              GlobalSettings.RelativeWebRoot + this.RegisterUrl,
                                                              this.RegisterCss) + string.Format(_msgPanel, this.MsgCss, this.Message));
            }
            sb.AppendFormat(_rightPanel, this.PanelRightCss);
            return sb.ToString();
        }
        public override void RenderControl(HtmlTextWriter writer)
        {
            writer.Write(RenderView());
            writer.WriteLine(Environment.NewLine);
        }
    }
}
