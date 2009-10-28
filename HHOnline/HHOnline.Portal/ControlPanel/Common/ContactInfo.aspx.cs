using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class ControlPanel_Common_ContactInfo : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        this.ShortTitle = "联系我们";
        this.SetTitle();
        this.SetTabName(this.ShortTitle);
    }
}
