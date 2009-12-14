using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Shops;

public partial class ControlPanel_ProductModal_BatchDelete : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnPost_Click(object sender, EventArgs e) {
        if (ftl1.SelectedValue == HHOnline.Shops.FocusType.None)
        {
            base.ExecuteJs("cancel();", false);
        }
        else {
            string ids = Request.QueryString["ids"];
            int i = Products.BatchSetFocus(ids, ftl1.SelectedValue, Profile.AccountInfo.UserID);
            if (i > 0)
            {
                ftl1.Visible = false;
                mbMsg.ShowMsg("操作成功，更新产品数：" + i + "条。");
            }
            else {
                mbMsg.ShowMsg("无法完成相关操作！", System.Drawing.Color.Red);
            }
            base.ExecuteJs("setTimeout('cancel()',500);", false);
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        this.PagePermission = "ProductModule-Edit";
    }
}
