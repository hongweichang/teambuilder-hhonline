using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using System.Drawing;
using HHOnline.Framework.Web.Enums;

public partial class ControlPanel_Permission_UserAdd : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            BindParentDept(id);
        }
        catch (Exception ex)
        {
            base.ExecuteJs("msg('" + ex.Message + "');", false);
        }
    }
    void BindParentDept(int id)
    {
        Organization org = Organizations.GetOrganization(id);
        ltDeptName.Text = org.OrganizationName;
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse(Request.QueryString["ID"]);
            Organization org=Organizations.GetOrganization(id);
            User u = new User();
            u.AccountStatus = AccountStatus.Authenticated;
            u.Comment = txtMemo.Text.Trim();
            u.CreateTime = DateTime.Now;
            u.CreateUser = Profile.AccountInfo.UserID;
            u.Department = ltDeptName.Text;
            u.DisplayName = txtDisplayName.Text.Trim();
            u.Email = txtEmail.Text.Trim();
            u.Fax = txtFax.Text.Trim();
            u.IsManager = rblManager.SelectedValue ? 1 : 2;
            u.LastActiveDate = DateTime.MinValue;
            u.LastLockonDate = DateTime.MinValue;
            u.Mobile = txtMobile.Text.Trim();
            u.OrganizationID = id;
            u.Password = txtPassword.Text.Trim();
            u.Phone = txtPhone.Text.Trim();
            u.Remark = txtMemo.Text.Trim();
            u.Title = txtTitle.Text.Trim();
            u.UpdateTime = DateTime.Now;
            u.UpdateUser = Profile.AccountInfo.UserID;
            u.UserName = txtUserName.Text.Trim();
            u.UserType = UserType.InnerUser;
            CreateUserStatus s = Users.Create(u);
            switch (s)
            {
                case CreateUserStatus.Success:
                    base.ExecuteJs("msg('操作成功，已成功增加一个新的用户，您可以通过“用户级别设置”功能来为其设置级别！',true);", false);
                    break;
                case CreateUserStatus.DisallowedUsername:
                    mbMsg.ShowMsg("此用户名被禁止使用，请使用其他！",Color.Red);
                    break;
                case CreateUserStatus.DuplicateUserName:
                    mbMsg.ShowMsg("此用户名已经被注册，请使用其他！", Color.Red);
                    break;
                case CreateUserStatus.DuplicateEmail:
                    mbMsg.ShowMsg("此电子邮箱已被注册，请使用其他！", Color.Red);
                    break;
                case CreateUserStatus.UnknownFailure:
                    mbMsg.ShowMsg("添加用户时发生了未知的错误，请联系管理员！", Color.Red);
                    break;
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
        this.ShortTitle = "新增用户";
        
        SetValidator(true, true, 5000);
    }
    protected override void OnPagePermissionChecking()
    {
        this.PagePermission = "UserModule-Add";
        base.OnPagePermissionChecking();
    }
}
