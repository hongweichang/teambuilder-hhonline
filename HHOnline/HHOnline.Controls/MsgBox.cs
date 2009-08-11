using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;

namespace HHOnline.Controls
{
    /// <summary>
    /// 提示信息条
    /// </summary>
    public class MsgBox:Label
    {
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowMsg(string msg)
        {
            ShowMsg(msg, Color.Black);
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="foreColor">颜色</param>
        public void ShowMsg(string msg, Color foreColor)
        {
            this.Text = msg;
            this.ForeColor = foreColor;
            this.Visible = true;
        }
        /// <summary>
        /// 隐藏提示信息
        /// </summary>
        public void HideMsg()
        {
            this.Visible = false;
        }
    }
}
