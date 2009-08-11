using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Main : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            SetSettings();
        }
        base.ExecuteJs("var showPicture='" + SiteUrlManager.GetShowPicture() + "';var relativeUrl='" + GlobalSettings.RelativeWebRoot + "';", true);
    }
    void SetSettings()
    {
        SiteSettings settings = HHContext.Current.SiteSettings;
        ltPhone.Text = settings.ServiceTel;
        ltIdeal.Text = settings.CompanyIdea;
        ltService.Text = settings.CompanyService;
        //divAdLogo.Style.Add("background-image", GlobalSettings.RelativeWebRoot + "images/default/ad.jpg");
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "首页";
        
        base.OnPageLoaded();
        AddJavaScriptInclude("scripts/jquery.watermark.js", true, false);
        AddJavaScriptInclude("scripts/jquery.flash.js", true, false);
        AddJavaScriptInclude("scripts/pages/main.aspx.js", true, false);
    }
}
