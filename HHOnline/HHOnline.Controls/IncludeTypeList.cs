using System;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;

namespace HHOnline.Controls
{
    public class IncludeTypeList : RadioButtonList
    {
        /// <summary>
        /// 选择值
        /// </summary>
        public new PriceIncludeType SelectedValue
        {
            get
            {
                return (PriceIncludeType)(Convert.ToInt32(base.SelectedValue));
            }
            set
            {
                base.SelectedValue = ((int)value).ToString();
            }
        }

        private PriceIncludeType _defaultValue = PriceIncludeType.Include;
        /// <summary>
        /// 默认值
        /// </summary>
        public PriceIncludeType DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public IncludeTypeList()
        {
            this.Items.Add(new ListItem("包含", ((int)PriceIncludeType.Include).ToString()));
            this.Items.Add(new ListItem("不包含", ((int)PriceIncludeType.Exclude).ToString()));
            this.RepeatColumns = this.Items.Count;
            ListItem item = this.Items.FindByValue(((int)_defaultValue).ToString());
            if (item != null)
                item.Selected = true;
        }
    }
}
