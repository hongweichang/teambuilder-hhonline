using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;

public partial class ControlPanel_Masters_ControlPanelMaster :HHMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public override void OnPageLoaded()
    {
        base.OnPageLoaded();
        AddJavaScriptInclude("scripts/jquery.cookie.js", false, true);
        AddJavaScriptInclude("scripts/jquery.jmodal.js", false, false);
        AddJavaScriptInclude("scripts/menu.js", false, true);
        SetValidator(true, true, 3);
    }
}
