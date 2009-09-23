using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_ControlPanel : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "管理中心";
        SetTitle();
        SetTabName(this.ShortTitle);
        AddJavaScriptInclude("scripts/jquery.cookie.js", false, false);
        AddJavaScriptInclude("scripts/pages/controlpanel.aspx.js", false, false);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "HHOnlineUser-View";
        base.OnPermissionChecking(e);
    }
}
