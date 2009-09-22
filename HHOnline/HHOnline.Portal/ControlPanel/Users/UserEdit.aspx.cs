using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework.Web.Enums;
using HF = HHOnline.Framework;
using HHOnline.Framework;

public partial class ControlPanel_Users_UserEdit : HHPage
{
    static string lastUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string mode = Request.QueryString["mode"];
        if (!IsPostBack)
        {
            lastUrl = Request.UrlReferrer.ToString();
            if (!string.IsNullOrEmpty(mode)&&mode.ToLower()=="add")
            {
                ltPwdDesc.Text = "密码为空时将使用\"hhonline\"作为密码！";
                ltPADesc.Text = "找回密码时需此答案与问题匹配才能取回密码！";
            }
            else
            {
                ltPwdDesc.Text = "<span class=\"needed\">密码为空时将使用原密码！</span>";
                ltPADesc.Text = "<span class=\"needed\">密码提示答案为空时将保持原答案不变！</span>";
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

        }
        else
        { 
            if(u==null)
            {
                int userId = int.Parse(Request.QueryString["ID"]);
                u = HF.Users.GetUser(userId); 
            }
            u.DisplayName = txtDisplayName.Text.Trim();
            u.PasswordQuestion = txtQuestion.Text.Trim();
            if (!string.IsNullOrEmpty(txtAnswer.Text.Trim()))
                u.PasswordAnswer = txtAnswer.Text.Trim();
            if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
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
                base.ExecuteJs("alert('成功更新用户信息！');", false);
            else
                base.ExecuteJs("alert('修改用户信息失败！');", false);
        }
    }
    protected void btnPostBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lastUrl))
            Response.Redirect(lastUrl);
    }
    public override void OnPageLoaded()
    {
        this.PagePermission = "CorpUserModule-Edit";
        this.PageInfoType = InfoType.IframeInfo;
        SetValidator(true, true, 5000);
        this.AddJavaScriptInclude("scripts/jquery.password.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/useredit.aspx.js", false, false);
    }
}
