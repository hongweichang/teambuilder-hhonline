using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Pages_Profiles_Dashboard : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProfile();
        }
        base.ExecuteJs("var _profileidentity='" + User.Identity.Name + "';", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        User u = Profile.AccountInfo;
        u.DisplayName = txtDisplayName.Text.Trim();
        u.PasswordQuestion = txtQuestion.Text.Trim();
        u.Email = txtEmail.Text.Trim();
        u.Mobile = txtMobile.Text.Trim();
        u.Phone = txtPhone.Text.Trim();
        u.Fax = txtFax.Text.Trim();
        u.Remark = txtMemo.Text.Trim();
        u.Title = txtTitle.Text.Trim();
        Users.UpdateUser(u);
    }
    void BindProfile()
    {
        User u = Profile.AccountInfo;
        lblLoginName.Text = u.UserName;
        txtDisplayName.Text = u.DisplayName;
        txtQuestion.Text = u.PasswordQuestion;
        txtEmail.Text = u.Email;
        txtMobile.Text = u.Mobile;
        txtPhone.Text = u.Phone;
        txtFax.Text = u.Fax;
        txtMemo.Text = u.Remark;
        txtTitle.Text = u.Title;
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "个人信息";
        this.SetTabName(this.ShortTitle);
        AddJavaScriptInclude("scripts/pages/dashboard.aspx.js", false, false);
    }
}
