using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;

public partial class ControlPanel_Site_LinkUrlAdd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        LinkUrl lnk = new LinkUrl();
        lnk.Title = txtTitle.Text.Trim();
        lnk.Desc = txtDesc.Text.Trim();
        lnk.Url = txtLink.Text.Trim();
        lnk.CreateTime = DateTime.Now;
        lnk.CreateUser = Profile.AccountInfo.UserID;
        bool r = LinkUrls.LinkUrlAdd(lnk);
        if (r)
            base.ExecuteJs("msg('成功新增一条常用链接！',true);", false);
        else
            base.ExecuteJs("msg('无法新增常用链接，请联系管理员！');", false);
    }
    public override void OnPageLoaded()
    {
        SetValidator(true, true, 5);
        this.PageInfoType = InfoType.PopWinInfo;
        this.PagePermission = "HHOnlineUser-View";
    }
}
