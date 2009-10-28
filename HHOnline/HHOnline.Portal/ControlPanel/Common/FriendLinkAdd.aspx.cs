using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Common_FriendLinkAdd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        FriendLink lnk = new FriendLink();
        lnk.Title = txtTitle.Text.Trim();
        lnk.Url = txtLink.Text.Trim();
        lnk.Rank = int.Parse(ddlRank.SelectedValue);
        lnk.CreateUser = Profile.AccountInfo.UserID;
        lnk.CreateTime = DateTime.Now;
        bool r = FriendLinks.FriendLinkAdd(lnk);
        if (r)
            base.ExecuteJs("msg('成功新增一条友情链接！',true)", false);
        else
            base.ExecuteJs("msg('新增友情链接失败，请联系管理员！')", false);
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.PagePermission = "FriendLinkModule-Add";
        SetValidator(true, true, 5);
    }
}
