using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web.Enums;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Cache;
using HHOnline.Permission.Components;

public partial class ControlPanel_Permission_Organize : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindOrganize();
        }
        SetValue();
    }
    private string nodeState = CacheKeyManager.PagePrefix + "SelectedDeptValue/";
    public void tvOganize_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode node = tvOganize.SelectedNode;
        node.Expand();
        HHCookie.AddCookie(nodeState + Profile.AccountInfo.UserName, node.Value, DateTime.Now.AddMinutes(1));
        BindUserDept(int.Parse(node.Value));
        SetValue();
    }
    protected void lnkChildDept_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        int orgID = int.Parse(lnk.Attributes["OrgID"]);
        HHCookie.AddCookie(nodeState + Profile.AccountInfo.UserName, orgID.ToString(), DateTime.Now.AddSeconds(20));
        CheckNode(tvOganize.Nodes, orgID.ToString());
        SetValue();
        BindUserDept(orgID);
    }

    #region -Organization-
    void LoadChild(TreeNode node)
    {
        List<Organization> orgs = Organizations.GetAllChildOrganizations(int.Parse(node.Value));
        if (orgs != null && orgs.Count > 0)
        {
            TreeNode tn = null;
            foreach (Organization o in orgs)
            {
                if (o.ParentID == int.Parse(node.Value))
                {
                    tn = new TreeNode(o.OrganizationName, o.OrganizationID.ToString(), GlobalSettings.RelativeWebRoot + "images/default/department.gif");
                    tn.ToolTip = o.OrganizationDesc;
                    LoadChild(tn);
                    node.ChildNodes.Add(tn);
                }
            }
        }
    }
    void BindOrganize()
    {
        List<Organization> orgs = Organizations.GetAllOrganizations();
        TreeNode tn = null;
        foreach (Organization o in orgs)
        {
            if (o.ParentID == 0)
            {
                tn = new TreeNode(o.OrganizationName, o.OrganizationID.ToString(), GlobalSettings.RelativeWebRoot + "images/default/company.gif");
                tn.ToolTip = o.OrganizationDesc;
                tn.CollapseAll();
                LoadChild(tn);
                tvOganize.Nodes.Add(tn);
            }
        }
        HttpCookie cache = HHCookie.GetCookie(nodeState + Profile.AccountInfo.UserName);
        if (cache != null)
        {
            CheckNode(tvOganize.Nodes, cache.Value);
            SetValue();
            BindUserDept(int.Parse(cache.Value));
        }
    }
    void CheckNode(TreeNodeCollection nodes, string value)
    {
        if (nodes != null && nodes.Count > 0)
        {
            foreach (TreeNode root in nodes)
            {
                if (value == root.Value)
                {
                    root.Select();
                    ExpandParent(root);
                }
                else
                {
                    CheckNode(root.ChildNodes, value);
                }
            }
        }
    }
    void ExpandParent(TreeNode tn)
    {
        TreeNode node = tn;
        while (node != null && node.Parent != null)
        {
            node = node.Parent;
            if (node != null)
            {
                node.Expand();
            }
        }
    }
    void SetValue()
    {
        if (tvOganize.SelectedNode != null)
        {
            ExecuteJs("window.$selectNodeId=" + tvOganize.SelectedValue, false);
        }
    }
    #endregion

    #region -Inner User-
    void BindUserDept(int orgId)
    {
        BindChildDept(orgId);
        BindUser(orgId);
    }
    void BindChildDept(int orgId)
    {
        rpChildDept.DataSource = Organizations.GetChildOrganizations(orgId);
        rpChildDept.DataBind();
    }
    void BindUser(int orgId)
    {
        UserQuery q = new UserQuery();
        q.OrganizationID = orgId;
        q.PageSize = Int32.MaxValue;
        q.UserType = UserType.InnerUser;
        PagingDataSet<User> pds = Users.GetUsers(q, false);
        rpUsers.DataSource = pds.Records;
        rpUsers.DataBind();
    }
    #endregion

    #region -Override-
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        base.OnPageLoaded();

        AddJavaScriptInclude("scripts/jquery.jmodal.js", false, true);
        AddJavaScriptInclude("scripts/pages/organize.aspx.js", false, false);
    }
    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "OrganizeModule-View";
        e.CheckPermissionControls.Add("OrganizeModule-Add", lbAddDept);
        e.CheckPermissionControls.Add("UserModule-Add", lbAddUser);
        e.CheckPermissionControls.Add("UserModule-Delete", lbDeleteUser);
        e.CheckPermissionControls.Add("OrganizeModule-Delete", lbDeleteDept);
        base.OnPermissionChecking(e);
    }
    #endregion
}
