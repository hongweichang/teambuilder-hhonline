using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class TestPages_flashupload : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "Flash Upload";
        base.OnPageLoaded();
        this.AddJavaScriptInclude("scripts/jquery.flash.js", false, false);
        this.AddJavaScriptInclude("scripts/jquery.tab.js", false, false);
    }
}
