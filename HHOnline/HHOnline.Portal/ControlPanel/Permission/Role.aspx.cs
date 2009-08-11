using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Permission.Components;
using HHOnline.Permission.Services;
using HHOnline.Framework;

public partial class ControlPanel_Permission_Role : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindRoles();
            BindLinkButton();
        }
    }

    #region -ExtendgridView-
    public void egvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Response.Redirect(GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?permission-roleadd&ID=" + egvRoles.DataKeys[e.RowIndex].Value);
    }

    public void egvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int roleId = (int)egvRoles.DataKeys[e.RowIndex].Value;
        RoleOpts result = PermissionManager.DeleteRole(roleId);
        switch (result)
        {
            case RoleOpts.Exist:
                throw new HHException(ExceptionType.Failed, "此角色下存在关联用户，无法直接删除(请先删除此角色下关联用户)！");
            case RoleOpts.Failed:
                throw new HHException(ExceptionType.Failed, "删除角色时失败，请确认此角色存在，并状态正常！");
            case RoleOpts.Success:
                BindRoles();
                break;
        }
    }
    #endregion

    #region -Bind Data-
    void BindLinkButton()
    {
        lbNewRole.PostBackUrl = GlobalSettings.RelativeWebRoot + "controlpanel/controlpanel.aspx?permission-roleadd";
    }
    public void egvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        egvRoles.PageIndex = e.NewPageIndex;
        BindRoles();
    }
    void BindRoles()
    {
        List<Role> roles = PermissionManager.LoadAllRoles();
        egvRoles.DataSource = roles;
        egvRoles.DataBind();
    }
    #endregion

    public override void OnPageLoaded()
    {
        this.ShortTitle = "角色管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.AddJavaScriptInclude("scripts/pages/role.aspx.js", true, false);
    }
    #region -Permission-
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "RoleModule-View";
        e.CheckPermissionControls.Add("RoleModule-Add", lbNewRole);
        base.OnPermissionChecking(e);
    }
    #endregion
}
