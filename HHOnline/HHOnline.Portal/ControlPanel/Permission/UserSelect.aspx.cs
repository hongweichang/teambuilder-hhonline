using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework;
using HHOnline.Permission.Services;
using HHOnline.Permission.Components;

public partial class ControlPanel_Permission_UserSelect : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            try
            {
                int id = int.Parse(Request.QueryString["ID"]);
                BindOrganize();
                BindUsersInRole(id);
            }
            catch(Exception ex)
            {
                base.ExecuteJs("msg('" + ex.Message + "')",false);
            }
        }
    }

    #region -Event-

    protected void lbAdd_Click(object sender, EventArgs e)
    {
        TreeNodeCollection tnc = tvOrganize.CheckedNodes;
        if (tnc.Count == 0)
        {
            return;
        }
        TreeNode n = null;
        foreach (TreeNode tn in tnc)
        {
            if (!IsInUser(tn.Value))
            {
                n = CreateNode(tn.Text, tn.Value,"images/default/person.gif");
                n.ShowCheckBox = true;
                tvUsers.Nodes.Add(n);
            }
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        TreeNodeCollection tnc = tvUsers.CheckedNodes;
        if (tnc.Count == 0)
        {
            return;
        }
        try
        {
            foreach (TreeNode tn in tnc)
            {
                tvUsers.Nodes.Remove(tn);
            }
        }
        catch{}
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        TreeNodeCollection tnc = tvUsers.Nodes;
        string userIds = string.Empty;
        foreach (TreeNode tn in tnc)
        {
            userIds = userIds + "," + tn.Value;
        }
        UserRole uroles = new UserRole();
        uroles.CreateTime = DateTime.Now;
        uroles.CreateUser = Profile.AccountInfo.UserID;
        uroles.RoleID= int.Parse(Request.QueryString["ID"]);
        uroles.UpdateTime = DateTime.Now;
        uroles.UpdateUser = Profile.AccountInfo.UserID;
        bool r = PermissionManager.AddUsersToRole(userIds, uroles);
        if (r)
            base.ExecuteJs("msg('操作已完成，成功为此角色分配用户！');", false);
        else
            base.ExecuteJs("msg('操作失败，无法为此分配用户！');", false);
    }
    #endregion

    #region -Users-
    bool IsInUser(string userId)
    {
        foreach (TreeNode n in tvUsers.Nodes)
        {
            if (n.Value == userId)
                return true;
        }
        return false;
    }
    void BindUsersInRole(int roleId)
    {
        UserQuery q = new UserQuery();
        q.AccountStatus = AccountStatus.Authenticated;
        q.PageSize = Int32.MaxValue;
        q.RoleID = roleId;
        PagingDataSet<User> pds = Users.GetUsers(q, false);
        TreeNode tn = null;
        foreach (User u in pds.Records)
        {
            tn = CreateNode(u.DisplayName, u.UserID.ToString(),  "images/default/person.gif");
            tn.ShowCheckBox = true;
            tvUsers.Nodes.Add(tn);
        }
    }
    #endregion

    #region -Bind Organization-
    void BindOrganize()
    {
        List<Organization> orgs = Organizations.GetAllOrganizations();
        TreeNode node =null;
        foreach (Organization o in orgs)
        {
            if (o.ParentID == 0)
            {
                node = CreateNode(o.OrganizationName, o.OrganizationID.ToString(), "images/default/department.gif");
                BindChildNode(node);
                tvOrganize.Nodes.Add(node);
            }
        }
    }
    TreeNode CreateNode(string text, string value, string imgUrl)
    {
        TreeNode node = new TreeNode(text, value, GlobalSettings.RelativeWebRoot + imgUrl);
        node.NavigateUrl = "javascript:void(0)";
        node.SelectAction = TreeNodeSelectAction.None;
        node.CollapseAll();
        return node;
    }
    void BindUsers(TreeNode node)
    {
        UserQuery q = new UserQuery();
        q.OrganizationID = int.Parse(node.Value);
        q.PageSize = Int32.MaxValue;
        q.UserType = UserType.InnerUser;
        PagingDataSet<User> pds = Users.GetUsers(q, false);
        TreeNode n = null;
        foreach (User u in pds.Records)
        {
            n = CreateNode(u.DisplayName, u.UserID.ToString(), "images/default/person.gif");
            n.ShowCheckBox = true;
            node.ChildNodes.Add(n);
        }
    }
    void BindChildNode(TreeNode node)
    {
        List<Organization> orgs = Organizations.GetChildOrganizations(int.Parse(node.Value));        
        TreeNode n = null;
        foreach (Organization o in orgs)
        {
            n = CreateNode(o.OrganizationName, o.OrganizationID.ToString(), "images/default/department.gif");
            BindChildNode(n);
            node.ChildNodes.Add(n);
        }
        BindUsers(node);
    }
    #endregion

    #region -Override-
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.ShortTitle = "选择当前角色用户";
        
        AddJavaScriptInclude("scripts/pages/userselect.aspx.js", false, false);
        base.OnPageLoaded();
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "UserRoleModule-Edit";
        base.OnPermissionChecking(e);
    }
    #endregion
}
