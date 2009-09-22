using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class CompanyStatusList:DropDownList
    {
        public CompanyStatusList()
        {
            this.Items.AddRange(new ListItem[]{
                    new ListItem(){Text="已审核",Value="Authenticated"},
                    new ListItem(){Text="待审核",Value="ApprovalPending"},
                    new ListItem(){Text="审核未通过",Value="Disapproved"},
                    new ListItem(){Text="公司停用",Value="Lockon"},
                    new ListItem(){Text="删除",Value="Deleted"}
                });
        }
        public new CompanyStatus SelectedValue
        {
            get
            {
                return (CompanyStatus)Enum.Parse(typeof(CompanyStatus), base.SelectedValue);
            }
            set
            {
                base.SelectedValue = value.ToString();
            }
        }
    }
}
