using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

public partial class Main : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            SetSettings();
        }
        BindPictures();
    }
    void BindPictures()
    {
        List<ShowPicture> picTemp = ShowPictures.GetShowPictures();
        List<ShowPicture> pics = picTemp.GetRange(0, Math.Min(5, picTemp.Count));
        base.ExecuteJs("var _showPictures = " + Newtonsoft.Json.JavaScriptConvert.SerializeObject(pics) + ";", true);
    }
    void SetSettings()
    {
        SiteSettings settings = HHContext.Current.SiteSettings;
        ltIdeal.Text = settings.CompanyIdea;
        ltService.Text = settings.CompanyService;
        //divAdLogo.Style.Add("background-image", GlobalSettings.RelativeWebRoot + "images/default/ad.jpg");
    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "首页";
        this.SetTitle();
        AddJavaScriptInclude("scripts/jquery.accordion.js", true, false);
        AddJavaScriptInclude("scripts/pages/main.aspx.js", true, false);
    }
}
