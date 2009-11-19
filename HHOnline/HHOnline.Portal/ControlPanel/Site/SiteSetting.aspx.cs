using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_SiteSetting : HHPage
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
        this.txtCopyright.Text = settings.Copyright;
        this.txtSearchMetaDescription.Text = settings.SearchMetaDescription;
        this.txtSearchMetaKeywords.Text = settings.SearchMetaKeywords;
        this.txtSiteDesc.Text = settings.SiteDescription;
        this.txtSiteName.Text = settings.SiteName;
        this.txtIdea.Text = settings.CompanyIdea;
        this.txtService.Text = settings.CompanyService;
        this.txtServiceTel.Text = settings.ServiceTel;
        this.txtICP.Text = settings.CompanyICP;
        this.imgShow.ImageUrl = SiteUrlManager.GetShowPicture((int)this.imgShow.Width.Value, (int)this.imgShow.Height.Value);
    }

    public override void OnPageLoaded()
    {
        this.ShortTitle = "站点信息";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }

    public void btnPost_Click(object sender, EventArgs e)
    {
        SiteSettings settings = SiteSettingsManager.GetSiteSettings();
        settings.Copyright = this.txtCopyright.Text.Trim();
        settings.SearchMetaDescription = this.txtSearchMetaDescription.Text.Trim();
        settings.SearchMetaKeywords = this.txtSearchMetaKeywords.Text.Trim();
        settings.SiteDescription = this.txtSiteDesc.Text.Trim();
        settings.SiteName = this.txtSiteName.Text.Trim();
        settings.CompanyIdea = this.txtIdea.Text;
        settings.CompanyService = this.txtService.Text;
        settings.CompanyICP = this.txtICP.Text.Trim();
        settings.ServiceTel = this.txtServiceTel.Text.Trim();
        if (fuShow.PostedFile != null && fuShow.PostedFile.ContentLength > 0)
        {
            settings.ShowPicture = Path.GetFileName(fuShow.PostedFile.FileName);
            SiteFiles.AddFile(fuShow.PostedFile.InputStream, "ShowPicture", settings.ShowPicture);
        }
        SiteSettingsManager.Save(settings);
        mbMessage.ShowMsg("修改站点信息成功！", Color.Navy);
        BindData();
    }

    protected override void OnPermissionChecking(PermissionCheckingArgs e)
    {
        this.PagePermission = "SiteSettingModule-View";
        e.CheckPermissionControls.Add("SiteSettingModule-Edit", btnPost);
        base.OnPermissionChecking(e);
    }
}
