using System;
using System.Web.UI.WebControls;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    /// <summary>
    /// 状态选择框
    /// </summary>
    public class ComponentStatusList : RadioButtonList
    {
        private bool _ShowAll = false;
        /// <summary>
        /// 是否显示所有(包含删除)
        /// </summary>
        public bool ShowAll
        {
            get { return _ShowAll; }
            set { _ShowAll = value; }
        }

        /// <summary>
        /// 选择值
        /// </summary>
        public new ComponentStatus SelectedValue
        {
            get
            {
                return (ComponentStatus)(Convert.ToInt32(base.SelectedValue));
            }
            set
            {
                base.SelectedValue = ((int)value).ToString();
            }
        }

        private ComponentStatus _defaultValue = ComponentStatus.Enabled;
        /// <summary>
        /// 默认值
        /// </summary>
        public ComponentStatus DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public ComponentStatusList()
        {
            this.Items.Add(new ListItem("启用", ((int)ComponentStatus.Enabled).ToString()));
            this.Items.Add(new ListItem("停用", ((int)ComponentStatus.Disabled).ToString()));
            if (_ShowAll)
                this.Items.Add(new ListItem("已删除", ((int)ComponentStatus.Deleted).ToString()));
            this.RepeatColumns = this.Items.Count;
            ListItem item = this.Items.FindByValue(((int)_defaultValue).ToString());
            if (item != null)
                item.Selected = true;
        }
    }
}
