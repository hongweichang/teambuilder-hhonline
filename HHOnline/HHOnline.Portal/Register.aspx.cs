using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using System.Reflection;
using HHOnline.Framework;
using System.Web.Security;

public partial class Register : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                throw new HHException(ExceptionType.AccessDenied, "您已经成功登录，无法继续访问注册页面！");
            }
        }
        BindRegion();
    }
    void BindRegion()
    {
        try
        {
            int regId = int.Parse(hfRegionCode.Value);
            if (regId != 0)
            {
                Area a = Areas.GetArea(regId);
                txtRegion.Text = a.RegionName;
            }
        }
        catch { }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        User u = new User();
        u.UserName = txtLoginName.Text.Trim();

        if (Users.GetUser(u.UserName) != null)
        {
            base.ExecuteJs("alert('用户名重复，要完成注册，请尝试其它的用户名！')", false);
            return;
        }
        u.Password = txtPassword.Text.Trim();
        u.DisplayName = txtDisplayName.Text.Trim();
        u.UserType = UserType.CompanyUser;
        u.PasswordQuestion = txtQuestion.Text.Trim();
        u.PasswordAnswer = txtAnswer.Text.Trim();
        u.Email = txtEmail.Text.Trim();
        u.Phone = txtPhone.Text.Trim();
        u.Mobile = txtMobile.Text.Trim();
        u.Fax = txtFax.Text.Trim();
        u.Department = txtDepartment.Text.Trim();
        u.Title = txtTitle.Text.Trim();
        u.Comment = txtMemo.Text.Trim();
        u.AccountStatus = AccountStatus.ApprovalPending;

        Company com = new Company();
        com.CompanyName = txtCompanyName.Text.Trim();
        com.CompanyRegion = int.Parse(hfRegionCode.Value);
        com.Phone = txtCompanyPhone.Text.Trim();
        com.Fax = txtFax.Text.Trim();
        com.Address = txtCompanyAddress.Text.Trim();
        com.Zipcode = txtZipCode.Text.Trim();
        com.Website = txtCompanyWebsite.Text.Trim();
        com.Orgcode = txtOrgCode.Text.Trim();
        com.Regcode = txtIcpCode.Text.Trim();
        com.Remark = txtCompanyMemo.Text.Trim();
        com.CompanyStatus = CompanyStatus.ApprovalPending;
        com.CompanyType = CompanyType.Ordinary;
        
 

        CreateUserStatus status = Users.Create(u, com, true);
        switch (status)
        {
            case CreateUserStatus.Success:
                HttpCookie cookie = FormsAuthentication.GetAuthCookie(u.UserName, true);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(
                                                                                ticket.Version,
                                                                                ticket.Name,
                                                                                ticket.IssueDate,
                                                                                ticket.Expiration,
                                                                                ticket.IsPersistent,
                                                                                DateTime.Now.ToShortDateString());
                cookie.Value = FormsAuthentication.Encrypt(newticket);
                HHCookie.AddCookie(cookie);
                string url = FormsAuthentication.GetRedirectUrl(u.UserName, true);

                throw new HHException(ExceptionType.Success, "注册成功，请返回首页继续浏览！");
                break;
            case CreateUserStatus.DisallowedUsername:
                base.ExecuteJs("注册失败，不能使用此登录名注册，请使用其他名称！",false);
                break;
            case CreateUserStatus.DuplicateUserName:
                base.ExecuteJs("注册失败，此登录名已经被注册，请使用其他名称！",false);
                break;
            case CreateUserStatus.DuplicateEmail:
                base.ExecuteJs("注册失败，此Email已经被注册，请使用其他Email！",false);
                break;
            case CreateUserStatus.UnknownFailure:
                base.ExecuteJs("注册失败，发生了未知的错误，请联系管理员！", false);
                break;
        }

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "用户注册";
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/jquery.password.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/register.aspx.js", false, false);
    }
}
