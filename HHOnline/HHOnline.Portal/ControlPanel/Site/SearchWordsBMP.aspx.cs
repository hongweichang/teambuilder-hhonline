using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_Site_SearchWordsBMP : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "热门搜索";
        this.SetTabName(this.ShortTitle);
        this.SetTitle();
        this.AddJavaScriptInclude("scripts/jquery.flash.js", false, false);
        this.AddJavaScriptInclude("scripts/pages/searchwords.aspx.js", false, false);
    }
    protected override void OnPagePermissionChecking()
    {
        this.PagePermission = "SearchWordModule-View";
        base.OnPagePermissionChecking();
    }
}
