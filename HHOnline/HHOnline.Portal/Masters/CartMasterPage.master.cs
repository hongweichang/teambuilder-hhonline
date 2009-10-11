using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;

public partial class Masters_CartMasterPage : HHMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void OnPageLoaded()
    {
        base.OnPageLoaded();
        AddJavaScriptInclude("scripts/jquery.cookie.js", false, true);
        AddJavaScriptInclude("scripts/jquery.jmodal.js", false, false);
        SetValidator(true, true, 3);
    }
}
