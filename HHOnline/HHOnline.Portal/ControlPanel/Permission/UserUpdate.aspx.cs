using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using System.Drawing;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_UserUpdate : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            if (!IsPostBack && !IsCallback)
            {
                BindOrgs();
                BindUser(id);
            }
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }
    void BindOrgs()
    {
        dlDepartment.Items.Clear();
        List<Organization> os = Organizations.GetAllOrganizations();
        ListItem li = null;
        foreach (Organization o in os)
        {
            if (o.ParentID == 0)
            {
                li = new ListItem(o.OrganizationName, o.OrganizationID.ToString());
                dlDepartment.Items.Add(li);
                BindChildOrgs(o.OrganizationID, 0);
            }
        }
    }
    void BindChildOrgs(int parentId, int deps)
    {
        List<Organization> os = Organizations.GetChildOrganizations(parentId);
        ListItem li = null;
        string block = "┗";
        for (int i = 0; i < deps; i++)
        {
            block = "　" + block;
        }
        foreach (Organization o in os)
        {
            li = new ListItem(block+o.OrganizationName, o.OrganizationID.ToString());
            dlDepartment.Items.Add(li);
            BindChildOrgs(o.OrganizationID, deps + 1);
        }
    }
    void BindUser(int id)
    {
        User u = Users.GetUser(id);
        ltUserName.Text = u.UserName;
        txtDisplayName.Text = u.DisplayName;
        txtEmail.Text = u.Email;
        txtFax.Text = u.Fax;
        txtMemo.Text = u.Remark;
        txtMobile.Text = u.Mobile;
        txtPassword.Text = u.Password;
        txtPhone.Text = u.Phone;
        txtTitle.Text = u.Title;
        dlDepartment.SelectedValue = u.OrganizationID.ToString();
        aslStatus.SelectedValue = u.AccountStatus;
        rblManager.SelectedValue = (u.IsManager == 1 ? true : false);
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            User u = Users.GetUser(id);
            u.AccountStatus = aslStatus.SelectedValue;
            u.Comment = txtMemo.Text.Trim();
            u.CreateTime = DateTime.Now;
            u.CreateUser = Profile.AccountInfo.UserID;
            u.DisplayName = txtDisplayName.Text.Trim();
            u.Email = txtEmail.Text.Trim();
            u.Fax = txtFax.Text.Trim();
            u.IsManager = rblManager.SelectedValue ? 1 : 2;
            u.LastActiveDate = DateTime.MinValue;
            u.LastLockonDate = DateTime.MinValue;
            u.Mobile = txtMobile.Text.Trim();
            u.Password = txtPassword.Text.Trim();
            u.Phone = txtPhone.Text.Trim();
            u.Remark = txtMemo.Text.Trim();
            u.Title = txtTitle.Text.Trim();
            u.UpdateTime = DateTime.Now;
            u.UpdateUser = Profile.AccountInfo.UserID;
            u.UserName = ltUserName.Text;
            u.UserType = UserType.InnerUser;

            Organization o = Organizations.GetOrganization(int.Parse(dlDepartment.SelectedValue));
            u.Department = o.OrganizationName;
            u.OrganizationID = o.OrganizationID;
            bool s = Users.UpdateUser(u);
            if (s)
            {
                base.ExecuteJs("msg('操作成功，已成功更新用户信息，您可以通过“用户级别设置”功能来为其设置级别！',true);", false);

            }
            else
            {
                mbMsg.ShowMsg("更新用户信息时发生了未知的错误，请联系管理员！", Color.Red);
            }
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.PopWinInfo;
        this.ShortTitle = "修改用户信息";
        
        base.OnPageLoaded();
        SetValidator(true, true, 5000);
    }
    protected override void OnPagePermissionChecking()
    {
        this.PagePermission = "UserModule-Edit"; 
        base.OnPagePermissionChecking();
    }
}
