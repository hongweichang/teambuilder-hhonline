using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Permission.Components;
using HHOnline.Permission.Services;
using HHOnline.Cache;
using HHOnline.Framework;
using System.Drawing;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_RoleAdd : HHPage
{
    internal enum RoleAction
    {
        Add = 0,
        Edit = 1
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["ID"];
        int roleId = 0;
        if (!string.IsNullOrEmpty(id))
        {
            if (int.TryParse(id, out roleId))
            {
                action = RoleAction.Edit;
            }
        }
        if (!IsPostBack && !IsCallback)
        {
            switch (action)
            {
                case RoleAction.Add:
                    BindTreeView();
                    break;
                case RoleAction.Edit:
                    btnPost.Text = "更新";
                    BindData(roleId);
                    break;
            }
            if (Request.UrlReferrer != null)
            {
                btnPostBack.PostBackUrl = Request.UrlReferrer.ToString();
                btnPostBack.Visible = true;
            }
            btnPost.Visible = true;
        }
    }
    private RoleAction action = RoleAction.Add;
    public void btnPost_Click(object sender, EventArgs e)
    {
        TreeNodeCollection tnc = tvPermission.CheckedNodes;
        if (tnc.Count == 0)
        {
            mbMessage.ShowMsg("至少应为此角色分配一种权限！", Color.Red);
            return;
        }
        mbMessage.HideMsg(); string moduleActionId = string.Empty;
        foreach (TreeNode tn in tnc)
        {            
            if (tn.Depth == 1)
            {
                moduleActionId += tn.Value + ",";
            }
        }
        switch (action)
        {
            case RoleAction.Add:
                AddRole(moduleActionId);
                break;
            case RoleAction.Edit:
                EditRole(moduleActionId, int.Parse(Request.QueryString["ID"]));
                break;
        }
    }

    #region -Methods-
    void EditRole(string moduleActionId,int roleId)
    {
        Role role = new Role(roleId,txtRoleName.Text.Trim(), txtRoleDesc.Text.Trim(), string.Empty, 1, DateTime.Now, Profile.AccountInfo.UserID, DateTime.Now, Profile.AccountInfo.UserID);
        try
        {
            RoleOpts added = PermissionManager.EditRole(role, moduleActionId);
            switch (added)
            {
                case RoleOpts.Exist:
                    mbMessage.ShowMsg("此角色名已存在，请勿重复使用！", Color.Red);
                    break;
                case RoleOpts.Failed:
                    mbMessage.ShowMsg("修改角色信息失败，信息无法入库！", Color.Red);
                    break;
                case RoleOpts.Success:
                    mbMessage.ShowMsg("修改角色成功，可继续修改角色信息，若完成请返回！", Color.Navy);
                    BindData(roleId);
                    break;
                default:
                    break;
            }
        }
        catch
        {
            throw new HHException(ExceptionType.Failed, "修改用户角色时发生了错误，请联系管理员！");
        }
    }
    void AddRole(string moduleActionId)
    {        
        Role role = new Role(txtRoleName.Text.Trim(), txtRoleDesc.Text.Trim(), string.Empty, 1, DateTime.Now, Profile.AccountInfo.UserID, DateTime.Now, Profile.AccountInfo.UserID);
        try
        {
            RoleOpts added = PermissionManager.AddRole(role, moduleActionId);
            switch (added)
            {
                case RoleOpts.Exist:
                    mbMessage.ShowMsg("此角色名已存在，请勿重复使用！", Color.Red);
                    break;
                case RoleOpts.Failed:
                    mbMessage.ShowMsg("新增角色失败，信息无法入库！", Color.Red);
                    break;
                case RoleOpts.Success:
                    mbMessage.ShowMsg("新增角色成功，可继续填写新角色信息，若完成请返回！", Color.Navy);
                    txtRoleDesc.Text = "";
                    txtRoleName.Text = "";
                    break;
                default:
                    break;
            }
        }
        catch
        {
            throw new HHException(ExceptionType.Failed, "新增用户角色时发生了错误，请联系管理员！");
        }
    }
    void BindData(int roleId)
    {
        Role role = PermissionManager.SelectRole(roleId);
        txtRoleName.Text = role.RoleName;
        txtRoleDesc.Text = role.Description;
        List<ModuleAction> mas = PermissionManager.LoadModuleAction(roleId);
        Dictionary<int, string> dics = new Dictionary<int, string>();
        foreach (ModuleAction m in mas)
        {
            dics.Add(m.ModuleActionId, m.ModuleActionName);
        }
        tvPermission.Nodes.Clear();
        List<Module> modules = PermissionManager.LoadAllModulesFromModuleAction();
        TreeNode tn = null;
        TreeNode n = null;
        List<ModuleAction> actions = null;
        bool allCheck = false;
        foreach (Module m in modules)
        {
            allCheck = true;
            tn = CreateNode(m.ModuleShortName, m.ModuleID.ToString(), m.Description);
            actions = PermissionManager.LoadModuleActions(m.ModuleID);
            foreach (ModuleAction a in actions)
            {
                n = CreateNode(a.ModuleActionName, a.ModuleActionId.ToString(), a.Description);
                if (dics.ContainsKey(a.ModuleActionId))
                {
                    n.Checked = true;
                }
                else
                {
                    allCheck = (allCheck && false);
                }
                tn.ChildNodes.Add(n);
            }
            if (allCheck)
                tn.Checked = true;
            tvPermission.Nodes.Add(tn);
        }
    }
    void BindTreeView()
    {
        tvPermission.Nodes.Clear();
        List<Module> modules = PermissionManager.LoadAllModulesFromModuleAction();
        TreeNode tn = null;
        TreeNode n = null;
        List<ModuleAction> actions = null;
        foreach (Module m in modules)
        {
            tn = CreateNode(m.ModuleShortName, m.ModuleID.ToString(), m.Description);
            actions = PermissionManager.LoadModuleActions(m.ModuleID);
            foreach (ModuleAction a in actions)
            {
                n = CreateNode(a.ModuleActionName, a.ModuleActionId.ToString(), a.Description);
                tn.ChildNodes.Add(n);
            }
            tvPermission.Nodes.Add(tn);
        }
    }

    TreeNode CreateNode(string text, string value,string description)
    {
        TreeNode tn = new TreeNode(text, value);
        tn.ToolTip = description;
        tn.ShowCheckBox = true;
        tn.NavigateUrl = "javascript:void(0)";
        tn.Collapse();
        return tn;
    }
    public override void OnPageLoaded()
    {
        switch (action)
        {
            case RoleAction.Add:
                this.ShortTitle = "新增角色";
                break;
            case RoleAction.Edit:
                this.ShortTitle = "编辑角色";
                break;
            default:
                break;
        }
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.AddJavaScriptInclude("scripts/pages/roleadd.aspx.js", true, false);

        this.PageInfoType = InfoType.PopWinInfo;
    }
    #endregion

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        switch (action)
        {
            case RoleAction.Add:
                this.PagePermission = "RoleModule-Add";
                break;
            case RoleAction.Edit:
                this.PagePermission = "RoleModule-Edit";
                break;
        }
        base.OnPermissionChecking(e);
    }
}
