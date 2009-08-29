using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using System.Reflection;
using HHOnline.Framework;

public partial class Register : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRegion();
        }
    }
    void BindRegion()
    {
        string regId = hfRegionCode.Value;
        if (!string.IsNullOrEmpty(regId))
        {
            Area a = Areas.GetArea(regId);
            txtRegion.Text = a.RegionName;
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        User u = new User();
        u.UserName = txtLoginName.Text.Trim();

        if (Users.GetUser(u.UserName) == null)
        {
            base.ExecuteJs("alert('用户名重复，要完成注册，请尝试其它的用户名！')", false);
            return;
        }
        u.Password = txtPassword.Text.Trim();
        u.DisplayName = txtDisplayName.Text.Trim();
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
 

        CreateUserStatus status = Users.Create(u, com, true);
        switch (status)
        {
            case CreateUserStatus.Success:
                break;
            case CreateUserStatus.DisallowedUsername:
                break;
            case CreateUserStatus.DuplicateUserName:
                break;
            case CreateUserStatus.DuplicateEmail:
                break;
            case CreateUserStatus.UnknownFailure:
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
