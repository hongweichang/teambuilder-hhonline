using System;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Shops;

namespace HHOnline.Controls
{
   public class SubCategoryHiddenList : RadioButtonList
    {
          /// <summary>
        /// 选择值
        /// </summary>
        public new SubCategoryHiddenType SelectedValue
        {
            get
            {
                return (SubCategoryHiddenType)(Convert.ToInt32(base.SelectedValue));
            }
            set
            {
                base.SelectedValue = ((int)value).ToString();
            }
        }

        private SubCategoryHiddenType _defaultValue = SubCategoryHiddenType.Visible;
        /// <summary>
        /// 默认值
        /// </summary>
        public SubCategoryHiddenType DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public SubCategoryHiddenList()
        {
            this.Items.Add(new ListItem("可见", ((int)SubCategoryHiddenType.Visible).ToString()));
            this.Items.Add(new ListItem("隐藏", ((int)SubCategoryHiddenType.Hidden).ToString()));
            this.RepeatColumns = this.Items.Count;
            ListItem item = this.Items.FindByValue(((int)_defaultValue).ToString());
            if (item != null)
                item.Selected = true;
        }
    }
}
