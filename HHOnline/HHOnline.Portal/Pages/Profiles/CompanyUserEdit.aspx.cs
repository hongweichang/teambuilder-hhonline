using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HF = HHOnline.Framework;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;
public partial class Pages_Profiles_CompanyUserEdit : HHPage
{
    static string lastUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string mode = Request.QueryString["mode"];
        if (!IsPostBack)
        {
            lastUrl = Request.UrlReferrer.ToString();
            if (!string.IsNullOrEmpty(mode) && mode.ToLower() == "add")
            {
                ltPwdDesc.Text = "密码为空时将使用\"hhonline\"作为密码！";
                ltPADesc.Text = "为了密码的安全，请填写密码提示答案！";

            }
            else
            {
                ltPwdDesc.Text = "密码为空时将使用原密码！";
                ltPADesc.Text = "密码提示答案为空时将保持原答案不变！";
                BindUser();
            }
        }
        if (!string.IsNullOrEmpty(mode) && mode.ToLower() == "add")
        {
            base.ExecuteJs("var isAdd=true;", true);
        }
    }
    HF.User u = null;
    void BindUser()
    {
        txtLoginName.ReadOnly = true;
        int userId = int.Parse(Request.QueryString["ID"]);
        u = HF.Users.GetUser(userId);
        txtLoginName.Text = u.UserName;
        txtDisplayName.Text = u.DisplayName;
        txtQuestion.Text = u.PasswordQuestion;
        txtEmail.Text = u.Email;
        rblManager.SelectedValue = (u.IsManager == 1);
        txtPhone.Text = u.Phone;
        txtMobile.Text = u.Mobile;
        txtFax.Text = u.Fax;
        txtTitle.Text = u.Title;
        txtMemo.Text = u.Remark;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string mode = Request.QueryString["mode"];
        if (!string.IsNullOrEmpty(mode) && mode.ToLower() == "add")
        {
            u = Users.GetUser(txtLoginName.Text.Trim());
            if (u != null && u.UserID != 0)
            {
                base.ExecuteJs("alert('此登录名已经被注册，请尝试使用其他用登录名!')", false);
            }
            else
            {
                u = new User();
                u.UserName = txtLoginName.Text.Trim();
                u.Password = txtPassword.Text.Trim();
                if (string.IsNullOrEmpty(u.Password))
                    u.Password = "hhonline";
                u.DisplayName = txtDisplayName.Text.Trim();
                u.PasswordQuestion = txtQuestion.Text.Trim();
                u.PasswordAnswer = txtAnswer.Text.Trim();
                u.IsManager = (rblManager.SelectedValue ? 1 : 2);
                u.Email = txtEmail.Text.Trim();
                u.Mobile = txtMobile.Text.Trim();
                u.Phone = txtPhone.Text.Trim();
                u.Fax = txtFax.Text.Trim();
                u.Title = txtTitle.Text.Trim();
                u.Remark = txtMemo.Text.Trim();
                u.AccountStatus = AccountStatus.Authenticated;
                u.CompanyID = Profile.AccountInfo.CompanyID;
                u.CreateTime = DateTime.Now;
                u.CreateUser = Profile.AccountInfo.UserID;
                u.UpdateTime = DateTime.Now;
                u.UpdateUser = Profile.AccountInfo.UserID;
                u.UserType = UserType.CompanyUser;
                CreateUserStatus s = Users.Create(u);
                switch (s)
                {
                    case CreateUserStatus.Success:
                        base.ExecuteJs("msg('成功添加新用户!',true)", false);
                        break;
                    case CreateUserStatus.DisallowedUsername:
                        base.ExecuteJs("alert('此登录名、显示名被禁止使用，请选择其它名称进行注册!')", false);
                        break;
                    case CreateUserStatus.DuplicateUserName:
                        base.ExecuteJs("alert('登录名重复，请选择其它名称!')", false);
                        break;
                    case CreateUserStatus.DuplicateEmail:
                        base.ExecuteJs("alert('邮件地址已经被注册，请使用其它邮件地址!')", false);
                        break;
                    case CreateUserStatus.UnknownFailure:
                        base.ExecuteJs("alert('发生了未知的错误，无法添加新用户!')", false);
                        break;
                }
            }
        }
        else
        {
            int userId = int.Parse(Request.QueryString["ID"]);
            if (u == null)
            {
                u = HF.Users.GetUser(userId);
            }
            u.DisplayName = txtDisplayName.Text.Trim();
            u.PasswordQuestion = txtQuestion.Text.Trim();
            if (!string.IsNullOrEmpty(txtAnswer.Text.Trim()))
                u.PasswordAnswer = txtAnswer.Text.Trim();
            u.Password = txtPassword.Text;
            u.Email = txtEmail.Text.Trim();
            u.IsManager = (rblManager.SelectedValue ? 1 : 2);
            u.Mobile = txtMobile.Text.Trim();
            u.Phone = txtPhone.Text.Trim();
            u.Fax = txtFax.Text.Trim();
            u.Title = txtTitle.Text.Trim();
            u.Remark = txtMemo.Text.Trim();
            bool result = HF.Users.UpdateUser(u);
            if (result)
                base.ExecuteJs("msg('成功更新用户信息！',true);", false);
            else
                base.ExecuteJs("msg('修改用户信息失败！');", false);
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);
        this.AddJavaScriptInclude("scripts/jquery.password.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/useredit.aspx.js", false, false);
    }
}
