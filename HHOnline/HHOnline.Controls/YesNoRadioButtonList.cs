using System;
using System.Web.UI.WebControls;

namespace HHOnline.Controls
{
    public class YesNoRadioButtonList : RadioButtonList
    {
        private bool _defaultValue=false;
        public bool DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                base.SelectedValue = value.ToString();
            }
        }

        public YesNoRadioButtonList()
        {
            this.Items.Add(new ListItem("是", true.ToString()));
            this.Items.Add(new ListItem("否", false.ToString()));
            this.RepeatColumns = 2;

            this.Items[_defaultValue ? 0 : 1].Selected = true;
        }

        public new bool SelectedValue
        {
            get { return bool.Parse(base.SelectedValue); }
            set { base.SelectedValue = value.ToString(); }
        }
    }
}
