using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

using HHOnline.Framework;

public partial class ControlPanel_Permission_User : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "组织结构";
        this.SetTitle();
        SetTabName(this.ShortTitle);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "OrganizeModule-View";
        base.OnPermissionChecking(e);
    }
}
