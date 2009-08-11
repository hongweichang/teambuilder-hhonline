using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Permission.Services;
using HHOnline.Permission.Components;

public partial class ControlPanel_Permission_UserRole : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindRoles();
        }
    }
    void BindRoles()
    {
        List<Role> roles = PermissionManager.LoadAllRoles();
        rpRoles.DataSource = roles;
        rpRoles.DataBind();
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "UserRoleModule-Edit";
        this.ShortTitle = "角色分配";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        AddJavaScriptInclude("scripts/pages/userrole.aspx.js", false, false);
    }
}
