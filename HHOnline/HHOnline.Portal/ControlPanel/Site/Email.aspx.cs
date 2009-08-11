using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_Email : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            BindData();
        }
    }

    void BindData()
    {
        SiteSettings settings = SiteSettingsManager.GetSiteSettings();
        this.txtAdminEmail.Text = settings.AdminEmailAddress;
        this.txtEmailThrottle.Text = settings.EmailThrottle.ToString();
        this.txtSmtpServer.Text = settings.SmtpServer;
        this.txtSmtpPort.Text = settings.SmtpPortNumber.ToString();
        this.txtSmtpPwd.Text = settings.SmtpServerPassword;
        this.txtSmtpUserName.Text = settings.SmtpServerUserName;
        this.rblEnableEmail.SelectedValue = settings.EnaleEmail;
        this.rblNeedLogin.SelectedValue = settings.SmtpServerRequiredLogin;
        this.rblUseSSL.SelectedValue = settings.SmtpServerUsingSsl;
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "邮件管理";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }


    public void btnPost_Click(object sender, EventArgs e)
    {
        SiteSettings settings = SiteSettingsManager.GetSiteSettings();
        settings.AdminEmailAddress = this.txtAdminEmail.Text;
        settings.EmailThrottle = GlobalSettings.IsNullOrEmpty(this.txtEmailThrottle.Text) ? -1 : Convert.ToInt32(this.txtEmailThrottle.Text);
        settings.SmtpServer = this.txtSmtpServer.Text;
        settings.SmtpPortNumber = GlobalSettings.IsNullOrEmpty(this.txtSmtpPort.Text) ? 25 : Convert.ToInt32(this.txtSmtpPort.Text);
        settings.SmtpServerPassword = this.txtSmtpPwd.Text;
        settings.SmtpServerUserName = this.txtSmtpUserName.Text;
        settings.EnaleEmail = this.rblEnableEmail.SelectedValue;
        settings.SmtpServerRequiredLogin = this.rblNeedLogin.SelectedValue;
        settings.SmtpServerUsingSsl = this.rblUseSSL.SelectedValue;
        SiteSettingsManager.Save(settings);
        mbMessage.ShowMsg("修改邮件配置信息成功！", Color.Navy);
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "EmailModule-View";
        e.CheckPermissionControls.Add("EmailModule-Edit", btnPost);
        base.OnPermissionChecking(e);
    }
}
