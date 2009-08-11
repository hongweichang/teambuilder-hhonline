using System;
using System.Collections.Generic;
using System.Text;

namespace HHOnline.Framework.Web.Enums
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum InfoType
    {
        /// <summary>
        /// 继承自模板页的页面提示消息
        /// </summary>
        PageInfo=0,
        /// <summary>
        /// 弹出窗口的提示信息
        /// </summary>
        PopWinInfo = 1,
        /// <summary>
        /// Iframe中页的提示信息
        /// </summary>
        IframeInfo = 2,
    }
}
