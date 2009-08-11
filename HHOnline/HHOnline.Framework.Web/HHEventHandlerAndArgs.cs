using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace HHOnline.Framework.Web
{
    #region -ControlPermissionEventHandler-
    /// <summary>
    /// 控件权限检查后的触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ControlPermissionEventHandler(object sender, ControlPermissionEventArgs e);
    #endregion

    #region -ControlPermissionEventArgs-
    /// <summary>
    /// 权限检查Args
    /// </summary>
    public class ControlPermissionEventArgs
    { 
        private IDictionary<string, Control> _Controls;
       /// <summary>
        /// 已经被检查权限的控件集合
       /// </summary>
        public IDictionary<string, Control> CheckedControls
        {
            get{return _Controls;}
            set{_Controls=value;}
        }

        /// <summary>
        /// 创建<see cref="HHOnline.Framework.Web.ControlPermissionEventArgs"/>的新实例
        /// </summary>
        /// <param name="checkedControls">已经被检查权限的控件集合</param>
        public ControlPermissionEventArgs(IDictionary<string, Control> checkedControls)
        {
            this._Controls = checkedControls;
        }
    }
    #endregion

    #region -PermissionCheckingEventHandle-
    /// <summary>
    /// 校验控件权限时触发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void PermissionCheckingEventHandle(object sender, PermissionCheckingArgs e);
    #endregion

    #region -PermissionCheckingArgs-
    /// <summary>
    /// 权限校验参数
    /// </summary>
    public class PermissionCheckingArgs
    {
        private IDictionary<string, Control> _CheckPermissionControls;
        /// <summary>
        /// 需要进行检查的控件集合
        /// </summary>
        public IDictionary<string, Control> CheckPermissionControls
        {
            get
            {
                if (_CheckPermissionControls == null)
                    return new Dictionary<string, Control>();

                return _CheckPermissionControls;
            }
        }

        private bool _Cancel;
        /// <summary>
        /// 取消权限检查
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _Cancel;
            }
            set
            {
                _Cancel = value;
            }
        }

        public PermissionCheckingArgs(IDictionary<string, Control> controls)
        {
            _CheckPermissionControls = controls;
        }
    }
    #endregion
}
