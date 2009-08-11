using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HHOnline.Framework;

namespace HHOnline.Controls
{
    public class AccountStatusList:DropDownList
    {
        public AccountStatusList()
        {
            if (_ShowAll)
            {
                this.Items.AddRange(new ListItem[]{
                    new ListItem(){Text="所有",Value="All"},
                    new ListItem(){Text="匿名",Value="Anonymous"},
                    new ListItem(){Text="已删除",Value="Deleted"}
                });
            }
            this.Items.AddRange(new ListItem[]{
                new ListItem(){Text="待审核",Value="ApprovalPending"},
                new ListItem(){Text="审核通过",Value="Authenticated"},
                new ListItem(){Text="审核未通过",Value="Disapproved"},
                new ListItem(){Text="锁定此用户",Value="Lockon"}
            });
        }
        public new AccountStatus SelectedValue
        {
            get
            {
                return (AccountStatus)Enum.Parse(typeof(AccountStatus), base.SelectedValue);
            }
            set
            {
                base.SelectedValue = value.ToString();
            }
        }
        private bool _ShowAll = false;
        /// <summary>
        /// 显示所有状态，包括所有，匿名和删除三种额外状态
        /// </summary>
        public bool ShowAll
        {
            get { return _ShowAll; }
            set { _ShowAll = value; }
        }
    }
}
