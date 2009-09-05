using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class Masters_HomeMasterPage : HHMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.ExecuteJs("var relativeUrl='" + GlobalSettings.RelativeWebRoot + "';", true);
    }

    public override void OnPageLoaded()
    {
        base.OnPageLoaded();
        AddJavaScriptInclude("scripts/jquery.jmodal.js", false, false);
        AddJavaScriptInclude("scripts/pages/master.aspx.js", false, false);
        SetValidator(true, true, 3);
    }
}
