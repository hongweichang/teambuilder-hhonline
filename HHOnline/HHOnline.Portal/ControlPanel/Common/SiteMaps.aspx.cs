using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Common_SiteMaps : HHPage
{
    public string _url = GlobalSettings.RelativeWebRoot;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "站点地图";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
        this.PagePermission = "SiteMapModule-Edit";
        this.AddJavaScriptInclude("scripts/pages/sitemap.aspx.js", false, false);
    }
}
