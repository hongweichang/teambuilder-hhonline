using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;
using HHOnline.Permission.Components;
using HHOnline.Permission.Services;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_RoleDetail : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
            BindRole();
    }
    void BindRole()
    {
        string id = Request.QueryString["ID"];
        int roleId = 0;

        if (string.IsNullOrEmpty(id))
            throw new HHException(ExceptionType.NoMasterError, "无法获取传递到此页面的参数值，请确认未对地址栏Url做任何修改。");
        if (int.TryParse(id, out roleId))
        {
            Role role = PermissionManager.SelectRole(roleId);
            if (role == null)
            {
                throw new HHException(ExceptionType.NoMasterError, "无查询到此角色数据，请确认此角色存在并且未被逻辑删除。");
            }
            else
            {
                List<Role> roles = new List<Role>();
                roles.Add(role);
                rpRole.DataSource = roles;
                rpRole.DataBind();
            }
        }
        else {
            throw new HHException(ExceptionType.NoMasterError, "无法将获取传的参数值转换成数字，请确认未对地址栏Url做任何修改。");
        }
    }
    public string GetUserName(string userId)
    {
        try
        {
            return Users.GetUser(int.Parse(userId)).DisplayName;
        }
        catch
        {
            return "佚名";
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        this.ShortTitle = "角色详细信息";
        SetTitle();
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "RoleModule-View";
        base.OnPermissionChecking(e);
    }
}
